// DNA-AND · shell · the ground's own matter, all the way down: below the finest piece of ground the camera carries on into what
// the ground is made of. First grains (sand, dust, rock chips) in the ground's own colour, their surfaces grown by mode-E; then,
// below about a micrometre, the mineral's own crystal (quartz on rocky land, water on seas and ice, basalt on lava worlds)
// as a lattice of its atoms; then the atoms themselves, coloured in chemistry's usual colours (hydrogen white, oxygen red,
// silicon tan, iron orange, magnesium green), each a small glow round its nucleus. One scale runs through all of it; each
// stage fades into the next, so nothing switches. (Microbes, bacteria and viruses join when life is built.)
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
        private UploadBuffer _buf; private StarVertex[] _v = new StarVertex[0]; private int _n;
        private double _builtScale = -1;
        public const double TopKm = 1e-6;          // 1 mm: where the matter begins (the ground's finest pieces are above this)
        public const double BottomKm = 2e-14;      // 0.02 nm: inside a single atom's glow
        private const double AtomSpacing = 2.5e-13; // about 0.25 nm between atoms in a crystal

        private sealed class Mineral { public string Name; public string[] Atoms; }
        private static readonly Dictionary<string, float[]> Cpk = new Dictionary<string, float[]>
        {
            { "H", new[] { 0.95f, 0.95f, 0.95f } }, { "O", new[] { 0.9f, 0.13f, 0.1f } }, { "Si", new[] { 0.85f, 0.7f, 0.5f } },
            { "Fe", new[] { 0.88f, 0.4f, 0.2f } }, { "Mg", new[] { 0.3f, 0.8f, 0.25f } }, { "Al", new[] { 0.75f, 0.65f, 0.65f } }, { "Ca", new[] { 0.3f, 1f, 0.3f } }
        };
        private Mineral _mineral; private float _gr, _gg, _gb;

        public MatterView(Gpu gpu, BillboardPipeline pipe) { _gpu = gpu; _dots = new BillboardDrawer(gpu.CommandList, pipe); }

        /// <summary>What the ground at this spot is made of: its kind, whether it is under water or ice, and its colour.</summary>
        public void Set(string planetKind, bool sea, bool ice, float r, float g, float b)
        {
            string[] atoms; string name;
            if (sea || ice || planetKind == "snowball") { name = ice ? "ice (water)" : "sea water"; atoms = new[] { "O", "H", "H" }; }
            else if (planetKind == "lava") { name = "basalt"; atoms = new[] { "Si", "O", "O", "Fe", "O", "Mg", "O", "Si", "O", "O" }; }
            else { name = "quartz"; atoms = new[] { "Si", "O", "O" }; }
            if (_mineral == null || _mineral.Name != name) { _mineral = new Mineral { Name = name, Atoms = atoms }; _builtScale = -1; }
            _gr = r; _gg = g; _gb = b;
        }

        public string MineralName { get { return _mineral != null ? _mineral.Name : "rock"; } }

        /// <summary>What you are looking at, at this scale (km across the view).</summary>
        public string Stage(double scaleKm)
        {
            if (scaleKm > 2e-9) return "grains of " + MineralName;
            if (scaleKm > 2.5e-11) return "the crystal of " + MineralName;
            return "atoms of " + MineralName;
        }

        // how much of a stage shows at this scale (0 to 1), fading across a factor of ten either side
        private static double Band(double scaleKm, double from, double to)
        {
            double a = Math.Log10(scaleKm), hi = Math.Log10(from), lo = Math.Log10(to);
            if (a > hi + 0.5 || a < lo - 0.5) return 0;
            double t = Math.Min(1, Math.Min(hi + 0.5 - a, a - (lo - 0.5)));
            return Math.Max(0, Math.Min(1, t));
        }

        // rebuild the points when the scale has moved by more than a few per cent
        private void Build(double s)
        {
            if (_builtScale > 0 && Math.Abs(Math.Log(s / _builtScale)) < 0.04) return;
            _builtScale = s;
            var l = new List<StarVertex>(20000);
            // grains: a layer of grains lying below you, each a few tenths of a millimetre, its shade grown by mode-E
            double gb = Band(s, TopKm, 2e-9);
            if (gb > 0)
            {
                double cell = Math.Max(s / 30, 3e-7);                                // grains never smaller than 0.3 mm across
                int n = 40;
                var view = ModeE.View.For(TopKm / s, 0.4, 0.5, 0.3);
                for (int i = -n; i <= n; i++)
                    for (int j = -n; j <= n; j++)
                    {
                        double x = i * cell, z = j * cell;
                        var p = ModeE.At(x / s * 2, z / s * 2, view);
                        float sh = (float)Math.Min(1.2, p.Shade) * (float)gb;
                        l.Add(new StarVertex { X = (float)(x / s), Y = (float)(-0.6 - p.Shade * 0.05), Z = (float)(z / s), Size = (float)(cell / s * 0.6), R = _gr * sh, G = _gg * sh, B = _gb * sh, A = 1 });
                    }
            }
            // the crystal and its atoms: a lattice of the mineral's atoms below you
            double cb = Math.Max(Band(s, 2e-9, 2.5e-11), Band(s, 2.5e-11, BottomKm));
            if (cb > 0 && _mineral != null && s > AtomSpacing * 60)
            {
                // wider than about 60 atoms: the crystal's face, filling the view, its texture grown by mode-E in the mineral's colours
                float[] c0 = Cpk[_mineral.Atoms[0]], c1 = Cpk[_mineral.Atoms[Math.Min(1, _mineral.Atoms.Length - 1)]];
                var view = ModeE.View.For(1e-9 / s, 0.7, 0.3, 1.1); int n = 60; double cell = s / 24;
                for (int i = -n; i <= n; i++)
                    for (int j = -n; j <= n; j++)
                    {
                        var p = ModeE.At(i * cell / s, j * cell / s, view); float t = (float)Math.Min(1, p.Shade * 0.8), f = (float)cb;
                        l.Add(new StarVertex { X = (float)(i * cell / s), Y = -0.6f, Z = (float)(j * cell / s), Size = (float)(cell / s * 0.75), R = (c0[0] * t + c1[0] * (1 - t)) * f, G = (c0[1] * t + c1[1] * (1 - t)) * f, B = (c0[2] * t + c1[2] * (1 - t)) * f, A = 1 });
                    }
            }
            else if (cb > 0 && _mineral != null)
            {
                // the atoms themselves, enough of them to fill the view
                double a = AtomSpacing; int half = (int)Math.Min(40, Math.Max(3, s / a * 1.4));
                for (int i = -half; i <= half; i++)
                    for (int j = -half; j <= half; j++)
                        for (int k = -6; k <= 0; k++)
                        {
                            int idx = Math.Abs(i * 7 + j * 13 + k * 3) % _mineral.Atoms.Length;
                            string el = _mineral.Atoms[idx]; float[] c = Cpk[el];
                            double rad = el == "H" ? 0.31e-13 : el == "O" ? 0.6e-13 : 1.1e-13;          // atoms' real sizes, about
                            float f = (float)cb;
                            l.Add(new StarVertex { X = (float)(i * a / s), Y = (float)((k * a - 0.6 * s) / s), Z = (float)(j * a / s), Size = (float)(rad * 2.2 / s), R = c[0] * f, G = c[1] * f, B = c[2] * f, A = 1 });
                        }
            }
            _n = l.Count;
            if (_v.Length < _n) _v = new StarVertex[_n + 2048];
            l.CopyTo(_v);
            if (_buf == null || _buf.Bytes < (long)_n * Marshal.SizeOf(typeof(StarVertex))) { if (_buf != null) _buf.Dispose(); _buf = new UploadBuffer(_gpu.Device, (long)(_n + 2048) * Marshal.SizeOf(typeof(StarVertex)) * 2); }
            _buf.Write(_v, _n);
        }

        /// <summary>Draws the matter at <paramref name="scaleKm"/> (km across the view), in view units (one unit = the scale).</summary>
        public void Draw(Mat4 view, double scaleKm, int w, int h, double time)
        {
            Build(scaleKm);
            if (_n == 0) return;
            Mat4 vp = view * Mat4.Perspective(0.9f, w / (float)Math.Max(1, h), 0.01f, 200f);
            var right = new Vec3(view.M11, view.M21, view.M31); var up = new Vec3(view.M12, view.M22, view.M32);
            var c = new float[BillboardPipeline.ConstantCount];
            c[0] = vp.M11; c[1] = vp.M12; c[2] = vp.M13; c[3] = vp.M14; c[4] = vp.M21; c[5] = vp.M22; c[6] = vp.M23; c[7] = vp.M24;
            c[8] = vp.M31; c[9] = vp.M32; c[10] = vp.M33; c[11] = vp.M34; c[12] = vp.M41; c[13] = vp.M42; c[14] = vp.M43; c[15] = vp.M44;
            c[16] = right.X; c[17] = right.Y; c[18] = right.Z; c[20] = up.X; c[21] = up.Y; c[22] = up.Z;
            c[24] = (float)time; c[25] = 1; c[26] = 0; c[27] = 4; c[28] = w; c[29] = h; c[30] = 0.001f;
            _dots.Draw(_buf, _n, c, 0, 0, w, h);
        }

        public void Dispose() { if (_buf != null) _buf.Dispose(); }
    }
}
