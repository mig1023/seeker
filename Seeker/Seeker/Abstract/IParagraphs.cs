using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Seeker.Abstract
{
    interface IParagraphs
    {
        Game.Paragraph Get(int id, XmlNode xmlParagraph);
    }
}
