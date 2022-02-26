using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.DeathOfAntiquary
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
            Xml.ModificationParse(xmlModification, new Modification());
    }
}
