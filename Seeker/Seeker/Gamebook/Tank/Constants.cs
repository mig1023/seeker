using System.Collections.Generic;

namespace Seeker.Gamebook.Tank
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Dictionary<string, string> CrewNames { get; set; }

        public static Dictionary<int, string> HitNames { get; set; }

        public static List<int> FrontMisses { get; set; }

        public static List<string> FightStatuses { get; set; }
    }
}
