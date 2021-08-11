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
            Option option = OptionsTemplateWithoutDestination(xmlOption);

            if (int.TryParse(xmlOption.Attributes["Destination"].Value, out int _))
                option.Destination = Xml.IntParse(xmlOption.Attributes["Destination"]);
            else
            {
                List<string> destinations = xmlOption.Attributes["Destination"].Value.Split(',').ToList<string>();
                option.Destination = int.Parse(destinations[random.Next(destinations.Count())]);
            }

            return option;
        }

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
            Xml.ModificationParse(xmlModification, new Modification());
    }
}
