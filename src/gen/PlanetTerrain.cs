// DNA-AND · generators · a whole planet's ground, from orbit down to a few metres.
// The planet is a cube blown up into a ball. Each of the 6 faces is split into smaller and smaller square
// patches near the camera (a patch splits into 4), so the land gets more detailed the closer you come.
// Heights and colours come from our own layered noise worked out in double precision. No life is placed:
// bare rock, soil, sand, salt, snow, ice, water and lava only.
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DnaAnd.Core;
using DnaAnd.Render;

namespace DnaAnd.Gen
{
    internal sealed class PlanetTerrain : IDisposable
    {
        public readonly string Kind, Name;
        public readonly double Radius;          // km
        public readonly bool Solid;             // false for gas and ice giants: no ground, only cloud tops
        public double[] StarDir = { 0.78, 0.35, 0.52 };   // direction the starlight comes from (unit length)
        private readonly uint _seed;
        private const int Grid = 16;            // quads along a patch edge
        // deepest split for each layer: the ground goes down to about a millionth of a metre (microbe size),
        // the sea surface to a few centimetres, the clouds to about a kilometre
        private static readonly int[] MaxLevel = { 44, 26, 10, 12 };
        private const int MaxCached = 1600;
        private readonly Dictionary<PKey, Patch> _cache = new Dictionary<PKey, Patch>();
        public const int Ground = 0, Water = 1, Clouds = 2, Hull = 3;

        // ── Dyson shells: a hollow world turned inside out ──
        /// <summary>True for a Dyson shell or ring: the land is on the inside, facing the star at the centre;
        /// the spin's centrifugal gravity holds everything onto the inner surface.</summary>
        public bool Shell { get; private set; }
        public bool Habitable { get; private set; }     // a landscape inside (land, seas, clouds)
        public int Mask;                                // 0 whole, 1 a ring band, 2 part-built, 3 a curved mirror cap (stellar engine)
        public double HullKm = 40;                      // the engineered hull, outside the land
        public float HullR = 0.3f, HullG = 0.31f, HullB = 0.35f, HullGlow = 0.35f;

        /// <summary>A Dyson shell (or ring, or partial shell) around a star, radius in km.</summary>
        public static PlanetTerrain MakeShell(IntPtr device, string name, uint seed, double radiusKm, bool habitable, int mask)
        {
            var t = new PlanetTerrain(device, habitable ? "dyson-shell" : "dyson-hull", name, seed, false, radiusKm);
            t.Shell = true; t.Habitable = habitable; t.Mask = mask;
            return t;
        }
        public readonly bool HasWater, HasClouds;
        public readonly double CloudKm, AirKm;
        public readonly float AirR, AirG, AirB;
        private UploadBuffer _airShell; private int _airCount;

        private struct PKey : IEquatable<PKey>
        {
            public long A, B;
            public bool Equals(PKey o) { return A == o.A && B == o.B; }
            public override bool Equals(object o) { return o is PKey && Equals((PKey)o); }
            public override int GetHashCode() { return (A * 397 ^ B).GetHashCode(); }
        }
        private readonly IntPtr _device;
        private int _frame, _budget;
        private readonly System.Diagnostics.Stopwatch _clock = new System.Diagnostics.Stopwatch();
        private double _msLimit = 9;               // milliseconds per frame spent making new detail
        /// <summary>The size (km) of the finest ground piece ready under the camera. The camera may not go lower
        /// than a few of its steps, so it always lands on finished ground and never inside a rough early piece.</summary>
        public double ReadyStep { get; private set; }
        private readonly StarVertex[] _scratch = new StarVertex[Grid * Grid * 6 + 4 * Grid * 6];
        private readonly double[] _hx = new double[(Grid + 1) * (Grid + 1)], _hy = new double[(Grid + 1) * (Grid + 1)], _hz = new double[(Grid + 1) * (Grid + 1)];
        private readonly double[] _hh = new double[(Grid + 1) * (Grid + 1)];
        private readonly float[] _ca = new float[(Grid + 1) * (Grid + 1)];
        private readonly float[] _cr = new float[(Grid + 1) * (Grid + 1)], _cg = new float[(Grid + 1) * (Grid + 1)], _cb = new float[(Grid + 1) * (Grid + 1)], _cgl = new float[(Grid + 1) * (Grid + 1)];

        private sealed class Patch
        {
            public double Cx, Cy, Cz, Edge; public UploadBuffer Buffer; public int Count; public int LastUsed; public int Level;
        }

        public PlanetTerrain(IntPtr device, string kind, string name, uint seed, bool resourceView = false, double radiusKm = 0)
        {
            _device = device; Kind = kind; Name = name; _seed = seed; ResourceView = resourceView;
            switch (kind)
            {
                case "ocean": Radius = 7400; break;
                case "desert": Radius = 5100; break;
                case "lava": Radius = 4300; break;
                case "snowball": Radius = 5900; break;
                case "tidally-locked": Radius = 6700; break;
                case "gas-giant": Radius = 69911; break;
                case "ice-giant": Radius = 24622; break;
                case "hot-jupiter": Radius = 95000; break;
                default: Radius = 6371; break;
            }
            if (radiusKm > 0) Radius = radiusKm;
            Solid = kind != "gas-giant" && kind != "ice-giant" && kind != "hot-jupiter" && kind != "dyson-hull";
            HasWater = kind == "temperate" || kind == "ocean" || kind == "dyson-shell";
            HasClouds = kind != "dyson-hull";   // giants get a high haze deck over their deep clouds
            // how high the clouds float and how thick the air is (km), and the air's colour
            switch (kind)
            {
                case "desert": CloudKm = 6; AirKm = 70; AirR = 0.95f; AirG = 0.62f; AirB = 0.38f; break;
                case "lava": CloudKm = 12; AirKm = 150; AirR = 1f; AirG = 0.42f; AirB = 0.18f; break;
                case "snowball": CloudKm = 5; AirKm = 50; AirR = 0.6f; AirG = 0.76f; AirB = 1f; break;
                case "tidally-locked": CloudKm = 9; AirKm = 80; AirR = 0.45f; AirG = 0.56f; AirB = 0.9f; break;
                case "gas-giant": CloudKm = 55; AirKm = 2200; AirR = 0.95f; AirG = 0.82f; AirB = 0.6f; break;
                case "ice-giant": CloudKm = 45; AirKm = 1000; AirR = 0.45f; AirG = 0.65f; AirB = 1f; break;
                case "hot-jupiter": CloudKm = 80; AirKm = 4500; AirR = 1f; AirG = 0.55f; AirB = 0.3f; break;
                case "ocean": CloudKm = 10; AirKm = 120; AirR = 0.35f; AirG = 0.56f; AirB = 1f; break;
                default: CloudKm = 9; AirKm = 110; AirR = 0.35f; AirG = 0.56f; AirB = 1f; break;
            }
        }

        // ── shape and colour ─────────────────────────────────────────────

        private static double Hash(long x, long y, long z, uint seed)
        {
            ulong h = (ulong)seed * 0x9E3779B97F4A7C15UL ^ (ulong)x * 0xBF58476D1CE4E5B9UL ^ (ulong)y * 0x94D049BB133111EBUL ^ (ulong)z * 0xD6E8FEB86659FD93UL;
            h ^= h >> 31; h *= 0x7FB5D329728EA185UL; h ^= h >> 27; h *= 0x81DADEF4BC2DD44DUL; h ^= h >> 33;
            return (h >> 11) * (1.0 / 9007199254740992.0);
        }

