using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook
{
    class Description
    {
        public delegate void ProtagonistMethod();

        public delegate string SaveMethod();

        public delegate void LoadMethod(string saveLine); 

        public delegate bool CheckOnlyIfMethod(string option);

        public string XmlBook;

        public string BookColor;

        public string FontColor;

        public string BorderColor;

        public string Illustration;

        public ProtagonistMethod Protagonist;

        public SaveMethod Save;

        public LoadMethod Load;

        public CheckOnlyIfMethod CheckOnlyIf;

        public Interfaces.IParagraphs Paragraphs;

        public Interfaces.IActions Actions;

        public Interfaces.IConstants Constants;

        public string Disclaimer;

        public bool ShowDisabledOption;
    }
}
