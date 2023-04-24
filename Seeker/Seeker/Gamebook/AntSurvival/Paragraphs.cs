using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.AntSurvival
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Paragraph Get(int id, XmlNode xmlParagraph) =>
            base.Get(xmlParagraph);

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
            Xml.ModificationParse(xmlModification, new Modification());

        public override Option OptionParse(XmlNode xmlOption) =>
            OptionParseWithDo(xmlOption, new Modification());
    }
}
