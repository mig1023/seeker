using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook
{
    class Description
    {
        public delegate void ProtagonistInit();

        public string XmlBook;

        public string BookColor;

        public ProtagonistInit Protagonist;

        public Interfaces.ICharacter Character;

        public Interfaces.IParagraphs Paragraphs;

        public Interfaces.IActions Actions;

        public Interfaces.IConstants Constants;

        public string Disclaimer;
    }
}
