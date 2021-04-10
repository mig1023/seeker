using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.CaptainSheltonsSecret
{
    class Constants : Abstract.IConstants
    {
        static Dictionary<ButtonTypes, string> ButtonsColors = new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#4682b4",
            [ButtonTypes.Action] = "#6495ed",
            [ButtonTypes.Option] = "#696969",
            [ButtonTypes.Continue] = "#90b4d2",
        };

        static Dictionary<ColorTypes, string> Colors = new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#b5cde1",
            [ColorTypes.ActionBox] = "#8fb3f2",
            [ColorTypes.StatusBar] = "#0a5c96",
        };

        public string GetButtonsColor(ButtonTypes type)
        {
            return (ButtonsColors.ContainsKey(type) ? ButtonsColors[type] : String.Empty);
        }

        public string GetColor(Game.Data.ColorTypes type)
        {
            return (Colors.ContainsKey(type) ? Colors[type] : String.Empty);
        }

        public string GetFont() => String.Empty;

        public Output.Interface.TextFontSize GetFontSize() => Output.Interface.TextFontSize.normal;

        public double? GetLineHeight() => null;

        public List<int> GetParagraphsWithoutStatuses() => new List<int> { 0 };

        public static Dictionary<int, int> Masterys() => new Dictionary<int, int>
        {
            [2] = 8,
            [3] = 10,
            [4] = 12,
            [5] = 9,
            [6] = 11,
            [7] = 9,
            [8] = 10,
            [9] = 8,
            [10] = 9,
            [11] = 10,
            [12] = 11
        };

        public static Dictionary<int, int> Endurances() => new Dictionary<int, int>
        {
            [2] = 22,
            [3] = 20,
            [4] = 16,
            [5] = 18,
            [6] = 20,
            [7] = 20,
            [8] = 16,
            [9] = 24,
            [10] = 22,
            [11] = 18,
            [12] = 20
        };

        public static Dictionary<int, string> LuckList() => new Dictionary<int, string>
        {
            [1] = "①",
            [2] = "②",
            [3] = "③",
            [4] = "④",
            [5] = "⑤",
            [6] = "⑥",

            [11] = "❶",
            [12] = "❷",
            [13] = "❸",
            [14] = "❹",
            [15] = "❺",
            [16] = "❻",
        };
    }
}
