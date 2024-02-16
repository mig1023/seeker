using System.Collections.Generic;

namespace Seeker.Gamebook.CaptainSheltonsSecret
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public new static Constants StaticInstance = new Constants();
        public new static Constants GetInstance() => StaticInstance;

        public static Dictionary<int, int> Mastery { get; set; }

        public static Dictionary<int, int> Endurances { get; set; }

        public static Dictionary<int, string> LuckList { get; set; }
    }
}
