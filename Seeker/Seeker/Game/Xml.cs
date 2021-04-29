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
    }
}
