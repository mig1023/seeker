using System;
using System.Collections.Generic;
using System.Linq;
using Seeker.Game;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;
using System.Reflection;

namespace Seeker.Prototypes
{
    class Constants : Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();
        public static Constants GetInstance() => StaticInstance;

        private Dictionary<ButtonTypes, string> ButtonsColorsList = null;

        private Dictionary<ColorTypes, string> ColorsList = null;

        private Dictionary<string, string> ButtonTextList = null;

        public List<int> WithoutStatuses { get; set; }

        public List<int> WithoutStaticsButtons { get; set; }

        private bool ShowDisabledOptionStatus = false;
        private bool HideSingletonOption = false;
        private bool AdditionalStatusesEqualParts = false;

        private int StartParagraph = 0;

        private Output.Interface.TextFontSize TextFontSizeDefault = Output.Interface.TextFontSize.Normal;

        public virtual string GetColor(ButtonTypes type)
        {
            Dictionary<ButtonTypes, string> color = (Settings.IsEnabled("WithoutStyles") ?
                Output.Constants.DEFAULT_BUTTONS : ButtonsColorsList);

            return (color.ContainsKey(type) ? color[type] : String.Empty);
        }

        public virtual string GetColor(Data.ColorTypes type)
        {
            bool defaultColors = Settings.IsEnabled("WithoutStyles") || (ColorsList == null);

            Dictionary<ColorTypes, string> color = (defaultColors ?
                Output.Constants.DEFAULT_COLORS : ColorsList);

            return (color.ContainsKey(type) ? color[type] : String.Empty);
        }

        public void Clean()
        {
            ButtonsColorsList = new Dictionary<ButtonTypes, string>();
            ColorsList = new Dictionary<ColorTypes, string>();
            WithoutStatuses = new List<int> { 0 };
            WithoutStaticsButtons = new List<int> { 0 };
            ButtonTextList = new Dictionary<string, string>();
            ShowDisabledOptionStatus = false;
            HideSingletonOption = false;
            AdditionalStatusesEqualParts = false;
        }

        public virtual void LoadColor(string type, string color)
        {
            if (Enum.TryParse(type, out ColorTypes colorTypes))
            {
                ColorsList.Add(colorTypes, $"#{color}");
            }
            else if (Enum.TryParse(type, out ButtonTypes buttonTypes))
            {
                ButtonsColorsList.Add(buttonTypes, $"#{color}");
            }
        }

        public virtual List<int> GetParagraphsWithoutStatuses() =>
            WithoutStatuses;

        public virtual List<int> GetParagraphsWithoutStaticsButtons() =>
            WithoutStaticsButtons;

        private void SetPropertyValue(string name, object value) =>
            this.GetType().GetProperty(name).SetValue(this, value);

        public virtual void LoadList(string name, List<string> list)
        {
            PropertyInfo listType = this.GetType().GetProperty(name);

            if (listType.PropertyType == typeof(List<int>))
                SetPropertyValue(name, list.Select(x => int.Parse(x)).ToList());
            else
                SetPropertyValue(name, list.Select(x => x.Trim()).ToList());
        }

        public virtual void LoadDictionary(string name, Dictionary<string, string> dictionary)
        {
            PropertyInfo dictType = this.GetType().GetProperty(name);

            if (dictType.PropertyType == typeof(Dictionary<int, string>))
            {
                SetPropertyValue(name, dictionary.ToDictionary(x => int.Parse(x.Key), x => x.Value));
            }
            else if (dictType.PropertyType == typeof(Dictionary<string, int>))
            {
                SetPropertyValue(name, dictionary.ToDictionary(x => x.Key, x => int.Parse(x.Value)));
            }
            else if (dictType.PropertyType == typeof(Dictionary<int, int>))
            {
                SetPropertyValue(name, dictionary.ToDictionary(x => int.Parse(x.Key), x => int.Parse(x.Value)));
            }
            else
            {
                SetPropertyValue(name, dictionary);
            }
        }

        public static string DefaultColor(Data.ColorTypes type) =>
            Output.Constants.DEFAULT_COLORS[type];

        public virtual string GetFont() =>
            String.Empty;

        public virtual bool GetParagraphsStatusesLimit(out int limitStart, out int limitEnd)
        {
            limitStart = 0;
            limitEnd = 0;

            return false;
        }

        public void LoadAdditionalStatusesEqualParts(string option) =>
            AdditionalStatusesEqualParts = option == "EqualParts";

        public bool ShowAdditionalStatusesEqualParts() =>
             AdditionalStatusesEqualParts;

        public void LoadEnabledDisabledOption(string option, string specific)
        {
            ShowDisabledOptionStatus = option == "Show";
            HideSingletonOption = specific == "SingletonsToo";
        }

        public virtual bool ShowDisabledOption(out bool HideSingleton)
        {
            HideSingleton = HideSingletonOption;
            return ShowDisabledOptionStatus;
        }

        public void LoadStartParagraphOption(string option)
        {
            if (int.TryParse(option, out int paragraph) && (paragraph > 0))
                StartParagraph = paragraph;
            else
                StartParagraph = 0;
        }
        
        public void LoadDefaultFontSize(string option)
        {
            if (Enum.TryParse(option, out Output.Interface.TextFontSize fontSize))
                TextFontSizeDefault = fontSize;
        }

        public virtual Output.Interface.TextFontSize GetFontSize() =>
            TextFontSizeDefault;

        public virtual int GetStartParagraph() =>
            StartParagraph;

        public virtual Dictionary<string, string> ButtonText() =>
            ButtonTextList;

        public void LoadButtonText(string button, string text) =>
            ButtonTextList.Add(button, text);
    }
}
