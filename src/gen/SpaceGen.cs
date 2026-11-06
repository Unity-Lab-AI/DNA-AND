// DNA-AND · gen · live generators for the deep zoom: galaxy structures (clusters, nebulas), the core's
// supermassive black hole, and full star systems. All built from the seed and the content files.
using System;
using System.Collections.Generic;
using DnaAnd.Core;
using DnaAnd.Render;

namespace DnaAnd.Gen
{
    /// <summary>Distance units. One galaxy unit is 50,000 light-years (about a galaxy's radius).</summary>
    internal static class Units
    {
        public const double LightYearsPerUnit = 50000.0;
        public const double KmPerLightYear = 9.4607e12;
        public const double KmPerAu = 1.496e8;
        public const double AuPerLightYear = 63241.0;
        public static double Km(double km) { return km / KmPerLightYear / LightYearsPerUnit; }
        public static double Au(double au) { return Km(au * KmPerAu); }

        public static string Describe(double units)
        {
            double ly = units * LightYearsPerUnit;
            if (ly >= 1) return ly.ToString(ly >= 100 ? "N0" : "0.0") + " light-years";
            double au = ly * AuPerLightYear;
            if (au >= 0.01) return au.ToString(au >= 100 ? "N0" : "0.00") + " au";
            return (au * KmPerAu).ToString("N0") + " km";
        }
    }

    internal static class SpaceGen
    {
        // ── galaxy structures: globular clusters, open clusters, nebulas ──
        public static StarVertex[] Structures(string galaxyType, ulong seed, List<StarKind> kinds, out int clusterCount, out int nebulaCount)
        {
            var rng = new Rng(seed ^ 0xC1u);
            var list = new List<StarVertex>();
            bool spiral = galaxyType.Contains("spiral");
            clusterCount = 0; nebulaCount = 0;
            int globulars = 30 + rng.Int(20);
            for (int g = 0; g < globulars; g++)
            {
                // halo: tight balls of ancient stars (content: globular clusters in the halo)
                var c = new Vec3(rng.Gaussian() * 0.5f, rng.Gaussian() * 0.35f, rng.Gaussian() * 0.5f);
                float r = (float)(60 / Units.LightYearsPerUnit);
                for (int i = 0; i < 500; i++)
                    list.Add(Star(c + new Vec3(rng.Gaussian(), rng.Gaussian(), rng.Gaussian()) * (r * (float)Math.Abs(rng.Gaussian()) * 0.6f), 0f, 1f, 0.75f, 0.45f, rng.Range(0.05f, 0.2f)));
                clusterCount++;
            }
            if (!spiral) return list.ToArray();
            int opens = 60 + rng.Int(40), nebulas = 40 + rng.Int(30);
            for (int k = 0; k < opens + nebulas; k++)
            {
                // place along the arms, where young stars and nebulas live (content rule: nebula-place)
                float rr = (float)Math.Pow(rng.NextDouble(), 0.7) * 0.85f + 0.1f;
                float ang = rng.Int(2) * (float)Math.PI + (float)Math.Log(rr / 0.05f + 1) * 3.2f + rng.Gaussian() * 0.25f;
                var c = new Vec3((float)Math.Cos(ang) * rr, rng.Gaussian() * 0.01f, (float)Math.Sin(ang) * rr);
                if (k < opens)
                {
                    float r = (float)(15 / Units.LightYearsPerUnit);
                    for (int i = 0; i < 60; i++)
                        list.Add(Star(c + new Vec3(rng.Gaussian(), rng.Gaussian() * 0.4f, rng.Gaussian()) * r, 0f, 0.7f, 0.8f, 1f, rng.Range(0.1f, 0.35f)));
                    clusterCount++;
                }
                else
                {
                    // emission nebula: soft red glow; some reflection nebulas: blue (content: nebulas.dnat)
                    bool emission = rng.NextDouble() < 0.75;
                    float r = (float)(rng.Range(40, 300) / Units.LightYearsPerUnit);
                    for (int i = 0; i < 120; i++)
                        list.Add(Star(c + new Vec3(rng.Gaussian(), rng.Gaussian() * 0.5f, rng.Gaussian()) * r, r * 2.5f,
                            emission ? 1f : 0.35f, emission ? 0.25f : 0.5f, emission ? 0.35f : 1f, 0.025f));
                    nebulaCount++;
                }
            }
            return list.ToArray();
        }

