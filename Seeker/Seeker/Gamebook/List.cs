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

        private static Dictionary<string, Interfaces.ICharacter> Characters = new Dictionary<string, Interfaces.ICharacter>
        {
            ["BlackCastleDungeon.xml"] = new BlackCastleDungeon.Character(),
        };

        private static Dictionary<string, Interfaces.IParagraphs> Paragraphs = new Dictionary<string, Interfaces.IParagraphs>
        {
            ["BlackCastleDungeon.xml"] = new BlackCastleDungeon.Paragraphs(),
        };

        public static List<string> Get()
        {
            return new List<string>(Destinations.Keys);
        }

        public static Interfaces.ICharacter GetProtagonist(string name)
        {
            return Characters[name];
        }

        public static Interfaces.IParagraphs GetParagraphs(string name)
        {
            return Paragraphs[name];
        }

        public static string Find(string text)
        {
            if (!Destinations.ContainsKey(text))
                return String.Empty;

            return Destinations[text];
        }
    }
}
