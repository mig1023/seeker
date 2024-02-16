using System.Collections.Generic;
using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.Ants
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public new static Paragraphs StaticInstance = new Paragraphs();
        public new static Paragraphs GetInstance() => StaticInstance;

        public override Paragraph Get(int id, XmlNode xmlParagraph) =>
            base.Get(xmlParagraph);

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
            Xml.ModificationParse(xmlModification, new Modification());

        public override Option OptionParse(XmlNode xmlOption)
        {
            List<Abstract.IModification> modifications = new List<Abstract.IModification>();

            foreach (XmlNode optionMod in xmlOption.SelectNodes("*"))
            {
                if (!optionMod.Name.StartsWith("Text"))
                    modifications.Add(new Modification());
            }

            return OptionParseWithDo(xmlOption, modifications);
        }

        public override Abstract.IActions ActionParse(XmlNode xmlAction) =>
            ActionTemplate(xmlAction, new Actions());
    }
}
