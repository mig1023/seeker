using System.Collections.Generic;

namespace Seeker.Gamebook.PresidentSimulator
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static new Constants StaticInstance = new Constants();
        public static new Constants GetInstance() => StaticInstance;

        public static Dictionary<string, string> TextByYears { get; set; }
    }
}
