using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Game.Buttons;

namespace Seeker.Gamebook.FaithfulSwordOfTheKing
{
    class Constants : Interfaces.IConstants
    {
        static Dictionary<ButtonTypes, string> ButtonsColors = new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#911",
            [ButtonTypes.Action] = "#ba2020",
            [ButtonTypes.Option] = "#696969",
        };

        public string GetButtonsColor(ButtonTypes type)
        {
            return ButtonsColors[type];
        }

        public string GetStatusBarColor()
        {
            return "#870808";
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
