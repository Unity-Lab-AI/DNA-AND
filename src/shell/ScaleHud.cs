// DNA-AND · shell · the scale on screen: a bar of a round length (1, 2 or 5 of a unit) and its label, showing how big things are
// where you are looking, from light-years down to micrometres. Drawn in screen pixels on top of the picture, in the game's own
// blocky letters. It can be turned on and off.
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DnaAnd.Core;
using DnaAnd.Gen;
using DnaAnd.Render;

namespace DnaAnd.Shell
{
    internal sealed class ScaleHud : IDisposable
    {
        private readonly IntPtr _device; private UploadBuffer _buf; private StarVertex[] _v = new StarVertex[0];
        public bool Visible = true;

        public ScaleHud(IntPtr device) { _device = device; }

        // a round length near the wanted one: 1, 2 or 5 times a power of ten
        private static double Round(double want)
        {
            double p = Math.Pow(10, Math.Floor(Math.Log10(want))), m = want / p;
            return (m < 2 ? 1 : m < 5 ? 2 : 5) * p;
        }

        // a length in km written plainly, from micrometres to light-years
        public static string Words(double km)
        {
            const double ly = 9.4607e12;
            if (km >= ly * 0.5) return Num(km / ly) + " light-years";
            if (km >= 1.496e8 * 0.5) return Num(km / 1.496e8) + " AU";
            if (km >= 1) return Num(km) + " km";
            if (km >= 1e-3) return Num(km * 1e3) + " m";
            if (km >= 1e-6) return Num(km * 1e6) + " mm";
            if (km >= 1e-9) return Num(km * 1e9) + " micrometres";
            if (km >= 1e-12) return Num(km * 1e12) + " nanometres";
            return Num(km * 1e15) + " picometres";
        }

        private static string Num(double v) { return v >= 1000 ? v.ToString("N0") : v >= 10 ? v.ToString("0") : v.ToString("0.#"); }

        /// <summary>Draws the scale for <paramref name="kmPerPixel"/> at the middle of the view.</summary>
        public void Draw(MeshPipes pipes, double kmPerPixel, int w, int h)
        {
            if (!Visible || kmPerPixel <= 0 || double.IsInfinity(kmPerPixel) || double.IsNaN(kmPerPixel)) return;
            float px = Math.Max(1, h / 540f);                                   // one letter cell, bigger on bigger screens
            double km = Round(kmPerPixel * 220 * px);
            float barLen = (float)(km / kmPerPixel);
            var list = new List<StarVertex>();
            float x0 = 30 * px, y0 = h - 40 * px;
            // the bar with its two end ticks
            Quad(list, x0, y0, barLen, 2 * px, 0.92f, 0.95f, 1f);
            Quad(list, x0, y0 - 6 * px, 2 * px, 8 * px, 0.92f, 0.95f, 1f);
            Quad(list, x0 + barLen - 2 * px, y0 - 6 * px, 2 * px, 8 * px, 0.92f, 0.95f, 1f);
            // its length, in the game's letters, above the bar
            string label = Words(km).ToUpperInvariant();
            float cx = x0, cy = y0 - 18 * px, cell = 1.6f * px;
            foreach (char ch in label)
            {
                if (ch != ' ') foreach (var c in IconFont.Cells(ch)) Quad(list, cx + c[0] * cell, cy + c[1] * cell, cell * 0.9f, cell * 0.9f, 0.92f, 0.95f, 1f);
                cx += 6 * cell;
            }
            int n = list.Count;
            if (_v.Length < n) _v = new StarVertex[n + 600];
            list.CopyTo(_v);
            if (_buf == null || _buf.Bytes < (long)n * Marshal.SizeOf(typeof(StarVertex))) { if (_buf != null) _buf.Dispose(); _buf = new UploadBuffer(_device, (long)(n + 600) * Marshal.SizeOf(typeof(StarVertex))); }
            _buf.Write(_v, n);
            // screen pixels straight to the picture: x right, y down, nothing in front of it
            var m = Mat4.Identity; m.M11 = 2f / w; m.M22 = -2f / h; m.M33 = 0; m.M41 = -1; m.M42 = 1; m.M43 = 0.0001f; m.M44 = 1;
            var c2 = new float[BillboardPipeline.ConstantCount];
            c2[0] = m.M11; c2[1] = m.M12; c2[2] = m.M13; c2[3] = m.M14; c2[4] = m.M21; c2[5] = m.M22; c2[6] = m.M23; c2[7] = m.M24;
            c2[8] = m.M31; c2[9] = m.M32; c2[10] = m.M33; c2[11] = m.M34; c2[12] = m.M41; c2[13] = m.M42; c2[14] = m.M43; c2[15] = m.M44;
            c2[18] = 1; c2[22] = 1;
            pipes.Solid.Draw(_buf, n, c2, 0, 0, w, h);
        }

