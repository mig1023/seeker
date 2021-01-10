﻿using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.AdventuresOfABeardlessDeceiver
{
    class Constants : Abstract.IConstants
    {
        static Dictionary<ButtonTypes, string> ButtonsColors = new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#5da130",
            [ButtonTypes.Action] = "#339933",
            [ButtonTypes.Option] = "#696969",
            [ButtonTypes.Continue] = "#b9cdab",
        };

        static Dictionary<ColorTypes, string> Colors = new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#dbeadc",
            [ColorTypes.ActionBox] = "#7cb281",
            [ColorTypes.StatusBar] = "#005100",
            [ColorTypes.AdditionalStatus] = "#99b999",
        };

        public string GetButtonsColor(ButtonTypes type)
        {
            return (ButtonsColors.ContainsKey(type) ? ButtonsColors[type] : String.Empty);
        }

        public string GetColor(Game.Data.ColorTypes type)
        {
            return (Colors.ContainsKey(type) ? Colors[type] : String.Empty);
        }

        public string GetFont() => String.Empty;

        public bool GetLtlFont() => false;

        public double? GetLineHeight() => null;

        public static List<int> GetParagraphsWithoutStaticsButtons() => new List<int> { 0, 30, 60, 90 };
    }
}
