using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook
{
    class List
    {
        private static Dictionary<string, string> Destinations = new Dictionary<string, string>
        {
            ["Подземелья чёрного замка"] = "BlackCastleDungeon.xml",
        };

        public static List<string> Get()
        {
            return new List<string>(Destinations.Keys);
        }

        public static string Find(string text)
        {
            if (!Destinations.ContainsKey(text))
                return String.Empty;

            return Destinations[text];
        }
    }
}
