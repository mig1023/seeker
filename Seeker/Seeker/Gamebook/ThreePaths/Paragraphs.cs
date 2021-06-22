using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;


namespace Seeker.Gamebook.ThreePaths
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Game.Paragraph Get(int id, XmlNode xmlParagraph) => GetTemplate(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());
            action.ThisIsSpell = Game.Xml.BoolParse(xmlAction["ThisIsSpell"]);

            return action;
        }

        public override Option OptionParse(XmlNode xmlOption) => OptionParseWithDo(xmlOption, new Modification());

        public override Abstract.IModification ModificationParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return null;

            Modification modification = new Modification
            {
                Name = Game.Xml.StringParse(xmlNode.Attributes["Name"]),
                Value = Game.Xml.IntParse(xmlNode.Attributes["Value"]),
                ValueString = Game.Xml.StringParse(xmlNode.Attributes["ValueString"]),
            };

            if (xmlNode.Attributes["Init"] != null)
            {
                modification.Init = true;
                modification.Do();
            }

            return modification;
        }
    }
}
