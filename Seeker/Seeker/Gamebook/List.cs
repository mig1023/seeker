using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook
{
    class List
    {
        private static Dictionary<string, Description> Books = new Dictionary<string, Description>
        {
            ["Подземелья чёрного замка"] = new Description
            {
                XmlBook = "Gamebooks/BlackCastleDungeon.xml",
                Links = BlackCastleDungeon.Constants.GetLinks(),
                Disclaimer = "Дмитрий Браславский, 1991",
                Author = "Дмитрий Браславский",
                Illustration = "BlackCastleDungeon.jpg",
            },

            ["Тайна капитана Шелтона"] = new Description
            {
                XmlBook = "Gamebooks/CaptainSheltonsSecret.xml",
                Links = CaptainSheltonsSecret.Constants.GetLinks(),
                Disclaimer = "Дмитрий Браславский, 1992",
                Author = "Дмитрий Браславский",
                Illustration = "CaptainSheltonsSecret.jpg",
            },

            ["Верная шпага короля"] = new Description
            {
                XmlBook = "Gamebooks/FaithfulSwordOfTheKing.xml",
                Links = FaithfulSwordOfTheKing.Constants.GetLinks(),
                Disclaimer = "Дмитрий Браславский, 1995",
                Author = "Дмитрий Браславский",
                Illustration = "FaithfulSwordOfTheKing.jpg",
            },

            ["Приключения безбородого обманщика"] = new Description
            {
                XmlBook = "Gamebooks/AdventuresOfABeardlessDeceiver.xml",
                Links = AdventuresOfABeardlessDeceiver.Constants.GetLinks(),
                Disclaimer = "Владимир Сизиков, 2015",
                Author = "Владимир Сизиков",
                Illustration = "AdventuresOfABeardlessDeceiver.jpg",
            },

            ["Джунгарское нашествие"] = new Description
            {
                XmlBook = "Gamebooks/DzungarWar.xml",
                Links = DzungarWar.Constants.GetLinks(),
                Disclaimer = "Владимир Сизиков, 2016",
                Author = "Владимир Сизиков",
                Illustration = "DzungarWar.jpg",
            },

            ["Скала ужаса"] = new Description
            {
                XmlBook = "Gamebooks/RockOfTerror.xml",
                Links = RockOfTerror.Constants.GetLinks(),
                Disclaimer = "Дмитрий Тышевич, 2009",
                Author = "Дмитрий Тышевич",
                Illustration = "RockOfTerror.jpg",
            },

            ["Рандеву"] = new Description
            {
                XmlBook = "Gamebooks/RendezVous.xml",
                Links = RendezVous.Constants.GetLinks(),
                Disclaimer = "Ал Торо, 2020",
                Author = "Ал Торо",
                Translator = "Мария Ерошкина",
                Illustration = "RendezVous.jpg",
            },

            ["Болотная лихорадка"] = new Description
            {
                XmlBook = "Gamebooks/SwampFever.xml",
                Links = SwampFever.Constants.GetLinks(),
                Disclaimer = "Пётр Прокошев, 2017",
                Author = "Пётр Прокошев",
                Illustration = "SwampFever.jpg",
            },

            ["Наставники всегда правы"] = new Description
            {
                XmlBook = "Gamebooks/MentorsAlwaysRight.xml",
                Links = MentorsAlwaysRight.Constants.GetLinks(),
                Disclaimer = "Роман Островерхов, 2011",
                Author = "Роман Островерхов",
                Illustration = "MentorsAlwaysRight.jpg",
            },

            ["Легенды всегда врут"] = new Description
            {
                XmlBook = "Gamebooks/LegendsAlwaysLie.xml",
                Links = LegendsAlwaysLie.Constants.GetLinks(),
                Disclaimer = "Роман Островерхов, 2012",
                Author = "Роман Островерхов",
                Illustration = "LegendsAlwaysLie.jpg",
            },
            
            ["Вереница миров или выводы из закона Мэрфи"] = new Description
            {
                XmlBook = "Gamebooks/StringOfWorlds.xml",
                Links = StringOfWorlds.Constants.GetLinks(),
                Disclaimer = "Ольга Голотвина, 1995",
                Author = "Ольга Голотвина",
                Illustration = "StringOfWorlds.jpg",
            },
            
            ["Три дороги"] = new Description
            {
                XmlBook = "Gamebooks/ThreePaths.xml",
                Links = ThreePaths.Constants.GetLinks(),
                Disclaimer = "Александр Бутягин, Дмитрий Чистов, 1999",
                Authors = "Александр Бутягин, Дмитрий Чистов",
                Illustration = "ThreePaths.jpg",
            },

            ["На невидимых фронтах"] = new Description
            {
                XmlBook = "Gamebooks/InvisibleFront.xml",
                Links = InvisibleFront.Constants.GetLinks(),
                Disclaimer = "mmvvss, 2018",
                Author = "mmvvss",
                Illustration = "InvisibleFront.jpg",
            },

            ["Silent School"] = new Description
            {
                XmlBook = "Gamebooks/SilentSchool.xml",
                Links = SilentSchool.Constants.GetLinks(),
                Disclaimer = "Роман Островерхов, 2013",
                Author = "Роман Островерхов",
                Illustration = "SilentSchool.jpg",
            },
            
            ["Идущие на смерть"] = new Description
            {
                XmlBook = "Gamebooks/ThoseWhoAreAboutToDie.xml",
                Links = ThoseWhoAreAboutToDie.Constants.GetLinks(),
                Disclaimer = "Александр Слюта, 2009",
                Author = "Александр Слюта",
                Illustration = "ThoseWhoAreAboutToDie.jpg",
            },

            ["Остров Осьминогов"] = new Description
            {
                XmlBook = "Gamebooks/OctopusIsland.xml",
                Links = OctopusIsland.Constants.GetLinks(),
                Disclaimer = "Филипп Эбли, 1992",
                Author = "Филипп Эбли",
                Translator = "Неизвестен",
                Illustration = "OctopusIsland.jpg",
            },

            ["Разрушитель"] = new Description
            {
                XmlBook = "Gamebooks/CreatureOfHavoc.xml",
                Links = CreatureOfHavoc.Constants.GetLinks(),
                Disclaimer = "Стив Джексон, 1986",
                Author = "Стив Джексон",
                Translator = "Неизвестен",
                Illustration = "CreatureOfHavoc.jpg",
            },

            ["Месть Альтея"] = new Description
            {
                XmlBook = "Gamebooks/BloodfeudOfAltheus.xml",
                Links = BloodfeudOfAltheus.Constants.GetLinks(),
                Disclaimer = "Джон Баттерфилд и др., 1985",
                Authors = "Джон Баттерфилд, Дэвид Хонигман и Филип Паркер",
                Translators = "Мария Ерошкина, GalinaSol, Xpromt, Johny Lee, fermalion, Тара-сан, Jumangee, Ajenta, Эргистал, Anuta и другие",
                Illustration = "BloodfeudOfAltheus.jpg",
            },

            ["Симулятор пенсионерки"] = new Description
            {
                XmlBook = "Gamebooks/PensionerSimulator.xml",
                Links = PensionerSimulator.Constants.GetLinks(),
                Disclaimer = "Zaratystra, the_arsonist, 2018",
                Authors = "Оригинальная история: Zaratystra\nКровавая Охота:\nthe_arsonist",
                Translators = "Перевод: Мария Ерошкина\nАдаптация перевода: Zaratystra\nРедактор: Wervek",
                Illustration = "PensionerSimulator.jpg",
            },

            ["Владыка степей"] = new Description
            {
                XmlBook = "Gamebooks/LordOfTheSteppes.xml",
                Links = LordOfTheSteppes.Constants.GetLinks(),
                Disclaimer = "Сергей Ступин, 2009",
                Author = "Сергей Ступин",
                Illustration = "LordOfTheSteppes.jpg",
            },

            ["Вой оборотня"] = new Description
            {
                XmlBook = "Gamebooks/HowlOfTheWerewolf.xml",
                Links = HowlOfTheWerewolf.Constants.GetLinks(),
                Disclaimer = "Джонатан Грин, 2007",
                Author = "Джонатан Грин",
                Translators = "Rustem, Vo1t",
                Illustration = "HowlOfTheWerewolf.jpg",
            },

            ["Крыса из нержавеющей стали"] = new Description
            {
                XmlBook = "Gamebooks/StainlessSteelRat.xml",
                Links = StainlessSteelRat.Constants.GetLinks(),
                Disclaimer = "Гарри Гаррисон, 1985",
                Author = "Гарри Гаррисон",
                Translator = "Александр Жаворонков",
                Illustration = "StainlessSteelRat.jpg",
            },

            ["Последнее хокку"] = new Description
            {
                XmlBook = "Gamebooks/LastHokku.xml",
                Links = LastHokku.Constants.GetLinks(),
                Disclaimer = "Юркий Слон, 2021",
                Author = "Юркий Слон",
                Illustration = "LastHokku.jpg",
            },

            ["Генезис"] = new Description
            {
                XmlBook = "Gamebooks/Genesis.xml",
                Links = Genesis.Constants.GetLinks(),
                Disclaimer = "Андрей Журавлёв, 2013",
                Author = "Андрей Журавлёв",
                Illustration = "Genesis.jpg",
            },
            
            ["Катарсис"] = new Description
            {
                XmlBook = "Gamebooks/Catharsis.xml",
                Links = Catharsis.Constants.GetLinks(),
                Disclaimer = "Андрей Журавлёв, 2013",
                Author = "Андрей Журавлёв",
                Illustration = "Catharsis.jpg",
            },
            
            ["По закону прерии"] = new Description
            {
                XmlBook = "Gamebooks/PrairieLaw.xml",
                Links = PrairieLaw.Constants.GetLinks(),
                Disclaimer = "Ольга Голотвина, 1995",
                Author = "Ольга Голотвина",
                Illustration = "PrairieLaw.jpg",
            },

            ["Сердце льда"] = new Description
            {
                XmlBook = "Gamebooks/HeartOfIce.xml",
                Links = HeartOfIce.Constants.GetLinks(),
                Disclaimer = "Дэйв Моррис, 1994",
                Author = "Дэйв Моррис",
                Translators = "Мария Ерошкина, Ageres, Jumangee, Vo1t, Fermalion, Johny Lee и другие",
                Illustration = "HeartOfIce.jpg",
            },

            ["Оружие возмездия"] = new Description
            {
                XmlBook = "Gamebooks/VWeapons.xml",
                Links = VWeapons.Constants.GetLinks(),
                Disclaimer = "Андрей Тишин, 2013",
                Author = "Андрей Тишин",
                Illustration = "VWeapons.jpg",
            },

            ["В краю непуганных медведей"] = new Description
            {
                XmlBook = "Gamebooks/LandOfUnwaryBears.xml",
                Links = LandOfUnwaryBears.Constants.GetLinks(),
                Disclaimer = "Геннадий Логинов, 2020",
                Author = "Геннадий Логинов",
                Illustration = "LandOfUnwaryBears.jpg",
            },

            ["Турнир юнлингов"] = new Description
            {
                XmlBook = "Gamebooks/YounglingTournament.xml",
                Links = YounglingTournament.Constants.GetLinks(),
                Disclaimer = "Александр Андросенко, 2018",
                Author = "Александр Андросенко",
                Illustration = "YounglingTournament.jpg",
            },
        };

        public static List<string> GetBooks() => Books.Keys.ToList();

        public static Description GetDescription(string name)
        {
            Description book = Books[name];

            book.Text = book.Links.Constants.GetDescription();

            Abstract.IConstants data = book.Links.Constants;

            book.BookColor = data.GetColor(Game.Data.ColorTypes.BookColor);
            book.FontColor = data.GetColor(Game.Data.ColorTypes.BookFontColor);
            book.BorderColor = data.GetColor(Game.Data.ColorTypes.BookBorderColor);

            return book;
        }
    }
}