        private static double Value(double x, double y, double z, uint seed)
        {
            double fx0 = Math.Floor(x), fy0 = Math.Floor(y), fz0 = Math.Floor(z);
            long ix = (long)fx0, iy = (long)fy0, iz = (long)fz0;
            double fx = x - fx0, fy = y - fy0, fz = z - fz0;
            fx = fx * fx * (3 - 2 * fx); fy = fy * fy * (3 - 2 * fy); fz = fz * fz * (3 - 2 * fz);
            double a = Hash(ix, iy, iz, seed), b = Hash(ix + 1, iy, iz, seed), c = Hash(ix, iy + 1, iz, seed), d = Hash(ix + 1, iy + 1, iz, seed);
            double e = Hash(ix, iy, iz + 1, seed), f = Hash(ix + 1, iy, iz + 1, seed), g = Hash(ix, iy + 1, iz + 1, seed), h = Hash(ix + 1, iy + 1, iz + 1, seed);
            double x0 = a + (b - a) * fx, x1 = c + (d - c) * fx, x2 = e + (f - e) * fx, x3 = g + (h - g) * fx;
            double y0 = x0 + (x1 - x0) * fy, y1 = x2 + (x3 - x2) * fy;
            return y0 + (y1 - y0) * fz;
        }

        /// <summary>Layered noise, 0..1. <paramref name="first"/> is the first layer's frequency.</summary>
        private static double Fbm(double x, double y, double z, uint seed, double first, int layers)
        {
            double sum = 0, amp = 0.5, norm = 0, f = first;
            for (int i = 0; i < layers; i++) { sum += Value(x * f + i * 17.3, y * f - i * 9.1, z * f + i * 3.7, seed + (uint)i * 101u) * amp; norm += amp; amp *= 0.5; f *= 2; }
            return sum / norm;
        }

        /// <summary>Layered noise, -1..1, where each finer layer keeps <paramref name="gain"/> of the one before:
        /// a gain near 0.6 keeps real roughness at every scale, from hills down to stones.</summary>
        private static double Rough(double x, double y, double z, uint seed, double first, int layers, double gain)
        {
            double sum = 0, amp = 1, f = first;
            for (int i = 0; i < layers; i++) { sum += (Value(x * f + i * 7.7, y * f - i * 3.3, z * f + i * 1.9, seed + (uint)i * 31u) * 2 - 1) * amp; amp *= gain; f *= 2; }
            return sum;
        }

        /// <summary>Fine roughness: from layer <paramref name="from"/> to <paramref name="to"/>, each layer's bumps are about a
        /// fifth of their own width tall, so the ground looks just as rough at a metre, a millimetre or a micrometre.</summary>
        private double Micro(double x, double y, double z, uint seed, int from, int to)
        {
            double sum = 0, f = 48 * Math.Pow(2, from);
            for (int i = from; i < to; i++)
            {
                double width = (Kind == "dyson-shell" ? 6371 : Radius) / f;   // km across one bump at this layer
                sum += (Value(x * f + i * 3.1, y * f - i * 5.3, z * f + i * 2.9, seed + (uint)i * 13u) * 2 - 1) * width * 0.18;
                f *= 2;
            }
            return sum;
        }

        private static double Ridged(double x, double y, double z, uint seed, double first, int layers)
        {
            double sum = 0, amp = 0.5, norm = 0, f = first;
            for (int i = 0; i < layers; i++)
            {
                double v = 1 - Math.Abs(Value(x * f + 5.1, y * f + i, z * f - 2.3, seed + 7000u + (uint)i) * 2 - 1);
                sum += v * v * amp; norm += amp; amp *= 0.5; f *= 2;
            }
            return sum / norm;
        }

        /// <summary>Ground height in km above the sea for a unit direction, with the colour and glow of the surface there.
        /// <paramref name="layers"/> is how much fine detail to add (more when the camera is close).</summary>
        public double Height(double x, double y, double z, int layers, out float r, out float g, out float b, out float glow)
        {
            if (Kind == "dyson-shell")
            {
                // the inner land has planet-sized features (continents thousands of km across, mountains a few km high):
                // the shell is just far bigger, so it holds the land of millions of Earths
                double sc = Radius / 6371.0;
                int shift = (int)Math.Round(Math.Log(sc, 2));
                _latOverride = Math.Abs(y) * 0.5;              // the shell has no frozen poles: its star is overhead everywhere
                double hs = HeightBase(x * sc, y * sc, z * sc, Math.Max(6, layers - shift), out r, out g, out b, out glow);
                _latOverride = -1;
                if (Mask == 1)
                {
                    // a ring world: tall walls along both edges hold the air in
                    double edge = Math.Abs(y) / 0.045;
                    if (edge > 0.82)
                    {
                        double t = Math.Min(1, (edge - 0.82) / 0.1);
                        hs = Math.Max(hs, t * t * 1500);
                        if (t > 0.3) { float tone = (float)(0.7 + 0.2 * Hash((long)(x * 2e5), (long)(z * 2e5), 0, _seed)); r = 0.42f * tone; g = 0.43f * tone; b = 0.47f * tone; glow = 0; }
                    }
                }
                LastField = -1; return hs;
            }
            double h = HeightBase(x, y, z, layers, out r, out g, out b, out glow);
            if (!Solid) { LastField = -1; return h; }
            h = Landforms(x, y, z, h, layers, ref r, ref g, ref b, ref glow);
            LastField = ClassifyField(x, y, z, h, layers, ref r, ref g, ref b, ref glow);
            if (ResourceView) ViewColour(LastField, h, ref r, ref g, ref b, ref glow);
            return h;
        }

