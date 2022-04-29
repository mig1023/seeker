using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.InvisibleFront
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Paragraph paragraph = ParagraphTemplate(xmlParagraph);

            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/*"))
                paragraph.Options.Add(OptionParse(xmlOption));

            foreach (XmlNode xmlModification in xmlParagraph.SelectNodes("Modifications/Modification"))
                paragraph.Modification.Add(ModificationParse(xmlModification));

            return paragraph;
        }

        public override Option OptionParse(XmlNode xmlOption)
        {
            Option option = OptionsTemplate(xmlOption);

            if (xmlOption.Attributes["Do"] != null)
            {
                Modification modification = new Modification { Name = Xml.StringParse(xmlOption.Attributes["Do"]) };

                ValueParse(xmlOption, ref modification);

                option.Do = modification;
            }

            return option;
        }

        public override Abstract.IModification ModificationParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return null;

            Modification modification = new Modification
            {
                Name = Xml.StringParse(xmlNode.Attributes["Name"]),
                Value = Xml.IntParse(xmlNode.Attributes["Value"]),
            };

            return modification;
        }

        private static void ValueParse(XmlNode xmlOption, ref Modification modification)
        {
            if (xmlOption.Attributes["Value"] == null)
                return;

            if (int.TryParse(xmlOption.Attributes["Value"].Value, out _))
                modification.Value = Xml.IntParse(xmlOption.Attributes["Value"]);
            else
                modification.ValueString = Xml.StringParse(xmlOption.Attributes["Value"]);
        }
    }
}
