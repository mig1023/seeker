using System.Collections.Generic;

namespace Seeker.Gamebook.SwampFever
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public new static Constants StaticInstance = new Constants();
        public new static Constants GetInstance() => StaticInstance;

        public static Dictionary<int, string> GetRangeTypes { get; set; }

        public static Dictionary<int, string> GetUpgrates { get; set; }

        public static Dictionary<string, int> GetPurchases { get; set; }

        public static Dictionary<int, string> GetFuryLevel { get; set; }
    }
}