        private double HeightBase(double x, double y, double z, int layers, out float r, out float g, out float b, out float glow)
        {
            glow = 0;
            double lat = _latOverride >= 0 ? _latOverride : Math.Abs(y);
            if (!Solid)
            {
                // a giant's "surface" is its deep cloud deck: billowing tops tens of km high, rough down to the smallest puff
                int puff = Math.Max(0, layers - 6);
                GiantColour(x, y, z, Math.Min(puff, 8), out r, out g, out b);
                double billow = Fbm(x, y, z, _seed + 300u, 30, 3 + Math.Min(puff, 6));
                double towers = Ridged(x, y, z, _seed + 310u, 60, 3 + Math.Min(puff, 5));
                double hh = (billow - 0.5) * 30 + towers * towers * 25 + (puff > 8 ? Micro(x, y, z, _seed + 320u, 8, Math.Min(puff, 30)) * 0.5 : 0);
                float shade = (float)(0.85 + 0.25 * billow);
                r *= shade; g *= shade; b *= shade;
                return hh;
            }
            int fine = Math.Max(0, layers - 6);
            double cont = Fbm(x, y, z, _seed, 1.1, 5) * 2 - 1;                     // continents and ocean basins
            double ridge = Ridged(x, y, z, _seed, 3.0, 5);                          // mountain chains
            double detail = fine > 0 ? Rough(x, y, z, _seed + 55u, 48, Math.Min(fine, 10), 0.62) : 0;   // hills and valleys
            double micro = fine > 8 ? Micro(x, y, z, _seed + 66u, 8, fine) : 0;                         // rocks, stones, grains: the same roughness at every size
            double mtnFine = fine > 0 ? Ridged(x, y, z, _seed + 77u, 96, Math.Min(fine, 12)) : 0.5; // sharp crests and gullies in the mountains
            double tint = Fbm(x, y, z, _seed + 99u, 20, 3 + Math.Min(fine, 8));      // colour variety: patches of different ground
            double soil = Fbm(x, y, z, _seed + 123u, 60, 3 + Math.Min(fine, 10));
            double land, h;
            switch (Kind)
            {
                case "ocean": land = cont - 0.38; break;
                case "desert": land = cont + 0.2; break;
                case "lava": case "snowball": land = cont + 0.1; break;
                default: land = cont + 0.06; break;            // Earth-like: about a third land
            }
            double mtn = ridge * ridge * Smooth(0, 0.35, land);
            _land = land; _mtn = mtn; _tint = tint;
            h = land * 3.2 + mtn * 7.5 + mtn * (mtnFine - 0.5) * 2.2 + detail * (0.12 + 0.35 * Smooth(0, 0.4, land) + 0.6 * mtn) + micro;
            if (Kind == "desert")
            {
                double dune = Math.Sin(x * 900 + Fbm(x, y, z, _seed + 3u, 30, 3) * 40) * 0.5 + 0.5;  // long dune ridges
                h += dune * 0.06 * Smooth(0.0, 0.3, 0.9 - ridge);
            }
            float tv = (float)(0.9 + 0.2 * tint);
            switch (Kind)
            {
                case "lava":
                    double crack = Ridged(x, y, z, _seed + 900u, 5, 4 + Math.Min(fine, 8));
                    if (crack > 0.9 - 0.04 * Math.Min(1, fine / 10.0) && h < 3) { r = 1f; g = (float)(0.3 + 0.35 * tint); b = 0.06f; glow = 0.9f; return h - 0.03; }   // glowing cracks and rivers
                    if (h < -0.3) { r = 1f; g = (float)(0.35 + 0.3 * tint); b = 0.08f; glow = 0.95f; h = -0.3; }   // molten lowlands
                    else { r = 0.16f * tv; g = 0.12f * tv; b = 0.11f * tv; if (h > 4) { r = 0.3f * tv; g = 0.26f * tv; b = 0.24f * tv; } }  // black crust, ash peaks
                    return h;
                case "desert":
                    if (h < -1.2) { r = 0.92f; g = 0.9f; b = 0.84f; }                                                 // salt flats in the old sea beds
                    else if (h < 2.0) { r = 0.82f * tv; g = 0.6f * tv; b = 0.36f * tv; }                               // sand
                    else { r = 0.6f * tv; g = 0.36f * tv; b = 0.24f * tv; }                                            // red rock
                    return h;
                case "snowball":
                    if (h < 0) { r = 0.78f * tv; g = 0.88f * tv; b = 0.97f; return 0; }                                // frozen sea: flat ice
                    if (h > 2.5 || tint > 0.55) { r = 0.95f; g = 0.96f; b = 1f; } else { r = 0.42f * tv; g = 0.43f * tv; b = 0.47f * tv; }
                    return h;
                case "tidally-locked":
                    {
                        double day = x * StarDir[0] + y * StarDir[1] + z * StarDir[2];
                        if (day > 0.3) { if (h < 0) h = h * 0.5; r = 0.55f * tv; g = 0.28f * tv; b = 0.14f * tv; return h; }      // scorched, its seas boiled away
                        if (day < -0.2) { if (h < 0) { r = 0.8f; g = 0.88f; b = 0.96f; return 0; } r = 0.92f; g = 0.94f; b = 0.98f; return h; }  // frozen night side
                        break;                                                                                                // twilight ring: water and bare land
                    }
            }
            if (h < 0)
            {
                if (lat > 0.9 && Kind != "tidally-locked") { r = 0.85f; g = 0.9f; b = 0.95f; return 0.002; }   // sea ice near the poles, floating on top
                if (Kind == "tidally-locked")
                {
                    // the twilight ring keeps its water painted on (no separate sea surface on this world)
                    double depth = Math.Min(1, -h / 4);
                    r = (float)(0.10 - 0.08 * depth); g = (float)(0.34 - 0.26 * depth); b = (float)(0.55 - 0.3 * depth);
                    return 0;
                }
                // the sea floor, under the water: sand in the shallows, then silt and mud, dark rock in the deeps
                if (h > -0.15) { r = 0.66f * tv; g = 0.6f * tv; b = 0.46f * tv; }
                else if (h > -1.5) { r = (float)(0.36 * tv); g = (float)(0.34 * tv); b = (float)(0.3 * tv); }
                else { r = 0.22f * tv; g = 0.22f * tv; b = 0.23f * tv; }
                return h;
            }
            if (h < 0.04) { r = 0.78f * tv; g = 0.72f * tv; b = 0.52f * tv; }                         // beaches
            else if (h < 2.4)
            {
                // bare ground in patches: brown soil, pale clay, dark gravel
                if (soil < 0.42) { r = 0.30f * tv; g = 0.28f * tv; b = 0.26f * tv; }
                else if (soil > 0.6) { r = 0.62f * tv; g = 0.55f * tv; b = 0.45f * tv; }
                else { r = 0.46f * tv; g = 0.38f * tv; b = 0.29f * tv; }
            }
            else if (h < 4.2) { r = 0.48f * tv; g = 0.47f * tv; b = 0.46f * tv; }                    // grey rock
            else { r = 0.94f; g = 0.95f; b = 0.97f; }                                                // snow on the peaks
            if (lat > 0.86 + 0.05 * tint) { r = 0.93f; g = 0.95f; b = 0.99f; }                       // ice caps
            return h;
        }

        private void GiantColour(double x, double y, double z, int extra, out float r, out float g, out float b)
        {
            // bands of cloud, pushed and curled by the winds (layered warping), with many storms of different sizes
            double wx = Fbm(x, y, z, _seed + 11u, 2.5, 4 + extra) - 0.5, wz = Fbm(x, y, z, _seed + 12u, 2.5, 4 + extra) - 0.5;
            double curl = Fbm(x + wx * 0.6, y, z + wz * 0.6, _seed + 13u, 7, 3 + extra) - 0.5;
            double yy = y + wx * 0.06 + curl * 0.05;
            double band = Math.Sin(yy * (Kind == "ice-giant" ? 9 : 19) + curl * 4) * 0.5 + 0.5;
            double fine = Fbm(x + wx, y * 6, z + wz, _seed + 14u, 12, 3 + extra);
            band = band * 0.8 + fine * 0.2;
            if (Kind == "ice-giant") { r = (float)(0.25 + 0.14 * band); g = (float)(0.48 + 0.14 * band); b = (float)(0.85 + 0.12 * band); }
            else if (Kind == "hot-jupiter") { r = (float)(0.32 + 0.5 * band); g = (float)(0.13 + 0.24 * band); b = (float)(0.09 + 0.1 * band); }
            else { r = (float)(0.42 + 0.54 * band); g = (float)(0.26 + 0.62 * band); b = (float)(0.16 + 0.62 * band); }   // brown belts, cream zones
            // storms: round swirling ovals, one great one and many small ones
            var rng = new Rng(_seed ^ 0x570CUL);
            for (int k = 0; k < 14; k++)
            {
                double la = (rng.NextDouble() - 0.5) * 1.6, lo = rng.NextDouble() * Math.PI * 2;
                double size = k == 0 ? 0.16 : 0.03 + rng.NextDouble() * 0.05;
                double sx = Math.Cos(la) * Math.Cos(lo), sy = Math.Sin(la), sz = Math.Cos(la) * Math.Sin(lo);
                double dx = x - sx, dy = (y - sy) * 1.7, dz = z - sz, d = Math.Sqrt(dx * dx + dy * dy + dz * dz);
                if (d > size) continue;
                double t = 1 - d / size, swirl = Math.Sin(d / size * 14 + Math.Atan2(dy, dx + dz) * 2) * 0.5 + 0.5;
                float sr, sg, sb;
                if (Kind == "ice-giant") { sr = 0.08f; sg = 0.15f; sb = 0.42f; }                 // dark storms, like Neptune's
                else if (k == 0) { sr = 0.82f; sg = 0.38f; sb = 0.26f; }                          // the great red storm
                else { sr = 0.96f; sg = 0.93f; sb = 0.86f; }                                      // white ovals
                float m = (float)(Math.Min(1, t * 2.5) * (0.75 + 0.25 * swirl));
                r += (sr - r) * m; g += (sg - g) * m; b += (sb - b) * m;
            }
        }

        // ── volcanoes, rivers and resource fields ───────────────────────

        private double _land, _mtn, _tint, _latOverride = -1;
        /// <summary>The resource field at the last point asked about (index into <see cref="Fields"/>), or -1.</summary>
        public int LastField { get; private set; }
        /// <summary>True: every field lights up in its own colour, buried ones too (the resource view).</summary>
        public readonly bool ResourceView;
        /// <summary>Coal, oil and gas are made from buried life, so they appear only once life has lived and died on this world.</summary>
        public bool LifeHasLived = false;

