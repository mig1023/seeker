using System.Collections.Generic;

namespace Seeker.Gamebook.BlackCastleDungeon
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();
        public static Constants GetInstance() => StaticInstance;

        public override bool GetParagraphsStatusesLimit(out int limitStart, out int limitEnd)
        {
            limitStart = ParagraphsStatusesLimit[0];
            limitEnd = ParagraphsStatusesLimit[1];

            return true;
        }

        public static List<int> ParagraphsStatusesLimit { get; set; }

        public static List<string> StaticSpells { get; set; }

        public static Links GetLinks() => new Links
        {
            Paragraphs = Paragraphs.StaticInstance,
            Actions = Actions.StaticInstance,
            Constants = StaticInstance,
        };
    }
}
