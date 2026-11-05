// DNA-AND · render · root signature, shaders and pipeline state for drawing glowing billboards
// (stars, galaxies, particles). Shaders are compiled by Windows' own d3dcompiler_47.dll.
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DnaAnd.Render
{
    internal sealed class BillboardPipeline : IDisposable
    {
        public const int ConstantCount = 32; // root constants (32-bit values) per draw
        public IntPtr RootSignature, Pipeline;

        [DllImport("d3d12.dll")]
        private static extern int D3D12SerializeRootSignature(IntPtr desc, int version, out IntPtr blob, out IntPtr errors);

        [DllImport("d3dcompiler_47.dll", CharSet = CharSet.Ansi)]
        private static extern int D3DCompile(byte[] src, UIntPtr size, string name, IntPtr defines, IntPtr include,
            string entry, string target, uint flags1, uint flags2, out IntPtr code, out IntPtr errors);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)] private delegate IntPtr BlobPtrFn(IntPtr self);   // 3
        [UnmanagedFunctionPointer(CallingConvention.StdCall)] private delegate UIntPtr BlobSizeFn(IntPtr self); // 4
        [UnmanagedFunctionPointer(CallingConvention.StdCall)] private delegate int CreateRootSignatureFn(IntPtr self, uint nodeMask, IntPtr blob, UIntPtr size, ref Guid iid, out IntPtr rs); // 16
        [UnmanagedFunctionPointer(CallingConvention.StdCall)] private delegate int CreateGraphicsPipelineStateFn(IntPtr self, IntPtr desc, ref Guid iid, out IntPtr pso); // 10

        private const string Hlsl = @"
cbuffer C : register(b0)
{
    row_major float4x4 viewProj;
    float4 right;   // xyz camera right
    float4 up;      // xyz camera up
    float4 misc;    // x time, y size scale, z style, w pixel grid size
    float4 screen;  // x width, y height (of this tile, in pixels)
};
struct Star { float3 pos; float size; float4 color; };
StructuredBuffer<Star> stars : register(t0);
struct VO { float4 pos : SV_Position; float2 uv : TEXCOORD0; float4 col : COLOR0; };

static const float2 corners[6] = { float2(-1,-1), float2(-1,1), float2(1,1), float2(-1,-1), float2(1,1), float2(1,-1) };

VO VS(uint vid : SV_VertexID, uint iid : SV_InstanceID)
{
    Star s = stars[iid];
    float2 c = corners[vid];
    float size = s.size * misc.y;
    float3 world = s.pos + (right.xyz * c.x + up.xyz * c.y) * size;
    VO o;
    o.pos = mul(float4(world, 1), viewProj);
    if (misc.z > 2.5)
    {
        // pixel style: snap the star's centre and size to a coarse pixel grid
        float g = misc.w;
        float4 cc = mul(float4(s.pos, 1), viewProj);
        float2 cn = cc.xy / cc.w;
        float2 cpx = floor((cn * 0.5 + 0.5) * screen.xy / g) * g;
        float2 ext = abs((o.pos.xy / o.pos.w - cn) * 0.5 * screen.xy);
        float h = max(g * 0.5, floor(max(ext.x, ext.y) / g + 0.5) * g);
        float2 px = cpx + c * h;
        o.pos.xy = ((px / screen.xy) * 2 - 1) * o.pos.w;
    }
    o.uv = c;
    o.col = s.color;
    return o;
}

