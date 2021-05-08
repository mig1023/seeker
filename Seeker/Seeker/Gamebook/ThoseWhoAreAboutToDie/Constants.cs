using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.ThoseWhoAreAboutToDie
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        static Dictionary<ButtonTypes, string> ButtonsColors = new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#fcdd76",
            [ButtonTypes.Action] = "#fcdd76",
            [ButtonTypes.Option] = "#fcdd76",
            [ButtonTypes.Font] = "#000000",
            [ButtonTypes.Continue] = "#fdeeba",
        };

        static Dictionary<ColorTypes, string> Colors = new Dictionary<ColorTypes, string>
        {
            [ColorTypes.StatusBar] = "#fce391",
            [ColorTypes.StatusFont] = "#000000",
        };

        public string GetButtonsColor(ButtonTypes type) => (ButtonsColors.ContainsKey(type) ? ButtonsColors[type] : String.Empty);

        public string GetColor(Game.Data.ColorTypes type) => (Colors.ContainsKey(type) ? Colors[type] : String.Empty);

        public override List<int> GetParagraphsWithoutStatuses() => new List<int> { 0 };
    }
}
