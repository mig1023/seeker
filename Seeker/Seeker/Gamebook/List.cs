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
                BookColor = "#151515",
                Illustration = "BlackCastleDungeon.jpg",
            },

            ["Тайна капитана Шелтона"] = new Description
            {
                XmlBook = "CaptainSheltonsSecret.xml",
                Protagonist = CaptainSheltonsSecret.Character.Protagonist.Init,
                CheckOnlyIf = CaptainSheltonsSecret.Actions.CheckOnlyIf,
                Paragraphs = new CaptainSheltonsSecret.Paragraphs(),
                Actions = new CaptainSheltonsSecret.Actions(),
                Constants = new CaptainSheltonsSecret.Constants(),
                Disclaimer = "Браславский Дмитрий, 1992",
                BookColor = "#4682B4",
                Illustration = "CaptainSheltonsSecret.jpg",
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
                Illustration = "FaithfulSwordOfTheKing.jpg",
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
                BookColor = "#5da130",
                Illustration = "AdventuresOfABeardlessDeceiver.jpg",
            },

            ["Джунгарское нашествие"] = new Description
            {
                XmlBook = "DzungarWar.xml",
                Protagonist = DzungarWar.Character.Protagonist.Init,
                CheckOnlyIf = DzungarWar.Actions.CheckOnlyIf,
                Paragraphs = new DzungarWar.Paragraphs(),
                Actions = new DzungarWar.Actions(),
                Constants = new DzungarWar.Constants(),
                Disclaimer = "Сизиков Владимир, 2016",
                BookColor = "#4a8026",
                Illustration = "DzungarWar.jpg",
                ShowDisabledOption = true,
            },

            ["Скала ужаса"] = new Description
            {
                XmlBook = "RockOfTerror.xml",
                Protagonist = RockOfTerror.Character.Protagonist.Init,
                CheckOnlyIf = RockOfTerror.Actions.CheckOnlyIf,
                Paragraphs = new RockOfTerror.Paragraphs(),
                Actions = new RockOfTerror.Actions(),
                Constants = new RockOfTerror.Constants(),
                Disclaimer = "Тышевич Дмитрий, 2009",
                BookColor = "#000000",
                Illustration = "RockOfTerror.jpg",
            },

            ["Болотная лихорадка"] = new Description
            {
                XmlBook = "SwampFever.xml",
                Protagonist = SwampFever.Character.Protagonist.Init,
                CheckOnlyIf = SwampFever.Actions.CheckOnlyIf,
                Paragraphs = new SwampFever.Paragraphs(),
                Actions = new SwampFever.Actions(),
                Constants = new SwampFever.Constants(),
                Disclaimer = "Прокошев Пётр, 2017",
                BookColor = "#34411c",
                Illustration = "SwampFever.jpg",
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
