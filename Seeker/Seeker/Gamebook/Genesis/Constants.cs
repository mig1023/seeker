using System.Collections.Generic;

namespace Seeker.Gamebook.Genesis
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();
        public static Constants GetInstance() => StaticInstance;

        public static Dictionary<string, int> GetStartValues { get; set; }
    }
}
