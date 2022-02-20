﻿using System;
using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Prototypes
{
    class Constants
    {
        public virtual Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>();

        public virtual Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>();

        public virtual string GetButtonsColor(ButtonTypes type)
        {
            Dictionary<ButtonTypes, string> color = (Game.Settings.IsEnabled("WithoutStyles") ? DefaultButtons() : ButtonsColors());
            return (color.ContainsKey(type) ? color[type] : String.Empty);
        }

        public virtual string GetColor(Game.Data.ColorTypes type)
        {
            Dictionary<ColorTypes, string> color = (Game.Settings.IsEnabled("WithoutStyles") ? DefaultColors() : Colors());
            return (color.ContainsKey(type) ? color[type] : String.Empty);
        }

        public virtual string GetFont() => String.Empty;

        public virtual Output.Interface.TextFontSize GetFontSize() => Output.Interface.TextFontSize.normal;

        public virtual List<int> GetParagraphsWithoutStatuses() => new List<int> { 0 };

        public virtual int? GetParagraphsStatusesLimit() => null;

        public virtual bool ShowDisabledOption() => false;

        private static Dictionary<ButtonTypes, string> DefaultButtons() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#dcdcdc",
            [ButtonTypes.Action] = "#9d9d9d",
            [ButtonTypes.Option] = "#f1f1f1",
            [ButtonTypes.Font] = "#000000",
            [ButtonTypes.Continue] = "#f1f1f1",
            [ButtonTypes.System] = "#f1f1f1",
        };

        private static Dictionary<ColorTypes, string> DefaultColors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.ActionBox] = "#d7d7d7",
            [ColorTypes.StatusBar] = "#5e5e5e",
            [ColorTypes.StatusFont] = "#ffffff",
            [ColorTypes.Font] = "#000000",
            [ColorTypes.BookColor] = "#000000",
            [ColorTypes.BookFontColor] = "#ffffff",
        };
    }
}
