// DNA-AND · render · solid faceted ground: triangles lit by one star, every face flat-shaded (the Populous-like look).
// Uses the same root signature as the billboards: [0] 32 root constants at b0, [1] the vertex list at t0.
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DnaAnd.Render
{
    internal sealed class MeshPipeline : IDisposable
    {
        public IntPtr Pipeline;
        public readonly IntPtr RootSignature;

        [DllImport("d3dcompiler_47.dll", CharSet = CharSet.Ansi)]
        private static extern int D3DCompile(byte[] src, UIntPtr size, string name, IntPtr defines, IntPtr include,
            string entry, string target, uint flags1, uint flags2, out IntPtr code, out IntPtr errors);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)] private delegate IntPtr BlobPtrFn(IntPtr self);   // 3
        [UnmanagedFunctionPointer(CallingConvention.StdCall)] private delegate UIntPtr BlobSizeFn(IntPtr self); // 4
        [UnmanagedFunctionPointer(CallingConvention.StdCall)] private delegate int CreateGraphicsPipelineStateFn(IntPtr self, IntPtr desc, ref Guid iid, out IntPtr pso); // 10

        // constants: c[0..15] viewProj, c[16..18] direction the starlight comes from, c[20..22] camera position,
        // c[24] time, c[25] fog distance (0 = no fog), c[26] kind of surface (0 ground, 1 air glow, 2 clouds, 3 water),
        // c[28..30] fog colour
        private const string Hlsl = @"
cbuffer C : register(b0)
{
    row_major float4x4 viewProj;
    float4 light;
    float4 cam;
    float4 misc;
    float4 fog;
    float4 sunCol;      // the starlight's colour on this ground (the star's colour through the air); a = 0: plain white
    float4 nightCol;    // the night side's light: half bright, in the colour of the air's glow and the starlight
};
struct V { float3 pos; float glow; float4 col; };
StructuredBuffer<V> verts : register(t0);
struct VO { float4 pos : SV_Position; float3 wp : TEXCOORD0; float4 col : COLOR0; float glow : TEXCOORD1; };

VO VS(uint vid : SV_VertexID)
{
    V v = verts[vid];
    VO o;
    o.pos = mul(float4(v.pos, 1), viewProj);
    o.wp = v.pos; o.col = v.col; o.glow = v.glow;
    return o;
}

