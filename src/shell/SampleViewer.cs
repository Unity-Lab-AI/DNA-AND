// DNA-AND · shell · the live sample viewer: 3 examples (rows) x 3 styles (columns), moving and rotatable.
// Set 1: galaxies. Styles: RimWorld-like (flat, top-down), Populous-like (low-poly, tilted 3D), Stardew-like (pixel).
using System;
using System.Collections.Generic;
using DnaAnd.Core;
using DnaAnd.Gen;
using DnaAnd.Platform;
using DnaAnd.Render;

namespace DnaAnd.Shell
{
    internal sealed class SampleViewer : IDisposable
    {
        private const int StarsPerGalaxy = 24000;
        private readonly string[] _types;
        private readonly string[] _typeNames;
        private readonly UploadBuffer[] _buffers = new UploadBuffer[3];
        private readonly BillboardDrawer _drawer;
        private float _yaw, _zoom = 1f;
        public int Set { get; private set; }
        private const int TrailPoints = 72;
        private static readonly string[] SystemLayouts = { "sun-like", "compact", "hot-jupiter" };
        private static readonly string[] SystemStars = { "g-type", "m-type", "g-type" };
        private readonly string[] _systemNames = new string[3];
        private readonly SpaceGen.StarSystem[] _systems = new SpaceGen.StarSystem[3];
        private readonly UploadBuffer[] _sysStatic = new UploadBuffer[3], _sysBodies = new UploadBuffer[3];
        private readonly StarVertex[][] _sysVerts = new StarVertex[3][];
        private readonly int[] _sysCount = new int[3];

        public string Title
        {
            get
            {
                if (Set == 2)
                    return "DNA-AND samples · Set 2: Solar systems · rows: " + string.Join(", ", _systemNames) +
                           " · columns: RimWorld-like, Populous-like, Stardew-like · [ and ] switch set, wheel to zoom, Esc to quit";
                return "DNA-AND samples · Set 1: Galaxies (locked: Populous-like) · rows: " + string.Join(", ", _typeNames) +
                       " · click a galaxy to zoom deep into it, [ and ] switch set, Esc to quit";
            }
        }

        public SampleViewer(Gpu gpu, BillboardPipeline pipeline, ContentLibrary content, ulong seed)
        {
            Set = 1;
            _drawer = new BillboardDrawer(gpu.CommandList, pipeline);
            // three galaxy types, taken from the content
            var wanted = new[] { "spiral", "barred-spiral", "elliptical" };
            var types = new List<string>(); var names = new List<string>();
            foreach (var e in content.OfKind("galaxies.dnat", "galaxy"))
                if (Array.IndexOf(wanted, e.Id) >= 0) { types.Add(e.Id); names.Add(e.Name); }
            while (types.Count < 3) { types.Add(wanted[types.Count]); names.Add(wanted[names.Count]); }
            _types = types.GetRange(0, 3).ToArray(); _typeNames = names.GetRange(0, 3).ToArray();

            List<StarKind> kinds = GalaxyGen.StarKinds(content);
            int stride = System.Runtime.InteropServices.Marshal.SizeOf(typeof(StarVertex));
            for (int row = 0; row < 3; row++)
            {
                StarVertex[] stars = GalaxyGen.Generate(_types[row], seed + (ulong)row * 7919UL, StarsPerGalaxy, kinds);
                _buffers[row] = new UploadBuffer(gpu.Device, (long)stars.Length * stride);
                _buffers[row].Write(stars, stars.Length);
            }

            // set 2: three kinds of solar system, from content/solar-systems.dnat and content/stars.dnat
            for (int row = 0; row < 3; row++)
            {
                string want = SystemStars[row];
                StarKind kind = kinds.Find(k => k.Id == want) ?? kinds[0];
                _systems[row] = SpaceGen.System(content, kind, seed * 13UL + (ulong)row, SystemLayouts[row]);
                _systemNames[row] = SystemLayouts[row];
                foreach (var e in content.OfKind("solar-systems.dnat", "system")) if (e.Id == SystemLayouts[row]) _systemNames[row] = e.Name;
                _sysStatic[row] = new UploadBuffer(gpu.Device, (long)Math.Max(1, _systems[row].Static.Length) * stride);
                _sysStatic[row].Write(_systems[row].Static, _systems[row].Static.Length);
                _sysVerts[row] = new StarVertex[(_systems[row].Bodies.Count + 1) * (1 + TrailPoints)];
                _sysBodies[row] = new UploadBuffer(gpu.Device, (long)_sysVerts[row].Length * stride);
            }
        }

        public void ShowSet(int set) { Set = set; }
        public int RowAt(int y, int height) { return Math.Max(0, Math.Min(2, y / Math.Max(1, height / 3))); }
        public string TypeOf(int row) { return _types[row]; }
        public string NameOf(int row) { return _typeNames[row]; }

        public void Update(Input input, double dt)
        {
            if (input.WasPressed(0xDB)) Set = 1;   // [ key
            if (input.WasPressed(0xDD)) Set = 2;   // ] key
            _yaw += (float)dt * 0.12f;
            if (input.Wheel != 0) _zoom = Math.Max(0.3f, Math.Min(4f, _zoom * (float)Math.Pow(1.15, input.Wheel)));
        }

