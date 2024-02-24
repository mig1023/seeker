using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.SwampFever
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public override Paragraph Get(int id, XmlNode xmlParagraph) =>
            base.Get(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction) =>
            base.ActionParse(xmlAction, new Actions(), GetProperties(new Actions()), new Modification());

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
           (Abstract.IModification)base.ModificationParse(xmlModification, new Modification());
    }
}
