// DNA-AND · shell · the keep-or-revert countdown bar across the top of the screen.
using System;
using System.Runtime.InteropServices;
using DnaAnd.Gen;
using DnaAnd.Render;

namespace DnaAnd.Shell
{
    internal sealed class KeepBar : IDisposable
    {
        private const int Dots = 240;
        private readonly UploadBuffer _buf; private readonly BillboardDrawer _drawer;
        private readonly StarVertex[] _verts = new StarVertex[Dots];

        public KeepBar(Gpu gpu, BillboardPipeline pipe)
        {
            _drawer = new BillboardDrawer(gpu.CommandList, pipe);
            _buf = new UploadBuffer(gpu.Device, (long)Dots * Marshal.SizeOf(typeof(StarVertex)));
        }

        /// <summary>Draws the bar, <paramref name="fraction"/> (0..1) of time left, across the top of the screen.</summary>
        public void Draw(double fraction, int w, int h)
        {
            int n = Math.Max(1, (int)(Dots * Math.Max(0, Math.Min(1, fraction))));
            for (int i = 0; i < n; i++)
            {
                float x = -1f + (i + 0.5f) * 2f / Dots;
                _verts[i] = SpaceGen.Star(new DnaAnd.Core.Vec3(x, 0.975f, 0.5f), 0.012f, 1f, 0.45f, 0.15f, 1f);
            }
            _buf.Write(_verts, n);
            var c = new float[BillboardPipeline.ConstantCount];
            c[0] = 1; c[5] = 1; c[10] = 1; c[15] = 1;    // identity: positions are already on-screen coordinates
            c[16] = 1; c[21] = 1;                         // right = x, up = y
            c[25] = 1; c[26] = 1; c[27] = 4; c[28] = w; c[29] = h;
            _drawer.Draw(_buf, n, c, 0, 0, w, h);
        }

        public void Dispose() { _buf.Dispose(); }
    }
}
