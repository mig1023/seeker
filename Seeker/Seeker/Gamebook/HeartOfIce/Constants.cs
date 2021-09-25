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
            [ButtonTypes.Main] = "#4a6e9c",
            [ButtonTypes.Option] = "#6e8baf",
            [ButtonTypes.Action] = "#6e8baf",
            [ButtonTypes.Continue] = "#99adc7",
            [ButtonTypes.System] = "#a6b7ce",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#b6c5d7",
            [ColorTypes.Font] = "#2c425d",
            [ColorTypes.ActionBox] = "#a5b6cd",
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
