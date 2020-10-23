using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Game.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.AdventuresOfABeardlessDeceiver
{
    class Constants : Interfaces.IConstants
    {
        static Dictionary<ButtonTypes, string> ButtonsColors = new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#5da130",
            [ButtonTypes.Action] = "#339933",
            [ButtonTypes.Option] = "#696969",
            [ButtonTypes.Font] = String.Empty,
            [ButtonTypes.Border] = String.Empty,
        };

        static Dictionary<ColorTypes, string> Colors = new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = String.Empty,
            [ColorTypes.Font] = String.Empty,
            [ColorTypes.ActionBox] = String.Empty,
            [ColorTypes.StatusBar] = "#005100",
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
