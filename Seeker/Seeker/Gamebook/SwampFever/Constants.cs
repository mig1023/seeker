using System.Collections.Generic;

namespace Seeker.Gamebook.SwampFever
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Dictionary<int, string> GetRangeTypes { get; set; }

        public static Dictionary<int, string> GetUpgrates { get; set; }

        public static Dictionary<string, int> GetPurchases { get; set; }

        public static Dictionary<int, string> GetFuryLevel { get; set; }

        public static Dictionary<int, string> NumTexts { get; set; }

        public static Dictionary<int, string> AllNumTexts { get; set; }
    }
}
