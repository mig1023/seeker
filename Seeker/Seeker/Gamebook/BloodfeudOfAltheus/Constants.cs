using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.BloodfeudOfAltheus
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#f9f2e8",
            [ColorTypes.ActionBox] = "#f3e5d1",
            [ColorTypes.StatusBar] = "#bcaa8f",
            [ColorTypes.StatusFont] = "#000000",
            [ColorTypes.AdditionalStatus] = "#d0c3b0",
        };

        public static List<int> GetParagraphsWithoutStaticsButtons() => new List<int> { 0, 1 };

        public static Dictionary<int, string> HealthLine() => new Dictionary<int, string>
        {
            [0] = "мертв",
            [1] = "тяжело ранен",
            [2] = "ранен",
            [3] = "здоров",
        };

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