        // ── the core's supermassive black hole ──
        public sealed class BlackHole
        {
            public double MassSol, HorizonUnits; public bool Active;
            public StarVertex[] Disk, Jets, Glow;
        }

        public static BlackHole CoreBlackHole(ContentLibrary content, ulong seed)
        {
            var rng = new Rng(seed ^ 0xB1ACu);
            double lo = 1e6, hi = 1e10;
            foreach (var e in content.OfKind("black-holes.dnat", "blackhole"))
                if (e.Id == "supermassive") e.TryRange("mass-sol", out lo, out hi);
            double mass = Math.Exp(Math.Log(lo) + rng.NextDouble() * (Math.Log(Math.Min(hi, 1e9)) - Math.Log(lo)));
            double rs = Units.Km(2.95 * mass);                    // event horizon radius
            var bh = new BlackHole { MassSol = mass, HorizonUnits = rs, Active = rng.NextDouble() < 0.5 };

            var disk = new List<StarVertex>();
            for (int i = 0; i < 9000; i++)
            {
                // accretion disk from just outside the photon sphere outward; hotter (whiter) toward the inside
                double r = rs * (2.6 + Math.Pow(rng.NextDouble(), 1.6) * 30);
                double a = rng.NextDouble() * Math.PI * 2;
                float heat = (float)Math.Max(0, 1 - (r / rs - 2.6) / 30);
                disk.Add(Star(new Vec3((float)(Math.Cos(a) * r), (float)(rng.Gaussian() * rs * 0.08), (float)(Math.Sin(a) * r)),
                    (float)(rs * 0.5), 1f, 0.45f + heat * 0.5f, 0.15f + heat * 0.8f, 0.05f + heat * 0.12f));
            }
            for (int i = 0; i < 1200; i++)
            {
                // photon ring: light bent all the way around the hole
                double a = rng.NextDouble() * Math.PI * 2, r = rs * 1.5;
                disk.Add(Star(new Vec3((float)(Math.Cos(a) * r), (float)(Math.Sin(a) * r * 0.9), (float)(rng.Gaussian() * rs * 0.05)), (float)(rs * 0.2), 1f, 0.9f, 0.7f, 0.18f));
            }
            bh.Disk = disk.ToArray();

            var jets = new List<StarVertex>();
            if (bh.Active)
                for (int i = 0; i < 5000; i++)
                {
                    // jets fired from the poles (content: jets, relativistic jet)
                    double h = rs * (3 + Math.Pow(rng.NextDouble(), 2.2) * 3000) * (rng.NextDouble() < 0.5 ? 1 : -1);
                    double w = Math.Abs(h) * 0.04 * Math.Abs(rng.Gaussian());
                    double a = rng.NextDouble() * Math.PI * 2;
                    jets.Add(Star(new Vec3((float)(Math.Cos(a) * w), (float)h, (float)(Math.Sin(a) * w)), (float)(Math.Abs(h) * 0.02 + rs * 0.3), 0.45f, 0.65f, 1f, 0.06f));
                }
            bh.Jets = jets.ToArray();

            var glow = new List<StarVertex>();
            for (int i = 0; i < 2500; i++)
            {
                // dense star crowd around the core, spread over the inner light-years
                Vec3 d = new Vec3(rng.Gaussian(), rng.Gaussian() * 0.7f, rng.Gaussian()).Normalized;
                double r = Units.Au(500) * Math.Pow(rng.NextDouble(), 2.5) * 4000;
                glow.Add(Star(d * (float)r, 0, 1f, 0.8f, 0.55f, rng.Range(0.1f, 0.5f)));
            }
            bh.Glow = glow.ToArray();
            return bh;
        }

