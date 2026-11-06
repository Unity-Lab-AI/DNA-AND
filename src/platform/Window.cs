// DNA-AND · platform · the game window, made with direct Windows calls.
using System;
using System.Runtime.InteropServices;

namespace DnaAnd.Platform
{
    internal sealed class Window
    {
        private const string ClassName = "DnaAndWindow";
        private readonly Win32.WndProc _proc; // kept alive so Windows can call it
        public IntPtr Handle { get; private set; }
        public bool IsOpen { get; private set; }
        public readonly Input Input = new Input();
        public int Width { get { Win32.RECT r; Win32.GetClientRect(Handle, out r); return r.Right - r.Left; } }
        public int Height { get { Win32.RECT r; Win32.GetClientRect(Handle, out r); return r.Bottom - r.Top; } }
        public bool Resized;
        /// <summary>How many pixels the game draws per window pixel (0.5 to 2); the picture is stretched to fit the window.</summary>
        public double RenderScale { get { return Input.Scale; } set { Input.Scale = Math.Max(0.5, Math.Min(2, value)); Resized = true; } }
        public int RenderWidth { get { return Math.Max(1, (int)(Width * Input.Scale)); } }
        public int RenderHeight { get { return Math.Max(1, (int)(Height * Input.Scale)); } }
        public static readonly int[] ScalePercents = { 50, 75, 100, 150, 200 };
        public const int CmdScaleBase = 50;   // render scale i is CmdScaleBase + i
        public int MenuCommand; // the last menu item picked, 0 when none

        public const int CmdExit = 1, CmdAskNow = 2, CmdClearMarks = 3;

        public const int CmdLegend = 4, CmdKeeperBase = 100; // keeper i is CmdKeeperBase + i
        private IntPtr _menu = IntPtr.Zero;

        /// <summary>Builds the review menu bar: Exit, Keep going or revert, Legend, and Keepers with a tick box per sample.</summary>
        public void SetReviewMenu(string[] keeperLabels, bool[] ticked)
        {
            IntPtr bar = Win32.CreateMenu(), keep = Win32.CreatePopupMenu();
            for (int i = 0; i < keeperLabels.Length; i++)
                Win32.AppendMenu(keep, Win32.MF_STRING | (ticked[i] ? Win32.MF_CHECKED : 0), (IntPtr)(CmdKeeperBase + i), keeperLabels[i]);
            Win32.AppendMenu(keep, Win32.MF_SEPARATOR, IntPtr.Zero, null);
            Win32.AppendMenu(keep, Win32.MF_STRING, (IntPtr)CmdClearMarks, "Clear all keepers in this set");
            Win32.AppendMenu(bar, Win32.MF_STRING, (IntPtr)CmdExit, "Exit");
            Win32.AppendMenu(bar, Win32.MF_STRING, (IntPtr)CmdAskNow, "Keep going or revert...");
            Win32.AppendMenu(bar, Win32.MF_POPUP, keep, "Keepers");
            Win32.AppendMenu(bar, Win32.MF_STRING, (IntPtr)CmdLegend, "Legend");
            IntPtr scale = Win32.CreatePopupMenu();
            for (int i = 0; i < ScalePercents.Length; i++)
                Win32.AppendMenu(scale, Win32.MF_STRING | (Math.Abs(ScalePercents[i] - Input.Scale * 100) < 1 ? Win32.MF_CHECKED : 0), (IntPtr)(CmdScaleBase + i), ScalePercents[i] + "%" + (ScalePercents[i] < 100 ? " (faster, softer)" : ScalePercents[i] > 100 ? " (sharper, slower)" : " (the window's own size)"));
            Win32.AppendMenu(bar, Win32.MF_POPUP, scale, "Render scale");
            Win32.SetMenu(Handle, bar);
            if (_menu != IntPtr.Zero) Win32.DestroyMenu(_menu);
            _menu = bar;
            Win32.DrawMenuBar(Handle);
        }

