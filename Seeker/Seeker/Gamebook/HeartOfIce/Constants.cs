using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.HeartOfIce
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#389fcf",
            [ButtonTypes.Option] = "#6ab7db",
            [ButtonTypes.Action] = "#0679b0",
            [ButtonTypes.Continue] = "#6ab7db",
            [ButtonTypes.System] = "#1f93c9",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#0787c4",
            [ColorTypes.Font] = "#d3f0fd",
            [ColorTypes.SystemFont] = "#d3f0fd",
            [ColorTypes.ActionBox] = "#389fcf",
            [ColorTypes.StatusBar] = "#5c7ca5",
            [ColorTypes.BookColor] = "#4a6e9c",
        };

        public override List<int> GetParagraphsWithoutStatuses() => new List<int> { 0, 454, 455, 456, 457 };

        public override bool ShowDisabledOption() => true;

        public static Links GetLinks() => new Links
        {
            Protagonist = Character.Protagonist.Init,
            CheckOnlyIf = Actions.StaticInstance.CheckOnlyIf,
            Paragraphs = Paragraphs.StaticInstance,
            Actions = Actions.StaticInstance,
            Constants = StaticInstance,
            Save = Character.Protagonist.Save,
            Load = Character.Protagonist.Load,
        };
    }
}
