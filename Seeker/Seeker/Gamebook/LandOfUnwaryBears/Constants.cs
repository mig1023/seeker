using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.LandOfUnwaryBears
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#d52b1e",
            [ButtonTypes.Option] = "#d52b1e",
            [ButtonTypes.Continue] = "#e57f78",
            [ButtonTypes.Font] = "#eede49",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#ffdadb",
            [ColorTypes.StatusBar] = "#aa2218",
            [ColorTypes.StatusFont] = "#eede49",
        };

        public override string GetFont() => "RobotoFont";
    }
}
