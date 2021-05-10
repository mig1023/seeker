using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.Catharsis
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        static Dictionary<ButtonTypes, string> ButtonsColors = new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#51514b",
            [ButtonTypes.Option] = "#858581",
            [ButtonTypes.Continue] = "#a9a9a6",
            [ButtonTypes.Action] = "#445d6f",
        };

        static Dictionary<ColorTypes, string> Colors = new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#e4e4e4",
            [ColorTypes.StatusBar] = "#656563",
            [ColorTypes.ActionBox] = "#6f6f6f",
        };

        public string GetButtonsColor(ButtonTypes type) => (ButtonsColors.ContainsKey(type) ? ButtonsColors[type] : String.Empty);

        public string GetColor(Game.Data.ColorTypes type) => (Colors.ContainsKey(type) ? Colors[type] : String.Empty);

        public static Dictionary<string, int> GetStartValues() => new Dictionary<string, int>
        {
            ["Skill"] = 30,
            ["Weapon"] = 15,
            ["Stealth"] = 3,
        };

        public override List<int> GetParagraphsWithoutStatuses() => new List<int> { 0 };
    }
}
