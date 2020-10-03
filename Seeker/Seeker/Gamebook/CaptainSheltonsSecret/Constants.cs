using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Game.Buttons;

namespace Seeker.Gamebook.CaptainSheltonsSecret
{
    class Constants : Interfaces.IConstants
    {
        static Dictionary<ButtonTypes, string> ButtonsColors = new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#4682B4",
            [ButtonTypes.Action] = "#6495ED",
            [ButtonTypes.Option] = "#696969",
        };

        public string GetButtonsColor(ButtonTypes type)
        {
            return ButtonsColors[type];
        }

        public string GetStatusBarColor()
        {
            return "#0A5C96";
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
