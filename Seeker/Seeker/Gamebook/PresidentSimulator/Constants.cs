using System.Collections.Generic;

namespace Seeker.Gamebook.PresidentSimulator
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Dictionary<string, string> TextByYears { get; set; }
    }
}
