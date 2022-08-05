﻿using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.StainlessSteelRat
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Paragraph paragraph = ParagraphTemplate(xmlParagraph);

            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/*"))
            {
                Option option = OptionsTemplateWithoutDestination(xmlOption);

                if (ThisIsGameover(xmlOption))
                {
                    option.Destination = GetDestination(xmlOption);
                }
                else if (int.TryParse(xmlOption.Attributes["Destination"].Value, out int _))
                {
                    option.Destination = Xml.IntParse(xmlOption.Attributes["Destination"]);
                }
                else
                {
                    List<string> link = xmlOption.Attributes["Destination"].Value.Split(',').ToList<string>();
                    option.Destination = int.Parse(link[random.Next(link.Count())]);
                }

                paragraph.Options.Add(option);
            }

            return paragraph;
        }
    }
}
