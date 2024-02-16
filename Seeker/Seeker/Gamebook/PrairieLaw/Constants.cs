using System.Collections.Generic;

namespace Seeker.Gamebook.PrairieLaw
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();
        public static Constants GetInstance() => StaticInstance;

        public static Dictionary<int, int> Skills { get; set; }

        public static Dictionary<int, int> Strengths { get; set; }

        public static Dictionary<int, int> Charms { get; set; }

        public static Dictionary<int, string> LuckList { get; set; }
    }
}
