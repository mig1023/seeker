using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.OctopusIsland
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#c93c20",
            [ButtonTypes.Action] = "#c93c20",
            [ButtonTypes.Option] = "#c93c20",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.StatusBar] = "#ce4f36",
        };
    }
}
