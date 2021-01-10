using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.LegendsAlwaysLie
{
    class Constants : Abstract.IConstants
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
            [ColorTypes.AdditionalStatus] = "#f1bbbb",
        };

        public string GetButtonsColor(ButtonTypes type)
        {
            return (ButtonsColors.ContainsKey(type) ? ButtonsColors[type] : String.Empty);
        }

        public string GetColor(Game.Data.ColorTypes type)
        {
            return (Colors.ContainsKey(type) ? Colors[type] : String.Empty);
        }

        public string GetFont() => "YanoneFont";

        public bool GetLtlFont() => true;

        public double? GetLineHeight() => 1.20;

        public static List<int> GetParagraphsWithoutStaticsButtons() => new List<int> { 0, 687, 714, 715, 701, 702 };
    }
}
