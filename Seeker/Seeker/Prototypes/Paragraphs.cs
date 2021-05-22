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

        public virtual Game.Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Game.Paragraph paragraph = new Game.Paragraph();

            paragraph.Options = new List<Option>();

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

        public Game.Paragraph ParagraphTemplate(XmlNode xmlParagraph) => new Game.Paragraph
        {
            Options = new List<Option>(),
            Actions = new List<Abstract.IActions>(),
            Modification = new List<Abstract.IModification>(),

            Trigger = Game.Xml.StringParse(xmlParagraph["Triggers"]),
            LateTrigger = Game.Xml.StringParse(xmlParagraph["LateTriggers"]),
            RemoveTrigger = Game.Xml.StringParse(xmlParagraph["RemoveTriggers"]),
            Image = Game.Xml.StringParse(xmlParagraph["Image"]),
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
    }
}
