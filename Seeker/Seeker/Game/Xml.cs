using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

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

            modification.Name = Game.Xml.StringParse(xmlNode.Attributes[name]);
            modification.Value = Game.Xml.IntParse(xmlNode.Attributes["Value"]);
            modification.ValueString = Game.Xml.StringParse(xmlNode.Attributes["ValueString"]);

            return modification;
        }
    }
}
