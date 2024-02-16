using System.Collections.Generic;

namespace Seeker.Gamebook.Sheriff
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public new static Constants StaticInstance = new Constants();
        public new static Constants GetInstance() => StaticInstance;

        public static List<string> CleaningNotebookList { get; set; }

        public static Dictionary<string, int> Levels { get; set; }
    }
}
