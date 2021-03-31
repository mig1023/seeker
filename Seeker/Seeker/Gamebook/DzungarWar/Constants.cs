﻿using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.DzungarWar
{
    class Constants : Abstract.IConstants
    {
        static Dictionary<ButtonTypes, string> ButtonsColors = new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#4a8026",
            [ButtonTypes.Action] = "#339933",
            [ButtonTypes.Option] = "#696969",
            [ButtonTypes.Continue] = "#a8c196",
        };

        static Dictionary<ColorTypes, string> Colors = new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#c3dcc6",
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

        public Output.Interface.TextFontSize GetFontSize() => Output.Interface.TextFontSize.normal;

        public double? GetLineHeight() => null;

        public List<int> GetParagraphsWithoutStatuses() => new List<int> { 0, 660 };
    }
}
