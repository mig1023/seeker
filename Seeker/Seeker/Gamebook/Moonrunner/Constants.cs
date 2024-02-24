using System.Collections.Generic;

namespace Seeker.Gamebook.Moonrunner
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Dictionary<int, string> SpellsList { get; set; }

        public static List<string> Skills { get; set; }
    }
}
