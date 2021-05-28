using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;

namespace Seeker.Gamebook.PensionerSimulator
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Game.Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Game.Paragraph paragraph = ParagraphTemplate(xmlParagraph);

            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/Option"))
            {
                Option option = OptionsTemplate(xmlOption);

                if (xmlOption.Attributes["Do"] != null)
                    option.Do = new Modification
                    {
                        Name = Game.Xml.StringParse(xmlOption.Attributes["Do"]),
                        ValueString = Game.Xml.StringParse(xmlOption.Attributes["Value"])
                    };

                paragraph.Options.Add(option);
            }

            return paragraph;
        }
    }
}
