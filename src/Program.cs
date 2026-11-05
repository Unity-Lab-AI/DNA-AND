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
            int smokeMs = -1; bool testStall = false; int testDeep = -1; double testZoom = -1; bool testVisit = false, testCore = false; int openSet = 0; int openLook = 0; int testPlanet = -1; double clickAt = -1, clickX = 0.5, clickY = 0.5; double wheelFrom = -1, wheelFor = 0, backFrom = -1, backFor = 0; int pressKey = -1; double pressAt = -1; int testDyson = -1; bool testInside = false; int testJumps = 0; bool testFieldView = false; double testAlt = -1; double[] tour = null; int tourStep = 0; string tourName = "tour"; double shotAt = -1; string shotPath = null; int testSpeed = -1; double tryEvery = -1;
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
                if (args[i] == "--set7") openSet = 7;
                if (args[i] == "--set8") openSet = 8;
                if (args[i] == "--style") openSet = 9;       // the style sample for living and built things
                if (args[i] == "--family") openSet = 10;     // a family of animals, built from their genes
                if (args[i] == "--look" && i + 1 < args.Length) { int lk; if (int.TryParse(args[i + 1], out lk)) openLook = lk - 1; }
                if (args[i] == "--wheelback" && i + 2 < args.Length)   // --wheelback <start s> <for s>: roll the wheel out, for test runs
                {
                    double.TryParse(args[i + 1], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out backFrom);
                    double.TryParse(args[i + 2], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out backFor);
                }
                if (args[i] == "--click" && i + 3 < args.Length)   // --click <x 0-1> <y 0-1> <s>: a left click at that place on screen, for test runs
                {
                    var ic = System.Globalization.CultureInfo.InvariantCulture; var nf = System.Globalization.NumberStyles.Float;
                    double.TryParse(args[i + 1], nf, ic, out clickX); double.TryParse(args[i + 2], nf, ic, out clickY); double.TryParse(args[i + 3], nf, ic, out clickAt);
                }
                if (args[i] == "--wheel" && i + 2 < args.Length)   // --wheel <start s> <for s>: roll the wheel in once a frame, for test runs
                {
                    double.TryParse(args[i + 1], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out wheelFrom);
                    double.TryParse(args[i + 2], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out wheelFor);
                }
                if (args[i] == "--press" && i + 2 < args.Length)   // --press <key letter> <seconds>: press a key once, for test runs
                {
                    pressKey = char.ToUpperInvariant(args[i + 1][0]);
                    double.TryParse(args[i + 2], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out pressAt);
                }
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
            // the folder holding "content": the exe's own folder or one above it (build\ or build\dev\)
            string root = exeDir;
            for (int up = 0; up < 3 && root != null && !System.IO.Directory.Exists(System.IO.Path.Combine(root, "content")); up++) root = System.IO.Path.GetDirectoryName(root);
            if (root == null) root = exeDir;
            System.IO.Directory.SetCurrentDirectory(root);
            var clock = new Clock();
            // Safety: force-exit if the main loop stalls 6 s, or if a test overruns its time by 5 s.
            var watchdog = new Watchdog(clock, 6.0, smokeMs >= 0 ? smokeMs / 1000.0 + 5.0 : -1);

            ContentLibrary content = ContentLibrary.Load("content");
            bool big = Array.IndexOf(args, "--big") >= 0;                      // a full-size check, for pictures at the size the player sees
            var window = new Window("DNA-AND", 1600, 900, smokeMs >= 0 && !big);   // timed checks open small and out of the way
            window.Input.IgnoreReal = smokeMs >= 0;          // timed checks never take the person's keyboard or mouse
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
            bool journey = Array.IndexOf(args, "--journey") >= 0;
            ElementView elements = openSet == 7 ? new ElementView(gpu, meshes, content) : null;
            MicrobeView microbes = openSet == 8 ? new MicrobeView(gpu, pipeline, Seed) : null;
            StyleView style = openSet == 9 || openSet == 10 ? new StyleView(gpu, meshes, Seed, openLook, openSet == 10) : null;
            if (openSet == 6 && testDyson < 0)
            {
                var one = viewer.DysonAt(5).Enterable;
                planet = new PlanetView(gpu, meshes, pipeline, one, one, false); planet.Extras = viewer.DysonAt(5).Pieces;
            }
            if (testDyson >= 0 && viewer.DysonAt(testDyson).Enterable != null)
            {
                var sh = viewer.DysonAt(testDyson).Enterable;
                planet = new PlanetView(gpu, meshes, pipeline, sh, sh, false); planet.Extras = viewer.DysonAt(testDyson).Pieces;
                if (testInside) planet.GoInside();
                if (Array.IndexOf(args, "--centre") >= 0) planet.Centre();
                if (testAlt > 0) planet.SetAltitude(testAlt);
            }
            if (testPlanet >= 0) { planet = new PlanetView(gpu, meshes, pipeline, viewer.TerrainAt(testPlanet), viewer.FieldTerrainAt(testPlanet), viewer.Set == 5 || testFieldView); for (int k = 0; k < testJumps; k++) planet.JumpToNextField(); if (testAlt > 0) planet.SetAltitude(testAlt); int si = Array.IndexOf(args, "--undersea"); if (si >= 0 && si + 1 < args.Length) planet.GoUnderSea(double.Parse(args[si + 1], System.Globalization.CultureInfo.InvariantCulture)); }
            UniverseView universe = journey ? new UniverseView(gpu, pipeline, content, Seed) : null;
            if (journey && Array.IndexOf(args, "--test-system") >= 0) { deep = new DeepView(gpu, pipeline, content, viewer.TypeOf(0), viewer.NameOf(0), Seed); deep.TestJourney(gpu.Width, gpu.Height); }
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
                else if (cmd == Window.CmdLegend) window.Tell(style != null ? style.Legend() : microbes != null ? microbes.Legend() : elements != null ? elements.Legend() : planet != null ? planet.Legend() : viewer.Legend(), "DNA-AND · Legend");
                else if (cmd == Window.CmdClearMarks) { viewer.ClearMarks(); menuDirty = true; }
                else if (cmd >= Window.CmdKeeperBase && cmd < Window.CmdKeeperBase + 9) { if (style != null) style.ToggleMark(cmd - Window.CmdKeeperBase); else viewer.ToggleMark(cmd - Window.CmdKeeperBase); menuDirty = true; }
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
                if (pressKey >= 0 && clock.Total >= pressAt) { window.Input.Press(pressKey); pressKey = -1; }
                if (wheelFrom >= 0 && clock.Total >= wheelFrom && clock.Total < wheelFrom + wheelFor) window.Input.Wheel = 1;
                if (backFrom >= 0 && clock.Total >= backFrom && clock.Total < backFrom + backFor) window.Input.Wheel = -1;
                if (clickAt >= 0 && clock.Total >= clickAt) { window.Input.MouseX = (int)(clickX * window.Width); window.Input.MouseY = (int)(clickY * window.Height); window.Input.LeftClicked = true; clickAt = -1; }
                if (window.Input.IsDown(0x11)) window.Input.Wheel *= 8;          // hold Ctrl: the wheel zooms eight times as fast, in every view
                if (style != null) style.Update(window.Input, clock.Delta, window.Width, window.Height);
                else if (microbes != null) microbes.Update(window.Input, clock.Delta);
                else if (elements != null) elements.Update(window.Input, clock.Delta);
                else if (planet != null) { if (!planet.Update(window.Input, clock.Delta))
                    {
                        if (viewer.Set == 6) window.Close();          // the Dyson sphere is the whole set: leaving it closes the sample
                        else { planet.Dispose(); planet = null; if (deep != null) deep.ReturnFromPlanet(); }   // the journey: back up to the solar system
                    } }
                else if (deep == null && universe == null)
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
                else if (deep == null && universe != null)
                {
                    // the start of the journey: the mix of galaxies; close enough to one, fly into it
                    if (!universe.Update(window.Input, clock.Delta, gpu.Width, gpu.Height)) window.Close();
                    else if (universe.HandOff >= 0)
                    {
                        var g = universe.Focus;
                        deep = new DeepView(gpu, pipeline, content, g.Type, g.Name, g.Seed) { Meshes = meshes };
                        // no target is forced: the wheel zooms where the mouse points, or click a star to fly to it
                        universe.HandOff = -1;
                    }
                }
                else if (!deep.Update(window.Input, clock.Delta, gpu.Width, gpu.Height))
                {
                    if (journey && deep.Ascend && universe != null) { deep.Dispose(); deep = null; universe.ReturnFromGalaxy(); }
                    else if (journey) window.Close();
                    else { deep.Dispose(); deep = null; }
                }
                else if (deep.HandOff >= 0)
                {
                    // the journey: close enough to a planet, so fly down to it, carrying on from the same distance
                    int b = deep.HandOff;
                    var body = deep.System.Bodies[b];
                    // carry on with the same 3D planet the solar system view was already drawing, so nothing jumps
                    var t = deep.FocusTerrain ?? new DnaAnd.Gen.PlanetTerrain(gpu.Device, TerrainKind(body.Kind), body.Kind, (uint)(deep.SystemSeed * 977UL + (ulong)b));
                    var tf = deep.FocusFields ?? t;
                    planet = new PlanetView(gpu, meshes, pipeline, t, tf, false) { Journey = true };
                    planet.SetAltitude((deep.Distance / deep.BodyRadiusUnits(b) - 1) * t.Radius);
                    deep.HandOff = -1;
                }
                gpu.BeginFrame(0.005f, 0.006f, 0.012f);
                if (style != null) style.Draw(gpu, gpu.Width, gpu.Height, clock.Total);
                else if (microbes != null) microbes.Draw(gpu, gpu.Width, gpu.Height, clock.Total);
                else if (elements != null) elements.Draw(gpu, gpu.Width, gpu.Height, clock.Total);
                else if (planet != null) planet.Draw(gpu, gpu.Width, gpu.Height);
                else if (deep == null && universe != null) universe.Draw(gpu.Width, gpu.Height, clock.Total);
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
                    lastTitle = string.Format("{0} · {1:0} fps", style != null ? style.Title : microbes != null ? microbes.Title : elements != null ? elements.Title : planet != null ? planet.Title : deep == null ? (universe != null ? universe.Title : viewer.Title) : deep.Title, frames / titleTimer);
                    window.SetTitle(lastTitle);
                    titleTimer = 0; frames = 0;
                }

                window.Input.EndFrame();
                if (smokeMs >= 0 && clock.Total * 1000 >= smokeMs) { window.Close(); smokeMs = -1; }
            }
            if (deep != null) deep.Dispose();
            if (elements != null) elements.Dispose();
            if (universe != null) universe.Dispose();
            if (microbes != null) microbes.Dispose();
            if (style != null) style.Dispose();
            viewer.Dispose(); if (planet != null) planet.Dispose(); if (meshes != null) meshes.Dispose(); pipeline.Dispose(); gpu.Dispose();
            watchdog.Stop();
            WriteLog(string.Format("ok · {0} content entries from {1} files · ran {2:0.00} s · gpu {3}x{4} · {5}", content.Entries.Count, content.Files.Count, clock.Total, gpu.Width, gpu.Height, endReason) + Environment.NewLine + "last title: " + lastTitle);
            return 0;
        }

        /// <summary>Which kind of ground a planet of the solar system gets in the planet view.</summary>
        private static string TerrainKind(string bodyKind)
        {
            switch (bodyKind)
            {
                case "Temperate world": return "temperate";
                case "Lava planet": return "lava";
                case "Ice world": return "snowball";
                case "Gas giant": return "gas-giant";
                case "Ice giant": return "ice-giant";
                case "Hot Jupiter": return "hot-jupiter";
                default: return "desert";                     // rocky planets and moons: dry rock and dust
            }
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
