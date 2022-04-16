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

        private Dictionary<string, string> ButtonTextList = null;

        private List<int> ParagraphsWithoutStatuses = null;

        private List<int> ParagraphsWithoutStaticsButtons = null;

        private bool ShowDisabledOptionStatus = false;

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
            ButtonTextList = new Dictionary<string, string>();
            ShowDisabledOptionStatus = false;
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

        public virtual List<int> GetParagraphsWithoutStaticsButtons() => ParagraphsWithoutStaticsButtons;

        public virtual void LoadParagraphsWithoutSomething(XmlDocument xmlFile, string type)
        {
            string nodeName = String.Format("GameBook/Introduction/Paragraphs/List[@Type='{0}']", type);
            XmlNode paragraphs = xmlFile.SelectSingleNode(nodeName);

            if (paragraphs == null)
                return;

            List<int> something = paragraphs.InnerText.Split(',').Select(x => int.Parse(x)).ToList();

            if (type == "WithoutStaticsButtons")
                ParagraphsWithoutStaticsButtons = something;

            else if (type == "WithoutStatuses")
                ParagraphsWithoutStatuses = something;
        }

        public static string DefaultColor(Game.Data.ColorTypes type) => Output.Constants.DEFAULT_COLORS[type];

        public virtual string GetFont() => String.Empty;

        public virtual Output.Interface.TextFontSize GetFontSize() => Output.Interface.TextFontSize.normal;

        public virtual int? GetParagraphsStatusesLimit() => null;

        public void LoadEnabledDisabledOption(string option) => ShowDisabledOptionStatus = (option == "Show");

        public virtual bool ShowDisabledOption() => ShowDisabledOptionStatus;

        public virtual Dictionary<string, string> ButtonText() => ButtonTextList;

        public void LoadButtonText(string button, string text) => ButtonTextList.Add(button, text);
    }
}
