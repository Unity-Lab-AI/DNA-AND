// DNA-AND · render · the Direct3D 12 device, swap chain and frame loop.
// Display safety: always windowed, Alt+Enter blocked, vsync on, every GPU wait has a timeout.
using System;
using System.Runtime.InteropServices;

namespace DnaAnd.Render
{
    internal sealed class Gpu : IDisposable
    {
        private const int FrameCount = 2;
        private const uint GpuWaitMs = 2000; // never wait forever on the GPU

        private IntPtr _factory, _device, _queue, _swapChain, _rtvHeap, _allocator, _list, _fence, _fenceEvent;
        private readonly IntPtr[] _backBuffers = new IntPtr[FrameCount];
        private uint _rtvStep; private IntPtr _rtvStart;
        private ulong _fenceValue;
        private uint _frameIndex;
        public int Width { get; private set; }
        public int Height { get; private set; }
        public IntPtr Device { get { return _device; } }
        public IntPtr CommandList { get { return _list; } }

        // per-frame calls, looked up once
        private D3D12.PresentFn _present; private D3D12.GetCurrentBackBufferIndexFn _getIndex;
        private D3D12.AllocatorResetFn _allocReset; private D3D12.ListResetFn _listReset; private D3D12.CloseFn _close;
        private D3D12.ResourceBarrierFn _barrier; private D3D12.OMSetRenderTargetsFn _setRT; private D3D12.ClearRenderTargetViewFn _clear;
        private D3D12.ExecuteCommandListsFn _execute; private D3D12.SignalFn _signal;
        private D3D12.GetCompletedValueFn _completed; private D3D12.SetEventOnCompletionFn _onCompletion;

        [DllImport("kernel32.dll")] private static extern IntPtr CreateEvent(IntPtr attrs, bool manualReset, bool initialState, string name);
        [DllImport("kernel32.dll")] private static extern uint WaitForSingleObject(IntPtr handle, uint ms);
        [DllImport("kernel32.dll")] private static extern bool CloseHandle(IntPtr handle);

        public Gpu(IntPtr hwnd, int width, int height)
        {
            Width = Math.Max(1, width); Height = Math.Max(1, height);
            Guid iid = D3D12.IID_IDXGIFactory2;
            Com.Check(D3D12.CreateDXGIFactory2(0, ref iid, out _factory), "CreateDXGIFactory2");
            iid = D3D12.IID_ID3D12Device;
            Com.Check(D3D12.D3D12CreateDevice(IntPtr.Zero, D3D12.D3D_FEATURE_LEVEL_11_0, ref iid, out _device), "D3D12CreateDevice");

            var qd = new D3D12.CommandQueueDesc { Type = D3D12.COMMAND_LIST_TYPE_DIRECT };
            iid = D3D12.IID_ID3D12CommandQueue;
            Com.Check(Com.Method<D3D12.CreateCommandQueueFn>(_device, 8)(_device, ref qd, ref iid, out _queue), "CreateCommandQueue");

            var sd = new D3D12.SwapChainDesc1
            {
                Width = (uint)Width, Height = (uint)Height, Format = D3D12.DXGI_FORMAT_R8G8B8A8_UNORM,
                SampleCount = 1, BufferUsage = D3D12.DXGI_USAGE_RENDER_TARGET_OUTPUT, BufferCount = FrameCount,
                SwapEffect = D3D12.DXGI_SWAP_EFFECT_FLIP_DISCARD
            };
            IntPtr sc1;
            // fullscreen description is null: windowed only, never exclusive fullscreen
            Com.Check(Com.Method<D3D12.CreateSwapChainForHwndFn>(_factory, 15)(_factory, _queue, hwnd, ref sd, IntPtr.Zero, IntPtr.Zero, out sc1), "CreateSwapChainForHwnd");
            _swapChain = Com.Query(sc1, D3D12.IID_IDXGISwapChain3, "IDXGISwapChain3");
            Com.Release(ref sc1);
            Com.Method<D3D12.MakeWindowAssociationFn>(_factory, 8)(_factory, hwnd, D3D12.DXGI_MWA_NO_ALT_ENTER);

            var hd = new D3D12.DescriptorHeapDesc { Type = D3D12.DESCRIPTOR_HEAP_TYPE_RTV, NumDescriptors = FrameCount };
            iid = D3D12.IID_ID3D12DescriptorHeap;
            Com.Check(Com.Method<D3D12.CreateDescriptorHeapFn>(_device, 14)(_device, ref hd, ref iid, out _rtvHeap), "CreateDescriptorHeap");
            _rtvStep = Com.Method<D3D12.GetDescriptorHandleIncrementSizeFn>(_device, 15)(_device, D3D12.DESCRIPTOR_HEAP_TYPE_RTV);
            Com.Method<D3D12.GetCpuHandleStartFn>(_rtvHeap, 9)(_rtvHeap, out _rtvStart);
            CreateTargets();

            iid = D3D12.IID_ID3D12CommandAllocator;
            Com.Check(Com.Method<D3D12.CreateCommandAllocatorFn>(_device, 9)(_device, D3D12.COMMAND_LIST_TYPE_DIRECT, ref iid, out _allocator), "CreateCommandAllocator");
            iid = D3D12.IID_ID3D12GraphicsCommandList;
            Com.Check(Com.Method<D3D12.CreateCommandListFn>(_device, 12)(_device, 0, D3D12.COMMAND_LIST_TYPE_DIRECT, _allocator, IntPtr.Zero, ref iid, out _list), "CreateCommandList");
            iid = D3D12.IID_ID3D12Fence;
            Com.Check(Com.Method<D3D12.CreateFenceFn>(_device, 36)(_device, 0, 0, ref iid, out _fence), "CreateFence");
            _fenceEvent = CreateEvent(IntPtr.Zero, false, false, null);

            _present = Com.Method<D3D12.PresentFn>(_swapChain, 8);
            _getIndex = Com.Method<D3D12.GetCurrentBackBufferIndexFn>(_swapChain, 36);
            _allocReset = Com.Method<D3D12.AllocatorResetFn>(_allocator, 8);
            _close = Com.Method<D3D12.CloseFn>(_list, 9);
            _listReset = Com.Method<D3D12.ListResetFn>(_list, 10);
            _barrier = Com.Method<D3D12.ResourceBarrierFn>(_list, 26);
            _setRT = Com.Method<D3D12.OMSetRenderTargetsFn>(_list, 46);
            _clear = Com.Method<D3D12.ClearRenderTargetViewFn>(_list, 48);
            _execute = Com.Method<D3D12.ExecuteCommandListsFn>(_queue, 10);
            _signal = Com.Method<D3D12.SignalFn>(_queue, 14);
            _completed = Com.Method<D3D12.GetCompletedValueFn>(_fence, 8);
            _onCompletion = Com.Method<D3D12.SetEventOnCompletionFn>(_fence, 9);

            _close(_list); // command lists start open; close until the first frame
            _frameIndex = _getIndex(_swapChain);
        }

