using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Seeker.Gamebook
{
    class List
    {
        private static Dictionary<string, Links> Books = new Dictionary<string, Links>
        {
            ["BlackCastleDungeon"] = BlackCastleDungeon.Constants.GetLinks(),
            ["CaptainSheltonsSecret"] = CaptainSheltonsSecret.Constants.GetLinks(),
            ["FaithfulSwordOfTheKing"] = FaithfulSwordOfTheKing.Constants.GetLinks(),
            ["AdventuresOfABeardlessDeceiver"] = AdventuresOfABeardlessDeceiver.Constants.GetLinks(),
            ["DzungarWar"] = DzungarWar.Constants.GetLinks(),
            ["RockOfTerror"] = RockOfTerror.Constants.GetLinks(),
            ["RendezVous"] = RendezVous.Constants.GetLinks(),
            ["SwampFever"] = SwampFever.Constants.GetLinks(),
            ["MentorsAlwaysRight"] = MentorsAlwaysRight.Constants.GetLinks(),
            ["LegendsAlwaysLie"] = LegendsAlwaysLie.Constants.GetLinks(),
            ["StringOfWorlds"] = StringOfWorlds.Constants.GetLinks(),
            ["ThreePaths"] = ThreePaths.Constants.GetLinks(),
            ["InvisibleFront"] = InvisibleFront.Constants.GetLinks(),
            ["SilentSchool"] = SilentSchool.Constants.GetLinks(),
            ["ThoseWhoAreAboutToDie"] = ThoseWhoAreAboutToDie.Constants.GetLinks(),
            ["OctopusIsland"] = OctopusIsland.Constants.GetLinks(),
            ["CreatureOfHavoc"] = CreatureOfHavoc.Constants.GetLinks(),
            ["BloodfeudOfAltheus"] = BloodfeudOfAltheus.Constants.GetLinks(),
            ["PensionerSimulator"] = PensionerSimulator.Constants.GetLinks(),
            ["HowlOfTheWerewolf"] = HowlOfTheWerewolf.Constants.GetLinks(),
            ["LordOfTheSteppes"] = LordOfTheSteppes.Constants.GetLinks(),
            ["StainlessSteelRat"] = StainlessSteelRat.Constants.GetLinks(),
            ["LastHokku"] = LastHokku.Constants.GetLinks(),
            ["Genesis"] = Genesis.Constants.GetLinks(),
            ["Catharsis"] = Catharsis.Constants.GetLinks(),           
            ["PrairieLaw"] = PrairieLaw.Constants.GetLinks(),
            ["HeartOfIce"] = HeartOfIce.Constants.GetLinks(),
            ["MasterOfTaiga"] = MasterOfTaiga.Constants.GetLinks(),
            ["VWeapons"] = VWeapons.Constants.GetLinks(),
            ["LandOfUnwaryBears"] = LandOfUnwaryBears.Constants.GetLinks(),
            ["YounglingTournament"] = YounglingTournament.Constants.GetLinks(),
            ["GoingToLaughter"] = GoingToLaughter.Constants.GetLinks(),
            ["WildDeath"] = WildDeath.Constants.GetLinks(),
            ["ChooseCthulhu"] = ChooseCthulhu.Constants.GetLinks(),
            ["Damanskiy"] = Damanskiy.Constants.GetLinks(),
            ["ByTheWillOfRome"] = ByTheWillOfRome.Constants.GetLinks(),
            ["OrcsDay"] = OrcsDay.Constants.GetLinks(),
            ["Sheriff"] = Sheriff.Constants.GetLinks(),
            ["Moonrunner"] = Moonrunner.Constants.GetLinks(),
            ["DeathOfAntiquary"] = DeathOfAntiquary.Constants.GetLinks(),
            ["Cyberpunk"] = Cyberpunk.Constants.GetLinks(),
            ["SilverAgeSilhouette"] = SilverAgeSilhouette.Constants.GetLinks(),
            ["StrikeBack"] = StrikeBack.Constants.GetLinks(),
            ["CommunityOfWorms"] = CommunityOfWorms.Constants.GetLinks(),
            ["Ants"] = Ants.Constants.GetLinks(),
            ["ConquistadorDiary"] = ConquistadorDiary.Constants.GetLinks(),
            ["DinosaurIsland"] = DinosaurIsland.Constants.GetLinks(),
            ["Trail"] = Trail.Constants.GetLinks(),
            ["UnexpectedPassenger"] = UnexpectedPassenger.Constants.GetLinks(),
            ["OutlawsOfSherwoodForest"] = OutlawsOfSherwoodForest.Constants.GetLinks(),
            ["AlamutFortress"] = AlamutFortress.Constants.GetLinks(),
            ["Moria"] = Moria.Constants.GetLinks(),
            ["YouAreMillionaire"] = YouAreMillionaire.Constants.GetLinks(),
            ["ProjectOne"] = ProjectOne.Constants.GetLinks(),
            ["MadameGuillotine"] = MadameGuillotine.Constants.GetLinks(),
            ["TenementBuilding"] = TenementBuilding.Constants.GetLinks(),
            ["PresidentSimulator"] = PresidentSimulator.Constants.GetLinks(),
            ["Quakers"] = Quakers.Constants.GetLinks(),
            ["ScorpionSwamp"] = ScorpionSwamp.Constants.GetLinks(),
            ["DangerFromBehindTheSnowWall"] = DangerFromBehindTheSnowWall.Constants.GetLinks(),
        };

        public static List<string> GetBooks() =>
            Books.Keys.ToList();

        public static int Sort() =>
            Game.Settings.GetValue("Sort");

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
                case 1:
                    return SortByTitle(list);

                case 2:
                    return SortByAuthors(list);

                case 3:
                    return list
                        .OrderByDescending(x => x.ParagraphSize())
                        .ToList();

                case 4:
                    return list
                        .OrderByDescending(x => int.Parse(x.Size))
                        .ToList();

                case 5:
                    return list
                        .OrderBy(x => x.Year)
                        .ToList();

                case 6:
                    return list
                        .OrderBy(x => x.Setting)
                        .ThenBy(x => x.AuthorsIndex())
                        .ThenBy(x => x.Year)
                        .ToList();

                case 7:
                    return list
                        .OrderBy(x => Output.Constants.PLAYTHROUGH_ORDER[x.PlaythroughTime])
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
                Links = Books[name],
                Book = name,
                Illustration = string.Format("{0}.jpg", name),
                XmlBook = string.Format("Gamebooks/{0}.xml", name),
            };

            Game.Xml.GetXmlDescriptionData(ref book);

            return book;
        }
    }
}
