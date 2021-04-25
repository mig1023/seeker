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
                Disclaimer = "Дмитрий Браславский, 1991",
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
                Disclaimer = "Дмитрий Браславский, 1992",
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
                Disclaimer = "Дмитрий Браславский, 1995",
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
                Disclaimer = "Владимир Сизиков, 2015",
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
                Disclaimer = "Владимир Сизиков, 2016",
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
                Disclaimer = "Дмитрий Тышевич, 2009",
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
                Disclaimer = "Пётр Прокошев, 2017",
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
                Disclaimer = "Роман Островерхов, 2012",
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
                Disclaimer = "Ольга Голотвина, 1995",
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
                Disclaimer = "Александр Бутягин, Дмитрий Чистов, 1999",
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
                Disclaimer = "Роман Островерхов, 2013",
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
                Disclaimer = "Александр Слюта, 2009",
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
                Disclaimer = "Филипп Эбли, 1992",
                BookColor = "#c93c20",
                Illustration = "OctopusIsland.jpg",
            },

            ["Разрушитель"] = new Description
            {
                XmlBook = "Gamebooks/CreatureOfHavoc.xml",
                Protagonist = CreatureOfHavoc.Character.Protagonist.Init,
                CheckOnlyIf = CreatureOfHavoc.Actions.CheckOnlyIf,
                Paragraphs = new CreatureOfHavoc.Paragraphs(),
                Actions = new CreatureOfHavoc.Actions(),
                Constants = new CreatureOfHavoc.Constants(),
                Save = CreatureOfHavoc.Character.Protagonist.Save,
                Load = CreatureOfHavoc.Character.Protagonist.Load,
                Disclaimer = "Стив Джексон, 1986",
                BookColor = "#145334",
                Illustration = "CreatureOfHavoc.jpg",
            },

            ["Месть Альтея"] = new Description
            {
                XmlBook = "Gamebooks/BloodfeudOfAltheus.xml",
                Protagonist = BloodfeudOfAltheus.Character.Protagonist.Init,
                CheckOnlyIf = BloodfeudOfAltheus.Actions.CheckOnlyIf,
                Paragraphs = new BloodfeudOfAltheus.Paragraphs(),
                Actions = new BloodfeudOfAltheus.Actions(),
                Constants = new BloodfeudOfAltheus.Constants(),
                Save = BloodfeudOfAltheus.Character.Protagonist.Save,
                Load = BloodfeudOfAltheus.Character.Protagonist.Load,
                Disclaimer = "Джон Баттерфилд и др, 1985; перевод Kadena и др.",
                BookColor = "#ebd5b3",
                FontColor = "#000000",
                Illustration = "BloodfeudOfAltheus.jpg",
                ShowDisabledOption = true,
            },

            ["Симулятор пенсионерки"] = new Description
            {
                XmlBook = "Gamebooks/PensionerSimulator.xml",
                Protagonist = PensionerSimulator.Character.Protagonist.Init,
                CheckOnlyIf = PensionerSimulator.Actions.CheckOnlyIf,
                Paragraphs = new PensionerSimulator.Paragraphs(),
                Actions = new PensionerSimulator.Actions(),
                Constants = new PensionerSimulator.Constants(),
                Save = PensionerSimulator.Character.Protagonist.Save,
                Load = PensionerSimulator.Character.Protagonist.Load,
                Disclaimer = "Zaratystra, 2018",
                BookColor = "#030436",
                Illustration = "PensionerSimulator.jpg",
            },

            ["Владыка степей"] = new Description
            {
                XmlBook = "Gamebooks/LordOfTheSteppes.xml",
                Protagonist = LordOfTheSteppes.Character.Protagonist.Init,
                CheckOnlyIf = LordOfTheSteppes.Actions.CheckOnlyIf,
                Paragraphs = new LordOfTheSteppes.Paragraphs(),
                Actions = new LordOfTheSteppes.Actions(),
                Constants = new LordOfTheSteppes.Constants(),
                Save = LordOfTheSteppes.Character.Protagonist.Save,
                Load = LordOfTheSteppes.Character.Protagonist.Load,
                Disclaimer = "Сергей Ступин, 2009",
                BookColor = "#b80f0a",
                Illustration = "LordOfTheSteppes.jpg",
            },

            ["Вой оборотня"] = new Description
            {
                XmlBook = "Gamebooks/HowlOfTheWerewolf.xml",
                Protagonist = HowlOfTheWerewolf.Character.Protagonist.Init,
                CheckOnlyIf = HowlOfTheWerewolf.Actions.CheckOnlyIf,
                Paragraphs = new HowlOfTheWerewolf.Paragraphs(),
                Actions = new HowlOfTheWerewolf.Actions(),
                Constants = new HowlOfTheWerewolf.Constants(),
                Save = HowlOfTheWerewolf.Character.Protagonist.Save,
                Load = HowlOfTheWerewolf.Character.Protagonist.Load,
                Disclaimer = "Джонатан Грин, 2007; перевод Rustem, Vo1t",
                BookColor = "#1b2b09",
                Illustration = "HowlOfTheWerewolf.jpg",
            },

            ["Последнее хокку"] = new Description
            {
                XmlBook = "Gamebooks/LastHokku.xml",
                Protagonist = LastHokku.Character.Protagonist.Init,
                CheckOnlyIf = LastHokku.Actions.CheckOnlyIf,
                Paragraphs = new LastHokku.Paragraphs(),
                Actions = new LastHokku.Actions(),
                Constants = new LastHokku.Constants(),
                Save = LastHokku.Character.Protagonist.Save,
                Load = LastHokku.Character.Protagonist.Load,
                Disclaimer = "Юркий Слон, 2021",
                BookColor = "#deb887",
                FontColor = "#000000",
                Illustration = "LastHokku.jpg",
            },

            ["Сердце льда"] = new Description
            {
                XmlBook = "Gamebooks/HeartOfIce.xml",
                Protagonist = HeartOfIce.Character.Protagonist.Init,
                CheckOnlyIf = HeartOfIce.Actions.CheckOnlyIf,
                Paragraphs = new HeartOfIce.Paragraphs(),
                Actions = new HeartOfIce.Actions(),
                Constants = new HeartOfIce.Constants(),
                Save = HeartOfIce.Character.Protagonist.Save,
                Load = HeartOfIce.Character.Protagonist.Load,
                Disclaimer = "Дэйв Моррис, 1994; перевод Kadena и др.",
                BookColor = "#418988",
                Illustration = "HeartOfIce.jpg",
                ShowDisabledOption = true,
            },
        };

        public static List<string> GetBooks() => new List<string>(Books.Keys);

        public static Description GetDescription(string name) => Books[name];
    }
}
