using System.Collections.Generic;

namespace Seeker.Gamebook.LastHokku
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();
        public static Constants GetInstance() => StaticInstance;

        public static List<int> GetParagraphsWithoutHokkuCreation { get; set; }
    }
}
