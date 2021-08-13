using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.SwampFever
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Paragraph Get(int id, XmlNode xmlParagraph) => base.Get(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction) =>
            base.ActionParse(xmlAction, new Actions(), Constants.GetActionParams(), new Modification());

        public override Abstract.IModification ModificationParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return null;

            Modification modification = new Modification();

            foreach (string param in Constants.GetModsParams())
                SetPropertyByAttr(modification, param, xmlNode);

            return modification;
        }
    }
}