        // ── a full star system ──
        public sealed class Body
        {
            public string Kind; public double OrbitUnits, RadiusUnits, Period, Phase; public float R, G, B;
            public int Parent = -1; // index of the planet a moon circles
        }
        public sealed class StarSystem
        {
            public string StarName; public double StarRadius, Luminosity, FrostLineAu, HabitableAu;
            public float SR, SG, SB;
            public readonly List<Body> Bodies = new List<Body>();
            public StarVertex[] Static; // belts, Kuiper belt, Oort cloud, orbit lines
            public int Planets;
        }

        public static StarSystem System(ContentLibrary content, StarKind kind, ulong seed, string layout = null)
        {
            var rng = new Rng(seed);
            var sys = new StarSystem { StarName = kind.Name, SR = kind.R, SG = kind.G, SB = kind.B };
            double massLo = 1, massHi = 1;
            foreach (var e in content.OfKind("stars.dnat", "star"))
                if (e.Id == kind.Id) e.TryRange("mass-sol", out massLo, out massHi);
            double mass = Math.Max(0.08, massLo + rng.NextDouble() * (massHi - massLo));
            sys.Luminosity = Math.Pow(mass, 3.5);
            // its real size, from content (giants and supergiants are far wider than their mass alone would make them)
            double rLo = -1, rHi = -1;
            foreach (var e in content.OfKind("stars.dnat", "star")) if (e.Id == kind.Id) e.TryRange("radius-sol", out rLo, out rHi);
            double radiusSol = rLo > 0 ? rLo + rng.NextDouble() * (rHi - rLo) : Math.Pow(mass, 0.8);
            sys.StarRadius = Units.Km(696000 * radiusSol);
            // how bright it shines, from its size and surface temperature (the Sun: 1 radius, 5772 K), when both are known
            double tLo = -1, tHi = -1;
            foreach (var e in content.OfKind("stars.dnat", "star")) if (e.Id == kind.Id) e.TryRange("temp-k", out tLo, out tHi);
            if (tLo > 0 && rLo > 0) { double tk = tLo + rng.NextDouble() * (tHi - tLo); sys.Luminosity = radiusSol * radiusSol * Math.Pow(tk / 5772, 4); }
            double frostSun = ZoneAu(content, "frost-line", 2.7), hzSun = ZoneAu(content, "habitable", 1.0);
            double scale = Math.Sqrt(sys.Luminosity);
            sys.FrostLineAu = frostSun * scale; sys.HabitableAu = hzSun * scale;

            int count = 3 + rng.Int(8);
            double a = 0.3 * scale * rng.Range(0.6f, 1.4f), step = rng.Range(1.45f, 1.9f);
            if (layout == "compact") { count = 6 + rng.Int(3); a = 0.012 * rng.Range(0.8f, 1.2f); step = rng.Range(1.25f, 1.4f); } // packed close to a dim star (content: compact system)
            if (layout == "hot-jupiter") count = 1 + rng.Int(3);                                                                  // few close neighbors (content: hot-Jupiter system)
            bool hotJupiter = layout == "hot-jupiter" || (layout == null && rng.NextDouble() < 0.1);
            double lastRocky = 0, firstGiant = 0, outer = 0;
            for (int i = 0; i < count; i++, a *= step)
            {
                var b = new Body { OrbitUnits = Units.Au(a), Phase = rng.NextDouble() * Math.PI * 2, Period = Math.Sqrt(a * a * a / mass) };
                bool giant = a > sys.FrostLineAu || (hotJupiter && i == 0);
                if (giant)
                {
                    bool ice = a > sys.FrostLineAu * 3.5;
                    b.Kind = (hotJupiter && i == 0) ? "Hot Jupiter" : ice ? "Ice giant" : "Gas giant";
                    b.RadiusUnits = Units.Km(ice ? 25000 : 70000 * rng.Range(0.6f, 1.2f));
                    if (ice) { b.R = 0.5f; b.G = 0.8f; b.B = 1f; } else { b.R = 0.95f; b.G = 0.75f; b.B = 0.5f; }
                    if (firstGiant == 0) firstGiant = a;
                }
                else
                {
                    bool hot = a < 0.1 * scale, wet = Math.Abs(a - sys.HabitableAu) < sys.HabitableAu * 0.25;
                    b.Kind = hot ? "Lava planet" : wet ? "Temperate world" : a > sys.HabitableAu ? "Ice world" : "Rocky planet";
                    b.RadiusUnits = Units.Km(6371 * rng.Range(0.4f, 1.8f));
                    if (hot) { b.R = 1f; b.G = 0.4f; b.B = 0.15f; } else if (wet) { b.R = 0.3f; b.G = 0.6f; b.B = 1f; }
                    else if (a > sys.HabitableAu) { b.R = 0.9f; b.G = 0.95f; b.B = 1f; } else { b.R = 0.75f; b.G = 0.65f; b.B = 0.55f; }
                    lastRocky = a;
                }
                sys.Bodies.Add(b); outer = a;
                int planetIndex = sys.Bodies.Count - 1;
                int moons = giant ? 2 + rng.Int(7) : rng.Int(3);
                for (int m = 0; m < moons; m++)
                    sys.Bodies.Add(new Body
                    {
                        Kind = "Moon", Parent = planetIndex, OrbitUnits = b.RadiusUnits * rng.Range(3f, 40f),
                        RadiusUnits = b.RadiusUnits * rng.Range(0.03f, 0.12f), Period = rng.Range(0.002f, 0.08f), Phase = rng.NextDouble() * Math.PI * 2,
                        R = 0.8f, G = 0.78f, B = 0.75f
                    });
            }
            sys.Planets = count;

            var stat = new List<StarVertex>();
            // orbit lines
            foreach (var b in sys.Bodies)
                if (b.Parent < 0)
                    for (int k = 0; k < 240; k++)
                    {
                        double t = k / 240.0 * Math.PI * 2;
                        stat.Add(Star(new Vec3((float)(Math.Cos(t) * b.OrbitUnits), 0, (float)(Math.Sin(t) * b.OrbitUnits)), 0, 0.35f, 0.45f, 0.6f, 0.22f));
                    }
            // asteroid belt in the gap between the rocky planets and the giants
            if (firstGiant > 0 && lastRocky > 0)
                Ring(stat, rng, Units.Au(lastRocky * 1.15), Units.Au(firstGiant * 0.85), 3000, 0.02, 0.7f, 0.6f, 0.5f, 0.5f);
            // Kuiper belt beyond the last planet, and the Oort cloud far out
            Ring(stat, rng, Units.Au(outer * 1.3), Units.Au(outer * 2.0), 4000, 0.05, 0.6f, 0.75f, 0.9f, 0.35f);
            for (int i = 0; i < 6000; i++)
            {
                Vec3 d = new Vec3(rng.Gaussian(), rng.Gaussian(), rng.Gaussian()).Normalized;
                double r = Units.Au(rng.Range(2000, 50000) * scale);
                stat.Add(Star(d * (float)r, 0, 0.55f, 0.7f, 0.85f, 0.12f));
            }
            sys.Static = stat.ToArray();
            return sys;
        }

