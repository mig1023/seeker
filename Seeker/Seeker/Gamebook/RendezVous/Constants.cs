using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Game.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.RendezVous
{
    class Constants : Interfaces.IConstants
    {
        static Dictionary<ButtonTypes, string> ButtonsColors = new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#FFFFFF",
            [ButtonTypes.Action] = "#666666",
            [ButtonTypes.Option] = "#FFFFFF",
            [ButtonTypes.Font] = "#000000",
            [ButtonTypes.Border] = "#000000",
        };

        static Dictionary<ColorTypes, string> Colors = new Dictionary<ColorTypes, string>
        {
            [ColorTypes.ActionBox] = "#999999",
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
    }
}
