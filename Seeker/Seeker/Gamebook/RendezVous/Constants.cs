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
            [ColorTypes.Background] = String.Empty,
            [ColorTypes.Font] = String.Empty,
            [ColorTypes.ActionBox] = "#999999",
            [ColorTypes.StatusBar] = "#FFFFFF",
            [ColorTypes.StatusFont] = "#000000",
        };

        public string GetButtonsColor(ButtonTypes type)
        {
            return ButtonsColors[type];
        }

        public string GetColor(Game.Data.ColorTypes type)
        {
            return Colors[type];
        }
    }
}