        private static double ZoneAu(ContentLibrary content, string id, double fallback)
        {
            foreach (var e in content.OfKind("solar-systems.dnat", "zone"))
                if (e.Id == id)
                {
                    string v = e.Get("solar-system-au", "").Replace("about", "").Trim();
                    var tmp = new ContentEntry(); tmp.Props["v"] = v;
                    double lo, hi; if (tmp.TryRange("v", out lo, out hi)) return (lo + hi) * 0.5;
                }
            return fallback;
        }

        private static void Ring(List<StarVertex> list, Rng rng, double inner, double outer, int n, double thick, float r, float g, float b, float a)
        {
            for (int i = 0; i < n; i++)
            {
                double rad = inner + rng.NextDouble() * (outer - inner), t = rng.NextDouble() * Math.PI * 2;
                list.Add(Star(new Vec3((float)(Math.Cos(t) * rad), (float)(rng.Gaussian() * rad * thick), (float)(Math.Sin(t) * rad)), 0, r, g, b, a));
            }
        }

        /// <summary>
        /// Builds this frame's star, planet and moon billboards. Calm motion: a body whose on-screen turn would be faster
        /// than <paramref name="maxTurnPerSecond"/> is drawn as a soft orbit trail instead of a moving dot.
        /// </summary>
        public static int BodyVerts(StarSystem sys, double simYears, double yearsPerSecond, double maxTurnPerSecond, StarVertex[] outV, int trailPoints)
        {
            double fx, fy, fz;
            return BodyVerts(sys, simYears, yearsPerSecond, maxTurnPerSecond, outV, -1, 1.0, out fx, out fy, out fz);
        }