        public sealed class FieldKind { public string Id, Name, Where; public float R, G, B; public bool Buried; }
        public static readonly List<FieldKind> Fields = new List<FieldKind>();

        // the colour each field shows in the resource view, and whether it is buried out of sight
        private static readonly string[] FieldIds = { "ocean-water", "volcanic-sulfide", "epithermal", "porphyry", "orogenic-gold", "skarn", "pegmatite",
            "placer", "evaporite", "laterite", "banded-iron", "kimberlite", "layered-intrusion", "coal", "oil-gas", "sediment-copper", "ice-field" };
        private static readonly float[,] FieldColour = { { 0.2f, 0.5f, 1f }, { 1f, 0.92f, 0.1f }, { 1f, 0.6f, 0.1f }, { 0.2f, 0.95f, 0.6f }, { 1f, 0.75f, 0f },
            { 0.75f, 0.4f, 1f }, { 1f, 0.5f, 0.8f }, { 1f, 0.88f, 0.45f }, { 1f, 1f, 1f }, { 0.95f, 0.32f, 0.12f }, { 0.8f, 0.1f, 0.1f }, { 0.45f, 0.9f, 1f },
            { 0.65f, 0.65f, 0.25f }, { 0.55f, 0.55f, 0.58f }, { 0.95f, 0.2f, 0.95f }, { 0.2f, 0.75f, 0.25f }, { 0.75f, 0.97f, 1f } };
        private static readonly bool[] FieldBuried = { false, false, false, true, false, true, true, false, false, false, false, true, true, true, true, true, false };

        /// <summary>Reads every field's name and where it is found from content/resource-fields.dnat.</summary>
        public static void LoadFields(ContentLibrary content)
        {
            if (Fields.Count > 0) return;
            for (int i = 0; i < FieldIds.Length; i++)
            {
                var k = new FieldKind { Id = FieldIds[i], Name = FieldIds[i], Where = "", R = FieldColour[i, 0], G = FieldColour[i, 1], B = FieldColour[i, 2], Buried = FieldBuried[i] };
                foreach (var e in content.Entries)
                    if (e.Id == FieldIds[i] && (e.Kind == "deposit" || e.Kind == "field")) { k.Name = e.Name; k.Where = e.Get("where", e.Get("says", "")); }
                Fields.Add(k);
            }
        }

        private static int FieldIndex(string id) { return Array.IndexOf(FieldIds, id); }

        private struct Volcano { public double X, Y, Z, Size, Tall; public bool Active; }
        private Volcano[] _volcanoes;

        private void MakeVolcanoes()
        {
            int count;
            switch (Kind) { case "lava": count = 60; break; case "desert": count = 12; break; case "snowball": count = 8; break; case "ocean": count = 22; break; default: count = 30; break; }
            var rng = new Rng(_seed ^ 0xF1AEUL);
            var list = new List<Volcano>();
            for (int tries = 0; tries < 4000 && list.Count < count; tries++)
            {
                double u = rng.NextDouble() * 2 - 1, a = rng.NextDouble() * Math.PI * 2, rr = Math.Sqrt(1 - u * u);
                double x = Math.Cos(a) * rr, y = u, z = Math.Sin(a) * rr;
                float r0, g0, b0, gl0;
                double h0 = HeightBase(x, y, z, 6, out r0, out g0, out b0, out gl0);
                // volcanoes rise where plates meet (the mountain belts) and on hot spots; on wet worlds some are islands
                bool belt = _mtn > 0.15, hotspot = rng.NextDouble() < 0.25;
                if (!belt && !hotspot) continue;
                if (h0 < -2.5 && Kind != "ocean") continue;
                list.Add(new Volcano { X = x, Y = y, Z = z, Size = (Kind == "lava" ? 0.012 : 0.007) * (0.6 + rng.NextDouble()) * 6371 / Radius,
                    Tall = Kind == "lava" ? 3 + rng.NextDouble() * 6 : 2 + rng.NextDouble() * 4, Active = Kind == "lava" || rng.NextDouble() < 0.4 });
            }
            _volcanoes = list.ToArray();
        }

        private double _volcano, _vent, _river; private bool _activeVent;

        private double Landforms(double x, double y, double z, double h, int layers, ref float r, ref float g, ref float b, ref float glow)
        {
            if (_volcanoes == null) MakeVolcanoes();
            _volcano = 0; _vent = 0; _activeVent = false;
            foreach (var v in _volcanoes)
            {
                double d = 1 - (x * v.X + y * v.Y + z * v.Z);           // about half the angle squared: cheap and smooth
                double s2 = v.Size * v.Size * 0.5;
                if (d > s2 * 9) continue;
                double t = Math.Sqrt(d / s2);                          // 0 at the top, 1 at the foot
                if (t < 1)
                {
                    double cone = Math.Pow(1 - t, 1.6) * v.Tall;
                    double crater = t < 0.12 ? (0.12 - t) / 0.12 * v.Tall * 0.25 : 0;
                    h = Math.Max(h, h * (1 - (1 - t)) + cone - crater);
                    if (1 - t > _volcano) { _volcano = 1 - t; _vent = t < 0.12 ? 1 - t / 0.12 : 0; _activeVent = v.Active; }
                }
                else if (3 - t > 0 && (3 - t) / 2 * 0.3 > _volcano) _volcano = (3 - t) / 2 * 0.3;   // the field of ash and hot springs around it
            }
            if (_volcano > 0.05)
            {
                // dark ash and lava rock on the slopes; a glowing crater if it is active
                float m = (float)Math.Min(1, _volcano * 1.5);
                r += (0.2f - r) * m; g += (0.18f - g) * m; b += (0.17f - b) * m;
                if (_vent > 0 && _activeVent) { r = 1f; g = 0.42f; b = 0.08f; glow = 0.9f; }
            }
            // rivers: thin winding channels through the lowlands, running toward the sea
            _river = 0;
            if (h > 0.02 && h < 2.2 && Kind != "lava" && Kind != "snowball")
            {
                double line = 1 - Math.Abs(Fbm(x, y, z, _seed + 4000u, 22, 3 + Math.Min(Math.Max(0, layers - 6), 6)) * 2 - 1);
                double width = 0.988 - 0.004 * Smooth(0, 2, h);
                if (line > width)
                {
                    _river = (line - width) / (1 - width);
                    h -= 0.03 + 0.05 * _river;
                    if (Kind == "desert") { r = 0.7f; g = 0.55f; b = 0.38f; }            // dry river beds
                    else { r = 0.1f; g = 0.3f; b = 0.5f; }                                  // river water
                }
                else if (line > width - 0.006) _river = -1;                               // the river banks
            }
            return h;
        }

        private bool Spot(double x, double y, double z, int id, double freq, double above)
        {
            return Fbm(x, y, z, _seed + 6000u + (uint)id * 97u, freq, 3) > above;
        }

