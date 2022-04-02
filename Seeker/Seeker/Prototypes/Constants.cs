using System;
using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;
using System.Linq;
using System.Xml;

namespace Seeker.Prototypes
{
    class Constants
    {
        private Dictionary<ButtonTypes, string> ButtonsColorsList = null;

        private Dictionary<ColorTypes, string> ColorsList = null;

        private List<int> ParagraphsWithoutStatuses = null;

        private List<int> ParagraphsWithoutStaticsButtons = null;

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
            ParagraphsWithoutStatuses = new List<int> { 0 };
            ParagraphsWithoutStaticsButtons = new List<int> { 0 };
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

        public virtual List<int> GetParagraphsWithoutStatuses() => ParagraphsWithoutStatuses;

        public virtual void LoadParagraphsWithoutStatuses(XmlNode paragraphs)
        {
            if (paragraphs != null)
                ParagraphsWithoutStatuses = paragraphs.InnerText.Split(',').Select(x => int.Parse(x)).ToList();
        }

        public virtual List<int> GetParagraphsWithoutStaticsButtons() => ParagraphsWithoutStaticsButtons;

        public virtual void LoadParagraphsWithoutStaticsButtons(XmlNode paragraphs)
        {
            if (paragraphs != null)
                ParagraphsWithoutStaticsButtons = paragraphs.InnerText.Split(',').Select(x => int.Parse(x)).ToList();
        }

        public static string DefaultColor(Game.Data.ColorTypes type) => Output.Constants.DEFAULT_COLORS[type];

        public virtual string GetFont() => String.Empty;

        public virtual Output.Interface.TextFontSize GetFontSize() => Output.Interface.TextFontSize.normal;

        public virtual int? GetParagraphsStatusesLimit() => null;

        public virtual bool ShowDisabledOption() => false;

        public virtual Dictionary<string, string> ButtonText() => new Dictionary<string, string>();
    }
}
