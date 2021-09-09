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
                Book = "BlackCastleDungeon",
                Links = BlackCastleDungeon.Constants.GetLinks(),
                Author = "Дмитрий Браславский",
                Year = 1991,
            },

            ["Тайна капитана Шелтона"] = new Description
            {
                Book = "CaptainSheltonsSecret",
                Links = CaptainSheltonsSecret.Constants.GetLinks(),
                Author = "Дмитрий Браславский",
                Year = 1992,
            },

            ["Верная шпага короля"] = new Description
            {
                Book = "FaithfulSwordOfTheKing",
                Links = FaithfulSwordOfTheKing.Constants.GetLinks(),
                Author = "Дмитрий Браславский",
                Year = 1995,
            },

            ["Приключения безбородого обманщика"] = new Description
            {
                Book = "AdventuresOfABeardlessDeceiver",
                Links = AdventuresOfABeardlessDeceiver.Constants.GetLinks(),
                Author = "Владимир Сизиков",
                Year = 2015,
            },

            ["Джунгарское нашествие"] = new Description
            {
                Book = "DzungarWar",
                Links = DzungarWar.Constants.GetLinks(),
                Author = "Владимир Сизиков",
                Year = 2016,
            },

            ["Скала ужаса"] = new Description
            {
                Book = "RockOfTerror",
                Links = RockOfTerror.Constants.GetLinks(),
                Author = "Дмитрий Тышевич",
                Year = 2009,
            },

            ["Рандеву"] = new Description
            {
                Book = "RendezVous",
                Links = RendezVous.Constants.GetLinks(),
                Author = "Ал Торо",
                Year = 2020,
                Translator = "Мария Ерошкина",
            },

            ["Болотная лихорадка"] = new Description
            {
                Book = "SwampFever",
                Links = SwampFever.Constants.GetLinks(),
                Author = "Пётр Прокошев",
                Year = 2017,
            },

            ["Наставники всегда правы"] = new Description
            {
                Book = "MentorsAlwaysRight",
                Links = MentorsAlwaysRight.Constants.GetLinks(),
                Author = "Роман Островерхов",
                Year = 2011,
            },

            ["Легенды всегда врут"] = new Description
            {
                Book = "LegendsAlwaysLie",
                Links = LegendsAlwaysLie.Constants.GetLinks(),
                Author = "Роман Островерхов",
                Year = 2012,
            },
            
            ["Вереница миров или выводы из закона Мэрфи"] = new Description
            {
                Book = "StringOfWorlds",
                Links = StringOfWorlds.Constants.GetLinks(),
                Author = "Ольга Голотвина",
                Year = 1995,
            },
            
            ["Три дороги"] = new Description
            {
                Book = "ThreePaths",
                Links = ThreePaths.Constants.GetLinks(),
                Authors = "Александр Бутягин, Дмитрий Чистов",
                Year = 1999,
            },

            ["На невидимых фронтах"] = new Description
            {
                Book = "InvisibleFront",
                Links = InvisibleFront.Constants.GetLinks(),
                Author = "mmvvss",
                Year = 2018,
            },

            ["Silent School"] = new Description
            {
                Book = "SilentSchool",
                Links = SilentSchool.Constants.GetLinks(),
                Author = "Роман Островерхов",
                Year = 2013,
            },
            
            ["Идущие на смерть"] = new Description
            {
                Book = "ThoseWhoAreAboutToDie",
                Links = ThoseWhoAreAboutToDie.Constants.GetLinks(),
                Author = "Александр Слюта",
                Year = 2009,
            },

            ["Остров Осьминогов"] = new Description
            {
                Book = "OctopusIsland",
                Links = OctopusIsland.Constants.GetLinks(),
                Author = "Филипп Эбли",
                Year = 1992,
                Translator = "Неизвестен",
            },

            ["Разрушитель"] = new Description
            {
                Book = "CreatureOfHavoc",
                Links = CreatureOfHavoc.Constants.GetLinks(),
                Author = "Стив Джексон",
                Year = 1986,
                Translator = "Неизвестен",
            },

            ["Месть Альтея"] = new Description
            {
                Book = "BloodfeudOfAltheus",
                Links = BloodfeudOfAltheus.Constants.GetLinks(),
                Authors = "Джон Баттерфилд, Дэвид Хонигман и Филип Паркер",
                Year = 1985,
                Translators = "Мария Ерошкина, GalinaSol, Xpromt, Johny Lee, fermalion, Тара-сан, Jumangee, Ajenta, Эргистал, Anuta и другие",
            },

            ["Симулятор пенсионерки"] = new Description
            {
                Book = "PensionerSimulator",
                Links = PensionerSimulator.Constants.GetLinks(),              
                Authors = "Zaratystra - оригинальная история,\nthe_arsonist - Кровавая охота",
                Year = 2018,
                Translators = "Перевод: Мария Ерошкина\nАдаптация перевода: Zaratystra\nРедактор: Wervek",
            },

            ["Владыка степей"] = new Description
            {
                Book = "LordOfTheSteppes",
                Links = LordOfTheSteppes.Constants.GetLinks(),
                Author = "Сергей Ступин",
                Year = 2009,
            },

            ["Вой оборотня"] = new Description
            {
                Book = "HowlOfTheWerewolf",
                Links = HowlOfTheWerewolf.Constants.GetLinks(),
                Author = "Джонатан Грин",
                Year = 2007,
                Translators = "Rustem, Vo1t",
            },

            ["Крыса из нержавеющей стали"] = new Description
            {
                Book = "StainlessSteelRat",
                Links = StainlessSteelRat.Constants.GetLinks(),
                Author = "Гарри Гаррисон",
                Year = 1985,
                Translator = "Александр Жаворонков",
            },

            ["Последнее хокку"] = new Description
            {
                Book = "LastHokku",
                Links = LastHokku.Constants.GetLinks(),
                Author = "Юркий Слон",
                Year = 2021,
            },

            ["Генезис"] = new Description
            {
                Book = "Genesis",
                Links = Genesis.Constants.GetLinks(),
                Author = "Андрей Журавлёв",
                Year = 2013,
            },
            
            ["Катарсис"] = new Description
            {
                Book = "Catharsis",
                Links = Catharsis.Constants.GetLinks(),
                Author = "Андрей Журавлёв",
                Year = 2013,
            },
            
            ["По закону прерии"] = new Description
            {
                Book = "PrairieLaw",
                Links = PrairieLaw.Constants.GetLinks(),
                Author = "Ольга Голотвина",
                Year = 1995,
            },

            ["Сердце льда"] = new Description
            {
                Book = "HeartOfIce",
                Links = HeartOfIce.Constants.GetLinks(),
                Author = "Дэйв Моррис",
                Year = 1994,
                Translators = "Мария Ерошкина, Ageres, Jumangee, Vo1t, Fermalion, Johny Lee и другие",
            },

            ["Оружие возмездия"] = new Description
            {
                Book = "VWeapons",
                Links = VWeapons.Constants.GetLinks(),
                Author = "Андрей Тишин",
                Year = 2013,
            },

            ["В краю непуганных медведей"] = new Description
            {
                Book = "LandOfUnwaryBears",
                Links = LandOfUnwaryBears.Constants.GetLinks(),
                Author = "Геннадий Логинов",
                Year = 2020,
            },

            ["Турнир юнлингов"] = new Description
            {
                Book = "YounglingTournament",
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
            book.Illustration = string.Format("{0}.jpg", book.Book);
            book.XmlBook = string.Format("Gamebooks/{0}.xml", book.Book);
           
            Abstract.IConstants data = book.Links.Constants;

            book.BookColor = data.GetColor(Game.Data.ColorTypes.BookColor);
            book.FontColor = data.GetColor(Game.Data.ColorTypes.BookFontColor);
            book.BorderColor = data.GetColor(Game.Data.ColorTypes.BookBorderColor);

            return book;
        }
    }
}
