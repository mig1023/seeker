using System.Collections.Generic;

namespace Seeker.Gamebook.InvisibleFront
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public new static Constants StaticInstance = new Constants();
        public new static Constants GetInstance() => StaticInstance;

        public static List<string> GetApartments { get; set; }
    }
}
