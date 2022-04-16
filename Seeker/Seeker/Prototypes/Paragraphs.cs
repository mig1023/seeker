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
        public virtual Game.Option OptionParse(XmlNode xmlOption) => OptionsTemplate(xmlOption);
        public virtual Abstract.IModification ModificationParse(XmlNode xmlxmlModification) => null;

        public virtual Game.Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Game.Paragraph paragraph = new Game.Paragraph { Options = new List<Option>() };

            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/Option"))
            {
                Option option = new Option
                {
                    Destination = Game.Xml.IntParse(xmlOption.Attributes["Destination"]),
                    Text = Game.Xml.StringParse(xmlOption.Attributes["Text"]),
                    Aftertext = Game.Xml.StringParse(xmlOption.Attributes["Aftertext"]),
                };

                paragraph.Options.Add(option);
            }

            return paragraph;
        }

        public Game.Paragraph Get(XmlNode xmlParagraph)
        {
            Game.Paragraph paragraph = ParagraphTemplate(xmlParagraph);

            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/Option"))
                paragraph.Options.Add(OptionParse(xmlOption));

            foreach (XmlNode xmlAction in xmlParagraph.SelectNodes("Actions/Action"))
                paragraph.Actions.Add(ActionParse(xmlAction));

            foreach (XmlNode xmlModification in xmlParagraph.SelectNodes("Modifications/Modification"))
                paragraph.Modification.Add(ModificationParse(xmlModification));

            return paragraph;
        }

        public Abstract.IActions ActionTemplate(XmlNode xmlAction, Abstract.IActions actions)
        {
            actions.Name = Game.Xml.StringParse(xmlAction["Type"]);
            actions.Button = Game.Xml.StringParse(xmlAction["Button"]);
            actions.Aftertext = Game.Xml.StringParse(xmlAction["Aftertext"]);
            actions.Aftertexts = Game.Xml.TextsParse(xmlAction, aftertext: true);
            actions.Trigger = Game.Xml.StringParse(xmlAction["Trigger"]);
            actions.Text = Game.Xml.StringParse(xmlAction["Text"]);
            actions.Price = Game.Xml.IntParse(xmlAction["Price"]);
            actions.Multiple = Game.Xml.BoolParse(xmlAction["Multiple"]);

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

            if (action.Name == "Option")
                action.Option = OptionParse(xmlAction["Option"]);

            return action;
        }

        public virtual object ModificationParse(XmlNode xmlModification, object modification)
        {
            if (xmlModification == null)
                return null;

            foreach (string param in GetProperties(modification))
                SetPropertyByAttr(modification, param, xmlModification);

            return modification;
        }

        public Game.Paragraph ParagraphTemplate(XmlNode xmlParagraph) => new Game.Paragraph
        {
            Options = new List<Option>(),
            Actions = new List<Abstract.IActions>(),
            Modification = new List<Abstract.IModification>(),

            Trigger = Game.Xml.StringParse(xmlParagraph["Triggers"]),
            LateTrigger = Game.Xml.StringParse(xmlParagraph["LateTriggers"]),
            RemoveTrigger = Game.Xml.StringParse(xmlParagraph["RemoveTriggers"]),

            Images = Game.Xml.ImagesParse(xmlParagraph["Images"]),
        };

        public Game.Option OptionsTemplateWithoutDestination(XmlNode xmlOption) => new Option()
        {
            Text = Game.Xml.StringParse(xmlOption.Attributes["Text"]),
            OnlyIf = Game.Xml.StringParse(xmlOption.Attributes["OnlyIf"]),
            Singleton = Game.Xml.StringParse(xmlOption.Attributes["Singleton"]),
            Input = Game.Xml.StringParse(xmlOption.Attributes["Input"]),
            Aftertext = Game.Xml.StringParse(xmlOption.SelectSingleNode("Text")),
            Aftertexts = Game.Xml.TextsParse(xmlOption),
        };

        public Game.Option OptionsTemplate(XmlNode xmlOption)
        {
            Game.Option option = OptionsTemplateWithoutDestination(xmlOption);

            option.Destination = Game.Xml.IntParse(xmlOption.Attributes["Destination"]);

            return option;
        }

        public Option OptionParseWithDo(XmlNode xmlOption, Abstract.IModification modification)
        {
            Option option = OptionsTemplate(xmlOption);

            if (xmlOption.Attributes["Do"] != null)
                option.Do = Game.Xml.ModificationParse(xmlOption, modification, name: "Do");

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

            object propetyValue = PropertyByType(action, value.Attributes[xmlField], param);

            if (propetyValue != null)
                action.GetType().GetProperty(param).SetValue(action, propetyValue);
        }
    }
}
