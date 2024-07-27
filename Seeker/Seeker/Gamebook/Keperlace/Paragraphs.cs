using Seeker.Game;
using System.Xml;

namespace Seeker.Gamebook.Keperlace
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public override Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Paragraph paragraph = ParagraphTemplate(xmlParagraph);

            foreach (XmlNode xmlModification in xmlParagraph.SelectNodes("Modifications/*"))
            {
                if (xmlModification.Name != "Redirect")
                    continue;

                string redirect = xmlModification.Attributes["Value"].InnerText;

                if (!Character.Protagonist.MPaths.ContainsKey(redirect))
                    continue;

                id = Character.Protagonist.MPaths[redirect];

                xmlParagraph = Game.Data.XmlParagraphs[id];
                paragraph = ParagraphTemplate(xmlParagraph);
            }

            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/*"))
                paragraph.Options.Add(OptionParse(xmlOption));

            foreach (XmlNode xmlModification in xmlParagraph.SelectNodes("Modifications/*"))
            {
                if (xmlModification.Name != "Redirect")
                    paragraph.Modification.Add(ModificationParse(xmlModification));
            }

            return paragraph;
        }

        public override Abstract.IModification ModificationParse(XmlNode xmlModification)
        {
            if (xmlModification.Name == "MPath")
            {
                var modification = new Modification
                {
                    Name = xmlModification.Name,
                    ValueString = xmlModification.Attributes["Value"].InnerText
                };

                return modification;
            }
            else
            {
                return (Abstract.IModification)base.ModificationParse(xmlModification, new Modification());
            }
        }
            
    }
}
