using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;

namespace Seeker.Gamebook.RockOfTerror
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Game.Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Game.Paragraph paragraph = ParagraphTemplate(xmlParagraph);

            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/Option"))
                paragraph.Options.Add(OptionsTemplate(xmlOption));

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
        };

        private static Modification ModificationParse(XmlNode xmlModification) => new Modification
        {
            Name = Game.Xml.StringParse(xmlModification.Attributes["Name"]),
            Value = Game.Xml.IntParse(xmlModification.Attributes["Value"]),
            Init = Game.Xml.BoolParse(xmlModification.Attributes["Init"]),
        };
    }
}
