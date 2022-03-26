using System;
using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Prototypes
{
    class Constants
    {
        private Dictionary<ButtonTypes, string> ButtonsColorsList = null;

        private Dictionary<ColorTypes, string> ColorsList = null;

        public virtual string GetColor(ButtonTypes type)
        {
            Dictionary<ButtonTypes, string> color = (Game.Settings.IsEnabled("WithoutStyles") ?
                Output.Constants.DEFAULT_BUTTONS : ButtonsColorsList);

            return (color.ContainsKey(type) ? color[type] : String.Empty);
        }

        public virtual string GetColor(Game.Data.ColorTypes type)
        {
            Dictionary<ColorTypes, string> color = (Game.Settings.IsEnabled("WithoutStyles") ?
                Output.Constants.DEFAULT_COLORS : ColorsList);

            return (color.ContainsKey(type) ? color[type] : String.Empty);
        }

        public void Clean()
        {
            ButtonsColorsList = new Dictionary<ButtonTypes, string>();
            ColorsList = new Dictionary<ColorTypes, string>();
        }

        public virtual void LoadColor(string type, string color, bool button)
        {
            bool success = Enum.TryParse(type, out ButtonTypes buttonTypes);

            if (success)
                ButtonsColorsList.Add(buttonTypes, color);
        }

        public virtual void LoadColor(string type, string color)
        {
            bool success = Enum.TryParse(type, out Game.Data.ColorTypes colorTypes);

            if (success)
                ColorsList.Add(colorTypes, color);
        }

        public static string DefaultColor(Game.Data.ColorTypes type) => Output.Constants.DEFAULT_COLORS[type];

        public virtual string GetFont() => String.Empty;

        public virtual Output.Interface.TextFontSize GetFontSize() => Output.Interface.TextFontSize.normal;

        public virtual List<int> GetParagraphsWithoutStatuses() => new List<int> { 0 };

        public virtual int? GetParagraphsStatusesLimit() => null;

        public virtual bool ShowDisabledOption() => false;

        public virtual string ButtonText() => String.Empty;
    }
}
