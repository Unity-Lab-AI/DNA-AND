// DNA-AND · shell · deep zoom: one galaxy full-window, zoomable from the whole galaxy down to the core's
// supermassive black hole, its clusters and nebulas, and any star's full system. Everything is generated live.
// Precision: positions are kept relative to the current focus (a moving origin), so tiny things stay exact.
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DnaAnd.Core;
using DnaAnd.Gen;
using DnaAnd.Platform;
using DnaAnd.Render;

namespace DnaAnd.Shell
{
    internal sealed class DeepView : IDisposable
    {
        private const int GalaxyStars = 150000;
        private readonly Gpu _gpu; private readonly BillboardDrawer _drawer; private readonly ContentLibrary _content;
        private readonly string _name; private readonly ulong _seed;
        private readonly StarVertex[] _stars, _structures; private readonly int[] _kinds;
        private readonly StarVertex[] _starsRel, _structRel;
        private readonly List<StarKind> _kindList;
        private readonly SpaceGen.BlackHole _bh;
        private readonly int _clusters, _nebulas;
        private UploadBuffer _starBuf, _structBuf, _diskBuf, _jetBuf, _coreBuf, _sysStatic, _sysBodies;
        private SpaceGen.StarSystem _system; private int _focusStar = -1;
        private int _focusBody = -1;               // in a solar system: the planet or moon the camera is centred on (-1 = the star)
        /// <summary>Set when the camera has come close enough to a planet to fly down to it: the body's index (else -1).</summary>
        public int HandOff = -1;
        /// <summary>Set when the player zooms out past the whole galaxy: back to the mix of galaxies.</summary>
        public bool Ascend;
        private int _pendingStar = -1;              // the journey: the star being flown toward, opened once close enough

        /// <summary>The journey: aim at a Sun-like star out in the disc, so rolling the wheel in flies to it.</summary>
        public void AimForJourney()
        {
            int best = -1; double bestScore = 1e9;
            for (int i = 0; i < _stars.Length; i += 7)
            {
                double r = Math.Sqrt(_stars[i].X * _stars[i].X + _stars[i].Z * _stars[i].Z);
                string k = _kindList[Math.Max(0, _kinds[i])].Id;
                double score = Math.Abs(r - 0.55) + (k == "g-type" ? 0 : 1);
                if (score < bestScore) { bestScore = score; best = i; }
            }
            if (best < 0) return;
            _pendingStar = best;
            SetFocus(_stars[best].X, _stars[best].Y, _stars[best].Z);
            _dist = 3.5;
        }

        private void OpenStar(int best)
        {
            _focusStar = best; _pendingStar = -1;
            StarKind kind = _kindList[Math.Max(0, _kinds[best])];
            _system = SpaceGen.System(_content, kind, _seed * 31UL + (ulong)best); _focusBody = -1;
            SetFocus(_stars[best].X, _stars[best].Y, _stars[best].Z);
            if (_sysStatic != null) _sysStatic.Dispose();
            _sysStatic = Upload(_system.Static);
            if (_sysBodies != null) _sysBodies.Dispose();
            _bodyVerts = new StarVertex[(_system.Bodies.Count + 1) * (1 + TrailPoints)];
            _staticShifted = new StarVertex[_system.Static.Length];
            _sysBodies = new UploadBuffer(_gpu.Device, (long)_bodyVerts.Length * Stride);
            // and pick its Earth-like world (or its first planet), so rolling on in flies down to it
            int pick = 0;
            for (int i = 0; i < _system.Bodies.Count; i++) if (_system.Bodies[i].Parent < 0 && _system.Bodies[i].Kind == "Temperate world") { pick = i; break; }
            PickBody(pick);
            _dist = Math.Max(_dist, _system.Bodies[pick].RadiusUnits * 4000);
        }
        public SpaceGen.StarSystem System { get { return _system; } }
        public ulong SystemSeed { get { return _seed * 31UL + (ulong)Math.Max(0, _focusStar); } }
        private double _fx, _fy, _fz;          // focus position in galaxy units (double)
        private float _yaw = 0.6f, _pitch = 0.9f; private double _dist = 2.4;
        private StarVertex[] _bodyVerts;
        private double _simYears;                 // simulation time in years
        private double _speed = 1; private bool _paused;
        private static readonly double[] Speeds = { 1, 10, 100, 1000, 100000 };
        private const double SecondsPerYearAt1x = 20.0;   // at 1x, one year takes 20 s
        private const double MaxVisualTurnPerSecond = 0.05; // radians: nothing on screen turns faster than this
        private static readonly int Stride = Marshal.SizeOf(typeof(StarVertex));

