using System.Collections.Generic;

namespace Seeker.Gamebook.ColdHeartOfDalrok
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static List<string> Fails { get; set; }

        public static Dictionary<int, int> Skills { get; set; }

        public static Dictionary<int, int> Strengths { get; set; }

        public static Dictionary<int, int> Charms { get; set; }

        public static Dictionary<int, string> LuckList { get; set; }
    }
}