        /// <summary>Star and bodies, worked out in double precision, written relative to the focus body
        /// (-1 = the star) and multiplied by <paramref name="scale"/>, so a single planet can fill the screen
        /// without shaking. The focus position (unscaled, star-centred) is given back for anything drawn with it.</summary>
        public static int BodyVerts(StarSystem sys, double simYears, double yearsPerSecond, double maxTurnPerSecond, StarVertex[] outV,
            int focus, double scale, out double fx, out double fy, out double fz)
        {
            int count = sys.Bodies.Count;
            var px = new double[count]; var pz = new double[count];
            double seconds = yearsPerSecond > 0 ? simYears / yearsPerSecond : 0;
            for (int i = 0; i < count; i++)
            {
                var b = sys.Bodies[i];
                double period = Math.Max(1e-4, b.Period);
                double turn = yearsPerSecond / period * Math.PI * 2;
                // calm motion: a body that would turn faster than the limit is shown turning at the limit
                double ang = turn <= maxTurnPerSecond || yearsPerSecond <= 0
                    ? b.Phase + simYears / period * Math.PI * 2
                    : b.Phase + seconds * maxTurnPerSecond;
                double cx = b.Parent >= 0 ? px[b.Parent] : 0, cz = b.Parent >= 0 ? pz[b.Parent] : 0;
                px[i] = cx + Math.Cos(ang) * b.OrbitUnits; pz[i] = cz + Math.Sin(ang) * b.OrbitUnits;
            }
            fx = focus >= 0 && focus < count ? px[focus] : 0; fy = 0; fz = focus >= 0 && focus < count ? pz[focus] : 0;
            int n = 0;
            outV[n++] = Star(new Vec3((float)(-fx * scale), 0, (float)(-fz * scale)), (float)(sys.StarRadius * scale), sys.SR, sys.SG, sys.SB, 1f);
            for (int i = 0; i < count; i++)
            {
                var b = sys.Bodies[i];
                outV[n++] = Star(new Vec3((float)((px[i] - fx) * scale), 0, (float)((pz[i] - fz) * scale)), (float)(b.RadiusUnits * scale),
                    b.R, b.G, b.B, b.Parent >= 0 ? 0.6f : 1f);
            }
            return n;
        }

        public static StarVertex Star(Vec3 p, float size, float r, float g, float b, float bright)
        {
            return new StarVertex { X = p.X, Y = p.Y, Z = p.Z, Size = size, R = r * bright, G = g * bright, B = b * bright, A = 1 };
        }
    }
}
