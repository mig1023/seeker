using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.RockOfTerror
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();
        public static Paragraphs GetInstance() => StaticInstance;

        public override Paragraph Get(int id, XmlNode xmlParagraph) =>
            base.Get(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction) =>
            base.ActionParse(xmlAction, new Actions(), GetProperties(new Actions()), new Modification());

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) => new Modification
        {
            Name = xmlModification.Name,
            Value = Xml.IntParse(xmlModification.Attributes["Value"]),
            Init = Xml.BoolParse(xmlModification.Attributes["Init"]),
        };
    }
}
