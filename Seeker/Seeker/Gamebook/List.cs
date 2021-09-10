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
            },

            ["Тайна капитана Шелтона"] = new Description
            {
                Book = "CaptainSheltonsSecret",
                Links = CaptainSheltonsSecret.Constants.GetLinks(),
                Author = "Дмитрий Браславский",
            },

            ["Верная шпага короля"] = new Description
            {
                Book = "FaithfulSwordOfTheKing",
                Links = FaithfulSwordOfTheKing.Constants.GetLinks(),
                Author = "Дмитрий Браславский",
            },

            ["Приключения безбородого обманщика"] = new Description
            {
                Book = "AdventuresOfABeardlessDeceiver",
                Links = AdventuresOfABeardlessDeceiver.Constants.GetLinks(),
                Author = "Владимир Сизиков",
            },

            ["Джунгарское нашествие"] = new Description
            {
                Book = "DzungarWar",
                Links = DzungarWar.Constants.GetLinks(),
                Author = "Владимир Сизиков",
            },

            ["Скала ужаса"] = new Description
            {
                Book = "RockOfTerror",
                Links = RockOfTerror.Constants.GetLinks(),
                Author = "Дмитрий Тышевич",
            },

            ["Рандеву"] = new Description
            {
                Book = "RendezVous",
                Links = RendezVous.Constants.GetLinks(),
                Author = "Ал Торо",
                Translator = "Мария Ерошкина",
            },

            ["Болотная лихорадка"] = new Description
            {
                Book = "SwampFever",
                Links = SwampFever.Constants.GetLinks(),
                Author = "Пётр Прокошев",
            },

            ["Наставники всегда правы"] = new Description
            {
                Book = "MentorsAlwaysRight",
                Links = MentorsAlwaysRight.Constants.GetLinks(),
                Author = "Роман Островерхов",
            },

            ["Легенды всегда врут"] = new Description
            {
                Book = "LegendsAlwaysLie",
                Links = LegendsAlwaysLie.Constants.GetLinks(),
                Author = "Роман Островерхов",
            },
            
            ["Вереница миров или выводы из закона Мэрфи"] = new Description
            {
                Book = "StringOfWorlds",
                Links = StringOfWorlds.Constants.GetLinks(),
                Author = "Ольга Голотвина",
            },
            
            ["Три дороги"] = new Description
            {
                Book = "ThreePaths",
                Links = ThreePaths.Constants.GetLinks(),
                Authors = "Александр Бутягин, Дмитрий Чистов",
            },

            ["На невидимых фронтах"] = new Description
            {
                Book = "InvisibleFront",
                Links = InvisibleFront.Constants.GetLinks(),
                Author = "mmvvss",
            },

            ["Silent School"] = new Description
            {
                Book = "SilentSchool",
                Links = SilentSchool.Constants.GetLinks(),
                Author = "Роман Островерхов",
            },
            
            ["Идущие на смерть"] = new Description
            {
                Book = "ThoseWhoAreAboutToDie",
                Links = ThoseWhoAreAboutToDie.Constants.GetLinks(),
                Author = "Александр Слюта",
            },

            ["Остров Осьминогов"] = new Description
            {
                Book = "OctopusIsland",
                Links = OctopusIsland.Constants.GetLinks(),
                Author = "Филипп Эбли",
                Translator = "Неизвестен",
            },

            ["Разрушитель"] = new Description
            {
                Book = "CreatureOfHavoc",
                Links = CreatureOfHavoc.Constants.GetLinks(),
                Author = "Стив Джексон",
                Translator = "Неизвестен",
            },

            ["Месть Альтея"] = new Description
            {
                Book = "BloodfeudOfAltheus",
                Links = BloodfeudOfAltheus.Constants.GetLinks(),
                Authors = "Джон Баттерфилд, Дэвид Хонигман и Филип Паркер",
                Translators = "Мария Ерошкина, GalinaSol, Xpromt, Johny Lee, fermalion, Тара-сан, Jumangee, Ajenta, Эргистал, Anuta и другие",
            },

            ["Симулятор пенсионерки"] = new Description
            {
                Book = "PensionerSimulator",
                Links = PensionerSimulator.Constants.GetLinks(),              
                Authors = "Zaratystra - оригинальная история,\nthe_arsonist - Кровавая охота",
                Translators = "Перевод: Мария Ерошкина\nАдаптация перевода: Zaratystra\nРедактор: Wervek",
            },

            ["Владыка степей"] = new Description
            {
                Book = "LordOfTheSteppes",
                Links = LordOfTheSteppes.Constants.GetLinks(),
                Author = "Сергей Ступин",
            },

            ["Вой оборотня"] = new Description
            {
                Book = "HowlOfTheWerewolf",
                Links = HowlOfTheWerewolf.Constants.GetLinks(),
                Author = "Джонатан Грин",
                Translators = "Rustem, Vo1t",
            },

            ["Крыса из нержавеющей стали"] = new Description
            {
                Book = "StainlessSteelRat",
                Links = StainlessSteelRat.Constants.GetLinks(),
                Author = "Гарри Гаррисон",
                Translator = "Александр Жаворонков",
            },

            ["Последнее хокку"] = new Description
            {
                Book = "LastHokku",
                Links = LastHokku.Constants.GetLinks(),
                Author = "Юркий Слон",
            },

            ["Генезис"] = new Description
            {
                Book = "Genesis",
                Links = Genesis.Constants.GetLinks(),
                Author = "Андрей Журавлёв",
            },
            
            ["Катарсис"] = new Description
            {
                Book = "Catharsis",
                Links = Catharsis.Constants.GetLinks(),
                Author = "Андрей Журавлёв",
            },
            
            ["По закону прерии"] = new Description
            {
                Book = "PrairieLaw",
                Links = PrairieLaw.Constants.GetLinks(),
                Author = "Ольга Голотвина",
            },

            ["Сердце льда"] = new Description
            {
                Book = "HeartOfIce",
                Links = HeartOfIce.Constants.GetLinks(),
                Author = "Дэйв Моррис",
                Translators = "Мария Ерошкина, Ageres, Jumangee, Vo1t, Fermalion, Johny Lee и другие",
            },

            ["Оружие возмездия"] = new Description
            {
                Book = "VWeapons",
                Links = VWeapons.Constants.GetLinks(),
                Author = "Андрей Тишин",
            },

            ["В краю непуганных медведей"] = new Description
            {
                Book = "LandOfUnwaryBears",
                Links = LandOfUnwaryBears.Constants.GetLinks(),
                Author = "Геннадий Логинов",
            },

            ["Турнир юнлингов"] = new Description
            {
                Book = "YounglingTournament",
                Links = YounglingTournament.Constants.GetLinks(),
                Author = "Александр Андросенко",
            },
        };

        public static List<string> GetBooks() => Books.Keys.ToList();

        public static Description GetDescription(string name)
        {
            Description book = Books[name];
            Game.Xml.GetXmlDescriptionData(ref book);

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
