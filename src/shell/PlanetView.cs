// DNA-AND · shell · fly down to one planet: from orbit to the surface, then on down to microbe size.
// The camera never goes into the ground: zooming in lands you on the surface, then keeps shrinking you toward it.
// Water is passed through on the way down to the sea floor.
// Mouse wheel: closer and further. Drag with the left button, or W A S D / arrow keys: move. Q and E: turn.
// Esc, Backspace or right-click: back to the samples.
using System;
using System.Runtime.InteropServices;
using DnaAnd.Core;
using DnaAnd.Gen;
using DnaAnd.Platform;
using DnaAnd.Render;

namespace DnaAnd.Shell
{
    internal sealed class PlanetView : IDisposable
    {
        private readonly PlanetTerrain _terrain;
        private readonly MeshPipes _pipes;
        private readonly BillboardDrawer _dots;
        private double _lat, _lon, _heading;       // where the camera is over the planet, and which way it faces
        private double _alt;                       // km above the solid ground (or sea floor) under the camera
        private readonly double _minAlt;
        private double _time;

        // weather near the camera: rain, snow, ash or dust, falling through a box around the camera
        private const int Drops = 900;
        private readonly double[] _dx = new double[Drops], _dy = new double[Drops], _dz = new double[Drops];
        private readonly StarVertex[] _dropVerts = new StarVertex[Drops];
        private readonly UploadBuffer _dropBuf;
        private readonly Rng _rng = new Rng(77);

        public PlanetView(Gpu gpu, MeshPipes pipes, BillboardPipeline dots, PlanetTerrain terrain)
        {
            _terrain = terrain; _pipes = pipes;
            _dots = new BillboardDrawer(gpu.CommandList, dots);
            _dropBuf = new UploadBuffer(gpu.Device, (long)Drops * Marshal.SizeOf(typeof(StarVertex)));
            for (int i = 0; i < Drops; i++) { _dx[i] = _rng.NextDouble() * 2 - 1; _dy[i] = _rng.NextDouble() * 2 - 1; _dz[i] = _rng.NextDouble() * 2 - 1; }
            _alt = terrain.Radius * 2.2;
            // solid worlds: all the way down to about a millionth of a metre; giants have no ground, so the cloud tops are the floor
            _minAlt = terrain.Solid ? 1e-9 : 0.05;   // giants: down onto the puffy tops of the deep cloud deck
            terrain.StartPoint(out _lat, out _lon);
        }

        public void SetAltitude(double km) { _alt = Math.Max(_minAlt, km); }

        private static string Size(double km)
        {
            double m = km * 1000;
            if (km >= 1000) return (km / 1000).ToString("0.0") + " thousand km";
            if (km >= 1) return km.ToString("0.0") + " km";
            if (m >= 1) return m.ToString("0.0") + " m";
            if (m >= 0.01) return (m * 100).ToString("0.0") + " cm";
            if (m >= 0.001) return (m * 1000).ToString("0.00") + " mm";
            return (m * 1e6).ToString("0.0") + " micrometres";
        }

        private bool _underWater, _overSea; private double _waterDepth;
        private double _shown = 1e9;   // the height actually used this frame (never below finished ground)

        public string Title
        {
            get
            {
                string where = _underWater ? " · under water, " + Size(_waterDepth) + " below the surface" : "";
                string loading = _shown > _alt * 1.01 ? " (finer ground still forming: going down to " + Size(_alt) + ")" : "";
                return "DNA-AND · " + _terrain.Name + " · " + Size(_shown) + loading + " above the " + (_terrain.Solid ? (_overSea ? "sea floor" : "ground") : "deep cloud tops") + where +
                       (_terrain.Solid ? "" : " (a giant has no ground)") + " · wheel: closer and further · drag or W A S D: move · Q E: turn · Esc: back";
            }
        }