        private UploadBuffer _ringBuf; private StarVertex[] _ringV = new StarVertex[0];
        /// <summary>A ring of small marks round a point on screen (the thing you picked), in pixels.</summary>
        public void DrawRing(MeshPipes pipes, double x, double y, double radius, int w, int h)
        {
            var list = new List<StarVertex>(); float px = Math.Max(1, h / 540f), s2 = 2.2f * px;
            int n = 32;
            for (int i = 0; i < n; i++)
            {
                if (i % 4 == 3) continue;                                            // a ring broken into dashes
                double a = i * Math.PI * 2 / n;
                Quad(list, (float)(x + Math.Cos(a) * radius) - s2 / 2, (float)(y + Math.Sin(a) * radius) - s2 / 2, s2, s2, 0.55f, 0.95f, 1f);
            }
            int cnt = list.Count;
            if (_ringV.Length < cnt) _ringV = new StarVertex[cnt + 64];
            list.CopyTo(_ringV);
            if (_ringBuf == null || _ringBuf.Bytes < (long)cnt * Marshal.SizeOf(typeof(StarVertex))) { if (_ringBuf != null) _ringBuf.Dispose(); _ringBuf = new UploadBuffer(_device, (long)(cnt + 64) * Marshal.SizeOf(typeof(StarVertex))); }
            _ringBuf.Write(_ringV, cnt);
            var m = Mat4.Identity; m.M11 = 2f / w; m.M22 = -2f / h; m.M33 = 0; m.M41 = -1; m.M42 = 1; m.M43 = 0.0001f; m.M44 = 1;
            var c2 = new float[BillboardPipeline.ConstantCount];
            c2[0] = m.M11; c2[5] = m.M22; c2[10] = m.M33; c2[12] = m.M41; c2[13] = m.M42; c2[14] = m.M43; c2[15] = m.M44;
            c2[18] = 1; c2[22] = 1;
            pipes.Solid.Draw(_ringBuf, cnt, c2, 0, 0, w, h);
        }

        private UploadBuffer _cardBuf; private StarVertex[] _cardV = new StarVertex[0];
        /// <summary>An information card on screen beside the thing you picked: just its lines of text, no box; the first line its name
        /// (brighter), then its facts. Placed at (x, y) in pixels, kept inside the screen.</summary>
        public void DrawCard(MeshPipes pipes, IList<string> lines, double x, double y, int w, int h)
        {
            if (lines == null || lines.Count == 0) return;
            float px = Math.Max(1, h / 540f), cell = 1.5f * px, adv = 6 * cell, lh = 10 * cell, pad = 6 * px;
            int longest = 0; foreach (var l in lines) longest = Math.Max(longest, l.Length);
            float cw = longest * adv + pad * 2, ch = lines.Count * lh + pad * 2 - 3 * cell;
            float x0 = (float)Math.Min(Math.Max(4, x), w - cw - 4), y0 = (float)Math.Min(Math.Max(4, y), h - ch - 4);
            var list = new List<StarVertex>();
            for (int i = 0; i < lines.Count; i++)
            {
                float cx = x0 + pad, cy = y0 + pad + i * lh, r = i == 0 ? 0.55f : 0.85f, g = i == 0 ? 0.95f : 0.88f, b = 1f;
                foreach (char c0 in lines[i].ToUpperInvariant())
                {
                    if (c0 != ' ') foreach (var c in IconFont.Cells(c0)) Quad(list, cx + c[0] * cell, cy + c[1] * cell, cell * 0.9f, cell * 0.9f, r, g, b);
                    cx += adv;
                }
            }
            int cnt = list.Count;
            if (_cardV.Length < cnt) _cardV = new StarVertex[cnt + 1200];
            list.CopyTo(_cardV);
            if (_cardBuf == null || _cardBuf.Bytes < (long)cnt * Marshal.SizeOf(typeof(StarVertex))) { if (_cardBuf != null) _cardBuf.Dispose(); _cardBuf = new UploadBuffer(_device, (long)(cnt + 1200) * Marshal.SizeOf(typeof(StarVertex))); }
            _cardBuf.Write(_cardV, cnt);
            var m = Mat4.Identity; m.M11 = 2f / w; m.M22 = -2f / h; m.M33 = 0; m.M41 = -1; m.M42 = 1; m.M43 = 0.0001f; m.M44 = 1;
            var c2 = new float[BillboardPipeline.ConstantCount];
            c2[0] = m.M11; c2[5] = m.M22; c2[10] = m.M33; c2[12] = m.M41; c2[13] = m.M42; c2[14] = m.M43; c2[15] = m.M44;
            c2[18] = 1; c2[22] = 1;
            pipes.Solid.Draw(_cardBuf, cnt, c2, 0, 0, w, h);
        }

        private static void Quad(List<StarVertex> l, float x, float y, float sw, float sh, float r, float g, float b)
        {
            var a = new StarVertex { X = x, Y = y, Z = 0, Size = 1, R = r, G = g, B = b, A = 1 };
            var bq = a; bq.X = x + sw; var c = a; c.X = x + sw; c.Y = y + sh; var d = a; d.Y = y + sh;
            l.Add(a); l.Add(bq); l.Add(c); l.Add(a); l.Add(c); l.Add(d);
        }

        public void Dispose() { if (_buf != null) _buf.Dispose(); if (_ringBuf != null) _ringBuf.Dispose(); if (_cardBuf != null) _cardBuf.Dispose(); }
    }
}
