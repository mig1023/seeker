using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.ThoseWhoAreAboutToDie
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#fcdd76",
            [ButtonTypes.Action] = "#fcdd76",
            [ButtonTypes.Option] = "#fcdd76",
            [ButtonTypes.Font] = "#000000",
            [ButtonTypes.Continue] = "#fdeeba",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#fef4d5",
            [ColorTypes.StatusBar] = "#fce391",
            [ColorTypes.StatusFont] = "#000000",
        };
    }
}
