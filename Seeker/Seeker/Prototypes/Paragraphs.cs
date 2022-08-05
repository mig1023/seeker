using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using Seeker.Game;

namespace Seeker.Prototypes
{
    class Paragraphs
    {
        internal Random random = new Random();

        public virtual Abstract.IActions ActionParse(XmlNode xmlAction) => null;
        public virtual Option OptionParse(XmlNode xmlOption) => OptionsTemplate(xmlOption);
        public virtual Abstract.IModification ModificationParse(XmlNode xmlxmlModification) => null;

        public virtual Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Paragraph paragraph = new Paragraph { Options = new List<Option>() };

            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/*"))
            {
                Option option = new Option
                {
                    Destination = GetDestination(xmlOption),
                    Text = Xml.StringParse(xmlOption.Attributes["Text"]),
                    Aftertext = Xml.StringParse(xmlOption.Attributes["Aftertext"]),
                };

                paragraph.Options.Add(option);
            }

            return paragraph;
        }

        public Paragraph Get(XmlNode xmlParagraph)
        {
            Paragraph paragraph = ParagraphTemplate(xmlParagraph);

            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/*"))
                paragraph.Options.Add(OptionParse(xmlOption));

            foreach (XmlNode xmlAction in xmlParagraph.SelectNodes("Actions/*"))
                paragraph.Actions.Add(ActionParse(xmlAction));

            foreach (XmlNode xmlModification in xmlParagraph.SelectNodes("Modifications/*"))
                paragraph.Modification.Add(ModificationParse(xmlModification));

            return paragraph;
        }

        protected bool ThisIsGameover(XmlNode xmlOption) =>
            xmlOption.Name == "Gameover";

        protected int GetDestination(XmlNode xmlOption)
        {
            if (ThisIsGameover(xmlOption))
                return Data.Constants.GetStartParagraph();
            else
                return Xml.IntParse(xmlOption.Attributes["Destination"]);
        }

        public Abstract.IActions ActionTemplate(XmlNode xmlAction, Abstract.IActions actions)
        {
            actions.Type = xmlAction.Name;
            actions.Button = Xml.StringParse(xmlAction["Button"]);
            actions.Aftertexts = Xml.TextsParse(xmlAction, action: true);
            actions.Trigger = Xml.StringParse(xmlAction["Trigger"]);
            actions.Text = Xml.StringParse(xmlAction["Text"]);
            actions.Price = Xml.IntParse(xmlAction["Price"]);
            actions.Multiple = Xml.BoolParse(xmlAction["Multiple"]);

            return actions;
        }

        public Abstract.IActions ActionParse(XmlNode xmlAction, Abstract.IActions actions,
            List<string> paramsList, Abstract.IModification modification)
        {
            Abstract.IActions action = ActionTemplate(xmlAction, actions);

            foreach (string param in paramsList)
                SetProperty(action, param, xmlAction);

            if (xmlAction["Benefit"] != null)
                action.Benefit = ModificationParse(xmlAction["Benefit"]);

            if (action.Type == "Option")
                action.Option = OptionParse(xmlAction["Option"]);

            return action;
        }

        public virtual object ModificationParse(XmlNode xmlModification, object modification)
        {
            if (xmlModification == null)
                return null;

            if (xmlModification.Attributes["Name"] == null)
                modification.GetType().GetProperty("Name").SetValue(modification, xmlModification.Name);

            foreach (string param in GetProperties(modification))
                SetPropertyByAttr(modification, param, xmlModification);

            return modification;
        }

        public Paragraph ParagraphTemplate(XmlNode xmlParagraph) => new Paragraph
        {
            Options = new List<Option>(),
            Actions = new List<Abstract.IActions>(),
            Modification = new List<Abstract.IModification>(),

            Trigger = Xml.StringParse(xmlParagraph["Triggers"]),
            LateTrigger = Xml.StringParse(xmlParagraph["LateTriggers"]),
            RemoveTrigger = Xml.StringParse(xmlParagraph["RemoveTriggers"]),

            Images = Xml.ImagesParse(xmlParagraph["Images"]),
        };

        public Option OptionsTemplateWithoutDestination(XmlNode xmlOption) => new Option()
        {
            Text = Xml.StringParse(xmlOption.Attributes["Text"]),
            Availability = Xml.StringParse(xmlOption.Attributes["Availability"]),
            Singleton = Xml.StringParse(xmlOption.Attributes["Singleton"]),
            Input = Xml.StringParse(xmlOption.Attributes["Input"]),
            Aftertext = Xml.StringParse(xmlOption.SelectSingleNode("Text")),
            Aftertexts = Xml.TextsParse(xmlOption),
        };
            
        public Option OptionsTemplate(XmlNode xmlOption)
        {
            Option option = OptionsTemplateWithoutDestination(xmlOption);

            option.Destination = GetDestination(xmlOption);

            return option;
        }

        public Option OptionParseWithDo(XmlNode xmlOption, Abstract.IModification modification)
        {
            Option option = OptionsTemplate(xmlOption);

            XmlNode optionMod = xmlOption.SelectSingleNode("Modification");

            if (optionMod != null)
                option.Do = Xml.ModificationParse(optionMod, modification);

            return option;
        }

        private object PropertyByType(object action, XmlNode value, string paramName)
        {
            if (value == null)
                return null;

            PropertyInfo param = action.GetType().GetProperty(paramName);

            if (param.PropertyType == typeof(bool))
                return Xml.BoolParse(value);

            else if (param.PropertyType == typeof(int))
                return Xml.IntParse(value);

            else if (param.PropertyType == typeof(string))
                return Xml.StringParse(value);

            else
                return null;
        }

        public List<string> GetProperties(object action) => action.GetType().GetProperties().Select(x => x.Name).ToList();

        public void SetProperty(object action, string param, XmlNode value)
        {
            object propetyValue = PropertyByType(action, value[param], param);

            if (propetyValue != null)
                action.GetType().GetProperty(param).SetValue(action, propetyValue);
        }

        public void SetPropertyByAttr(object action, string param, XmlNode value, bool maxPrefix = false)
        {
            string xmlField = (maxPrefix && param.StartsWith("Max")) ? param.Substring(3) : param;

            object propetyValue = PropertyByType(action, value.Attributes[Services.ValueStringFuse(xmlField)], param);

            if (propetyValue != null)
                action.GetType().GetProperty(param).SetValue(action, propetyValue);
        }
    }
}
