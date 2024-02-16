using System.Collections.Generic;

namespace Seeker.Gamebook.ConquistadorDiary
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();
        public static Constants GetInstance() => StaticInstance;

        public static List<int> WithStatuses { get; set; }
    }
}
