// DNA-AND · shell · the one world: a single camera that flies through every scale at any angle, with no views to switch.
// The universe of galaxies and each galaxy's own stars are drawn together; as you come closer to a galaxy its far glow
// fades out while its real stars fade in, so it grows into its stars without a seam. Zooming moves the camera along the
// line under the mouse by a share of the distance to whatever is nearest, so it slows by itself as things come close
// and never runs out. Positions are kept in a chain of frames (the universe, then a galaxy), each with its own origin,
// so nothing loses precision however deep you go. (Star systems, planets and the ground join the same camera next.)
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DnaAnd.Core;
using DnaAnd.Gen;
using DnaAnd.Platform;
using DnaAnd.Render;

namespace DnaAnd.Shell
{
    internal sealed class WorldView : IDisposable
    {
        private sealed class Galaxy { public string Type, Name; public ulong Seed; public double X, Y, Z; public float Tilt, Turn, Scale; public UploadBuffer Far; public int FarCount; }
        private readonly List<Galaxy> _galaxies = new List<Galaxy>();
        private readonly Gpu _gpu; private readonly BillboardDrawer _dots; private readonly ContentLibrary _content;
        private readonly List<StarKind> _kinds;
        private static readonly int Stride = Marshal.SizeOf(typeof(StarVertex));
        private const int FarStars = 2500, NearStars = 150000;
        private const double Spread = 120;                 // the universe patch, in galaxy units (one unit is 50,000 light-years)

        // the camera: where it is (universe units, double) and which way it looks
        private double _cx, _cy, _cz, _yaw = Math.PI + 0.4, _pitch = -0.35;
        // the galaxy whose own stars are being shown (or -1), and those stars in its own frame
        private int _near = -1; private StarVertex[] _nearLocal, _nearDrawn; private UploadBuffer _nearBuf; private float _nearFade = -1;
        private int[] _gridHead, _gridNext; private const int GridN = 64;     // a grid over the near galaxy's stars, to find the nearest one fast

        public WorldView(Gpu gpu, BillboardPipeline pipe, ContentLibrary content, ulong seed)
        {
            _gpu = gpu; _dots = new BillboardDrawer(gpu.CommandList, pipe); _content = content;
            var types = new List<string>(); var names = new List<string>();
            foreach (var e in content.OfKind("galaxies.dnat", "galaxy")) { types.Add(e.Id); names.Add(e.Name); }
            if (types.Count == 0) { types.Add("spiral"); names.Add("Spiral galaxy"); }
            var rng = new Rng(seed ^ 0x6A1AUL);
            _kinds = GalaxyGen.StarKinds(content);
            for (int i = 0; i < 36; i++)
            {
                int t = i == 0 ? Math.Max(0, types.IndexOf("spiral")) : rng.Below(types.Count);
                var g = new Galaxy { Type = types[t], Name = names[t], Seed = seed + (ulong)i * 104729UL,
                    X = i == 0 ? 0 : rng.Range(-1, 1) * Spread, Y = i == 0 ? 0 : rng.Range(-0.35f, 0.35f) * Spread, Z = i == 0 ? 0 : rng.Range(-1, 1) * Spread,
                    Tilt = rng.Range(-1.2f, 1.2f), Turn = rng.Range(0, 6.28f), Scale = types[t].StartsWith("dwarf") ? 0.35f : types[t].StartsWith("giant") ? 1.6f : rng.Range(0.8f, 1.2f) };
                // the far look: a few thousand stars in the galaxy's own frame (placed by its tilt, turn and size when drawn)
                var stars = GalaxyGen.Generate(g.Type, g.Seed, FarStars, _kinds);
                g.Far = new UploadBuffer(gpu.Device, (long)stars.Length * Stride); g.Far.Write(stars, stars.Length); g.FarCount = stars.Length;
                _galaxies.Add(g);
            }
            // start out among the galaxies, looking at the first one
            _cx = 30; _cy = 14; _cz = 22;
            LookAt(0, 0, 0);
        }

