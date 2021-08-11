using System;
using System.Collections.Generic;
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
            actions.Name = Game.Xml.StringParse(xmlAction["Name"]);
            actions.Button = Game.Xml.StringParse(xmlAction["Button"]);
            actions.Aftertext = Game.Xml.StringParse(xmlAction["Aftertext"]);
            actions.Trigger = Game.Xml.StringParse(xmlAction["Trigger"]);
            actions.Text = Game.Xml.StringParse(xmlAction["Text"]);
            actions.Price = Game.Xml.IntParse(xmlAction["Price"]);
            actions.Multiple = Game.Xml.BoolParse(xmlAction["Multiple"]);

            return actions;
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
            Aftertext = Game.Xml.StringParse(xmlOption.Attributes["Aftertext"]),
            Input = Game.Xml.StringParse(xmlOption.Attributes["Input"]),
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

        private object PropertyType(object action, XmlNode value, string paramName)
        {
            PropertyInfo param = action.GetType().GetProperty(paramName);

            if (param.PropertyType == typeof(bool))
                return Xml.BoolParse(value[paramName]);

            else if (param.PropertyType == typeof(bool))
                return Xml.IntParse(value[paramName]);

            else
                return Xml.StringParse(value[paramName]);
        }

        public void SetProperty(object action, string param, XmlNode value) =>
            action.GetType().GetProperty(param).SetValue(action, PropertyType(action, value, param));
    }
}
