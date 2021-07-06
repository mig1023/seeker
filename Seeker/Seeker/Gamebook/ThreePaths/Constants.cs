using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.ThreePaths
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#009999",
            [ButtonTypes.Option] = "#009999",
            [ButtonTypes.Action] = "#004848",
            [ButtonTypes.Continue] = "#7fcccc",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Font] = "#ccdddd",
            [ColorTypes.Background] = "#007b7b",
            [ColorTypes.ActionBox] = "#6f9f9f",
            [ColorTypes.StatusBar] = "#007a7a",
        };
    }
}
