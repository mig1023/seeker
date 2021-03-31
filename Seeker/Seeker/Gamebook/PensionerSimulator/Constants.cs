﻿using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.PensionerSimulator
{
    class Constants : Abstract.IConstants
    {
        static Dictionary<ButtonTypes, string> ButtonsColors = new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#030436",
        };

        static Dictionary<ColorTypes, string> Colors = new Dictionary<ColorTypes, string>();

        public string GetButtonsColor(ButtonTypes type)
        {
            return (ButtonsColors.ContainsKey(type) ? ButtonsColors[type] : String.Empty);
        }

        public string GetColor(Game.Data.ColorTypes type)
        {
            return (Colors.ContainsKey(type) ? Colors[type] : String.Empty);
        }

        public string GetFont() => String.Empty;

        public Output.Interface.TextFontSize GetFontSize() => Output.Interface.TextFontSize.big;

        public double? GetLineHeight() => null;

        public List<int> GetParagraphsWithoutStatuses() => new List<int> { 0 };
    }
}
