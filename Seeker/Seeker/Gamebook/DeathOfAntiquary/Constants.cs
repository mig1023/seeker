using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.DeathOfAntiquary
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#2e2e2e",
            [ButtonTypes.Option] = "#2e2e2e",
            [ButtonTypes.Continue] = "#575757",
            [ButtonTypes.System] = "#6c6c6c",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.BookColor] = "#2e2e2e",
        };

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
