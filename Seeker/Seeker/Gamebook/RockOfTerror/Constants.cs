using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Game.Buttons;

namespace Seeker.Gamebook.RockOfTerror
{
    class Constants : Interfaces.IConstants
    {
        static Dictionary<ButtonTypes, string> ButtonsColors = new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#191919",
            [ButtonTypes.Action] = "#2a2a2a",
            [ButtonTypes.Option] = "#494949",
        };

        public string GetButtonsColor(ButtonTypes type)
        {
            return ButtonsColors[type];
        }

        public string GetStatusBarColor()
        {
            return "#151515";
        }

        public string GetFontColor()
        {
            return "#FFFFFF";
        }

        public string GetBackgroundColor()
        {
            return "#000000";
        }
    }
}
