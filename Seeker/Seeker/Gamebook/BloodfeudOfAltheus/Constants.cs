using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.BloodfeudOfAltheus
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();
        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#ebd5b3",
            [ButtonTypes.Action] = "#ebd5b3",
            [ButtonTypes.Option] = "#f9f2e8",
            [ButtonTypes.Continue] = "#f5ead9",
            [ButtonTypes.Font] = "#000000",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.ActionBox] = "#f9f2e8",
            [ColorTypes.StatusBar] = "#bcaa8f",
            [ColorTypes.StatusFont] = "#000000",
        };

        public static List<int> GetParagraphsWithoutStaticsButtons() => new List<int> { 0, 1 };

        public static Dictionary<int, string> HealthLine() => new Dictionary<int, string>
        {
            [0] = "мертв",
            [1] = "тяжело ранен",
            [2] = "ранен",
            [3] = "здоров",
        };
    }
}
