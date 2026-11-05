// DNA-AND · entry point.
using System;
using DnaAnd.Core;
using DnaAnd.Platform;
using DnaAnd.Render;
using DnaAnd.Shell;

namespace DnaAnd
{
    internal static class Program
    {
        [STAThread]
        private static int Main(string[] args)
        {
            // --smoke <ms>: a timed test run that closes itself after <ms> milliseconds.
            int smokeMs = -1; bool testStall = false; int testDeep = -1; double testZoom = -1; bool testVisit = false, testCore = false; int testSpeed = -1; double tryEvery = -1;
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "--smoke" && i + 1 < args.Length) int.TryParse(args[i + 1], out smokeMs);
                if (args[i] == "--test-stall") testStall = true; // proves the watchdog fires
                if (args[i] == "--deep" && i + 1 < args.Length) int.TryParse(args[i + 1], out testDeep);       // open deep zoom on a row
                if (args[i] == "--zoom" && i + 1 < args.Length) double.TryParse(args[i + 1], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out testZoom);
                if (args[i] == "--visit") testVisit = true;  // visit the middle star
                if (args[i] == "--core") testCore = true;    // zoom to the core black hole
                if (args[i] == "--speed" && i + 1 < args.Length) int.TryParse(args[i + 1], out testSpeed); // 0-4
                if (args[i] == "--try" && i + 1 < args.Length) double.TryParse(args[i + 1], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out tryEvery); // keep-or-revert prompt every N s
            }

            try { Win32.SetProcessDpiAwarenessContext(Win32.DPI_PER_MONITOR_AWARE_V2); } catch { }
            // run from the repo root no matter how the game was started (double-click, test.cmd, play.cmd)
            string exeDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string root = System.IO.Directory.Exists(System.IO.Path.Combine(exeDir, "content")) ? exeDir : System.IO.Path.GetDirectoryName(exeDir);
            System.IO.Directory.SetCurrentDirectory(root);
            var clock = new Clock();
            // Safety: force-exit if the main loop stalls 6 s, or if a test overruns its time by 5 s.
            var watchdog = new Watchdog(clock, 6.0, smokeMs >= 0 ? smokeMs / 1000.0 + 5.0 : -1);

            ContentLibrary content = ContentLibrary.Load("content");
            var window = new Window("DNA-AND", 1600, 900);
            Gpu gpu;
            try { gpu = new Gpu(window.Handle, window.Width, window.Height); }
            catch (Exception ex) { WriteLog("gpu error · " + ex.Message); window.Close(); return 2; }
            BillboardPipeline pipeline; SampleViewer viewer;
            try
            {
                pipeline = new BillboardPipeline(gpu.Device);
                viewer = new SampleViewer(gpu, pipeline, content, 20261105UL);
            }
            catch (Exception ex) { WriteLog("setup error · " + ex.Message); gpu.Dispose(); window.Close(); return 2; }

            double titleTimer = 0; int frames = 0; bool firstFrame = true;
            const double KeepWindow = 15.0;          // seconds to answer the keep-or-revert prompt
            double nextAsk = tryEvery > 0 ? tryEvery : double.MaxValue, askStarted = -1;
            var bar = new KeepBar(gpu, pipeline);
            string endReason = "closed normally";
            const ulong Seed = 20261105UL;
            DeepView deep = null;
            if (testDeep >= 0) deep = new DeepView(gpu, pipeline, content, viewer.TypeOf(testDeep), viewer.NameOf(testDeep), Seed + (ulong)testDeep * 7919UL);
            while (window.Pump())
            {
                clock.Tick();
                watchdog.Beat();

                bool cameBack = false;
                if (deep == null && !cameBack && window.Input.WasPressed(Win32.VK_ESCAPE)) window.Close();
                // keep-or-revert: like a display-settings prompt. K keeps it running; no answer closes it safely.
                double left = -1;
                if (tryEvery > 0)
                {
                    if (askStarted < 0 && clock.Total >= nextAsk) askStarted = clock.Total;
                    if (askStarted >= 0)
                    {
                        left = KeepWindow - (clock.Total - askStarted);
                        if (window.Input.WasPressed(0x4B)) { askStarted = -1; nextAsk = clock.Total + tryEvery; left = -1; }
                        else if (left <= 0) { endReason = "closed safely: no answer to the keep-or-revert prompt"; window.Close(); }
                    }
                }
                if (testStall && clock.Total > 0.5) System.Threading.Thread.Sleep(10000);

                if (window.Resized) { window.Resized = false; gpu.Resize(window.Width, window.Height); }
                if (deep != null && testDeep >= 0 && frames == 2)
                {
                    if (testCore) deep.TestCore(); if (testVisit) deep.TestVisit(gpu.Width, gpu.Height);
                    if (testZoom > 0) deep.TestZoom(testZoom);
                    if (testSpeed >= 0) deep.TestSpeed(testSpeed);
                    testDeep = -1;
                }
                if (deep == null)
                {
                    viewer.Update(window.Input, clock.Delta);
                    if (window.Input.LeftClicked)
                    {
                        int row = viewer.RowAt(window.Input.MouseY, window.Height);
                        deep = new DeepView(gpu, pipeline, content, viewer.TypeOf(row), viewer.NameOf(row), Seed + (ulong)row * 7919UL);
                    }
                }
                else if (!deep.Update(window.Input, clock.Delta, gpu.Width, gpu.Height)) { deep.Dispose(); deep = null; cameBack = true; }
                gpu.BeginFrame(0.005f, 0.006f, 0.012f);
                if (deep == null) viewer.Draw(gpu.Width, gpu.Height, clock.Total); else deep.Draw(gpu.Width, gpu.Height, clock.Total);
                if (left > 0) bar.Draw(left / KeepWindow, gpu.Width, gpu.Height);
                gpu.EndFrame();
                if (firstFrame) { firstFrame = false; watchdog.Running(); }

                frames++; titleTimer += clock.Delta;
                if (titleTimer >= 0.5 || (left > 0 && titleTimer >= 0.2))
                {
                    string head = left > 0 ? string.Format("⚠ Press K to keep it running · closes in {0:0} s · ", Math.Ceiling(left)) : "";
                    window.SetTitle(string.Format("{0}{1} · {2:0} fps", head, deep == null ? viewer.Title : deep.Title, frames / titleTimer));
                    titleTimer = 0; frames = 0;
                }

                window.Input.EndFrame();
                if (smokeMs >= 0 && clock.Total * 1000 >= smokeMs) { window.Close(); smokeMs = -1; }
            }
            if (deep != null) deep.Dispose();
            bar.Dispose();
            viewer.Dispose(); pipeline.Dispose(); gpu.Dispose();
            watchdog.Stop();
            WriteLog(string.Format("ok · {0} content entries from {1} files · ran {2:0.00} s · gpu {3}x{4} · {5}", content.Entries.Count, content.Files.Count, clock.Total, gpu.Width, gpu.Height, endReason));
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
