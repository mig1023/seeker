using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Game.Buttons;

namespace Seeker.Gamebook.BlackCastleDungeon
{
    class Constants : Interfaces.IConstants
    {
        static Dictionary<ButtonTypes, string> ButtonsColors = new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#151515",
            [ButtonTypes.Action] = "#3f3f3f",
            [ButtonTypes.Option] = "#696969",
        };

        public string GetButtonsColor(ButtonTypes type)
        {
            return ButtonsColors[type];
        }

        public string GetStatusBarColor()
        {
            return "#2a2a2a";
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