        public DeepView(Gpu gpu, BillboardPipeline pipe, ContentLibrary content, string type, string name, ulong seed)
        {
            _gpu = gpu; _drawer = new BillboardDrawer(gpu.CommandList, pipe); _content = content; _name = name; _seed = seed;
            _kindList = GalaxyGen.StarKinds(content);
            _kinds = new int[GalaxyStars];
            _stars = GalaxyGen.Generate(type, seed, GalaxyStars, _kindList, _kinds);
            _structures = SpaceGen.Structures(type, seed, _kindList, out _clusters, out _nebulas);
            _starsRel = new StarVertex[_stars.Length]; _structRel = new StarVertex[_structures.Length];
            _starBuf = new UploadBuffer(gpu.Device, (long)_stars.Length * Stride);
            _structBuf = new UploadBuffer(gpu.Device, (long)Math.Max(1, _structures.Length) * Stride);
            _bh = SpaceGen.CoreBlackHole(content, seed);
            _diskBuf = Upload(_bh.Disk); _jetBuf = Upload(_bh.Jets); _coreBuf = Upload(_bh.Glow);
            SetFocus(0, 0, 0);
        }

        private UploadBuffer Upload(StarVertex[] v)
        {
            var b = new UploadBuffer(_gpu.Device, (long)Math.Max(1, v.Length) * Stride);
            if (v.Length > 0) b.Write(v, v.Length);
            return b;
        }

        private void SetFocus(double x, double y, double z)
        {
            _fx = x; _fy = y; _fz = z;
            Relative(_stars, _starsRel); _starBuf.Write(_starsRel, _starsRel.Length);
            if (_structures.Length > 0) { Relative(_structures, _structRel); _structBuf.Write(_structRel, _structRel.Length); }
        }

        private void Relative(StarVertex[] src, StarVertex[] dst)
        {
            for (int i = 0; i < src.Length; i++)
            {
                StarVertex v = src[i];
                v.X = (float)(src[i].X - _fx); v.Y = (float)(src[i].Y - _fy); v.Z = (float)(src[i].Z - _fz);
                dst[i] = v;
            }
        }

        /// <summary>Returns false when the player wants to go back to the sample grid.</summary>
        public bool Update(Input input, double dt, int w, int h)
        {
            if (input.WasPressed(0x08) || input.WasPressed(0x1B)) return false;                    // Backspace or Esc: back
            // in a solar system: click a planet or moon (or Tab for the next one) to centre on it; the wheel then flies to it
            if (_system != null && input.WasPressed(0x09)) PickBody((_focusBody + 2) % (_system.Bodies.Count + 1) - 1);
            if (_system != null && input.LeftClicked) ClickBody(input.MouseX, input.MouseY, w, h);
            if (input.LeftDown) { _yaw += input.MouseDX * 0.006f; _pitch = Math.Max(-1.5f, Math.Min(1.5f, _pitch + input.MouseDY * 0.006f)); }
            if (input.Wheel < 0 && _focusStar < 0 && _dist >= 3.99) { Ascend = true; return false; }     // out past the galaxy: back to the universe
            // zooming out lets go step by step: the planet, then the solar system, then the galaxy
            if (input.Wheel < 0 && _system != null)
            {
                double outer = 0; foreach (var b in _system.Bodies) if (b.Parent < 0) outer = Math.Max(outer, b.OrbitUnits);
                if (_focusBody >= 0 && _dist > outer * 1.5) { _focusBody = -1; }
                else if (_focusBody < 0 && _dist > outer * 40) { GoCore(); _dist = Math.Max(_dist, outer * 40); return true; }
            }
            if (_pendingStar >= 0 && input.Wheel < 0 && _dist > 0.5) _pendingStar = -1;      // backing away from the target star: let it go
            if (input.Wheel != 0) _dist *= Math.Pow(0.82, input.Wheel);
            if (_pendingStar >= 0 && _dist < 2e-6) OpenStar(_pendingStar);                 // close enough to the star: open its solar system
            double minDist = _focusStar >= 0 ? _system.StarRadius * 1.5 : _pendingStar >= 0 ? 1e-6 : _bh.HorizonUnits * 3;
            if (_focusBody >= 0) minDist = _system.Bodies[_focusBody].RadiusUnits * 1.05;
            _dist = Math.Max(minDist, Math.Min(4.0, _dist));
            // close enough to a planet: hand over to the planet view, at the same distance
            if (_focusBody >= 0 && input.Wheel > 0 && _dist < _system.Bodies[_focusBody].RadiusUnits * 6) HandOff = _focusBody;
            if (input.WasPressed(0x20)) _paused = !_paused;                          // Space: pause
            for (int k = 0; k < 5; k++) if (input.WasPressed(0x31 + k)) { _speed = Speeds[k]; _paused = false; } // 1-5: speeds
            if (!_paused) _simYears += dt / SecondsPerYearAt1x * _speed;
            if (input.WasPressed(0x48)) GoCore();                                    // H: the core black hole
            if (input.WasPressed(0x0D)) VisitCenterStar(w, h);                       // Enter: visit the star in the middle
            return true;
        }

