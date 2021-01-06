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
                XmlBook = "Gamebooks/BlackCastleDungeon.xml",
                Protagonist = BlackCastleDungeon.Character.Protagonist.Init,
                CheckOnlyIf = BlackCastleDungeon.Actions.CheckOnlyIf,
                Paragraphs = new BlackCastleDungeon.Paragraphs(),
                Actions = new BlackCastleDungeon.Actions(),
                Constants = new BlackCastleDungeon.Constants(),
                Save = BlackCastleDungeon.Character.Protagonist.Save,
                Load = BlackCastleDungeon.Character.Protagonist.Load,
                Disclaimer = "Браславский Дмитрий, 1991",
                BookColor = "#151515",
                Illustration = "BlackCastleDungeon.jpg",
            },

            ["Тайна капитана Шелтона"] = new Description
            {
                XmlBook = "Gamebooks/CaptainSheltonsSecret.xml",
                Protagonist = CaptainSheltonsSecret.Character.Protagonist.Init,
                CheckOnlyIf = CaptainSheltonsSecret.Actions.CheckOnlyIf,
                Paragraphs = new CaptainSheltonsSecret.Paragraphs(),
                Actions = new CaptainSheltonsSecret.Actions(),
                Constants = new CaptainSheltonsSecret.Constants(),
                Save = CaptainSheltonsSecret.Character.Protagonist.Save,
                Load = CaptainSheltonsSecret.Character.Protagonist.Load,
                Disclaimer = "Браславский Дмитрий, 1992",
                BookColor = "#4682B4",
                Illustration = "CaptainSheltonsSecret.jpg",
            },

            ["Верная шпага короля"] = new Description
            {
                XmlBook = "Gamebooks/FaithfulSwordOfTheKing.xml",
                Protagonist = FaithfulSwordOfTheKing.Character.Protagonist.Init,
                CheckOnlyIf = FaithfulSwordOfTheKing.Actions.CheckOnlyIf,
                Paragraphs = new FaithfulSwordOfTheKing.Paragraphs(),
                Actions = new FaithfulSwordOfTheKing.Actions(),
                Constants = new FaithfulSwordOfTheKing.Constants(),
                Save = FaithfulSwordOfTheKing.Character.Protagonist.Save,
                Load = FaithfulSwordOfTheKing.Character.Protagonist.Load,
                Disclaimer = "Браславский Дмитрий, 1995",
                BookColor = "#911",
                Illustration = "FaithfulSwordOfTheKing.jpg",
            },

            ["Приключения безбородого обманщика"] = new Description
            {
                XmlBook = "Gamebooks/AdventuresOfABeardlessDeceiver.xml",
                Protagonist = AdventuresOfABeardlessDeceiver.Character.Protagonist.Init,
                CheckOnlyIf = AdventuresOfABeardlessDeceiver.Actions.CheckOnlyIf,
                Paragraphs = new AdventuresOfABeardlessDeceiver.Paragraphs(),
                Actions = new AdventuresOfABeardlessDeceiver.Actions(),
                Constants = new AdventuresOfABeardlessDeceiver.Constants(),
                Save = AdventuresOfABeardlessDeceiver.Character.Protagonist.Save,
                Load = AdventuresOfABeardlessDeceiver.Character.Protagonist.Load,
                Disclaimer = "Сизиков Владимир, 2015",
                BookColor = "#5da130",
                Illustration = "AdventuresOfABeardlessDeceiver.jpg",
            },

            ["Джунгарское нашествие"] = new Description
            {
                XmlBook = "Gamebooks/DzungarWar.xml",
                Protagonist = DzungarWar.Character.Protagonist.Init,
                CheckOnlyIf = DzungarWar.Actions.CheckOnlyIf,
                Paragraphs = new DzungarWar.Paragraphs(),
                Actions = new DzungarWar.Actions(),
                Constants = new DzungarWar.Constants(),
                Save = DzungarWar.Character.Protagonist.Save,
                Load = DzungarWar.Character.Protagonist.Load,
                Disclaimer = "Сизиков Владимир, 2016",
                BookColor = "#4a8026",
                Illustration = "DzungarWar.jpg",
                ShowDisabledOption = true,
            },

            ["Скала ужаса"] = new Description
            {
                XmlBook = "Gamebooks/RockOfTerror.xml",
                Protagonist = RockOfTerror.Character.Protagonist.Init,
                CheckOnlyIf = RockOfTerror.Actions.CheckOnlyIf,
                Paragraphs = new RockOfTerror.Paragraphs(),
                Actions = new RockOfTerror.Actions(),
                Constants = new RockOfTerror.Constants(),
                Save = RockOfTerror.Character.Protagonist.Save,
                Load = RockOfTerror.Character.Protagonist.Load,
                Disclaimer = "Тышевич Дмитрий, 2009",
                BookColor = "#000000",
                Illustration = "RockOfTerror.jpg",
            },

            ["Рандеву"] = new Description
            {
                XmlBook = "Gamebooks/RendezVous.xml",
                Protagonist = RendezVous.Character.Protagonist.Init,
                CheckOnlyIf = RendezVous.Actions.CheckOnlyIf,
                Paragraphs = new RendezVous.Paragraphs(),
                Actions = new RendezVous.Actions(),
                Constants = new RendezVous.Constants(),
                Save = RendezVous.Character.Protagonist.Save,
                Load = RendezVous.Character.Protagonist.Load,
                Disclaimer = "Ал Торо, 2020; перевод Марии Ерошкиной",
                BookColor = "#ffffff",
                FontColor = "#000000",
                BorderColor = "#000000",
                Illustration = "RendezVous.jpg",
            },

            ["Болотная лихорадка"] = new Description
            {
                XmlBook = "Gamebooks/SwampFever.xml",
                Protagonist = SwampFever.Character.Protagonist.Init,
                CheckOnlyIf = SwampFever.Actions.CheckOnlyIf,
                Paragraphs = new SwampFever.Paragraphs(),
                Actions = new SwampFever.Actions(),
                Constants = new SwampFever.Constants(),
                Save = SwampFever.Character.Protagonist.Save,
                Load = SwampFever.Character.Protagonist.Load,
                Disclaimer = "Прокошев Пётр, 2017",
                BookColor = "#ff557c48",
                Illustration = "SwampFever.jpg",
            },

            ["Легенды всегда врут"] = new Description
            {
                XmlBook = "Gamebooks/LegendsAlwaysLie.xml",
                Protagonist = LegendsAlwaysLie.Character.Protagonist.Init,
                CheckOnlyIf = LegendsAlwaysLie.Actions.CheckOnlyIf,
                Paragraphs = new LegendsAlwaysLie.Paragraphs(),
                Actions = new LegendsAlwaysLie.Actions(),
                Constants = new LegendsAlwaysLie.Constants(),
                Save = LegendsAlwaysLie.Character.Protagonist.Save,
                Load = LegendsAlwaysLie.Character.Protagonist.Load,
                Disclaimer = "Островерхов Роман, 2012",
                BookColor = "#4c0000",
                Illustration = "LegendsAlwaysLie.jpg",
            },
            
            ["Вереница миров или выводы из закона Мэрфи"] = new Description
            {
                XmlBook = "Gamebooks/StringOfWorlds.xml",
                Protagonist = StringOfWorlds.Character.Protagonist.Init,
                CheckOnlyIf = StringOfWorlds.Actions.CheckOnlyIf,
                Paragraphs = new StringOfWorlds.Paragraphs(),
                Actions = new StringOfWorlds.Actions(),
                Constants = new StringOfWorlds.Constants(),
                Save = StringOfWorlds.Character.Protagonist.Save,
                Load = StringOfWorlds.Character.Protagonist.Load,
                Disclaimer = "Голотвина Ольга, 1995",
                BookColor = "#990066",
                Illustration = "StringOfWorlds.jpg",
            },
            
            ["Три дороги"] = new Description
            {
                XmlBook = "Gamebooks/ThreePaths.xml",
                Protagonist = ThreePaths.Character.Protagonist.Init,
                CheckOnlyIf = ThreePaths.Actions.CheckOnlyIf,
                Paragraphs = new ThreePaths.Paragraphs(),
                Actions = new ThreePaths.Actions(),
                Constants = new ThreePaths.Constants(),
                Save = ThreePaths.Character.Protagonist.Save,
                Load = ThreePaths.Character.Protagonist.Load,
                Disclaimer = "Бутягин Александр, Чистов Дмитрий, 1999",
                BookColor = "#009999",
                Illustration = "ThreePaths.jpg",
            },

            ["На невидимых фронтах"] = new Description
            {
                XmlBook = "Gamebooks/InvisibleFront.xml",
                Protagonist = InvisibleFront.Character.Protagonist.Init,
                CheckOnlyIf = InvisibleFront.Actions.CheckOnlyIf,
                Paragraphs = new InvisibleFront.Paragraphs(),
                Actions = new InvisibleFront.Actions(),
                Constants = new InvisibleFront.Constants(),
                Save = InvisibleFront.Character.Protagonist.Save,
                Load = InvisibleFront.Character.Protagonist.Load,
                Disclaimer = "mmvvss, 2018",
                BookColor = "#d52b1e",
                FontColor = "#eede49",
                Illustration = "InvisibleFront.jpg",
            },

            ["Silent School"] = new Description
            {
                XmlBook = "Gamebooks/SilentSchool.xml",
                Protagonist = SilentSchool.Character.Protagonist.Init,
                CheckOnlyIf = SilentSchool.Actions.CheckOnlyIf,
                Paragraphs = new SilentSchool.Paragraphs(),
                Actions = new SilentSchool.Actions(),
                Constants = new SilentSchool.Constants(),
                Save = SilentSchool.Character.Protagonist.Save,
                Load = SilentSchool.Character.Protagonist.Load,
                Disclaimer = "Островерхов Роман, 2013",
                BookColor = "#151515",
                Illustration = "SilentSchool.jpg",
            },
            
            ["Идущие на смерть"] = new Description
            {
                XmlBook = "Gamebooks/ThoseWhoAreAboutToDie.xml",
                Protagonist = ThoseWhoAreAboutToDie.Character.Protagonist.Init,
                CheckOnlyIf = ThoseWhoAreAboutToDie.Actions.CheckOnlyIf,
                Paragraphs = new ThoseWhoAreAboutToDie.Paragraphs(),
                Actions = new ThoseWhoAreAboutToDie.Actions(),
                Constants = new ThoseWhoAreAboutToDie.Constants(),
                Save = ThoseWhoAreAboutToDie.Character.Protagonist.Save,
                Load = ThoseWhoAreAboutToDie.Character.Protagonist.Load,
                Disclaimer = "Слюта Александр, 2009",
                BookColor = "#fcdd76",
                FontColor = "#000000",
                Illustration = "ThoseWhoAreAboutToDie.jpg",
                ShowDisabledOption = true,
            },

            ["Остров Осьминогов"] = new Description
            {
                XmlBook = "Gamebooks/OctopusIsland.xml",
                Protagonist = OctopusIsland.Character.Protagonist.Init,
                CheckOnlyIf = OctopusIsland.Actions.CheckOnlyIf,
                Paragraphs = new OctopusIsland.Paragraphs(),
                Actions = new OctopusIsland.Actions(),
                Constants = new OctopusIsland.Constants(),
                Save = OctopusIsland.Character.Protagonist.Save,
                Load = OctopusIsland.Character.Protagonist.Load,
                Disclaimer = "Эбли Филипп, 1992",
                BookColor = "#c93c20",
                Illustration = "OctopusIsland.jpg",
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
