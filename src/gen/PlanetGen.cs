// DNA-AND · generators · a single planet or giant seen up close, built from content/planets.dnat and gas-giants.dnat.
// Same method as the stars: a ball of faceted points (Populous-like), only the near side drawn each frame,
// lit from one side by its star, with land and sea, ice caps, clouds, bands, an air glow and rings.
using System;
using System.Collections.Generic;
using DnaAnd.Core;
using DnaAnd.Render;

namespace DnaAnd.Gen
{
    internal sealed class PlanetSurface
    {
        public string Kind; public Vec3[] Normals; public float Size; public uint Seed;
        public readonly List<Vec3> Rings = new List<Vec3>(); public readonly List<float> RingShade = new List<float>();
        public int MaxPoints { get { return Normals.Length + Rings.Count; } }
        public Vec3 Light = new Vec3(-0.8f, 0.25f, 0.55f);   // direction the starlight comes from (made unit length below)

        public int Draw(Vec3 toEye, double time, StarVertex[] outV)
        {
            int n = 0;
            float len = (float)Math.Sqrt(Vec3.Dot(Light, Light)); Vec3 L = Light * (1f / len);
            bool locked = Kind == "tidally-locked";
            double spin = locked ? 0 : time * 0.04;                // slow turning; a tidally locked world never turns to its star
            float cs = (float)Math.Cos(spin), sn = (float)Math.Sin(spin);
            float t = (float)time;
            for (int i = 0; i < Normals.Length; i++)
            {
                Vec3 q = Normals[i];
                float facing = Vec3.Dot(q, toEye);
                if (facing < -0.05f) continue;
                // the surface turns: look the colour up at the point it came from
                var m = new Vec3(q.X * cs + q.Z * sn, q.Y, -q.X * sn + q.Z * cs);
                float r, g, b;
                Colour(m, q, L, t, out r, out g, out b);
                float day = Vec3.Dot(q, L);
                float light = 0.06f + 0.94f * Math.Max(0f, Math.Min(1f, day * 1.4f + 0.15f));   // soft line between day and night
                if (Kind == "lava") light = Math.Max(light, 0.55f);  // molten rock glows on its own
                light *= 0.55f + 0.45f * Math.Max(0f, facing);
                outV[n++] = SpaceGen.Star(q, Size, r * light * 0.62f, g * light * 0.62f, b * light * 0.62f, 1f);
            }
            // rings: a ring point hidden behind the planet's ball is left out
            for (int i = 0; i < Rings.Count; i++)
            {
                Vec3 p = Rings[i];
                float along = Vec3.Dot(p, toEye);
                if (along < 0)
                {
                    Vec3 side = p - toEye * along;
                    if (Vec3.Dot(side, side) < 1f) continue;
                }
                outV[n++] = SpaceGen.Star(p, 0.02f, 0.85f, 0.78f, 0.62f, RingShade[i]);
            }
            return n;
        }

