using System.Collections.Generic;

namespace Seeker.Gamebook.VWeapons
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();
        public static Constants GetInstance() => StaticInstance;

        public static Dictionary<string, string> HealingParts { get; set; }
    }
}
