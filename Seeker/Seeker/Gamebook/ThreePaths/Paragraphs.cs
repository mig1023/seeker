using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;


namespace Seeker.Gamebook.ThreePaths
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Game.Paragraph Get(int id, XmlNode xmlParagraph)
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

        private Actions ActionParse(XmlNode xmlAction) => new Actions
        {
            ActionName = Game.Xml.StringParse(xmlAction["ActionName"]),
            ButtonName = Game.Xml.StringParse(xmlAction["ButtonName"]),
            Aftertext = Game.Xml.StringParse(xmlAction["Aftertext"]),
            Trigger = Game.Xml.StringParse(xmlAction["Trigger"]),
            Text = Game.Xml.StringParse(xmlAction["Text"]),
            ThisIsSpell = Game.Xml.BoolParse(xmlAction["ThisIsSpell"]),
        };

        private Option OptionParse(XmlNode xmlOption)
        {
            Option option = OptionsTemplate(xmlOption);

            if (xmlOption.Attributes["Do"] != null)
                option.Do = Game.Xml.ModificationParse(xmlOption, new Modification(), name: "Do");

            return option;
        }

        private static Modification ModificationParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return null;

            Modification modification = new Modification
            {
                Name = Game.Xml.StringParse(xmlNode.Attributes["Name"]),
                Value = Game.Xml.IntParse(xmlNode.Attributes["Value"]),
                ValueString = Game.Xml.StringParse(xmlNode.Attributes["ValueString"]),
            };

            if (xmlNode.Attributes["Init"] != null)
            {
                modification.Init = true;
                modification.Do();
            }

            return modification;
        }
    }
}
