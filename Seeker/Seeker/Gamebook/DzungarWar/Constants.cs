using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Game.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.DzungarWar
{
    class Constants : Interfaces.IConstants
    {
        static Dictionary<ButtonTypes, string> ButtonsColors = new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#4a8026",
            [ButtonTypes.Action] = "#339933",
            [ButtonTypes.Option] = "#696969",
            [ButtonTypes.Font] = String.Empty,
            [ButtonTypes.Border] = String.Empty,
        };

        static Dictionary<ColorTypes, string> Colors = new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#c3dcc6",
            [ColorTypes.Font] = String.Empty,
            [ColorTypes.ActionBox] = "#7cb281",
            [ColorTypes.StatusBar] = "#005100",
            [ColorTypes.StatusFont] = String.Empty,
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
