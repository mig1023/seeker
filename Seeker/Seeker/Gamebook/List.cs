using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static Seeker.Output.Constants;

namespace Seeker.Gamebook
{
    class List
    {
        private static List<string> Books = new List<string>
        {
            "BlackCastleDungeon",
            "CaptainSheltonsSecret",
            "FaithfulSwordOfTheKing",
            "AdventuresOfABeardlessDeceiver",
            "DzungarWar",
            "RockOfTerror",
            "RendezVous",
            "SwampFever",
            "MentorsAlwaysRight",
            "LegendsAlwaysLie",
            "StringOfWorlds",
            "ThreePaths",
            "InvisibleFront",
            "SilentSchool",
            "ThoseWhoAreAboutToDie",
            "OctopusIsland",
            "CreatureOfHavoc",
            "BloodfeudOfAltheus",
            "PensionerSimulator",
            "HowlOfTheWerewolf",
            "LordOfTheSteppes",
            "StainlessSteelRat",
            "LastHokku",
            "Genesis",
            "Catharsis",
            "PrairieLaw",
            "HeartOfIce",
            "MasterOfTaiga",
            "VWeapons",
            "LandOfUnwaryBears",
            "YounglingTournament",
            "GoingToLaughter",
            "WildDeath",
            "ChooseCthulhu",
            "Damanskiy",
            "ByTheWillOfRome",
            "OrcsDay",
            "Sheriff",
            "Moonrunner",
            "DeathOfAntiquary",
            "Cyberpunk",
            "SilverAgeSilhouette",
            "StrikeBack",
            "CommunityOfWorms",
            "Ants",
            "ConquistadorDiary",
            "DinosaurIsland",
            "Trail",
            "UnexpectedPassenger",
            "OutlawsOfSherwoodForest",
            "AlamutFortress",
            "Moria",
            "YouAreMillionaire",
            "ProjectOne",
            "MadameGuillotine",
            "TenementBuilding",
            "PresidentSimulator",
            "Quakers",
            "ScorpionSwamp",
            "DangerFromBehindTheSnowWall",
        };

        public static List<string> GetBooks() =>
            Books;

        public static SortBy Sort()
        {
            string value = Game.Settings.GetValue("Sort").ToString();
            return (SortBy)Enum.Parse(typeof(SortBy), value);
        }

        public static bool Sort(SortBy sort) =>
            Game.Settings.GetValue("Sort") == (int)sort;

        private static List<Description> SortByTitle(List<Description> list)
        {
            List<Description> engishList = new List<Description>(list
                .Where(x => Regex.Match(x.Title, @"^[A-Za-z]").Success)
                .OrderBy(x => x.Title)
                .ToList());

            List<Description> russianList = new List<Description>(list
                .Where(x => !Regex.Match(x.Title, @"^[A-Za-z]").Success)
                .OrderBy(x => x.Title)
                .ToList());

            russianList.AddRange(engishList);
            return russianList;
        }

        private static List<Description> SortByAuthors(List<Description> list)
        {
            List<Description> engishList = new List<Description>(list
                .Where(x => Regex.Match(x.AuthorsIndex(), @"^[A-Za-z]").Success)
                .OrderBy(x => x.AuthorsIndex())
                .ThenBy(x => x.Year)
                .ToList());

            List<Description> russianList = new List<Description>(list
                .Where(x => !Regex.Match(x.AuthorsIndex(), @"^[A-Za-z]").Success)
                .OrderBy(x => x.AuthorsIndex())
                .ThenBy(x => x.Year)
                .ToList());

            russianList.AddRange(engishList);
            return russianList;
        }

        public static List<Description> GetSortedBooks()
        {
            List<Description> list = new List<Description>(List.GetBooks().Select(x => List.GetDescription(x)));

            switch (Sort())
            {
                case SortBy.Title:

                    return SortByTitle(list);

                case SortBy.Author:

                    return SortByAuthors(list);

                case SortBy.Paragraphs:

                    return list
                        .OrderByDescending(x => x.ParagraphSize())
                        .ToList();

                case SortBy.Size:

                    return list
                        .OrderByDescending(x => int.Parse(x.Size))
                        .ToList();

                case SortBy.Year:

                    return list
                        .OrderBy(x => x.Year)
                        .ToList();

                case SortBy.Setting:

                    return list
                        .OrderBy(x => x.Setting)
                        .ThenBy(x => x.AuthorsIndex())
                        .ThenBy(x => x.Year)
                        .ToList();

                case SortBy.Time:

                    return list
                        .OrderBy(x => PLAYTHROUGH_ORDER[x.PlaythroughTime])
                        .ThenBy(x => x.ParagraphSize())
                        .ToList();

                default:
                    return list;
            }
        }

        public static Description GetDescription(string name)
        {
            Description book = new Description
            {
                Book = name,
                Illustration = string.Format("{0}.jpg", name),
                XmlBook = string.Format("Gamebooks/{0}.xml", name),
            };

            Game.Xml.GetXmlDescriptionData(ref book);

            return book;
        }
    }
}
