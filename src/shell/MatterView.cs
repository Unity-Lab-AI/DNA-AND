// DNA-AND · shell · the ground's own matter, all the way down, at its real sizes: below the finest piece of ground the camera
// carries on into what the ground is made of. Everything sits at a fixed place around the point you dive into, so zooming truly
// scales it into view (nothing fades between pictures):
//   grains (about 0.2 mm), each one mineral from its ground's own mix (content/minerals.dnat);
//   the grain's surface, its detail at every size fixed in place;
//   the mineral's atoms in rows about 0.25 nm apart, its real elements in their real ratio, one atom right under you;
//   that atom: its electrons in their real shells (worked out from its atomic number), moving round it;
//   inside it: mostly empty, the nucleus a spark at its middle;
//   the nucleus: its real protons and neutrons (from the element's most common isotope, content/elements.dnat).
// (Microbes, bacteria and viruses join when life is built.)
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DnaAnd.Core;
using DnaAnd.Gen;
using DnaAnd.Render;

namespace DnaAnd.Shell
{
    internal sealed class MatterView : IDisposable
    {
        private readonly Gpu _gpu; private readonly BillboardDrawer _dots;
        private UploadBuffer _solidBuf, _glowBuf; private StarVertex[] _sv = new StarVertex[0], _gv = new StarVertex[0];
        public const double TopKm = 1e-6;           // 1 mm: where the matter begins (the ground's finest pieces are above this)
        public const double BottomKm = 3e-19;       // 0.3 femtometres: among the protons and neutrons of a nucleus
        private const double Grain = 2e-7;          // 0.2 mm: a grain of sand or dust
        private const double AtomSpacing = 2.5e-13; // about 0.25 nm between atoms in a crystal
        private const double Nucleon = 0.85e-18;    // a proton's or neutron's radius, about 0.85 femtometres

        private sealed class Mineral { public string Id, Name, Formula; public string[] Atoms; public float R, G, B; }
        private sealed class Element { public string Symbol, Name; public int Z, A; public float R, G, B; public int[] Shells; }
        private readonly Dictionary<string, Mineral> _minerals = new Dictionary<string, Mineral>();
        private readonly Dictionary<string, List<KeyValuePair<Mineral, double>>> _mixes = new Dictionary<string, List<KeyValuePair<Mineral, double>>>();
        private readonly Dictionary<string, Element> _elements = new Dictionary<string, Element>();
        private List<KeyValuePair<Mineral, double>> _mix; private string _mixId = "";
        private ulong _seed;

        // atoms in chemistry's usual colours (CPK), the rest a soft grey-violet
        private static readonly Dictionary<string, float[]> Cpk = new Dictionary<string, float[]>
        {
            { "H", new[] { 0.95f, 0.95f, 0.95f } }, { "C", new[] { 0.3f, 0.3f, 0.3f } }, { "N", new[] { 0.2f, 0.3f, 0.95f } },
            { "O", new[] { 0.9f, 0.13f, 0.1f } }, { "Na", new[] { 0.67f, 0.36f, 0.95f } }, { "Mg", new[] { 0.3f, 0.8f, 0.25f } },
            { "Al", new[] { 0.75f, 0.65f, 0.65f } }, { "Si", new[] { 0.85f, 0.7f, 0.5f } }, { "S", new[] { 0.95f, 0.85f, 0.2f } },
            { "Cl", new[] { 0.12f, 0.94f, 0.12f } }, { "K", new[] { 0.56f, 0.25f, 0.83f } }, { "Ca", new[] { 0.24f, 1f, 0.24f } },
            { "Ti", new[] { 0.75f, 0.76f, 0.78f } }, { "Fe", new[] { 0.88f, 0.4f, 0.2f } }
        };

