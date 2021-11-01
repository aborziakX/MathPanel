using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MathPanelExt
{
    public class IniReader
    {
        Dictionary<string, string> pairs = new Dictionary<string, string>();
        
        public string AppSettings(string key)
        {
            if (!pairs.ContainsKey(key))
                return null;

            var s = pairs[key];
            return s;
        }
        public IniReader(string fname)
        {
            string [] lines = File.ReadAllLines(fname, Encoding.UTF8);
            for(int i = 0; i < lines.Length; i++)
            {
                string s = lines[i].Trim();
                if (string.IsNullOrEmpty(s))
                    continue;
                int pos = s.IndexOf("#");
                if (pos >= 0)
                    s = s.Substring(0, pos).Trim();
                if (string.IsNullOrEmpty(s))
                    continue;
                pos = s.IndexOf("=");
                if (pos <= 0)
                    continue;
                string key = s.Substring(0, pos).Trim();
                string val = s.Substring(pos + 1).Trim();
                if (key == "" || val == "")
                    continue;
                if (pairs.ContainsKey(key))
                    continue;
                pairs.Add(key, val);
            }
        }
    }
}
