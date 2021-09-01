using System;
using System.Collections.Generic;
using System.Xml;
using Seeker.Output;

namespace Seeker.Game
{
    class Xml
    {
        public static int IntParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return 0;

            bool success = int.TryParse(xmlNode.InnerText, out int value);

            return (success ? value : 0);
        }

        public static string StringParse(XmlNode xmlNode) => (xmlNode == null ? String.Empty : xmlNode.InnerText);

        public static Dictionary<string, string> ImagesParse(XmlNode xmlNode)
        {
            Dictionary<string, string> images = new Dictionary<string, string>();

            if (xmlNode == null)
                return images;

            foreach (XmlNode xmlImage in xmlNode.SelectNodes("Image"))
                images.Add(StringParse(xmlImage.Attributes["Image"]), StringParse(xmlImage.Attributes["Aftertext"]));

            return images;
        }

        public static bool BoolParse(XmlNode xmlNode) => xmlNode != null;

        public static Abstract.IModification ModificationParse(XmlNode xmlNode, Abstract.IModification modification, string name = "Name")
        {
            if (xmlNode == null)
                return null;

            modification.Name = StringParse(xmlNode.Attributes[name]);
            modification.Value = IntParse(xmlNode.Attributes["Value"]);
            modification.ValueString = StringParse(xmlNode.Attributes["ValueString"]);

            return modification;
        }

        public static string TextParse(int id, string optionName)
        {
            string textByParagraph = String.Empty;

            if (Game.Data.XmlParagraphs[id]["Text"] != null)
                textByParagraph = Game.Data.XmlParagraphs[id]["Text"].InnerText;

            string textByOption = Game.Data.Actions.TextByOptions(optionName);

            return (String.IsNullOrEmpty(textByOption) ? textByParagraph : textByOption);
        }

        public static List<Text> TextsParse(XmlNode xmlNode, bool aftertext = false)
        {
            List<Text> texts = new List<Text>();

            foreach (XmlNode text in xmlNode.SelectNodes(aftertext ? "Aftertexts/Aftertext" : "Texts/Text"))
            {
                string font = StringParse(text.Attributes["Font"]);

                Text outputText = new Text
                {
                    Content = text.InnerText,
                    Bold = (font == "Bold"),
                    Italic = (font == "Italic"),
                    Alignment = StringParse(text.Attributes["Alignment"])
                };

                if (text.Attributes["Size"] != null)
                {
                    Enum.TryParse(StringParse(text.Attributes["Size"]), out Interface.TextFontSize size);
                    outputText.Size = size;
                }
                else
                    outputText.Size = Interface.TextFontSize.nope;

                texts.Add(outputText);
            }

            return texts;
        }
    }
}