        public MatterView(Gpu gpu, BillboardPipeline pipe, ContentLibrary content)
        {
            _gpu = gpu; _dots = new BillboardDrawer(gpu.CommandList, pipe);
            foreach (var e in content.OfKind("elements.dnat", "element"))
            {
                int z, a; if (!int.TryParse(e.Get("number", ""), out z) || !int.TryParse(e.Get("mass-number", ""), out a)) continue;
                string sym = e.Get("symbol", e.Id); float[] c; if (!Cpk.TryGetValue(sym, out c)) c = new[] { 0.7f, 0.6f, 0.8f };
                _elements[sym] = new Element { Symbol = sym, Name = e.Name, Z = z, A = a, R = c[0], G = c[1], B = c[2], Shells = Shells(z) };
            }
            foreach (var e in content.OfKind("minerals.dnat", "mineral"))
            {
                var col = e.Get("colour", "0.7 0.7 0.7").Split(' ');
                var m = new Mineral { Id = e.Id, Name = e.Name, Formula = e.Get("formula", ""), Atoms = e.Get("atoms", "Si O O").Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries) };
                float r, g, b; float.TryParse(col[0], out r); float.TryParse(col.Length > 1 ? col[1] : col[0], out g); float.TryParse(col.Length > 2 ? col[2] : col[0], out b);
                m.R = r; m.G = g; m.B = b; _minerals[m.Id] = m;
            }
            foreach (var e in content.OfKind("minerals.dnat", "mix"))
            {
                var list = new List<KeyValuePair<Mineral, double>>();
                foreach (var part in e.Get("minerals", "").Split(','))
                {
                    var w = part.Trim().Split(' '); double share; Mineral m;
                    if (w.Length == 2 && double.TryParse(w[1], out share) && _minerals.TryGetValue(w[0], out m)) list.Add(new KeyValuePair<Mineral, double>(m, share));
                }
                if (list.Count > 0) _mixes[e.Id] = list;
            }
        }

        // electrons per shell (1, 2, 3, ...) for atomic number z, filled in the usual order (Madelung's rule)
        private static int[] Shells(int z)
        {
            int[][] order = { new[] { 1, 0 }, new[] { 2, 0 }, new[] { 2, 1 }, new[] { 3, 0 }, new[] { 3, 1 }, new[] { 4, 0 }, new[] { 3, 2 }, new[] { 4, 1 }, new[] { 5, 0 }, new[] { 4, 2 }, new[] { 5, 1 },
                new[] { 6, 0 }, new[] { 4, 3 }, new[] { 5, 2 }, new[] { 6, 1 }, new[] { 7, 0 }, new[] { 5, 3 }, new[] { 6, 2 }, new[] { 7, 1 } };
            var shells = new int[7]; int left = z, top = 0;
            foreach (var o in order)
            {
                if (left <= 0) break;
                int take = Math.Min(left, 2 * (2 * o[1] + 1)); shells[o[0] - 1] += take; left -= take; top = Math.Max(top, o[0]);
            }
            var r = new int[top]; Array.Copy(shells, r, top); return r;
        }

        /// <summary>What the ground at this spot is made of: its kind, whether it is under water or ice, and its colour; and
        /// the spot (a seed), so the same place always holds the same grains.</summary>
        public void Set(string planetKind, bool sea, bool ice, float r, float g, float b, ulong spot)
        {
            string id = sea ? "sea" : ice ? "ice" : planetKind;
            if (!_mixes.ContainsKey(id)) id = "desert";
            _mixId = id; _mix = _mixes.ContainsKey(id) ? _mixes[id] : null; _seed = spot;
        }

        private static double Hash(long i, long j, long k, ulong seed)
        {
            ulong h = (ulong)i * 0x9E3779B97F4A7C15UL ^ (ulong)j * 0xC2B2AE3D27D4EB4FUL ^ (ulong)k * 0x165667B19E3779F9UL ^ seed;
            h ^= h >> 31; h *= 0xBF58476D1CE4E5B9UL; h ^= h >> 29; h *= 0x94D049BB133111EBUL; h ^= h >> 32;
            return (h & 0xFFFFFF) / (double)0x1000000;
        }

        // the mineral of the grain in cell (i, j)
        private Mineral GrainMineral(long i, long j)
        {
            if (_mix == null) return null;
            double total = 0; foreach (var kv in _mix) total += kv.Value;
            double t = Hash(i, j, 7, _seed) * total;
            foreach (var kv in _mix) { t -= kv.Value; if (t <= 0) return kv.Key; }
            return _mix[_mix.Count - 1].Key;
        }

        private Element El(string sym) { Element e; return _elements.TryGetValue(sym, out e) ? e : null; }

