﻿using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.WalkInThePark
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public override Paragraph Get(int id, XmlNode xmlParagraph) =>
            base.Get(xmlParagraph);
    }
}
