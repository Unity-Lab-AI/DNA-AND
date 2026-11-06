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
        private readonly MeshPipes _meshes;
        private readonly ScaleHud _hud;
        // the planet or moon you have come close to: its real ground, drawn in the same camera (-1: none)
        private int _planet = -1; private PlanetTerrain _ground; private double _px, _py, _pz;   // its centre from the star, in universe units
        private const double PlanetRange = 40;                // within 40 of its radii, the ground is made and drawn
        private static readonly int Stride = Marshal.SizeOf(typeof(StarVertex));
        private const int FarStars = 2500, NearStars = 150000;
        private const double Spread = 120;                 // the universe patch, in galaxy units (one unit is 50,000 light-years)

        // the camera: an anchor (a galaxy's centre, or the star of the system you are in, in universe units) plus how far the camera
        // is from it. Distances near the camera stay exact because they are worked out from the small offset, not the huge anchor.
        private double _ax, _ay, _az, _ox, _oy, _oz, _yaw = Math.PI + 0.4, _pitch = -0.35;
        private double _cx { get { return _ax + _ox; } }
        private double _cy { get { return _ay + _oy; } }
        private double _cz { get { return _az + _oz; } }
        private void Anchor(double x, double y, double z)
        {
            // move the anchor without moving the camera: the offset takes up the difference (worked out before adding)
            _ox = (_ax - x) + _ox; _oy = (_ay - y) + _oy; _oz = (_az - z) + _oz; _ax = x; _ay = y; _az = z;
        }

        // the star system around you, once you are within about 2,000 AU of a star of the near galaxy
        private int _sysStar = -1; private SpaceGen.StarSystem _system; private int[] _nearKinds;
        private UploadBuffer _sysStatic, _sysBodies; private StarVertex[] _bodyVerts, _staticShifted; private int _bodyCount;
        private double _simYears;
        private StarSurface _starSurf; private StarVertex[] _starCorona, _starSurfVerts; private UploadBuffer _coronaBuf, _surfBuf;
        private static readonly double SystemRange = Units.Au(2000);
        // the galaxy whose own stars are being shown (or -1), and those stars in its own frame
        private int _near = -1; private StarVertex[] _nearLocal, _nearDrawn; private UploadBuffer _nearBuf; private float _nearFade = -1;
        private int[] _gridHead, _gridNext; private const int GridN = 64;     // a grid over the near galaxy's stars, to find the nearest one fast

        public WorldView(Gpu gpu, BillboardPipeline pipe, MeshPipes meshes, ContentLibrary content, ulong seed)
        {
            _meshes = meshes; _gpu = gpu; _hud = new ScaleHud(gpu.Device); _dots = new BillboardDrawer(gpu.CommandList, pipe); _content = content;
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
            _ox = 30; _oy = 14; _oz = 22;
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
            m.M41 = (float)((g.X - _ax) - _ox); m.M42 = (float)((g.Y - _ay) - _oy); m.M43 = (float)((g.Z - _az) - _oz);
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
            if (_system != null) best = Math.Min(best, PlaceSystem());
            if (_ground != null) best = Math.Min(best, AboveGround());
            if (_near >= 0 && _nearLocal != null) { double ds; if (NearestStar(out ds) >= 0) best = Math.Min(best, ds); }
            return best;
        }

        private static int Cell(double v) { return (int)Math.Floor((v + 1.6) / 3.2 * GridN); }

        // ── the stars of the space you are in ──
        // A real galaxy holds about a hundred billion stars; its sample shows 150,000. Around the camera, the real ones are made:
        // space is cut into cubes about 25 light-years across, and each cube near you gets the stars the galaxy's density gives it
        // there (from how many sample stars share its big grid cell), placed from the cube's own position, so they are always the
        // same stars when you come back.
        private const double RealStars = 1e11;
        private static readonly double LocalCell = 25 / 50000.0;           // 25 light-years, in galaxy units
        private readonly Dictionary<long, StarVertex[]> _localCells = new Dictionary<long, StarVertex[]>();
        private readonly List<StarVertex> _localList = new List<StarVertex>();   // in the near galaxy's own frame
        private readonly List<int> _localKind = new List<int>(); private readonly List<long> _localId = new List<long>();
        private UploadBuffer _localBuf; private StarVertex[] _localDrawn = new StarVertex[0]; private int _localCount;
        private long _localKey = long.MinValue;

        // the galaxy's density of real stars at a point of its own frame (stars per galaxy unit cubed)
        private double Density(double lx, double ly, double lz)
        {
            int x = Cell(lx), y = Cell(ly), z = Cell(lz);
            if (x < 0 || y < 0 || z < 0 || x >= GridN || y >= GridN || z >= GridN) return 0;
            int n = 0; for (int k = _gridHead[(x * GridN + y) * GridN + z]; k >= 0; k = _gridNext[k]) n++;
            double cellSize = 3.2 / GridN;
            return n / (double)NearStars * RealStars / (cellSize * cellSize * cellSize);
        }

        // refresh the local stars when the camera moves into another cube
        private void LocalStars()
        {
            if (_near < 0 || _nearLocal == null) { _localCount = 0; return; }
            double lx, ly, lz; ToGalaxy(_galaxies[_near], _cx, _cy, _cz, out lx, out ly, out lz);
            long cx = (long)Math.Floor(lx / LocalCell), cy = (long)Math.Floor(ly / LocalCell), cz = (long)Math.Floor(lz / LocalCell);
            long key = (cx * 73856093L) ^ (cy * 19349663L) ^ (cz * 83492791L);
            if (key == _localKey) return;
            _localKey = key;
            _localList.Clear(); _localKind.Clear(); _localId.Clear();
            var keep = new Dictionary<long, StarVertex[]>();
            for (long ox = -1; ox <= 1; ox++) for (long oy = -1; oy <= 1; oy++) for (long oz = -1; oz <= 1; oz++)
            {
                long x = cx + ox, y = cy + oy, z = cz + oz, id = (x * 73856093L) ^ (y * 19349663L) ^ (z * 83492791L);
                StarVertex[] cell;
                if (!_localCells.TryGetValue(id, out cell))
                {
                    double vol = LocalCell * LocalCell * LocalCell, expect = Density((x + 0.5) * LocalCell, (y + 0.5) * LocalCell, (z + 0.5) * LocalCell) * vol;
                    var rng = new Rng((ulong)id ^ _galaxies[_near].Seed);
                    int count = (int)Math.Min(4000, Math.Floor(expect + rng.NextDouble()));
                    cell = new StarVertex[count];
                    for (int k = 0; k < count; k++)
                    {
                        var kind = PickKind(rng);
                        var kc = _kinds[kind];
                        cell[k] = new StarVertex { X = (float)((x + rng.NextDouble()) * LocalCell), Y = (float)((y + rng.NextDouble()) * LocalCell), Z = (float)((z + rng.NextDouble()) * LocalCell),
                            Size = (float)Units.Km(696000), R = kc.R, G = kc.G, B = kc.B, A = kind };      // A carries the kind's index
                    }
                }
                keep[id] = cell;
                for (int k = 0; k < cell.Length; k++) { _localList.Add(cell[k]); _localKind.Add((int)cell[k].A); _localId.Add(id * 4096 + k); }
            }
            _localCells.Clear(); foreach (var kv in keep) _localCells[kv.Key] = kv.Value;
            _localCount = _localList.Count;
            if (_localDrawn.Length < _localCount) _localDrawn = new StarVertex[_localCount + 1024];
            if (_localBuf == null || _localBuf.Bytes < (long)_localCount * Stride) { if (_localBuf != null) _localBuf.Dispose(); _localBuf = new UploadBuffer(_gpu.Device, (long)(_localCount + 1024) * Stride * 2); }
        }

        // a star kind by how common each is (the same shares the galaxy generator uses)
        private int PickKind(Rng rng)
        {
            double total = 0; foreach (var k in _kinds) total += k.Weight;
            double r = rng.NextDouble() * total;
            for (int i = 0; i < _kinds.Count; i++) { r -= _kinds[i].Weight; if (r <= 0) return i; }
            return 0;
        }

        // the near galaxy's star nearest the camera (index, and distance in universe units), or -1
        private int NearestStar(out double dist)
        {
            dist = double.MaxValue; if (_near < 0 || _nearLocal == null) return -1;
            double lx, ly, lz; ToGalaxy(_galaxies[_near], _cx, _cy, _cz, out lx, out ly, out lz);
            // the real stars of this space (index from 1,000,000 up), then the galaxy's sample stars
            int bestL = -1; double bl = double.MaxValue;
            for (int k = 0; k < _localCount; k++) { var s0 = _localList[k]; double dx = s0.X - lx, dy = s0.Y - ly, dz = s0.Z - lz, d = dx * dx + dy * dy + dz * dz; if (d < bl) { bl = d; bestL = k; } }
            int gx = Cell(lx), gy = Cell(ly), gz = Cell(lz), best = -1; double bd = double.MaxValue;
            for (int ox = -1; ox <= 1; ox++) for (int oy = -1; oy <= 1; oy++) for (int oz = -1; oz <= 1; oz++)
            {
                int x = gx + ox, y = gy + oy, z = gz + oz;
                if (x < 0 || y < 0 || z < 0 || x >= GridN || y >= GridN || z >= GridN) continue;
                for (int k = _gridHead[(x * GridN + y) * GridN + z]; k >= 0; k = _gridNext[k])
                {
                    var s = _nearLocal[k]; double dx = s.X - lx, dy = s.Y - ly, dz = s.Z - lz, d = dx * dx + dy * dy + dz * dz;
                    if (d < bd) { bd = d; best = k; }
                }
            }
            if (bestL >= 0 && bl < bd) { dist = Math.Sqrt(bl) * _galaxies[_near].Scale; return 1000000 + bestL; }
            if (best >= 0) dist = Math.Sqrt(bd) * _galaxies[_near].Scale;
            return best;
        }

        // where a star of the near galaxy sits in the universe
        private void StarPos(int k, out double x, out double y, out double z) { var s = k >= 1000000 ? _localList[k - 1000000] : _nearLocal[k]; ToUniverse(_galaxies[_near], s.X, s.Y, s.Z, out x, out y, out z); }
        private int StarKindOf(int k) { return k >= 1000000 ? _localKind[k - 1000000] : _nearKinds[k]; }
        private ulong StarSeed(int k) { return k >= 1000000 ? (ulong)_localId[k - 1000000] : (ulong)k; }

        // open the system of the star you have come close to (and anchor the camera on it), or close it when you leave
        private void ChooseSystem()
        {
            double d; int k = NearestStar(out d);
            if (_system != null && (k != _sysStar && d < SystemRange * 0.5 || DistToSysStar() > SystemRange * 1.5)) CloseSystem();
            if (_system == null && k >= 0 && d < SystemRange)
            {
                _sysStar = k;
                var kind = _kinds[Math.Max(0, Math.Min(_kinds.Count - 1, StarKindOf(k)))];
                _system = SpaceGen.System(_content, kind, _galaxies[_near].Seed * 31UL + StarSeed(k));
                _bodyVerts = new StarVertex[_system.Bodies.Count + 1];
                _sysBodies = new UploadBuffer(_gpu.Device, (long)_bodyVerts.Length * Stride);
                _staticShifted = new StarVertex[_system.Static.Length];
                _sysStatic = new UploadBuffer(_gpu.Device, (long)Math.Max(1, _system.Static.Length) * Stride);
                double sx, sy, sz; StarPos(k, out sx, out sy, out sz); Anchor(sx, sy, sz);
                // the star's own body: its plasma surface and corona, in its kind's colours (as the star close-ups are made)
                string sname; _starCorona = StarGen.Build(_content, kind.Id, _galaxies[_near].Seed * 77UL + StarSeed(k), out sname, out _starSurf);
                _starSurfVerts = new StarVertex[_starSurf.Normals.Length];
                _coronaBuf = new UploadBuffer(_gpu.Device, (long)Math.Max(1, _starCorona.Length) * Stride); if (_starCorona.Length > 0) _coronaBuf.Write(_starCorona, _starCorona.Length);
                _surfBuf = new UploadBuffer(_gpu.Device, (long)_starSurfVerts.Length * Stride);
            }
            if (_system == null && _near >= 0) { var g = _galaxies[_near]; if (Math.Abs(_ax - g.X) + Math.Abs(_ay - g.Y) + Math.Abs(_az - g.Z) > 0) Anchor(g.X, g.Y, g.Z); }
        }

        private double DistToSysStar() { return Math.Sqrt(_ox * _ox + _oy * _oy + _oz * _oz); }    // anchored on the star: the offset is the distance

        private void CloseSystem()
        {
            DropPlanet();
            _system = null; _sysStar = -1;
            if (_sysStatic != null) { _sysStatic.Dispose(); _sysStatic = null; }
            if (_sysBodies != null) { _sysBodies.Dispose(); _sysBodies = null; }
            if (_coronaBuf != null) { _coronaBuf.Dispose(); _coronaBuf = null; }
            if (_surfBuf != null) { _surfBuf.Dispose(); _surfBuf = null; }
            _starSurf = null;
        }

        // a planet or moon's centre from its star, in double precision
        private void BodyPos(int i, out double x, out double y, out double z)
        {
            var tmp = new StarVertex[_system.Bodies.Count + 1]; double fx, fy, fz;
            SpaceGen.BodyVerts(_system, _simYears, 1 / 20.0, 0.05, tmp, i, 1.0, out fx, out fy, out fz);
            x = fx; y = fy; z = fz;
        }

        // the camera from the planet's centre, in km
        private void CamFromPlanet(out double x, out double y, out double z)
        {
            double k = Units.Km(1); x = (_ox - _px) / k; y = (_oy - _py) / k; z = (_oz - _pz) / k;
        }

        // make the ground of the body you have come close to, or let it go once you leave
        private void ChoosePlanet()
        {
            if (_system == null) { DropPlanet(); return; }
            int best = -1; double bd = double.MaxValue;
            for (int i = 0; i < _system.Bodies.Count; i++)
            {
                double x, y, z; BodyPos(i, out x, out y, out z);
                double d = Math.Sqrt((x - _ox) * (x - _ox) + (y - _oy) * (y - _oy) + (z - _oz) * (z - _oz)) / _system.Bodies[i].RadiusUnits;
                if (d < bd) { bd = d; best = i; }
            }
            if (bd > PlanetRange * 1.2) { DropPlanet(); return; }
            if (best != _planet && bd < PlanetRange)
            {
                DropPlanet();
                var b = _system.Bodies[best];
                _planet = best;
                _ground = new PlanetTerrain(_gpu.Device, DeepView.TerrainKind(b.Kind), b.Kind, (uint)(_galaxies[_near].Seed * 977UL + (ulong)_sysStar * 31UL + (ulong)best), false, b.RadiusUnits / Units.Km(1));
            }
            if (_planet >= 0)
            {
                // near a planet the camera rides along with it as it moves round its star, as anything near it would
                double ox = _px, oy = _py, oz = _pz; bool had = _hadPos;
                BodyPos(_planet, out _px, out _py, out _pz);
                if (had) { _ox += _px - ox; _oy += _py - oy; _oz += _pz - oz; }
                _hadPos = true;
            }
            else _hadPos = false;
        }

        private bool _hadPos;
        private void DropPlanet() { _hadPos = false; if (_ground != null) { _ground.Dispose(); _ground = null; } _planet = -1; }

        // how far above the planet's ground the camera is (universe units)
        private double AboveGround()
        {
            if (_ground == null) return double.MaxValue;
            double x, y, z; CamFromPlanet(out x, out y, out z);
            double r = Math.Sqrt(x * x + y * y + z * z); if (r <= 0) return 0;
            double g = _ground.Solid ? _ground.GroundAt(x / r, y / r, z / r) : 0;
            return Math.Max(1e-9, (r - _ground.Radius - Math.Max(0, g))) * Units.Km(1);
        }

        // the system's planets, moons and star placed around the star (anchor), and the nearest surface to the camera
        private double PlaceSystem()
        {
            if (_system == null) return double.MaxValue;
            double fx, fy, fz;
            _bodyCount = SpaceGen.BodyVerts(_system, _simYears, 1 / 20.0, 0.05, _bodyVerts, -1, 1.0, out fx, out fy, out fz);
            double best = double.MaxValue;
            for (int i = 0; i < _bodyCount; i++)
            {
                double r = i == 0 ? _system.StarRadius : _system.Bodies[i - 1].RadiusUnits;
                double dx = _bodyVerts[i].X - _ox, dy = _bodyVerts[i].Y - _oy, dz = _bodyVerts[i].Z - _oz;
                best = Math.Min(best, Math.Max(r * 0.05, Math.Sqrt(dx * dx + dy * dy + dz * dz) - r));
            }
            return best;
        }

        private static void ToGalaxy(Galaxy g, double ux, double uy, double uz, out double x, out double y, out double z)
        {
            double px = ux - g.X, py = uy - g.Y, pz = uz - g.Z;
            double ct = Math.Cos(g.Tilt), st = Math.Sin(g.Tilt), cu = Math.Cos(g.Turn), su = Math.Sin(g.Turn);
            double x1 = px * cu + pz * su, z1 = -px * su + pz * cu;                  // undo the turn
            double y2 = py * ct + z1 * st, z2 = -py * st + z1 * ct;                  // undo the tilt
            x = x1 / g.Scale; y = y2 / g.Scale; z = z2 / g.Scale;
        }

        /// <summary>Test runs: the camera turns to the next thing on the way down (the galaxy, a star, a planet, its ground) each frame.</summary>
        public bool AutoPath;
        private void AimNext()
        {
            double tx, ty, tz;
            if (_ground != null) { tx = _px; ty = _py; tz = _pz; }                                  // straight down to the ground
            else if (_system != null)
            {
                int pick = -1; for (int i = 0; i < _system.Bodies.Count; i++) if (_system.Bodies[i].Parent < 0 && (pick < 0 || _system.Bodies[i].Kind == "Temperate world")) pick = i;
                if (pick < 0) return;
                BodyPos(pick, out tx, out ty, out tz);
            }
            else if (_near >= 0 && _localCount > 0 && NearShare() > 0.9)
            {
                double d; int k = NearestStar(out d); if (k < 0) return;
                double ux, uy, uz; StarPos(k, out ux, out uy, out uz); tx = (ux - _ax); ty = (uy - _ay); tz = (uz - _az);
            }
            else { var g = _galaxies[0]; tx = g.X - _ax; ty = g.Y - _ay; tz = g.Z - _az; }
            double dx = tx - _ox, dy = ty - _oy, dz = tz - _oz, l = Math.Sqrt(dx * dx + dy * dy + dz * dz);
            if (l > 0) { _yaw = Math.Atan2(dz, dx); _pitch = Math.Asin(dy / l); }
        }

        public bool Update(Input input, double dt, int w, int h)
        {
            if (AutoPath) AimNext();
            if (input.WasPressed(0x48)) _hud.Visible = !_hud.Visible;                 // H: the scale on screen, on or off
            if (input.WasPressed(Win32.VK_ESCAPE)) return false;
            if (input.LeftDown) { _yaw += input.MouseDX * 0.004; _pitch = Math.Max(-1.55, Math.Min(1.55, _pitch - input.MouseDY * 0.004)); }
            double reach = NearestDistance();
            if (input.Wheel != 0)
            {
                // fly along the line under the mouse, by a share of the distance to the nearest thing: slower as it comes close, never ending
                var dir = MouseRay(input.MouseX, input.MouseY, w, h);
                // a star close to the line you point along (within about 4 degrees) is flown at straight, not past
                double tdist; var tdir = AimStar(dir, out tdist);
                if (input.Wheel > 0 && tdist > 0) { dir = tdir; reach = Math.Min(reach, tdist); }
                double step = reach * (1 - Math.Pow(0.87, Math.Abs(input.Wheel))) * Math.Sign(input.Wheel);
                if (input.Wheel < 0) step = -Math.Max(reach, 0.01) * (Math.Pow(1 / 0.87, -input.Wheel) - 1);
                _ox += dir.X * step; _oy += dir.Y * step; _oz += dir.Z * step;
                if (_ground != null)
                {
                    // never lower than a few steps of the finest ground already made under you (so you never see the rough
                    // first pieces stretched across the screen), and never below half a millimetre
                    double ready = _ground.ReadyStep < 1e30 ? _ground.ReadyStep * 3 : _ground.Radius;
                    double floorKm = Math.Max(5e-7, ready);
                    if (AboveGround() < Units.Km(floorKm))
                    {
                        double x, y, z; CamFromPlanet(out x, out y, out z);
                        double r = Math.Sqrt(x * x + y * y + z * z), g = _ground.Solid ? Math.Max(0, _ground.GroundAt(x / r, y / r, z / r)) : 0;
                        double want = _ground.Radius + g + floorKm, k = Units.Km(1);
                        _ox = _px + x / r * want * k; _oy = _py + y / r * want * k; _oz = _pz + z / r * want * k;
                    }
                }
            }
            _simYears += dt / 20.0;                                                    // one year in 20 seconds, as in the system view
            ChooseNear();
            LocalStars();
            ChooseSystem();
            ChoosePlanet();
            return true;
        }

        // the star nearest the line along dir (local stars, or the open system's star): its direction and distance (else 0)
        private Vec3 AimStar(Vec3 dir, out double dist)
        {
            dist = 0; var best = dir; double bestCos = Math.Cos(4 * Math.PI / 180);
            if (_system != null)
            {
                // in a system: the star or any planet or moon near the line you point along (the one nearest the line wins)
                PlaceSystem();
                for (int i = 0; i < _bodyCount; i++)
                {
                    double r = i == 0 ? _system.StarRadius : _system.Bodies[i - 1].RadiusUnits;
                    double dx = _bodyVerts[i].X - _ox, dy = _bodyVerts[i].Y - _oy, dz = _bodyVerts[i].Z - _oz, l = Math.Sqrt(dx * dx + dy * dy + dz * dz);
                    if (l <= 0) continue;
                    double c = (dx * dir.X + dy * dir.Y + dz * dir.Z) / l;
                    if (c > bestCos) { bestCos = c; best = new Vec3((float)(dx / l), (float)(dy / l), (float)(dz / l)); dist = Math.Max(l - r * 1.5, r * 0.05); }
                }
                return best;
            }
            if (_near < 0) return best;
            var g = _galaxies[_near];
            for (int k = 0; k < _localCount; k++)
            {
                var s0 = _localList[k]; double ux, uy, uz; ToUniverse(g, s0.X, s0.Y, s0.Z, out ux, out uy, out uz);
                double dx = (ux - _ax) - _ox, dy = (uy - _ay) - _oy, dz = (uz - _az) - _oz, l = Math.Sqrt(dx * dx + dy * dy + dz * dz);
                if (l <= 0) continue;
                double c = (dx * dir.X + dy * dir.Y + dz * dir.Z) / l;
                if (c > bestCos) { bestCos = c; best = new Vec3((float)(dx / l), (float)(dy / l), (float)(dz / l)); dist = l; }
            }
            return best;
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
                _near = best; _nearFade = -1; CloseSystem(); _localCells.Clear(); _localKey = long.MinValue; _localCount = 0;
                if (_nearBuf != null) { _nearBuf.Dispose(); _nearBuf = null; }
                _nearLocal = null;
                if (best >= 0)
                {
                    var g = _galaxies[best];
                    _nearKinds = new int[NearStars];
                    _nearLocal = GalaxyGen.Generate(g.Type, g.Seed, NearStars, _kinds, _nearKinds);
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
            if (_near >= 0 && _localCount > 0 && share > 0.5)
            {
                // the real stars of this space at their real sizes (a small point from afar, never smaller than a dot on screen)
                for (int k = 0; k < _localCount; k++) { var v = _localList[k]; v.A = 1; _localDrawn[k] = v; }
                _localBuf.Write(_localDrawn, _localCount);
                DrawPoints(_localBuf, _localCount, Placement(_galaxies[_near]) * vp, right, up, 1f, 0.0022f, w, h, time);
            }
            if (_system != null)
            {
                // the system around its star (the anchor): orbits and belts, then the star, planets and moons as glows at their real sizes
                _gpu.ClearDepth();
                PlaceSystem();
                var st = _system.Static;
                for (int i = 0; i < st.Length; i++) { var v = st[i]; v.X = (float)(st[i].X - _ox); v.Y = (float)(st[i].Y - _oy); v.Z = (float)(st[i].Z - _oz); _staticShifted[i] = v; }
                if (st.Length > 0) { _sysStatic.Write(_staticShifted, st.Length); DrawPoints(_sysStatic, st.Length, vp, right, up, 0f, 0.002f, w, h, time); }
                for (int i = 0; i < _bodyCount; i++) { _bodyVerts[i].X = (float)(_bodyVerts[i].X - _ox); _bodyVerts[i].Y = (float)(_bodyVerts[i].Y - _oy); _bodyVerts[i].Z = (float)(_bodyVerts[i].Z - _oz); }
                _sysBodies.Write(_bodyVerts, _bodyCount);
                DrawPoints(_sysBodies, _bodyCount, vp, right, up, 1f, 0.004f, w, h, time);
                // close enough to see its disc: the star's real body at its real size
                double R = _system.StarRadius, sd = Math.Sqrt(_ox * _ox + _oy * _oy + _oz * _oz);
                if (_starSurf != null && R / Math.Max(1e-30, sd) > 0.004)
                {
                    var toEye = new Vec3((float)(_ox / sd), (float)(_oy / sd), (float)(_oz / sd));
                    int n = _starSurf.Draw(toEye, time, _starSurfVerts, 0);
                    _surfBuf.Write(_starSurfVerts, n);
                    var m = Mat4.Identity; m.M11 = m.M22 = m.M33 = (float)R; m.M41 = (float)-_ox; m.M42 = (float)-_oy; m.M43 = (float)-_oz;
                    DrawPoints(_surfBuf, n, m * vp, right, up, 1f, 0f, w, h, time);
                    DrawPoints(_coronaBuf, _starCorona.Length, m * vp, right, up, 1f, 0f, w, h, time);
                }
            }
            if (_ground != null)
            {
                // the planet's real ground, in km around its centre, with the same view turn; near and far follow the height
                _gpu.ClearDepth();
                double x, y, z; CamFromPlanet(out x, out y, out z);
                double alt = Math.Max(1e-9, Math.Sqrt(x * x + y * y + z * z) - _ground.Radius);
                // the depth range starts a little in front of the ground and ends just past the horizon, so near things (the sea
                // and the shore just under it) never fight for the same depth and shimmer
                double horizon = Math.Sqrt(alt * (2 * _ground.Radius + alt)) + Math.Sqrt(2 * _ground.Radius * 30);
                Mat4 kproj = Mat4.Perspective(0.9f, w / (float)Math.Max(1, h), (float)Math.Max(alt * 0.15, 1e-9), (float)(horizon * 1.3 + 100));
                var sd = _system.StarRadius; double lx = -_px, ly = -_py, lz = -_pz, ll = Math.Sqrt(lx * lx + ly * ly + lz * lz);
                if (ll > 0) { _ground.StarDir[0] = lx / ll; _ground.StarDir[1] = ly / ll; _ground.StarDir[2] = lz / ll; }
                _ground.Draw(_meshes, x, y, z, view, kproj, time, 3.0 * Math.Max(1, h / 600.0), 0, 0, 0, 0, 0, 0, w, h, 400);
            }
            DrawScale(w, h);
        }

        // the scale on screen: how many km one pixel spans at the distance of the nearest thing, in the middle of the view
        private void DrawScale(int w, int h)
        {
            double d = Math.Max(1e-30, NearestDistance()) / Units.Km(1), viewKm = 2 * d * Math.Tan(0.45) * w / (double)Math.Max(1, h);
            _gpu.ClearDepth();
            _hud.Draw(_meshes, viewKm / Math.Max(1, w), w, h);
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
                string where = _ground != null ? "at " + _system.Bodies[_planet].Kind + ", " + Units.Describe(AboveGround()) + " above the ground"
                    : _system != null ? "in the system of a " + _system.StarName + " (" + _system.Bodies.Count + " planets and moons)"
                    : _near >= 0 ? (NearShare() > 0.5 ? "among the stars of " : "near ") + _galaxies[_near].Name : "among the galaxies";
                return "DNA-AND · one world · " + where + " · nearest thing " + Units.Describe(NearestDistance()) + " away · wheel: fly in or out toward the mouse · drag: look around · H: scale on or off · Esc: close";
            }
        }

        public void Dispose()
        {
            foreach (var g in _galaxies) g.Far.Dispose();
            if (_nearBuf != null) _nearBuf.Dispose();
            CloseSystem();
            DropPlanet();
            _hud.Dispose();
            if (_localBuf != null) _localBuf.Dispose();
        }
    }
}