        // the atom right under you: one of the focus grain's own atoms (picked from its formula by the spot)
        private Element FocusAtom()
        {
            var m = GrainMineral(0, 0); if (m == null) return null;
            return El(m.Atoms[(int)(Hash(0, 0, 11, _seed) * m.Atoms.Length) % m.Atoms.Length]);
        }

        private static double AtomRadius(Element e) { return e == null ? 1e-13 : e.Z == 1 ? 0.53e-13 : (0.7 + 0.5 * Math.Min(1, e.Z / 30.0)) * 1e-13; }
        private static double NucleusRadius(Element e) { return 1.2e-18 * Math.Pow(e == null ? 28 : e.A, 1 / 3.0); }

        public string MineralName { get { var m = GrainMineral(0, 0); return m != null ? m.Name : "rock"; } }

        /// <summary>What you are looking at, at this scale.</summary>
        public string Stage(double s)
        {
            var m = GrainMineral(0, 0); var e = FocusAtom();
            string mn = m != null ? m.Name.ToLowerInvariant() + " (" + m.Formula + ")" : "rock";
            if (s > Grain * 1.5) return "grains of " + (_mixId == "sea" ? "the sea" : "the ground") + ", this one " + mn;
            if (s > AtomSpacing * 12) return "the surface of a grain of " + mn;
            if (e == null) return "atoms of " + mn;
            double ar = AtomRadius(e), nr = NucleusRadius(e);
            if (s > ar * 3) return "the atoms of " + mn + "; under you " + e.Name.ToLowerInvariant();
            string sh = string.Join(", ", Array.ConvertAll(e.Shells, x => x.ToString()));
            if (s > nr * 40) return "an atom of " + e.Name.ToLowerInvariant() + ": " + e.Z + " electrons in shells of " + sh + ", round a nucleus far below";
            return "the nucleus of " + e.Name.ToLowerInvariant() + ": " + e.Z + " protons and " + (e.A - e.Z) + " neutrons";
        }

        private static void Add(List<StarVertex> l, double x, double y, double z, double size, float r, float g, float b, double s)
        {
            double vx = x / s, vy = y / s - 0.6, vz = z / s, vs = size / s;                  // view units: one unit = the scale; you are 0.6 above
            if (Math.Abs(vx) > 60 || Math.Abs(vz) > 60 || vy < -60 || vs < 2e-4 || vs > 400) return;
            l.Add(new StarVertex { X = (float)vx, Y = (float)vy, Z = (float)vz, Size = (float)vs, R = r, G = g, B = b, A = 1 });
        }

        private static double Fade(double s, double from, double to) { return Math.Max(0, Math.Min(1, Math.Log(from / s) / Math.Log(from / to))); }

