using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.CreatureOfHavoc
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#145334",
            [ButtonTypes.Action] = "#104229",
            [ButtonTypes.Option] = "#42755c",
            [ButtonTypes.Continue] = "#a8c196",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#c9d7d0",
            [ColorTypes.StatusBar] = "#0e3b24",
        };
    }
}
