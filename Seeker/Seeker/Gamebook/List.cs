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
                SmallDisclaimer = "Дмитрий Браславский, 1991",
                Illustration = "BlackCastleDungeon.jpg",
            },

            ["Тайна капитана Шелтона"] = new Description
            {
                XmlBook = "Gamebooks/CaptainSheltonsSecret.xml",
                Links = CaptainSheltonsSecret.Constants.GetLinks(),
                SmallDisclaimer = "Дмитрий Браславский, 1992",
                Illustration = "CaptainSheltonsSecret.jpg",
            },

            ["Верная шпага короля"] = new Description
            {
                XmlBook = "Gamebooks/FaithfulSwordOfTheKing.xml",
                Links = FaithfulSwordOfTheKing.Constants.GetLinks(),
                SmallDisclaimer = "Дмитрий Браславский, 1995",
                Illustration = "FaithfulSwordOfTheKing.jpg",
            },

            ["Приключения безбородого обманщика"] = new Description
            {
                XmlBook = "Gamebooks/AdventuresOfABeardlessDeceiver.xml",
                Links = AdventuresOfABeardlessDeceiver.Constants.GetLinks(),
                SmallDisclaimer = "Владимир Сизиков, 2015",
                Illustration = "AdventuresOfABeardlessDeceiver.jpg",
            },

            ["Джунгарское нашествие"] = new Description
            {
                XmlBook = "Gamebooks/DzungarWar.xml",
                Links = DzungarWar.Constants.GetLinks(),
                SmallDisclaimer = "Владимир Сизиков, 2016",
                Illustration = "DzungarWar.jpg",
            },

            ["Скала ужаса"] = new Description
            {
                XmlBook = "Gamebooks/RockOfTerror.xml",
                Links = RockOfTerror.Constants.GetLinks(),
                SmallDisclaimer = "Дмитрий Тышевич, 2009",
                Illustration = "RockOfTerror.jpg",
            },

            ["Рандеву"] = new Description
            {
                XmlBook = "Gamebooks/RendezVous.xml",
                Links = RendezVous.Constants.GetLinks(),
                SmallDisclaimer = "Ал Торо, 2020",
                FullDisclaimer = "Автор: Ал Торо\n\nПереводчик: Мария Ерошкина",
                Illustration = "RendezVous.jpg",
            },

            ["Болотная лихорадка"] = new Description
            {
                XmlBook = "Gamebooks/SwampFever.xml",
                Links = SwampFever.Constants.GetLinks(),
                SmallDisclaimer = "Пётр Прокошев, 2017",
                Illustration = "SwampFever.jpg",
            },

            ["Наставники всегда правы"] = new Description
            {
                XmlBook = "Gamebooks/MentorsAlwaysRight.xml",
                Links = MentorsAlwaysRight.Constants.GetLinks(),
                SmallDisclaimer = "Роман Островерхов, 2011",
                Illustration = "MentorsAlwaysRight.jpg",
            },

            ["Легенды всегда врут"] = new Description
            {
                XmlBook = "Gamebooks/LegendsAlwaysLie.xml",
                Links = LegendsAlwaysLie.Constants.GetLinks(),
                SmallDisclaimer = "Роман Островерхов, 2012",
                Illustration = "LegendsAlwaysLie.jpg",
            },
            
            ["Вереница миров или выводы из закона Мэрфи"] = new Description
            {
                XmlBook = "Gamebooks/StringOfWorlds.xml",
                Links = StringOfWorlds.Constants.GetLinks(),
                SmallDisclaimer = "Ольга Голотвина, 1995",
                Illustration = "StringOfWorlds.jpg",
            },
            
            ["Три дороги"] = new Description
            {
                XmlBook = "Gamebooks/ThreePaths.xml",
                Links = ThreePaths.Constants.GetLinks(),
                SmallDisclaimer = "Александр Бутягин, Дмитрий Чистов, 1999",
                Illustration = "ThreePaths.jpg",
            },

            ["На невидимых фронтах"] = new Description
            {
                XmlBook = "Gamebooks/InvisibleFront.xml",
                Links = InvisibleFront.Constants.GetLinks(),
                SmallDisclaimer = "mmvvss, 2018",
                Illustration = "InvisibleFront.jpg",
            },

            ["Silent School"] = new Description
            {
                XmlBook = "Gamebooks/SilentSchool.xml",
                Links = SilentSchool.Constants.GetLinks(),
                SmallDisclaimer = "Роман Островерхов, 2013",
                Illustration = "SilentSchool.jpg",
            },
            
            ["Идущие на смерть"] = new Description
            {
                XmlBook = "Gamebooks/ThoseWhoAreAboutToDie.xml",
                Links = ThoseWhoAreAboutToDie.Constants.GetLinks(),
                SmallDisclaimer = "Александр Слюта, 2009",
                Illustration = "ThoseWhoAreAboutToDie.jpg",
            },

            ["Остров Осьминогов"] = new Description
            {
                XmlBook = "Gamebooks/OctopusIsland.xml",
                Links = OctopusIsland.Constants.GetLinks(),
                SmallDisclaimer = "Филипп Эбли, 1992",
                Illustration = "OctopusIsland.jpg",
            },

            ["Разрушитель"] = new Description
            {
                XmlBook = "Gamebooks/CreatureOfHavoc.xml",
                Links = CreatureOfHavoc.Constants.GetLinks(),
                SmallDisclaimer = "Стив Джексон, 1986",
                Illustration = "CreatureOfHavoc.jpg",
            },

            ["Месть Альтея"] = new Description
            {
                XmlBook = "Gamebooks/BloodfeudOfAltheus.xml",
                Links = BloodfeudOfAltheus.Constants.GetLinks(),
                SmallDisclaimer = "Джон Баттерфилд и др., 1985",
                FullDisclaimer = "Авторы: Джон Баттерфилд, Дэвид Хонигман и Филип Паркер\n\n" +
                    "Переводчики: Мария Ерошкина, GalinaSol, Xpromt, Johny Lee, fermalion, Тара-сан, Jumangee, Ajenta, Эргистал, Anuta и другие",
                Illustration = "BloodfeudOfAltheus.jpg",
            },

            ["Симулятор пенсионерки"] = new Description
            {
                XmlBook = "Gamebooks/PensionerSimulator.xml",
                Links = PensionerSimulator.Constants.GetLinks(),
                SmallDisclaimer = "Zaratystra, the_arsonist, 2018",
                FullDisclaimer = "Симулятор пенсионерки:\nАвтор: Zaratystra, 2018\n\n" +
                    "Симулятор пенсионерки 2, Кровавая Охота:\nАвтор: the_arsonist, 2019\nПеревод: Мария Ерошкина\n" +
                    "Адаптация перевода: Zaratystra\nРедактор: Wervek",
                Illustration = "PensionerSimulator.jpg",
            },

            ["Владыка степей"] = new Description
            {
                XmlBook = "Gamebooks/LordOfTheSteppes.xml",
                Links = LordOfTheSteppes.Constants.GetLinks(),
                SmallDisclaimer = "Сергей Ступин, 2009",
                Illustration = "LordOfTheSteppes.jpg",
            },

            ["Вой оборотня"] = new Description
            {
                XmlBook = "Gamebooks/HowlOfTheWerewolf.xml",
                Links = HowlOfTheWerewolf.Constants.GetLinks(),
                SmallDisclaimer = "Джонатан Грин, 2007",
                FullDisclaimer = "Автор: Джонатан Грин\n\nПереводчики: Rustem, Vo1t",
                Illustration = "HowlOfTheWerewolf.jpg",
            },

            ["Крыса из нержавеющей стали"] = new Description
            {
                XmlBook = "Gamebooks/StainlessSteelRat.xml",
                Links = StainlessSteelRat.Constants.GetLinks(),
                SmallDisclaimer = "Гарри Гаррисон, 1985",
                FullDisclaimer = "Автор: Гарри Гаррисон\n\nПереводчик: Александр Жаворонков",
                Illustration = "StainlessSteelRat.jpg",
            },

            ["Последнее хокку"] = new Description
            {
                XmlBook = "Gamebooks/LastHokku.xml",
                Links = LastHokku.Constants.GetLinks(),
                SmallDisclaimer = "Юркий Слон, 2021",
                Illustration = "LastHokku.jpg",
            },

            ["Генезис"] = new Description
            {
                XmlBook = "Gamebooks/Genesis.xml",
                Links = Genesis.Constants.GetLinks(),
                SmallDisclaimer = "Андрей Журавлёв, 2013",
                Illustration = "Genesis.jpg",
            },
            
            ["Катарсис"] = new Description
            {
                XmlBook = "Gamebooks/Catharsis.xml",
                Links = Catharsis.Constants.GetLinks(),
                SmallDisclaimer = "Андрей Журавлёв, 2013",
                Illustration = "Catharsis.jpg",
            },
            
            ["По закону прерии"] = new Description
            {
                XmlBook = "Gamebooks/PrairieLaw.xml",
                Links = PrairieLaw.Constants.GetLinks(),
                SmallDisclaimer = "Ольга Голотвина, 1995",
                Illustration = "PrairieLaw.jpg",
            },

            ["Сердце льда"] = new Description
            {
                XmlBook = "Gamebooks/HeartOfIce.xml",
                Links = HeartOfIce.Constants.GetLinks(),
                SmallDisclaimer = "Дэйв Моррис, 1994",
                FullDisclaimer = "Авторы: Дэйв Моррис\n\nПереводчики: Мария Ерошкина, Ageres, Jumangee, Vo1t, Fermalion, Johny Lee и другие",
                Illustration = "HeartOfIce.jpg",
            },

            ["Оружие возмездия"] = new Description
            {
                XmlBook = "Gamebooks/VWeapons.xml",
                Links = VWeapons.Constants.GetLinks(),
                SmallDisclaimer = "Андрей Тишин, 2013",
                Illustration = "VWeapons.jpg",
            },

            ["В краю непуганных медведей"] = new Description
            {
                XmlBook = "Gamebooks/LandOfUnwaryBears.xml",
                Links = LandOfUnwaryBears.Constants.GetLinks(),
                SmallDisclaimer = "Геннадий Логинов, 2020",
                Illustration = "LandOfUnwaryBears.jpg",
            },

            ["Турнир юнлингов"] = new Description
            {
                XmlBook = "Gamebooks/YounglingTournament.xml",
                Links = YounglingTournament.Constants.GetLinks(),
                SmallDisclaimer = "Александр Андросенко, 2018",
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
