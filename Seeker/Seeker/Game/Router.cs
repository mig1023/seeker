using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Game
{
    class Router
    {
        private static Dictionary<string, int> Destinations = new Dictionary<string, int>();

        public static void Clean()
        {
            Destinations.Clear();
        }

        public static void Add(string text, int index)
        {
            Destinations.Add(text, index);
        }

        public static int? Find(string text)
        {
            foreach (KeyValuePair<string, int> k in Destinations)
                if (k.Key == text)
                    return k.Value;

            return null;
        }
    }
}