        public double BodyRadiusUnits(int i) { return _system.Bodies[i].RadiusUnits; }
        public double Distance { get { return _dist; } }

        /// <summary>Back from the planet view: hover just above the planet, where the planet view let go.</summary>
        public void ReturnFromPlanet() { HandOff = -1; if (_focusBody >= 0) _dist = _system.Bodies[_focusBody].RadiusUnits * 6.5; }

        /// <summary>Test runs: go to the middle star's system and centre on its first planet.</summary>
        public void TestJourney(int w, int h)
        {
            TestVisit(w, h);
            if (_system == null) return;
            int pick = 0;
            for (int i = 0; i < _system.Bodies.Count; i++) if (_system.Bodies[i].Parent < 0 && _system.Bodies[i].Kind == "Temperate world") { pick = i; break; }
            PickBody(pick);
        }

        private void PickBody(int i)
        {
            if (_system == null) return;
            _focusBody = i < 0 || i >= _system.Bodies.Count ? -1 : i;
            double outer = 0; foreach (var b in _system.Bodies) if (b.Parent < 0) outer = Math.Max(outer, b.OrbitUnits);
            _dist = _focusBody < 0 ? outer * 2.2 : _system.Bodies[_focusBody].RadiusUnits * 60;
        }

        private void ClickBody(int mx, int my, int w, int h)
        {
            Mat4 vp = ViewProj(w, h);
            float x = mx / (float)Math.Max(1, w) * 2 - 1, y = 1 - my / (float)Math.Max(1, h) * 2;
            int best = -2; float bd = 0.05f;
            for (int i = 0; i < _bodyCount; i++)
            {
                var v = _bodyVerts[i];
                float cx = v.X * vp.M11 + v.Y * vp.M21 + v.Z * vp.M31 + vp.M41, cy = v.X * vp.M12 + v.Y * vp.M22 + v.Z * vp.M32 + vp.M42, cw = v.X * vp.M14 + v.Y * vp.M24 + v.Z * vp.M34 + vp.M44;
                if (cw <= 0) continue;
                float d = (float)Math.Sqrt((cx / cw - x) * (cx / cw - x) + (cy / cw - y) * (cy / cw - y));
                if (d < bd) { bd = d; best = i - 1; }       // vertex 0 is the star
            }
            if (best > -2) PickBody(best);
        }

        public void TestCore() { GoCore(); _dist = _bh.HorizonUnits * 60; }
        public void TestVisit(int w, int h) { _pitch = 1.45f; _dist = 0.6; VisitCenterStar(w, h); if (_focusStar < 0) { _dist = 0.05; VisitCenterStar(w, h); } _pitch = 0.7f; }
        public void TestZoom(double d) { _dist = d; }
        public void TestSpeed(int index) { _speed = Speeds[Math.Max(0, Math.Min(4, index))]; }

