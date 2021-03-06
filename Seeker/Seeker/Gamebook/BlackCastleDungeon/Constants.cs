﻿using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.BlackCastleDungeon
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#151515",
            [ButtonTypes.Action] = "#3f3f3f",
            [ButtonTypes.Option] = "#696969",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.StatusBar] = "#2a2a2a",
        };

        public static List<int> GetParagraphsWithoutStaticsButtons() => new List<int> { 0, 619 };

        public static List<string> StaticSpells() => new List<string> { "ЗАКЛЯТИЕ КОПИИ", "ЗАКЛЯТИЕ СИЛЫ", "ЗАКЛЯТИЕ СЛАБОСТИ" };
    }
}