        /// <summary>Returns false when the player goes back.</summary>
        public bool Update(Input input, double dt)
        {
            _time += dt;
            if (input.WasPressed(Win32.VK_ESCAPE) || input.WasPressed(0x08) || input.RightClicked) return false;
            if (input.Wheel != 0) _alt = Math.Max(_minAlt, Math.Min(_terrain.Radius * 4, _alt * Math.Pow(0.7, input.Wheel)));
            double R = _terrain.Radius;
            double step = (_alt + 1e-9) / R;                               // ground speed grows with height
            double fwd = 0, side = 0;
            if (input.IsDown(0x57) || input.IsDown(Win32.VK_UP)) fwd += 1;     // W
            if (input.IsDown(0x53) || input.IsDown(Win32.VK_DOWN)) fwd -= 1;   // S
            if (input.IsDown(0x44) || input.IsDown(Win32.VK_RIGHT)) side += 1; // D
            if (input.IsDown(0x41) || input.IsDown(Win32.VK_LEFT)) side -= 1;  // A
            if (input.IsDown(0x51)) _heading -= dt * 0.8;                       // Q
            if (input.IsDown(0x45)) _heading += dt * 0.8;                       // E
            double ch = Math.Cos(_heading), sh = Math.Sin(_heading);
            double north = (fwd * ch - side * sh) * step * dt * 1.2, east = (fwd * sh + side * ch) * step * dt * 1.2;
            if (input.LeftDown)
            {
                // drag the ground: moving the mouse up moves you forward
                double dragF = input.MouseDY * step * 0.004, dragS = -input.MouseDX * step * 0.004;
                north += dragF * ch - dragS * sh; east += dragF * sh + dragS * ch;
            }
            _lat = Math.Max(-1.55, Math.Min(1.55, _lat + north));
            _lon += east / Math.Max(0.05, Math.Cos(_lat));
            return true;
        }

        public void Draw(Gpu gpu, int width, int height)
        {
            double R = _terrain.Radius;
            double ux = Math.Cos(_lat) * Math.Cos(_lon), uy = Math.Sin(_lat), uz = Math.Cos(_lat) * Math.Sin(_lon);
            // the camera always sits on top of the solid ground (the sea floor under water): it lands, it never sinks in
            double ground = _terrain.GroundAt(ux, uy, uz);
            // land on finished ground: while finer ground is still being made, stay a few of its steps above it
            if (_terrain.ReadyStep < double.MaxValue) _shown = Math.Max(_alt, _terrain.ReadyStep * 3); else _shown = _alt;
            double camR = R + ground + _shown;
            _overSea = _terrain.HasWater && ground < 0;
            _underWater = _terrain.HasWater && camR < R;
            _waterDepth = R - camR;
            double cx = ux * camR, cy = uy * camR, cz = uz * camR;
            // which way it looks: straight down from high up, toward the horizon near the ground
            var up = new Vec3((float)ux, (float)uy, (float)uz);
            var northV = new Vec3((float)(-Math.Sin(_lat) * Math.Cos(_lon)), (float)Math.Cos(_lat), (float)(-Math.Sin(_lat) * Math.Sin(_lon)));
            var eastV = Vec3.Cross(up, northV);
            float ch = (float)Math.Cos(_heading), sh = (float)Math.Sin(_heading);
            var forward = northV * ch + eastV * sh;
            double alt = _shown;
            double t = Math.Max(0, Math.Min(1, (Math.Log10(alt) - Math.Log10(2)) / (Math.Log10(R * 0.4) - Math.Log10(2))));
            double pitch = -(0.18 + (1.53 - 0.18) * t * t * (3 - 2 * t));
            if (alt < 2) pitch = -0.18 - 0.5 * Math.Max(0, Math.Min(1, (Math.Log10(2) - Math.Log10(alt)) / 6));  // tiny: look a bit more down at what is in front of you
            var look = forward * (float)Math.Cos(pitch) + up * (float)Math.Sin(pitch);
            var lookUp = forward * (float)(-Math.Sin(pitch)) + up * (float)Math.Cos(pitch);
            Mat4 view = Mat4.LookAt(new Vec3(0, 0, 0), look, lookUp);

            double horizon = Math.Sqrt(Math.Max(0, camR * camR - R * R));
            float near = (float)Math.Max(1e-10, alt * 0.25);
            float far = (float)Math.Min(horizon * 1.3 + R * 0.05 + 50, alt * 2e5 + 1e-6);
            if (alt > R * 0.2) { near = (float)(alt * 0.2); far = (float)(alt + R * 2.2); }
            Mat4 proj = Mat4.Perspective(1.0f, width / (float)Math.Max(1, height), near, far);

            // the sky: the air's colour near the ground, fading to black space higher up; under water, dark blue
            float sr = _terrain.AirR * 0.55f, sg = _terrain.AirG * 0.6f, sb = _terrain.AirB * 0.75f;
            float airT = (float)Math.Max(0, Math.Min(1, 1 - Math.Log10(Math.Max(1, camR - R)) / Math.Log10(Math.Max(10, _terrain.AirKm))));
            float fogDist = airT > 0.01 ? (float)Math.Min(horizon * 1.25 + 5, far * 0.9) : 0;
            if (_underWater)
            {
                sr = 0.02f; sg = 0.10f; sb = 0.2f; airT = 1;
                fogDist = (float)Math.Min(0.06 + alt * 50, far * 0.9);   // you can see only a few tens of metres through water
            }
            gpu.ClearNow(sr * airT, sg * airT, sb * airT);
            _terrain.Draw(_pipes, cx, cy, cz, view, proj, _time, 3.0, fogDist, sr, sg, sb, 0, 0, width, height, 400);
            DrawWeather(view, proj, up, northV, eastV, width, height);
        }

