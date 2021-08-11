using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.SwampFever
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Paragraph Get(int id, XmlNode xmlParagraph) => base.Get(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            action.EnemyName = Xml.StringParse(xmlAction["EnemyName"]);
            action.EnemyCombination = Xml.StringParse(xmlAction["EnemyCombination"]);
            action.Level = Xml.IntParse(xmlAction["Level"]);
            action.Birds = Xml.BoolParse(xmlAction["Birds"]);
            action.Benefit = ModificationParse(xmlAction["Benefit"]);

            return action;
        }

        public override Abstract.IModification ModificationParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return null;

            Modification modification = new Modification
            {
                Name = Xml.StringParse(xmlNode.Attributes["Name"]),
                Value = Xml.IntParse(xmlNode.Attributes["Value"]),
                Multiplication = Xml.BoolParse(xmlNode.Attributes["Multiplication"]),
                Division = Xml.BoolParse(xmlNode.Attributes["Division"]),
            };

            return modification;
        }
    }
}
