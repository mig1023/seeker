using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Game.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.CaptainSheltonsSecret
{
    class Constants : Interfaces.IConstants
    {
        static Dictionary<ButtonTypes, string> ButtonsColors = new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#4682b4",
            [ButtonTypes.Action] = "#6495ed",
            [ButtonTypes.Option] = "#696969",
            [ButtonTypes.Font] = String.Empty,
            [ButtonTypes.Border] = String.Empty,
        };

        static Dictionary<ColorTypes, string> Colors = new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#b5cde1",
            [ColorTypes.Font] = String.Empty,
            [ColorTypes.ActionBox] = "#8fb3f2",
            [ColorTypes.StatusBar] = "#0a5c96",
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
