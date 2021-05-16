using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.LandOfUnwaryBears
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        static Dictionary<ButtonTypes, string> ButtonsColors = new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#881b13",
            [ButtonTypes.Continue] = "#881b13",
            [ButtonTypes.Font] = "#e5ce3c",
        };

        static Dictionary<ColorTypes, string> Colors = new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Font] = "#e5ce3c",
            [ColorTypes.Background] = "#aa2218",
        };

        public string GetButtonsColor(ButtonTypes type) => (ButtonsColors.ContainsKey(type) ? ButtonsColors[type] : String.Empty);

        public string GetColor(Game.Data.ColorTypes type) => (Colors.ContainsKey(type) ? Colors[type] : String.Empty);

        public override Output.Interface.TextFontSize GetFontSize() => Output.Interface.TextFontSize.normal;
    }
}
