// DNA-AND · generators · the 118 elements as real samples, from content/elements.dnat.
// Solids are lumps with the element's real look, gases glow in their own colour inside a glass bulb,
// liquids are drops, and lab-made elements are a faint flicker because they fall apart almost at once.
using System;
using System.Collections.Generic;
using DnaAnd.Core;
using DnaAnd.Render;

namespace DnaAnd.Gen
{
    internal sealed class ElementSample
    {
        public int Number; public string Symbol, Name, Group, State, Origin, Use, FoundIn;
        public int Col, Row;                       // place in the periodic table (rows 8 and 9 are the two lower rows)
        public float R, G, B; public bool Shiny, Radioactive;
        public double MeltK = -1, BoilK = -1;      // -1: never measured

        /// <summary>solid, liquid, gas, or unknown, at a temperature (kelvin) and pressure (atmospheres).
        /// Higher pressure raises the boiling point; lower pressure lowers it.</summary>
        public string StateAt(double k, double atm)
        {
            if (MeltK < 0) return "unknown";
            double boil = BoilK < 0 ? MeltK * 1.6 : BoilK * (1 + 0.06 * Math.Log(Math.Max(1e-6, atm)));
            boil = Math.Max(boil, MeltK * (BoilK == MeltK ? 1 : 1.0001));
            if (k < MeltK && !(BoilK == MeltK && k >= boil)) return "solid";
            if (k < boil && BoilK != MeltK) return "liquid";
            return k >= boil ? "gas" : "solid";
        }
        public Vec3 Centre;
    }

    internal static class ElementGen
    {
        /// <summary>The element's place in the standard periodic table, from its number.</summary>
        public static void Place(int z, out int col, out int row)
        {
            if (z == 1) { col = 1; row = 1; return; }
            if (z == 2) { col = 18; row = 1; return; }
            if (z <= 18)
            {
                int start = z <= 10 ? 3 : 11; row = z <= 10 ? 2 : 3;
                int k = z - start;
                col = k < 2 ? k + 1 : k + 11; return;
            }
            if (z <= 54) { row = z <= 36 ? 4 : 5; col = z - (z <= 36 ? 18 : 36); return; }
            int first = z <= 86 ? 55 : 87; int prow = z <= 86 ? 6 : 7;
            int i = z - first;
            if (i < 2) { col = i + 1; row = prow; return; }
            if (i < 17) { col = i + 1; row = prow + 2; return; }       // the lanthanides and actinides, in the two lower rows
            col = i - 13; row = prow; return;                             // hafnium (or rutherfordium) onward: columns 4 to 18
        }

        // real looks: (name of the element, red, green, blue, shiny metal?)
        private static readonly Dictionary<string, float[]> Looks = new Dictionary<string, float[]>
        {
            { "Au", new[] { 1f, 0.78f, 0.25f, 1 } }, { "Cu", new[] { 0.85f, 0.45f, 0.3f, 1 } }, { "Cs", new[] { 0.95f, 0.85f, 0.55f, 1 } },
            { "Os", new[] { 0.6f, 0.68f, 0.78f, 1 } }, { "Bi", new[] { 0.8f, 0.6f, 0.85f, 1 } }, { "Fe", new[] { 0.55f, 0.55f, 0.57f, 1 } },
            { "C", new[] { 0.12f, 0.12f, 0.13f, 0 } }, { "S", new[] { 0.95f, 0.85f, 0.2f, 0 } }, { "P", new[] { 0.75f, 0.2f, 0.15f, 0 } },
            { "I", new[] { 0.3f, 0.2f, 0.35f, 1 } }, { "Se", new[] { 0.45f, 0.45f, 0.47f, 1 } }, { "B", new[] { 0.35f, 0.25f, 0.2f, 0 } },
            { "Si", new[] { 0.45f, 0.5f, 0.6f, 1 } }, { "Ge", new[] { 0.6f, 0.6f, 0.62f, 1 } }, { "As", new[] { 0.5f, 0.5f, 0.52f, 1 } },
            { "Te", new[] { 0.62f, 0.62f, 0.64f, 1 } }, { "Sb", new[] { 0.68f, 0.68f, 0.7f, 1 } }, { "Br", new[] { 0.55f, 0.12f, 0.05f, 0 } },
            { "Hg", new[] { 0.82f, 0.82f, 0.85f, 1 } }, { "Pb", new[] { 0.45f, 0.46f, 0.52f, 1 } }, { "Zn", new[] { 0.68f, 0.7f, 0.75f, 1 } },
            { "Ti", new[] { 0.6f, 0.6f, 0.62f, 1 } }, { "Cr", new[] { 0.78f, 0.8f, 0.85f, 1 } }, { "Co", new[] { 0.55f, 0.55f, 0.6f, 1 } },
            { "Ni", new[] { 0.7f, 0.68f, 0.62f, 1 } }, { "Ag", new[] { 0.9f, 0.9f, 0.92f, 1 } }, { "Pt", new[] { 0.85f, 0.85f, 0.83f, 1 } },
            { "U", new[] { 0.45f, 0.47f, 0.45f, 1 } }, { "Pu", new[] { 0.5f, 0.45f, 0.42f, 1 } }, { "Ra", new[] { 0.85f, 0.88f, 0.85f, 1 } },
        };
        // the colour each gas glows when lit by electricity (its emission colour)
        private static readonly Dictionary<string, float[]> Glows = new Dictionary<string, float[]>
        {
            { "H", new[] { 0.9f, 0.4f, 0.7f } }, { "He", new[] { 1f, 0.75f, 0.55f } }, { "Ne", new[] { 1f, 0.35f, 0.1f } }, { "Ar", new[] { 0.65f, 0.4f, 1f } },
            { "Kr", new[] { 0.85f, 0.9f, 1f } }, { "Xe", new[] { 0.45f, 0.6f, 1f } }, { "Rn", new[] { 0.95f, 0.25f, 0.2f } }, { "N", new[] { 0.85f, 0.45f, 0.95f } },
            { "O", new[] { 0.55f, 0.75f, 1f } }, { "F", new[] { 0.95f, 0.95f, 0.55f } }, { "Cl", new[] { 0.7f, 0.95f, 0.3f } },
        };

