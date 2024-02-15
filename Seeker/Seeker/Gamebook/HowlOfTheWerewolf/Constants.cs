using System.Collections.Generic;

namespace Seeker.Gamebook.HowlOfTheWerewolf
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();
        public static Constants GetInstance() => StaticInstance;

        public static Dictionary<int, string> GetCountName { get; set; }

        public static Dictionary<int, string> GetPassageName { get; set; }

        public static int GetUlrichMastery() => 8;

        public static int GetVanRichtenMastery() => 10;

        public static Links GetLinks() => new Links
        {
            Paragraphs = Paragraphs.StaticInstance,
            Actions = Actions.StaticInstance,
            Constants = StaticInstance,
        };
    }
}