        /// <summary>Which resource field is here, by real geology, and what it looks like where it shows at the surface.</summary>
        private int ClassifyField(double x, double y, double z, double h, int layers, ref float r, ref float g, ref float b, ref float glow)
        {
            double lat = Math.Abs(y), land = _land, mtn = _mtn;
            if (HasWater && h < -0.0001 && lat < 0.9) return FieldIndex("ocean-water");
            if (lat > 0.86 + 0.05 * _tint || Kind == "snowball" && h <= 0.0001) return FieldIndex("ice-field");
            if (_vent > 0.3) { r = 0.95f; g = 0.85f; b = 0.15f; if (_activeVent) { r = 1f; g = 0.42f; b = 0.08f; } return FieldIndex("volcanic-sulfide"); }   // yellow sulfur crust
            if (_volcano > 0.25) { if (Spot(x, y, z, 2, 30, 0.55)) { r = 0.85f; g = 0.55f; b = 0.2f; } return FieldIndex("epithermal"); }   // hot-spring stains
            if (_river < 0) { if (Spot(x, y, z, 7, 200, 0.62)) { r = 0.9f; g = 0.78f; b = 0.3f; } return FieldIndex("placer"); }   // glints of gold in the banks
            if (_volcano > 0.05 && mtn > 0.1) return FieldIndex("porphyry");
            if (mtn > 0.3)
            {
                if (Spot(x, y, z, 4, 40, 0.6)) { if (Spot(x, y, z, 40, 900, 0.7)) { r = 0.95f; g = 0.8f; b = 0.25f; } return FieldIndex("orogenic-gold"); }   // quartz veins with gold
                if (Spot(x, y, z, 5, 35, 0.64)) { r = 0.3f; g = 0.55f; b = 0.45f; return FieldIndex("skarn"); }        // green copper stains
                if (Spot(x, y, z, 6, 50, 0.66)) return FieldIndex("pegmatite");
            }
            bool dry = Kind == "desert" || Kind == "tidally-locked";
            if (dry && h < -0.8) return FieldIndex("evaporite");                                      // the salt flats (already white)
            if (!dry && h > 0 && h < 0.06 && lat < 0.45 && Spot(x, y, z, 8, 25, 0.62)) { r = 0.93f; g = 0.92f; b = 0.88f; return FieldIndex("evaporite"); }   // salt pans by the sea
            if (Kind == "temperate" || Kind == "ocean")
                if (lat < 0.32 && h > 0.04 && h < 1.2 && mtn < 0.15) { r = 0.62f; g = 0.26f; b = 0.14f; return FieldIndex("laterite"); }   // red tropical soil
            if (land > 0.25 && mtn < 0.08)
            {
                if (Spot(x, y, z, 10, 18, 0.6))
                {
                    double band = Math.Sin(Fbm(x, y, z, _seed + 77u, 300, 3) * 60) * 0.5 + 0.5;            // red and grey stripes
                    if (band > 0.5) { r = 0.55f; g = 0.2f; b = 0.15f; } else { r = 0.4f; g = 0.38f; b = 0.38f; }
                    return FieldIndex("banded-iron");
                }
                if (Spot(x, y, z, 11, 60, 0.74)) { r = 0.3f; g = 0.36f; b = 0.38f; return FieldIndex("kimberlite"); }   // blue-grey pipe rock
                if (Spot(x, y, z, 12, 20, 0.68)) return FieldIndex("layered-intrusion");
            }
            if (h > -0.4 && h < 0.7 && mtn < 0.12)
            {
                // coal, oil and gas form only from buried dead life: none until life has lived and died here
                if (LifeHasLived && Spot(x, y, z, 14, 14, 0.6)) { if (h > 0 && Spot(x, y, z, 41, 400, 0.72)) { r = 0.06f; g = 0.05f; b = 0.05f; } return FieldIndex("oil-gas"); }   // black seeps
                if (LifeHasLived && h > 0 && Spot(x, y, z, 13, 16, 0.6)) return FieldIndex("coal");
                if (Spot(x, y, z, 15, 22, 0.66)) return FieldIndex("sediment-copper");
            }
            return -1;
        }

        private void ViewColour(int field, double h, ref float r, ref float g, ref float b, ref float glow)
        {
            if (field < 0) { float grey = (r + g + b) / 3 * 0.35f; r = g = b = grey; glow = 0; return; }   // no field: dim grey
            r = FieldColour[field, 0]; g = FieldColour[field, 1]; b = FieldColour[field, 2]; glow = 0.55f;  // lit even at night
        }

        /// <summary>Where on the planet a field of this kind can be found (unit direction), searching from a seed.</summary>
        public bool FindField(int field, int start, out double x, out double y, out double z)
        {
            var rng = new Rng(_seed ^ (ulong)(field * 7919 + start * 104729));
            for (int i = 0; i < 20000; i++)
            {
                double u = rng.NextDouble() * 2 - 1, a = rng.NextDouble() * Math.PI * 2, rr = Math.Sqrt(1 - u * u);
                x = Math.Cos(a) * rr; y = u; z = Math.Sin(a) * rr;
                if (x * StarDir[0] + y * StarDir[1] + z * StarDir[2] < 0.35 && i < 15000) continue;   // a sunlit example
                float r0, g0, b0, gl0;
                Height(x, y, z, 10, out r0, out g0, out b0, out gl0);
                if (LastField == field) return true;
            }
            x = y = 0; z = 1; return false;
        }

        /// <summary>The field under a unit direction (with full detail).</summary>
        public int FieldAt(double x, double y, double z) { float r, g, b, gl; Height(x, y, z, 20, out r, out g, out b, out gl); return LastField; }

        private static double Smooth(double a, double b, double v) { double t = Math.Max(0, Math.Min(1, (v - a) / (b - a))); return t * t * (3 - 2 * t); }

        /// <summary>Ground height in km (below 0 is the sea floor) under a unit direction, with every detail: keeps the camera on top of the ground.</summary>
        public double GroundAt(double x, double y, double z)
        {
            float r, g, b, gl;
            return Height(x, y, z, 46, out r, out g, out b, out gl);
        }

        /// <summary>A good place to start the fly-down: on the lit side, on land with hills or mountains in view.</summary>
        public void StartPoint(out double lat, out double lon)
        {
            lat = Math.Asin(StarDir[1]) * 0.7; lon = Math.Atan2(StarDir[2], StarDir[0]);
            if (!Solid) return;
            var rng = new Rng(_seed ^ 0x5EEDUL);
            double best = -1;
            for (int i = 0; i < 4000; i++)
            {
                double la = Math.Asin(rng.NextDouble() * 1.6 - 0.8), lo = rng.NextDouble() * Math.PI * 2;
                double x = Math.Cos(la) * Math.Cos(lo), y = Math.Sin(la), z = Math.Cos(la) * Math.Sin(lo);
                double day = x * StarDir[0] + y * StarDir[1] + z * StarDir[2];
                if (day < 0.45 || day > 0.8) continue;           // a lower star: hills throw shading
                float r, g, b, gl;
                double h = Height(x, y, z, 8, out r, out g, out b, out gl);
                double score = (h > 0.15 && h < 1.8 ? 1 : 0) - Math.Abs(h - 0.8) * 0.2 + (Kind == "lava" && gl > 0 ? -1 : 0);
                if (score > best) { best = score; lat = la; lon = lo; }
            }
        }

        /// <summary>How cloudy the sky is (0..1) straight above a unit direction (in the clouds' own turning frame).</summary>
        public double CloudCover(double x, double y, double z)
        {
            if (!HasClouds) return 0;
            if (!Solid)
            {
                // giants: high clouds stretched out along the bands by the winds
                double gc = Fbm(x * 1.2, y * 9, z * 1.2, _seed + 510u, 2.0, 7);
                return Smooth(0.55, 0.72, gc) * 0.55;
            }
            double c = Fbm(x, y, z, _seed + 500u, 2.4, 7) + 0.1 * Math.Cos(y * 7);
            if (Kind == "lava") c += 0.12;
            if (Kind == "snowball") c -= 0.08;
            if (Kind == "tidally-locked") { double day = x * StarDir[0] + y * StarDir[1] + z * StarDir[2]; c += 0.25 * Smooth(-0.3, 0.3, day) - 0.18; }
            return Smooth(0.54, 0.7, c);
        }

        /// <summary>How far the clouds have drifted round the planet by <paramref name="time"/> seconds (radians).</summary>
        public double CloudTurn(double time) { return time * 0.0035; }

        // ── patches ──────────────────────────────────────────────────────

