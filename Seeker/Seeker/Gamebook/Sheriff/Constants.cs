using System.Collections.Generic;

namespace Seeker.Gamebook.Sheriff
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static List<string> CleaningNotebookList { get; set; }

        public static Dictionary<string, int> Levels { get; set; }
    }
}
