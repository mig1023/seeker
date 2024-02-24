using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.ChooseCthulhu
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public override Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Constants.ChangeBackground();
            return base.Get(xmlParagraph);
        }

        public override Abstract.IActions ActionParse(XmlNode xmlAction) =>
            (Actions)ActionTemplate(xmlAction, new Actions());

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
            Xml.ModificationParse(xmlModification, new Modification());
    }
}
