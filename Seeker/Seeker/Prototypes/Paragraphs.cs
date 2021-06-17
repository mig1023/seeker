using System;
using System.Collections.Generic;
using System.Text;
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

        public Game.Paragraph GetTemplate(XmlNode xmlParagraph)
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
    }
}
