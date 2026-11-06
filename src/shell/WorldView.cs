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
        private sealed class Galaxy { public string Type, Name; public ulong Seed; public double X, Y, Z; public float Tilt, Turn, Scale; public UploadBuffer Far; public int FarCount; public StarVertex[] FarPts; public int[] FarKinds; }
        private readonly List<Galaxy> _galaxies = new List<Galaxy>();
        private readonly Gpu _gpu; private readonly BillboardDrawer _dots; private readonly ContentLibrary _content;
        private readonly List<StarKind> _kinds;
        private readonly MeshPipes _meshes;
        private readonly ScaleHud _hud;
        private readonly MatterView _matter; private double _matterScale;     // below the ground's finest piece: how big the view is (km), 0 = not in the matter
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
        private int _near = -1; private StarVertex[] _nearLocal, _nearDrawn; private UploadBuffer _nearBuf;
        private int[] _gridHead, _gridNext; private const int GridN = 64;     // a grid over the near galaxy's stars, to find the nearest one fast

        public WorldView(Gpu gpu, BillboardPipeline pipe, MeshPipes meshes, ContentLibrary content, ulong seed)
        {
            _meshes = meshes; _gpu = gpu; _hud = new ScaleHud(gpu.Device); _matter = new MatterView(gpu, pipe); _dots = new BillboardDrawer(gpu.CommandList, pipe); _content = content;
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
                var fk = new int[FarStars];
                var stars = GalaxyGen.Generate(g.Type, g.Seed, FarStars, _kinds, fk);
                g.Far = new UploadBuffer(gpu.Device, (long)stars.Length * Stride); g.Far.Write(stars, stars.Length); g.FarCount = stars.Length; g.FarPts = stars; g.FarKinds = fk;
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
            if (_matterScale > 0) return Units.Km(_matterScale) * 0.5;
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
            if (_system != null && k >= 0 && k != _sysStar)
            {
                // the same star can come back under a new number as the stars around you are made again: it is another
                // star only if it sits somewhere else (more than an AU from this system's star, the anchor)
                double sx0, sy0, sz0; StarPos(k, out sx0, out sy0, out sz0);
                if (Math.Abs(sx0 - _ax) + Math.Abs(sy0 - _ay) + Math.Abs(sz0 - _az) < Units.Au(1)) _sysStar = k;
            }
            if (_system != null && (k != _sysStar && d < SystemRange * 0.5 || DistToSysStar() > SystemRange * 1.5))
            {
                ZoomLog("SYSTEM CLOSED: " + (DistToSysStar() > SystemRange * 1.5 ? "left it, " + Units.Describe(DistToSysStar()) + " from its star" : "another star " + k + " (was " + _sysStar + ") within " + Units.Describe(d)));
                CloseSystem();
            }
            if (_system == null && k >= 0 && d < SystemRange)
            {
                _sysStar = k; ZoomLog("SYSTEM OPENED: star " + k + " at " + Units.Describe(d));
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
            DropPlanet(); _hadPos = false; _followBody = -1;                       // a new system's bodies are new: ride along afresh
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
            _cpBest = best; _cpBd = bd;
            if (bd > PlanetRange * 1.2) { DropPlanet(); return; }
            if (best != _planet && bd < PlanetRange)
            {
                DropPlanet();
                var b = _system.Bodies[best];
                _planet = best;
                _ground = new PlanetTerrain(_gpu.Device, DeepView.TerrainKind(b.Kind), b.Kind, (uint)(_galaxies[_near].Seed * 977UL + (ulong)_sysStar * 31UL + (ulong)best), false, b.RadiusUnits / Units.Km(1));
            }
            if (_planet >= 0) BodyPos(_planet, out _px, out _py, out _pz);
        }

        // the camera rides along with the planet or moon you picked (or are at) as it goes round its star
        private void FollowBody()
        {
            int follow = _tKind == 2 && _system != null && _tIndex < _system.Bodies.Count ? _tIndex : _planet;
            if (follow < 0 && _system != null)
            {
                // nothing picked: the nearest planet or moon is matched once you are within about 300 of its radii
                double bd = double.MaxValue;
                for (int i = 0; i < _system.Bodies.Count; i++)
                {
                    double x, y, z; BodyPos(i, out x, out y, out z);
                    double d = Math.Sqrt((x - _ox) * (x - _ox) + (y - _oy) * (y - _oy) + (z - _oz) * (z - _oz)) / _system.Bodies[i].RadiusUnits;
                    if (d < bd) { bd = d; follow = i; }
                }
                if (bd > 300) follow = -1;
            }
            if (follow >= 0)
            {
                double fx, fy, fz; BodyPos(follow, out fx, out fy, out fz);
                if (_hadPos && follow == _followBody) { _ox += fx - _fbx; _oy += fy - _fby; _oz += fz - _fbz; }
                _fbx = fx; _fby = fy; _fbz = fz; _followBody = follow; _hadPos = true;
            }
            else { _hadPos = false; _followBody = -1; }
        }
        private int _followBody = -1; private double _fbx, _fby, _fbz;

        private bool _hadPos;
        private int _cpBest = -1; private double _cpBd;                         // for the log: the nearest body in its own radii
        private void DropPlanet() { if (_ground != null) { _ground.Dispose(); _ground = null; } _planet = -1; }

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
        public bool AutoPath; private double _autoLogT;
        // ── what you clicked: the camera turns to face it and the wheel flies to it ──
        private int _tKind = -1;                 // 0 a galaxy, 1 a star, 2 a planet or moon, 3 a spot on the planet's ground
        private int _tIndex; private double _tx, _ty, _tz, _tRadius;   // galaxy or body index; a star's place (universe units) or a ground spot (unit direction)
        private string _tName = "";
        private bool _pressed, _dragged; private int _pressX, _pressY;
        private Mat4 _lastVp; private int _lastW = 1, _lastH = 1; private bool _haveVp;
        /// <summary>Set when something the menu shows has changed (the scale's tick), so the menu is rebuilt.</summary>
        public bool MenuChanged;
        /// <summary>Whether the scale shows on screen (H, or the View menu).</summary>
        public bool ScaleOn { get { return _hud.Visible; } set { _hud.Visible = value; } }
        private void AimNext()
        {
            double tx, ty, tz;
            if (_ground != null) { tx = _px; ty = _py; tz = _pz; }                                  // straight down to the ground
            else if (_system != null)
            {
                int pick = -1; for (int i = 0; i < _system.Bodies.Count; i++) if (_system.Bodies[i].Parent < 0 && (pick < 0 || _system.Bodies[i].Kind == "Temperate world")) pick = i;
                if (pick < 0) return;
                BodyPos(pick, out tx, out ty, out tz);
                // come in over its day side: a point a few radii out toward the star
                double l0 = Math.Sqrt(tx * tx + ty * ty + tz * tz), rr = _system.Bodies[pick].RadiusUnits * 4;
                if (l0 > 0) { tx -= tx / l0 * rr; ty -= ty / l0 * rr; tz -= tz / l0 * rr; }
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

        // where the clicked thing is now, from the camera (universe units), and how big it is; false if it is gone
        private bool TargetFromCamera(out double dx, out double dy, out double dz, out double radius)
        {
            dx = dy = dz = 0; radius = 0;
            if (_tKind == 0) { var g = _galaxies[_tIndex]; dx = (g.X - _ax) - _ox; dy = (g.Y - _ay) - _oy; dz = (g.Z - _az) - _oz; radius = g.Scale * 0.3; return true; }
            if (_tKind == 1) { dx = (_tx - _ax) - _ox; dy = (_ty - _ay) - _oy; dz = (_tz - _az) - _oz; radius = _tRadius; return true; }
            if (_tKind == 2 && _system != null && _tIndex < _system.Bodies.Count)
            { double x, y, z; BodyPos(_tIndex, out x, out y, out z); dx = x - _ox; dy = y - _oy; dz = z - _oz; radius = _system.Bodies[_tIndex].RadiusUnits; return true; }
            if (_tKind == 3 && _ground != null)
            {
                double g = _ground.Solid ? Math.Max(0, _ground.GroundAt(_tx, _ty, _tz)) : 0, r = (_ground.Radius + g) * Units.Km(1);
                dx = _px + _tx * r - _ox; dy = _py + _ty * r - _oy; dz = _pz + _tz * r - _oz; radius = 0; return true;
            }
            return false;
        }

        // where a point (relative to the camera, universe units) lands on screen, in pixels; false if behind you
        private bool ToScreen(double dx, double dy, double dz, out double sx, out double sy)
        {
            sx = sy = 0; if (!_haveVp) return false;
            var m = _lastVp; float x = (float)dx, y = (float)dy, z = (float)dz;
            float cx = x * m.M11 + y * m.M21 + z * m.M31 + m.M41, cy = x * m.M12 + y * m.M22 + z * m.M32 + m.M42, cw = x * m.M14 + y * m.M24 + z * m.M34 + m.M44;
            if (cw <= 0) return false;
            sx = (cx / cw * 0.5 + 0.5) * _lastW; sy = (0.5 - cy / cw * 0.5) * _lastH; return true;
        }

        // pick what is under the mouse: the planet's ground if you are at one, else the nearest planet or moon, star or galaxy to the line
        private void Pick(Vec3 dir, int mx, int my)
        {
            double reachPx = 24 * Math.Max(1, _lastH / 1080.0), best = reachPx * reachPx; int kind = -1, idx = 0; double tx = 0, ty = 0, tz = 0, trad = 0; string name = "";
            Func<double, double, double, double> near = (dx, dy, dz) => { double sx, sy; if (!ToScreen(dx, dy, dz, out sx, out sy)) return double.MaxValue; return (sx - mx) * (sx - mx) + (sy - my) * (sy - my); };
            if (_ground != null)
            {
                // the ground: where the line under the mouse meets the planet
                double x, y, z; CamFromPlanet(out x, out y, out z);
                double R = _ground.Radius, b = x * dir.X + y * dir.Y + z * dir.Z, c = x * x + y * y + z * z - R * R, disc = b * b - c;
                if (disc >= 0)
                {
                    double t = -b - Math.Sqrt(disc);
                    if (t > 0) { double hx = x + dir.X * t, hy = y + dir.Y * t, hz = z + dir.Z * t, l = Math.Sqrt(hx * hx + hy * hy + hz * hz); _tKind = 3; _tx = hx / l; _ty = hy / l; _tz = hz / l; _tName = "a spot on " + _system.Bodies[_planet].Kind; return; }
                }
            }
            if (_system != null)
            {
                PlaceSystem();
                for (int i = 1; i < _bodyCount; i++)
                {
                    double d2 = near(_bodyVerts[i].X - _ox, _bodyVerts[i].Y - _oy, _bodyVerts[i].Z - _oz);
                    if (d2 < best) { best = d2; kind = 2; idx = i - 1; name = _system.Bodies[i - 1].Kind; }
                }
            }
            if (_near >= 0 && kind < 0)
            {
                var g = _galaxies[_near];
                for (int k = 0; k < _localCount; k++)
                {
                    var s0 = _localList[k]; double ux, uy, uz; ToUniverse(g, s0.X, s0.Y, s0.Z, out ux, out uy, out uz);
                    double d2 = near((ux - _ax) - _ox, (uy - _ay) - _oy, (uz - _az) - _oz);
                    if (d2 < best) { best = d2; kind = 1; tx = ux; ty = uy; tz = uz; trad = Units.Km(696000); name = "a " + _kinds[_localKind[k]].Name; }
                }
                if (kind < 0 && NearShare() > 0.5)
                    for (int k = 0; k < _nearLocal.Length; k++)
                    {
                        var s0 = _nearLocal[k]; double ux, uy, uz; ToUniverse(g, s0.X, s0.Y, s0.Z, out ux, out uy, out uz);
                        double d2 = near((ux - _ax) - _ox, (uy - _ay) - _oy, (uz - _az) - _oz);
                        if (d2 < best) { best = d2; kind = 1; tx = ux; ty = uy; tz = uz; trad = Units.Km(696000); name = "a " + _kinds[_nearKinds[k]].Name; }
                    }
            }
            if (kind < 0)
                for (int i = 0; i < _galaxies.Count; i++)
                {
                    var g = _galaxies[i]; double dx = (g.X - _ax) - _ox, dy = (g.Y - _ay) - _oy, dz = (g.Z - _az) - _oz, l = Math.Sqrt(dx * dx + dy * dy + dz * dz);
                    double sizePx = g.Scale / Math.Max(1e-9, l) / Math.Tan(0.45) * _lastH * 0.5;   // its size on screen
                    double d2 = near(dx, dy, dz), allow = Math.Max(reachPx, sizePx);
                    if (d2 < allow * allow && (kind < 0 || d2 < best)) { best = d2; kind = 0; idx = i; name = g.Name; }
                }
            if (kind < 0) return;
            _tKind = kind; _tIndex = idx; _tx = tx; _ty = ty; _tz = tz; _tRadius = trad; _tName = name;
        }

        public bool Update(Input input, double dt, int w, int h)
        {
            Mark();
            // a click (pressed and let go without dragging) picks what is under the mouse
            if (input.LeftClicked) { _pressed = true; _dragged = false; _pressX = input.MouseX; _pressY = input.MouseY; }
            if (_pressed && input.LeftDown && Math.Abs(input.MouseX - _pressX) + Math.Abs(input.MouseY - _pressY) > 6) _dragged = true;   // dragging circles round the pick (or looks around if nothing is picked)
            if (_pressed && !input.LeftDown) { _pressed = false; if (!_dragged) Pick(MouseRay(input.MouseX, input.MouseY, w, h), input.MouseX, input.MouseY); }
            // the camera turns smoothly to face what you picked
            double tdx, tdy, tdz, trad;
            if (_tKind >= 0 && !(_pressed && _dragged) && TargetFromCamera(out tdx, out tdy, out tdz, out trad))
            {
                double l = Math.Sqrt(tdx * tdx + tdy * tdy + tdz * tdz);
                if (l > 0 && _tKind == 2 && _tIndex == _planet && _ground != null)
                {
                    // coming down to the picked planet: from a tenth of its radius the view levels out from its middle toward
                    // its horizon (about 15 degrees below it), keeping the way you face, so you see the land ahead, not your feet
                    double alt = AboveGround() / Units.Km(1), lev = Math.Max(0, Math.Min(1, 1 - alt / (_ground.Radius * 0.1)));
                    lev = lev * lev * (3 - 2 * lev);
                    double ux = -tdx / l, uy = -tdy / l, uz = -tdz / l;                 // up: from the planet's middle to you
                    var lk0 = Look; double dot = lk0.X * ux + lk0.Y * uy + lk0.Z * uz;
                    double hx = lk0.X - ux * dot, hy = lk0.Y - uy * dot, hz = lk0.Z - uz * dot, hl = Math.Sqrt(hx * hx + hy * hy + hz * hz);
                    if (hl < 1e-6) { hx = -uz; hy = 0; hz = ux; hl = Math.Sqrt(hx * hx + hz * hz); if (hl < 1e-6) { hx = 1; hl = 1; } }
                    double down = Math.Sin(15 * Math.PI / 180), flat = Math.Cos(15 * Math.PI / 180);
                    double wx = hx / hl * flat - ux * down, wy = hy / hl * flat - uy * down, wz = hz / hl * flat - uz * down;
                    tdx = tdx / l * (1 - lev) + wx * lev; tdy = tdy / l * (1 - lev) + wy * lev; tdz = tdz / l * (1 - lev) + wz * lev;
                    l = Math.Sqrt(tdx * tdx + tdy * tdy + tdz * tdz);
                }
                if (l > 0)
                {
                    double wantYaw = Math.Atan2(tdz, tdx), wantPitch = Math.Asin(Math.Max(-1, Math.Min(1, tdy / l)));
                    double dyaw = Math.IEEERemainder(wantYaw - _yaw, 2 * Math.PI), k = 1 - Math.Exp(-dt * 5);
                    _yaw += dyaw * k; _pitch += (wantPitch - _pitch) * k;
                }
            }
            if (AutoPath)
            {
                // the test path: pick a planet as a click would, roll the wheel in, and write where the camera is each second
                if (_system != null && _tKind != 2 && _ground == null)
                {
                    int pick = -1; for (int i = 0; i < _system.Bodies.Count; i++) if (_system.Bodies[i].Parent < 0 && (pick < 0 || _system.Bodies[i].Kind == "Temperate world")) pick = i;
                    if (pick >= 0) { _tKind = 2; _tIndex = pick; _tName = _system.Bodies[pick].Kind; }
                }
                if (_tKind != 2) AimNext();
                input.Wheel += 1;
                _autoLogT += dt;
                if (_autoLogT > 1) { _autoLogT = 0; try { System.IO.File.AppendAllText(System.IO.Path.Combine(System.IO.Path.GetTempPath(), "dna_autopath.log"), Title + Environment.NewLine); } catch { } }
            }
            if (input.RightClicked) { _tKind = -1; _tName = ""; }                    // right click: let go of what you picked
            if (input.WasPressed(0x48)) { _hud.Visible = !_hud.Visible; MenuChanged = true; }   // H: the scale on screen, on or off
            if (input.WasPressed(Win32.VK_ESCAPE)) return false;
            if (input.LeftDown)
            {
                // dragging looks around; by default the view moves the other way to the mouse (like pulling the sky), both ways
                // can be flipped in the View menu
                double sx = Settings.Get("invert-left-right", false) ? 1 : -1, sy = Settings.Get("invert-up-down", false) ? -1 : 1;
                double odx, ody, odz, orad;
                if (_tKind >= 0 && _tKind != 3 && _dragged && TargetFromCamera(out odx, out ody, out odz, out orad) && odx * odx + ody * ody + odz * odz > 0)
                {
                    // with something picked: circle round it at the distance you are at, always facing it
                    // (see a solar system from above, a galaxy edge-on, a planet from any side)
                    double r = Math.Sqrt(odx * odx + ody * ody + odz * odz);
                    double ox = -odx / r, oy = -ody / r, oz = -odz / r;                // from the pick to the camera
                    double th = Math.Atan2(oz, ox) - sx * input.MouseDX * 0.006, ph = Math.Asin(Math.Max(-1, Math.Min(1, oy)));
                    ph = Math.Max(-1.55, Math.Min(1.55, ph + sy * input.MouseDY * 0.006));
                    double nx = Math.Cos(ph) * Math.Cos(th) * r, ny = Math.Sin(ph) * r, nz = Math.Cos(ph) * Math.Sin(th) * r;
                    _ox += nx + odx; _oy += ny + ody; _oz += nz + odz;                  // the camera moves to its new place round the pick
                    _yaw = Math.Atan2(-nz, -nx); _pitch = Math.Asin(Math.Max(-1, Math.Min(1, -ny / r)));
                }
                else if (_ground != null)
                {
                    // near a planet: look around its own up, so turning stays level with its horizon
                    var u = ViewUp(); var f = Look;
                    f = Turn(f, u, -sx * input.MouseDX * 0.004);              // the same way round as looking around in space
                    var rt = Vec3.Cross(f, u).Normalized; var f2 = Turn(f, rt, sy * input.MouseDY * 0.004);
                    if (Math.Abs(Vec3.Dot(f2, u)) < 0.995f) f = f2;                   // never quite straight up or down
                    _yaw = Math.Atan2(f.Z, f.X); _pitch = Math.Asin(Math.Max(-1, Math.Min(1, f.Y)));
                }
                else { _yaw += sx * input.MouseDX * 0.004; _pitch = Math.Max(-1.55, Math.Min(1.55, _pitch + sy * input.MouseDY * 0.004)); }
            }
            Jumped("dragging");
            double reach = NearestDistance();
            if (_matterScale <= 0)
            {
                // W A S D, Q and E: fly like a first-person game, at a speed scaled to the nearest thing (half its distance a
                // second, Shift four times faster), so it feels the same among galaxies as a metre above the ground
                double mf = (input.IsDown(0x57) ? 1 : 0) - (input.IsDown(0x53) ? 1 : 0), mr = (input.IsDown(0x44) ? 1 : 0) - (input.IsDown(0x41) ? 1 : 0), mu = (input.IsDown(0x45) ? 1 : 0) - (input.IsDown(0x51) ? 1 : 0);
                if (mf != 0 || mr != 0 || mu != 0)
                {
                    _tKind = -1;                                                       // flying yourself lets go of the pick
                    var f = Look; var rt = _haveAxes ? _viewRight : Vec3.Cross(f, ViewUp()).Normalized; var upv = _haveAxes ? _viewUp : Vec3.Cross(rt, f).Normalized;
                    double sp = reach * 0.5 * dt * (input.IsDown(0x10) ? 4 : 1);
                    _ox += (f.X * mf + rt.X * mr + upv.X * mu) * sp; _oy += (f.Y * mf + rt.Y * mr + upv.Y * mu) * sp; _oz += (f.Z * mf + rt.Z * mr + upv.Z * mu) * sp;
                    double fk = _ground != null ? Math.Max(5e-7, _ground.ReadyStep < 1e30 ? _ground.ReadyStep * 0.6 : 0) : 0;
                    if (_ground != null && AboveGround() < Units.Km(fk))
                    {
                        // never through the ground, nor into its drawn pieces: back up above them
                        double x, y, z; CamFromPlanet(out x, out y, out z);
                        double r = Math.Sqrt(x * x + y * y + z * z), g = _ground.Solid ? Math.Max(0, _ground.GroundAt(x / r, y / r, z / r)) : 0;
                        double want = _ground.Radius + g + fk, k = Units.Km(1);
                        _ox = _px + x / r * want * k; _oy = _py + y / r * want * k; _oz = _pz + z / r * want * k;
                    }
                }
            }
            Jumped("W A S D Q E");
            if (_matterScale > 0)
            {
                // in the ground's matter: the wheel shrinks or grows the scale, from a millimetre down inside an atom
                if (input.Wheel != 0) _matterScale *= Math.Pow(0.87, input.Wheel);
                if (_matterScale > MatterView.TopKm) _matterScale = 0;                 // back up to the ground
                else _matterScale = Math.Max(MatterView.BottomKm, _matterScale);
                ChooseNear(); LocalStars(); ChooseSystem(); FollowBody(); ChoosePlanet();
                return true;
            }
            if (_ground != null && input.Wheel > 0 && AboveGround() < Units.Km(1.5e-6))
            {
                // at the finest ground: on down into what it is made of
                double x, y, z; CamFromPlanet(out x, out y, out z); double r = Math.Sqrt(x * x + y * y + z * z);
                bool sea = _ground.HasWater && _ground.GroundAt(x / r, y / r, z / r) < 0, ice = Math.Abs(y / r) > 0.86;
                _matter.Set(_ground.Kind, sea, ice, sea ? 0.2f : 0.58f, sea ? 0.4f : 0.5f, sea ? 0.6f : 0.4f);
                _matterScale = MatterView.TopKm;
                return true;
            }
            if (input.Wheel != 0)
            {
                // fly along the line under the mouse, by a share of the distance to the nearest thing: slower as it comes close, never ending
                var dir = MouseRay(input.MouseX, input.MouseY, w, h);
                double fdx, fdy, fdz, frad;
                if (_tKind >= 0 && TargetFromCamera(out fdx, out fdy, out fdz, out frad))
                {
                    // flying to what you picked: straight at it, a share of the way to its surface each notch
                    double fl = Math.Sqrt(fdx * fdx + fdy * fdy + fdz * fdz);
                    // the steps go by the distance to what you picked only: stars you fly past no longer hold them back
                    if (fl > 0)
                    {
                        dir = new Vec3((float)(fdx / fl), (float)(fdy / fl), (float)(fdz / fl));
                        // at the picked planet's ground, the steps go by the height above the ground, all the way down;
                        // from afar, by the distance to just outside it
                        bool atIt = _tKind == 2 && _tIndex == _planet && _ground != null;
                        reach = atIt ? AboveGround() : Math.Max(fl - frad * 1.2, fl * 1e-6);
                    }
                }
                // a star close to the line you point along (within about 4 degrees) is flown at straight, not past
                if (_tKind < 0) { double tdist; var tdir = AimStar(dir, out tdist); if (input.Wheel > 0 && tdist > 0) { dir = tdir; reach = Math.Min(reach, tdist); } }
                double step = reach * (1 - Math.Pow(0.87, Math.Abs(input.Wheel))) * Math.Sign(input.Wheel);
                if (input.Wheel < 0) step = -Math.Max(reach, Units.Km(1e-9)) * (Math.Pow(1 / 0.87, -input.Wheel) - 1);   // out from right at the ground: a tiny step, never light-years
                double altBefore = _ground != null ? AboveGround() / Units.Km(1) : 0;
                ZoomLog("wheel " + input.Wheel.ToString("0") + " step " + Units.Describe(Math.Abs(step)) + " reach " + Units.Describe(reach) + " pick " + _tKind + ":" + _tIndex + " planet " + _planet + " nearest-body " + _cpBest + " at " + _cpBd.ToString("0.##") + " radii follow " + _followBody + (_ground != null ? " ground-ready " + (_ground.ReadyStep < 1e30 ? _ground.ReadyStep.ToString("0.###") + " km" : "none") : ""));
                _ox += dir.X * step; _oy += dir.Y * step; _oz += dir.Z * step;
                if (_ground != null)
                {
                    // slow down near the finest ground already made under you (so the rough first pieces are not stretched
                    // across the screen), but never push you back and always let each notch close in a tenth, so the finer
                    // ground is asked for and catches up; never below half a millimetre
                    double ready = _ground.ReadyStep < 1e30 ? _ground.ReadyStep * 3 : 0;
                    // ...but never inside the drawn ground: its pieces are flat triangles about ReadyStep across that can stand above
                    // the true ground, so stay at least 0.6 of a step above it until finer pieces are made (close enough to ask for them)
                    double floorKm = Math.Max(5e-7, Math.Min(ready, Math.Max(altBefore * 0.9, ready / 3 * 0.6)));
                    {
                        // over the sea: no lower than a metre above the water, until the dive under the sea is in the one world
                        double cx0, cy0, cz0; CamFromPlanet(out cx0, out cy0, out cz0); double r0 = Math.Sqrt(cx0 * cx0 + cy0 * cy0 + cz0 * cz0);
                        // over the sea the floor is the water itself, not the sea bed far below (whose pieces stay coarse): down to a millimetre
                        if (_ground.HasWater && _ground.GroundAt(cx0 / r0, cy0 / r0, cz0 / r0) < 0) floorKm = 5e-7;
                    }
                    if (AboveGround() < Units.Km(floorKm))
                    {
                        double x, y, z; CamFromPlanet(out x, out y, out z);
                        double r = Math.Sqrt(x * x + y * y + z * z), g = _ground.Solid ? Math.Max(0, _ground.GroundAt(x / r, y / r, z / r)) : 0;
                        double want = _ground.Radius + g + floorKm, k = Units.Km(1);
                        _ox = _px + x / r * want * k; _oy = _py + y / r * want * k; _oz = _pz + z / r * want * k;
                    }
                }
            }
            Jumped("the wheel");
            _simYears += dt / 20.0;                                                    // one year in 20 seconds, as in the system view
            ChooseNear(); Jumped("ChooseNear");
            LocalStars(); Jumped("LocalStars");
            ChooseSystem(); Jumped("ChooseSystem");
            // ride along with the body first, then see which one you are at: planets move about a hundred thousand km a frame,
            // so choosing before riding along saw you many radii behind (a moon never got its ground, and flickered)
            FollowBody(); Mark();                                                // riding along is meant to move you far: not logged as a jump
            ChoosePlanet(); Jumped("ChoosePlanet");
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

        // v turned by angle a (radians) round the unit axis k
        private static Vec3 Turn(Vec3 v, Vec3 k, double a)
        {
            float c = (float)Math.Cos(a), sn = (float)Math.Sin(a), d = Vec3.Dot(k, v); var x = Vec3.Cross(k, v);
            return new Vec3(v.X * c + x.X * sn + k.X * d * (1 - c), v.Y * c + x.Y * sn + k.Y * d * (1 - c), v.Z * c + x.Z * sn + k.Z * d * (1 - c)).Normalized;
        }

        // the view's up: the universe's up in space; near a planet's ground, the planet's own up (so its horizon is level)
        private Vec3 ViewUp()
        {
            var wu = new Vec3(0, 1, 0);
            if (_ground == null) return wu;
            double x, y, z; CamFromPlanet(out x, out y, out z); double r = Math.Sqrt(x * x + y * y + z * z); if (r <= 0) return wu;
            double alt = r - _ground.Radius, k = Math.Max(0, Math.Min(1, 1 - alt / (_ground.Radius * 0.1)));
            var lu = new Vec3((float)(x / r), (float)(y / r), (float)(z / r));
            var u = new Vec3(wu.X * (float)(1 - k) + lu.X * (float)k, wu.Y * (float)(1 - k) + lu.Y * (float)k, wu.Z * (float)(1 - k) + lu.Z * (float)k);
            var lk = Look; if (Math.Abs(Vec3.Dot(u.Normalized, lk)) > 0.995f) return Math.Abs(lk.Y) < 0.9f ? wu : new Vec3(1, 0, 0);
            return u.Normalized;
        }

        private Vec3 _viewRight, _viewUp; private bool _haveAxes;
        private Vec3 MouseRay(int mx, int my, int w, int h)
        {
            double nx = mx / (double)Math.Max(1, w) * 2 - 1, ny = 1 - my / (double)Math.Max(1, h) * 2, t = Math.Tan(0.45), aspect = w / (double)Math.Max(1, h);
            // the screen's own right and up, as the view was last drawn (so the line goes exactly through the pixel clicked)
            var f = Look; var right = _haveAxes ? _viewRight : Vec3.Cross(f, ViewUp()).Normalized; var up = _haveAxes ? _viewUp : Vec3.Cross(right, f).Normalized;
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
                _near = best; CloseSystem(); _localCells.Clear(); _localKey = long.MinValue; _localCount = 0;
                if (_nearBuf != null) { _nearBuf.Dispose(); _nearBuf = null; }
                _nearLocal = null;
                if (best >= 0)
                {
                    var g = _galaxies[best];
                    // the same stars as its far glow, at once; the rest of its stars are made in the background and added after them
                    _nearKinds = (int[])g.FarKinds.Clone();
                    _nearLocal = (StarVertex[])g.FarPts.Clone();
                    _nearDrawn = _nearLocal;
                    _nearBuf = new UploadBuffer(_gpu.Device, (long)NearStars * Stride); _nearBuf.Write(_nearLocal, _nearLocal.Length);
                    int forGalaxy = best; var gt = g.Type; var gs = g.Seed ^ 0xE47A5UL;
                    Jobs.Run(() =>
                    {
                        var ek = new int[NearStars - FarStars];
                        var extra = GalaxyGen.Generate(gt, gs, NearStars - FarStars, _kinds, ek);
                        lock (_extraLock) { _extraFor = forGalaxy; _extra = extra; _extraKinds = ek; }
                    });
                    BuildGrid();
                }
            }
            // the rest of the near galaxy's stars, when they are ready: added after the ones already shown (nothing changes on screen)
            StarVertex[] ex = null; int[] exk = null;
            lock (_extraLock) { if (_extra != null && _extraFor == _near) { ex = _extra; exk = _extraKinds; } if (_extra != null) { _extra = null; _extraKinds = null; } }
            if (ex != null && _nearLocal != null && _nearLocal.Length == FarStars)
            {
                var all = new StarVertex[FarStars + ex.Length]; Array.Copy(_nearLocal, all, FarStars); Array.Copy(ex, 0, all, FarStars, ex.Length);
                var allK = new int[all.Length]; Array.Copy(_nearKinds, allK, FarStars); Array.Copy(exk, 0, allK, FarStars, exk.Length);
                _nearLocal = all; _nearKinds = allK; _nearDrawn = all; _nearBuf.Write(all, all.Length);
                BuildGrid();
            }
        }

        private readonly object _extraLock = new object(); private StarVertex[] _extra; private int[] _extraKinds; private int _extraFor = -1;

        // the grid used to find the nearest star of the near galaxy
        private void BuildGrid()
        {
            {
                {
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

        // how far into its own stars the near galaxy is (0: only the stars of its far glow, 1: all of its stars), from how close you are
        private double NearShare()
        {
            if (_near < 0) return 0;
            var g = _galaxies[_near]; double dx = g.X - _cx, dy = g.Y - _cy, dz = g.Z - _cz, d = Math.Sqrt(dx * dx + dy * dy + dz * dz) / g.Scale;
            double t = (8 - d) / 6.5; t = Math.Max(0, Math.Min(1, t)); return t * t * (3 - 2 * t);
        }

        // how deep into the near galaxy you are (0 outside, 1 well inside), smoothly
        private double InsideShare()
        {
            if (_near < 0) return 0;
            var gn = _galaxies[_near]; double dx = gn.X - _cx, dy = gn.Y - _cy, dz = gn.Z - _cz, dg = Math.Sqrt(dx * dx + dy * dy + dz * dz) / gn.Scale;
            return Math.Max(0, Math.Min(1, 1.6 - dg));
        }

        // how many of a galaxy's stars to draw: more as it grows on screen, each smaller, so its glow stays the same overall
        private int StarsShown(int i)
        {
            if (i != _near || _nearLocal == null) return _galaxies[i].FarCount;
            return (int)Math.Min(_nearLocal.Length, FarStars + (NearStars - FarStars) * NearShare());
        }

        public void Draw(int w, int h, double time)
        {
            var look = Look;
            Mat4 view = Mat4.LookAt(new Vec3(0, 0, 0), look, ViewUp());
            double reach = Math.Max(1e-12, NearestDistance());
            Mat4 proj = Mat4.Perspective(0.9f, w / (float)Math.Max(1, h), (float)(reach * 1e-3), (float)(Spread * 6));
            Mat4 vp = view * proj;
            _lastVp = vp; _lastW = w; _lastH = h; _haveVp = true;
            var right = new Vec3(view.M11, view.M21, view.M31); var up = new Vec3(view.M12, view.M22, view.M32);
            _viewRight = right; _viewUp = up; _haveAxes = true;
            double share = NearShare();
            for (int i = 0; i < _galaxies.Count; i++)
            {
                // every galaxy is one field of its own stars: the near one shows more of them as it grows on screen, each point
                // a little smaller so the galaxy's glow stays the same; the first stars are always the same ones, so nothing swaps
                var g = _galaxies[i]; int count = StarsShown(i);
                float shrink = (float)Math.Sqrt(FarStars / (double)Math.Max(FarStars, count));
                var buf = i == _near && _nearBuf != null ? _nearBuf : g.Far;
                // inside the galaxy its points ease down from glows to plain points of light, so no nearby one looms as a blob
                float glow = i == _near ? (float)Math.Pow(1 - InsideShare(), 3) : 1f;   // to nothing at all well inside: only points of light remain
                // a galaxy's points never cover more than a small angle on screen: from afar they glow, up close they stay points of light
                DrawPoints(buf, count, Placement(g) * vp, right, up, 0.006f * g.Scale * shrink * glow, 0.0016f, w, h, time, 0.012f);
            }
            double inside = InsideShare();
            if (_near >= 0 && _localCount > 0 && inside > 0)
            {
                // the real stars of this space at their real sizes (a small point from afar, never smaller than a dot on screen)
                float lf = (float)(inside * inside * (3 - 2 * inside));
                for (int k = 0; k < _localCount; k++) { var v = _localList[k]; v.A = 1; v.R *= lf; v.G *= lf; v.B *= lf; _localDrawn[k] = v; }
                _localBuf.Write(_localDrawn, _localCount);
                DrawPoints(_localBuf, _localCount, Placement(_galaxies[_near]) * vp, right, up, 1f, 0.0022f, w, h, time);
            }
            if (_system != null)
            {
                // the system around its star (the anchor): orbits and belts, then the star, planets and moons as glows at their real sizes
                _gpu.ClearDepth();
                PlaceSystem();
                var st = _system.Static;
                float stFade = (float)Math.Max(0, Math.Min(1, (SystemRange - DistToSysStar()) / (SystemRange * 0.75)));   // orbits and belts fade in with the planets
                for (int i = 0; i < st.Length; i++) { var v = st[i]; v.X = (float)(st[i].X - _ox); v.Y = (float)(st[i].Y - _oy); v.Z = (float)(st[i].Z - _oz); v.R *= stFade; v.G *= stFade; v.B *= stFade; _staticShifted[i] = v; }
                if (st.Length > 0) { _sysStatic.Write(_staticShifted, st.Length); DrawPoints(_sysStatic, st.Length, vp, right, up, 0f, 0.002f, w, h, time); }
                for (int i = 0; i < _bodyCount; i++) { _bodyVerts[i].X = (float)(_bodyVerts[i].X - _ox); _bodyVerts[i].Y = (float)(_bodyVerts[i].Y - _oy); _bodyVerts[i].Z = (float)(_bodyVerts[i].Z - _oz); }
                // the planet whose real ground is showing is not drawn as a glow too (it showed through its own see-through sea and clouds as a frosted ghost)
                // the system's planets and moons fade in as you close in on its star (from 2,000 AU to 500 AU), never all at once
                double sysFade = Math.Max(0, Math.Min(1, (SystemRange - DistToSysStar()) / (SystemRange * 0.75)));
                for (int i = 1; i < _bodyCount; i++) { _bodyVerts[i].R *= (float)sysFade; _bodyVerts[i].G *= (float)sysFade; _bodyVerts[i].B *= (float)sysFade; }
                if (_ground != null && _planet + 1 < _bodyCount) { _bodyVerts[_planet + 1].Size = 0; _bodyVerts[_planet + 1].R = _bodyVerts[_planet + 1].G = _bodyVerts[_planet + 1].B = 0; }
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
            if (_matterScale > 0)
            {
                // the matter, in front of everything; above a tenth of a millimetre the ground still shows through behind it
                if (_matterScale < 1e-7) _gpu.ClearNow(0, 0, 0);                    // deep in the matter: only the matter is in view
                _gpu.ClearDepth();
                // the matter lies under the planet's own up and down: turn the view into that frame (up = away from the planet's middle)
                double x, y, z; CamFromPlanet(out x, out y, out z); double r = Math.Sqrt(x * x + y * y + z * z);
                var upL = new Vec3((float)(x / r), (float)(y / r), (float)(z / r));
                var east = Vec3.Cross(Math.Abs(upL.Y) < 0.99f ? new Vec3(0, 1, 0) : new Vec3(1, 0, 0), upL).Normalized; var north = Vec3.Cross(upL, east).Normalized;
                var lk = new Vec3(Vec3.Dot(look, east), Vec3.Dot(look, upL), Vec3.Dot(look, north));
                if (lk.Y > -0.2f) lk = new Vec3(lk.X, -0.2f, lk.Z);               // always looking down into it at least a little
                Mat4 mview = Mat4.LookAt(new Vec3(0, 0, 0), lk.Normalized, Math.Abs(lk.Normalized.Y) > 0.98f ? new Vec3(0, 0, 1) : new Vec3(0, 1, 0));
                _matter.Draw(mview, _matterScale, w, h, time);
            }
            DrawScale(w, h);
            double mdx, mdy, mdz, mrad, msx, msy;
            if (_tKind >= 0 && _matterScale <= 0 && TargetFromCamera(out mdx, out mdy, out mdz, out mrad) && ToScreen(mdx, mdy, mdz, out msx, out msy))
            {
                double l = Math.Sqrt(mdx * mdx + mdy * mdy + mdz * mdz);
                double rpx = Math.Max(14 * Math.Max(1, h / 1080.0), mrad / Math.Max(1e-30, l) / Math.Tan(0.45) * h * 0.5 * 1.25);
                bool fills = rpx > h * 0.45;                                           // bigger than the view: no ring, the card sits top left
                if (!fills) _hud.DrawRing(_meshes, msx, msy, rpx, w, h);
                if (_hud.Visible) { if (fills) _hud.DrawCard(_meshes, Card(), 24 * Math.Max(1, h / 1080.0), 24 * Math.Max(1, h / 1080.0), w, h); else _hud.DrawCard(_meshes, Card(), msx + rpx + 12 * Math.Max(1, h / 1080.0), msy - rpx, w, h); }
            }
        }

        // the scale on screen: how many km one pixel spans at the distance of the nearest thing, in the middle of the view
        private void DrawScale(int w, int h)
        {
            double d = Math.Max(1e-30, NearestDistance()) / Units.Km(1), viewKm = 2 * d * Math.Tan(0.45) * w / (double)Math.Max(1, h);
            _gpu.ClearDepth();
            _hud.Draw(_meshes, viewKm / Math.Max(1, w), w, h);
        }

        private void DrawPoints(UploadBuffer buf, int count, Mat4 vp, Vec3 right, Vec3 up, float sizeScale, float minSize, int w, int h, double time, float maxSize = 0)
        {
            if (buf == null || count <= 0) return;
            var c = new float[BillboardPipeline.ConstantCount];
            c[0] = vp.M11; c[1] = vp.M12; c[2] = vp.M13; c[3] = vp.M14; c[4] = vp.M21; c[5] = vp.M22; c[6] = vp.M23; c[7] = vp.M24;
            c[8] = vp.M31; c[9] = vp.M32; c[10] = vp.M33; c[11] = vp.M34; c[12] = vp.M41; c[13] = vp.M42; c[14] = vp.M43; c[15] = vp.M44;
            c[16] = right.X; c[17] = right.Y; c[18] = right.Z; c[20] = up.X; c[21] = up.Y; c[22] = up.Z;
            c[24] = (float)time; c[25] = sizeScale; c[26] = 2; c[27] = 4; c[28] = w; c[29] = h; c[30] = minSize; c[31] = maxSize;
            _dots.Draw(buf, count, c, 0, 0, w, h);
        }

        // ── the information card of what you picked: what it is, its id, and the facts that matter about it ──
        private static string Au(double units) { double au = units / Units.Au(1); return au >= 10 ? au.ToString("0") + " AU" : au.ToString("0.##") + " AU"; }
        private static string Km(double units) { double km = units / Units.Km(1); return km >= 10 ? km.ToString("N0") + " km" : km.ToString("0.##") + " km"; }

        // what a kind of planet or moon is made of
        private static string[] MakeUp(string kind, bool water)
        {
            switch (kind)
            {
                case "Temperate world": return new[] { "rock: silicate crust, iron core", water ? "seas of liquid water" : "dry basins", "air: nitrogen, carbon dioxide, water vapour" };
                case "Lava planet": return new[] { "rock: molten silicate seas", "iron core", "air: thin rock vapour and sulfur" };
                case "Ice world": return new[] { "ice: frozen water, carbon dioxide, nitrogen", "rock and iron beneath", "air: thin, or none" };
                case "Gas giant": return new[] { "hydrogen and helium", "clouds: ammonia, water", "core: rock and ice" };
                case "Ice giant": return new[] { "water, ammonia and methane ices", "air: hydrogen, helium, methane", "core: rock" };
                case "Hot Jupiter": return new[] { "hydrogen and helium, swollen by heat", "clouds: silicates and iron", "core: rock" };
                case "Moon": return new[] { "rock and dust, iron-poor", "no air", "craters from strikes" };
                default: return new[] { "rock: silicate crust, iron core", "dust and stone", "air: thin carbon dioxide" };
            }
        }

        // a short log of each turn of the wheel (where you are and why the step is what it is), in the temp folder, to find
        // where a zoom stops; kept small (started over past about 200 KB)
        // for the log: a camera jump of over a million km in one step of the update says which step did it
        private double _mx, _my, _mz;
        private void Mark() { _mx = _cx; _my = _cy; _mz = _cz; }
        private void Jumped(string step)
        {
            double dx = _cx - _mx, dy = _cy - _my, dz = _cz - _mz, d = Math.Sqrt(dx * dx + dy * dy + dz * dz);
            if (d > Units.Km(1e6) || double.IsNaN(d)) ZoomLog("JUMP " + Units.Describe(d) + " in " + step);
            Mark();
        }

        private void ZoomLog(string what)
        {
            try
            {
                string f = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "dna_zoom.log");
                if (System.IO.File.Exists(f) && new System.IO.FileInfo(f).Length > 200000) System.IO.File.Delete(f);
                System.IO.File.AppendAllText(f, DateTime.Now.ToString("HH:mm:ss.f") + "  " + what + "  |  " + Title.Replace("DNA-AND · one world · ", "").Split(new[] { " · wheel:" }, StringSplitOptions.None)[0] + Environment.NewLine);
            }
            catch { }
        }

        private List<string> Card()
        {
            var L = new List<string>();
            double dx, dy, dz, rad; if (!TargetFromCamera(out dx, out dy, out dz, out rad)) return L;
            double dist = Math.Max(0, Math.Sqrt(dx * dx + dy * dy + dz * dz) - rad);
            if (_tKind == 0)
            {
                var g = _galaxies[_tIndex];
                L.Add(g.Name); L.Add("id: G-" + (g.Seed & 0xFFFFFF).ToString("X6"));
                L.Add("type: " + g.Type); L.Add("width: about " + Units.Describe(g.Scale));
                L.Add("stars: about 100 billion"); L.Add("distance: " + Units.Describe(dist));
            }
            else if (_tKind == 1)
            {
                bool open = _system != null && Math.Abs(_tx - _ax) + Math.Abs(_ty - _ay) + Math.Abs(_tz - _az) < Units.Km(1e6);
                L.Add(_tName.StartsWith("a ") ? _tName.Substring(2) : _tName);
                L.Add("id: S-" + (((long)(_tx / Units.Km(1e9)) * 73856093L ^ (long)(_ty / Units.Km(1e9)) * 19349663L ^ (long)(_tz / Units.Km(1e9)) * 83492791L) & 0xFFFFFF).ToString("X6"));
                if (open)
                {
                    var sy = _system;
                    L.Add("surface: " + sy.TempK.ToString("N0") + " K"); L.Add("mass: " + sy.MassSol.ToString("0.##") + " suns, width " + sy.RadiusSol.ToString("0.##") + " suns");
                    L.Add("brightness: " + sy.Luminosity.ToString(sy.Luminosity < 0.1 ? "0.###" : "0.##") + " suns");
                    L.Add("planets and moons: " + sy.Bodies.Count + " (" + sy.Planets + " planets)");
                    L.Add("habitable zone: " + (sy.HabitableAu * 0.75).ToString("0.##") + " to " + (sy.HabitableAu * 1.4).ToString("0.##") + " AU");
                    L.Add("frost line: " + sy.FrostLineAu.ToString("0.##") + " AU");
                }
                else L.Add("its system opens within 2,000 AU");
                L.Add("distance: " + Units.Describe(dist));
            }
            else if (_tKind == 2)
            {
                var sy = _system; var b = sy.Bodies[_tIndex];
                int moons = 0; for (int i = 0; i < sy.Bodies.Count; i++) if (sy.Bodies[i].Parent == _tIndex) moons++;
                L.Add(b.Kind + (b.Parent >= 0 ? " of " + sy.Bodies[b.Parent].Kind : ""));
                L.Add("id: S-" + _sysStar.ToString("X") + (b.Parent >= 0 ? "-P" + (b.Parent + 1) + "-M" : "-P") + (_tIndex + 1));
                L.Add("width: " + Km(b.RadiusUnits * 2));
                var top = b.Parent >= 0 ? sy.Bodies[b.Parent] : b;
                if (b.Parent < 0)
                {
                    double au = b.OrbitUnits / Units.Au(1);
                    L.Add("orbit: " + Au(b.OrbitUnits) + ", year " + (b.Period >= 1 ? b.Period.ToString("0.#") + " years" : (b.Period * 365.25).ToString("0") + " days"));
                    L.Add(au >= sy.HabitableAu * 0.75 && au <= sy.HabitableAu * 1.4 ? "in the habitable zone" : au < sy.HabitableAu * 0.75 ? "inside the habitable zone: too hot" : "outside the habitable zone: too cold");
                    L.Add("moons: " + moons);
                }
                else L.Add("orbit: " + Km(b.OrbitUnits) + " from its planet");
                bool here = _ground != null && _planet == _tIndex;
                foreach (var m in MakeUp(b.Kind, here ? _ground.HasWater : b.Kind == "Temperate world")) L.Add(m);
                if (here) { L.Add("air: " + (_ground.AirKm > 0 ? "about " + _ground.AirKm.ToString("0") + " km deep" : "none") + (_ground.HasClouds ? ", clouds" : "")); }
                L.Add("distance: " + Units.Describe(dist));
            }
            else if (_tKind == 3 && _ground != null)
            {
                double gh = _ground.Solid ? _ground.GroundAt(_tx, _ty, _tz) : 0;
                bool sea = _ground.HasWater && gh < 0, ice = Math.Abs(_ty) > 0.86;
                L.Add(sea ? "sea" : ice ? "ice" : "land"); 
                L.Add("on: " + _system.Bodies[_planet].Kind);
                L.Add("place: " + (Math.Asin(_ty) * 180 / Math.PI).ToString("0.#") + " lat, " + (Math.Atan2(_tz, _tx) * 180 / Math.PI).ToString("0.#") + " long");
                L.Add(sea ? "depth: " + (-gh * 1000).ToString("N0") + " m of water" : "height: " + (gh * 1000).ToString("N0") + " m above the sea");
                L.Add(sea ? "water over sand and mud" : ice ? "ice and packed snow" : gh > 3 ? "bare rock and scree" : gh > 1 ? "rock and thin soil" : "soil, sand and stone");
                L.Add("distance: " + Units.Describe(dist));
            }
            return L;
        }

        public string Title
        {
            get
            {
                double pdx, pdy, pdz, prad; string picked = _tKind >= 0 && TargetFromCamera(out pdx, out pdy, out pdz, out prad) ? " · going to " + _tName + ", " + Units.Describe(Math.Max(0, Math.Sqrt(pdx * pdx + pdy * pdy + pdz * pdz) - prad)) + " away" : " · click something to go to it";
                string where = _matterScale > 0 ? "inside the ground of " + _system.Bodies[_planet].Kind + ": " + _matter.Stage(_matterScale)
                    : _ground != null ? "at " + _system.Bodies[_planet].Kind + ", " + Units.Describe(AboveGround()) + " above the ground"
                    : _system != null ? "in the system of a " + _system.StarName + " (" + _system.Bodies.Count + " planets and moons)"
                    : _near >= 0 ? (NearShare() > 0.5 ? "among the stars of " : "near ") + _galaxies[_near].Name : "among the galaxies";
                return "DNA-AND · one world · " + where + picked + " · nearest thing " + Units.Describe(NearestDistance()) + " away · wheel: fly in or out toward the mouse · drag: circle the pick or look around · right click: clear the pick · W A S D Q E: fly · H: scale on or off · Esc: close";
            }
        }

        public void Dispose()
        {
            foreach (var g in _galaxies) g.Far.Dispose();
            if (_nearBuf != null) _nearBuf.Dispose();
            CloseSystem();
            DropPlanet();
            _hud.Dispose(); _matter.Dispose();
            if (_localBuf != null) _localBuf.Dispose();
        }
    }
}
