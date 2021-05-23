using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.LastHokku
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#deb887",
            [ButtonTypes.Font] = "#000000",
        };

        public override Output.Interface.TextFontSize GetFontSize() => Output.Interface.TextFontSize.big;

        public static List<int> GetParagraphsWithoutHokkuCreation() => new List<int> { 0, 1, 9, 10, 11, 12 };
    }
}
