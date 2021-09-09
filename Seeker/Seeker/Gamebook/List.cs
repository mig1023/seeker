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
                XmlBook = "BlackCastleDungeon",
                Links = BlackCastleDungeon.Constants.GetLinks(),
                Author = "Дмитрий Браславский",
                Year = 1991,
            },

            ["Тайна капитана Шелтона"] = new Description
            {
                XmlBook = "CaptainSheltonsSecret",
                Links = CaptainSheltonsSecret.Constants.GetLinks(),
                Author = "Дмитрий Браславский",
                Year = 1992,
            },

            ["Верная шпага короля"] = new Description
            {
                XmlBook = "FaithfulSwordOfTheKing",
                Links = FaithfulSwordOfTheKing.Constants.GetLinks(),
                Author = "Дмитрий Браславский",
                Year = 1995,
            },

            ["Приключения безбородого обманщика"] = new Description
            {
                XmlBook = "AdventuresOfABeardlessDeceiver",
                Links = AdventuresOfABeardlessDeceiver.Constants.GetLinks(),
                Author = "Владимир Сизиков",
                Year = 2015,
            },

            ["Джунгарское нашествие"] = new Description
            {
                XmlBook = "DzungarWar",
                Links = DzungarWar.Constants.GetLinks(),
                Author = "Владимир Сизиков",
                Year = 2016,
            },

            ["Скала ужаса"] = new Description
            {
                XmlBook = "RockOfTerror",
                Links = RockOfTerror.Constants.GetLinks(),
                Author = "Дмитрий Тышевич",
                Year = 2009,
            },

            ["Рандеву"] = new Description
            {
                XmlBook = "RendezVous",
                Links = RendezVous.Constants.GetLinks(),
                Author = "Ал Торо",
                Year = 2020,
                Translator = "Мария Ерошкина",
            },

            ["Болотная лихорадка"] = new Description
            {
                XmlBook = "SwampFever",
                Links = SwampFever.Constants.GetLinks(),
                Author = "Пётр Прокошев",
                Year = 2017,
            },

            ["Наставники всегда правы"] = new Description
            {
                XmlBook = "MentorsAlwaysRight",
                Links = MentorsAlwaysRight.Constants.GetLinks(),
                Author = "Роман Островерхов",
                Year = 2011,
            },

            ["Легенды всегда врут"] = new Description
            {
                XmlBook = "LegendsAlwaysLie",
                Links = LegendsAlwaysLie.Constants.GetLinks(),
                Author = "Роман Островерхов",
                Year = 2012,
            },
            
            ["Вереница миров или выводы из закона Мэрфи"] = new Description
            {
                XmlBook = "StringOfWorlds",
                Links = StringOfWorlds.Constants.GetLinks(),
                Author = "Ольга Голотвина",
                Year = 1995,
            },
            
            ["Три дороги"] = new Description
            {
                XmlBook = "ThreePaths",
                Links = ThreePaths.Constants.GetLinks(),
                Authors = "Александр Бутягин, Дмитрий Чистов",
                Year = 1999,
            },

            ["На невидимых фронтах"] = new Description
            {
                XmlBook = "InvisibleFront",
                Links = InvisibleFront.Constants.GetLinks(),
                Author = "mmvvss",
                Year = 2018,
            },

            ["Silent School"] = new Description
            {
                XmlBook = "SilentSchool",
                Links = SilentSchool.Constants.GetLinks(),
                Author = "Роман Островерхов",
                Year = 2013,
            },
            
            ["Идущие на смерть"] = new Description
            {
                XmlBook = "ThoseWhoAreAboutToDie",
                Links = ThoseWhoAreAboutToDie.Constants.GetLinks(),
                Author = "Александр Слюта",
                Year = 2009,
            },

            ["Остров Осьминогов"] = new Description
            {
                XmlBook = "OctopusIsland",
                Links = OctopusIsland.Constants.GetLinks(),
                Author = "Филипп Эбли",
                Year = 1992,
                Translator = "Неизвестен",
            },

            ["Разрушитель"] = new Description
            {
                XmlBook = "CreatureOfHavoc",
                Links = CreatureOfHavoc.Constants.GetLinks(),
                Author = "Стив Джексон",
                Year = 1986,
                Translator = "Неизвестен",
            },

            ["Месть Альтея"] = new Description
            {
                XmlBook = "BloodfeudOfAltheus",
                Links = BloodfeudOfAltheus.Constants.GetLinks(),
                Authors = "Джон Баттерфилд, Дэвид Хонигман и Филип Паркер",
                Year = 1985,
                Translators = "Мария Ерошкина, GalinaSol, Xpromt, Johny Lee, fermalion, Тара-сан, Jumangee, Ajenta, Эргистал, Anuta и другие",
            },

            ["Симулятор пенсионерки"] = new Description
            {
                XmlBook = "PensionerSimulator",
                Links = PensionerSimulator.Constants.GetLinks(),              
                Authors = "Zaratystra - оригинальная история,\nthe_arsonist - Кровавая охота",
                Year = 2018,
                Translators = "Перевод: Мария Ерошкина\nАдаптация перевода: Zaratystra\nРедактор: Wervek",
            },

            ["Владыка степей"] = new Description
            {
                XmlBook = "LordOfTheSteppes",
                Links = LordOfTheSteppes.Constants.GetLinks(),
                Author = "Сергей Ступин",
                Year = 2009,
            },

            ["Вой оборотня"] = new Description
            {
                XmlBook = "HowlOfTheWerewolf",
                Links = HowlOfTheWerewolf.Constants.GetLinks(),
                Author = "Джонатан Грин",
                Year = 2007,
                Translators = "Rustem, Vo1t",
            },

            ["Крыса из нержавеющей стали"] = new Description
            {
                XmlBook = "StainlessSteelRat",
                Links = StainlessSteelRat.Constants.GetLinks(),
                Author = "Гарри Гаррисон",
                Year = 1985,
                Translator = "Александр Жаворонков",
            },

            ["Последнее хокку"] = new Description
            {
                XmlBook = "LastHokku",
                Links = LastHokku.Constants.GetLinks(),
                Author = "Юркий Слон",
                Year = 2021,
            },

            ["Генезис"] = new Description
            {
                XmlBook = "Genesis",
                Links = Genesis.Constants.GetLinks(),
                Author = "Андрей Журавлёв",
                Year = 2013,
            },
            
            ["Катарсис"] = new Description
            {
                XmlBook = "Catharsis",
                Links = Catharsis.Constants.GetLinks(),
                Author = "Андрей Журавлёв",
                Year = 2013,
            },
            
            ["По закону прерии"] = new Description
            {
                XmlBook = "PrairieLaw",
                Links = PrairieLaw.Constants.GetLinks(),
                Author = "Ольга Голотвина",
                Year = 1995,
            },

            ["Сердце льда"] = new Description
            {
                XmlBook = "HeartOfIce",
                Links = HeartOfIce.Constants.GetLinks(),
                Author = "Дэйв Моррис",
                Year = 1994,
                Translators = "Мария Ерошкина, Ageres, Jumangee, Vo1t, Fermalion, Johny Lee и другие",
            },

            ["Оружие возмездия"] = new Description
            {
                XmlBook = "VWeapons",
                Links = VWeapons.Constants.GetLinks(),
                Author = "Андрей Тишин",
                Year = 2013,
            },

            ["В краю непуганных медведей"] = new Description
            {
                XmlBook = "LandOfUnwaryBears",
                Links = LandOfUnwaryBears.Constants.GetLinks(),
                Author = "Геннадий Логинов",
                Year = 2020,
            },

            ["Турнир юнлингов"] = new Description
            {
                XmlBook = "YounglingTournament",
                Links = YounglingTournament.Constants.GetLinks(),
                Author = "Александр Андросенко",
                Year = 2018,
            },
        };

        public static List<string> GetBooks() => Books.Keys.ToList();

        public static Description GetDescription(string name)
        {
            Description book = Books[name];

            book.Text = book.Links.Constants.GetDescription();
            book.Illustration = string.Format("{0}.jpg", book.XmlBook);
            book.XmlBook = string.Format("Gamebooks/{0}.xml", book.XmlBook);
           
            Abstract.IConstants data = book.Links.Constants;

            book.BookColor = data.GetColor(Game.Data.ColorTypes.BookColor);
            book.FontColor = data.GetColor(Game.Data.ColorTypes.BookFontColor);
            book.BorderColor = data.GetColor(Game.Data.ColorTypes.BookBorderColor);

            return book;
        }
    }
}