        private void LookAt(double x, double y, double z)
        {
            double dx = x - _cx, dy = y - _cy, dz = z - _cz, l = Math.Sqrt(dx * dx + dy * dy + dz * dz);
            _yaw = Math.Atan2(dz, dx); _pitch = Math.Asin(dy / l);
        }

        private Vec3 Look { get { return new Vec3((float)(Math.Cos(_yaw) * Math.Cos(_pitch)), (float)Math.Sin(_pitch), (float)(Math.Sin(_yaw) * Math.Cos(_pitch))); } }

        // a galaxy's own frame placed in the universe: its size, tilt and turn (as the universe always placed them)
        private static void ToUniverse(Galaxy g, double x, double y, double z, out double ux, out double uy, out double uz)
        {
            x *= g.Scale; y *= g.Scale; z *= g.Scale;
            double ct = Math.Cos(g.Tilt), st = Math.Sin(g.Tilt), cu = Math.Cos(g.Turn), su = Math.Sin(g.Turn);
            double y2 = y * ct - z * st, z2 = y * st + z * ct;
            ux = x * cu - z2 * su + g.X; uy = y2 + g.Y; uz = x * su + z2 * cu + g.Z;
        }

        // the same placing as a matrix, relative to the camera (the translation is the galaxy's centre minus the camera, in double first)
        private Mat4 Placement(Galaxy g)
        {
            float ct = (float)Math.Cos(g.Tilt), st = (float)Math.Sin(g.Tilt), cu = (float)Math.Cos(g.Turn), su = (float)Math.Sin(g.Turn), s = g.Scale;
            var m = Mat4.Identity;
            // rows: where the galaxy's x, y and z axes go (row vectors, as the rest of the renderer uses)
            // worked from ToUniverse: x' = x·cu − (y·st + z·ct)·su, y' = y·ct − z·st, z' = x·su + (y·st + z·ct)·cu
            m.M11 = cu * s; m.M12 = 0; m.M13 = su * s;
            m.M21 = -st * su * s; m.M22 = ct * s; m.M23 = st * cu * s;
            m.M31 = -ct * su * s; m.M32 = -st * s; m.M33 = ct * cu * s;
            m.M41 = (float)(g.X - _cx); m.M42 = (float)(g.Y - _cy); m.M43 = (float)(g.Z - _cz);
            return m;
        }

        // how close the nearest thing is (universe units): a star of the near galaxy, or the edge of the nearest galaxy
        private double NearestDistance()
        {
            double best = double.MaxValue;
            foreach (var g in _galaxies)
            {
                double dx = g.X - _cx, dy = g.Y - _cy, dz = g.Z - _cz, d = Math.Sqrt(dx * dx + dy * dy + dz * dz) - g.Scale * 1.0;
                best = Math.Min(best, Math.Max(d, g.Scale * 0.02));
            }
            if (_near >= 0 && _nearLocal != null)
            {
                // the camera in the near galaxy's own frame, then the stars in the grid cells around it
                double lx, ly, lz; ToGalaxy(_galaxies[_near], _cx, _cy, _cz, out lx, out ly, out lz);
                int gx = Cell(lx), gy = Cell(ly), gz = Cell(lz);
                double bestL = double.MaxValue;
                for (int ox = -1; ox <= 1; ox++) for (int oy = -1; oy <= 1; oy++) for (int oz = -1; oz <= 1; oz++)
                {
                    int x = gx + ox, y = gy + oy, z = gz + oz;
                    if (x < 0 || y < 0 || z < 0 || x >= GridN || y >= GridN || z >= GridN) continue;
                    for (int k = _gridHead[(x * GridN + y) * GridN + z]; k >= 0; k = _gridNext[k])
                    {
                        var s = _nearLocal[k]; double dx = s.X - lx, dy = s.Y - ly, dz = s.Z - lz;
                        bestL = Math.Min(bestL, dx * dx + dy * dy + dz * dz);
                    }
                }
                if (bestL < double.MaxValue) best = Math.Min(best, Math.Sqrt(bestL) * _galaxies[_near].Scale);
            }
            return best;
        }

        private static int Cell(double v) { return (int)Math.Floor((v + 1.6) / 3.2 * GridN); }