        private void Build(double s, double time, List<StarVertex> solid, List<StarVertex> glow)
        {
            // grains: each in its own cell, its mineral picked from the mix, resting with its top at the level you dive into
            if (s > AtomSpacing * 40)
            {
                long reach = (long)Math.Ceiling(Math.Min(40, 2.5 * s / Grain)) + 1;
                for (long i = -reach; i <= reach; i++)
                    for (long j = -reach; j <= reach; j++)
                    {
                        var m = GrainMineral(i, j); if (m == null) continue;
                        double rad = Grain * (0.32 + 0.16 * Hash(i, j, 1, _seed)), jx = (i == 0 && j == 0) ? 0 : (Hash(i, j, 2, _seed) - 0.5) * Grain * 0.3, jz = (i == 0 && j == 0) ? 0 : (Hash(i, j, 3, _seed) - 0.5) * Grain * 0.3;
                        float sh = (float)(0.8 + 0.3 * Hash(i, j, 4, _seed));
                        Add(solid, i * Grain + jx, -rad, j * Grain + jz, rad / 0.75, m.R * sh, m.G * sh, m.B * sh, s);
                    }
            }
            // the focus grain's surface: detail at every size, fixed in place (cells of powers of ten, two at a time, blended),
            // in the grain's colour; its finest cells take on the colours of its atoms
            var fm = GrainMineral(0, 0);
            if (fm != null && s < Grain * 1.5 && s > AtomSpacing * 8)
            {
                double c1 = Math.Pow(10, Math.Floor(Math.Log10(s / 30))), frac = Math.Log10(s / 30) - Math.Floor(Math.Log10(s / 30));
                double atomish = Fade(s, AtomSpacing * 200, AtomSpacing * 20);
                for (int lv = 0; lv < 2; lv++)
                {
                    double c = lv == 0 ? c1 : c1 * 10, wgt = lv == 0 ? 1 - frac : frac;
                    long n = (long)Math.Ceiling(1.6 * s / c);
                    if (n > 70) continue;
                    for (long i = -n; i <= n; i++)
                        for (long j = -n; j <= n; j++)
                        {
                            double x = i * c, z = j * c;
                            if (x * x + z * z > 2.6 * s * s) continue;
                            long ci = (long)Math.Floor(x / c), cj = (long)Math.Floor(z / c);
                            float sh = (float)(0.7 + 0.45 * Hash(ci, cj, (long)Math.Round(Math.Log10(c) * 10), _seed));
                            float r = fm.R, g = fm.G, b = fm.B;
                            if (atomish > 0) { var ae = El(fm.Atoms[(int)(Hash(ci, cj, 5, _seed) * fm.Atoms.Length) % fm.Atoms.Length]); if (ae != null) { r = (float)(r * (1 - atomish) + ae.R * atomish); g = (float)(g * (1 - atomish) + ae.G * atomish); b = (float)(b * (1 - atomish) + ae.B * atomish); } }
                            float f = (float)(wgt * Fade(s, Grain * 1.5, Grain * 0.6) * (1 - Fade(s, AtomSpacing * 14, AtomSpacing * 8)));
                            if (f > 0.01) Add(solid, x, -c * 0.05, z, c * 0.72, r * sh * f, g * sh * f, b * sh * f, s);
                        }
                }
            }
            // the mineral's atoms, rows about 0.25 nm apart, in its real ratio; one atom right at the point you dive into
            var focus = FocusAtom();
            if (fm != null && s < AtomSpacing * 14)
            {
                double a = AtomSpacing; long n = (long)Math.Min(30, Math.Ceiling(1.8 * s / a) + 1);
                float f = (float)Fade(s, AtomSpacing * 14, AtomSpacing * 9);
                for (long i = -n; i <= n; i++)
                    for (long j = -n; j <= n; j++)
                        for (long k = 0; k >= -2; k--)
                        {
                            var el = (i == 0 && j == 0 && k == 0) ? focus : El(fm.Atoms[(int)(Hash(i, j, k + 20, _seed) * fm.Atoms.Length) % fm.Atoms.Length]);
                            if (el == null) continue;
                            double ar = AtomRadius(el);
                            bool inFocus = i == 0 && j == 0 && k == 0 && s < ar * 4;
                            if (inFocus) continue;                                          // you are going into this one: drawn from inside below
                            float sh = (float)(1 - 0.25 * -k);
                            Add(solid, i * a, k * a - ar, j * a, ar * 0.9, el.R * sh * f, el.G * sh * f, el.B * sh * f, s);
                        }
            }
            // the atom under you, from inside: its electrons in their shells, moving round it, and its nucleus at the middle
            if (focus != null && s < AtomRadius(focus) * 4)
            {
                double ar = AtomRadius(focus), cy = -ar, nr = NucleusRadius(focus);
                int shells = focus.Shells.Length;
                float cloud = (float)Fade(s, ar * 4, ar * 2);
                for (int sh = 0; sh < shells; sh++)
                {
                    double rr = ar * (sh + 1) / shells;                                    // the shells, outermost at the atom's edge
                    // the shell itself: a faint glow where its electrons are most likely to be
                    for (int q = 0; q < 48; q++)
                    {
                        double u = q * 2.399963, v = Math.Acos(1 - 2 * (q + 0.5) / 48.0);
                        Add(glow, rr * Math.Sin(v) * Math.Cos(u), cy + rr * Math.Cos(v), rr * Math.Sin(v) * Math.Sin(u), rr * 0.35, 0.15f * cloud, 0.22f * cloud, 0.4f * cloud, s);
                    }
                    // its electrons, each on its own tilted path, the inner ones faster
                    int ne = focus.Shells[sh];
                    for (int e = 0; e < ne; e++)
                    {
                        double tilt = Hash(sh, e, 31, _seed) * Math.PI, turn = Hash(sh, e, 32, _seed) * Math.PI * 2, ang = time * (2.2 / (sh + 1)) + e * Math.PI * 2 / ne;
                        double ox = Math.Cos(ang) * rr, oz = Math.Sin(ang) * rr, oy = oz * Math.Sin(tilt); oz *= Math.Cos(tilt);
                        double x2 = ox * Math.Cos(turn) - oz * Math.Sin(turn), z2 = ox * Math.Sin(turn) + oz * Math.Cos(turn);
                        Add(glow, x2, cy + oy, z2, ar * 0.06, 0.4f * cloud, 0.75f * cloud, 1f * cloud, s);
                    }
                }
                // the nucleus: a bright spark from afar; close in, its protons (red) and neutrons (grey) packed in a ball
                if (s > nr * 40) Add(glow, 0, cy, 0, Math.Max(nr * 4, s * 0.004), 1f, 0.85f, 0.6f, s);
                else
                {
                    int A = focus.A, Z = focus.Z, protons = 0;
                    for (int k = 0; k < A; k++)
                    {
                        double rr = nr * Math.Pow((k + 0.5) / A, 1 / 3.0), u = k * 2.399963, v = Math.Acos(1 - 2 * ((k * 0.618034) % 1));
                        bool proton = protons < Z && (k * Z) / A >= protons; if (proton) protons++;
                        Add(solid, rr * Math.Sin(v) * Math.Cos(u), cy + rr * Math.Cos(v), rr * Math.Sin(v) * Math.Sin(u), Nucleon / 0.75,
                            proton ? 0.9f : 0.6f, proton ? 0.22f : 0.65f, proton ? 0.2f : 0.75f, s);
                    }
                }
            }
        }

