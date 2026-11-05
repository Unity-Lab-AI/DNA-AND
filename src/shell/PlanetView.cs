// DNA-AND · shell · fly down to one planet: from orbit all the way to standing on the ground.
// Mouse wheel: height. Drag with the left button, or W A S D / arrow keys: move over the ground. Q and E: turn.
// Esc, Backspace or right-click: back to the samples.
using System;
using DnaAnd.Core;
using DnaAnd.Gen;
using DnaAnd.Platform;
using DnaAnd.Render;

namespace DnaAnd.Shell
{
    internal sealed class PlanetView : IDisposable
    {
        private readonly PlanetTerrain _terrain;
        private readonly MeshDrawer _mesh;
        private double _lat = 0.35, _lon = 0.6, _heading = 0;   // where the camera is over the planet, and which way it faces
        private double _alt;                                     // km above the ground under the camera
        private readonly double _minAlt;

        public PlanetView(Gpu gpu, MeshPipeline meshPipe, PlanetTerrain terrain)
        {
            _terrain = terrain;
            _mesh = new MeshDrawer(gpu.CommandList, meshPipe);
            _alt = terrain.Radius * 2.2;
            _minAlt = terrain.Solid ? 0.0018 : 40;               // stand on rock (about head height); giants have no ground, stop above the clouds
            // start over the day side
            terrain.StartPoint(out _lat, out _lon);
        }

        public void SetAltitude(double km) { _alt = Math.Max(_minAlt, km); }

        public string Title
        {
            get
            {
                string h = _alt >= 1000 ? (_alt / 1000).ToString("0.0") + " thousand km" : _alt >= 1 ? _alt.ToString("0.0") + " km" : (_alt * 1000).ToString("0") + " m";
                return "DNA-AND · " + _terrain.Name + " · height " + h + (_terrain.Solid ? "" : " (a giant has no ground: this is as low as the cloud tops)") +
                       " · wheel: up and down · drag or W A S D: move · Q E: turn · Esc: back";
            }
        }

        /// <summary>Returns false when the player goes back.</summary>
        public bool Update(Input input, double dt)
        {
            if (input.WasPressed(Win32.VK_ESCAPE) || input.WasPressed(0x08) || input.RightClicked) return false;
            if (input.Wheel != 0) _alt = Math.Max(_minAlt, Math.Min(_terrain.Radius * 4, _alt * Math.Pow(0.8, input.Wheel)));
            double R = _terrain.Radius;
            double step = (_alt + 0.002) / R;                              // ground speed grows with height
            double fwd = 0, side = 0;
            if (input.IsDown(0x57) || input.IsDown(Win32.VK_UP)) fwd += 1;   // W
            if (input.IsDown(0x53) || input.IsDown(Win32.VK_DOWN)) fwd -= 1; // S
            if (input.IsDown(0x44) || input.IsDown(Win32.VK_RIGHT)) side += 1; // D
            if (input.IsDown(0x41) || input.IsDown(Win32.VK_LEFT)) side -= 1;  // A
            if (input.IsDown(0x51)) _heading -= dt * 0.8;                     // Q
            if (input.IsDown(0x45)) _heading += dt * 0.8;                     // E
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
            // where the camera is: straight up from the ground under it
            double ux = Math.Cos(_lat) * Math.Cos(_lon), uy = Math.Sin(_lat), uz = Math.Cos(_lat) * Math.Sin(_lon);
            double ground = _terrain.Solid ? _terrain.GroundAt(ux, uy, uz) : 0;
            double camR = R + ground + _alt;
            double cx = ux * camR, cy = uy * camR, cz = uz * camR;
            // which way it looks: straight down from high up, toward the horizon near the ground
            var up = new Vec3((float)ux, (float)uy, (float)uz);
            var northV = new Vec3((float)(-Math.Sin(_lat) * Math.Cos(_lon)), (float)Math.Cos(_lat), (float)(-Math.Sin(_lat) * Math.Sin(_lon)));
            var eastV = Vec3.Cross(up, northV);
            float ch = (float)Math.Cos(_heading), sh = (float)Math.Sin(_heading);
            var forward = northV * ch + eastV * sh;
            double t = Math.Max(0, Math.Min(1, (Math.Log10(_alt) - Math.Log10(2)) / (Math.Log10(R * 0.4) - Math.Log10(2))));
            double pitch = -(0.18 + (1.53 - 0.18) * t * t * (3 - 2 * t));
            var look = forward * (float)Math.Cos(pitch) + up * (float)Math.Sin(pitch);
            var lookUp = forward * (float)(-Math.Sin(pitch)) + up * (float)Math.Cos(pitch);
            Mat4 view = Mat4.LookAt(new Vec3(0, 0, 0), look, lookUp);

            double horizon = Math.Sqrt(Math.Max(0, camR * camR - R * R));
            float near = (float)Math.Max(0.0002, _alt * 0.25);
            float far = (float)(horizon * 1.3 + R * 0.05 + 50);
            if (_alt > R * 0.2) { near = (float)(_alt * 0.2); far = (float)(_alt + R * 2.2); }
            Mat4 proj = Mat4.Perspective(1.0f, width / (float)Math.Max(1, height), near, far);

            // the air: blue sky near the ground on worlds with air, fading to black space higher up
            float sr, sg, sb; Sky(out sr, out sg, out sb);
            float airT = (float)Math.Max(0, Math.Min(1, 1 - Math.Log10(Math.Max(1, _alt)) / 2.3));   // 1 at the ground, 0 above ~200 km
            float fogDist = airT > 0.01 ? (float)(horizon * 1.25 + 5) : 0;
            gpu.ClearNow(sr * airT, sg * airT, sb * airT);
            _terrain.Draw(_mesh, cx, cy, cz, view, proj, 3.0, fogDist, sr, sg, sb, 0, 0, width, height, 10);
        }

        private void Sky(out float r, out float g, out float b)
        {
            switch (_terrain.Kind)
            {
                case "desert": r = 0.75f; g = 0.55f; b = 0.38f; break;
                case "lava": r = 0.45f; g = 0.18f; b = 0.1f; break;
                case "snowball": r = 0.55f; g = 0.7f; b = 0.9f; break;
                case "tidally-locked": r = 0.5f; g = 0.55f; b = 0.7f; break;
                case "hot-jupiter": r = 0.6f; g = 0.3f; b = 0.15f; break;
                case "gas-giant": r = 0.7f; g = 0.6f; b = 0.45f; break;
                case "ice-giant": r = 0.3f; g = 0.5f; b = 0.85f; break;
                default: r = 0.42f; g = 0.62f; b = 0.92f; break;
            }
        }

        public void Dispose() { }
    }
}
