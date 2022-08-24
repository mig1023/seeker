using System.Collections.Generic;

namespace Seeker.Gamebook.LastHokku
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Output.Interface.TextFontSize GetFontSize() =>
            Output.Interface.TextFontSize.big;

        public static List<int> GetParagraphsWithoutHokkuCreation { get; set; }

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
