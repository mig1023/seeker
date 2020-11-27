using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.RendezVous
{
    class Constants : Abstract.IConstants
    {
        static Dictionary<ButtonTypes, string> ButtonsColors = new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#FFFFFF",
            [ButtonTypes.Action] = "#FFFFFF",
            [ButtonTypes.Option] = "#FFFFFF",
            [ButtonTypes.Font] = "#000000",
            [ButtonTypes.Border] = "#000000",
            [ButtonTypes.Continue] = "#FFFFFF",
        };

        static Dictionary<ColorTypes, string> Colors = new Dictionary<ColorTypes, string>
        {
            [ColorTypes.ActionBox] = "#efefef",
            [ColorTypes.StatusBar] = "#FFFFFF",
            [ColorTypes.StatusFont] = "#000000",
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

        public bool GetLtlFont() => false;

        public double? GetLineHeight() => null;
    }
}
