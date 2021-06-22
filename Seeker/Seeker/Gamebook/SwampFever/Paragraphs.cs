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

        public override Game.Paragraph Get(int id, XmlNode xmlParagraph) => GetTemplate(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            action.EnemyName = Game.Xml.StringParse(xmlAction["EnemyName"]);
            action.EnemyCombination = Game.Xml.StringParse(xmlAction["EnemyCombination"]);
            action.Level = Game.Xml.IntParse(xmlAction["Level"]);
            action.Birds = Game.Xml.BoolParse(xmlAction["Birds"]);
            action.Benefit = ModificationParse(xmlAction["Benefit"]);

            return action;
        }

        public override Abstract.IModification ModificationParse(XmlNode xmlNode)
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
