using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.ThoseWhoAreAboutToDie
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Paragraph Get(int id, XmlNode xmlParagraph) => base.Get(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction) => (Actions)ActionTemplate(xmlAction, new Actions());

        public override Option OptionParse(XmlNode xmlOption)
        {
            Option option = OptionsTemplateWithoutLink(xmlOption);

            if (int.TryParse(xmlOption.Attributes["Link"].Value, out int _))
                option.Link = Xml.IntParse(xmlOption.Attributes["Link"]);
            else
            {
                List<string> link = xmlOption.Attributes["Link"].Value.Split(',').ToList<string>();
                option.Link = int.Parse(link[random.Next(link.Count())]);
            }

            return option;
        }

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
            Xml.ModificationParse(xmlModification, new Modification());
    }
}
