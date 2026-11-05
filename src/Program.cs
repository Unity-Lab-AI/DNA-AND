// DNA-AND · entry point.
using System;
using DnaAnd.Platform;

namespace DnaAnd
{
    internal static class Program
    {
        [STAThread]
        private static int Main(string[] args)
        {
            // --smoke <ms>: open the window, then close it on its own after <ms> milliseconds (build check).
            int smokeMs = -1;
            for (int i = 0; i + 1 < args.Length; i++)
                if (args[i] == "--smoke") int.TryParse(args[i + 1], out smokeMs);

            var window = new Window("DNA-AND", 1600, 900);
            DateTime start = DateTime.UtcNow;
            while (window.Pump())
            {
                if (smokeMs >= 0 && (DateTime.UtcNow - start).TotalMilliseconds >= smokeMs)
                {
                    window.Close();
                    smokeMs = -1;
                }
                Win32.Sleep(1);
            }
            return 0;
        }
    }
}
