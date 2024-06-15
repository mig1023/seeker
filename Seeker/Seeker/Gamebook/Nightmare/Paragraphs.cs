using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.Nightmare
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public override Paragraph Get(int id, XmlNode xmlParagraph) =>
            base.Get(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction) =>
            (Actions)ActionTemplate(xmlAction, new Actions());
    }
}