        private void Colour(Vec3 m, Vec3 q, Vec3 L, float t, out float r, out float g, out float b)
        {
            float h = Noise.Fractal(m.X * 2.2f + 3, m.Y * 2.2f, m.Z * 2.2f + 7, Seed, 5);          // height
            float fine = Noise.Fractal(m.X * 9, m.Y * 9, m.Z * 9, Seed + 5, 2);
            float lat = Math.Abs(m.Y);
            float cloud = Noise.Fractal(m.X * 3 + t * 0.03f, m.Y * 5, m.Z * 3 - t * 0.02f, Seed + 40, 4);
            switch (Kind)
            {
                case "ocean":
                    Mix(0.05f, 0.18f, 0.45f, 0.10f, 0.35f, 0.62f, h, out r, out g, out b);
                    if (lat > 0.88f) Set(0.85f, 0.9f, 0.95f, out r, out g, out b);
                    break;
                case "desert":
                    Mix(0.55f, 0.36f, 0.18f, 0.88f, 0.70f, 0.45f, h + 0.2f * (fine - 0.5f), out r, out g, out b);
                    cloud = 0;
                    break;
                case "lava":
                    {
                        float crack = Math.Abs(Noise.Fractal(m.X * 4, m.Y * 4, m.Z * 4, Seed + 3, 4) - 0.5f);
                        if (crack < 0.06f) Set(1f, 0.55f + 0.3f * (0.06f - crack) * 8, 0.12f, out r, out g, out b);   // glowing rivers of lava
                        else Set(0.16f + 0.1f * fine, 0.07f, 0.05f, out r, out g, out b);                                // cooled black crust
                        cloud = 0;
                    }
                    break;
                case "snowball":
                    Mix(0.62f, 0.72f, 0.82f, 0.95f, 0.97f, 1f, h + 0.3f * (fine - 0.5f), out r, out g, out b);
                    cloud *= 0.5f;
                    break;
                case "tidally-locked":
                    {
                        float d = Vec3.Dot(q, L);
                        if (d > 0.35f) Mix(0.45f, 0.20f, 0.10f, 0.85f, 0.55f, 0.30f, h, out r, out g, out b);            // scorched day side
                        else if (d < -0.15f) Mix(0.55f, 0.65f, 0.78f, 0.9f, 0.94f, 1f, h, out r, out g, out b);         // frozen night side
                        else Mix(0.12f, 0.30f, 0.20f, 0.30f, 0.45f, 0.25f, h, out r, out g, out b);                     // twilight ring: the only mild land
                        cloud = 0;
                    }
                    break;
                case "gas-giant":
                case "hot-jupiter":
                    {
                        bool hot = Kind == "hot-jupiter";
                        float band = (float)Math.Sin(m.Y * 16 + 2.5f * Noise.Fractal(m.X * 2 + t * 0.05f, m.Y * 3, m.Z * 2, Seed + 11, 3));
                        if (hot) Mix(0.25f, 0.10f, 0.08f, 0.75f, 0.38f, 0.20f, 0.5f + 0.5f * band, out r, out g, out b);
                        else Mix(0.62f, 0.45f, 0.30f, 0.95f, 0.88f, 0.75f, 0.5f + 0.5f * band, out r, out g, out b);
                        // one great storm
                        var spot = new Vec3(0.75f, -0.35f, 0.56f);
                        float sd = Vec3.Dot(m, spot * (1f / (float)Math.Sqrt(Vec3.Dot(spot, spot))));
                        if (sd > 0.96f) Set(hot ? 1f : 0.85f, hot ? 0.55f : 0.40f, hot ? 0.25f : 0.28f, out r, out g, out b);
                        cloud = 0;
                    }
                    break;
                case "ice-giant":
                    {
                        float band = (float)Math.Sin(m.Y * 9 + 1.2f * Noise.Fractal(m.X * 2 + t * 0.04f, m.Y * 2, m.Z * 2, Seed + 12, 2));
                        Mix(0.22f, 0.45f, 0.85f, 0.38f, 0.62f, 0.95f, 0.5f + 0.25f * band, out r, out g, out b);
                        cloud = cloud > 0.72f ? cloud : 0;     // a few bright high clouds
                    }
                    break;
                default: // temperate, Earth-like
                    if (h < 0.5f) Mix(0.03f, 0.12f, 0.38f, 0.06f, 0.28f, 0.55f, h * 2, out r, out g, out b);           // sea
                    else if (h < 0.53f) Set(0.70f, 0.64f, 0.42f, out r, out g, out b);                                // beaches
                    else if (h < 0.66f) Mix(0.12f, 0.42f, 0.12f, 0.30f, 0.45f, 0.16f, (h - 0.53f) * 7.7f, out r, out g, out b); // forest and grass
                    else Mix(0.42f, 0.36f, 0.30f, 0.92f, 0.92f, 0.95f, (h - 0.66f) * 3f, out r, out g, out b);         // mountains and snow
                    if (lat > 0.85f - 0.1f * fine) Set(0.92f, 0.95f, 1f, out r, out g, out b);                        // ice caps
                    break;
            }
            if (cloud > 0.6f) { float c = Math.Min(1f, (cloud - 0.6f) * 4f); r += (1 - r) * c; g += (1 - g) * c; b += (1 - b) * c; }
        }

