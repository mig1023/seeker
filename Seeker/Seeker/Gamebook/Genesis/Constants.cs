using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.Genesis
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#293342",
            [ColorTypes.Font] = "#b6cbd8",
            [ColorTypes.StatusBar] = "#3c5363",
            [ColorTypes.ActionBox] = "#69707a",
            [ColorTypes.BookColor] = "#202b41",
            [ColorTypes.SystemFont] = "#b1b5ba",
        };

        public static Dictionary<string, int> GetStartValues() => new Dictionary<string, int>
        {
            ["Skill"] = 30,
            ["Weapon"] = 15,
            ["Stealth"] = 3,
        };

        public override List<int> GetParagraphsWithoutStatuses() => new List<int> { 0, 201, 202, 203, 204, 205, 206, 207 };

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
