using System.Collections.Generic;

namespace Seeker.Gamebook.TenementBuilding
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Dictionary<int, string> LuckList { get; set; }
    }
}