        private static void Set(float r0, float g0, float b0, out float r, out float g, out float b) { r = r0; g = g0; b = b0; }
        private static void Mix(float r0, float g0, float b0, float r1, float g1, float b1, float t, out float r, out float g, out float b)
        {
            t = Math.Max(0f, Math.Min(1f, t));
            r = r0 + (r1 - r0) * t; g = g0 + (g1 - g0) * t; b = b0 + (b1 - b0) * t;
        }
    }

    internal static class PlanetGen
    {
        /// <summary>The planet's living surface (redrawn each frame) and its fixed extras: the air glow and any rings.</summary>
        public static StarVertex[] Build(ContentLibrary content, string id, ulong seed, out string name, out PlanetSurface surface)
        {
            name = id;
            foreach (var e in content.OfKind("planets.dnat", "planet")) if (e.Id == id) name = e.Name;
            foreach (var e in content.OfKind("gas-giants.dnat", "giant")) if (e.Id == id) name = e.Name;
            bool giant = id == "gas-giant" || id == "ice-giant" || id == "hot-jupiter";
            int count = giant ? 6000 : 5000;
            var normals = new Vec3[count];
            for (int i = 0; i < count; i++)
            {
                double yv = 1 - 2 * (i + 0.5) / count, rr = Math.Sqrt(1 - yv * yv), th = i * 2.399963229728653;
                normals[i] = new Vec3((float)(Math.Cos(th) * rr), (float)yv, (float)(Math.Sin(th) * rr));
            }
            surface = new PlanetSurface { Kind = id, Normals = normals, Seed = (uint)(seed & 0xFFFFFFFF),
                Size = (float)Math.Sqrt(4 * Math.PI / count) * 0.62f };

            var rng = new Rng(seed);
            var list = new List<StarVertex>();
            // the air: a thin glowing edge, coloured by what the air is made of
            float ar = 0.35f, ag = 0.55f, ab = 1f, thick = 1.06f; int air = 320;
            if (id == "desert") { ar = 0.9f; ag = 0.6f; ab = 0.35f; }
            if (id == "lava") { ar = 1f; ag = 0.35f; ab = 0.1f; thick = 1.08f; }
            if (id == "tidally-locked") { air = 160; }
            if (id == "hot-jupiter") { ar = 1f; ag = 0.5f; ab = 0.25f; thick = 1.12f; air = 500; }   // hot and puffed up
            if (id == "gas-giant") { ar = 0.9f; ag = 0.8f; ab = 0.6f; }
            for (int i = 0; i < air; i++)
            {
                double u = rng.NextDouble() * 2 - 1, a = rng.NextDouble() * Math.PI * 2, rr = Math.Sqrt(1 - u * u);
                float h = rng.Range(1.01f, thick);
                list.Add(SpaceGen.Star(new Vec3((float)(Math.Cos(a) * rr) * h, (float)u * h, (float)(Math.Sin(a) * rr) * h), 0.05f, ar, ag, ab, 0.07f));
            }
            // rings: the gas giant gets bright ones, the ice giant faint dark ones
            if (id == "gas-giant" || id == "ice-giant")
            {
                bool bright = id == "gas-giant";
                int rings = bright ? 5200 : 1400;
                for (int i = 0; i < rings; i++)
                {
                    float rad = rng.Range(bright ? 1.35f : 1.6f, bright ? 2.3f : 2.0f);
                    if (bright && rad > 1.85f && rad < 1.92f) continue;           // a gap in the rings
                    double a = rng.NextDouble() * Math.PI * 2;
                    float shade = bright ? 0.6f + 0.4f * (float)Math.Sin(rad * 40) * 0.5f : 0.25f;
                    var p = new Vec3((float)Math.Cos(a) * rad, rng.Range(-0.005f, 0.005f), (float)Math.Sin(a) * rad);
                    // tilt the rings
                    p = new Vec3(p.X, p.Y * 0.95f + p.Z * 0.3f, p.Z * 0.95f - p.Y * 0.3f);
                    surface.Rings.Add(p); surface.RingShade.Add(shade * 0.5f);
                }
            }
            return list.ToArray();
        }
    }
}
