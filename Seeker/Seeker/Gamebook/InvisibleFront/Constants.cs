using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.InvisibleFront
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        static Dictionary<ButtonTypes, string> ButtonsColors = new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#d52b1e",
            [ButtonTypes.Option] = "#d52b1e",
            [ButtonTypes.Continue] = "#e57f78",
            [ButtonTypes.Font] = "#eede49",
        };

        static Dictionary<ColorTypes, string> Colors = new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#ffdadb",
            [ColorTypes.StatusBar] = "#aa2218",
            [ColorTypes.StatusFont] = "#eede49",
        };

        public string GetButtonsColor(ButtonTypes type) => (ButtonsColors.ContainsKey(type) ? ButtonsColors[type] : String.Empty);

        public string GetColor(Game.Data.ColorTypes type) => (Colors.ContainsKey(type) ? Colors[type] : String.Empty);

        public override List<int> GetParagraphsWithoutStatuses() => new List<int> { 0 };

        public static List<string> GetApartments() => new List<string> { "один", "два", "три", "один", "два", "три" };
    }
}
