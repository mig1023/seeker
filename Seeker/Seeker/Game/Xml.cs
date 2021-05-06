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

        public static string StringParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return String.Empty;

            return xmlNode.InnerText;
        }

        public static bool BoolParse(XmlNode xmlNode)
        {
            return (xmlNode == null ? false : true);
        }

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