        public static List<ElementSample> Load(ContentLibrary content)
        {
            var list = new List<ElementSample>();
            foreach (var e in content.OfKind("elements.dnat", "element"))
            {
                int z; if (!int.TryParse(e.Get("number", "0"), out z) || z < 1) continue;
                var s = new ElementSample { Number = z, Symbol = e.Get("symbol", "?"), Name = e.Name, Group = e.Get("group", ""),
                    State = e.Get("state-at-room-temp", "unknown"), Origin = e.Get("origin", ""), Use = e.Get("in-game", "") };
                Place(z, out s.Col, out s.Row);
                double mk, bk;
                if (double.TryParse(e.Get("melt-k", "x"), out mk)) s.MeltK = mk;
                if (double.TryParse(e.Get("boil-k", "x"), out bk)) s.BoilK = bk;
                bool metal = s.Group.Contains("metal") || s.Group == "Transition" || s.Group == "Post-transition" || s.Group == "Lanthanide" || s.Group == "Actinide";
                s.R = 0.72f; s.G = 0.73f; s.B = 0.76f; s.Shiny = metal;
                float[] look;
                if (Looks.TryGetValue(s.Symbol, out look)) { s.R = look[0]; s.G = look[1]; s.B = look[2]; s.Shiny = look[3] > 0.5f; }
                else if (!metal && s.State == "solid") { s.R = 0.6f; s.G = 0.58f; s.B = 0.55f; }
                float[] glow;
                if (s.State == "gas" && Glows.TryGetValue(s.Symbol, out glow)) { s.R = glow[0]; s.G = glow[1]; s.B = glow[2]; }
                s.Radioactive = s.Group == "Actinide" || z == 43 || z == 61 || z >= 84;
                list.Add(s);
            }
            list.Sort((a, b) => a.Number.CompareTo(b.Number));
            // where each is found: from the resource fields of set 5 and the space fields
            foreach (var s in list) s.FoundIn = FoundIn(s);
            return list;
        }

