using System.Collections.Generic;

namespace Seeker.Gamebook.SwampFever
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();
        public static Constants GetInstance() => StaticInstance;

        public static Dictionary<int, string> GetRangeTypes { get; set; }

        public static Dictionary<int, string> GetUpgrates { get; set; }

        public static Dictionary<string, int> GetPurchases { get; set; }

        public static Dictionary<int, string> GetFuryLevel { get; set; }

        public static Links GetLinks() => new Links
        {
            Paragraphs = Paragraphs.StaticInstance,
            Actions = Actions.StaticInstance,
            Constants = StaticInstance,
        };
    }
}
