using System.Collections.Generic;

namespace Seeker.Gamebook.BlackCastleDungeon
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override bool GetParagraphsStatusesLimit(out int limitStart, out int limitEnd)
        {
            if ((ParagraphsStatusesLimit != null) && (ParagraphsStatusesLimit.Count == 2))
            {
                limitStart = ParagraphsStatusesLimit[0];
                limitEnd = ParagraphsStatusesLimit[1];

                return true;
            }
            else
                return base.GetParagraphsStatusesLimit(out limitStart, out limitEnd);
        }

        public static List<int> ParagraphsStatusesLimit { get; set; }

        public static List<string> StaticSpells { get; set; }

        public static Links GetLinks() => new Links
        {
            Protagonist = Character.Protagonist.Init,
            Availability = Actions.StaticInstance.Availability,
            Paragraphs = Paragraphs.StaticInstance,
            Actions = Actions.StaticInstance,
            Constants = StaticInstance,
            Save = Character.Protagonist.Save,
            Load = Character.Protagonist.Load,
            Debug = Character.Protagonist.Debug,
        };
    }
}
