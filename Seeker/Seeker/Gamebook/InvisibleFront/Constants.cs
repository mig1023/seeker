using System.Collections.Generic;

namespace Seeker.Gamebook.InvisibleFront
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();
        public static Constants GetInstance() => StaticInstance;

        public static List<string> GetApartments { get; set; }
    }
}
