using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.FaithfulSwordOfTheKing
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        static Dictionary<ButtonTypes, string> ButtonsColors = new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#911",
            [ButtonTypes.Action] = "#ba2020",
            [ButtonTypes.Option] = "#696969",
        };

        static Dictionary<ColorTypes, string> Colors = new Dictionary<ColorTypes, string>
        {
            [ColorTypes.StatusBar] = "#870808",
        };

        public string GetButtonsColor(ButtonTypes type) => (ButtonsColors.ContainsKey(type) ? ButtonsColors[type] : String.Empty);

        public string GetColor(Game.Data.ColorTypes type) => (Colors.ContainsKey(type) ? Colors[type] : String.Empty);

        public override List<int> GetParagraphsWithoutStatuses() => new List<int> { 0, 660 };

        public static Dictionary<int, int> Skills = new Dictionary<int, int>
        {
            [1] = 12,
            [2] = 8,
            [3] = 10,
            [4] = 7,
            [5] = 9,
            [6] = 11
        };

        public static Dictionary<int, int> Strengths = new Dictionary<int, int>
        {
            [1] = 22,
            [2] = 18,
            [3] = 14,
            [4] = 24,
            [5] = 16,
            [6] = 20
        };
    }
}