        private static string FoundIn(ElementSample s)
        {
            if (s.Origin.StartsWith("lab")) return "nowhere in nature in any amount: made in laboratories, one atom at a time";
            switch (s.Symbol)
            {
                case "H": case "He": return "stars, gas giants and nebula gas; hydrogen also in ocean water";
                case "O": return "the air, ocean water, and nearly every rock";
                case "Na": case "Cl": case "K": case "Mg": return "ocean water and salt flats (evaporites)";
                case "S": return "volcano vents (volcanic sulfide fields)";
                case "Fe": return "banded iron formations, laterite soil, metal asteroids, planet cores";
                case "Au": return "mountain-belt gold veins and river placers";
                case "Cu": return "porphyry copper over the mountain belts, skarns, buried copper basins";
                case "C": return "coal, oil and gas (only once life has lived), diamond pipes (kimberlite), carbon asteroids";
                case "Ni": case "Pt": case "Cr": return "layered intrusions and metal asteroids";
                case "Li": case "Be": case "Ta": case "Nb": case "Cs": case "Rb": return "pegmatites";
                case "Al": return "laterite soil (bauxite) and most rock";
                case "Si": return "sand and nearly every rock";
                case "Ag": case "Zn": case "Pb": return "hot-spring veins (epithermal) and volcanic sulfides";
                case "U": case "Th": return "pegmatites and old continent rock";
                case "Sn": case "W": case "Mo": return "porphyry and skarn deposits";
                case "N": case "Ar": return "the air";
                case "Ne": case "Kr": case "Xe": return "the air, in tiny amounts";
            }
            if (s.Group == "Lanthanide") return "rare-earth layers in pegmatites and placers";
            if (s.Group == "Transition") return "metal ores in mountain belts and metal asteroids";
            return "scattered through rock and seawater";
        }

        /// <summary>The points of one sample, centred on 0, about 0.4 across.</summary>
        public static void Build(ElementSample s, string state, Rng rng, List<StarVertex> outPts)
        {
            bool gas = state == "gas", liquid = state == "liquid", lab = state == "unknown";
            if (gas)
            {
                // a glass bulb, and the gas glowing inside it
                for (int i = 0; i < 160; i++) outPts.Add(SpaceGen.Star(OnSphere(rng) * 0.22f, 0.012f, 0.7f, 0.8f, 0.9f, 0.18f));
                for (int i = 0; i < 260; i++) outPts.Add(SpaceGen.Star(InSphere(rng) * 0.19f, 0.03f, s.R, s.G, s.B, 0.22f));
                return;
            }
            if (lab)
            {
                // a few short-lived atoms: a faint flickering glow
                for (int i = 0; i < 40; i++) outPts.Add(SpaceGen.Star(InSphere(rng) * 0.12f, 0.02f, 0.6f, 0.9f, 0.7f, 0.25f));
                return;
            }
            // solids and liquids: a lump (or a flattened drop), faceted, lit as metal or as a dull solid
            int n = 700;
            for (int i = 0; i < n; i++)
            {
                double yv = 1 - 2 * (i + 0.5) / n, rr = Math.Sqrt(1 - yv * yv), th = i * 2.399963229728653;
                var dir = new Vec3((float)(Math.Cos(th) * rr), (float)yv, (float)(Math.Sin(th) * rr));
                float bump = liquid ? 1f : 0.82f + 0.3f * Noise.Fractal(dir.X * 3 + s.Number, dir.Y * 3, dir.Z * 3, (uint)s.Number, 3);
                var p = dir * (0.18f * bump);
                if (liquid) p = new Vec3(p.X * 1.2f, p.Y * 0.55f - 0.04f, p.Z * 1.2f);             // a drop resting on the shelf
                float shade = 0.55f + 0.45f * Math.Max(0, Vec3.Dot(dir, new Vec3(-0.5f, 0.75f, -0.45f)));
                if (s.Shiny) shade += (float)Math.Pow(Math.Max(0, Vec3.Dot(dir, new Vec3(-0.35f, 0.8f, -0.5f))), 18) * 0.9f;   // metal glint
                if (p.Z > 0.03f) continue;                                    // only the side facing the viewer: piled-up points wash out to white
                outPts.Add(SpaceGen.Star(p, 0.02f, s.R * shade, s.G * shade, s.B * shade, 0.5f));
            }
            if (s.Radioactive)
                for (int i = 0; i < 60; i++) outPts.Add(SpaceGen.Star(InSphere(rng) * 0.26f, 0.015f, 0.4f, 1f, 0.5f, 0.2f));   // a faint glow from its radiation
        }

        private static Vec3 OnSphere(Rng rng)
        {
            double u = rng.NextDouble() * 2 - 1, a = rng.NextDouble() * Math.PI * 2, r = Math.Sqrt(1 - u * u);
            return new Vec3((float)(Math.Cos(a) * r), (float)u, (float)(Math.Sin(a) * r));
        }
        private static Vec3 InSphere(Rng rng) { return OnSphere(rng) * (float)Math.Pow(rng.NextDouble(), 1.0 / 3); }
    }
}