        private static void ToGalaxy(Galaxy g, double ux, double uy, double uz, out double x, out double y, out double z)
        {
            double px = ux - g.X, py = uy - g.Y, pz = uz - g.Z;
            double ct = Math.Cos(g.Tilt), st = Math.Sin(g.Tilt), cu = Math.Cos(g.Turn), su = Math.Sin(g.Turn);
            double x1 = px * cu + pz * su, z1 = -px * su + pz * cu;                  // undo the turn
            double y2 = py * ct + z1 * st, z2 = -py * st + z1 * ct;                  // undo the tilt
            x = x1 / g.Scale; y = y2 / g.Scale; z = z2 / g.Scale;
        }

        public bool Update(Input input, double dt, int w, int h)
        {
            if (input.WasPressed(Win32.VK_ESCAPE)) return false;
            if (input.LeftDown) { _yaw += input.MouseDX * 0.004; _pitch = Math.Max(-1.55, Math.Min(1.55, _pitch - input.MouseDY * 0.004)); }
            double reach = NearestDistance();
            if (input.Wheel != 0)
            {
                // fly along the line under the mouse, by a share of the distance to the nearest thing: slower as it comes close, never ending
                var dir = MouseRay(input.MouseX, input.MouseY, w, h);
                double step = reach * (1 - Math.Pow(0.87, Math.Abs(input.Wheel))) * Math.Sign(input.Wheel);
                if (input.Wheel < 0) step = -Math.Max(reach, 0.01) * (Math.Pow(1 / 0.87, -input.Wheel) - 1);
                _cx += dir.X * step; _cy += dir.Y * step; _cz += dir.Z * step;
            }
            ChooseNear();
            return true;
        }

        private Vec3 MouseRay(int mx, int my, int w, int h)
        {
            double nx = mx / (double)Math.Max(1, w) * 2 - 1, ny = 1 - my / (double)Math.Max(1, h) * 2, t = Math.Tan(0.45), aspect = w / (double)Math.Max(1, h);
            var f = Look; var right = Vec3.Cross(f, new Vec3(0, 1, 0)).Normalized; var up = Vec3.Cross(right, f).Normalized;
            return (f + right * (float)(nx * t * aspect) + up * (float)(ny * t)).Normalized;
        }

        // the galaxy whose real stars are shown: the nearest one, once the camera is within a few of its sizes
        private void ChooseNear()
        {
            int best = -1; double bd = double.MaxValue;
            for (int i = 0; i < _galaxies.Count; i++)
            {
                var g = _galaxies[i]; double dx = g.X - _cx, dy = g.Y - _cy, dz = g.Z - _cz, d = Math.Sqrt(dx * dx + dy * dy + dz * dz) / g.Scale;
                if (d < bd) { bd = d; best = i; }
            }
            if (bd > 8) best = -1;
            if (best != _near)
            {
                _near = best; _nearFade = -1;
                if (_nearBuf != null) { _nearBuf.Dispose(); _nearBuf = null; }
                _nearLocal = null;
                if (best >= 0)
                {
                    var g = _galaxies[best];
                    _nearLocal = GalaxyGen.Generate(g.Type, g.Seed, NearStars, _kinds);
                    _nearDrawn = new StarVertex[_nearLocal.Length];
                    _nearBuf = new UploadBuffer(_gpu.Device, (long)_nearLocal.Length * Stride);
                    // the grid used to find the nearest star
                    _gridHead = new int[GridN * GridN * GridN]; for (int k = 0; k < _gridHead.Length; k++) _gridHead[k] = -1;
                    _gridNext = new int[_nearLocal.Length];
                    for (int k = 0; k < _nearLocal.Length; k++)
                    {
                        int x = Math.Max(0, Math.Min(GridN - 1, Cell(_nearLocal[k].X))), y = Math.Max(0, Math.Min(GridN - 1, Cell(_nearLocal[k].Y))), z = Math.Max(0, Math.Min(GridN - 1, Cell(_nearLocal[k].Z)));
                        int c = (x * GridN + y) * GridN + z; _gridNext[k] = _gridHead[c]; _gridHead[c] = k;
                    }
                }
            }
        }

