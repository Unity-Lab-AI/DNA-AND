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
        /// <summary>For drawing the planet being approached as the real 3D planet (set by the journey).</summary>
        public MeshPipes Meshes;
        /// <summary>The 3D planet being approached (made when a planet is picked; the planet view carries on with this same one).</summary>
        public PlanetTerrain FocusTerrain, FocusFields;
        private StarSurface _starSurface; private StarVertex[] _starCorona, _starSurfVerts; private UploadBuffer _starCoronaBuf, _starSurfBuf, _shineBuf;
        private double _glideFromX, _glideFromY, _glideFromZ; private bool _gliding; private double _glideStartDist = 3.5;
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
            _glideFromX = 0; _glideFromY = 0; _glideFromZ = 0; _gliding = true; _glideStartDist = 3.5;   // start on the whole galaxy, glide toward the star as you zoom
            _dist = 3.5;
        }

        /// <summary>In the galaxy: the star nearest the mouse becomes the one you fly to; the view glides toward it as you zoom in.</summary>
        private void ClickStar(int mx, int my, int w, int h)
        {
            Mat4 vp = ViewProj(w, h);
            float x = mx / (float)Math.Max(1, w) * 2 - 1, y = 1 - my / (float)Math.Max(1, h) * 2;
            int best = -1; float bd = 0.03f;
            for (int i = 0; i < _starsRel.Length; i++)
            {
                StarVertex v = _starsRel[i];
                float cx = v.X * vp.M11 + v.Y * vp.M21 + v.Z * vp.M31 + vp.M41, cy = v.X * vp.M12 + v.Y * vp.M22 + v.Z * vp.M32 + vp.M42, cw = v.X * vp.M14 + v.Y * vp.M24 + v.Z * vp.M34 + vp.M44;
                if (cw <= 0) continue;
                float d = Math.Abs(cx / cw - x) + Math.Abs(cy / cw - y);
                if (d < bd) { bd = d; best = i; }
            }
            if (best < 0) return;
            _pendingStar = best; _glideFromX = _fx; _glideFromY = _fy; _glideFromZ = _fz; _gliding = true;
            _glideStartDist = _dist;
        }

        private void OpenStar(int best)
        {
            _focusStar = best; _pendingStar = -1;
            StarKind kind = _kindList[Math.Max(0, _kinds[best])];
            string sname; _starCorona = StarGen.Build(_content, kind.Id, _seed * 77UL + (ulong)best, out sname, out _starSurface);
            _starSurfVerts = new StarVertex[_starSurface.Normals.Length];
            if (_starCoronaBuf != null) _starCoronaBuf.Dispose(); if (_starSurfBuf != null) _starSurfBuf.Dispose();
            _starCoronaBuf = Upload(_starCorona); _starSurfBuf = new UploadBuffer(_gpu.Device, (long)_starSurfVerts.Length * Stride);
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
            if (_system == null && input.LeftClicked) ClickStar(input.MouseX, input.MouseY, w, h);   // in the galaxy: any star can be picked
            if (input.LeftDown) { _yaw += input.MouseDX * 0.006f; _pitch = Math.Max(-1.5f, Math.Min(1.5f, _pitch + input.MouseDY * 0.006f)); }
            if (input.Wheel < 0 && _focusStar < 0 && _dist >= 3.99) { Ascend = true; return false; }     // out past the galaxy: back to the universe
            // zooming out lets go step by step: the planet, then the solar system, then the galaxy
            if (input.Wheel < 0 && _system != null)
            {
                double outer = 0; foreach (var b in _system.Bodies) if (b.Parent < 0) outer = Math.Max(outer, b.OrbitUnits);
                if (_focusBody >= 0 && _dist > outer * 1.5) { _focusBody = -1; }
                else if (_focusBody < 0 && _dist > outer * 40) { GoCore(); _dist = Math.Max(_dist, outer * 40); return true; }
            }
            if (_pendingStar >= 0 && input.Wheel < 0 && _dist > Math.Max(0.5, _glideStartDist)) _pendingStar = -1;      // backing away from the target star: let it go
            if (input.Wheel != 0) _dist *= Math.Pow(0.82, input.Wheel);
            if (_pendingStar >= 0 && _gliding)
            {
                // how far the zoom has come from the whole galaxy (3.5) to near the star (0.002): the view's centre slides the same share of the way
                double t = Math.Max(0, Math.Min(1, (Math.Log(_glideStartDist) - Math.Log(Math.Max(_dist, 1e-9))) / (Math.Log(_glideStartDist) - Math.Log(Math.Min(0.002, _glideStartDist * 0.5)))));
                t = t * t * (3 - 2 * t);
                var s0 = _stars[_pendingStar];
                double nx = _glideFromX + (s0.X - _glideFromX) * t, ny = _glideFromY + (s0.Y - _glideFromY) * t, nz = _glideFromZ + (s0.Z - _glideFromZ) * t;
                double moved = Math.Abs(nx - _fx) + Math.Abs(ny - _fy) + Math.Abs(nz - _fz);
                if (moved > _dist * 0.01 || t >= 1) SetFocus(nx, ny, nz);
                if (t >= 1) _gliding = false;
            }
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
            FocusTerrain = null; FocusFields = null;
            if (_focusBody >= 0)
            {
                var body = _system.Bodies[_focusBody];
                string kind = TerrainKind(body.Kind);
                uint tseed = (uint)(SystemSeed * 977UL + (ulong)_focusBody);
                FocusTerrain = new PlanetTerrain(_gpu.Device, kind, body.Kind, tseed);
                FocusFields = new PlanetTerrain(_gpu.Device, kind, body.Kind, tseed, true);
            }
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

        public static string TerrainKind(string bodyKind)
        {
            switch (bodyKind)
            {
                case "Temperate world": return "temperate";
                case "Lava planet": return "lava";
                case "Ice world": return "snowball";
                case "Gas giant": return "gas-giant";
                case "Ice giant": return "ice-giant";
                case "Hot Jupiter": return "hot-jupiter";
                default: return "desert";
            }
        }

        /// <summary>The camera, built from its direction (always unit length) and then moved out by the distance,
        /// so it stays exact even a trillionth of a galaxy unit from a planet.</summary>
        private Mat4 OrbitView()
        {
            var dir = new Vec3((float)(Math.Cos(_yaw) * Math.Cos(_pitch)), (float)Math.Sin(_pitch), (float)(Math.Sin(_yaw) * Math.Cos(_pitch)));
            Mat4 v = Mat4.LookAt(dir, new Vec3(0, 0, 0), new Vec3(0, 1, 0));
            v.M41 = (float)(v.M41 * _dist); v.M42 = (float)(v.M42 * _dist); v.M43 = (float)(v.M43 * _dist);
            return v;
        }

        private Mat4 ViewProj(int w, int h)
        {
            var eye = new Vec3((float)(Math.Cos(_yaw) * Math.Cos(_pitch) * _dist), (float)(Math.Sin(_pitch) * _dist), (float)(Math.Sin(_yaw) * Math.Cos(_pitch) * _dist));
            Mat4 view = OrbitView();
            float near = (float)(_dist * 1e-3), far = (float)Math.Max(_dist * 1e5, 12);
            return view * Mat4.Perspective(0.9f, w / (float)Math.Max(1, h), near, far);
        }

        public void Draw(int w, int h, double time)
        {
            var eye = new Vec3((float)(Math.Cos(_yaw) * Math.Cos(_pitch) * _dist), (float)(Math.Sin(_pitch) * _dist), (float)(Math.Sin(_yaw) * Math.Cos(_pitch) * _dist));
            Mat4 view = OrbitView();
            Mat4 vp = ViewProj(w, h);
            var right = new Vec3(view.M11, view.M21, view.M31); var up = new Vec3(view.M12, view.M22, view.M32);

            // the galaxy's stars, clusters and nebulas, with their own near and far range so they stay in view however close you are to a planet
            Mat4 bgVp = view * Mat4.Perspective(0.9f, w / (float)Math.Max(1, h), (float)Math.Max(_dist * 1e-3, 1e-5), (float)Math.Max(_dist * 1e5, 12));
            Draw(_starBuf, _stars.Length, bgVp, right, up, 0f, 0.0028f, 2f, w, h, time);            // galaxy stars: Populous-like (the chosen galaxy style)
            if (_structures.Length > 0) Draw(_structBuf, _structures.Length, bgVp, right, up, 1f, 0.0018f, 0f, w, h, time); // clusters, nebulas
            _gpu.ClearDepth();
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
                Draw(_sysBodies, _bodyCount, vp, right, up, 1f, 0.004f, 0f, w, h, time);    // the planets and moons: glows in their own colours
                DrawStar(vp, view, eye, right, up, w, h, time);
                DrawFocusPlanet(eye, w, h, time);
            }
        }

        // the system's star: a wide shine in its own colour from afar, and close up its moving plasma and corona (the set 3 look)
        private void DrawStar(Mat4 vp, Mat4 view, Vec3 eye, Vec3 right, Vec3 up, int w, int h, double time)
        {
            if (_starSurface == null) return;
            var sp = _bodyVerts[0];                                  // the star's place, relative to what the camera is centred on
            double R = _system.StarRadius;
            // the shine: three soft layers in the star's colour, never smaller than a little blur on screen
            var shine = new StarVertex[3];
            for (int k = 0; k < 3; k++) shine[k] = SpaceGen.Star(new Vec3(sp.X, sp.Y, sp.Z), (float)(R * (2 + k * 4)), _system.SR, _system.SG, _system.SB, 0.5f / (k + 1));
            if (_shineBuf == null) _shineBuf = new UploadBuffer(_gpu.Device, 3L * Stride);
            _shineBuf.Write(shine, 3);
            Draw(_shineBuf, 3, vp, right, up, 1f, 0.02f, 0f, w, h, time);
            // close enough to see its disc: the plasma surface and corona, scaled to the star's real size
            var toStar = new Vec3(sp.X - eye.X, sp.Y - eye.Y, sp.Z - eye.Z);
            double d = Math.Sqrt(Vec3.Dot(toStar, toStar));
            if (R / Math.Max(1e-30, d) < 0.004) return;
            var toEye = toStar * (float)(-1 / Math.Max(1e-30, d));
            int n = _starSurface.Draw(toEye, time, _starSurfVerts, 0);
            _starSurfBuf.Write(_starSurfVerts, n);
            var m = Mat4.Identity; m.M11 = m.M22 = m.M33 = (float)R; m.M41 = sp.X; m.M42 = sp.Y; m.M43 = sp.Z;
            Mat4 svp = m * vp;
            Draw(_starSurfBuf, n, svp, right, up, (float)R, 0f, 2f, w, h, time, true);
            Draw(_starCoronaBuf, _starCorona.Length, svp, right, up, (float)R, 0f, 2f, w, h, time, true);
        }

        // the planet being approached, as the real 3D planet, once it is more than a speck on screen
        private void DrawFocusPlanet(Vec3 eye, int w, int h, double time)
        {
            if (FocusTerrain == null || Meshes == null || _focusBody < 0) return;
            double rU = _system.Bodies[_focusBody].RadiusUnits, eyeD = Math.Sqrt(Vec3.Dot(eye, eye));
            if (rU / eyeD < 0.002) return;
            double kmPerUnit = FocusTerrain.Radius / rU;
            double ex = eye.X / eyeD, ey = eye.Y / eyeD, ez = eye.Z / eyeD, camKm = eyeD * kmPerUnit;
            Mat4 viewRot = Mat4.LookAt(new Vec3(0, 0, 0), new Vec3((float)-ex, (float)-ey, (float)-ez), new Vec3(0, 1, 0));
            Mat4 proj = Mat4.Perspective(0.9f, w / (float)Math.Max(1, h), (float)(camKm * 0.05), (float)(camKm * 4));
            _gpu.ClearDepth();
            FocusTerrain.Draw(Meshes, ex * camKm, ey * camKm, ez * camKm, viewRot, proj, time, 2.6, 0, 0, 0, 0, 0, 0, w, h, 24);
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

        private void Draw(UploadBuffer buf, int count, Mat4 vp, Vec3 right, Vec3 up, float sizeScale, float minSize, float style, int w, int h, double time, bool scaled)
        {
            // a scaled group (the star's surface): the size of each point is already in the group's own units
            Draw(buf, count, vp, right, up, scaled ? 1f : sizeScale, minSize, style, w, h, time);
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
                return string.Format("DNA-AND deep zoom · {0} · view {1} across · {2} · time {3} · wheel zoom, drag rotate, Enter visit middle star, click a star in the galaxy or a planet in a system to pick it, then wheel in to fly to it, H core, 1-5 speed, Space pause, Esc = back",
                    _name, Units.Describe(_dist * 1.0), focus, _paused ? "paused" : _speed.ToString("N0") + "x");
            }
        }

        public void Dispose()
        {
            foreach (var b in new[] { _starBuf, _structBuf, _diskBuf, _jetBuf, _coreBuf, _sysStatic, _sysBodies, _starCoronaBuf, _starSurfBuf, _shineBuf }) if (b != null) b.Dispose();
        }
    }
}
