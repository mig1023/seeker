using System.Collections.Generic;

namespace Seeker.Gamebook.Moonrunner
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();
        public static Constants GetInstance() => StaticInstance;

        public static Dictionary<int, string> SpellsList { get; set; }

        public static List<string> Skills { get; set; }

        public static Links GetLinks() => new Links
        {
            Paragraphs = Paragraphs.StaticInstance,
            Actions = Actions.StaticInstance,
            Constants = StaticInstance,
        };
    }
}
