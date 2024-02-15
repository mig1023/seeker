using System.Collections.Generic;

namespace Seeker.Gamebook.CaptainSheltonsSecret
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();
        public static Constants GetInstance() => StaticInstance;

        public static Dictionary<int, int> Mastery { get; set; }

        public static Dictionary<int, int> Endurances { get; set; }

        public static Dictionary<int, string> LuckList { get; set; }

        public static Links GetLinks() => new Links
        {
            Paragraphs = Paragraphs.StaticInstance,
            Actions = Actions.StaticInstance,
            Constants = StaticInstance,
        };
    }
}
