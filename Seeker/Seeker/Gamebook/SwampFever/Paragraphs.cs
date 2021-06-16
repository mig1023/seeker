using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;

namespace Seeker.Gamebook.SwampFever
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
            EnemyName = Game.Xml.StringParse(xmlAction["EnemyName"]),
            EnemyCombination = Game.Xml.StringParse(xmlAction["EnemyCombination"]),
            Text = Game.Xml.StringParse(xmlAction["Text"]),
            Price = Game.Xml.IntParse(xmlAction["Price"]),
            Level = Game.Xml.IntParse(xmlAction["Level"]),
            Birds = Game.Xml.BoolParse(xmlAction["Birds"]),
            Benefit = ModificationParse(xmlAction["Benefit"]),
        };

        private static Modification ModificationParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return null;

            Modification modification = new Modification
            {
                Name = Game.Xml.StringParse(xmlNode.Attributes["Name"]),
                Value = Game.Xml.IntParse(xmlNode.Attributes["Value"]),
                Multiplication = Game.Xml.BoolParse(xmlNode.Attributes["Multiplication"]),
                Division = Game.Xml.BoolParse(xmlNode.Attributes["Division"]),
            };

            return modification;
        }
    }
}
