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
        private Dictionary<ButtonTypes, string> ButtonsColorsList = null;

        private Dictionary<ColorTypes, string> ColorsList = null;

        private Dictionary<string, string> ButtonTextList = null;

        private Dictionary<string, string> ConstantSettings = null;

        public List<int> WithoutStatuses { get; set; }
        public List<int> WithoutStaticsButtons { get; set; }

        private bool WalkingInCirclesAcceptable = false;

        public void Load(string name, string value) =>
            ConstantSettings[name] = value;

        public bool GetBool(string name) =>
            ConstantSettings.ContainsKey(name) && (ConstantSettings[name] == "True");

        public string GetString(string name) =>
            ConstantSettings[name];

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
            ConstantSettings = new Dictionary<string, string>();
            ButtonsColorsList = new Dictionary<ButtonTypes, string>();
            ColorsList = new Dictionary<ColorTypes, string>();
            WithoutStatuses = new List<int> { 0 };
            WithoutStaticsButtons = new List<int> { 0 };
            ButtonTextList = new Dictionary<string, string>();
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

        public void LoadWalkingInCirclesAcceptable(string option) =>
            WalkingInCirclesAcceptable = option == "True";

        public bool GetWalkingInCirclesAcceptable() =>
            WalkingInCirclesAcceptable;

        public virtual Dictionary<string, string> ButtonText() =>
            ButtonTextList;

        public void LoadButtonText(string button, string text) =>
            ButtonTextList.Add(button, text);
    }
}
