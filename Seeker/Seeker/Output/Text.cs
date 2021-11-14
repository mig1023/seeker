using System;

namespace Seeker.Output
{
    class Text
    {
        public string Content { get; set; }
        public string Alignment { get; set; }
        public bool Selected { get; set; }

        public Interface.TextFontSize Size { get; set; }

        public bool Bold { get; set; }
        public bool Italic { get; set; }
    }
}
