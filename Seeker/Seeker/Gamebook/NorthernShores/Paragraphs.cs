﻿using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.NorthernShores
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public override Paragraph Get(int id, XmlNode xmlParagraph) =>
            base.Get(xmlParagraph);

        public override Option OptionParse(XmlNode xmlOption)
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
                List<string> link = xmlOption.Attributes["Goto"].Value.Split(',').ToList<string>();
                option.Goto = int.Parse(link[random.Next(link.Count())]);
            }

            return option;
        }
    }
}
