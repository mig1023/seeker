using System.Collections.Generic;

namespace Seeker.Gamebook.BlackCastleDungeon
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public override bool GetParagraphsStatusesLimit(out int limitStart, out int limitEnd)
        {
            limitStart = ParagraphsStatusesLimit[0];
            limitEnd = ParagraphsStatusesLimit[1];

            return true;
        }

        public static List<int> ParagraphsStatusesLimit { get; set; }

        public static List<string> StaticSpells { get; set; }
    }
}
