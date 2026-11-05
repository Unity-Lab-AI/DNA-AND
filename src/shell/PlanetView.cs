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
        private PlanetTerrain _terrain;                        // the one being shown
        private readonly PlanetTerrain _normal, _fields;       // the planet as it looks, and the same planet in the resource view
        private int _jumpField = -1, _jumpCount;
        private readonly MeshPipes _pipes;
        private readonly BillboardDrawer _dots;
        private double _lat, _lon, _heading;       // where the camera is over the planet, and which way it faces
        private double _alt;                       // km above the solid ground (or sea floor) under the camera
        private readonly double _minAlt;
        private double _time;
        /// <summary>Part of the one journey from the galaxy: zooming out past the planet goes back up to its solar system.</summary>
        public bool Journey; public bool Ascend;
        private DnaAnd.Sim.MicrobeWorld _microbes; private double _mx, _my, _mz;      // the living microbes on the spot below, once you are small enough
        private Vec3 _mEast, _mNorth, _mUp; private double _microbeAccum;
        private UploadBuffer _microbeBuf; private StarVertex[] _microbeVerts;
        private bool _inside;
        // free flight inside a sphere: a real position and a look direction. W/S fly along the look, A/D step sideways,
        // drag with the left button to look around, the wheel flies forward and back. You stop at the surface.
        private double _px, _py, _pz;            // km, star at 0
        private Vec3 _look = new Vec3(1, 0, 0);
        private bool _flyReady;
        private bool _centre; private double _centreTurn;   // floating near the star, looking out at the whole inner world                      // inside a Dyson shell: standing on the inner land, the star overhead
        private UploadBuffer _starBuf; private int _starCount;
        public StarVertex[] Extras;                // the structure's own pieces (docks, ships, collectors), in km around the star; set by the sample viewer
        private UploadBuffer _extraBuf;

        // weather near the camera: rain, snow, ash or dust, falling through a box around the camera
        private const int Drops = 900;
        private readonly double[] _dx = new double[Drops], _dy = new double[Drops], _dz = new double[Drops];
        private readonly StarVertex[] _dropVerts = new StarVertex[Drops];
        private readonly UploadBuffer _dropBuf;
        private readonly Rng _rng = new Rng(77);

        public PlanetView(Gpu gpu, MeshPipes pipes, BillboardPipeline dots, PlanetTerrain normal, PlanetTerrain fields, bool startInFieldView)
        {
            _normal = normal; _fields = fields; _pipes = pipes;
            var terrain = startInFieldView ? fields : normal;
            _terrain = terrain;
            _dots = new BillboardDrawer(gpu.CommandList, dots);
            _dropBuf = new UploadBuffer(gpu.Device, (long)Drops * Marshal.SizeOf(typeof(StarVertex)));
            for (int i = 0; i < Drops; i++) { _dx[i] = _rng.NextDouble() * 2 - 1; _dy[i] = _rng.NextDouble() * 2 - 1; _dz[i] = _rng.NextDouble() * 2 - 1; }
            _alt = terrain.Radius * 2.3;   // the same distance the grid shows it from, so the zoom carries straight on
            // solid worlds: all the way down to about a millionth of a metre; giants have no ground, so the cloud tops are the floor
            _minAlt = terrain.Solid ? 1e-9 : 0.05;   // giants: down onto the puffy tops of the deep cloud deck
            terrain.StartPoint(out _lat, out _lon);
            if (terrain.Shell)
            {
                _alt = terrain.Radius * 1.6; _lat = 0.3; _lon = 0.4;     // start outside, the whole sphere in view
                // the star at the centre: a bright glowing ball
                var rng = new Rng(5);
                // the star at the centre: a bright ball with a wide glowing halo, so it reads clearly from anywhere inside
                var sv = new StarVertex[4200];
                for (int i = 0; i < sv.Length; i++)
                {
                    bool ball = i < 2400, halo = i >= 3600;
                    double u = rng.NextDouble() * 2 - 1, a = rng.NextDouble() * Math.PI * 2, rr = Math.Sqrt(1 - u * u);
                    double d = 696000 * (ball ? 1 : halo ? rng.Range(2f, 9f) : rng.Range(1.05f, 2f));
                    sv[i] = SpaceGen.Star(new Vec3((float)(Math.Cos(a) * rr * d), (float)(u * d), (float)(Math.Sin(a) * rr * d)),
                        ball ? 160000 : halo ? 2500000 : 500000, 1f, 0.9f, 0.62f, ball ? 0.7f : halo ? 0.025f : 0.1f);
                }
                _starCount = sv.Length;
                _starBuf = new UploadBuffer(gpu.Device, (long)sv.Length * Marshal.SizeOf(typeof(StarVertex)));
                _starBuf.Write(sv, sv.Length);
            }
        }

        public void SetAltitude(double km) { _alt = Math.Max(_minAlt, km); }
        public void Centre() { _centre = true; }
        public void GoInside() { if (_terrain.Shell && _terrain.Habitable) { _inside = true; _alt = 30; _terrain.StartPoint(out _lat, out _lon); _flyReady = false; } }

        private void PlaceFlyer()
        {
            double R = _terrain.Radius;
            double ux = Math.Cos(_lat) * Math.Cos(_lon), uy = Math.Sin(_lat), uz = Math.Cos(_lat) * Math.Sin(_lon);
            if (double.IsNaN(_alt) || _alt <= 0 || _alt > R * 0.5) _alt = 30;   // a sane height to come in at
            double r = R - _terrain.GroundAt(ux, uy, uz) - _alt;
            _px = ux * r; _py = uy * r; _pz = uz * r;
            var up = new Vec3((float)-ux, (float)-uy, (float)-uz);
            var north = new Vec3((float)(-Math.Sin(_lat) * Math.Cos(_lon)), (float)Math.Cos(_lat), (float)(-Math.Sin(_lat) * Math.Sin(_lon)));
            _look = (north + up * -0.35f).Normalized;            // along the land, a little down toward it
            _flyReady = true;
        }

        /// <summary>Height above the land under the camera (km), and the land's distance from the star there.</summary>
        private double FlyHeight(out double surfR)
        {
            double r = Math.Sqrt(_px * _px + _py * _py + _pz * _pz);
            double g = _terrain.GroundAt(_px / r, _py / r, _pz / r);
            surfR = _terrain.Radius - g;
            return surfR - r;
        }

        private bool Fly(Input input, double dt)
        {
            if (!_flyReady) PlaceFlyer();
            double surfR; double h = Math.Max(1e-9, FlyHeight(out surfR));
            double r = Math.Sqrt(_px * _px + _py * _py + _pz * _pz);
            var up = new Vec3((float)(-_px / r), (float)(-_py / r), (float)(-_pz / r));       // up points toward the star
            var side = Vec3.Cross(up, _look).Normalized;                                     // to your right
            // look around: drag left and right turns, up and down tilts
            if ((input.LeftDown || input.RightDown) && (input.MouseDX != 0 || input.MouseDY != 0))
            {
                float yaw = input.MouseDX * 0.004f, tilt = -input.MouseDY * 0.004f;
                _look = (_look * (float)Math.Cos(yaw) + side * (float)Math.Sin(yaw)).Normalized;
                side = Vec3.Cross(up, _look).Normalized;
                var nl = (_look * (float)Math.Cos(tilt) + Vec3.Cross(_look, side) * (float)Math.Sin(tilt)).Normalized;
                if (Math.Abs(Vec3.Dot(nl, up)) < 0.98f) _look = nl;
            }
            if (input.IsDown(0x51)) { _look = (_look * (float)Math.Cos(-dt) + side * (float)Math.Sin(-dt)).Normalized; }   // Q: turn left
            if (input.IsDown(0x45)) { _look = (_look * (float)Math.Cos(dt) + side * (float)Math.Sin(dt)).Normalized; }     // E: turn right
            side = Vec3.Cross(up, _look).Normalized;
            // move: in a straight line, faster the higher you are
            double speed = h * 1.2 + 1e-9;
            double fwd = 0, step = 0;
            if (input.IsDown(0x57) || input.IsDown(Win32.VK_UP)) fwd += 1;
            if (input.IsDown(0x53) || input.IsDown(Win32.VK_DOWN)) fwd -= 1;
            if (input.IsDown(0x44) || input.IsDown(Win32.VK_RIGHT)) step += 1;
            if (input.IsDown(0x41) || input.IsDown(Win32.VK_LEFT)) step -= 1;
            double dx = (_look.X * fwd + side.X * step) * speed * dt, dy = (_look.Y * fwd + side.Y * step) * speed * dt, dz = (_look.Z * fwd + side.Z * step) * speed * dt;
            if (input.Wheel != 0) { double w = input.Wheel * h * 0.5; dx += _look.X * w; dy += _look.Y * w; dz += _look.Z * w; }
            _px += dx; _py += dy; _pz += dz;
            // never through the land: stop just above it (and never into the star)
            double h2 = FlyHeight(out surfR);
            double r2 = Math.Sqrt(_px * _px + _py * _py + _pz * _pz), minH = 1e-9;
            if (h2 < minH) { double k = (surfR - minH) / r2; _px *= k; _py *= k; _pz *= k; }
            if (r2 < 2e6) { double k = 2e6 / r2; _px *= k; _py *= k; _pz *= k; }
            _alt = Math.Max(minH, FlyHeight(out surfR));
            return true;
        }

        private void DrawFly(Gpu gpu, int width, int height)
        {
            // place the camera before the first picture inside (otherwise it sits at the star and every number breaks)
            if (!_flyReady) PlaceFlyer();
            double R = _terrain.Radius, surfR;
            double h = Math.Max(1e-9, FlyHeight(out surfR));
            _alt = h; _shown = h; _underWater = false; _overSea = false; _fieldHere = -1;
            double r = Math.Sqrt(_px * _px + _py * _py + _pz * _pz);
            var up = new Vec3((float)(-_px / r), (float)(-_py / r), (float)(-_pz / r));
            Mat4 view = Mat4.LookAt(new Vec3(0, 0, 0), _look, up);
            float near = (float)Math.Max(1e-10, h * 0.2);
            float far = (float)Math.Min(R * 2.2, Math.Max(h * 3e4, 1e-4));
            Mat4 proj = Mat4.Perspective(1.0f, width / (float)Math.Max(1, height), near, far);
            float airT = (float)Math.Max(0, Math.Min(1, 1 - Math.Log10(Math.Max(1, h)) / 3.7));
            float sr = _terrain.AirR * 0.55f, sg = _terrain.AirG * 0.6f, sb = _terrain.AirB * 0.75f;
            float fogDist = airT > 0.01 ? (float)(far * 0.9) : 0;
            gpu.ClearNow(sr * airT, sg * airT, sb * airT);
            _terrain.Draw(_pipes, _px, _py, _pz, view, proj, _time, 3.0, fogDist, sr, sg, sb, 0, 0, width, height, 400);
            DrawStructure(gpu, view, proj, _px, _py, _pz, width, height);
        }

        /// <summary>Flies to an example of the next kind of resource field, 2 km above it.</summary>
        public void JumpToNextField()
        {
            for (int k = 0; k < PlanetTerrain.Fields.Count; k++)
            {
                _jumpField = (_jumpField + 1) % PlanetTerrain.Fields.Count;
                double x, y, z;
                if (_normal.FindField(_jumpField, _jumpCount++, out x, out y, out z))
                {
                    _lat = Math.Asin(Math.Max(-1, Math.Min(1, y))); _lon = Math.Atan2(z, x); _alt = 2;
                    return;
                }
            }
        }

        /// <summary>What each colour means in the resource view, and the keys.</summary>
        public string Legend()
        {
            string air; PlanetTerrain.Air.TryGetValue(_terrain.Kind, out air);
            var sb = new System.Text.StringBuilder(_terrain.Name + "\n\nAir: " + (air ?? (_terrain.Shell ? "nitrogen 78%, oxygen 21%, argon 1% (made to be breathed)" : "none")) + "\n\nResource fields (the AI species find and use these; you are a god and have no use for them):\n\n");
            foreach (var f in PlanetTerrain.Fields)
                sb.Append("  " + ColourName(f) + "  " + f.Name + (f.Buried ? " (buried)" : "") + (f.Id == "coal" || f.Id == "oil-gas" ? " (made from buried life: none until life has lived here)" : "") + (f.Where.Length > 0 ? ": " + f.Where : "") + "\n");
            sb.Append("\nKeys:\n  R: resource view on or off\n  N: fly to the next kind of field\n  Mouse wheel: closer and further · drag or W A S D: move · Q E: turn\n  Esc: back");
            return sb.ToString();
        }

        private static string ColourName(PlanetTerrain.FieldKind f)
        {
            // a plain name for each colour, so the list can be read without seeing it
            string[] names = { "blue", "yellow", "orange", "mint green", "gold", "purple", "pink", "pale gold", "white", "rust red", "dark red", "sky blue", "olive", "grey", "magenta", "green", "ice blue" };
            int i = PlanetTerrain.Fields.IndexOf(f);
            return "[" + (i >= 0 && i < names.Length ? names[i] : "?") + "]";
        }

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

        private bool _underWater, _overSea; private double _waterDepth; private int _fieldHere = -1;
        private double _shown = 1e9;   // the height actually used this frame (never below finished ground)

        public string Title
        {
            get
            {
                string where = _underWater ? " · under water, " + Size(_waterDepth) + " below the surface" : "";
                if (_terrain.Solid && _fieldHere >= 0 && !_terrain.Shell) where += " · field here: " + PlanetTerrain.Fields[_fieldHere].Name;
                if (_centre) return "DNA-AND · " + _terrain.Name + " · THE VIEW FROM THE MIDDLE: the whole inner world wrapped around its star, the metal hull outside · drag to turn · C to go back";
                if (_terrain.Shell)
                    where += _inside ? " · INSIDE: the shell's spin holds you on its inner land, the star overhead (I or C to go back out, M for the view from the middle)"
                                     : (_terrain.Habitable ? " · outside the hull (I or C to go inside, M for the view from the middle)" : " · outside the hull");
                if (_terrain == _fields) where += " · RESOURCE VIEW (R to switch off, Legend for colours)";
                string loading = _shown > _alt * 1.01 ? " (finer ground still forming: going down to " + Size(_alt) + ")" : "";
                return "DNA-AND · " + _terrain.Name + " · " + Size(_shown) + loading + " above the " + (_terrain.Shell && !_inside ? "hull" : _terrain.Solid ? (_overSea ? "sea floor" : "ground") : "deep cloud tops") + where +
                       (_terrain.Solid ? "" : " (a giant has no ground)") + (_inside ? " · W S: fly forward and back · A D: step sideways · drag: look around · wheel: fly forward and back · Esc: back"
                            : " · wheel: closer and further · drag or W A S D: move · Q E: turn · R: resource view · N: next field · Esc: back");
            }
        }

        /// <summary>Returns false when the player goes back.</summary>
        public bool Update(Input input, double dt)
        {
            _time += dt;
            if (input.WasPressed(Win32.VK_ESCAPE) || input.WasPressed(0x08)) return false;   // only Esc or Backspace leave; a mouse click never does
            if (input.WasPressed(0x52) && _terrain.Solid && !_terrain.Shell) _terrain = _terrain == _normal ? _fields : _normal;     // R: resource view on or off
            // I: in or out through the hull, from anywhere (even the view from the middle)
            if ((input.WasPressed(0x49) || input.WasPressed(0x43)) && _terrain.Shell && _terrain.Habitable)   // I or C
            {
                if (_inside || _centre)
                {
                    // out: come out right above where you were inside
                    if (_flyReady) { double rr = Math.Sqrt(_px * _px + _py * _py + _pz * _pz); _lat = Math.Asin(_py / rr); _lon = Math.Atan2(_pz, _px); }
                    _inside = false; _centre = false; _alt = 2000;
                }
                else { _inside = true; _terrain.StartPoint(out _lat, out _lon); _flyReady = false; _alt = 30; }
                return true;
            }
            // M: the view from the middle, and M again goes back to where you were
            if (input.WasPressed(0x4D) && _terrain.Shell && _terrain.Habitable) { _centre = !_centre; if (!_centre && !_inside) { _inside = true; _flyReady = false; } return true; }
            if (_centre)
            {
                // no automatic turning: only dragging turns the view
                if (input.LeftDown) _centreTurn += input.MouseDX * 0.004;
                // the wheel flies you down from the middle onto the inner surface, and on down from there
                if (input.Wheel > 0)
                {
                    // from the middle, fly straight out the way you are looking, toward the land
                    _centre = false; _inside = true;
                    double sR = _terrain.Radius, a = _centreTurn;
                    _px = Math.Cos(a) * sR * 0.45; _py = sR * 0.1; _pz = Math.Sin(a) * sR * 0.45;
                    _look = new Vec3((float)-Math.Cos(a + 0.35), -0.22f, (float)-Math.Sin(a + 0.35)).Normalized; _flyReady = true;
                }
                else return true;
            }
            if (_inside && !_centre) return Fly(input, dt);
            if (input.WasPressed(0x4E) && _terrain.Solid) JumpToNextField();                                       // N: go to the next kind of field
            double maxAlt = _inside ? _terrain.Radius * 0.95 : _terrain.Radius * 4;     // inside, you can rise almost to the star
            double perNotch = _inside && _alt > 10 ? 0.4 : 0.7;      // inside a sphere the heights are huge: bigger steps down
            if (Journey && !_inside && input.Wheel < 0 && _alt >= maxAlt * 0.99) { Ascend = true; return false; }   // out past the planet: back to its solar system
            if (input.Wheel != 0) _alt = Math.Max(_minAlt, Math.Min(maxAlt, _alt * Math.Pow(perNotch, input.Wheel)));
            if (_microbes != null) { _microbeAccum += dt; var sw = System.Diagnostics.Stopwatch.StartNew(); while (_microbeAccum > 0.05 && sw.Elapsed.TotalMilliseconds < 6) { _microbes.Step(0.05); _microbeAccum -= 0.05; } if (_microbeAccum > 1) _microbeAccum = 0; }
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
            if (input.RightDown) _heading += input.MouseDX * 0.004;       // right-drag: turn
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
            if (_centre) { DrawCentre(gpu, width, height); return; }
            if (_inside) { DrawFly(gpu, width, height); return; }
            double R = _terrain.Radius;
            double ux = Math.Cos(_lat) * Math.Cos(_lon), uy = Math.Sin(_lat), uz = Math.Cos(_lat) * Math.Sin(_lon);
            // the camera always sits on top of the solid ground (the sea floor under water): it lands, it never sinks in
            double ground = _terrain.Shell ? (_inside ? _terrain.GroundAt(ux, uy, uz) : _terrain.HullKm + Math.Max(0, _terrain.HullHeightAt(ux, uy, uz))) : _terrain.GroundAt(ux, uy, uz);
            _fieldHere = _terrain.Solid ? _terrain.LastField : -1;
            // land on finished ground: while finer ground is still being made, stay a few of its steps above it
            if (_terrain.ReadyStep < double.MaxValue) _shown = Math.Max(_alt, _terrain.ReadyStep * 3); else _shown = _alt;
            double camR = _inside ? R - ground - _shown : R + ground + _shown;   // inside a shell, "up" points toward the star
            _overSea = _terrain.HasWater && ground < 0 && (_inside || !_terrain.Shell);
            _underWater = _terrain.HasWater && (_inside ? camR > R : camR < R && !_terrain.Shell);
            _waterDepth = Math.Abs(R - camR);
            double cx = ux * camR, cy = uy * camR, cz = uz * camR;
            // which way it looks: straight down from high up, toward the horizon near the ground
            var up = _inside ? new Vec3((float)-ux, (float)-uy, (float)-uz) : new Vec3((float)ux, (float)uy, (float)uz);
            var northV = new Vec3((float)(-Math.Sin(_lat) * Math.Cos(_lon)), (float)Math.Cos(_lat), (float)(-Math.Sin(_lat) * Math.Sin(_lon)));
            var eastV = Vec3.Cross(up, northV);
            float ch = (float)Math.Cos(_heading), sh = (float)Math.Sin(_heading);
            var forward = northV * ch + eastV * sh;
            double alt = _shown;
            double t = Math.Max(0, Math.Min(1, (Math.Log10(alt) - Math.Log10(2)) / (Math.Log10(R * 0.4) - Math.Log10(2))));
            double pitch = -(0.18 + (1.53 - 0.18) * t * t * (3 - 2 * t));
            if (alt < 2) pitch = -0.18 - 0.5 * Math.Max(0, Math.Min(1, (Math.Log10(2) - Math.Log10(alt)) / 6));  // tiny: look a bit more down at what is in front of you
            if (_inside && alt > 2000) pitch = -0.25;     // high up inside: look out across the inner world, which curves up all around you
            var look = forward * (float)Math.Cos(pitch) + up * (float)Math.Sin(pitch);
            var lookUp = forward * (float)(-Math.Sin(pitch)) + up * (float)Math.Cos(pitch);
            Mat4 view = Mat4.LookAt(new Vec3(0, 0, 0), look, lookUp);

            double horizon = _inside ? Math.Min(R * 2, 3000 + _shown * 400) : Math.Sqrt(Math.Max(0, camR * camR - R * R));
            float near = (float)Math.Max(1e-10, alt * 0.25);
            float far = (float)Math.Min(horizon * 1.3 + R * 0.05 + 50, alt * 2e5 + 1e-6);
            if (alt > R * 0.2 && !_inside) { near = (float)(alt * 0.2); far = (float)(alt + R * 2.2); }
            if (_inside) far = (float)(alt < 5000 ? Math.Min(alt * 2e5 + 50, 2e6) : R * 2.2);   // low: the far land is lost in haze; high: see the whole inner world
            Mat4 proj = Mat4.Perspective(1.0f, width / (float)Math.Max(1, height), near, far);

            // the sky: the air's colour near the ground, fading to black space higher up; under water, dark blue
            float sr = _terrain.AirR * 0.55f, sg = _terrain.AirG * 0.6f, sb = _terrain.AirB * 0.75f;
            float airT = (float)Math.Max(0, Math.Min(1, 1 - Math.Log10(Math.Max(1, camR - R)) / Math.Log10(Math.Max(10, _terrain.AirKm))));
            if (_terrain.Shell) airT = _inside ? (float)Math.Max(0, Math.Min(1, 1 - Math.Log10(Math.Max(1, alt)) / 3.7)) : 0;   // the air is held near the inner land; high up it thins to clear
            float fogDist = airT > 0.01 ? (float)Math.Min(horizon * 1.25 + 5, far * 0.9) : 0;
            if (_underWater)
            {
                sr = 0.02f; sg = 0.10f; sb = 0.2f; airT = 1;
                fogDist = (float)Math.Min(0.06 + alt * 50, far * 0.9);   // you can see only a few tens of metres through water
            }
            gpu.ClearNow(sr * airT, sg * airT, sb * airT);
            _terrain.Draw(_pipes, cx, cy, cz, view, proj, _time, 3.0, fogDist, sr, sg, sb, 0, 0, width, height, 400);
            if (_terrain.Shell) DrawStructure(gpu, view, proj, cx, cy, cz, width, height);
            DrawWeather(view, proj, up, northV, eastV, width, height);
            DrawMicrobes(gpu, view, proj, cx, cy, cz, ux, uy, uz, ground, up, northV, eastV, width, height);
        }

        // the view from the middle: float a third of the way out from the star and look at the inner world all around
        private void DrawCentre(Gpu gpu, int width, int height)
        {
            double R = _terrain.Radius, a = _centreTurn;
            double cx = Math.Cos(a) * R * 0.45, cy = R * 0.1, cz = Math.Sin(a) * R * 0.45;
            var look = new Vec3((float)-Math.Cos(a + 0.35), -0.22f, (float)-Math.Sin(a + 0.35));   // back across the star toward the far side
            float ll = (float)Math.Sqrt(Vec3.Dot(look, look)); look = look * (1f / ll);
            Mat4 view = Mat4.LookAt(new Vec3(0, 0, 0), look, new Vec3(0, 1, 0));
            Mat4 proj = Mat4.Perspective(1.3f, width / (float)Math.Max(1, height), (float)(R * 0.02), (float)(R * 3));
            gpu.ClearNow(0.01f, 0.012f, 0.02f);
            _terrain.Draw(_pipes, cx, cy, cz, view, proj, _time, 2.2, 0, 0, 0, 0, 0, 0, width, height, 30);
            DrawStructure(gpu, view, proj, cx, cy, cz, width, height);
        }

        // the star in the middle, and the structure's docks, ships and collectors, all drawn relative to the camera
        private void DrawStructure(Gpu gpu, Mat4 view, Mat4 proj, double cx, double cy, double cz, int width, int height)
        {
            Mat4 vp = Mat4.Translation((float)-cx, (float)-cy, (float)-cz) * view * proj;
            var c = new float[BillboardPipeline.ConstantCount];
            c[0] = vp.M11; c[1] = vp.M12; c[2] = vp.M13; c[3] = vp.M14; c[4] = vp.M21; c[5] = vp.M22; c[6] = vp.M23; c[7] = vp.M24;
            c[8] = vp.M31; c[9] = vp.M32; c[10] = vp.M33; c[11] = vp.M34; c[12] = vp.M41; c[13] = vp.M42; c[14] = vp.M43; c[15] = vp.M44;
            c[16] = view.M11; c[17] = view.M21; c[18] = view.M31; c[20] = view.M12; c[21] = view.M22; c[22] = view.M32;
            c[25] = 1; c[26] = 0; c[28] = width; c[29] = height; c[30] = 0.004f;
            _dots.Draw(_starBuf, _starCount, c, 0, 0, width, height);
            if (Extras != null && Extras.Length > 0)
            {
                if (_extraBuf == null) { _extraBuf = new UploadBuffer(gpu.Device, (long)Extras.Length * Marshal.SizeOf(typeof(StarVertex))); _extraBuf.Write(Extras, Extras.Length); }
                c[26] = 2; c[30] = 0.0015f;
                _dots.Draw(_extraBuf, Extras.Length, c, 0, 0, width, height);
            }
        }

        // at microbe size, the living microbes of set 8 on the spot below (a patch of ground or water about 0.4 mm across)
        private void DrawMicrobes(Gpu gpu, Mat4 view, Mat4 proj, double cx, double cy, double cz, double ux, double uy, double uz, double ground, Vec3 up, Vec3 north, Vec3 east, int width, int height)
        {
            if (!_terrain.Solid || _terrain.Kind == "lava") { _microbes = null; return; }       // nothing can live in molten rock
            double sz = _shown;
            if (sz > 2e-6) { if (sz > 5e-6) _microbes = null; return; }                            // only below about 2 mm
            if (_microbes == null)
            {
                // a patch here: its own seed from the place, so each spot has its own life
                ulong seed = (ulong)(Math.Abs(ux * 1e7) + Math.Abs(uy * 3e7) + Math.Abs(uz * 7e7)) + 17;
                _microbes = new DnaAnd.Sim.MicrobeWorld(seed);
                double R = _terrain.Radius + Math.Max(0, ground);
                _mx = ux * R; _my = uy * R; _mz = uz * R; _mEast = east; _mNorth = north; _mUp = up;
                _microbeVerts = new StarVertex[DnaAnd.Sim.MicrobeWorld.MaxMicrobes * 6 + DnaAnd.Sim.MicrobeWorld.MaxViruses + 16];
                if (_microbeBuf == null) _microbeBuf = new UploadBuffer(gpu.Device, (long)_microbeVerts.Length * Marshal.SizeOf(typeof(StarVertex)));
            }
            const double Um = 1e-9;                                                                 // one micrometre, in km
            int n = 0;
            float ox = (float)(_mx - cx), oy = (float)(_my - cy), oz = (float)(_mz - cz);
            foreach (var m in _microbes.Microbes)
            {
                if (n > _microbeVerts.Length - 6) break;
                double lx = (m.X - DnaAnd.Sim.MicrobeWorld.Width / 2) * Um, ly = (m.Y - DnaAnd.Sim.MicrobeWorld.Height / 2) * Um;
                var p = new Vec3(ox, oy, oz) + _mEast * (float)lx + _mNorth * (float)ly + _mUp * (float)(m.G.Size * 1.2 * Um);
                var g = m.G;
                _microbeVerts[n++] = SpaceGen.Star(p, (float)(g.Size * Um), g.R, g.G, g.B, 0.9f);
                if (g.Shape != 0)
                {
                    double a = Math.Atan2(m.Vy, m.Vx);
                    for (int k = -1; k <= 1; k += 2) _microbeVerts[n++] = SpaceGen.Star(p + (_mEast * (float)Math.Cos(a) + _mNorth * (float)Math.Sin(a)) * (float)(k * g.Size * 0.9 * Um), (float)(g.Size * 0.62 * Um), g.R, g.G, g.B, 0.9f);
                }
                if (m.Infected >= 0) _microbeVerts[n++] = SpaceGen.Star(p, (float)(g.Size * 1.35 * Um), 1f, 0.15f, 0.1f, 0.35f);
            }
            foreach (var v in _microbes.Viruses)
            {
                if (n >= _microbeVerts.Length) break;
                double lx = (v.X - DnaAnd.Sim.MicrobeWorld.Width / 2) * Um, ly = (v.Y - DnaAnd.Sim.MicrobeWorld.Height / 2) * Um;
                _microbeVerts[n++] = SpaceGen.Star(new Vec3(ox, oy, oz) + _mEast * (float)lx + _mNorth * (float)ly + _mUp * (float)(0.6 * Um), (float)(0.3 * Um), 0.95f, 0.9f, 0.85f, 0.8f);
            }
            if (n == 0) return;
            _microbeBuf.Write(_microbeVerts, n);
            Mat4 vp = view * proj;
            var c = new float[BillboardPipeline.ConstantCount];
            c[0] = vp.M11; c[1] = vp.M12; c[2] = vp.M13; c[3] = vp.M14; c[4] = vp.M21; c[5] = vp.M22; c[6] = vp.M23; c[7] = vp.M24;
            c[8] = vp.M31; c[9] = vp.M32; c[10] = vp.M33; c[11] = vp.M34; c[12] = vp.M41; c[13] = vp.M42; c[14] = vp.M43; c[15] = vp.M44;
            c[16] = view.M11; c[17] = view.M21; c[18] = view.M31; c[20] = view.M12; c[21] = view.M22; c[22] = view.M32;
            c[25] = 1; c[26] = 2; c[27] = 4; c[28] = width; c[29] = height;
            _dots.Draw(_microbeBuf, n, c, 0, 0, width, height);
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

        public void Dispose() { if (_microbeBuf != null) _microbeBuf.Dispose(); _dropBuf.Dispose(); if (_starBuf != null) _starBuf.Dispose(); if (_extraBuf != null) _extraBuf.Dispose(); }
    }
}
