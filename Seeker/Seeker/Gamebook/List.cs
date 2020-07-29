using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook
{
    class List
    {
        private static Dictionary<string, Description> Books = new Dictionary<string, Description>
        {
            ["Подземелья чёрного замка"] = new Description
            {
                XmlBook = "BlackCastleDungeon.xml",
                Protagonist = BlackCastleDungeon.Character.Protagonist.Init,
                CheckOnlyIf = BlackCastleDungeon.Actions.CheckOnlyIf,
                Paragraphs = new BlackCastleDungeon.Paragraphs(),
                Actions = new BlackCastleDungeon.Actions(),
                Constants = new BlackCastleDungeon.Constants(),
                Disclaimer = "Браславский Дмитрий, 1991",
                BookColor = "#4682B4",
            },
        };

        public static List<string> GetBooks()
        {
            return new List<string>(Books.Keys);
        }

        public static Description GetDescription(string name)
        {
            return Books[name];
        }
    }
}