        private static readonly double[,] FaceN = { { 1, 0, 0 }, { -1, 0, 0 }, { 0, 1, 0 }, { 0, -1, 0 }, { 0, 0, 1 }, { 0, 0, -1 } };
        private static readonly double[,] FaceU = { { 0, 0, -1 }, { 0, 0, 1 }, { 1, 0, 0 }, { 1, 0, 0 }, { 1, 0, 0 }, { -1, 0, 0 } };
        private static readonly double[,] FaceV = { { 0, 1, 0 }, { 0, 1, 0 }, { 0, 0, -1 }, { 0, 0, 1 }, { 0, 1, 0 }, { 0, 1, 0 } };

        private static void Dir(int f, double s, double t, out double x, out double y, out double z)
        {
            x = FaceN[f, 0] + FaceU[f, 0] * s + FaceV[f, 0] * t;
            y = FaceN[f, 1] + FaceU[f, 1] * s + FaceV[f, 1] * t;
            z = FaceN[f, 2] + FaceU[f, 2] * s + FaceV[f, 2] * t;
            double l = Math.Sqrt(x * x + y * y + z * z); x /= l; y /= l; z /= l;
        }

        private static PKey Key(int layer, int f, int level, long i, long j) { return new PKey { A = i | ((long)level << 45) | ((long)f << 51) | ((long)layer << 55), B = j }; }

        private double LayerRadius(int layer)
        {
            if (layer == Hull) return Radius + HullKm;
            if (layer == Clouds) return Shell ? Radius - CloudKm : Radius + CloudKm;
            return Radius;
        }

        /// <summary>Whether this part of a shell has been built (rings, part-built shells and mirror caps have gaps).</summary>
        private bool Built(double x, double y, double z, int level, double angle)
        {
            if (Mask == 0 || level < 1) return true;
            if (Mask == 1) return Math.Abs(y) < 0.045 + angle;                                    // a band around the middle
            if (Mask == 3) return y > 0.25 - angle;                                               // a curved mirror on one side
            if (level < 3) return true;
            return level > 3 || Fbm(x, y, z, _seed + 8100u, 2.2, 3) > 0.47;                       // part-built: whole panels missing
        }

        private Patch Make(int layer, int f, int level, long i, long j)
        {
            double n = Math.Pow(2, level), size = 2.0 / n, s0 = -1 + i * size, t0 = -1 + j * size;
            double cx, cy, cz; Dir(f, s0 + size / 2, t0 + size / 2, out cx, out cy, out cz);
            double baseR = LayerRadius(layer);
            var p = new Patch { Level = level, Edge = size * baseR * 0.9 };
            int layers = Math.Min(46, 6 + level);
            // the piece's centre sits on the real ground, not at sea level, so distances to the camera are true
            double hc = 0;
            if (layer == Ground) { float r0, g0, b0, gl0; hc = Height(cx, cy, cz, layers, out r0, out g0, out b0, out gl0); }
            p.Cx = cx * (baseR + hc); p.Cy = cy * (baseR + hc); p.Cz = cz * (baseR + hc);
            for (int b = 0; b <= Grid; b++)
                for (int a = 0; a <= Grid; a++)
                {
                    double x, y, z; Dir(f, s0 + size * a / Grid, t0 + size * b / Grid, out x, out y, out z);
                    float r, g, bl, gl, al = 1;
                    double h;
                    if (layer == Hull)
                    {
                        // hull plates: a grid of panels with seams, some lit by docks and windows
                        h = 0; gl = HullGlow; al = 1;
                        // plates: each square of the hull grid has its own shade; thin dark seams between them, at two sizes
                        double k1 = Radius / 40000.0, k2 = k1 * 40;
                        double px = x * k1, py = y * k1, pz = z * k1;
                        double fx = px - Math.Floor(px), fy = py - Math.Floor(py), fz = pz - Math.Floor(pz);
                        double qx = x * k2 - Math.Floor(x * k2), qy = y * k2 - Math.Floor(y * k2), qz = z * k2 - Math.Floor(z * k2);
                        double edge = Math.Min(Math.Min(Math.Min(fx, 1 - fx), Math.Min(fy, 1 - fy)), Math.Min(fz, 1 - fz));
                        double edge2 = Math.Min(Math.Min(Math.Min(qx, 1 - qx), Math.Min(qy, 1 - qy)), Math.Min(qz, 1 - qz));
                        bool seam = edge < 0.02 || edge2 < 0.03;
                        float tone = (float)(0.75 + 0.5 * Hash((long)Math.Floor(px), (long)Math.Floor(py), (long)Math.Floor(pz), _seed + 8200u));
                        r = HullR * tone; g = HullG * tone; bl = HullB * tone;
                        if (seam) { r *= 0.55f; g *= 0.55f; bl *= 0.55f; }
                    }
                    else if (layer == Ground) h = Height(x, y, z, layers, out r, out g, out bl, out gl);
                    else if (layer == Water) { h = 0; r = 0.04f; g = 0.18f; bl = 0.34f; gl = 0; al = 0.72f; }
                    else
                    {
                        h = 0; gl = 0; al = (float)(CloudCover(x, y, z) * 0.92);
                        r = g = bl = 0.97f;
                        if (Kind == "lava") { r = 0.24f; g = 0.21f; bl = 0.2f; }                // ash clouds
                        else if (Kind == "ice-giant") { r = 0.85f; g = 0.92f; bl = 1f; }        // methane ice clouds
                        else if (Kind == "hot-jupiter") { r = 0.45f; g = 0.3f; bl = 0.26f; }    // hot dark haze
                        else if (Kind == "gas-giant") { r = 0.98f; g = 0.95f; bl = 0.88f; }     // ammonia ice clouds
                        else if (Kind == "desert") { r = 0.82f; g = 0.62f; bl = 0.42f; al *= 0.4f; }  // thin dust
                    }
                    double rad = Shell && layer != Hull ? baseR - h : baseR + h;   // a shell's land rises inward, toward the star
                    int k = b * (Grid + 1) + a;
                    _hx[k] = x * rad - p.Cx; _hy[k] = y * rad - p.Cy; _hz[k] = z * rad - p.Cz;
                    _cr[k] = r; _cg[k] = g; _cb[k] = bl; _cgl[k] = gl; _hh[k] = h; _ca[k] = al;
                }
            if (Solid && layer == Ground && !Shell)
            {
                double spacing = p.Edge / Grid;
                for (int b = 0; b <= Grid; b++)
                    for (int a = 0; a <= Grid; a++)
                    {
                        int k = b * (Grid + 1) + a;
                        if (_hh[k] <= 0.04 || _cgl[k] > 0) continue;
                        int ka = a < Grid ? k + 1 : k - 1, kb = b < Grid ? k + Grid + 1 : k - Grid - 1;
                        double slope = Math.Max(Math.Abs(_hh[ka] - _hh[k]), Math.Abs(_hh[kb] - _hh[k])) / spacing;
                        float rock = (float)Math.Max(0, Math.Min(1, (slope - 0.7) * 2.0));
                        if (rock <= 0) continue;
                        float rr = 0.40f, rg = 0.38f, rb = 0.36f;
                        if (Kind == "desert") { rr = 0.58f; rg = 0.34f; rb = 0.22f; }
                        if (Kind == "lava") { rr = 0.12f; rg = 0.1f; rb = 0.1f; }
                        _cr[k] += (rr - _cr[k]) * rock; _cg[k] += (rg - _cg[k]) * rock; _cb[k] += (rb - _cb[k]) * rock;
                    }
            }
            int v = 0;
            for (int b = 0; b < Grid; b++)
                for (int a = 0; a < Grid; a++)
                {
                    int k00 = b * (Grid + 1) + a, k10 = k00 + 1, k01 = k00 + Grid + 1, k11 = k01 + 1;
                    Tri(ref v, k00, k10, k11); Tri(ref v, k00, k11, k01);
                }
            // skirts: short walls hanging down from the patch edges, so no cracks show where detail levels meet
            double drop = p.Edge / Grid * 4;
            for (int e = 0; e < (layer == Ground || layer == Hull ? 4 : 0); e++)
                for (int q = 0; q < Grid; q++)
                {
                    int ka, kb;
                    if (e == 0) { ka = q; kb = q + 1; }
                    else if (e == 1) { ka = Grid * (Grid + 1) + q; kb = ka + 1; }
                    else if (e == 2) { ka = q * (Grid + 1); kb = ka + Grid + 1; }
                    else { ka = q * (Grid + 1) + Grid; kb = ka + Grid + 1; }
                    Skirt(ref v, ka, kb, drop, p);
                }
            p.Count = v;
            p.Buffer = new UploadBuffer(_device, (long)v * Marshal.SizeOf(typeof(StarVertex)));
            p.Buffer.Write(_scratch, v);
            return p;
        }

