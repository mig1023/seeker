using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.StainlessSteelRat
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        static Dictionary<ButtonTypes, string> ButtonsColors = new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#738595",
            [ButtonTypes.Continue] = "#8f9daa",
        };


        static Dictionary<ColorTypes, string> Colors = new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#b9c2ca",
        };

        public string GetButtonsColor(ButtonTypes type) => (ButtonsColors.ContainsKey(type) ? ButtonsColors[type] : String.Empty);

        public string GetColor(Game.Data.ColorTypes type) => (Colors.ContainsKey(type) ? Colors[type] : String.Empty);

        public override Output.Interface.TextFontSize GetFontSize() => Output.Interface.TextFontSize.big;
    }
}
