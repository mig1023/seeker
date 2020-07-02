using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook
{
    class Description
    {
        public delegate void ProtagonistInit();

        public string XmlBook;

        public ProtagonistInit Protagonist;

        public Interfaces.IParagraphs Paragraphs;

        public Interfaces.IActions Actions;
    }
}
