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
            [ButtonTypes.Action] = "#485432",
            [ButtonTypes.Option] = "#5c6649",
        };

        static Dictionary<ColorTypes, string> Colors = new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#34411c",
            [ColorTypes.Font] = "#eaece8",
            [ColorTypes.ActionBox] = "#707a60",
            [ColorTypes.StatusBar] = "#485432",
        };

        public static Dictionary<int, string> GetRangeTypes()
        {
            return new Dictionary<int, string>
            {
                [6] = "ДАЛЬНЯЯ ДИСТАНЦИЯ",
                [5] = "СРЕДНЯЯ ДИСТАНЦИЯ",
                [4] = "БЛИЖНЯЯ ДИСТАНЦИЯ",
            };
        }

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
