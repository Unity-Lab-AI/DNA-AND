// DNA-AND · platform · safety watchdog.
// If the main loop stops answering, or a timed test run overruns, the game shuts itself down
// so the graphics card and the window are always released. The game never changes display
// settings, so exiting the process is a full revert.
using System;
using System.IO;
using System.Threading;

namespace DnaAnd.Platform
{
    internal sealed class Watchdog
    {
        private long _lastBeat;
        private readonly double _stallSeconds;
        private readonly double _hardLimitSeconds; // < 0 means no limit (normal play)
        private readonly Clock _clock;
        private volatile bool _stop;

        public Watchdog(Clock clock, double stallSeconds, double hardLimitSeconds)
        {
            _clock = clock; _stallSeconds = stallSeconds; _hardLimitSeconds = hardLimitSeconds;
            Beat();
            var t = new Thread(Run) { IsBackground = true, Name = "DNA-AND watchdog" };
            t.Start();
        }

        /// <summary>Call once per frame from the main loop.</summary>
        public void Beat() { Interlocked.Exchange(ref _lastBeat, BitConverter.DoubleToInt64Bits(_clock.Total)); }

        public void Stop() { _stop = true; }

        private void Run()
        {
            while (!_stop)
            {
                Thread.Sleep(250);
                double now = _clock.Total;
                double last = BitConverter.Int64BitsToDouble(Interlocked.Read(ref _lastBeat));
                if (now - last > _stallSeconds) Bail("main loop stalled for " + (now - last).ToString("0.0") + " s");
                if (_hardLimitSeconds >= 0 && now > _hardLimitSeconds) Bail("test run passed its hard limit of " + _hardLimitSeconds + " s");
            }
        }

        private static void Bail(string why)
        {
            try
            {
                Directory.CreateDirectory("build");
                File.AppendAllText(Path.Combine("build", "watchdog.log"), DateTime.Now.ToString("s") + "  SAFETY EXIT: " + why + Environment.NewLine);
            }
            catch { }
            Environment.Exit(3);
        }
    }
}
