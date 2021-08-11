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

            foreach (string param in Constants.GetActionParams())
                SetProperty(action, param, xmlAction);

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