float4 PS(VO i) : SV_Target
{
    float style = misc.z;
    float d = length(i.uv);
    float a;
    if (style < 0.5)        { a = saturate(1 - d); a = a * a; }                     // smooth glow
    else if (style < 1.5)   { a = d < 0.55 ? 1 : 0; }                               // flat, clean dots
    else if (style < 2.5)                                                             // low-poly: faceted hexagon
    {
        float2 p = abs(i.uv);
        float hex = max(p.x * 0.866 + p.y * 0.5, p.y);
        a = hex < 0.7 ? 1 : 0;
        float facet = 0.75 + 0.25 * sign(i.uv.x) * sign(i.uv.y);
        return float4(i.col.rgb * facet * a, a);
    }
    else                                                                              // pixel art: hard square, few colours
    {
        a = 1;
        float3 q = floor(i.col.rgb * 6 + 0.5) / 6;
        return float4(q * 0.35 * a, a);
    }
    return float4(i.col.rgb * a, a);
}
";

        public BillboardPipeline(IntPtr device)
        {
            // ── root signature: [0] root constants b0, [1] root SRV t0 ──
            IntPtr parms = Marshal.AllocHGlobal(64);
            IntPtr desc = Marshal.AllocHGlobal(40);
            try
            {
                for (int i = 0; i < 64; i++) Marshal.WriteByte(parms, i, 0);
                Marshal.WriteInt32(parms, 0, 1);                 // 32-bit constants
                Marshal.WriteInt32(parms, 8, 0);                 // register b0
                Marshal.WriteInt32(parms, 12, 0);                // space 0
                Marshal.WriteInt32(parms, 16, ConstantCount);    // count
                Marshal.WriteInt32(parms, 24, 0);                // visible to all stages
                Marshal.WriteInt32(parms, 32, 3);                // root SRV
                Marshal.WriteInt32(parms, 40, 0);                // register t0
                Marshal.WriteInt32(parms, 44, 0);
                Marshal.WriteInt32(parms, 56, 0);
                for (int i = 0; i < 40; i++) Marshal.WriteByte(desc, i, 0);
                Marshal.WriteInt32(desc, 0, 2);
                Marshal.WriteIntPtr(desc, 8, parms);

                IntPtr blob, err;
                int hr = D3D12SerializeRootSignature(desc, 1, out blob, out err);
                if (hr < 0) throw new InvalidOperationException("Root signature: " + BlobText(err));
                Guid iid = D3D12.IID_ID3D12RootSignature;
                Com.Check(Com.Method<CreateRootSignatureFn>(device, 16)(device, 0, BlobPtr(blob), BlobSize(blob), ref iid, out RootSignature), "CreateRootSignature");
                Com.Release(ref blob);
            }
            finally { Marshal.FreeHGlobal(parms); Marshal.FreeHGlobal(desc); }

            IntPtr vs = Compile("VS", "vs_5_0"), ps = Compile("PS", "ps_5_0");

            // ── pipeline state (laid out field by field, 656 bytes on 64-bit Windows) ──
            IntPtr p = Marshal.AllocHGlobal(656);
            try
            {
                for (int i = 0; i < 656; i++) Marshal.WriteByte(p, i, 0);
                Marshal.WriteIntPtr(p, 0, RootSignature);
                Marshal.WriteIntPtr(p, 8, BlobPtr(vs)); Marshal.WriteInt64(p, 16, (long)BlobSize(vs).ToUInt64());
                Marshal.WriteIntPtr(p, 24, BlobPtr(ps)); Marshal.WriteInt64(p, 32, (long)BlobSize(ps).ToUInt64());
                // blend state at 120: render target 0 at 128, additive
                int rt = 128;
                Marshal.WriteInt32(p, rt + 0, 1);   // blend on
                Marshal.WriteInt32(p, rt + 8, 2);   // src ONE
                Marshal.WriteInt32(p, rt + 12, 2);  // dest ONE
                Marshal.WriteInt32(p, rt + 16, 1);  // add
                Marshal.WriteInt32(p, rt + 20, 2);  // src alpha ONE
                Marshal.WriteInt32(p, rt + 24, 2);  // dest alpha ONE
                Marshal.WriteInt32(p, rt + 28, 1);  // add
                Marshal.WriteInt32(p, rt + 32, 4);  // logic op: no-op
                Marshal.WriteByte(p, rt + 36, 0xF); // write all colours
                for (int k = 1; k < 8; k++) { Marshal.WriteInt32(p, 128 + k * 40 + 32, 4); Marshal.WriteByte(p, 128 + k * 40 + 36, 0xF); }
                Marshal.WriteInt32(p, 448, -1);     // sample mask
                // rasterizer at 452: solid, no culling, depth clip on
                Marshal.WriteInt32(p, 452, 3); Marshal.WriteInt32(p, 456, 1); Marshal.WriteInt32(p, 476, 1);
                // depth/stencil at 496: depth off; valid compare funcs and stencil ops
                Marshal.WriteInt32(p, 504, 2);
                for (int f = 0; f < 2; f++) { int b = 516 + f * 16; Marshal.WriteInt32(p, b, 1); Marshal.WriteInt32(p, b + 4, 1); Marshal.WriteInt32(p, b + 8, 1); Marshal.WriteInt32(p, b + 12, 8); }
                Marshal.WriteInt32(p, 572, 3);      // triangles
                Marshal.WriteInt32(p, 576, 1);      // one render target
                Marshal.WriteInt32(p, 580, (int)D3D12.DXGI_FORMAT_R8G8B8A8_UNORM);
                Marshal.WriteInt32(p, 616, 1);      // sample count
                Guid iid = D3D12.IID_ID3D12PipelineState;
                Com.Check(Com.Method<CreateGraphicsPipelineStateFn>(device, 10)(device, p, ref iid, out Pipeline), "CreateGraphicsPipelineState");
            }
            finally { Marshal.FreeHGlobal(p); Com.Release(ref vs); Com.Release(ref ps); }
        }

        private static IntPtr Compile(string entry, string target)
        {
            byte[] src = Encoding.ASCII.GetBytes(Hlsl);
            IntPtr code, err;
            int hr = D3DCompile(src, (UIntPtr)src.Length, "billboard", IntPtr.Zero, IntPtr.Zero, entry, target, 1u << 15, 0, out code, out err);
            if (hr < 0) throw new InvalidOperationException("Shader " + entry + ": " + BlobText(err));
            Com.Release(ref err);
            return code;
        }

        private static IntPtr BlobPtr(IntPtr b) { return Com.Method<BlobPtrFn>(b, 3)(b); }
        private static UIntPtr BlobSize(IntPtr b) { return Com.Method<BlobSizeFn>(b, 4)(b); }
        private static string BlobText(IntPtr b)
        {
            if (b == IntPtr.Zero) return "(no details)";
            string s = Marshal.PtrToStringAnsi(BlobPtr(b), (int)BlobSize(b).ToUInt64());
            Com.Release(ref b); return s;
        }

        public void Dispose() { Com.Release(ref Pipeline); Com.Release(ref RootSignature); }
    }
}
