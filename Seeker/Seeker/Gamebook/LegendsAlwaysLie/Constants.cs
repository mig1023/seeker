using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.LegendsAlwaysLie
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#911",
            [ButtonTypes.Action] = "#ba2020",
            [ButtonTypes.Option] = "#cc8888",
            [ButtonTypes.Continue] = "#cc8888",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#f5e7e5",
            [ColorTypes.StatusBar] = "#870808",
            [ColorTypes.AdditionalStatus] = "#b70b0b",
            [ColorTypes.AdditionalFont] = "#ffffff",
        };

        public override Output.Interface.TextFontSize GetFontSize() => Output.Interface.TextFontSize.little;

        public override double? GetLineHeight() => 1.20;

        public static List<int> GetParagraphsWithoutStaticsButtons() => new List<int> { 0, 687, 714, 715, 701, 702 };

        public override List<int> GetParagraphsWithoutStatuses() => new List<int> { 0, 714, 715, 716 };
    }
}
