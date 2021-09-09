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
                Illustration = "BlackCastleDungeon.jpg",
            },

            ["Тайна капитана Шелтона"] = new Description
            {
                XmlBook = "CaptainSheltonsSecret",
                Links = CaptainSheltonsSecret.Constants.GetLinks(),
                Author = "Дмитрий Браславский",
                Year = 1992,
                Illustration = "CaptainSheltonsSecret.jpg",
            },

            ["Верная шпага короля"] = new Description
            {
                XmlBook = "FaithfulSwordOfTheKing",
                Links = FaithfulSwordOfTheKing.Constants.GetLinks(),
                Author = "Дмитрий Браславский",
                Year = 1995,
                Illustration = "FaithfulSwordOfTheKing.jpg",
            },

            ["Приключения безбородого обманщика"] = new Description
            {
                XmlBook = "AdventuresOfABeardlessDeceiver",
                Links = AdventuresOfABeardlessDeceiver.Constants.GetLinks(),
                Author = "Владимир Сизиков",
                Year = 2015,
                Illustration = "AdventuresOfABeardlessDeceiver.jpg",
            },

            ["Джунгарское нашествие"] = new Description
            {
                XmlBook = "DzungarWar",
                Links = DzungarWar.Constants.GetLinks(),
                Author = "Владимир Сизиков",
                Year = 2016,
                Illustration = "DzungarWar.jpg",
            },

            ["Скала ужаса"] = new Description
            {
                XmlBook = "RockOfTerror",
                Links = RockOfTerror.Constants.GetLinks(),
                Author = "Дмитрий Тышевич",
                Year = 2009,
                Illustration = "RockOfTerror.jpg",
            },

            ["Рандеву"] = new Description
            {
                XmlBook = "RendezVous",
                Links = RendezVous.Constants.GetLinks(),
                Author = "Ал Торо",
                Year = 2020,
                Translator = "Мария Ерошкина",
                Illustration = "RendezVous.jpg",
            },

            ["Болотная лихорадка"] = new Description
            {
                XmlBook = "SwampFever",
                Links = SwampFever.Constants.GetLinks(),
                Author = "Пётр Прокошев",
                Year = 2017,
                Illustration = "SwampFever.jpg",
            },

            ["Наставники всегда правы"] = new Description
            {
                XmlBook = "MentorsAlwaysRight",
                Links = MentorsAlwaysRight.Constants.GetLinks(),
                Author = "Роман Островерхов",
                Year = 2011,
                Illustration = "MentorsAlwaysRight.jpg",
            },

            ["Легенды всегда врут"] = new Description
            {
                XmlBook = "LegendsAlwaysLie",
                Links = LegendsAlwaysLie.Constants.GetLinks(),
                Author = "Роман Островерхов",
                Year = 2012,
                Illustration = "LegendsAlwaysLie.jpg",
            },
            
            ["Вереница миров или выводы из закона Мэрфи"] = new Description
            {
                XmlBook = "StringOfWorlds",
                Links = StringOfWorlds.Constants.GetLinks(),
                Author = "Ольга Голотвина",
                Year = 1995,
                Illustration = "StringOfWorlds.jpg",
            },
            
            ["Три дороги"] = new Description
            {
                XmlBook = "ThreePaths",
                Links = ThreePaths.Constants.GetLinks(),
                Authors = "Александр Бутягин, Дмитрий Чистов",
                Year = 1999,
                Illustration = "ThreePaths.jpg",
            },

            ["На невидимых фронтах"] = new Description
            {
                XmlBook = "InvisibleFront",
                Links = InvisibleFront.Constants.GetLinks(),
                Author = "mmvvss",
                Year = 2018,
                Illustration = "InvisibleFront.jpg",
            },

            ["Silent School"] = new Description
            {
                XmlBook = "SilentSchool",
                Links = SilentSchool.Constants.GetLinks(),
                Author = "Роман Островерхов",
                Year = 2013,
                Illustration = "SilentSchool.jpg",
            },
            
            ["Идущие на смерть"] = new Description
            {
                XmlBook = "ThoseWhoAreAboutToDie",
                Links = ThoseWhoAreAboutToDie.Constants.GetLinks(),
                Author = "Александр Слюта",
                Year = 2009,
                Illustration = "ThoseWhoAreAboutToDie.jpg",
            },

            ["Остров Осьминогов"] = new Description
            {
                XmlBook = "OctopusIsland",
                Links = OctopusIsland.Constants.GetLinks(),
                Author = "Филипп Эбли",
                Year = 1992,
                Translator = "Неизвестен",
                Illustration = "OctopusIsland.jpg",
            },

            ["Разрушитель"] = new Description
            {
                XmlBook = "CreatureOfHavoc",
                Links = CreatureOfHavoc.Constants.GetLinks(),
                Author = "Стив Джексон",
                Year = 1986,
                Translator = "Неизвестен",
                Illustration = "CreatureOfHavoc.jpg",
            },

            ["Месть Альтея"] = new Description
            {
                XmlBook = "BloodfeudOfAltheus",
                Links = BloodfeudOfAltheus.Constants.GetLinks(),
                Authors = "Джон Баттерфилд, Дэвид Хонигман и Филип Паркер",
                Year = 1985,
                Translators = "Мария Ерошкина, GalinaSol, Xpromt, Johny Lee, fermalion, Тара-сан, Jumangee, Ajenta, Эргистал, Anuta и другие",
                Illustration = "BloodfeudOfAltheus.jpg",
            },

            ["Симулятор пенсионерки"] = new Description
            {
                XmlBook = "PensionerSimulator",
                Links = PensionerSimulator.Constants.GetLinks(),              
                Authors = "Zaratystra - оригинальная история,\nthe_arsonist - Кровавая охота",
                Year = 2018,
                Translators = "Перевод: Мария Ерошкина\nАдаптация перевода: Zaratystra\nРедактор: Wervek",
                Illustration = "PensionerSimulator.jpg",
            },

            ["Владыка степей"] = new Description
            {
                XmlBook = "LordOfTheSteppes",
                Links = LordOfTheSteppes.Constants.GetLinks(),
                Author = "Сергей Ступин",
                Year = 2009,
                Illustration = "LordOfTheSteppes.jpg",
            },

            ["Вой оборотня"] = new Description
            {
                XmlBook = "HowlOfTheWerewolf",
                Links = HowlOfTheWerewolf.Constants.GetLinks(),
                Author = "Джонатан Грин",
                Year = 2007,
                Translators = "Rustem, Vo1t",
                Illustration = "HowlOfTheWerewolf.jpg",
            },

            ["Крыса из нержавеющей стали"] = new Description
            {
                XmlBook = "StainlessSteelRat",
                Links = StainlessSteelRat.Constants.GetLinks(),
                Author = "Гарри Гаррисон",
                Year = 1985,
                Translator = "Александр Жаворонков",
                Illustration = "StainlessSteelRat.jpg",
            },

            ["Последнее хокку"] = new Description
            {
                XmlBook = "LastHokku",
                Links = LastHokku.Constants.GetLinks(),
                Author = "Юркий Слон",
                Year = 2021,
                Illustration = "LastHokku.jpg",
            },

            ["Генезис"] = new Description
            {
                XmlBook = "Genesis",
                Links = Genesis.Constants.GetLinks(),
                Author = "Андрей Журавлёв",
                Year = 2013,
                Illustration = "Genesis.jpg",
            },
            
            ["Катарсис"] = new Description
            {
                XmlBook = "Catharsis",
                Links = Catharsis.Constants.GetLinks(),
                Author = "Андрей Журавлёв",
                Year = 2013,
                Illustration = "Catharsis.jpg",
            },
            
            ["По закону прерии"] = new Description
            {
                XmlBook = "PrairieLaw",
                Links = PrairieLaw.Constants.GetLinks(),
                Author = "Ольга Голотвина",
                Year = 1995,
                Illustration = "PrairieLaw.jpg",
            },

            ["Сердце льда"] = new Description
            {
                XmlBook = "HeartOfIce",
                Links = HeartOfIce.Constants.GetLinks(),
                Author = "Дэйв Моррис",
                Year = 1994,
                Translators = "Мария Ерошкина, Ageres, Jumangee, Vo1t, Fermalion, Johny Lee и другие",
                Illustration = "HeartOfIce.jpg",
            },

            ["Оружие возмездия"] = new Description
            {
                XmlBook = "VWeapons",
                Links = VWeapons.Constants.GetLinks(),
                Author = "Андрей Тишин",
                Year = 2013,
                Illustration = "VWeapons.jpg",
            },

            ["В краю непуганных медведей"] = new Description
            {
                XmlBook = "LandOfUnwaryBears",
                Links = LandOfUnwaryBears.Constants.GetLinks(),
                Author = "Геннадий Логинов",
                Year = 2020,
                Illustration = "LandOfUnwaryBears.jpg",
            },

            ["Турнир юнлингов"] = new Description
            {
                XmlBook = "YounglingTournament",
                Links = YounglingTournament.Constants.GetLinks(),
                Author = "Александр Андросенко",
                Year = 2018,
                Illustration = "YounglingTournament.jpg",
            },
        };

        public static List<string> GetBooks() => Books.Keys.ToList();

        public static Description GetDescription(string name)
        {
            Description book = Books[name];

            book.Text = book.Links.Constants.GetDescription();
            book.XmlBook = string.Format("Gamebooks/{0}.xml", book.XmlBook);

            Abstract.IConstants data = book.Links.Constants;

            book.BookColor = data.GetColor(Game.Data.ColorTypes.BookColor);
            book.FontColor = data.GetColor(Game.Data.ColorTypes.BookFontColor);
            book.BorderColor = data.GetColor(Game.Data.ColorTypes.BookBorderColor);

            return book;
        }
    }
}
