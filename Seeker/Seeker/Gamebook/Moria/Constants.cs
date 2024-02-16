using System.Collections.Generic;

namespace Seeker.Gamebook.Moria
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();
        public static Constants GetInstance() => StaticInstance;

        public static Dictionary<string, int> Fellowship { get; set; }

        public static Dictionary<string, int> Enemies { get; set; }

        public static Dictionary<string, string> Declination { get; set; }
    }
}
