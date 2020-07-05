using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Game
{
    class Buttons
    {
        public enum ButtonTypes { Main, Action, Option }

        static Dictionary<ButtonTypes, string> ButtonsColors = new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#4682B4",
            [ButtonTypes.Action] = "#6495ED",
            [ButtonTypes.Option] = "#696969",
        };

        public static string NextColor(ButtonTypes button)
        {
            return ButtonsColors[button];
        }
    }
}
