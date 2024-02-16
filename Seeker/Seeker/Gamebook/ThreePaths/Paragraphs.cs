using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.ThreePaths
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public new static Paragraphs StaticInstance = new Paragraphs();
        public new static Paragraphs GetInstance() => StaticInstance;

        public override Paragraph Get(int id, XmlNode xmlParagraph) =>
            base.Get(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());
            action.ThisIsSpell = Xml.BoolParse(xmlAction["ThisIsSpell"]);

            return action;
        }

        public override Option OptionParse(XmlNode xmlOption) =>
            OptionParseWithDo(xmlOption, new Modification());

        public override Abstract.IModification ModificationParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return null;

            Modification modification = new Modification();

            foreach (string param in GetProperties(modification))
                SetPropertyByAttr(modification, param, xmlNode);

            if (xmlNode.Attributes["Init"] != null)
            {
                modification.Init = true;
                modification.Do();
            }

            return modification;
        }
    }
}
