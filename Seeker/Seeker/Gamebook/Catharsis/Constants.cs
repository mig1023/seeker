using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.Catharsis
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#51514b",
            [ButtonTypes.Option] = "#858581",
            [ButtonTypes.Continue] = "#a9a9a6",
            [ButtonTypes.Action] = "#939393",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#cdcdcd",
            [ColorTypes.StatusBar] = "#b8b8b8",
            [ColorTypes.StatusFont] = "#000000",
            [ColorTypes.AdditionalStatus] = "#bfbfbf",
            [ColorTypes.ActionBox] = "#b8b8b8",
        };

        public static Dictionary<string, int> GetStartValues() => new Dictionary<string, int>
        {
            ["Fight"] = 10,
            ["Accuracy"] = 10,
            ["Stealth"] = 3,
        };

        public override List<int> GetParagraphsWithoutStatuses() => new List<int> { 0, 401, 402 };

        public override bool ShowDisabledOption() => true;
    }
}
