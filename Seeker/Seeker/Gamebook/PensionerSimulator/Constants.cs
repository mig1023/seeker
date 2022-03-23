using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.PensionerSimulator
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Output.Interface.TextFontSize GetFontSize() => Output.Interface.TextFontSize.big;

        public static Links GetLinks() => new Links
        {
            Protagonist = Character.Protagonist.Init,
            CheckOnlyIf = Actions.StaticInstance.CheckOnlyIf,
            Paragraphs = Paragraphs.StaticInstance,
            Actions = Actions.StaticInstance,
            Constants = StaticInstance,
            Save = Character.Protagonist.Save,
            Load = Character.Protagonist.Load,
            Debug = Character.Protagonist.Debug,
        };
    }
}
