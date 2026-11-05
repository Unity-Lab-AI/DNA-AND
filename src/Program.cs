// DNA-AND · entry point.
using System;
using DnaAnd.Core;
using DnaAnd.Platform;
using DnaAnd.Render;

namespace DnaAnd
{
    internal static class Program
    {
        [STAThread]
        private static int Main(string[] args)
        {
            // --smoke <ms>: a timed test run that closes itself after <ms> milliseconds.
            int smokeMs = -1; bool testStall = false;
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "--smoke" && i + 1 < args.Length) int.TryParse(args[i + 1], out smokeMs);
                if (args[i] == "--test-stall") testStall = true; // proves the watchdog fires
            }

            var clock = new Clock();
            // Safety: force-exit if the main loop stalls 3 s, or if a test overruns its time by 5 s.
            var watchdog = new Watchdog(clock, 3.0, smokeMs >= 0 ? smokeMs / 1000.0 + 5.0 : -1);

            ContentLibrary content = ContentLibrary.Load("content");
            var window = new Window("DNA-AND", 1600, 900);
            Gpu gpu;
            try { gpu = new Gpu(window.Handle, window.Width, window.Height); }
            catch (Exception ex) { WriteLog("gpu error · " + ex.Message); window.Close(); return 2; }

            double titleTimer = 0; int frames = 0;
            while (window.Pump())
            {
                clock.Tick();
                watchdog.Beat();

                if (window.Input.WasPressed(Win32.VK_ESCAPE)) window.Close();
                if (testStall && clock.Total > 0.5) System.Threading.Thread.Sleep(10000);

                if (window.Resized) { window.Resized = false; gpu.Resize(window.Width, window.Height); }
                float t = (float)clock.Total;
                gpu.BeginFrame(0.02f, 0.03f + 0.02f * (float)Math.Sin(t), 0.08f);
                gpu.EndFrame();

                frames++; titleTimer += clock.Delta;
                if (titleTimer >= 0.5)
                {
                    window.SetTitle(string.Format("DNA-AND · {0:0} fps · {1} content entries from {2} files",
                        frames / titleTimer, content.Entries.Count, content.Files.Count));
                    titleTimer = 0; frames = 0;
                }

                window.Input.EndFrame();
                if (smokeMs >= 0 && clock.Total * 1000 >= smokeMs) { window.Close(); smokeMs = -1; }
            }
            gpu.Dispose();
            watchdog.Stop();
            WriteLog(string.Format("ok · {0} content entries from {1} files · ran {2:0.00} s · gpu {3}x{4}", content.Entries.Count, content.Files.Count, clock.Total, gpu.Width, gpu.Height));
            return 0;
        }

        private static void WriteLog(string line)
        {
            try
            {
                System.IO.Directory.CreateDirectory("build");
                System.IO.File.WriteAllText(System.IO.Path.Combine("build", "last-run.log"), line + Environment.NewLine);
            }
            catch { }
        }
    }
}