        private void Tri(ref int v, int a, int b, int c)
        {
            float r = (_cr[a] + _cr[b] + _cr[c]) / 3, g = (_cg[a] + _cg[b] + _cg[c]) / 3, bl = (_cb[a] + _cb[b] + _cb[c]) / 3, gl = (_cgl[a] + _cgl[b] + _cgl[c]) / 3;
            Put(ref v, a, r, g, bl, gl); Put(ref v, b, r, g, bl, gl); Put(ref v, c, r, g, bl, gl);
        }

        private void Put(ref int v, int k, float r, float g, float b, float gl)
        {
            _scratch[v++] = new StarVertex { X = (float)_hx[k], Y = (float)_hy[k], Z = (float)_hz[k], Size = gl, R = r, G = g, B = b, A = _ca[k] };
        }

        private void Skirt(ref int v, int a, int b, double drop, Patch p)
        {
            // straight down toward the planet's centre
            double ax = _hx[a] + p.Cx, ay = _hy[a] + p.Cy, az = _hz[a] + p.Cz, al = Math.Sqrt(ax * ax + ay * ay + az * az);
            if (Shell) drop = -drop;                              // inside a shell, "down" is outward, away from the star
            double bx = _hx[b] + p.Cx, by = _hy[b] + p.Cy, bz = _hz[b] + p.Cz, bl = Math.Sqrt(bx * bx + by * by + bz * bz);
            var A = new StarVertex { X = (float)_hx[a], Y = (float)_hy[a], Z = (float)_hz[a], Size = _cgl[a], R = _cr[a], G = _cg[a], B = _cb[a], A = 1 };
            var B = new StarVertex { X = (float)_hx[b], Y = (float)_hy[b], Z = (float)_hz[b], Size = _cgl[b], R = _cr[a], G = _cg[a], B = _cb[a], A = 1 };
            var A2 = A; A2.X = (float)(_hx[a] - ax / al * drop); A2.Y = (float)(_hy[a] - ay / al * drop); A2.Z = (float)(_hz[a] - az / al * drop);
            var B2 = B; B2.X = (float)(_hx[b] - bx / bl * drop); B2.Y = (float)(_hy[b] - by / bl * drop); B2.Z = (float)(_hz[b] - bz / bl * drop);
            _scratch[v++] = A; _scratch[v++] = B; _scratch[v++] = B2;
            _scratch[v++] = A; _scratch[v++] = B2; _scratch[v++] = A2;
        }

        private Patch Get(int layer, int f, int level, long i, long j, bool mayMake)
        {
            PKey key = Key(layer, f, level, i, j);
            Patch p;
            if (_cache.TryGetValue(key, out p)) { p.LastUsed = _frame; return p; }
            if (!mayMake) return null;
            if (level > 1 && (_budget <= 0 || _clock.Elapsed.TotalMilliseconds > _msLimit)) return null;
            _budget--;
            p = Make(layer, f, level, i, j); p.LastUsed = _frame;
            _cache[key] = p;
            return p;
        }

        // ── drawing ──────────────────────────────────────────────────────

        /// <summary>Draws the whole planet as seen from the camera (km, planet centre at 0): ground, sea, clouds and air.
        /// <paramref name="viewRot"/> is the camera's turn only (the camera sits at the origin), so everything is drawn
        /// relative to the camera in double precision and never shakes, from orbit down to microbe size.</summary>
        public void Draw(MeshPipes pipes, double camX, double camY, double camZ, Mat4 viewRot, Mat4 proj, double time, double splitFactor,
            float fogDist, float fr, float fg, float fb, int x, int y, int w, int h, int newPatchBudget)
        {
            _frame++; _budget = newPatchBudget; _clock.Restart(); _msLimit = newPatchBudget >= 400 ? 10 : 30;
            ReadyStep = double.MaxValue;
            float lx = (float)StarDir[0], ly = (float)StarDir[1], lz = (float)StarDir[2];
            if (Shell)
            {
                // a shell: the hull outside, the land inside, both lit by the star at the centre
                Collect(Hull, camX, camY, camZ, splitFactor);
                DrawList(pipes.Solid, 0, camX, camY, camZ, Mat4.Identity, viewRot, proj, lx, ly, lz, fogDist, fr, fg, fb, x, y, w, h);
                if (!Habitable) { Trim(); return; }
            }
            // the ground (and the sea floor)
            Collect(Ground, camX, camY, camZ, splitFactor);
            DrawList(pipes.Solid, 0, camX, camY, camZ, Mat4.Identity, viewRot, proj, lx, ly, lz, fogDist, fr, fg, fb, x, y, w, h);
            // the sea surface: see-through, with glints of starlight
            if (HasWater)
            {
                Collect(Water, camX, camY, camZ, splitFactor);
                DrawList(pipes.Clear, 3, camX, camY, camZ, Mat4.Identity, viewRot, proj, lx, ly, lz, fogDist, fr, fg, fb, x, y, w, h);
            }
            // the clouds: a slowly turning layer, drawn in its own turned frame
            if (HasClouds && !(ResourceView && Solid))   // the resource view hides the clouds so every field shows
            {
                double ang = CloudTurn(time), ca = Math.Cos(ang), sa = Math.Sin(ang);
                double kx = camX * ca - camZ * sa, kz = camX * sa + camZ * ca;            // the camera, turned back into the clouds' frame
                float clx = (float)(lx * ca - lz * sa), clz = (float)(lx * sa + lz * ca);
                Collect(Clouds, kx, camY, kz, splitFactor);
                DrawList(pipes.Clear, 2, kx, camY, kz, Mat4.RotationY((float)ang), viewRot, proj, clx, ly, clz, fogDist, fr, fg, fb, x, y, w, h);
            }
            if (!Shell) DrawAir(pipes.Glow, camX, camY, camZ, viewRot, proj, lx, ly, lz, x, y, w, h);
            Trim();
        }

        private int _camF; private double _camS, _camT;

        private void Collect(int layer, double camX, double camY, double camZ, double splitFactor)
        {
            // which cube face, and where on it, lies straight under the camera
            double cl = Math.Sqrt(camX * camX + camY * camY + camZ * camZ), dx = camX / cl, dy = camY / cl, dz = camZ / cl, best = -2;
            for (int f = 0; f < 6; f++)
            {
                double k = FaceN[f, 0] * dx + FaceN[f, 1] * dy + FaceN[f, 2] * dz;
                if (k > best) { best = k; _camF = f; }
            }
            double kk = best;
            _camS = (FaceU[_camF, 0] * dx + FaceU[_camF, 1] * dy + FaceU[_camF, 2] * dz) / kk;
            _camT = (FaceV[_camF, 0] * dx + FaceV[_camF, 1] * dy + FaceV[_camF, 2] * dz) / kk;
            _drawList.Clear();
            double[] fd = new double[6]; int[] fo = { 0, 1, 2, 3, 4, 5 };
            for (int f = 0; f < 6; f++) fd[f] = -(FaceN[f, 0] * camX + FaceN[f, 1] * camY + FaceN[f, 2] * camZ);
            Array.Sort(fd, fo);
            for (int q = 0; q < 6; q++) Visit(layer, fo[q], 0, 0, 0, camX, camY, camZ, splitFactor);
        }

