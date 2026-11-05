// DNA-AND · core · reads the game's content files (docs/CONTENT-FORMAT.md). Our own parser.
using System;
using System.Collections.Generic;
using System.IO;

namespace DnaAnd.Core
{
    internal sealed class ContentEntry
    {
        public string Kind, Id, File, Section;
        public readonly Dictionary<string, string> Props = new Dictionary<string, string>();
        public string Get(string key, string fallback) { string v; return Props.TryGetValue(key, out v) ? v : fallback; }
        public string Name { get { return Get("name", Id); } }

        /// <summary>Reads a range like "30000..50000" (or a single number). Returns false if missing.</summary>
        public bool TryRange(string key, out double lo, out double hi)
        {
            lo = hi = 0; string v;
            if (!Props.TryGetValue(key, out v)) return false;
            int dots = v.IndexOf("..", StringComparison.Ordinal);
            string a = dots >= 0 ? v.Substring(0, dots) : v, b = dots >= 0 ? v.Substring(dots + 2) : v;
            if (!ParseNumber(a, out lo)) return false;
            if (b.Length == 0 || !ParseNumber(b, out hi)) hi = lo;
            return true;
        }
        private static bool ParseNumber(string s, out double d)
        {
            return double.TryParse(s.Trim(), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out d);
        }
    }

    internal sealed class ContentLibrary
    {
        public readonly List<ContentEntry> Entries = new List<ContentEntry>();
        public readonly List<string> Files = new List<string>();

        public static ContentLibrary Load(string folder)
        {
            var lib = new ContentLibrary();
            if (!Directory.Exists(folder)) return lib;
            foreach (string path in Directory.GetFiles(folder, "*.dnat", SearchOption.AllDirectories))
            {
                lib.Files.Add(path);
                lib.ParseFile(path);
            }
            return lib;
        }

        public IEnumerable<ContentEntry> OfKind(string file, string kind)
        {
            foreach (var e in Entries)
                if (e.Kind == kind && (file == null || e.File.EndsWith(file, StringComparison.OrdinalIgnoreCase))) yield return e;
        }

        private void ParseFile(string path)
        {
            string section = "";
            ContentEntry cur = null;
            foreach (string raw in File.ReadAllLines(path))
            {
                string line = raw.TrimEnd();
                if (line.StartsWith("# ──")) { section = line.Trim('#', ' ', '─'); cur = null; continue; }
                if (line.Length == 0 || line.StartsWith("#")) { cur = null; continue; }
                if (line.StartsWith("@"))
                {
                    string[] head = line.Substring(1).Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                    cur = new ContentEntry { Kind = head[0], Id = head.Length > 1 ? head[1] : "", File = path.Replace('\\', '/'), Section = section };
                    Entries.Add(cur);
                    continue;
                }
                int colon = line.IndexOf(':');
                if (cur != null && colon > 0) cur.Props[line.Substring(0, colon).Trim()] = line.Substring(colon + 1).Trim();
            }
        }
    }
}
