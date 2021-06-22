using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;

namespace Seeker.Gamebook.RockOfTerror
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Game.Paragraph Get(int id, XmlNode xmlParagraph) => GetTemplate(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction) => (Actions)ActionTemplate(xmlAction, new Actions());

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) => new Modification
        {
            Name = Game.Xml.StringParse(xmlModification.Attributes["Name"]),
            Value = Game.Xml.IntParse(xmlModification.Attributes["Value"]),
            Init = Game.Xml.BoolParse(xmlModification.Attributes["Init"]),
        };
    }
}
