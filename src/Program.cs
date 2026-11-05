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
            int smokeMs = -1; bool testStall = false; int testDeep = -1; double testZoom = -1; bool testVisit = false, testCore = false; int openSet = 0; int testPlanet = -1; int testDyson = -1; bool testInside = false; int testJumps = 0; bool testFieldView = false; double testAlt = -1; double[] tour = null; int tourStep = 0; string tourName = "tour"; double shotAt = -1; string shotPath = null; int testSpeed = -1; double tryEvery = -1;
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "--smoke" && i + 1 < args.Length) int.TryParse(args[i + 1], out smokeMs);
                if (args[i] == "--test-stall") testStall = true; // proves the watchdog fires
                if (args[i] == "--deep" && i + 1 < args.Length) int.TryParse(args[i + 1], out testDeep);       // open deep zoom on a row
                if (args[i] == "--zoom" && i + 1 < args.Length) double.TryParse(args[i + 1], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out testZoom);
                if (args[i] == "--visit") testVisit = true;  // visit the middle star
                if (args[i] == "--set2") openSet = 2;       // open one sample set on its own
                if (args[i] == "--set3") openSet = 3;
                if (args[i] == "--set4") openSet = 4;
                if (args[i] == "--set5") openSet = 5;
                if (args[i] == "--set6") openSet = 6;
                if (args[i] == "--dyson" && i + 1 < args.Length) int.TryParse(args[i + 1], out testDyson);   // fly to Dyson sample N
                if (args[i] == "--inside") testInside = true;
                if (args[i] == "--jump" && i + 1 < args.Length) int.TryParse(args[i + 1], out testJumps);   // fly to the Nth kind of field
                if (args[i] == "--fieldview") testFieldView = true;
                if (args[i] == "--planet" && i + 1 < args.Length) int.TryParse(args[i + 1], out testPlanet);
                if (args[i] == "--tour" && i + 2 < args.Length)   // --tour <name> <km,km,...>: pictures at each height, 4 s apart
                {
                    tourName = args[i + 1];
                    var parts = args[i + 2].Split(',');
                    tour = new double[parts.Length];
                    for (int k = 0; k < parts.Length; k++) double.TryParse(parts[k], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out tour[k]);
                }
                if (args[i] == "--alt" && i + 1 < args.Length) double.TryParse(args[i + 1], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out testAlt);
                if (args[i] == "--shot" && i + 2 < args.Length)   // --shot <seconds> <file.bmp>: save the game's own picture once
                {
                    double.TryParse(args[i + 1], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out shotAt);
                    shotPath = args[i + 2];
                }
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
            bool big = Array.IndexOf(args, "--big") >= 0;                      // a full-size check, for pictures at the size the player sees
            var window = new Window("DNA-AND", 1600, 900, smokeMs >= 0 && !big);   // timed checks open small and out of the way
            Gpu gpu;
            try { gpu = new Gpu(window.Handle, window.Width, window.Height); }
            catch (Exception ex) { WriteLog("gpu error · " + ex.Message); window.Close(); return 2; }
            BillboardPipeline pipeline; SampleViewer viewer; MeshPipes meshes = null;
            try
            {
                pipeline = new BillboardPipeline(gpu.Device);
                meshes = new MeshPipes(gpu, pipeline.RootSignature);
                DnaAnd.Gen.PlanetTerrain.LoadFields(content);
                viewer = new SampleViewer(gpu, pipeline, meshes, content, 20261105UL);
            }
            catch (Exception ex) { WriteLog("setup error · " + ex.Message); gpu.Dispose(); window.Close(); return 2; }

            double titleTimer = 0; int frames = 0; bool firstFrame = true;
            double nextAsk = tryEvery > 0 ? tryEvery : double.MaxValue;
            int menuSet = -1; bool menuDirty = true; string lastTitle = "";
            string endReason = "closed normally";
            const ulong Seed = 20261105UL;
            DeepView deep = null;
            PlanetView planet = null;
            if (openSet > 0) viewer.ShowSet(openSet);
            if (testDyson >= 0 && viewer.DysonAt(testDyson).Enterable != null)
            {
                var sh = viewer.DysonAt(testDyson).Enterable;
                planet = new PlanetView(gpu, meshes, pipeline, sh, sh, false); planet.Extras = viewer.DysonAt(testDyson).Pieces;
                if (testInside) planet.GoInside();
                if (Array.IndexOf(args, "--centre") >= 0) planet.Centre();
                if (testAlt > 0) planet.SetAltitude(testAlt);
            }
            if (testPlanet >= 0) { planet = new PlanetView(gpu, meshes, pipeline, viewer.TerrainAt(testPlanet), viewer.FieldTerrainAt(testPlanet), viewer.Set == 5 || testFieldView); for (int k = 0; k < testJumps; k++) planet.JumpToNextField(); if (testAlt > 0) planet.SetAltitude(testAlt); }
            if (testDeep >= 0) deep = new DeepView(gpu, pipeline, content, viewer.TypeOf(testDeep), viewer.NameOf(testDeep), Seed + (ulong)testDeep * 7919UL);
            while (window.Pump())
            {
                clock.Tick();
                watchdog.Beat();

                if (deep == null && planet == null && window.Input.WasPressed(Win32.VK_ESCAPE)) window.Close();
                // the review menu: rebuilt whenever the set or the keepers change
                if (viewer.Set != menuSet || menuDirty) { window.SetReviewMenu(viewer.KeeperLabels(), viewer.KeeperTicks()); menuSet = viewer.Set; menuDirty = false; }
                int cmd = window.MenuCommand; window.MenuCommand = 0;
                bool askNow = false;
                if (cmd == Window.CmdExit) { endReason = "closed with the Exit button"; window.Close(); }
                else if (cmd == Window.CmdAskNow) askNow = true;
                else if (cmd == Window.CmdLegend) window.Tell(planet != null ? planet.Legend() : viewer.Legend(), "DNA-AND · Legend");
                else if (cmd == Window.CmdClearMarks) { viewer.ClearMarks(); menuDirty = true; }
                else if (cmd >= Window.CmdKeeperBase && cmd < Window.CmdKeeperBase + 9) { viewer.ToggleMark(cmd - Window.CmdKeeperBase); menuDirty = true; }
                // keep-or-revert safety pop-up: Yes keeps the test going, No or no answer in 15 s closes it safely
                if (askNow || (tryEvery > 0 && clock.Total >= nextAsk))
                {
                    int answer = window.Ask("Keep this test running?\n\nYes = keep going\nNo = revert (close the game safely)\n\nNo answer in 15 seconds = revert.", "DNA-AND · Keep or revert", 15);
                    if (answer == Win32.IDYES) nextAsk = tryEvery > 0 ? clock.Total + tryEvery : double.MaxValue;
                    else { endReason = answer == Win32.MB_TIMEDOUT ? "closed safely: no answer to the keep-or-revert pop-up" : "reverted with the No button"; window.Close(); }
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
                if (planet != null) { if (!planet.Update(window.Input, clock.Delta)) { planet.Dispose(); planet = null; } }
                else if (deep == null)
                {
                    viewer.Update(window.Input, clock.Delta);
                    if (window.Input.RightClicked) { viewer.ToggleMark(window.Input.MouseX, window.Input.MouseY, window.Width, window.Height); menuDirty = true; }
                    else if ((window.Input.LeftClicked || window.Input.Wheel > 0) && viewer.Set == 6)
                    {
                        int col = Math.Max(0, Math.Min(2, window.Input.MouseX / Math.Max(1, window.Width / 3)));
                        var ds = viewer.DysonAt(viewer.RowAt(window.Input.MouseY, window.Height) * 3 + col);
                        if (ds.Enterable != null) { planet = new PlanetView(gpu, meshes, pipeline, ds.Enterable, ds.Enterable, false); planet.Extras = ds.Pieces; }
                    }
                    else if ((window.Input.LeftClicked || window.Input.Wheel > 0) && (viewer.Set == 4 || viewer.Set == 5))
                    {
                        int col = Math.Max(0, Math.Min(2, window.Input.MouseX / Math.Max(1, window.Width / 3)));
                        int pick = viewer.RowAt(window.Input.MouseY, window.Height) * 3 + col;
                        planet = new PlanetView(gpu, meshes, pipeline, viewer.TerrainAt(pick), viewer.FieldTerrainAt(pick), viewer.Set == 5);
                    }
                    else if (window.Input.LeftClicked && viewer.Set == 2) viewer.PickFocus(window.Input.MouseX, window.Input.MouseY, window.Width, window.Height);
                    else if (window.Input.LeftClicked && viewer.Set == 1)
                    {
                        int row = viewer.RowAt(window.Input.MouseY, window.Height);
                        deep = new DeepView(gpu, pipeline, content, viewer.TypeOf(row), viewer.NameOf(row), Seed + (ulong)row * 7919UL);
                    }
                }
                else if (!deep.Update(window.Input, clock.Delta, gpu.Width, gpu.Height)) { deep.Dispose(); deep = null; }
                gpu.BeginFrame(0.005f, 0.006f, 0.012f);
                if (planet != null) planet.Draw(gpu, gpu.Width, gpu.Height);
                else if (deep == null) viewer.Draw(gpu.Width, gpu.Height, clock.Total); else deep.Draw(gpu.Width, gpu.Height, clock.Total);
                if (shotPath != null && clock.Total >= shotAt) { gpu.CaptureNext = shotPath; shotPath = null; }
                if (tour != null && planet != null && tourStep < tour.Length * 2 && clock.Total >= 1.0 + tourStep * 3.0)
                {
                    if (tourStep % 2 == 0) planet.SetAltitude(tour[tourStep / 2]);
                    else gpu.CaptureNext = "build/" + tourName + "_" + (tourStep / 2) + ".bmp";
                    tourStep++;
                    if (tourStep == tour.Length * 2) smokeMs = (int)(clock.Total * 1000) + 500;
                }
                gpu.EndFrame();
                if (firstFrame) { firstFrame = false; watchdog.Running(); }

                frames++; titleTimer += clock.Delta;
                if (titleTimer >= 0.5)
                {
                    lastTitle = string.Format("{0} · {1:0} fps", planet != null ? planet.Title : deep == null ? viewer.Title : deep.Title, frames / titleTimer);
                    window.SetTitle(string.Format("{0} · {1:0} fps", planet != null ? planet.Title : deep == null ? viewer.Title : deep.Title, frames / titleTimer));
                    titleTimer = 0; frames = 0;
                }

                window.Input.EndFrame();
                if (smokeMs >= 0 && clock.Total * 1000 >= smokeMs) { window.Close(); smokeMs = -1; }
            }
            if (deep != null) deep.Dispose();
            viewer.Dispose(); if (planet != null) planet.Dispose(); if (meshes != null) meshes.Dispose(); pipeline.Dispose(); gpu.Dispose();
            watchdog.Stop();
            WriteLog(string.Format("ok · {0} content entries from {1} files · ran {2:0.00} s · gpu {3}x{4} · {5}", content.Entries.Count, content.Files.Count, clock.Total, gpu.Width, gpu.Height, endReason) + Environment.NewLine + "last title: " + lastTitle);
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