        public const int CmdAbout = 200, CmdReadme = 201, CmdCredits = 202, CmdScaleHud = 203, CmdInvertX = 204, CmdInvertY = 205, CmdHowItWorks = 206;
        /// <summary>The preview's menu bar: Exit; View (the scale on screen, render scale); Help (Readme, Credits, About).</summary>
        public void SetWorldMenu(bool scaleOn)
        {
            IntPtr bar = Win32.CreateMenu(), view = Win32.CreatePopupMenu(), help = Win32.CreatePopupMenu(), scale = Win32.CreatePopupMenu();
            Win32.AppendMenu(view, Win32.MF_STRING | (scaleOn ? Win32.MF_CHECKED : 0), (IntPtr)CmdScaleHud, "Scale on screen	H");
            for (int i = 0; i < ScalePercents.Length; i++)
                Win32.AppendMenu(scale, Win32.MF_STRING | (Math.Abs(ScalePercents[i] - Input.Scale * 100) < 1 ? Win32.MF_CHECKED : 0), (IntPtr)(CmdScaleBase + i), ScalePercents[i] + "%" + (ScalePercents[i] < 100 ? " (faster, softer)" : ScalePercents[i] > 100 ? " (sharper, slower)" : " (the window's own size)"));
            Win32.AppendMenu(view, Win32.MF_POPUP, scale, "Render scale");
            Win32.AppendMenu(view, Win32.MF_SEPARATOR, IntPtr.Zero, null);
            Win32.AppendMenu(view, Win32.MF_STRING | (Settings.Get("invert-left-right", false) ? Win32.MF_CHECKED : 0), (IntPtr)CmdInvertX, "Invert left-right drag");
            Win32.AppendMenu(view, Win32.MF_STRING | (Settings.Get("invert-up-down", false) ? Win32.MF_CHECKED : 0), (IntPtr)CmdInvertY, "Invert up-down drag");
            Win32.AppendMenu(help, Win32.MF_STRING, (IntPtr)CmdReadme, "Readme");
            Win32.AppendMenu(help, Win32.MF_STRING, (IntPtr)CmdHowItWorks, "How it works (waveform, infinite zoom)");
            Win32.AppendMenu(help, Win32.MF_STRING, (IntPtr)CmdCredits, "Credits");
            Win32.AppendMenu(help, Win32.MF_SEPARATOR, IntPtr.Zero, null);
            Win32.AppendMenu(help, Win32.MF_STRING, (IntPtr)CmdAbout, "About DNA-AND");
            Win32.AppendMenu(bar, Win32.MF_STRING, (IntPtr)CmdExit, "Exit");
            Win32.AppendMenu(bar, Win32.MF_POPUP, view, "View");
            Win32.AppendMenu(bar, Win32.MF_POPUP, help, "Help");
            Win32.SetMenu(Handle, bar);
            if (_menu != IntPtr.Zero) Win32.DestroyMenu(_menu);
            _menu = bar;
            Win32.DrawMenuBar(Handle);
        }

        /// <summary>A Windows pop-up with just an OK button.</summary>
        public void Tell(string text, string caption)
        {
            InSizeMove = true;
            try { Win32.MessageBoxTimeout(Handle, text, caption, Win32.MB_OK | Win32.MB_ICONINFORMATION | Win32.MB_TOPMOST, 0, 0xFFFFFFFF); }
            finally { InSizeMove = false; }
        }

        /// <summary>A Windows pop-up with Yes and No buttons. Returns Win32.IDYES, another button id, or Win32.MB_TIMEDOUT.
        /// The game is held still while it is open, so the watchdog is told to wait.</summary>
        public int Ask(string text, string caption, uint seconds)
        {
            InSizeMove = true;
            try { return Win32.MessageBoxTimeout(Handle, text, caption, Win32.MB_YESNO | Win32.MB_ICONQUESTION | Win32.MB_TOPMOST, 0, seconds * 1000); }
            finally { InSizeMove = false; }
        }
        public static volatile bool InSizeMove; // the game is held still on purpose: window moved or resized, or a pop-up is open