        public void Draw(int width, int height, double time)
        {
            if (Set == 2) { DrawSystems(width, height, time); return; }
            int tileW = width / 3, tileH = height / 3;
            for (int row = 0; row < 3; row++)
                for (int col = 0; col < 3; col++)
                {
                    int x = col * tileW, y = row * tileH;
                    float pitch = col == 1 ? 0.55f : 1.45f;                // Populous-like is tilted 3D; the others look down
                    float dist = 2.3f / _zoom;
                    var eye = new Vec3((float)(Math.Cos(_yaw) * Math.Cos(pitch)) * dist, (float)Math.Sin(pitch) * dist, (float)(Math.Sin(_yaw) * Math.Cos(pitch)) * dist);
                    Mat4 view = Mat4.LookAt(eye, new Vec3(0, 0, 0), new Vec3(0, 1, 0));
                    Mat4 proj = Mat4.Perspective(0.9f, tileW / (float)Math.Max(1, tileH), 0.01f, 50f);
                    Mat4 vp = view * proj;
                    var right = new Vec3(view.M11, view.M21, view.M31);
                    var up = new Vec3(view.M12, view.M22, view.M32);
                    float style = col + 1;                                  // 1 flat, 2 low-poly, 3 pixel
                    float sizeScale = col == 2 ? 0.010f : col == 1 ? 0.008f : 0.0055f;
                    var c = new float[BillboardPipeline.ConstantCount];
                    Pack(c, vp);
                    c[16] = right.X; c[17] = right.Y; c[18] = right.Z;
                    c[20] = up.X; c[21] = up.Y; c[22] = up.Z;
                    c[24] = (float)time; c[25] = sizeScale / _zoom * 1.0f; c[26] = style; c[27] = 4f;
                    c[28] = tileW; c[29] = tileH;
                    _drawer.Draw(_buffers[row], StarsPerGalaxy, c, x, y, tileW, tileH);
                }
        }

        private void DrawSystems(int width, int height, double time)
        {
            int tileW = width / 3, tileH = height / 3;
            for (int row = 0; row < 3; row++)
            {
                var sys = _systems[row];
                double outer = 0; foreach (var b in sys.Bodies) if (b.Parent < 0) outer = Math.Max(outer, b.OrbitUnits);
                _sysCount[row] = SpaceGen.BodyVerts(sys, time / 20.0, 1 / 20.0, 0.5, _sysVerts[row], TrailPoints);
                _sysBodies[row].Write(_sysVerts[row], _sysCount[row]);
                for (int col = 0; col < 3; col++)
                {
                    float pitch = col == 1 ? 0.55f : 1.45f;
                    double dist = outer * 2.6 / _zoom;
                    var eye = new Vec3((float)(Math.Cos(_yaw) * Math.Cos(pitch) * dist), (float)(Math.Sin(pitch) * dist), (float)(Math.Sin(_yaw) * Math.Cos(pitch) * dist));
                    Mat4 view = Mat4.LookAt(eye, new Vec3(0, 0, 0), new Vec3(0, 1, 0));
                    Mat4 vp = view * Mat4.Perspective(0.9f, tileW / (float)Math.Max(1, tileH), (float)(dist * 1e-3), (float)(dist * 1e3));
                    var right = new Vec3(view.M11, view.M21, view.M31); var up = new Vec3(view.M12, view.M22, view.M32);
                    float style = col + 1;
                    DrawTile(_sysStatic[row], sys.Static.Length, vp, right, up, 0f, 0.0025f, style, col * tileW, row * tileH, tileW, tileH, time);
                    DrawTile(_sysBodies[row], _sysCount[row], vp, right, up, 1f, col == 2 ? 0.012f : 0.008f, style, col * tileW, row * tileH, tileW, tileH, time);
                }
            }
        }

        private void DrawTile(UploadBuffer buf, int count, Mat4 vp, Vec3 right, Vec3 up, float sizeScale, float minSize, float style, int x, int y, int w, int h, double time)
        {
            if (count <= 0) return;
            var c = new float[BillboardPipeline.ConstantCount];
            Pack(c, vp);
            c[16] = right.X; c[17] = right.Y; c[18] = right.Z; c[20] = up.X; c[21] = up.Y; c[22] = up.Z;
            c[24] = (float)time; c[25] = sizeScale; c[26] = style; c[27] = 4f; c[28] = w; c[29] = h; c[30] = minSize;
            _drawer.Draw(buf, count, c, x, y, w, h);
        }

        private static void Pack(float[] c, Mat4 m)
        {
            c[0] = m.M11; c[1] = m.M12; c[2] = m.M13; c[3] = m.M14; c[4] = m.M21; c[5] = m.M22; c[6] = m.M23; c[7] = m.M24;
            c[8] = m.M31; c[9] = m.M32; c[10] = m.M33; c[11] = m.M34; c[12] = m.M41; c[13] = m.M42; c[14] = m.M43; c[15] = m.M44;
        }

        public void Dispose()
        {
            foreach (var b in _buffers) if (b != null) b.Dispose();
            foreach (var b in _sysStatic) if (b != null) b.Dispose();
            foreach (var b in _sysBodies) if (b != null) b.Dispose();
        }
    }
}
