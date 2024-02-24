using System.Collections.Generic;

namespace Seeker.Gamebook.LastHokku
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static List<int> GetParagraphsWithoutHokkuCreation { get; set; }
    }
}
