using System;
using System.Collections.Generic;
using System.Linq;
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
                CheckOnlyIf = BlackCastleDungeon.Actions.StaticInstance.CheckOnlyIf,
                Paragraphs = BlackCastleDungeon.Paragraphs.StaticInstance,
                Actions = BlackCastleDungeon.Actions.StaticInstance,
                Constants = BlackCastleDungeon.Constants.StaticInstance,
                Save = BlackCastleDungeon.Character.Protagonist.Save,
                Load = BlackCastleDungeon.Character.Protagonist.Load,
                SmallDisclaimer = "Дмитрий Браславский, 1991",
                BookColor = "#151515",
                Illustration = "BlackCastleDungeon.jpg",
            },

            ["Тайна капитана Шелтона"] = new Description
            {
                XmlBook = "Gamebooks/CaptainSheltonsSecret.xml",
                Protagonist = CaptainSheltonsSecret.Character.Protagonist.Init,
                CheckOnlyIf = CaptainSheltonsSecret.Actions.StaticInstance.CheckOnlyIf,
                Paragraphs = CaptainSheltonsSecret.Paragraphs.StaticInstance,
                Actions = CaptainSheltonsSecret.Actions.StaticInstance,
                Constants = CaptainSheltonsSecret.Constants.StaticInstance,
                Save = CaptainSheltonsSecret.Character.Protagonist.Save,
                Load = CaptainSheltonsSecret.Character.Protagonist.Load,
                SmallDisclaimer = "Дмитрий Браславский, 1992",
                BookColor = "#4682B4",
                Illustration = "CaptainSheltonsSecret.jpg",
            },

            ["Верная шпага короля"] = new Description
            {
                XmlBook = "Gamebooks/FaithfulSwordOfTheKing.xml",
                Protagonist = FaithfulSwordOfTheKing.Character.Protagonist.Init,
                CheckOnlyIf = FaithfulSwordOfTheKing.Actions.StaticInstance.CheckOnlyIf,
                Paragraphs = FaithfulSwordOfTheKing.Paragraphs.StaticInstance,
                Actions = FaithfulSwordOfTheKing.Actions.StaticInstance,
                Constants = FaithfulSwordOfTheKing.Constants.StaticInstance,
                Save = FaithfulSwordOfTheKing.Character.Protagonist.Save,
                Load = FaithfulSwordOfTheKing.Character.Protagonist.Load,
                SmallDisclaimer = "Дмитрий Браславский, 1995",
                BookColor = "#911",
                Illustration = "FaithfulSwordOfTheKing.jpg",
            },

            ["Приключения безбородого обманщика"] = new Description
            {
                XmlBook = "Gamebooks/AdventuresOfABeardlessDeceiver.xml",
                Protagonist = AdventuresOfABeardlessDeceiver.Character.Protagonist.Init,
                CheckOnlyIf = AdventuresOfABeardlessDeceiver.Actions.StaticInstance.CheckOnlyIf,
                Paragraphs = AdventuresOfABeardlessDeceiver.Paragraphs.StaticInstance,
                Actions = AdventuresOfABeardlessDeceiver.Actions.StaticInstance,
                Constants = AdventuresOfABeardlessDeceiver.Constants.StaticInstance,
                Save = AdventuresOfABeardlessDeceiver.Character.Protagonist.Save,
                Load = AdventuresOfABeardlessDeceiver.Character.Protagonist.Load,
                SmallDisclaimer = "Владимир Сизиков, 2015",
                BookColor = "#5da130",
                Illustration = "AdventuresOfABeardlessDeceiver.jpg",
            },

            ["Джунгарское нашествие"] = new Description
            {
                XmlBook = "Gamebooks/DzungarWar.xml",
                Protagonist = DzungarWar.Character.Protagonist.Init,
                CheckOnlyIf = DzungarWar.Actions.StaticInstance.CheckOnlyIf,
                Paragraphs = DzungarWar.Paragraphs.StaticInstance,
                Actions = DzungarWar.Actions.StaticInstance,
                Constants = DzungarWar.Constants.StaticInstance,
                Save = DzungarWar.Character.Protagonist.Save,
                Load = DzungarWar.Character.Protagonist.Load,
                SmallDisclaimer = "Владимир Сизиков, 2016",
                BookColor = "#533818",
                Illustration = "DzungarWar.jpg",
                ShowDisabledOption = true,
            },

            ["Скала ужаса"] = new Description
            {
                XmlBook = "Gamebooks/RockOfTerror.xml",
                Protagonist = RockOfTerror.Character.Protagonist.Init,
                CheckOnlyIf = RockOfTerror.Actions.StaticInstance.CheckOnlyIf,
                Paragraphs = RockOfTerror.Paragraphs.StaticInstance,
                Actions = RockOfTerror.Actions.StaticInstance,
                Constants = RockOfTerror.Constants.StaticInstance,
                Save = RockOfTerror.Character.Protagonist.Save,
                Load = RockOfTerror.Character.Protagonist.Load,
                SmallDisclaimer = "Дмитрий Тышевич, 2009",
                BookColor = "#000000",
                Illustration = "RockOfTerror.jpg",
            },

            ["Рандеву"] = new Description
            {
                XmlBook = "Gamebooks/RendezVous.xml",
                Protagonist = RendezVous.Character.Protagonist.Init,
                CheckOnlyIf = RendezVous.Actions.StaticInstance.CheckOnlyIf,
                Paragraphs = RendezVous.Paragraphs.StaticInstance,
                Actions = RendezVous.Actions.StaticInstance,
                Constants = RendezVous.Constants.StaticInstance,
                Save = RendezVous.Character.Protagonist.Save,
                Load = RendezVous.Character.Protagonist.Load,
                SmallDisclaimer = "Ал Торо, 2020",
                FullDisclaimer = "Автор: Ал Торо\n\nПереводчик: Мария Ерошкина",
                BookColor = "#ffffff",
                FontColor = "#000000",
                BorderColor = "#000000",
                Illustration = "RendezVous.jpg",
            },

            ["Болотная лихорадка"] = new Description
            {
                XmlBook = "Gamebooks/SwampFever.xml",
                Protagonist = SwampFever.Character.Protagonist.Init,
                CheckOnlyIf = SwampFever.Actions.StaticInstance.CheckOnlyIf,
                Paragraphs = SwampFever.Paragraphs.StaticInstance,
                Actions = SwampFever.Actions.StaticInstance,
                Constants = SwampFever.Constants.StaticInstance,
                Save = SwampFever.Character.Protagonist.Save,
                Load = SwampFever.Character.Protagonist.Load,
                SmallDisclaimer = "Пётр Прокошев, 2017",
                BookColor = "#ff557c48",
                Illustration = "SwampFever.jpg",
            },

            ["Наставники всегда правы"] = new Description
            {
                XmlBook = "Gamebooks/MentorsAlwaysRight.xml",
                Protagonist = MentorsAlwaysRight.Character.Protagonist.Init,
                CheckOnlyIf = MentorsAlwaysRight.Actions.StaticInstance.CheckOnlyIf,
                Paragraphs = MentorsAlwaysRight.Paragraphs.StaticInstance,
                Actions = MentorsAlwaysRight.Actions.StaticInstance,
                Constants = MentorsAlwaysRight.Constants.StaticInstance,
                Save = MentorsAlwaysRight.Character.Protagonist.Save,
                Load = MentorsAlwaysRight.Character.Protagonist.Load,
                SmallDisclaimer = "Роман Островерхов, 2011",
                BookColor = "#3662ae",
                Illustration = "MentorsAlwaysRight.jpg",
            },

            ["Легенды всегда врут"] = new Description
            {
                XmlBook = "Gamebooks/LegendsAlwaysLie.xml",
                Protagonist = LegendsAlwaysLie.Character.Protagonist.Init,
                CheckOnlyIf = LegendsAlwaysLie.Actions.StaticInstance.CheckOnlyIf,
                Paragraphs = LegendsAlwaysLie.Paragraphs.StaticInstance,
                Actions = LegendsAlwaysLie.Actions.StaticInstance,
                Constants = LegendsAlwaysLie.Constants.StaticInstance,
                Save = LegendsAlwaysLie.Character.Protagonist.Save,
                Load = LegendsAlwaysLie.Character.Protagonist.Load,
                SmallDisclaimer = "Роман Островерхов, 2012",
                BookColor = "#4c0000",
                Illustration = "LegendsAlwaysLie.jpg",
            },
            
            ["Вереница миров или выводы из закона Мэрфи"] = new Description
            {
                XmlBook = "Gamebooks/StringOfWorlds.xml",
                Protagonist = StringOfWorlds.Character.Protagonist.Init,
                CheckOnlyIf = StringOfWorlds.Actions.StaticInstance.CheckOnlyIf,
                Paragraphs = StringOfWorlds.Paragraphs.StaticInstance,
                Actions = StringOfWorlds.Actions.StaticInstance,
                Constants = StringOfWorlds.Constants.StaticInstance,
                Save = StringOfWorlds.Character.Protagonist.Save,
                Load = StringOfWorlds.Character.Protagonist.Load,
                SmallDisclaimer = "Ольга Голотвина, 1995",
                BookColor = "#990066",
                Illustration = "StringOfWorlds.jpg",
            },
            
            ["Три дороги"] = new Description
            {
                XmlBook = "Gamebooks/ThreePaths.xml",
                Protagonist = ThreePaths.Character.Protagonist.Init,
                CheckOnlyIf = ThreePaths.Actions.StaticInstance.CheckOnlyIf,
                Paragraphs = ThreePaths.Paragraphs.StaticInstance,
                Actions = ThreePaths.Actions.StaticInstance,
                Constants = ThreePaths.Constants.StaticInstance,
                Save = ThreePaths.Character.Protagonist.Save,
                Load = ThreePaths.Character.Protagonist.Load,
                SmallDisclaimer = "Александр Бутягин, Дмитрий Чистов, 1999",
                BookColor = "#009999",
                Illustration = "ThreePaths.jpg",
            },

            ["На невидимых фронтах"] = new Description
            {
                XmlBook = "Gamebooks/InvisibleFront.xml",
                Protagonist = InvisibleFront.Character.Protagonist.Init,
                CheckOnlyIf = InvisibleFront.Actions.StaticInstance.CheckOnlyIf,
                Paragraphs = InvisibleFront.Paragraphs.StaticInstance,
                Actions = InvisibleFront.Actions.StaticInstance,
                Constants = InvisibleFront.Constants.StaticInstance,
                Save = InvisibleFront.Character.Protagonist.Save,
                Load = InvisibleFront.Character.Protagonist.Load,
                SmallDisclaimer = "mmvvss, 2018",
                BookColor = "#d52b1e",
                FontColor = "#eede49",
                Illustration = "InvisibleFront.jpg",
            },

            ["Silent School"] = new Description
            {
                XmlBook = "Gamebooks/SilentSchool.xml",
                Protagonist = SilentSchool.Character.Protagonist.Init,
                CheckOnlyIf = SilentSchool.Actions.StaticInstance.CheckOnlyIf,
                Paragraphs = SilentSchool.Paragraphs.StaticInstance,
                Actions = SilentSchool.Actions.StaticInstance,
                Constants = SilentSchool.Constants.StaticInstance,
                Save = SilentSchool.Character.Protagonist.Save,
                Load = SilentSchool.Character.Protagonist.Load,
                SmallDisclaimer = "Роман Островерхов, 2013",
                BookColor = "#151515",
                Illustration = "SilentSchool.jpg",
            },
            
            ["Идущие на смерть"] = new Description
            {
                XmlBook = "Gamebooks/ThoseWhoAreAboutToDie.xml",
                Protagonist = ThoseWhoAreAboutToDie.Character.Protagonist.Init,
                CheckOnlyIf = ThoseWhoAreAboutToDie.Actions.StaticInstance.CheckOnlyIf,
                Paragraphs = ThoseWhoAreAboutToDie.Paragraphs.StaticInstance,
                Actions = ThoseWhoAreAboutToDie.Actions.StaticInstance,
                Constants = ThoseWhoAreAboutToDie.Constants.StaticInstance,
                Save = ThoseWhoAreAboutToDie.Character.Protagonist.Save,
                Load = ThoseWhoAreAboutToDie.Character.Protagonist.Load,
                SmallDisclaimer = "Александр Слюта, 2009",
                BookColor = "#fcdd76",
                FontColor = "#000000",
                Illustration = "ThoseWhoAreAboutToDie.jpg",
                ShowDisabledOption = true,
            },

            ["Остров Осьминогов"] = new Description
            {
                XmlBook = "Gamebooks/OctopusIsland.xml",
                Protagonist = OctopusIsland.Character.Protagonist.Init,
                CheckOnlyIf = OctopusIsland.Actions.StaticInstance.CheckOnlyIf,
                Paragraphs = OctopusIsland.Paragraphs.StaticInstance,
                Actions = OctopusIsland.Actions.StaticInstance,
                Constants = OctopusIsland.Constants.StaticInstance,
                Save = OctopusIsland.Character.Protagonist.Save,
                Load = OctopusIsland.Character.Protagonist.Load,
                SmallDisclaimer = "Филипп Эбли, 1992",
                BookColor = "#c93c20",
                Illustration = "OctopusIsland.jpg",
            },

            ["Разрушитель"] = new Description
            {
                XmlBook = "Gamebooks/CreatureOfHavoc.xml",
                Protagonist = CreatureOfHavoc.Character.Protagonist.Init,
                CheckOnlyIf = CreatureOfHavoc.Actions.StaticInstance.CheckOnlyIf,
                Paragraphs = CreatureOfHavoc.Paragraphs.StaticInstance,
                Actions = CreatureOfHavoc.Actions.StaticInstance,
                Constants = CreatureOfHavoc.Constants.StaticInstance,
                Save = CreatureOfHavoc.Character.Protagonist.Save,
                Load = CreatureOfHavoc.Character.Protagonist.Load,
                SmallDisclaimer = "Стив Джексон, 1986",
                BookColor = "#145334",
                Illustration = "CreatureOfHavoc.jpg",
            },

            ["Месть Альтея"] = new Description
            {
                XmlBook = "Gamebooks/BloodfeudOfAltheus.xml",
                Protagonist = BloodfeudOfAltheus.Character.Protagonist.Init,
                CheckOnlyIf = BloodfeudOfAltheus.Actions.StaticInstance.CheckOnlyIf,
                Paragraphs = BloodfeudOfAltheus.Paragraphs.StaticInstance,
                Actions = BloodfeudOfAltheus.Actions.StaticInstance,
                Constants = BloodfeudOfAltheus.Constants.StaticInstance,
                Save = BloodfeudOfAltheus.Character.Protagonist.Save,
                Load = BloodfeudOfAltheus.Character.Protagonist.Load,
                SmallDisclaimer = "Джон Баттерфилд и др., 1985",
                FullDisclaimer = "Авторы: Джон Баттерфилд, Дэвид Хонигман и Филип Паркер\n\n" +
                    "Переводчики: Мария Ерошкина, GalinaSol, Xpromt, Johny Lee, fermalion, Тара-сан, Jumangee, Ajenta, Эргистал, Anuta и другие",
                BookColor = "#ebd5b3",
                FontColor = "#000000",
                Illustration = "BloodfeudOfAltheus.jpg",
                ShowDisabledOption = true,
            },

            ["Симулятор пенсионерки"] = new Description
            {
                XmlBook = "Gamebooks/PensionerSimulator.xml",
                Protagonist = PensionerSimulator.Character.Protagonist.Init,
                CheckOnlyIf = PensionerSimulator.Actions.StaticInstance.CheckOnlyIf,
                Paragraphs = PensionerSimulator.Paragraphs.StaticInstance,
                Actions = PensionerSimulator.Actions.StaticInstance,
                Constants = PensionerSimulator.Constants.StaticInstance,
                Save = PensionerSimulator.Character.Protagonist.Save,
                Load = PensionerSimulator.Character.Protagonist.Load,
                SmallDisclaimer = "Zaratystra, the_arsonist, 2018",
                FullDisclaimer = "Симулятор пенсионерки:\nАвтор: Zaratystra, 2018\n\n" +
                    "Симулятор пенсионерки 2, Кровавая Охота:\nАвтор: the_arsonist, 2019\nПеревод: Мария Ерошкина\n" +
                    "Адаптация перевода: Zaratystra\nРедактор: Wervek",
                BookColor = "#030436",
                Illustration = "PensionerSimulator.jpg",
            },

            ["Владыка степей"] = new Description
            {
                XmlBook = "Gamebooks/LordOfTheSteppes.xml",
                Protagonist = LordOfTheSteppes.Character.Protagonist.Init,
                CheckOnlyIf = LordOfTheSteppes.Actions.StaticInstance.CheckOnlyIf,
                Paragraphs = LordOfTheSteppes.Paragraphs.StaticInstance,
                Actions = LordOfTheSteppes.Actions.StaticInstance,
                Constants = LordOfTheSteppes.Constants.StaticInstance,
                Save = LordOfTheSteppes.Character.Protagonist.Save,
                Load = LordOfTheSteppes.Character.Protagonist.Load,
                SmallDisclaimer = "Сергей Ступин, 2009",
                BookColor = "#b80f0a",
                Illustration = "LordOfTheSteppes.jpg",
            },

            ["Вой оборотня"] = new Description
            {
                XmlBook = "Gamebooks/HowlOfTheWerewolf.xml",
                Protagonist = HowlOfTheWerewolf.Character.Protagonist.Init,
                CheckOnlyIf = HowlOfTheWerewolf.Actions.StaticInstance.CheckOnlyIf,
                Paragraphs = HowlOfTheWerewolf.Paragraphs.StaticInstance,
                Actions = HowlOfTheWerewolf.Actions.StaticInstance,
                Constants = HowlOfTheWerewolf.Constants.StaticInstance,
                Save = HowlOfTheWerewolf.Character.Protagonist.Save,
                Load = HowlOfTheWerewolf.Character.Protagonist.Load,
                SmallDisclaimer = "Джонатан Грин, 2007",
                FullDisclaimer = "Автор: Джонатан Грин\n\nПереводчики: Rustem, Vo1t",
                BookColor = "#383e3b",
                Illustration = "HowlOfTheWerewolf.jpg",
            },

            ["Сердце льда"] = new Description
            {
                XmlBook = "Gamebooks/HeartOfIce.xml",
                Protagonist = HeartOfIce.Character.Protagonist.Init,
                CheckOnlyIf = HeartOfIce.Actions.StaticInstance.CheckOnlyIf,
                Paragraphs = HeartOfIce.Paragraphs.StaticInstance,
                Actions = HeartOfIce.Actions.StaticInstance,
                Constants = HeartOfIce.Constants.StaticInstance,
                Save = HeartOfIce.Character.Protagonist.Save,
                Load = HeartOfIce.Character.Protagonist.Load,
                SmallDisclaimer = "Дэйв Моррис, 1994",
                FullDisclaimer = "Авторы: Дэйв Моррис\n\nПереводчики: Мария Ерошкина, Ageres, Jumangee, Vo1t, Fermalion, Johny Lee и другие",
                BookColor = "#418988",
                Illustration = "HeartOfIce.jpg",
                ShowDisabledOption = true,
            },

            ["Крыса из нержавеющей стали"] = new Description
            {
                XmlBook = "Gamebooks/StainlessSteelRat.xml",
                Protagonist = StainlessSteelRat.Character.Protagonist.Init,
                CheckOnlyIf = StainlessSteelRat.Actions.StaticInstance.CheckOnlyIf,
                Paragraphs = StainlessSteelRat.Paragraphs.StaticInstance,
                Actions = StainlessSteelRat.Actions.StaticInstance,
                Constants = StainlessSteelRat.Constants.StaticInstance,
                Save = StainlessSteelRat.Character.Protagonist.Save,
                Load = StainlessSteelRat.Character.Protagonist.Load,
                SmallDisclaimer = "Гарри Гаррисон, 1985",
                FullDisclaimer = "Автор: Гарри Гаррисон\n\nПереводчик: Александр Жаворонков",
                BookColor = "#738595",
                Illustration = "StainlessSteelRat.jpg",
            },

            ["Последнее хокку"] = new Description
            {
                XmlBook = "Gamebooks/LastHokku.xml",
                Protagonist = LastHokku.Character.Protagonist.Init,
                CheckOnlyIf = LastHokku.Actions.StaticInstance.CheckOnlyIf,
                Paragraphs = LastHokku.Paragraphs.StaticInstance,
                Actions = LastHokku.Actions.StaticInstance,
                Constants = LastHokku.Constants.StaticInstance,
                Save = LastHokku.Character.Protagonist.Save,
                Load = LastHokku.Character.Protagonist.Load,
                SmallDisclaimer = "Юркий Слон, 2021",
                BookColor = "#deb887",
                FontColor = "#000000",
                Illustration = "LastHokku.jpg",
            },

            ["Генезис"] = new Description
            {
                XmlBook = "Gamebooks/Genesis.xml",
                Protagonist = Genesis.Character.Protagonist.Init,
                CheckOnlyIf = Genesis.Actions.StaticInstance.CheckOnlyIf,
                Paragraphs = Genesis.Paragraphs.StaticInstance,
                Actions = Genesis.Actions.StaticInstance,
                Constants = Genesis.Constants.StaticInstance,
                Save = Genesis.Character.Protagonist.Save,
                Load = Genesis.Character.Protagonist.Load,
                SmallDisclaimer = "Андрей Журавлёв, 2013",
                BookColor = "#202b41",
                Illustration = "Genesis.jpg",
                ShowDisabledOption = true,
            },
            
            ["Катарсис"] = new Description
            {
                XmlBook = "Gamebooks/Catharsis.xml",
                Protagonist = Catharsis.Character.Protagonist.Init,
                CheckOnlyIf = Catharsis.Actions.StaticInstance.CheckOnlyIf,
                Paragraphs = Catharsis.Paragraphs.StaticInstance,
                Actions = Catharsis.Actions.StaticInstance,
                Constants = Catharsis.Constants.StaticInstance,
                Save = Catharsis.Character.Protagonist.Save,
                Load = Catharsis.Character.Protagonist.Load,
                SmallDisclaimer = "Андрей Журавлёв, 2013",
                BookColor = "#51514b",
                Illustration = "Catharsis.jpg",
                ShowDisabledOption = true,
            },         
            
            ["По закону прерии"] = new Description
            {
                XmlBook = "Gamebooks/PrairieLaw.xml",
                Protagonist = PrairieLaw.Character.Protagonist.Init,
                CheckOnlyIf = PrairieLaw.Actions.StaticInstance.CheckOnlyIf,
                Paragraphs = PrairieLaw.Paragraphs.StaticInstance,
                Actions = PrairieLaw.Actions.StaticInstance,
                Constants = PrairieLaw.Constants.StaticInstance,
                Save = PrairieLaw.Character.Protagonist.Save,
                Load = PrairieLaw.Character.Protagonist.Load,
                SmallDisclaimer = "Ольга Голотвина, 1995",
                BookColor = "#b66247",
                Illustration = "PrairieLaw.jpg",
            },

            ["Оружие возмездия"] = new Description
            {
                XmlBook = "Gamebooks/VWeapons.xml",
                Protagonist = VWeapons.Character.Protagonist.Init,
                CheckOnlyIf = VWeapons.Actions.StaticInstance.CheckOnlyIf,
                Paragraphs = VWeapons.Paragraphs.StaticInstance,
                Actions = VWeapons.Actions.StaticInstance,
                Constants = VWeapons.Constants.StaticInstance,
                Save = VWeapons.Character.Protagonist.Save,
                Load = VWeapons.Character.Protagonist.Load,
                SmallDisclaimer = "Андрей Тишин, 2013",
                BookColor = "#ffffff",
                FontColor = "#000000",
                BorderColor = "#000000",
                Illustration = "VWeapons.jpg",
            },

            ["В краю непуганных медведей"] = new Description
            {
                XmlBook = "Gamebooks/LandOfUnwaryBears.xml",
                Protagonist = LandOfUnwaryBears.Character.Protagonist.Init,
                CheckOnlyIf = LandOfUnwaryBears.Actions.StaticInstance.CheckOnlyIf,
                Paragraphs = LandOfUnwaryBears.Paragraphs.StaticInstance,
                Actions = LandOfUnwaryBears.Actions.StaticInstance,
                Constants = LandOfUnwaryBears.Constants.StaticInstance,
                Save = LandOfUnwaryBears.Character.Protagonist.Save,
                Load = LandOfUnwaryBears.Character.Protagonist.Load,
                SmallDisclaimer = "Геннадий Логинов, 2020",
                BookColor = "#9e003a",
                Illustration = "LandOfUnwaryBears.jpg",
            },
        };

        public static List<string> GetBooks() => Books.Keys.ToList();

        public static Description GetDescription(string name) => Books[name];
    }
}
