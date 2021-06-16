using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;

namespace Seeker.Gamebook.RendezVous
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Game.Paragraph Get(int id, XmlNode xmlParagraph) => GetTemplateModXml(xmlParagraph, new Modification());

        public override Abstract.IActions ActionParse(XmlNode xmlAction) => new Actions
        {
            ActionName = Game.Xml.StringParse(xmlAction["ActionName"]),
            ButtonName = Game.Xml.StringParse(xmlAction["ButtonName"]),
            Aftertext = Game.Xml.StringParse(xmlAction["Aftertext"]),
            Trigger = Game.Xml.StringParse(xmlAction["Trigger"]),

            Dices = Game.Xml.IntParse(xmlAction["Dices"]),
        };

        public override Option OptionParse(XmlNode xmlOption) => OptionsTemplate(xmlOption);
    }
}
