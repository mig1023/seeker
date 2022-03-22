using System;
using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Prototypes
{
    class Constants
    {
        private Dictionary<ButtonTypes, string> ButtonsColorsList = new Dictionary<ButtonTypes, string>();
        private Dictionary<ColorTypes, string> ColorsList = new Dictionary<ColorTypes, string>();

        public virtual Dictionary<ColorTypes, string> Colors() => ColorsList;

        public virtual string GetButtonsColor(ButtonTypes type)
        {
            Dictionary<ButtonTypes, string> color = (Game.Settings.IsEnabled("WithoutStyles") ? DefaultButtons() : ButtonsColorsList);
            return (color.ContainsKey(type) ? color[type] : String.Empty);
        }

        public virtual void LoadButtonsColor(string type, string color)
        {
            bool success = Enum.TryParse(type, out ButtonTypes buttonTypes);

            if (success)
                ButtonsColorsList.Add(buttonTypes, String.Format("#{0}", color));
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

        public virtual string ButtonText() => String.Empty;

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
            [ColorTypes.BookColor] = "#ffffff",
            [ColorTypes.BookFontColor] = "#000000",
            [ColorTypes.BookBorderColor] = "#000000",
        };
    }
}