        private void DrawList(MeshDrawer drawer, int surface, double camX, double camY, double camZ, Mat4 turn, Mat4 viewRot, Mat4 proj,
            float lx, float ly, float lz, float fogDist, float fr, float fg, float fb, int x, int y, int w, int h)
        {
            Mat4 rest = turn * viewRot * proj;
            foreach (var p in _drawList)
            {
                float ox = (float)(p.Cx - camX), oy = (float)(p.Cy - camY), oz = (float)(p.Cz - camZ);
                Mat4 vp = Mat4.Translation(ox, oy, oz) * rest;
                var c = Pack(vp);
                c[16] = lx; c[17] = ly; c[18] = lz;
                if (Shell)
                {
                    // inside a shell the star is at the centre: light comes from the centre toward each piece
                    double pl = Math.Sqrt(p.Cx * p.Cx + p.Cy * p.Cy + p.Cz * p.Cz);
                    c[16] = (float)(-p.Cx / pl); c[17] = (float)(-p.Cy / pl); c[18] = (float)(-p.Cz / pl);
                }
                c[20] = -ox; c[21] = -oy; c[22] = -oz;          // the camera, in the patch's own frame
                c[25] = fogDist; c[26] = surface; c[28] = fr; c[29] = fg; c[30] = fb;
                drawer.Draw(p.Buffer, p.Count, c, x, y, w, h);
            }
        }

        private static float[] Pack(Mat4 vp)
        {
            var c = new float[BillboardPipeline.ConstantCount];
            c[0] = vp.M11; c[1] = vp.M12; c[2] = vp.M13; c[3] = vp.M14; c[4] = vp.M21; c[5] = vp.M22; c[6] = vp.M23; c[7] = vp.M24;
            c[8] = vp.M31; c[9] = vp.M32; c[10] = vp.M33; c[11] = vp.M34; c[12] = vp.M41; c[13] = vp.M42; c[14] = vp.M43; c[15] = vp.M44;
            return c;
        }

        // the air: one glowing shell around the planet, brightest at its edge, seen from space and from the ground alike
        private void DrawAir(MeshDrawer drawer, double camX, double camY, double camZ, Mat4 viewRot, Mat4 proj, float lx, float ly, float lz, int x, int y, int w, int h)
        {
            if (_airShell == null)
            {
                const int Lon = 96, Lat = 48;
                var v = new StarVertex[Lon * Lat * 6]; int n = 0;
                double rr = Radius + AirKm;
                for (int a = 0; a < Lat; a++)
                    for (int o = 0; o < Lon; o++)
                    {
                        double la0 = -Math.PI / 2 + Math.PI * a / Lat, la1 = -Math.PI / 2 + Math.PI * (a + 1) / Lat;
                        double lo0 = Math.PI * 2 * o / Lon, lo1 = Math.PI * 2 * (o + 1) / Lon;
                        StarVertex p00 = AirPoint(la0, lo0, rr), p10 = AirPoint(la0, lo1, rr), p01 = AirPoint(la1, lo0, rr), p11 = AirPoint(la1, lo1, rr);
                        v[n++] = p00; v[n++] = p10; v[n++] = p11; v[n++] = p00; v[n++] = p11; v[n++] = p01;
                    }
                _airCount = n;
                _airShell = new UploadBuffer(_device, (long)n * Marshal.SizeOf(typeof(StarVertex)));
                _airShell.Write(v, n);
            }
            // the shell is in the planet's frame; the camera is far smaller than the shell's size, so plain floats are enough here
            Mat4 vp = Mat4.Translation((float)-camX, (float)-camY, (float)-camZ) * viewRot * proj;
            var c = Pack(vp);
            c[16] = lx; c[17] = ly; c[18] = lz;
            c[20] = (float)camX; c[21] = (float)camY; c[22] = (float)camZ;
            c[26] = 1;
            drawer.Draw(_airShell, _airCount, c, x, y, w, h);
        }

        private StarVertex AirPoint(double lat, double lon, double r)
        {
            return new StarVertex { X = (float)(Math.Cos(lat) * Math.Cos(lon) * r), Y = (float)(Math.Sin(lat) * r), Z = (float)(Math.Cos(lat) * Math.Sin(lon) * r), R = AirR, G = AirG, B = AirB, A = 1 };
        }

        private readonly List<Patch> _drawList = new List<Patch>();

        private void Visit(int layer, int f, int level, long i, long j, double camX, double camY, double camZ, double splitFactor)
        {
            Patch p = Get(layer, f, level, i, j, true);
            if (p == null) return;
            double dx = p.Cx - camX, dy = p.Cy - camY, dz = p.Cz - camZ, dist = Math.Sqrt(dx * dx + dy * dy + dz * dz);
            // past the horizon: hidden by the planet itself
            double camDist = Math.Sqrt(camX * camX + camY * camY + camZ * camZ), r = LayerRadius(layer);
            // gaps in a ring, a part-built shell or a mirror cap
            if (Shell)
            {
                double pl = Math.Sqrt(p.Cx * p.Cx + p.Cy * p.Cy + p.Cz * p.Cz);
                if (!Built(p.Cx / pl, p.Cy / pl, p.Cz / pl, level, p.Edge / Radius * 0.8)) return;
            }
            if (level >= 2 && camDist > r && !Shell)
            {
                double horizon = Math.Sqrt(camDist * camDist - Radius * Radius) + Math.Sqrt(Math.Max(0, r * r - Radius * Radius));
                if (dist > horizon + p.Edge * 1.6 + 12) return;
            }
            bool mustSplit = Shell && Mask != 0 && level < 4;     // rings and gaps show only once the shell is cut into small enough pieces
            if (level < MaxLevel[layer] && (dist < p.Edge * splitFactor || mustSplit))
            {
                // the four smaller pieces, nearest to the camera first, so detail fills in where you are looking from
                long[] ci = { i * 2, i * 2 + 1, i * 2, i * 2 + 1 }, cj = { j * 2, j * 2, j * 2 + 1, j * 2 + 1 };
                double[] cd = new double[4]; int[] order = { 0, 1, 2, 3 };
                double n = Math.Pow(2, level + 1), size = 2.0 / n;
                for (int q = 0; q < 4; q++)
                {
                    double x, y, z; Dir(f, -1 + (ci[q] + 0.5) * size, -1 + (cj[q] + 0.5) * size, out x, out y, out z);
                    double rr = LayerRadius(layer);
                    cd[q] = (x * rr - camX) * (x * rr - camX) + (y * rr - camY) * (y * rr - camY) + (z * rr - camZ) * (z * rr - camZ);
                }
                Array.Sort(cd, order);
                bool all = true;
                for (int q = 0; q < 4; q++) if (Get(layer, f, level + 1, ci[order[q]], cj[order[q]], true) == null) all = false;
                if (all)
                {
                    for (int q = 0; q < 4; q++) Visit(layer, f, level + 1, ci[order[q]], cj[order[q]], camX, camY, camZ, splitFactor);
                    return;
                }
            }
            if ((layer == Ground || layer == Hull && !Habitable) && f == _camF)
            {
                double sz = 2.0 / Math.Pow(2, level), s0 = -1 + i * sz, t0 = -1 + j * sz;
                if (_camS >= s0 && _camS <= s0 + sz && _camT >= t0 && _camT <= t0 + sz) ReadyStep = p.Edge / Grid;
            }
            _drawList.Add(p);
        }

        private void Trim()
        {
            if (_cache.Count <= MaxCached) return;
            var old = new List<PKey>();
            foreach (var kv in _cache) if (kv.Value.Level > 1 && _frame - kv.Value.LastUsed > 30) old.Add(kv.Key);
            foreach (PKey k in old) { _cache[k].Buffer.Dispose(); _cache.Remove(k); }
        }

        public void Dispose()
        {
            foreach (var p in _cache.Values) p.Buffer.Dispose();
            _cache.Clear();
            if (_airShell != null) _airShell.Dispose();
        }
    }
}
