using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.WildDeath
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Paragraph paragraph = ParagraphTemplate(xmlParagraph);

            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/*"))
            {
                Option option = OptionsTemplateWithoutLink(xmlOption);

                if (ThisIsGameover(xmlOption))
                {
                    option.Link = GetLink(xmlOption);
                }
                else if (int.TryParse(xmlOption.Attributes["Link"].Value, out int _))
                {
                    option.Link = Xml.IntParse(xmlOption.Attributes["Link"]);
                }
                else
                {
                    List<string> link = xmlOption.Attributes["Link"].Value.Split(',').ToList<string>();
                    option.Link = int.Parse(link[random.Next(link.Count())]);
                }

                paragraph.Options.Add(option);
            }

            return paragraph;
        }
    }
}