        private void GoCore()
        {
            _focusStar = -1; _system = null;
            if (_sysStatic != null) { _sysStatic.Dispose(); _sysStatic = null; }
            if (_sysBodies != null) { _sysBodies.Dispose(); _sysBodies = null; }
            SetFocus(0, 0, 0);
            _dist = Math.Max(_bh.HorizonUnits * 40, Math.Min(_dist, 0.05));
        }

        private void VisitCenterStar(int w, int h)
        {
            Mat4 vp = ViewProj(w, h);
            int best = -1; double bestD = 40 * 40; // within 40 pixels of the middle
            for (int i = 0; i < _starsRel.Length; i++)
            {
                StarVertex s = _starsRel[i];
                float cx = s.X * vp.M11 + s.Y * vp.M21 + s.Z * vp.M31 + vp.M41;
                float cy = s.X * vp.M12 + s.Y * vp.M22 + s.Z * vp.M32 + vp.M42;
                float cw = s.X * vp.M14 + s.Y * vp.M24 + s.Z * vp.M34 + vp.M44;
                if (cw <= 0) continue;
                double px = cx / cw * w * 0.5, py = cy / cw * h * 0.5, d = px * px + py * py;
                if (d < bestD) { bestD = d; best = i; }
            }
            if (best < 0) return;
            _focusStar = best;
            StarKind kind = _kindList[Math.Max(0, _kinds[best])];
            _system = SpaceGen.System(_content, kind, _seed * 31UL + (ulong)best); _focusBody = -1;
            SetFocus(_stars[best].X, _stars[best].Y, _stars[best].Z);
            if (_sysStatic != null) _sysStatic.Dispose();
            _sysStatic = Upload(_system.Static);
            if (_sysBodies != null) _sysBodies.Dispose();
            _bodyVerts = new StarVertex[(_system.Bodies.Count + 1) * (1 + TrailPoints)];
            _staticShifted = new StarVertex[_system.Static.Length];
            _sysBodies = new UploadBuffer(_gpu.Device, (long)_bodyVerts.Length * Stride);
            double outer = 0; foreach (var b in _system.Bodies) if (b.Parent < 0) outer = Math.Max(outer, b.OrbitUnits);
            _dist = outer * 2.2;
        }

        private Mat4 ViewProj(int w, int h)
        {
            var eye = new Vec3((float)(Math.Cos(_yaw) * Math.Cos(_pitch) * _dist), (float)(Math.Sin(_pitch) * _dist), (float)(Math.Sin(_yaw) * Math.Cos(_pitch) * _dist));
            Mat4 view = Mat4.LookAt(eye, new Vec3(0, 0, 0), new Vec3(0, 1, 0));
            float near = (float)(_dist * 1e-3), far = (float)Math.Max(_dist * 1e5, 12);
            return view * Mat4.Perspective(0.9f, w / (float)Math.Max(1, h), near, far);
        }

        public void Draw(int w, int h, double time)
        {
            var eye = new Vec3((float)(Math.Cos(_yaw) * Math.Cos(_pitch) * _dist), (float)(Math.Sin(_pitch) * _dist), (float)(Math.Sin(_yaw) * Math.Cos(_pitch) * _dist));
            Mat4 view = Mat4.LookAt(eye, new Vec3(0, 0, 0), new Vec3(0, 1, 0));
            Mat4 vp = ViewProj(w, h);
            var right = new Vec3(view.M11, view.M21, view.M31); var up = new Vec3(view.M12, view.M22, view.M32);

            Draw(_starBuf, _stars.Length, vp, right, up, 0f, 0.0028f, 2f, w, h, time);            // galaxy stars: Populous-like (the chosen galaxy style)
            if (_structures.Length > 0) Draw(_structBuf, _structures.Length, vp, right, up, 1f, 0.0018f, 0f, w, h, time); // clusters, nebulas
            if (_focusStar < 0)
            {
                Draw(_coreBuf, _bh.Glow.Length, vp, right, up, 0f, 0.0016f, 0f, w, h, time);
                Mat4 spin = Mat4.RotationY((float)(time * 0.3)) * vp;                             // the disk turns, gently (calm motion)
                Draw(_diskBuf, _bh.Disk.Length, spin, right, up, 1f, 0.0012f, 0f, w, h, time);
                if (_bh.Jets.Length > 0) Draw(_jetBuf, _bh.Jets.Length, vp, right, up, 1f, 0.0012f, 0f, w, h, time);
            }
            else
            {
                UpdateBodies(time);
                Draw(_sysStatic, _system.Static.Length, vp, right, up, 0f, 0.002f, 1f, w, h, time); // orbits, belts, Oort cloud
                Draw(_sysBodies, _bodyCount, vp, right, up, 1f, 0.006f, 1f, w, h, time);    // the star, planets and moons
            }
        }

