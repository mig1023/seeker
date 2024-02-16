using System.Collections.Generic;

namespace Seeker.Gamebook.Moonrunner
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public new static Constants StaticInstance = new Constants();
        public new static Constants GetInstance() => StaticInstance;

        public static Dictionary<int, string> SpellsList { get; set; }

        public static List<string> Skills { get; set; }
    }
}