        private void Put(List<StarVertex> l, ref StarVertex[] arr, ref UploadBuffer buf)
        {
            if (arr.Length < l.Count) arr = new StarVertex[l.Count + 2048];
            l.CopyTo(arr);
            long need = (long)Math.Max(1, l.Count) * Marshal.SizeOf(typeof(StarVertex));
            if (buf == null || buf.Bytes < need) { if (buf != null) buf.Dispose(); buf = new UploadBuffer(_gpu.Device, (long)(l.Count + 2048) * Marshal.SizeOf(typeof(StarVertex)) * 2); }
            if (l.Count > 0) buf.Write(arr, l.Count);
        }

        /// <summary>Draws the matter at <paramref name="scaleKm"/> (how far above the point you dive into), in view units.</summary>
        public void Draw(Mat4 view, double scaleKm, int w, int h, double time)
        {
            var solid = new List<StarVertex>(20000); var glow = new List<StarVertex>(2000);
            Build(scaleKm, time, solid, glow);
            Put(solid, ref _sv, ref _solidBuf); Put(glow, ref _gv, ref _glowBuf);
            Mat4 vp = view * Mat4.Perspective(0.9f, w / (float)Math.Max(1, h), 0.001f, 200f);
            var right = new Vec3(view.M11, view.M21, view.M31); var up = new Vec3(view.M12, view.M22, view.M32);
            var c = new float[BillboardPipeline.ConstantCount];
            c[0] = vp.M11; c[1] = vp.M12; c[2] = vp.M13; c[3] = vp.M14; c[4] = vp.M21; c[5] = vp.M22; c[6] = vp.M23; c[7] = vp.M24;
            c[8] = vp.M31; c[9] = vp.M32; c[10] = vp.M33; c[11] = vp.M34; c[12] = vp.M41; c[13] = vp.M42; c[14] = vp.M43; c[15] = vp.M44;
            c[16] = right.X; c[17] = right.Y; c[18] = right.Z; c[20] = up.X; c[21] = up.Y; c[22] = up.Z;
            c[24] = (float)time; c[25] = 1; c[28] = w; c[29] = h; c[30] = 0.0012f;
            c[26] = 1; if (solid.Count > 0) _dots.Draw(_solidBuf, solid.Count, c, 0, 0, w, h);    // grains, atoms, protons and neutrons: round, solid
            c[26] = 0; if (glow.Count > 0) _dots.Draw(_glowBuf, glow.Count, c, 0, 0, w, h);      // electrons, their shells and the nucleus's spark: glows
        }

        public void Dispose() { if (_solidBuf != null) _solidBuf.Dispose(); if (_glowBuf != null) _glowBuf.Dispose(); }
    }
}
