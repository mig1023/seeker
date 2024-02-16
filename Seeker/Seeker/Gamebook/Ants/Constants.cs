using System.Collections.Generic;

namespace Seeker.Gamebook.Ants
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public new static Constants StaticInstance = new Constants();
        public new static Constants GetInstance() => StaticInstance;

        public static Dictionary<string, int> Rating { get; set; }

        public static Dictionary<string, string> Government { get; set; }

        public static List<string> EndingOne { get; set; }

        public static List<string> EndingTwo { get; set; }
    }
}
