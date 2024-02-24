using System.Collections.Generic;

namespace Seeker.Gamebook.HowlOfTheWerewolf
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Dictionary<int, string> GetCountName { get; set; }

        public static Dictionary<int, string> GetPassageName { get; set; }

        public static int GetUlrichMastery() => 8;

        public static int GetVanRichtenMastery() => 10;
    }
}
