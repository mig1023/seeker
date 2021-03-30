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

        public int GetInitParagraph() => 1;
    }
}
