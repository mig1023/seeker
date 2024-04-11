using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.PrisonerOfMoritaiCastle
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public override Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Paragraph paragraph = ParagraphTemplate(xmlParagraph);

            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/*"))
            {
                Option option = OptionsTemplateWithoutGoto(xmlOption);

                if (ThisIsGameover(xmlOption))
                {
                    option.Goto = GetGoto(xmlOption);
                }
                else if (int.TryParse(xmlOption.Attributes["Goto"].Value, out int _))
                {
                    option.Goto = Xml.IntParse(xmlOption.Attributes["Goto"]);
                }
                else
                {
                    List<string> link = xmlOption.Attributes["Goto"].Value
                        .Split(',')
                        .ToList<string>();

                    option.Goto = int.Parse(link[random.Next(link.Count())]);
                }

                XmlNode trigger = xmlOption.SelectSingleNode("Trigger");

                if (trigger != null)
                    option.Do.Add(Xml.ModificationParse(trigger, new Modification()));

                paragraph.Options.Add(option);
            }

            return paragraph;
        }
    }
}