using System.Collections.Generic;

namespace Seeker.Gamebook.LastHokku
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public new static Constants StaticInstance = new Constants();
        public new static Constants GetInstance() => StaticInstance;

        public static List<int> GetParagraphsWithoutHokkuCreation { get; set; }
    }
}
