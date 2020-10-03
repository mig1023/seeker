using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Game.Buttons;

namespace Seeker.Gamebook.AdventuresOfABeardlessDeceiver
{
    class Constants : Interfaces.IConstants
    {
        static Dictionary<ButtonTypes, string> ButtonsColors = new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#5da130",
            [ButtonTypes.Action] = "#339933",
            [ButtonTypes.Option] = "#696969",
        };

        public string GetButtonsColor(ButtonTypes type)
        {
            return ButtonsColors[type];
        }

        public string GetStatusBarColor()
        {
            return "#005100";
        }

        public string GetBackgroundColor()
        {
            return String.Empty;
        }

        public string GetFontColor()
        {
            return String.Empty;
        }
    }
}