float4 PS(VO i) : SV_Target
{
    // flat facets: the face direction from how the position changes across the triangle
    float3 cr = cross(ddx(i.wp), ddy(i.wp));
    float cl = length(cr);
    float3 n = cl > 1e-30 ? cr / cl : light.xyz;      // a tiny or broken triangle: treat it as facing the light
    float3 toCam = cam.xyz - i.wp;
    if (dot(n, toCam) < 0) n = -n;
    float3 v = normalize(toCam);
    if (misc.w > 0.5)
    {
        // shells: the inner land shows only from inside, the hull only from outside, so the two never fight on screen
        float3 radial = float3(light.w, cam.w, fog.w);
        float outward = dot(v, radial);
        if (misc.w < 1.5 && outward > 0.02) discard;
        if (misc.w > 1.5 && outward < -0.02) discard;
    }
    if (misc.z > 0.5 && misc.z < 1.5)
    {
        // the air: a glowing shell, brightest where you look through the most of it (the edge), only on the lit side
        float3 up = normalize(i.wp);
        float edge = 1 - saturate(abs(dot(up, v)));
        float day = saturate(dot(up, light.xyz) * 1.6 + 0.35);
        float a = (pow(edge, 3) * 1.1 + 0.08) * day;
        return float4(i.col.rgb * a, 1);
    }
    float sun = saturate(dot(n, light.xyz));
    float lit = 0.5 + 0.5 * sun;                                  // a god sees by night: the night side is about half as bright, never black
    float3 sc = sunCol.a > 0 ? sunCol.rgb : float3(1, 1, 1), nc = sunCol.a > 0 ? nightCol.rgb : float3(0.5, 0.5, 0.5);
    float3 lit3 = lerp(nc, sc, sun);                              // by day the star's own light through the air, by night the air's glow
    if (misc.z > 1.5 && misc.z < 2.5)
    {
        // clouds scatter light through themselves: lit from the star's side of the planet, glowing grey from below
        lit = 0.45 + 0.55 * saturate(abs(dot(n, light.xyz)));
    }
    float3 c = misc.z > 1.5 && misc.z < 2.5 ? i.col.rgb * max(lit, i.glow) * sc : i.col.rgb * max(lit3, i.glow);
    float alpha = misc.z > 1.5 ? i.col.a : 1;
    if (misc.z > 2.5)
    {
        // water: a sharp glint of starlight, and more see-through when looked at from straight above
        float3 r = reflect(-light.xyz, n);
        c += pow(saturate(dot(r, v)), 60) * 0.8 * saturate(dot(n, light.xyz) * 4);
        alpha = lerp(alpha, 0.97, pow(1 - saturate(abs(dot(n, v))), 4));
    }
    if (misc.y > 0)
    {
        float f = saturate(length(toCam) / misc.y);
        c = lerp(c, fog.rgb, f * f);
    }
    return float4(c, alpha);
}
";

        /// <summary><paramref name="blend"/>: 0 solid, 1 adds light (glowing air), 2 see-through (clouds, water).</summary>
        public MeshPipeline(IntPtr device, IntPtr rootSignature, int blend)
        {
            RootSignature = rootSignature;
            IntPtr vs = Compile("VS", "vs_5_0"), ps = Compile("PS", "ps_5_0");
            IntPtr p = Marshal.AllocHGlobal(656);
            try
            {
                for (int i = 0; i < 656; i++) Marshal.WriteByte(p, i, 0);
                Marshal.WriteIntPtr(p, 0, RootSignature);
                Marshal.WriteIntPtr(p, 8, BlobPtr(vs)); Marshal.WriteInt64(p, 16, (long)BlobSize(vs).ToUInt64());
                Marshal.WriteIntPtr(p, 24, BlobPtr(ps)); Marshal.WriteInt64(p, 32, (long)BlobSize(ps).ToUInt64());
                // blend: off (solid), every render target: no logic op, write all colours
                for (int k = 0; k < 8; k++)
                {
                    int rt = 128 + k * 40;
                    if (k == 0 && blend > 0)
                    {
                        Marshal.WriteInt32(p, rt, 1);
                        Marshal.WriteInt32(p, rt + 8, blend == 1 ? 2 : 5);     // ONE or SRC_ALPHA
                        Marshal.WriteInt32(p, rt + 12, blend == 1 ? 2 : 6);    // ONE or INV_SRC_ALPHA
                        Marshal.WriteInt32(p, rt + 16, 1);
                        Marshal.WriteInt32(p, rt + 20, 2); Marshal.WriteInt32(p, rt + 24, 2); Marshal.WriteInt32(p, rt + 28, 1);
                        Marshal.WriteInt32(p, rt + 32, 4); Marshal.WriteByte(p, rt + 36, 0xF);
                        continue;
                    }
                    Marshal.WriteInt32(p, rt + 8, 2); Marshal.WriteInt32(p, rt + 12, 1); Marshal.WriteInt32(p, rt + 16, 1);
                    Marshal.WriteInt32(p, rt + 20, 2); Marshal.WriteInt32(p, rt + 24, 1); Marshal.WriteInt32(p, rt + 28, 1);
                    Marshal.WriteInt32(p, rt + 32, 4); Marshal.WriteByte(p, rt + 36, 0xF);
                }
                Marshal.WriteInt32(p, 448, -1);     // sample mask
                Marshal.WriteInt32(p, 452, 3); Marshal.WriteInt32(p, 456, 1); Marshal.WriteInt32(p, 476, 1);   // solid, no culling, depth clip
                Marshal.WriteInt32(p, 496, 1); Marshal.WriteInt32(p, 500, blend == 0 ? 1 : 0); Marshal.WriteInt32(p, 504, blend == 0 ? 5 : 7);   // depth on (reversed: nearer is greater); only solid ground writes it
                for (int f = 0; f < 2; f++) { int b = 516 + f * 16; Marshal.WriteInt32(p, b, 1); Marshal.WriteInt32(p, b + 4, 1); Marshal.WriteInt32(p, b + 8, 1); Marshal.WriteInt32(p, b + 12, 8); }
                Marshal.WriteInt32(p, 572, 3);      // triangles
                Marshal.WriteInt32(p, 576, 1);
                Marshal.WriteInt32(p, 580, (int)D3D12.DXGI_FORMAT_R8G8B8A8_UNORM);
                Marshal.WriteInt32(p, 612, (int)D3D12.DXGI_FORMAT_D32_FLOAT);
                Marshal.WriteInt32(p, 616, 1);
                Guid iid = D3D12.IID_ID3D12PipelineState;
                Com.Check(Com.Method<CreateGraphicsPipelineStateFn>(device, 10)(device, p, ref iid, out Pipeline), "CreateGraphicsPipelineState(mesh)");
            }
            finally { Marshal.FreeHGlobal(p); Com.Release(ref vs); Com.Release(ref ps); }
        }

        private static IntPtr Compile(string entry, string target)
        {
            byte[] src = Encoding.ASCII.GetBytes(Hlsl);
            IntPtr code, err;
            int hr = D3DCompile(src, (UIntPtr)src.Length, "mesh", IntPtr.Zero, IntPtr.Zero, entry, target, 1u << 15, 0, out code, out err);
            if (hr < 0)
            {
                string why = err == IntPtr.Zero ? "(no details)" : Marshal.PtrToStringAnsi(BlobPtr(err), (int)BlobSize(err).ToUInt64());
                throw new InvalidOperationException("Mesh shader " + entry + ": " + why);
            }
            Com.Release(ref err);
            return code;
        }

        private static IntPtr BlobPtr(IntPtr b) { return Com.Method<BlobPtrFn>(b, 3)(b); }
        private static UIntPtr BlobSize(IntPtr b) { return Com.Method<BlobSizeFn>(b, 4)(b); }

        public void Dispose() { Com.Release(ref Pipeline); }
    }

    /// <summary>The three kinds of triangle drawing, ready to record: solid ground, glowing air, see-through clouds and water.</summary>
    internal sealed class MeshPipes : IDisposable
    {
        public readonly MeshPipeline SolidPipe, GlowPipe, ClearPipe;
        public readonly MeshDrawer Solid, Glow, Clear;
        public MeshPipes(Gpu gpu, IntPtr rootSignature)
        {
            SolidPipe = new MeshPipeline(gpu.Device, rootSignature, 0);
            GlowPipe = new MeshPipeline(gpu.Device, rootSignature, 1);
            ClearPipe = new MeshPipeline(gpu.Device, rootSignature, 2);
            Solid = new MeshDrawer(gpu.CommandList, SolidPipe); Glow = new MeshDrawer(gpu.CommandList, GlowPipe); Clear = new MeshDrawer(gpu.CommandList, ClearPipe);
        }
        public void Dispose() { SolidPipe.Dispose(); GlowPipe.Dispose(); ClearPipe.Dispose(); }
    }

    /// <summary>Records solid triangle draws into the frame's command list.</summary>
    internal sealed class MeshDrawer
    {
        [StructLayout(LayoutKind.Sequential)] private struct Viewport { public float X, Y, W, H, MinZ, MaxZ; }
        [StructLayout(LayoutKind.Sequential)] private struct Rect { public int L, T, R, B; }
        [UnmanagedFunctionPointer(CallingConvention.StdCall)] private delegate void SetPsoFn(IntPtr self, IntPtr pso);                    // 25
        [UnmanagedFunctionPointer(CallingConvention.StdCall)] private delegate void SetRootSigFn(IntPtr self, IntPtr rs);                 // 30
        [UnmanagedFunctionPointer(CallingConvention.StdCall)] private delegate void SetConstantsFn(IntPtr self, uint index, uint count, float[] data, uint offset); // 36
        [UnmanagedFunctionPointer(CallingConvention.StdCall)] private delegate void SetRootSrvFn(IntPtr self, uint index, ulong address); // 40
        [UnmanagedFunctionPointer(CallingConvention.StdCall)] private delegate void SetTopologyFn(IntPtr self, int topology);             // 20
        [UnmanagedFunctionPointer(CallingConvention.StdCall)] private delegate void SetViewportsFn(IntPtr self, uint count, ref Viewport vp); // 21
        [UnmanagedFunctionPointer(CallingConvention.StdCall)] private delegate void SetScissorsFn(IntPtr self, uint count, ref Rect r);   // 22
        [UnmanagedFunctionPointer(CallingConvention.StdCall)] private delegate void DrawInstancedFn(IntPtr self, uint verts, uint instances, uint firstVert, uint firstInst); // 12

        private readonly SetPsoFn _pso; private readonly SetRootSigFn _rs; private readonly SetConstantsFn _consts; private readonly SetRootSrvFn _srv;
        private readonly SetTopologyFn _topo; private readonly SetViewportsFn _vp; private readonly SetScissorsFn _sc; private readonly DrawInstancedFn _draw;
        private readonly IntPtr _list; private readonly MeshPipeline _pipe;

        public MeshDrawer(IntPtr commandList, MeshPipeline pipeline)
        {
            _list = commandList; _pipe = pipeline;
            _pso = Com.Method<SetPsoFn>(_list, 25); _rs = Com.Method<SetRootSigFn>(_list, 30);
            _consts = Com.Method<SetConstantsFn>(_list, 36); _srv = Com.Method<SetRootSrvFn>(_list, 40);
            _topo = Com.Method<SetTopologyFn>(_list, 20); _vp = Com.Method<SetViewportsFn>(_list, 21);
            _sc = Com.Method<SetScissorsFn>(_list, 22); _draw = Com.Method<DrawInstancedFn>(_list, 12);
        }

        public void Draw(UploadBuffer buffer, int vertexCount, float[] constants, int x, int y, int w, int h)
        {
            if (vertexCount <= 0) return;
            var vp = new Viewport { X = x, Y = y, W = w, H = h, MinZ = 0, MaxZ = 1 };
            var r = new Rect { L = x, T = y, R = x + w, B = y + h };
            _pso(_list, _pipe.Pipeline); _rs(_list, _pipe.RootSignature);
            _consts(_list, 0, BillboardPipeline.ConstantCount, constants, 0);
            _srv(_list, 1, buffer.GpuAddress);
            _topo(_list, 4);
            _vp(_list, 1, ref vp); _sc(_list, 1, ref r);
            _draw(_list, (uint)vertexCount, 1, 0, 0);
        }
    }
}
