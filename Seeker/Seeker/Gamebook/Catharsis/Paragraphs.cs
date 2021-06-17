using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;

namespace Seeker.Gamebook.Catharsis
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Game.Paragraph Get(int id, XmlNode xmlParagraph) => GetTemplate(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction) => new Actions
        {
            ActionName = Game.Xml.StringParse(xmlAction["ActionName"]),
            ButtonName = Game.Xml.StringParse(xmlAction["ButtonName"]),
            Text = Game.Xml.StringParse(xmlAction["Text"]),
            Aftertext = Game.Xml.StringParse(xmlAction["Aftertext"]),
            Trigger = Game.Xml.StringParse(xmlAction["Trigger"]),
            Bonus = Game.Xml.StringParse(xmlAction["Bonus"]),
        };

        public override Option OptionParse(XmlNode xmlOption)
        {
            Option option = OptionsTemplate(xmlOption);

            if (xmlOption.Attributes["Do"] != null)
                option.Do = Game.Xml.ModificationParse(xmlOption, new Modification(), name: "Do");

            return option;
        }

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
            Game.Xml.ModificationParse(xmlModification, new Modification());
    }
}
