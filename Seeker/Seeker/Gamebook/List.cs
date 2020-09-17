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
                BookColor = "#000000",
            },

            ["Тайна капитана Шелдона"] = new Description
            {
                XmlBook = "CaptainSheltonsSecret.xml",
                Protagonist = CaptainSheltonsSecret.Character.Protagonist.Init,
                CheckOnlyIf = CaptainSheltonsSecret.Actions.CheckOnlyIf,
                Paragraphs = new CaptainSheltonsSecret.Paragraphs(),
                Actions = new CaptainSheltonsSecret.Actions(),
                Constants = new CaptainSheltonsSecret.Constants(),
                Disclaimer = "Браславский Дмитрий, 1992",
                BookColor = "#4682B4",
            },

            ["Верная шпага короля"] = new Description
            {
                XmlBook = "FaithfulSwordOfTheKing.xml",
                Protagonist = FaithfulSwordOfTheKing.Character.Protagonist.Init,
                CheckOnlyIf = FaithfulSwordOfTheKing.Actions.CheckOnlyIf,
                Paragraphs = new FaithfulSwordOfTheKing.Paragraphs(),
                Actions = new FaithfulSwordOfTheKing.Actions(),
                Constants = new FaithfulSwordOfTheKing.Constants(),
                Disclaimer = "Браславский Дмитрий, 1995",
                BookColor = "#911",
            },
	    
	    ["Приключения безбородого обманщика"] = new Description
            {
                XmlBook = "AdventuresOfABeardlessDeceiver.xml",
                Protagonist = AdventuresOfABeardlessDeceiver.Character.Protagonist.Init,
                CheckOnlyIf = AdventuresOfABeardlessDeceiver.Actions.CheckOnlyIf,
                Paragraphs = new AdventuresOfABeardlessDeceiver.Paragraphs(),
                Actions = new AdventuresOfABeardlessDeceiver.Actions(),
                Constants = new AdventuresOfABeardlessDeceiver.Constants(),
                Disclaimer = "Сизиков Владимир, 2015",
                BookColor = "#006600",
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