        // how much of the near galaxy is its real stars (1) rather than its far glow (0), from how close the camera is
        private double NearShare()
        {
            if (_near < 0) return 0;
            var g = _galaxies[_near]; double dx = g.X - _cx, dy = g.Y - _cy, dz = g.Z - _cz, d = Math.Sqrt(dx * dx + dy * dy + dz * dz) / g.Scale;
            double t = (8 - d) / 4; return Math.Max(0, Math.Min(1, t));      // fades in between 8 and 4 galaxy sizes away
        }

        public void Draw(int w, int h, double time)
        {
            var look = Look;
            Mat4 view = Mat4.LookAt(new Vec3(0, 0, 0), look, new Vec3(0, 1, 0));
            double reach = Math.Max(1e-12, NearestDistance());
            Mat4 proj = Mat4.Perspective(0.9f, w / (float)Math.Max(1, h), (float)(reach * 1e-3), (float)(Spread * 6));
            Mat4 vp = view * proj;
            var right = new Vec3(view.M11, view.M21, view.M31); var up = new Vec3(view.M12, view.M22, view.M32);
            double share = NearShare();
            for (int i = 0; i < _galaxies.Count; i++)
            {
                var g = _galaxies[i];
                if (i == _near && share >= 0.999) continue;                    // its real stars have taken over
                // the far glow fades out as the real stars fade in (dimmer points, fewer of them drawn)
                int count = i == _near ? (int)(g.FarCount * (1 - share)) : g.FarCount;
                DrawPoints(g.Far, count, Placement(g) * vp, right, up, 0.006f * g.Scale, 0.0016f, w, h, time);
            }
            if (_near >= 0 && share > 0.001)
            {
                // the near galaxy's real stars, faded in by brightness as the camera closes in
                if (Math.Abs(share - _nearFade) > 0.02)
                {
                    _nearFade = (float)share;
                    for (int k = 0; k < _nearLocal.Length; k++) { var v = _nearLocal[k]; v.R *= _nearFade; v.G *= _nearFade; v.B *= _nearFade; _nearDrawn[k] = v; }
                    _nearBuf.Write(_nearDrawn, _nearDrawn.Length);
                }
                DrawPoints(_nearBuf, _nearDrawn.Length, Placement(_galaxies[_near]) * vp, right, up, 0f, 0.0028f, w, h, time);
            }
        }

        private void DrawPoints(UploadBuffer buf, int count, Mat4 vp, Vec3 right, Vec3 up, float sizeScale, float minSize, int w, int h, double time)
        {
            if (buf == null || count <= 0) return;
            var c = new float[BillboardPipeline.ConstantCount];
            c[0] = vp.M11; c[1] = vp.M12; c[2] = vp.M13; c[3] = vp.M14; c[4] = vp.M21; c[5] = vp.M22; c[6] = vp.M23; c[7] = vp.M24;
            c[8] = vp.M31; c[9] = vp.M32; c[10] = vp.M33; c[11] = vp.M34; c[12] = vp.M41; c[13] = vp.M42; c[14] = vp.M43; c[15] = vp.M44;
            c[16] = right.X; c[17] = right.Y; c[18] = right.Z; c[20] = up.X; c[21] = up.Y; c[22] = up.Z;
            c[24] = (float)time; c[25] = sizeScale; c[26] = 2; c[27] = 4; c[28] = w; c[29] = h; c[30] = minSize;
            _dots.Draw(buf, count, c, 0, 0, w, h);
        }

        public string Title
        {
            get
            {
                string where = _near >= 0 ? (NearShare() > 0.5 ? "among the stars of " : "near ") + _galaxies[_near].Name : "among the galaxies";
                return "DNA-AND · one world · " + where + " · nearest thing " + Units.Describe(NearestDistance()) + " away · wheel: fly in or out toward the mouse · drag: look around · Esc: close";
            }
        }

        public void Dispose()
        {
            foreach (var g in _galaxies) g.Far.Dispose();
            if (_nearBuf != null) _nearBuf.Dispose();
        }
    }
}
