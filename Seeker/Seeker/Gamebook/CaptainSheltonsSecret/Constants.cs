using System.Collections.Generic;

namespace Seeker.Gamebook.CaptainSheltonsSecret
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Dictionary<int, int> Mastery { get; set; }

        public static Dictionary<int, int> Endurances { get; set; }

        public static Dictionary<int, string> LuckList { get; set; }
    }
}