        /// <summary><paramref name="quiet"/>: a short automatic check. It opens small in the bottom-right corner and does not
        /// take the keyboard, so it never gets in the way of the window the player is using.</summary>
        /// <param name="native">Open as a normal window covering the whole screen at its own resolution (no display change), for checks at the size the player plays at; it does not take the keyboard.</param>
        public Window(string title, int width, int height, bool quiet = false, bool native = false)
        {
            _proc = OnMessage;
            IntPtr instance = Win32.GetModuleHandle(null);
            var wc = new Win32.WNDCLASSEX
            {
                cbSize = (uint)Marshal.SizeOf(typeof(Win32.WNDCLASSEX)),
                style = Win32.CS_HREDRAW | Win32.CS_VREDRAW,
                lpfnWndProc = _proc,
                hInstance = instance,
                hCursor = Win32.LoadCursor(IntPtr.Zero, (IntPtr)Win32.IDC_ARROW),
                lpszClassName = ClassName
            };
            if (Win32.RegisterClassEx(ref wc) == 0)
                throw new InvalidOperationException("RegisterClassEx failed: " + Marshal.GetLastWin32Error());

            int x = Win32.CW_USEDEFAULT, y = Win32.CW_USEDEFAULT;
            if (native)
            {
                width = Win32.GetSystemMetrics(0); height = Win32.GetSystemMetrics(1); x = 0; y = 0; quiet = true;
            }
            else if (quiet)
            {
                int sw = Win32.GetSystemMetrics(0), sh = Win32.GetSystemMetrics(1);   // the screen size
                width = 640; height = 360; x = sw - width - 8; y = sh - height - 56;
            }
            Handle = Win32.CreateWindowEx(quiet ? Win32.WS_EX_NOACTIVATE : 0u, ClassName, title, Win32.WS_OVERLAPPEDWINDOW | (quiet ? 0u : Win32.WS_VISIBLE),
                x, y, width, height,
                IntPtr.Zero, IntPtr.Zero, instance, IntPtr.Zero);
            if (Handle == IntPtr.Zero)
                throw new InvalidOperationException("CreateWindowEx failed: " + Marshal.GetLastWin32Error());

            // normal play opens maximized, so the game draws at the screen's full resolution (still a window: display settings untouched)
            Win32.ShowWindow(Handle, quiet ? Win32.SW_SHOWNOACTIVATE : Win32.SW_SHOWMAXIMIZED);
            IsOpen = true;
        }

        /// <summary>Handles every waiting Windows message. Returns false once the window has closed.</summary>
        public bool Pump()
        {
            Win32.MSG msg;
            while (Win32.PeekMessage(out msg, IntPtr.Zero, 0, 0, Win32.PM_REMOVE))
            {
                if (msg.message == Win32.WM_QUIT) { IsOpen = false; break; }
                if (DocViewer.PreTranslate(ref msg)) continue;                        // the document viewer's own keys
                Win32.TranslateMessage(ref msg);
                Win32.DispatchMessage(ref msg);
            }
            return IsOpen;
        }

        public void SetTitle(string title) { Win32.SetWindowText(Handle, title); }

        public void Close()
        {
            if (Handle != IntPtr.Zero) Win32.DestroyWindow(Handle);
        }

        private IntPtr OnMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            if (Input.OnMessage(msg, wParam, lParam)) return IntPtr.Zero;
            if (msg == Win32.WM_SIZE) Resized = true;
            if (msg == Win32.WM_COMMAND) { MenuCommand = (int)((long)wParam & 0xFFFF); return IntPtr.Zero; }
            if (msg == Win32.WM_ENTERSIZEMOVE) InSizeMove = true;
            if (msg == Win32.WM_EXITSIZEMOVE) InSizeMove = false;
            if (msg == Win32.WM_DESTROY)
            {
                Win32.PostQuitMessage(0);
                return IntPtr.Zero;
            }
            return Win32.DefWindowProc(hWnd, msg, wParam, lParam);
        }
    }
}
