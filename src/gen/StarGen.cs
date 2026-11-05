// DNA-AND · generators · a single star seen up close, built from content/stars.dnat.
// The star is a ball of faceted points (Populous-like) with surface detail that depends on the kind:
// granulation and spots, giant convection cells, bands on brown dwarfs, beams on pulsars.
using System;
using System.Collections.Generic;
using DnaAnd.Core;
using DnaAnd.Render;

namespace DnaAnd.Gen
{
    internal static class StarGen
    {
        /// <summary>Points for one star, radius about 1, centred on the origin.</summary>
        public static StarVertex[] Build(ContentLibrary content, string id, ulong seed, out string name)
        {
            name = id; string color = "white";
            foreach (var e in content.OfKind("stars.dnat", "star"))
                if (e.Id == id) { name = e.Name; color = e.Get("color", "white"); }
            float r, g, b; GalaxyGen.ColorOf(color, out r, out g, out b);
            var rng = new Rng(seed);
            var list = new List<StarVertex>();
            uint nseed = (uint)(seed & 0xFFFFFFFF);

            bool giant = id.Contains("giant") || id.Contains("supergiant") || id == "hypergiant";
            bool brown = id == "brown-dwarf", white = id == "white-dwarf";
            bool compact = id == "neutron-star" || id == "pulsar" || id == "magnetar";
            bool spotty = id == "m-type" || id == "k-type" || id == "g-type";
            float radius = compact ? 0.28f : white ? 0.45f : 1f;
            int surface = compact ? 900 : white ? 1500 : 3200;
            float cell = giant ? 1.6f : brown ? 1f : 5f;       // noise frequency: giants have huge convection cells

            for (int i = 0; i < surface; i++)
            {
                // even spread over the sphere
                double yv = 1 - 2 * (i + 0.5) / surface, rr = Math.Sqrt(1 - yv * yv), th = i * 2.399963229728653;
                var n = new Vec3((float)(Math.Cos(th) * rr), (float)yv, (float)(Math.Sin(th) * rr));
                float noise = Noise.Fractal(n.X * cell + 7, n.Y * cell + 3, n.Z * cell + 11, nseed, 4);
                float bright;
                if (brown) bright = 0.45f + 0.35f * (float)Math.Sin(n.Y * 9 + noise * 3);           // cloud bands
                else if (giant) bright = 0.55f + 0.9f * (noise - 0.5f) + 0.25f;                      // big boiling cells
                else bright = 0.85f + 0.4f * (noise - 0.5f);                                         // fine granulation
                if (spotty && Noise.Fractal(n.X * 3 + 50, n.Y * 3, n.Z * 3, nseed + 9, 2) > 0.72f) bright *= 0.35f; // star spots
                if (white || compact) bright = 1.1f;
                bright = Math.Max(0.12f, Math.Min(1.3f, bright));
                list.Add(SpaceGen.Star(n * radius, radius * (compact ? 0.16f : 0.085f), r, g, b, bright));
            }

            // the glow around it: a loose shell of dim points
            int corona = brown ? 0 : compact ? 160 : 420;
            for (int i = 0; i < corona; i++)
            {
                double u = rng.NextDouble() * 2 - 1, a = rng.NextDouble() * Math.PI * 2, rr = Math.Sqrt(1 - u * u);
                float h = radius * rng.Range(1.08f, giant ? 1.9f : 1.5f);
                var p = new Vec3((float)(Math.Cos(a) * rr) * h, (float)u * h, (float)(Math.Sin(a) * rr) * h);
                list.Add(SpaceGen.Star(p, radius * 0.18f, r, g, b, 0.10f * (1.6f - h / radius * 0.5f)));
            }

            // pulsars and magnetars: two narrow beams out of the magnetic poles, tilted from the spin axis
            if (compact && id != "neutron-star")
            {
                var axis = new Vec3(0.42f, 0.9f, 0.1f); float len = (float)Math.Sqrt(Vec3.Dot(axis, axis)); axis = axis * (1f / len);
                for (int s = -1; s <= 1; s += 2)
                    for (int k = 0; k < 260; k++)
                    {
                        float t = k / 260f, d = radius * 1.1f + t * 2.6f, spread = t * 0.25f;
                        var p = axis * (d * s) + new Vec3(rng.Range(-spread, spread), rng.Range(-spread, spread) * 0.3f, rng.Range(-spread, spread));
                        list.Add(SpaceGen.Star(p, 0.05f, 0.75f, 0.85f, 1f, 0.9f * (1 - t)));
                    }
            }
            return list.ToArray();
        }
    }
}
