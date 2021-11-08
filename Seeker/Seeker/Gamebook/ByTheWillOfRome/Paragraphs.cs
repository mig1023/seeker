using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.ByTheWillOfRome
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Paragraph paragraph = ParagraphTemplate(xmlParagraph);

            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/Option"))
            {
                Option option = OptionsTemplateWithoutDestination(xmlOption);

                if (int.TryParse(xmlOption.Attributes["Destination"].Value, out int _))
                    option.Destination = Xml.IntParse(xmlOption.Attributes["Destination"]);
                else
                {
                    List<string> destinations = xmlOption.Attributes["Destination"].Value.Split(',').ToList<string>();
                    option.Destination = int.Parse(destinations[random.Next(destinations.Count())]);
                }

                if (xmlOption.Attributes["Do"] != null)
                    option.Do = Xml.ModificationParse(xmlOption, new Modification(), name: "Do");

                paragraph.Options.Add(option);
            }

            foreach (XmlNode xmlModification in xmlParagraph.SelectNodes("Modifications/Modification"))
                paragraph.Modification.Add(ModificationParse(xmlModification));

            return paragraph;
        }

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
           Game.Xml.ModificationParse(xmlModification, new Modification());
    }
}