        // rain, snow, ash or dust falling around the camera when it is low and under a cloud
        private void DrawWeather(Mat4 view, Mat4 proj, Vec3 up, Vec3 north, Vec3 east, int width, int height)
        {
            if (!_terrain.Solid || _underWater || _alt > 3) return;
            double ang = _terrain.CloudTurn(_time), ca = Math.Cos(ang), sa = Math.Sin(ang);
            double ux = up.X * ca - up.Z * sa, uz = up.X * sa + up.Z * ca;      // the spot above us, in the clouds' frame
            double cover = _terrain.CloudCover(ux, up.Y, uz);
            if (cover < 0.45) return;
            float r = 0.55f, g = 0.6f, b = 0.7f, fall = 1f, size = 0.0012f;
            string k = _terrain.Kind;
            bool snow = k == "snowball" || Math.Abs(up.Y) > 0.8;
            if (snow) { r = g = b = 0.95f; fall = 0.15f; size = 0.002f; }
            if (k == "lava") { r = 0.35f; g = 0.32f; b = 0.3f; fall = 0.25f; }      // falling ash
            if (k == "desert") { r = 0.8f; g = 0.6f; b = 0.4f; fall = 0.05f; }       // blowing dust
            double box = Math.Max(1e-6, Math.Min(0.05, _alt * 3 + 0.002));          // km: a box a few times the camera's height
            int n = (int)(Drops * Math.Min(1, (cover - 0.45) * 3));
            float s = (float)(box * size);
            for (int i = 0; i < n; i++)
            {
                _dy[i] -= 0.016 * fall * 6;
                if (k == "desert") _dx[i] += 0.016 * 0.8;
                if (_dy[i] < -1) _dy[i] += 2;
                if (_dx[i] > 1) _dx[i] -= 2;
                var p = (east * (float)_dx[i] + up * (float)_dy[i] + north * (float)_dz[i]) * (float)box;
                _dropVerts[i] = SpaceGen.Star(p, s, r, g, b, 0.6f);
            }
            if (n <= 0) return;
            _dropBuf.Write(_dropVerts, n);
            Mat4 vp = view * proj;
            var c = new float[BillboardPipeline.ConstantCount];
            c[0] = vp.M11; c[1] = vp.M12; c[2] = vp.M13; c[3] = vp.M14; c[4] = vp.M21; c[5] = vp.M22; c[6] = vp.M23; c[7] = vp.M24;
            c[8] = vp.M31; c[9] = vp.M32; c[10] = vp.M33; c[11] = vp.M34; c[12] = vp.M41; c[13] = vp.M42; c[14] = vp.M43; c[15] = vp.M44;
            c[16] = view.M11; c[17] = view.M21; c[18] = view.M31; c[20] = view.M12; c[21] = view.M22; c[22] = view.M32;
            c[25] = 1; c[26] = 1; c[27] = 4; c[28] = width; c[29] = height;
            _dots.Draw(_dropBuf, n, c, 0, 0, width, height);
        }

        public void Dispose() { _dropBuf.Dispose(); }
    }
}