        private const int TrailPoints = 72;
        private StarVertex[] _staticShifted = new StarVertex[0];
        private int _bodyCount;

        private void UpdateBodies(double time)
        {
            // everything is placed relative to the planet in view (worked out in double precision), so it never shakes up close
            double fx, fy, fz;
            _bodyCount = SpaceGen.BodyVerts(_system, _simYears, _paused ? 0 : _speed / SecondsPerYearAt1x, MaxVisualTurnPerSecond, _bodyVerts, _focusBody, 1.0, out fx, out fy, out fz);
            _sysBodies.Write(_bodyVerts, _bodyCount);
            var st = _system.Static;
            for (int i = 0; i < st.Length; i++) { var v = st[i]; v.X = (float)(st[i].X - fx); v.Y = (float)(st[i].Y - fy); v.Z = (float)(st[i].Z - fz); _staticShifted[i] = v; }
            if (st.Length > 0) _sysStatic.Write(_staticShifted, st.Length);
        }

        private void Draw(UploadBuffer buf, int count, Mat4 vp, Vec3 right, Vec3 up, float sizeScale, float minSize, float style, int w, int h, double time)
        {
            if (buf == null || count <= 0) return;
            var c = new float[BillboardPipeline.ConstantCount];
            c[0] = vp.M11; c[1] = vp.M12; c[2] = vp.M13; c[3] = vp.M14; c[4] = vp.M21; c[5] = vp.M22; c[6] = vp.M23; c[7] = vp.M24;
            c[8] = vp.M31; c[9] = vp.M32; c[10] = vp.M33; c[11] = vp.M34; c[12] = vp.M41; c[13] = vp.M42; c[14] = vp.M43; c[15] = vp.M44;
            c[16] = right.X; c[17] = right.Y; c[18] = right.Z; c[20] = up.X; c[21] = up.Y; c[22] = up.Z;
            c[24] = (float)time; c[25] = sizeScale; c[26] = style; c[27] = 4f; c[28] = w; c[29] = h; c[30] = minSize;
            _drawer.Draw(buf, count, c, 0, 0, w, h);
        }

        public string Title
        {
            get
            {
                string focus = _focusStar < 0
                    ? string.Format("focus: the core's supermassive black hole, {0:0.0e0} Sun masses{1} · {2} star clusters, {3} nebulas",
                        _bh.MassSol, _bh.Active ? ", feeding (jets on)" : ", quiet", _clusters, _nebulas)
                    : _focusBody >= 0 ? "focus: " + _system.Bodies[_focusBody].Kind + " (keep rolling the wheel in to fly down to it)"
                    : string.Format("focus: {0} star · {1} planets · frost line {2:0.0} au · habitable zone {3:0.00} au",
                        _system.StarName, _system.Planets, _system.FrostLineAu, _system.HabitableAu);
                return string.Format("DNA-AND deep zoom · {0} · view {1} across · {2} · time {3} · wheel zoom, drag rotate, Enter visit middle star, click a planet or Tab to pick it then wheel in to fly down to it, H core, 1-5 speed, Space pause, Esc = back",
                    _name, Units.Describe(_dist * 1.0), focus, _paused ? "paused" : _speed.ToString("N0") + "x");
            }
        }

        public void Dispose()
        {
            foreach (var b in new[] { _starBuf, _structBuf, _diskBuf, _jetBuf, _coreBuf, _sysStatic, _sysBodies }) if (b != null) b.Dispose();
        }
    }
}
