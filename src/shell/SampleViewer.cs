// DNA-AND · shell · the live sample viewer: 3 examples (rows) x 3 styles (columns), moving and rotatable.
// Set 1: galaxies. Styles: RimWorld-like (flat, top-down), Populous-like (low-poly, tilted 3D), Stardew-like (pixel).
using System;
using System.Runtime.InteropServices;
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
        private readonly bool[,] _marks = new bool[3, 9];   // [set, row*3+col]: the samples marked good
        private static readonly string[] StyleNames = { "RimWorld-like", "Populous-like", "Stardew-like" };
        private const int FrameDots = 48;
        private UploadBuffer _frameBuf;
        private readonly StarVertex[] _frameVerts = new StarVertex[9 * 4 * FrameDots];
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
                           " · columns: RimWorld-like, Populous-like, Stardew-like · right-click marks a good one · [ and ] switch set";
                return "DNA-AND samples · Set 1: Galaxies (locked: Populous-like) · rows: " + string.Join(", ", _typeNames) +
                       " · click a galaxy to zoom deep into it, right-click marks a good one, [ and ] switch set";
            }
        }

        public SampleViewer(Gpu gpu, BillboardPipeline pipeline, ContentLibrary content, ulong seed)
        {
            Set = 1;
            _frameBuf = new UploadBuffer(gpu.Device, (long)_frameVerts.Length * Marshal.SizeOf(typeof(StarVertex)));
            LoadMarks();
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

        public bool OneSet; // opened for a single set: [ and ] do nothing
        public void ShowSet(int set) { Set = set; OneSet = true; }
        public int RowAt(int y, int height) { return Math.Max(0, Math.Min(2, y / Math.Max(1, height / 3))); }
        public string TypeOf(int row) { return _types[row]; }
        public string NameOf(int row) { return _typeNames[row]; }

        public void Update(Input input, double dt)
        {
            if (!OneSet && input.WasPressed(0xDB)) Set = 1;   // [ key
            if (!OneSet && input.WasPressed(0xDD)) Set = 2;   // ] key
            _yaw += (float)dt * 0.03f;
            if (input.Wheel != 0) _zoom = Math.Max(0.3f, Math.Min(4f, _zoom * (float)Math.Pow(1.15, input.Wheel)));
        }

        public void Draw(int width, int height, double time)
        {
            if (Set == 2) { DrawSystems(width, height, time); DrawMarks(width, height); return; }
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
                _sysCount[row] = SpaceGen.BodyVerts(sys, time / 200.0, 1 / 200.0, 0.05, _sysVerts[row], TrailPoints);
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

        /// <summary>Right-click: marks or unmarks the sample under the mouse as a good one, and saves the marks.</summary>
        public void ToggleMark(int x, int y, int width, int height)
        {
            int col = Math.Max(0, Math.Min(2, x / Math.Max(1, width / 3)));
            int i = RowAt(y, height) * 3 + col;
            _marks[Set, i] = !_marks[Set, i];
            SaveMarks();
        }

        public void ToggleMark(int i) { _marks[Set, i] = !_marks[Set, i]; SaveMarks(); }

        public string[] KeeperLabels()
        {
            var l = new string[9];
            for (int i = 0; i < 9; i++) l[i] = "Row " + (i / 3 + 1) + ": " + RowName(Set, i / 3) + "  ·  column " + (i % 3 + 1) + ": " + StyleNames[i % 3];
            return l;
        }

        public bool[] KeeperTicks() { var t = new bool[9]; for (int i = 0; i < 9; i++) t[i] = _marks[Set, i]; return t; }

        public string Legend()
        {
            string what = Set == 2 ? "Set 2 of 17: solar systems" : "Set 1 of 17: galaxies (locked: column 2, Populous-like)";
            var sb = new System.Text.StringBuilder(what + "\n\nRows (top to bottom):\n");
            for (int r = 0; r < 3; r++) sb.Append("  " + (r + 1) + ". " + RowName(Set, r) + "\n");
            sb.Append("\nColumns (left to right):\n");
            for (int c = 0; c < 3; c++) sb.Append("  " + (c + 1) + ". " + StyleNames[c] + "\n");
            sb.Append("\nControls:\n  Right-click a sample: mark or unmark it as a keeper (green frame)\n  Keepers menu: the same, with tick boxes\n");
            if (Set == 1) sb.Append("  Left-click a galaxy: zoom deep into it (Esc or right-click to come back)\n");
            sb.Append("  Mouse wheel: zoom   ·   [ and ]: previous and next set\n  Exit menu or Esc: close the game\n\nKeepers are saved in picks.txt in the game folder.");
            return sb.ToString();
        }

        public void ClearMarks() { for (int i = 0; i < 9; i++) _marks[Set, i] = false; SaveMarks(); }

        private string RowName(int set, int row) { return set == 2 ? _systemNames[row] : _typeNames[row]; }

        // the marks go in picks.txt next to the game, one line per marked sample, so they can be read back
        private void SaveMarks()
        {
            try
            {
                var lines = new System.Collections.Generic.List<string>();
                for (int set = 1; set <= 2; set++)
                    for (int i = 0; i < 9; i++)
                        if (_marks[set, i]) lines.Add("set " + set + " | " + RowName(set, i / 3) + " | " + StyleNames[i % 3]);
                System.IO.File.WriteAllLines("picks.txt", lines.ToArray());
            }
            catch { }
        }

        private void LoadMarks()
        {
            try
            {
                if (!System.IO.File.Exists("picks.txt")) return;
                foreach (var line in System.IO.File.ReadAllLines("picks.txt"))
                {
                    var parts = line.Split('|');
                    if (parts.Length != 3) continue;
                    int set; if (!int.TryParse(parts[0].Trim().Replace("set ", ""), out set) || set < 1 || set > 2) continue;
                    for (int i = 0; i < 9; i++)
                        if (RowName(set, i / 3) == parts[1].Trim() && StyleNames[i % 3] == parts[2].Trim()) _marks[set, i] = true;
                }
            }
            catch { }
        }

        // a green frame of dots around every marked sample
        private void DrawMarks(int width, int height)
        {
            int n = 0;
            for (int i = 0; i < 9; i++)
            {
                if (!_marks[Set, i]) continue;
                float x0 = -1f + (i % 3) * 2f / 3f, x1 = x0 + 2f / 3f, y0 = 1f - (i / 3) * 2f / 3f, y1 = y0 - 2f / 3f;
                const float inset = 0.012f;
                x0 += inset; x1 -= inset; y0 -= inset * 1.6f; y1 += inset * 1.6f;
                for (int k = 0; k < FrameDots; k++)
                {
                    float t = k / (float)(FrameDots - 1);
                    _frameVerts[n++] = SpaceGen.Star(new Vec3(x0 + (x1 - x0) * t, y0, 0.5f), 0.006f, 0.2f, 1f, 0.35f, 1f);
                    _frameVerts[n++] = SpaceGen.Star(new Vec3(x0 + (x1 - x0) * t, y1, 0.5f), 0.006f, 0.2f, 1f, 0.35f, 1f);
                    _frameVerts[n++] = SpaceGen.Star(new Vec3(x0, y1 + (y0 - y1) * t, 0.5f), 0.006f, 0.2f, 1f, 0.35f, 1f);
                    _frameVerts[n++] = SpaceGen.Star(new Vec3(x1, y1 + (y0 - y1) * t, 0.5f), 0.006f, 0.2f, 1f, 0.35f, 1f);
                }
            }
            if (n == 0) return;
            _frameBuf.Write(_frameVerts, n);
            var c = new float[BillboardPipeline.ConstantCount];
            c[0] = 1; c[5] = 1; c[10] = 1; c[15] = 1; c[16] = 1; c[21] = 1;
            c[25] = 1; c[26] = 1; c[27] = 4; c[28] = width; c[29] = height;
            _drawer.Draw(_frameBuf, n, c, 0, 0, width, height);
        }

        public void Dispose()
        {
            if (_frameBuf != null) _frameBuf.Dispose();
            foreach (var b in _buffers) if (b != null) b.Dispose();
            foreach (var b in _sysStatic) if (b != null) b.Dispose();
            foreach (var b in _sysBodies) if (b != null) b.Dispose();
        }
    }
}
