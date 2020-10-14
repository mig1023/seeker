using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Game.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.SwampFever
{
    class Constants : Interfaces.IConstants
    {
        static Dictionary<ButtonTypes, string> ButtonsColors = new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#5c6649",
            [ButtonTypes.Action] = "#242d13",
            [ButtonTypes.Option] = "#494949",
        };

        static Dictionary<ColorTypes, string> Colors = new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#34411c",
            [ColorTypes.Font] = "#eaece8",
            [ColorTypes.ActionBox] = "#707a60",
            [ColorTypes.StatusBar] = "#485432",
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
