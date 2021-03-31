using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.LordOfTheSteppes
{
    class Constants : Abstract.IConstants
    {
        static Dictionary<ButtonTypes, string> ButtonsColors = new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#b80f0a",
            [ButtonTypes.Action] = "#a92605",
            [ButtonTypes.Option] = "#878787",
        };

        static Dictionary<ColorTypes, string> Colors = new Dictionary<ColorTypes, string>
        {
            [ColorTypes.StatusBar] = "#b42806",
            [ColorTypes.StatusFont] = "#ffffff",
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

        public Output.Interface.TextFontSize GetFontSize() => Output.Interface.TextFontSize.little;

        public double? GetLineHeight() => 1.20;

        public static Dictionary<string, int> GetStartValues() => new Dictionary<string, int>
        {
            ["Attack"] = 8,
            ["Defence"] = 15,
            ["Endurance"] = 14,
            ["Initiative"] = 10,
        };

        public List<int> GetParagraphsWithoutStatuses() => new List<int> { 0, 904, 905, 906 };
    }
}
