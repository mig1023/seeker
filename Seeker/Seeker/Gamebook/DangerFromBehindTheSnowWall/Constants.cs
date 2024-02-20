using System.Collections.Generic;

namespace Seeker.Gamebook.DangerFromBehindTheSnowWall
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public new static Constants StaticInstance = new Constants();
        public new static Constants GetInstance() => StaticInstance;

        public static Dictionary<int, int> Skills { get; set; }

        public static Dictionary<int, int> Strengths { get; set; }

        public static Dictionary<int, int> Damage { get; set; }

        public static Dictionary<int, int> Observation { get; set; }

        public static Dictionary<int, int> Money { get; set; }

        public static Dictionary<int, string> LuckList { get; set; }
    }
}