        private void CreateTargets()
        {
            var createRtv = Com.Method<D3D12.CreateRenderTargetViewFn>(_device, 20);
            var getBuffer = Com.Method<D3D12.GetBufferFn>(_swapChain, 9);
            for (uint i = 0; i < FrameCount; i++)
            {
                Guid iid = D3D12.IID_ID3D12Resource;
                Com.Check(getBuffer(_swapChain, i, ref iid, out _backBuffers[i]), "GetBuffer");
                createRtv(_device, _backBuffers[i], IntPtr.Zero, Rtv(i));
            }
        }

        private IntPtr Rtv(uint i) { return new IntPtr(_rtvStart.ToInt64() + i * _rtvStep); }

        public void Resize(int width, int height)
        {
            if (width < 1 || height < 1 || (width == Width && height == Height)) return;
            WaitForGpu();
            for (int i = 0; i < FrameCount; i++) Com.Release(ref _backBuffers[i]);
            Com.Check(Com.Method<D3D12.ResizeBuffersFn>(_swapChain, 13)(_swapChain, FrameCount, (uint)width, (uint)height, D3D12.DXGI_FORMAT_R8G8B8A8_UNORM, 0), "ResizeBuffers");
            Width = width; Height = height;
            CreateTargets();
            _frameIndex = _getIndex(_swapChain);
        }

        /// <summary>Starts a frame and clears the screen to a color.</summary>
        public void BeginFrame(float r, float g, float b)
        {
            Com.Check(_allocReset(_allocator), "Allocator.Reset");
            Com.Check(_listReset(_list, _allocator, IntPtr.Zero), "CommandList.Reset");
            var toTarget = new D3D12.TransitionBarrier { Resource = _backBuffers[_frameIndex], Subresource = 0xFFFFFFFF,
                StateBefore = D3D12.RESOURCE_STATE_PRESENT, StateAfter = D3D12.RESOURCE_STATE_RENDER_TARGET };
            _barrier(_list, 1, ref toTarget);
            IntPtr rtv = Rtv(_frameIndex);
            _setRT(_list, 1, ref rtv, 0, IntPtr.Zero);
            _clear(_list, rtv, new[] { r, g, b, 1f }, 0, IntPtr.Zero);
        }

        /// <summary>Finishes the frame and shows it, synced to the monitor (vsync).</summary>
        public void EndFrame()
        {
            var toPresent = new D3D12.TransitionBarrier { Resource = _backBuffers[_frameIndex], Subresource = 0xFFFFFFFF,
                StateBefore = D3D12.RESOURCE_STATE_RENDER_TARGET, StateAfter = D3D12.RESOURCE_STATE_PRESENT };
            _barrier(_list, 1, ref toPresent);
            Com.Check(_close(_list), "CommandList.Close");
            _execute(_queue, 1, new[] { _list });
            Com.Check(_present(_swapChain, 1, 0), "Present");
            WaitForGpu();
            _frameIndex = _getIndex(_swapChain);
        }

        private void WaitForGpu()
        {
            ulong value = ++_fenceValue;
            Com.Check(_signal(_queue, _fence, value), "Queue.Signal");
            if (_completed(_fence) < value)
            {
                Com.Check(_onCompletion(_fence, value, _fenceEvent), "Fence.SetEventOnCompletion");
                WaitForSingleObject(_fenceEvent, GpuWaitMs); // timeout: never hang forever
            }
        }

        public void Dispose()
        {
            if (_queue != IntPtr.Zero && _fence != IntPtr.Zero) WaitForGpu();
            for (int i = 0; i < FrameCount; i++) Com.Release(ref _backBuffers[i]);
            Com.Release(ref _fence); Com.Release(ref _list); Com.Release(ref _allocator); Com.Release(ref _rtvHeap);
            Com.Release(ref _swapChain); Com.Release(ref _queue); Com.Release(ref _device); Com.Release(ref _factory);
            if (_fenceEvent != IntPtr.Zero) { CloseHandle(_fenceEvent); _fenceEvent = IntPtr.Zero; }
        }
    }
}
