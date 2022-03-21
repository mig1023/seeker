using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.Catharsis
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#cdcdcd",
            [ColorTypes.StatusBar] = "#b8b8b8",
            [ColorTypes.StatusFont] = "#000000",
            [ColorTypes.AdditionalStatus] = "#bfbfbf",
            [ColorTypes.ActionBox] = "#b8b8b8",
            [ColorTypes.BookColor] = "#51514b",
        };

        public static Dictionary<string, int> GetStartValues() => new Dictionary<string, int>
        {
            ["Fight"] = 10,
            ["Accuracy"] = 10,
            ["Stealth"] = 3,
        };

        public override List<int> GetParagraphsWithoutStatuses() => new List<int> { 0, 401, 402 };

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
            Debug = Character.Protagonist.Debug,
        };
    }
}
