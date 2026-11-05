// DNA-AND · shell · the style sample for living and built things: nine subjects (one from each kingdom of life, a village
// and a spaceship) all made by the body builder, shown in one of three looks. Keys 1, 2, 3: smooth, chunky, blocky.
// Drag: turn them. Wheel: closer and further. Space: stop or start the slow turning. Right-click a tile: mark it as a keeper.
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DnaAnd.Core;
using DnaAnd.Gen;
using DnaAnd.Platform;
using DnaAnd.Render;

namespace DnaAnd.Shell
{
    internal sealed class StyleView : IDisposable
    {
        private readonly MeshPipes _pipes; private readonly IntPtr _device;
        private readonly Body[] _bodies = new Body[9];
        private readonly UploadBuffer[] _bufs = new UploadBuffer[9]; private readonly int[] _counts = new int[9];
        private int _look, _built = -1;
        private double _turn, _tilt = 0.35, _zoom = 1; private bool _still;
        private readonly bool[] _marks = new bool[9];

        public StyleView(Gpu gpu, MeshPipes pipes, ulong seed, int look)
        {
            _device = gpu.Device; _pipes = pipes; _look = Math.Max(0, Math.Min(2, look));
            for (int i = 0; i < 9; i++) _bodies[i] = BodyGen.Subject(i, seed);
        }

        public string Title
        {
            get
            {
                var kept = new List<string>(); for (int i = 0; i < 9; i++) if (_marks[i]) kept.Add((i + 1).ToString());
                return "DNA-AND samples · Style of living and built things · look " + (_look + 1) + " of 3: " + BodyGen.LookNames[_look] +
                       " · keepers: " + (kept.Count == 0 ? "none" : string.Join(", ", kept.ToArray())) +
                       " · 1 2 3: change look · drag: turn · wheel: zoom · Space: stop turning · right-click: mark keeper · Legend";
            }
        }

        public string Legend()
        {
            var sb = new System.Text.StringBuilder("Style sample: how living and built things are drawn\n\n");
            sb.Append("The same nine subjects, all built from their own seed by the body builder, in one of three looks:\n");
            for (int k = 0; k < 3; k++) sb.Append("  " + (k + 1) + ": " + BodyGen.LookNames[k] + (k == _look ? "   <- showing now" : "") + "\n");
            sb.Append("\nTiles, left to right, top to bottom:\n");
            for (int i = 0; i < 9; i++) sb.Append("  " + (i + 1) + ": " + BodyGen.SubjectNames[i] + (_marks[i] ? "   (keeper)" : "") + "\n");
            sb.Append("\nControls:\n  1, 2, 3: change the look\n  Drag: turn them · Mouse wheel: closer and further\n  Space: stop or start the slow turning\n  Right-click a tile, or the Keepers menu: mark it\n  Exit menu or Esc: close");
            return sb.ToString();
        }

        public void ToggleMark(int i) { if (i >= 0 && i < 9) _marks[i] = !_marks[i]; }
        public bool[] Marks { get { return _marks; } }

        public void Update(Input input, double dt, int width, int height)
        {
            for (int k = 0; k < 3; k++) if (input.WasPressed(0x31 + k)) _look = k;
            if (input.WasPressed(Win32.VK_SPACE)) _still = !_still;
            if (input.Wheel != 0) _zoom = Math.Max(0.3, Math.Min(3, _zoom * Math.Pow(0.87, input.Wheel)));
            if (input.LeftDown) { _turn += input.MouseDX * 0.01; _tilt = Math.Max(-1.2, Math.Min(1.4, _tilt + input.MouseDY * 0.01)); }
            if (!_still) _turn += dt * 0.35;
            if (input.RightClicked)
            {
                int col = Math.Max(0, Math.Min(2, input.MouseX * 3 / Math.Max(1, width))), row = Math.Max(0, Math.Min(2, input.MouseY * 3 / Math.Max(1, height)));
                ToggleMark(row * 3 + col);
            }
        }

        private void Rebuild()
        {
            if (_built == _look) return;
            _built = _look;
            int st = Marshal.SizeOf(typeof(StarVertex));
            for (int i = 0; i < 9; i++)
            {
                var v = BodyGen.Build(_bodies[i], _look);
                if (_bufs[i] != null) _bufs[i].Dispose();
                _counts[i] = v.Length; _bufs[i] = new UploadBuffer(_device, (long)Math.Max(1, v.Length) * st); _bufs[i].Write(v, v.Length);
            }
        }

        public void Draw(Gpu gpu, int width, int height, double time)
        {
            Rebuild();
            gpu.ClearNow(0.16f, 0.2f, 0.26f);
            int tw = width / 3, th = height / 3;
            var light = new Vec3(-0.45f, 0.75f, -0.5f).Normalized;
            for (int i = 0; i < 9; i++)
            {
                int x = (i % 3) * tw, y = (i / 3) * th;
                float d = _bodies[i].Radius * 2.0f * (float)_zoom;
                var eye = new Vec3((float)(Math.Sin(_turn) * Math.Cos(_tilt)) * d, (float)Math.Sin(_tilt) * d, (float)(-Math.Cos(_turn) * Math.Cos(_tilt)) * d);
                var vp = Mat4.LookAt(eye, new Vec3(0, 0, 0), new Vec3(0, 1, 0)) * Mat4.Perspective(0.8f, tw / (float)Math.Max(1, th), d * 0.05f, d * 4);
                var c = new float[BillboardPipeline.ConstantCount];
                c[0] = vp.M11; c[1] = vp.M12; c[2] = vp.M13; c[3] = vp.M14; c[4] = vp.M21; c[5] = vp.M22; c[6] = vp.M23; c[7] = vp.M24;
                c[8] = vp.M31; c[9] = vp.M32; c[10] = vp.M33; c[11] = vp.M34; c[12] = vp.M41; c[13] = vp.M42; c[14] = vp.M43; c[15] = vp.M44;
                c[16] = light.X; c[17] = light.Y; c[18] = light.Z; c[20] = eye.X; c[21] = eye.Y; c[22] = eye.Z;
                _pipes.Solid.Draw(_bufs[i], _counts[i], c, x + 2, y + 2, tw - 4, th - 4);
            }
        }

        public void Dispose() { foreach (var b in _bufs) if (b != null) b.Dispose(); }
    }
}
