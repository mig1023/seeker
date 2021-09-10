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
            },

            ["Тайна капитана Шелтона"] = new Description
            {
                Book = "CaptainSheltonsSecret",
                Links = CaptainSheltonsSecret.Constants.GetLinks(),
            },

            ["Верная шпага короля"] = new Description
            {
                Book = "FaithfulSwordOfTheKing",
                Links = FaithfulSwordOfTheKing.Constants.GetLinks(),
            },

            ["Приключения безбородого обманщика"] = new Description
            {
                Book = "AdventuresOfABeardlessDeceiver",
                Links = AdventuresOfABeardlessDeceiver.Constants.GetLinks(),
            },

            ["Джунгарское нашествие"] = new Description
            {
                Book = "DzungarWar",
                Links = DzungarWar.Constants.GetLinks(),
            },

            ["Скала ужаса"] = new Description
            {
                Book = "RockOfTerror",
                Links = RockOfTerror.Constants.GetLinks(),
            },

            ["Рандеву"] = new Description
            {
                Book = "RendezVous",
                Links = RendezVous.Constants.GetLinks(),
            },

            ["Болотная лихорадка"] = new Description
            {
                Book = "SwampFever",
                Links = SwampFever.Constants.GetLinks(),
            },

            ["Наставники всегда правы"] = new Description
            {
                Book = "MentorsAlwaysRight",
                Links = MentorsAlwaysRight.Constants.GetLinks(),
            },

            ["Легенды всегда врут"] = new Description
            {
                Book = "LegendsAlwaysLie",
                Links = LegendsAlwaysLie.Constants.GetLinks(),
            },
            
            ["Вереница миров или выводы из закона Мэрфи"] = new Description
            {
                Book = "StringOfWorlds",
                Links = StringOfWorlds.Constants.GetLinks(),
            },
            
            ["Три дороги"] = new Description
            {
                Book = "ThreePaths",
                Links = ThreePaths.Constants.GetLinks(),
            },

            ["На невидимых фронтах"] = new Description
            {
                Book = "InvisibleFront",
                Links = InvisibleFront.Constants.GetLinks(),
            },

            ["Silent School"] = new Description
            {
                Book = "SilentSchool",
                Links = SilentSchool.Constants.GetLinks(),
            },
            
            ["Идущие на смерть"] = new Description
            {
                Book = "ThoseWhoAreAboutToDie",
                Links = ThoseWhoAreAboutToDie.Constants.GetLinks(),
            },

            ["Остров Осьминогов"] = new Description
            {
                Book = "OctopusIsland",
                Links = OctopusIsland.Constants.GetLinks(),
            },

            ["Разрушитель"] = new Description
            {
                Book = "CreatureOfHavoc",
                Links = CreatureOfHavoc.Constants.GetLinks(),
            },

            ["Месть Альтея"] = new Description
            {
                Book = "BloodfeudOfAltheus",
                Links = BloodfeudOfAltheus.Constants.GetLinks(),
            },

            ["Симулятор пенсионерки"] = new Description
            {
                Book = "PensionerSimulator",
                Links = PensionerSimulator.Constants.GetLinks(),              
            },

            ["Владыка степей"] = new Description
            {
                Book = "LordOfTheSteppes",
                Links = LordOfTheSteppes.Constants.GetLinks(),
            },

            ["Вой оборотня"] = new Description
            {
                Book = "HowlOfTheWerewolf",
                Links = HowlOfTheWerewolf.Constants.GetLinks(),
            },

            ["Крыса из нержавеющей стали"] = new Description
            {
                Book = "StainlessSteelRat",
                Links = StainlessSteelRat.Constants.GetLinks(),
            },

            ["Последнее хокку"] = new Description
            {
                Book = "LastHokku",
                Links = LastHokku.Constants.GetLinks(),
            },

            ["Генезис"] = new Description
            {
                Book = "Genesis",
                Links = Genesis.Constants.GetLinks(),
            },
            
            ["Катарсис"] = new Description
            {
                Book = "Catharsis",
                Links = Catharsis.Constants.GetLinks(),
            },
            
            ["По закону прерии"] = new Description
            {
                Book = "PrairieLaw",
                Links = PrairieLaw.Constants.GetLinks(),
            },

            ["Сердце льда"] = new Description
            {
                Book = "HeartOfIce",
                Links = HeartOfIce.Constants.GetLinks(),
            },

            ["Оружие возмездия"] = new Description
            {
                Book = "VWeapons",
                Links = VWeapons.Constants.GetLinks(),
            },

            ["В краю непуганных медведей"] = new Description
            {
                Book = "LandOfUnwaryBears",
                Links = LandOfUnwaryBears.Constants.GetLinks(),
            },

            ["Турнир юнлингов"] = new Description
            {
                Book = "YounglingTournament",
                Links = YounglingTournament.Constants.GetLinks(),
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
