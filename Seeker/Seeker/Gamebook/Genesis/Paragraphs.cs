using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;

namespace Seeker.Gamebook.Genesis
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Game.Paragraph Get(int id, XmlNode xmlParagraph) => GetTemplate(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());
            action.Bonus = Game.Xml.StringParse(xmlAction["Bonus"]);

            return action;
        }

        public override Option OptionParse(XmlNode xmlOption) => OptionParseWithDo(xmlOption, new Modification());

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
            Game.Xml.ModificationParse(xmlModification, new Modification());
    }
}
