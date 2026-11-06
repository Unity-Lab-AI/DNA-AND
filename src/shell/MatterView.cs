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
        private UploadBuffer _solidBuf, _glowBuf, _meshBuf; private StarVertex[] _sv = new StarVertex[0], _gv = new StarVertex[0], _mv = new StarVertex[0];
        public const double TopKm = 2.6e-6;         // where the matter alone is drawn, about 1.5 mm up (above it the grains lie over the ground)
        public const double OverlayKm = 1e-5;       // 1 cm up: the grains begin to show over the ground, as specks that grow as you come down
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
        // the point you dive into (km, across the ground) and where it is easing to (what you picked)
        private double _fx, _fz, _tfx, _tfz, _lastTime = -1;
        private sealed class Body { public int Kind; public long I, J; public double X, Y, Z, Size; public string Sym; }   // 0 a grain, 1 an atom
        private readonly List<Body> _bodies = new List<Body>();
        private Mat4 _lastVp; private int _lastW = 1, _lastH = 1; private bool _haveVp;
        private Body _picked;

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
            _fx = _fz = _tfx = _tfz = 0; _picked = null;
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
        private long FocusGI { get { return (long)Math.Round(_fx / Grain); } }
        private long FocusGJ { get { return (long)Math.Round(_fz / Grain); } }
        private Mineral FocusMineral { get { return GrainMineral(FocusGI, FocusGJ); } }
        private long FocusAI { get { return (long)Math.Round(_fx / AtomSpacing); } }
        private long FocusAJ { get { return (long)Math.Round(_fz / AtomSpacing); } }
        private Element SiteAtom(Mineral m, long i, long j, long k)
        {
            if (m == null) return null;
            KeyValuePair<string, double[]>[] mol;
            if (Molecules.TryGetValue(m.Id, out mol)) return El(mol[0].Key);                    // a molecule's first atom sits on the site
            return El(m.Atoms[(int)(Hash(i, j, k + 20, _seed) * m.Atoms.Length) % m.Atoms.Length]);
        }

        // the atom right under you: the site of the focus grain's lattice nearest the point you dive into
        private Element FocusAtom() { double x, y, z; return Focus(out x, out y, out z); }

        // the atom you dive into and where it is: the nearest atom of the top layer to the point you dive into
        private Element Focus(out double x, out double y, out double z)
        {
            var m = FocusMineral; x = FocusAI * AtomSpacing; z = FocusAJ * AtomSpacing; y = 0;
            if (m == null) return null;
            if (Structured(m.Id))
            {
                Element best = null; double bd = double.MaxValue;
                foreach (var at in Crystal(m.Id, _fx, _fz, AtomSpacing * 3, 1))
                {
                    if (at.Y < -0.3 * AtomSpacing) continue;                                // the top layer only
                    double d = (at.X - _fx) * (at.X - _fx) + (at.Z - _fz) * (at.Z - _fz);
                    if (d < bd) { bd = d; best = at.E; x = at.X; y = at.Y; z = at.Z; }
                }
                if (best != null) { y -= AtomRadius(best); return best; }
            }
            var e = SiteAtom(m, FocusAI, FocusAJ, 0); y = -AtomRadius(e); return e;
        }

        // ── real crystal structures, where they are in: rock salt (halite) and the silica network (quartz) ──
        private sealed class CAtom { public Element E; public double X, Y, Z; }
        private static bool Structured(string id) { return id == "halite" || id == "quartz"; }

        // the atoms of a structured crystal within reach of (cx, cz), top layer at height 0 and below it 'layers' cells deep
        private List<CAtom> Crystal(string id, double cx, double cz, double reach, int layers)
        {
            var list = new List<CAtom>();
            if (id == "halite")
            {
                // rock salt: sodium and chlorine alternating on a cube grid, 0.282 nm apart
                double d = 2.82e-13; long n = (long)Math.Min(40, Math.Ceiling(reach / d) + 1), i0 = (long)Math.Round(cx / d), j0 = (long)Math.Round(cz / d);
                Element na = El("Na"), cl = El("Cl");
                for (long i = i0 - n; i <= i0 + n; i++)
                    for (long j = j0 - n; j <= j0 + n; j++)
                        for (long k = 0; k > -2 * layers - 1; k--)
                            list.Add(new CAtom { E = ((i + j + k) & 1) == 0 ? na : cl, X = i * d, Y = k * d, Z = j * d });
            }
            else if (id == "quartz")
            {
                // the silica network: each silicon bonded to four oxygens, each oxygen bridging two silicons (shown here in its
                // simplest form, cristobalite's cubic cell, 0.716 nm across: silicon on a diamond lattice, oxygen mid-bond)
                double A = 7.16e-13; long n = (long)Math.Min(12, Math.Ceiling(reach / A) + 1), i0 = (long)Math.Floor(cx / A), j0 = (long)Math.Floor(cz / A);
                Element si = El("Si"), o = El("O");
                double[][] fcc = { new[] { 0, 0, 0.0 }, new[] { 0, 0.5, 0.5 }, new[] { 0.5, 0, 0.5 }, new[] { 0.5, 0.5, 0.0 } };
                double[][] bonds = { new[] { 0.25, 0.25, 0.25 }, new[] { 0.25, -0.25, -0.25 }, new[] { -0.25, 0.25, -0.25 }, new[] { -0.25, -0.25, 0.25 } };
                for (long i = i0 - n; i <= i0 + n; i++)
                    for (long j = j0 - n; j <= j0 + n; j++)
                        for (long k = 0; k > -layers - 1; k--)
                            foreach (var b in fcc)
                            {
                                double bx = (i + b[0]) * A, by = (k + b[1]) * A - A * 0.25, bz = (j + b[2]) * A;
                                list.Add(new CAtom { E = si, X = bx, Y = by, Z = bz });
                                list.Add(new CAtom { E = si, X = bx + 0.25 * A, Y = by + 0.25 * A, Z = bz + 0.25 * A });
                                foreach (var v in bonds) list.Add(new CAtom { E = o, X = bx + v[0] * A * 0.5, Y = by + v[1] * A * 0.5, Z = bz + v[2] * A * 0.5 });
                            }
                list.RemoveAll(at => at.Y > 1e-20);                                          // the grain's face: nothing above it
            }
            return list;
        }

        // minerals made of separate molecules: each lattice site holds one, its atoms in the molecule's real shape (km, from its
        // first atom, which sits on the site). Network minerals (quartz, feldspar, olivine...) have none: their atoms are the sites.
        private static readonly Dictionary<string, KeyValuePair<string, double[]>[]> Molecules = BuildMolecules();
        private static Dictionary<string, KeyValuePair<string, double[]>[]> BuildMolecules()
        {
            var d = new Dictionary<string, KeyValuePair<string, double[]>[]>();
            double oh = 0.957e-13, half = 104.5 / 2 * Math.PI / 180;                       // water: O-H 0.0957 nm, bent at 104.5 degrees
            var water = new[] { P("O", 0, 0, 0), P("H", oh * Math.Sin(half), oh * Math.Cos(half), 0), P("H", -oh * Math.Sin(half), oh * Math.Cos(half), 0) };
            d["water-ice"] = water; d["sea-water"] = water;
            double co = 1.16e-13;                                                            // carbon dioxide: straight, C=O 0.116 nm
            d["dry-ice"] = new[] { P("C", 0, 0, 0), P("O", co, 0, 0), P("O", -co, 0, 0) };
            double ch = 1.09e-13 / Math.Sqrt(3);                                             // methane: a tetrahedron, C-H 0.109 nm
            d["methane-ice"] = new[] { P("C", 0, 0, 0), P("H", ch, ch, ch), P("H", ch, -ch, -ch), P("H", -ch, ch, -ch), P("H", -ch, -ch, ch) };
            d["nitrogen-ice"] = new[] { P("N", 0, 0, 0), P("N", 1.10e-13, 0, 0) };          // nitrogen: a pair, N-N 0.110 nm
            return d;
        }
        private static KeyValuePair<string, double[]> P(string el, double x, double y, double z) { return new KeyValuePair<string, double[]>(el, new[] { x, y, z }); }

        // turns a molecule's offset by its site's own orientation (from the site's hash)
        private void Orient(long i, long j, long k, double[] v, out double x, out double y, out double z)
        {
            double a = Hash(i, j, k + 40, _seed) * Math.PI * 2, b = Math.Acos(1 - 2 * Hash(i, j, k + 41, _seed)), c = Hash(i, j, k + 42, _seed) * Math.PI * 2;
            double x1 = v[0] * Math.Cos(a) - v[1] * Math.Sin(a), y1 = v[0] * Math.Sin(a) + v[1] * Math.Cos(a), z1 = v[2];
            double y2 = y1 * Math.Cos(b) - z1 * Math.Sin(b), z2 = y1 * Math.Sin(b) + z1 * Math.Cos(b);
            x = x1 * Math.Cos(c) - z2 * Math.Sin(c); z = x1 * Math.Sin(c) + z2 * Math.Cos(c); y = y2;
        }

        private static double AtomRadius(Element e) { return e == null ? 1e-13 : e.Z == 1 ? 0.53e-13 : (0.7 + 0.5 * Math.Min(1, e.Z / 30.0)) * 1e-13; }
        private static double NucleusRadius(Element e) { return 1.2e-18 * Math.Pow(e == null ? 28 : e.A, 1 / 3.0); }

        public string MineralName { get { var m = FocusMineral; return m != null ? m.Name : "rock"; } }

        /// <summary>What you are looking at, at this scale.</summary>
        public string Stage(double s)
        {
            var m = FocusMineral; var e = FocusAtom();
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

        private void AddBody(int kind, long i, long j, double x, double y, double z, double size, double s)
        {
            double vx = (x - _fx) / s, vz = (z - _fz) / s, vs = size / s;
            if (Math.Abs(vx) > 6 || Math.Abs(vz) > 6 || vs < 0.004) return;               // only bodies big enough on screen to pick
            _bodies.Add(new Body { Kind = kind, I = i, J = j, X = x, Y = y, Z = z, Size = size });
        }

        private void Add(List<StarVertex> l, double x, double y, double z, double size, float r, float g, float b, double s)
        {
            x -= _fx; z -= _fz;                                                             // around the point you dive into
            double vx = x / s, vy = y / s - 0.6, vz = z / s, vs = size / s;                  // view units: one unit = the scale; you are 0.6 above
            if (Math.Abs(vx) > 60 || Math.Abs(vz) > 60 || vy < -60 || vs < 2e-4 || vs > 400) return;
            l.Add(new StarVertex { X = (float)vx, Y = (float)vy, Z = (float)vz, Size = (float)vs, R = r, G = g, B = b, A = 1 });
        }

        private static double Fade(double s, double from, double to) { return Math.Max(0, Math.Min(1, Math.Log(from / s) / Math.Log(from / to))); }

        // a grain as a lit, lumpy stone: a once-split icosahedron, squashed and turned its own way, each corner pushed in or out
        private static Vec3[] _ico; private static int[] _icoTri;
        private static void MakeIco()
        {
            double t = (1 + Math.Sqrt(5)) / 2;
            var v = new List<Vec3> { new Vec3(-1, (float)t, 0), new Vec3(1, (float)t, 0), new Vec3(-1, (float)-t, 0), new Vec3(1, (float)-t, 0), new Vec3(0, -1, (float)t), new Vec3(0, 1, (float)t),
                new Vec3(0, -1, (float)-t), new Vec3(0, 1, (float)-t), new Vec3((float)t, 0, -1), new Vec3((float)t, 0, 1), new Vec3((float)-t, 0, -1), new Vec3((float)-t, 0, 1) };
            int[] f = { 0,11,5, 0,5,1, 0,1,7, 0,7,10, 0,10,11, 1,5,9, 5,11,4, 11,10,2, 10,7,6, 7,1,8, 3,9,4, 3,4,2, 3,2,6, 3,6,8, 3,8,9, 4,9,5, 2,4,11, 6,2,10, 8,6,7, 9,8,1 };
            var mid = new Dictionary<long, int>(); var tri = new List<int>();
            Func<int, int, int> m = (a, b) => { long key = Math.Min(a, b) * 1000L + Math.Max(a, b); int idx; if (mid.TryGetValue(key, out idx)) return idx; var p = (v[a] + v[b]) * 0.5f; v.Add(p); mid[key] = v.Count - 1; return v.Count - 1; };
            for (int i = 0; i < f.Length; i += 3)
            {
                int a = f[i], b = f[i + 1], c = f[i + 2], ab = m(a, b), bc = m(b, c), ca = m(c, a);
                tri.AddRange(new[] { a, ab, ca, b, bc, ab, c, ca, bc, ab, bc, ca });
            }
            _ico = new Vec3[v.Count]; for (int i = 0; i < v.Count; i++) _ico[i] = v[i].Normalized; _icoTri = tri.ToArray();
        }

        private void Stone(List<StarVertex> l, long gi, long gj, double cx, double cy, double cz, double rad, float r, float g, float b, double s)
        {
            if (_ico == null) MakeIco();
            double sx = 0.8 + 0.4 * Hash(gi, gj, 6, _seed), sy = 0.6 + 0.35 * Hash(gi, gj, 7, _seed), sz = 0.8 + 0.4 * Hash(gi, gj, 8, _seed), turn = Hash(gi, gj, 9, _seed) * Math.PI * 2;
            double ct = Math.Cos(turn), st = Math.Sin(turn);
            var pts = new double[_ico.Length * 3];
            for (int k = 0; k < _ico.Length; k++)
            {
                double bump = 0.82 + 0.3 * Hash(gi * 131 + k, gj, 10, _seed);
                double x = _ico[k].X * sx * bump, y = _ico[k].Y * sy * bump, z = _ico[k].Z * sz * bump;
                pts[k * 3] = (cx + (x * ct - z * st) * rad - _fx) / s; pts[k * 3 + 1] = (cy + y * rad) / s - 0.6; pts[k * 3 + 2] = (cz + (x * st + z * ct) * rad - _fz) / s;
            }
            foreach (int k in _icoTri) l.Add(new StarVertex { X = (float)pts[k * 3], Y = (float)pts[k * 3 + 1], Z = (float)pts[k * 3 + 2], Size = 0, R = r, G = g, B = b, A = 1 });
        }

        /// <summary>The starlight's way in the ground's own frame (x east, y up, z north), for lighting the grains.</summary>
        public float[] Light = { 0.4f, 0.8f, 0.3f };

        private void Build(double s, double time, List<StarVertex> solid, List<StarVertex> glow, List<StarVertex> mesh, bool overlay)
        {
            // grains: lumpy stones, each its own mineral from the mix, scattered (not on a grid) in two layers, the top one
            // resting with its tops at the level you dive into; near ones are lit 3D stones, far ones specks of their colour
            if (s > AtomSpacing * 40)
            {
                long reach = (long)Math.Ceiling(Math.Min(110, 2.2 * s / Grain)) + 1, gi0 = FocusGI, gj0 = FocusGJ;
                int meshes = 0;
                for (int layer = 0; layer < 2; layer++)
                    for (long i = gi0 - reach; i <= gi0 + reach; i++)
                        for (long j = gj0 - reach; j <= gj0 + reach; j++)
                        {
                            long gi = i * 2 + layer, gj = j * 2 + layer;                       // each layer's own grains
                            var m = GrainMineral(gi, gj); if (m == null) continue;
                            double rad = Grain * (0.3 + 0.2 * Hash(gi, gj, 1, _seed));
                            double gx = (i + layer * 0.5 + (Hash(gi, gj, 2, _seed) - 0.5) * 0.55) * Grain, gz = (j + layer * 0.5 + (Hash(gi, gj, 3, _seed) - 0.5) * 0.55) * Grain;
                            double gy = layer == 0 ? -rad * (0.85 + 0.3 * Hash(gi, gj, 5, _seed)) : -Grain * 0.75 - rad * 0.6;
                            float sh = (float)(0.75 + 0.35 * Hash(gi, gj, 4, _seed)) * (layer == 0 ? 1f : 0.7f);
                            double dx = gx - _fx, dz = gz - _fz, dist = Math.Sqrt(dx * dx + dz * dz + (0.6 * s - gy) * (0.6 * s - gy));
                            double px = rad / Math.Max(1e-30, dist) * _lastH / (2 * Math.Tan(0.45));    // its size on screen, in pixels
                            if (px > 5 && meshes < 1400 && mesh != null) { Stone(mesh, gi, gj, gx, gy, gz, rad, m.R * sh, m.G * sh, m.B * sh, s); meshes++; }
                            else Add(solid, gx, gy, gz, rad / 0.75, m.R * sh, m.G * sh, m.B * sh, s);
                            if (layer == 0) AddBody(0, gi, gj, gx, gy, gz, rad, s);
                        }
            }
            if (overlay) return;                                                        // over the ground: only its grains so far
            // the focus grain's surface: detail at every size, fixed in place (cells of powers of ten, two at a time, blended),
            // in the grain's colour; its finest cells take on the colours of its atoms
            var fm = FocusMineral;
            if (fm != null && s < Grain * 1.5 && s > AtomSpacing * 8)
            {
                double c1 = Math.Pow(10, Math.Floor(Math.Log10(s / 30))), frac = Math.Log10(s / 30) - Math.Floor(Math.Log10(s / 30));
                double atomish = Fade(s, AtomSpacing * 200, AtomSpacing * 20);
                for (int lv = 0; lv < 2; lv++)
                {
                    double c = lv == 0 ? c1 : c1 * 10, wgt = lv == 0 ? 1 - frac : frac;
                    long n = (long)Math.Ceiling(1.6 * s / c), ci0 = (long)Math.Round(_fx / c), cj0 = (long)Math.Round(_fz / c);
                    if (n > 70) continue;
                    for (long i = ci0 - n; i <= ci0 + n; i++)
                        for (long j = cj0 - n; j <= cj0 + n; j++)
                        {
                            double x = i * c, z = j * c;
                            if ((x - _fx) * (x - _fx) + (z - _fz) * (z - _fz) > 2.6 * s * s) continue;
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
            double fax, fay, faz; var focus = Focus(out fax, out fay, out faz);
            if (fm != null && s < AtomSpacing * 14 && Structured(fm.Id))
            {
                float f = (float)Fade(s, AtomSpacing * 14, AtomSpacing * 9);
                foreach (var at in Crystal(fm.Id, _fx, _fz, 1.8 * s, 1))
                {
                    double ar = AtomRadius(at.E), ay = at.Y - ar;
                    bool inFocus = at.E == focus && Math.Abs(at.X - fax) < 1e-20 && Math.Abs(ay - fay) < 1e-20 && Math.Abs(at.Z - faz) < 1e-20 && s < ar * 4;
                    if (inFocus) continue;
                    float sh = (float)(1 + 0.6 * at.Y / AtomSpacing * 0.25); sh = Math.Max(0.45f, sh);
                    Add(solid, at.X, ay, at.Z, ar * 0.62, at.E.R * sh * f, at.E.G * sh * f, at.E.B * sh * f, s);
                    if (at.Y > -0.3 * AtomSpacing) { AddBody(1, 0, 0, at.X, ay, at.Z, ar, s); if (_bodies.Count > 0) _bodies[_bodies.Count - 1].Sym = at.E.Symbol; }
                }
            }
            else if (fm != null && s < AtomSpacing * 14)
            {
                double a = AtomSpacing; long n = (long)Math.Min(30, Math.Ceiling(1.8 * s / a) + 1), ai0 = FocusAI, aj0 = FocusAJ;
                float f = (float)Fade(s, AtomSpacing * 14, AtomSpacing * 9);
                for (long i = ai0 - n; i <= ai0 + n; i++)
                    for (long j = aj0 - n; j <= aj0 + n; j++)
                        for (long k = 0; k >= -2; k--)
                        {
                            var el = SiteAtom(fm, i, j, k);
                            if (el == null) continue;
                            double ar = AtomRadius(el);
                            bool inFocus = i == ai0 && j == aj0 && k == 0 && s < ar * 4;
                            if (inFocus) continue;                                          // you are going into this one: drawn from inside below
                            float sh = (float)(1 - 0.25 * -k);
                            KeyValuePair<string, double[]>[] mol;
                            if (Molecules.TryGetValue(fm.Id, out mol))
                            {
                                // a whole molecule on this site, in its real shape, each turned its own way
                                for (int q = 1; q < mol.Length; q++)
                                {
                                    var me = El(mol[q].Key); if (me == null) continue; double ox, oy, oz; Orient(i, j, k, mol[q].Value, out ox, out oy, out oz);
                                    double mr = AtomRadius(me) * 0.55;                       // bonded atoms overlap: drawn at their bonded size
                                    Add(solid, i * a + ox, k * a - ar + oy, j * a + oz, mr, me.R * sh * f, me.G * sh * f, me.B * sh * f, s);
                                }
                                Add(solid, i * a, k * a - ar, j * a, ar * 0.55, el.R * sh * f, el.G * sh * f, el.B * sh * f, s);
                            }
                            else Add(solid, i * a, k * a - ar, j * a, ar * 0.9, el.R * sh * f, el.G * sh * f, el.B * sh * f, s);
                            if (k == 0) { int nb = _bodies.Count; AddBody(1, i, j, i * a, -ar, j * a, ar, s); if (_bodies.Count > nb) _bodies[nb].Sym = el.Symbol; }
                        }
            }
            // the atom under you, from inside: its electrons in their shells, moving round it, and its nucleus at the middle
            if (focus != null && s < AtomRadius(focus) * 4)
            {
                double ar = AtomRadius(focus), cy = fay, nr = NucleusRadius(focus), ax0 = fax, az0 = faz;
                int shells = focus.Shells.Length;
                float cloud = (float)Fade(s, ar * 4, ar * 2);
                for (int sh = 0; sh < shells; sh++)
                {
                    double rr = ar * (sh + 1) / shells;                                    // the shells, outermost at the atom's edge
                    // the shell itself: a faint glow where its electrons are most likely to be
                    for (int q = 0; q < 48; q++)
                    {
                        double u = q * 2.399963, v = Math.Acos(1 - 2 * (q + 0.5) / 48.0);
                        Add(glow, ax0 + rr * Math.Sin(v) * Math.Cos(u), cy + rr * Math.Cos(v), az0 + rr * Math.Sin(v) * Math.Sin(u), rr * 0.35, 0.08f * cloud, 0.12f * cloud, 0.22f * cloud, s);
                    }
                    // its electrons, each on its own tilted path, the inner ones faster
                    int ne = focus.Shells[sh];
                    for (int e = 0; e < ne; e++)
                    {
                        double tilt = Hash(sh, e, 31, _seed) * Math.PI, turn = Hash(sh, e, 32, _seed) * Math.PI * 2, ang = time * (2.2 / (sh + 1)) + e * Math.PI * 2 / ne;
                        // its path: a faint ring, like a planet's orbit in a solar system
                        for (int q = 0; q < 40; q++)
                        {
                            double pa = q * Math.PI * 2 / 40, px = Math.Cos(pa) * rr, pz = Math.Sin(pa) * rr, py = pz * Math.Sin(tilt); pz *= Math.Cos(tilt);
                            Add(glow, ax0 + px * Math.Cos(turn) - pz * Math.Sin(turn), cy + py, az0 + px * Math.Sin(turn) + pz * Math.Cos(turn), ar * 0.012, 0.1f * cloud, 0.18f * cloud, 0.28f * cloud, s);
                        }
                        double ox = Math.Cos(ang) * rr, oz = Math.Sin(ang) * rr, oy = oz * Math.Sin(tilt); oz *= Math.Cos(tilt);
                        double x2 = ox * Math.Cos(turn) - oz * Math.Sin(turn), z2 = ox * Math.Sin(turn) + oz * Math.Cos(turn);
                        Add(glow, ax0 + x2, cy + oy, az0 + z2, ar * 0.06, 0.4f * cloud, 0.75f * cloud, 1f * cloud, s);
                    }
                }
                // the nucleus: a bright spark from afar; close in, its protons (red) and neutrons (grey) packed in a ball
                if (s > nr * 40) Add(glow, ax0, cy, az0, Math.Max(nr * 4, s * 0.004), 1f, 0.85f, 0.6f, s);
                else
                {
                    int A = focus.A, Z = focus.Z, protons = 0;
                    for (int k = 0; k < A; k++)
                    {
                        double rr = nr * Math.Pow((k + 0.5) / A, 1 / 3.0), u = k * 2.399963, v = Math.Acos(1 - 2 * ((k * 0.618034) % 1));
                        bool proton = protons < Z && (k * Z) / A >= protons; if (proton) protons++;
                        Add(solid, ax0 + rr * Math.Sin(v) * Math.Cos(u), cy + rr * Math.Cos(v), az0 + rr * Math.Sin(v) * Math.Sin(u), Nucleon / 0.75,
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
        public void Draw(Mat4 view, double scaleKm, int w, int h, double time) { Draw(view, scaleKm, w, h, time, null, false); }

        /// <summary>Draws the matter; with <paramref name="overlay"/>, only its grains, lying over the ground still drawn below.</summary>
        public void Draw(Mat4 view, double scaleKm, int w, int h, double time, MeshPipes pipes, bool overlay)
        {
            _lastH = h;
            // the point you dive into eases across to what you picked
            double dt = _lastTime < 0 ? 0 : Math.Max(0, Math.Min(0.1, time - _lastTime)); _lastTime = time;
            double k = 1 - Math.Exp(-dt * 4); _fx += (_tfx - _fx) * k; _fz += (_tfz - _fz) * k;
            var solid = new List<StarVertex>(20000); var glow = new List<StarVertex>(2000); var mesh = pipes != null ? new List<StarVertex>(60000) : null;
            _bodies.Clear();
            Build(scaleKm, time, solid, glow, mesh, overlay);
            Put(solid, ref _sv, ref _solidBuf); Put(glow, ref _gv, ref _glowBuf);
            if (mesh != null) Put(mesh, ref _mv, ref _meshBuf);
            Mat4 vp = view * Mat4.Perspective(0.9f, w / (float)Math.Max(1, h), 0.001f, 200f);
            _lastVp = vp; _lastW = w; _lastH = h; _haveVp = true; _scale = scaleKm; _lastView = view;
            var right = new Vec3(view.M11, view.M21, view.M31); var up = new Vec3(view.M12, view.M22, view.M32);
            var c = new float[BillboardPipeline.ConstantCount];
            c[0] = vp.M11; c[1] = vp.M12; c[2] = vp.M13; c[3] = vp.M14; c[4] = vp.M21; c[5] = vp.M22; c[6] = vp.M23; c[7] = vp.M24;
            c[8] = vp.M31; c[9] = vp.M32; c[10] = vp.M33; c[11] = vp.M34; c[12] = vp.M41; c[13] = vp.M42; c[14] = vp.M43; c[15] = vp.M44;
            c[16] = right.X; c[17] = right.Y; c[18] = right.Z; c[20] = up.X; c[21] = up.Y; c[22] = up.Z;
            c[24] = (float)time; c[25] = 1; c[28] = w; c[29] = h; c[30] = 0.0012f;
            c[26] = 1; if (solid.Count > 0) _dots.Draw(_solidBuf, solid.Count, c, 0, 0, w, h);    // grains, atoms, protons and neutrons: round, solid
            c[26] = 0; if (glow.Count > 0) _dots.Draw(_glowBuf, glow.Count, c, 0, 0, w, h);      // electrons, their shells and the nucleus's spark: glows
            if (mesh != null && mesh.Count > 0)
            {
                // the near grains as lit stones (flat facets), in the starlight of where you are
                var mc = new float[BillboardPipeline.ConstantCount];
                for (int q = 0; q < 16; q++) mc[q] = c[q];
                mc[16] = Light[0]; mc[17] = Light[1]; mc[18] = Light[2]; mc[20] = 0; mc[21] = 0; mc[22] = 0; mc[25] = 0; mc[26] = 0;
                pipes.Solid.Draw(_meshBuf, mesh.Count, mc, 0, 0, w, h);
            }
        }

        private double _scale = TopKm; private Mat4 _lastView;

        /// <summary>Picks the grain or atom under the mouse (within about 24 pixels): the point you dive into eases to it.
        /// False if nothing is there.</summary>
        public bool Pick(int mx, int my)
        {
            if (!_haveVp) return false;
            double best = Math.Pow(24 * Math.Max(1, _lastH / 1080.0), 2); Body pick = null; var m = _lastVp;
            foreach (var b in _bodies)
            {
                float x = (float)((b.X - _fx) / _scale), y = (float)(b.Y / _scale - 0.6), z = (float)((b.Z - _fz) / _scale);
                float cx = x * m.M11 + y * m.M21 + z * m.M31 + m.M41, cy = x * m.M12 + y * m.M22 + z * m.M32 + m.M42, cw = x * m.M14 + y * m.M24 + z * m.M34 + m.M44;
                if (cw <= 0) continue;
                double sx = (cx / cw * 0.5 + 0.5) * _lastW, sy = (0.5 - cy / cw * 0.5) * _lastH, d2 = (sx - mx) * (sx - mx) + (sy - my) * (sy - my);
                double rpx = b.Size / _scale / cw / Math.Tan(0.45) * _lastH * 0.5;            // its own size on screen counts too
                if (d2 < Math.Max(best, rpx * rpx) && (pick == null || d2 < best)) { best = d2; pick = b; }
            }
            if (pick == null)
            {
                // nothing right there: the spot you clicked on the surface (where the line under the mouse meets it)
                var v = _lastView; var right = new Vec3(v.M11, v.M21, v.M31); var up = new Vec3(v.M12, v.M22, v.M32); var fwd = Vec3.Cross(up, right).Normalized;
                double t = Math.Tan(0.45), nx = mx / (double)Math.Max(1, _lastW) * 2 - 1, ny = 1 - my / (double)Math.Max(1, _lastH) * 2, aspect = _lastW / (double)Math.Max(1, _lastH);
                for (int sign = 0; sign < 2; sign++)
                {
                    var f = sign == 0 ? fwd : new Vec3(-fwd.X, -fwd.Y, -fwd.Z);
                    var d = (f + right * (float)(nx * t * aspect) + up * (float)(ny * t)).Normalized;
                    float cw = d.X * m.M14 + d.Y * m.M24 + d.Z * m.M34 + m.M44;              // in front of you?
                    if (cw <= 0 || d.Y >= -1e-4f) continue;
                    double k = -0.6 / d.Y;                                                   // the surface lies 0.6 below you
                    _picked = null; _tfx = _fx + d.X * k * _scale; _tfz = _fz + d.Z * k * _scale; return true;
                }
                return false;
            }
            _picked = pick; _tfx = pick.X; _tfz = pick.Z; return true;
        }

        /// <summary>Puts the point you dive into right under you (km east and north of where this spot's matter began), so the
        /// grains pass by as you fly over them.</summary>
        public void Follow(double east, double north) { _fx = _tfx = east; _fz = _tfz = north; _picked = null; }

        /// <summary>Lets go of the pick (the point you dive into stays where it is).</summary>
        public void Unpick() { _picked = null; _tfx = _fx; _tfz = _fz; }

        /// <summary>The information card: what you are in, and what you picked.</summary>
        public List<string> Card()
        {
            var L = new List<string>();
            if (_picked != null && _picked.Kind == 0)
            {
                var g = GrainMineral(_picked.I, _picked.J); if (g == null) return L;
                L.Add("a grain of " + g.Name.ToLowerInvariant()); L.Add("formula: " + g.Formula);
                L.Add("width: " + ScaleHud.Words(_picked.Size * 2));
                var seen = new List<string>(); foreach (var a in g.Atoms) if (!seen.Contains(a)) seen.Add(a);
                L.Add("elements: " + string.Join(", ", seen.ToArray()));
                L.Add("ground: " + (_mixes.ContainsKey(_mixId) ? _mixId : "rock"));
                return L;
            }
            var e = _picked != null && _picked.Kind == 1 && _picked.Sym != null ? El(_picked.Sym) : FocusAtom();
            var fm = FocusMineral;
            if (e == null) return L;
            L.Add((_picked != null ? "an atom of " : "under you: ") + e.Name.ToLowerInvariant() + " (" + e.Symbol + ")");
            L.Add("atomic number " + e.Z + ", mass " + e.A);
            L.Add("protons: " + e.Z + ", neutrons: " + (e.A - e.Z) + ", electrons: " + e.Z);
            L.Add("shells: " + string.Join(", ", Array.ConvertAll(e.Shells, x => x.ToString())));
            L.Add("width: about " + ScaleHud.Words(AtomRadius(e) * 2) + ", nucleus " + ScaleHud.Words(NucleusRadius(e) * 2));
            if (fm != null && Molecules.ContainsKey(fm.Id)) L.Add("in a molecule of " + fm.Formula.Split(' ')[0] + ", in " + fm.Name.ToLowerInvariant());
            else if (fm != null) L.Add("in a grain of " + fm.Name.ToLowerInvariant() + " (" + fm.Formula + ")" + (fm.Id == "halite" ? ", a rock-salt crystal" : fm.Id == "quartz" ? ", a silica network" : ""));
            return L;
        }

        public void Dispose() { if (_solidBuf != null) _solidBuf.Dispose(); if (_glowBuf != null) _glowBuf.Dispose(); if (_meshBuf != null) _meshBuf.Dispose(); }
    }
}
