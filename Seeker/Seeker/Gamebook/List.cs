using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook
{
    class List
    {
        delegate void ProtagonistInit();

        private static Dictionary<string, string> Destinations = new Dictionary<string, string>
        {
            ["Подземелья чёрного замка"] = "BlackCastleDungeon.xml",
        };

        private static Dictionary<string, ProtagonistInit> CharactersInit = new Dictionary<string, ProtagonistInit>
        {
            ["BlackCastleDungeon.xml"] = BlackCastleDungeon.Character.Protagonist.Init,
        };

        private static Dictionary<string, Interfaces.IParagraphs> Paragraphs = new Dictionary<string, Interfaces.IParagraphs>
        {
            ["BlackCastleDungeon.xml"] = new BlackCastleDungeon.Paragraphs(),
        };

        private static Dictionary<string, Interfaces.IActions> Actions = new Dictionary<string, Interfaces.IActions>
        {
            ["BlackCastleDungeon.xml"] = new BlackCastleDungeon.Actions(),
        };

        public static List<string> GetBooks()
        {
            return new List<string>(Destinations.Keys);
        }

        public static void SetProtagonistInit(string name)
        {
            CharactersInit[name]();
        }

        public static Interfaces.IParagraphs GetParagraphs(string name)
        {
            return Paragraphs[name];
        }

        public static Interfaces.IActions GetActions(string name)
        {
            return Actions[name];
        }

        public static string Find(string text)
        {
            if (!Destinations.ContainsKey(text))
                return String.Empty;

            return Destinations[text];
        }
    }
}
