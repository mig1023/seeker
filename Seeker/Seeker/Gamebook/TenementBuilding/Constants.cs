using System.Collections.Generic;

namespace Seeker.Gamebook.TenementBuilding
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();
        public static Constants GetInstance() => StaticInstance;

        public static Dictionary<int, string> LuckList { get; set; }
    }
}
