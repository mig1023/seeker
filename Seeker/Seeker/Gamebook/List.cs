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
                Author = "Дмитрий Браславский",
                Year = 1991,
                Illustration = "BlackCastleDungeon.jpg",
            },

            ["Тайна капитана Шелтона"] = new Description
            {
                XmlBook = "Gamebooks/CaptainSheltonsSecret.xml",
                Links = CaptainSheltonsSecret.Constants.GetLinks(),
                Author = "Дмитрий Браславский",
                Year = 1992,
                Illustration = "CaptainSheltonsSecret.jpg",
            },

            ["Верная шпага короля"] = new Description
            {
                XmlBook = "Gamebooks/FaithfulSwordOfTheKing.xml",
                Links = FaithfulSwordOfTheKing.Constants.GetLinks(),
                Author = "Дмитрий Браславский",
                Year = 1995,
                Illustration = "FaithfulSwordOfTheKing.jpg",
            },

            ["Приключения безбородого обманщика"] = new Description
            {
                XmlBook = "Gamebooks/AdventuresOfABeardlessDeceiver.xml",
                Links = AdventuresOfABeardlessDeceiver.Constants.GetLinks(),
                Author = "Владимир Сизиков",
                Year = 2015,
                Illustration = "AdventuresOfABeardlessDeceiver.jpg",
            },

            ["Джунгарское нашествие"] = new Description
            {
                XmlBook = "Gamebooks/DzungarWar.xml",
                Links = DzungarWar.Constants.GetLinks(),
                Author = "Владимир Сизиков",
                Year = 2016,
                Illustration = "DzungarWar.jpg",
            },

            ["Скала ужаса"] = new Description
            {
                XmlBook = "Gamebooks/RockOfTerror.xml",
                Links = RockOfTerror.Constants.GetLinks(),
                Author = "Дмитрий Тышевич",
                Year = 2009,
                Illustration = "RockOfTerror.jpg",
            },

            ["Рандеву"] = new Description
            {
                XmlBook = "Gamebooks/RendezVous.xml",
                Links = RendezVous.Constants.GetLinks(),
                Author = "Ал Торо",
                Year = 2020,
                Translator = "Мария Ерошкина",
                Illustration = "RendezVous.jpg",
            },

            ["Болотная лихорадка"] = new Description
            {
                XmlBook = "Gamebooks/SwampFever.xml",
                Links = SwampFever.Constants.GetLinks(),
                Author = "Пётр Прокошев",
                Year = 2017,
                Illustration = "SwampFever.jpg",
            },

            ["Наставники всегда правы"] = new Description
            {
                XmlBook = "Gamebooks/MentorsAlwaysRight.xml",
                Links = MentorsAlwaysRight.Constants.GetLinks(),
                Author = "Роман Островерхов",
                Year = 2011,
                Illustration = "MentorsAlwaysRight.jpg",
            },

            ["Легенды всегда врут"] = new Description
            {
                XmlBook = "Gamebooks/LegendsAlwaysLie.xml",
                Links = LegendsAlwaysLie.Constants.GetLinks(),
                Author = "Роман Островерхов",
                Year = 2012,
                Illustration = "LegendsAlwaysLie.jpg",
            },
            
            ["Вереница миров или выводы из закона Мэрфи"] = new Description
            {
                XmlBook = "Gamebooks/StringOfWorlds.xml",
                Links = StringOfWorlds.Constants.GetLinks(),
                Author = "Ольга Голотвина",
                Year = 1995,
                Illustration = "StringOfWorlds.jpg",
            },
            
            ["Три дороги"] = new Description
            {
                XmlBook = "Gamebooks/ThreePaths.xml",
                Links = ThreePaths.Constants.GetLinks(),
                Authors = "Александр Бутягин, Дмитрий Чистов",
                Year = 1999,
                Illustration = "ThreePaths.jpg",
            },

            ["На невидимых фронтах"] = new Description
            {
                XmlBook = "Gamebooks/InvisibleFront.xml",
                Links = InvisibleFront.Constants.GetLinks(),
                Author = "mmvvss",
                Year = 2018,
                Illustration = "InvisibleFront.jpg",
            },

            ["Silent School"] = new Description
            {
                XmlBook = "Gamebooks/SilentSchool.xml",
                Links = SilentSchool.Constants.GetLinks(),
                Author = "Роман Островерхов",
                Year = 2013,
                Illustration = "SilentSchool.jpg",
            },
            
            ["Идущие на смерть"] = new Description
            {
                XmlBook = "Gamebooks/ThoseWhoAreAboutToDie.xml",
                Links = ThoseWhoAreAboutToDie.Constants.GetLinks(),
                Author = "Александр Слюта",
                Year = 2009,
                Illustration = "ThoseWhoAreAboutToDie.jpg",
            },

            ["Остров Осьминогов"] = new Description
            {
                XmlBook = "Gamebooks/OctopusIsland.xml",
                Links = OctopusIsland.Constants.GetLinks(),
                Author = "Филипп Эбли",
                Year = 1992,
                Translator = "Неизвестен",
                Illustration = "OctopusIsland.jpg",
            },

            ["Разрушитель"] = new Description
            {
                XmlBook = "Gamebooks/CreatureOfHavoc.xml",
                Links = CreatureOfHavoc.Constants.GetLinks(),
                Author = "Стив Джексон",
                Year = 1986,
                Translator = "Неизвестен",
                Illustration = "CreatureOfHavoc.jpg",
            },

            ["Месть Альтея"] = new Description
            {
                XmlBook = "Gamebooks/BloodfeudOfAltheus.xml",
                Links = BloodfeudOfAltheus.Constants.GetLinks(),
                Authors = "Джон Баттерфилд, Дэвид Хонигман и Филип Паркер",
                Year = 1985,
                Translators = "Мария Ерошкина, GalinaSol, Xpromt, Johny Lee, fermalion, Тара-сан, Jumangee, Ajenta, Эргистал, Anuta и другие",
                Illustration = "BloodfeudOfAltheus.jpg",
            },

            ["Симулятор пенсионерки"] = new Description
            {
                XmlBook = "Gamebooks/PensionerSimulator.xml",
                Links = PensionerSimulator.Constants.GetLinks(),              
                Authors = "Zaratystra - оригинальная история,\nthe_arsonist - Кровавая охота",
                Year = 2018,
                Translators = "Перевод: Мария Ерошкина\nАдаптация перевода: Zaratystra\nРедактор: Wervek",
                Illustration = "PensionerSimulator.jpg",
            },

            ["Владыка степей"] = new Description
            {
                XmlBook = "Gamebooks/LordOfTheSteppes.xml",
                Links = LordOfTheSteppes.Constants.GetLinks(),
                Author = "Сергей Ступин",
                Year = 2009,
                Illustration = "LordOfTheSteppes.jpg",
            },

            ["Вой оборотня"] = new Description
            {
                XmlBook = "Gamebooks/HowlOfTheWerewolf.xml",
                Links = HowlOfTheWerewolf.Constants.GetLinks(),
                Author = "Джонатан Грин",
                Year = 2007,
                Translators = "Rustem, Vo1t",
                Illustration = "HowlOfTheWerewolf.jpg",
            },

            ["Крыса из нержавеющей стали"] = new Description
            {
                XmlBook = "Gamebooks/StainlessSteelRat.xml",
                Links = StainlessSteelRat.Constants.GetLinks(),
                Author = "Гарри Гаррисон",
                Year = 1985,
                Translator = "Александр Жаворонков",
                Illustration = "StainlessSteelRat.jpg",
            },

            ["Последнее хокку"] = new Description
            {
                XmlBook = "Gamebooks/LastHokku.xml",
                Links = LastHokku.Constants.GetLinks(),
                Author = "Юркий Слон",
                Year = 2021,
                Illustration = "LastHokku.jpg",
            },

            ["Генезис"] = new Description
            {
                XmlBook = "Gamebooks/Genesis.xml",
                Links = Genesis.Constants.GetLinks(),
                Author = "Андрей Журавлёв",
                Year = 2013,
                Illustration = "Genesis.jpg",
            },
            
            ["Катарсис"] = new Description
            {
                XmlBook = "Gamebooks/Catharsis.xml",
                Links = Catharsis.Constants.GetLinks(),
                Author = "Андрей Журавлёв",
                Year = 2013,
                Illustration = "Catharsis.jpg",
            },
            
            ["По закону прерии"] = new Description
            {
                XmlBook = "Gamebooks/PrairieLaw.xml",
                Links = PrairieLaw.Constants.GetLinks(),
                Author = "Ольга Голотвина",
                Year = 1995,
                Illustration = "PrairieLaw.jpg",
            },

            ["Сердце льда"] = new Description
            {
                XmlBook = "Gamebooks/HeartOfIce.xml",
                Links = HeartOfIce.Constants.GetLinks(),
                Author = "Дэйв Моррис",
                Year = 1994,
                Translators = "Мария Ерошкина, Ageres, Jumangee, Vo1t, Fermalion, Johny Lee и другие",
                Illustration = "HeartOfIce.jpg",
            },

            ["Оружие возмездия"] = new Description
            {
                XmlBook = "Gamebooks/VWeapons.xml",
                Links = VWeapons.Constants.GetLinks(),
                Author = "Андрей Тишин",
                Year = 2013,
                Illustration = "VWeapons.jpg",
            },

            ["В краю непуганных медведей"] = new Description
            {
                XmlBook = "Gamebooks/LandOfUnwaryBears.xml",
                Links = LandOfUnwaryBears.Constants.GetLinks(),
                Author = "Геннадий Логинов",
                Year = 2020,
                Illustration = "LandOfUnwaryBears.jpg",
            },

            ["Турнир юнлингов"] = new Description
            {
                XmlBook = "Gamebooks/YounglingTournament.xml",
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

            Abstract.IConstants data = book.Links.Constants;

            book.BookColor = data.GetColor(Game.Data.ColorTypes.BookColor);
            book.FontColor = data.GetColor(Game.Data.ColorTypes.BookFontColor);
            book.BorderColor = data.GetColor(Game.Data.ColorTypes.BookBorderColor);

            return book;
        }
    }
}
