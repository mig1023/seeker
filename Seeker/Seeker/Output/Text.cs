using System;
using System.Drawing;

namespace Seeker.Output
{
    class Text
    {
        public string Content { get; set; }
        public string Alignment { get; set; }
        public bool Selected { get; set; }
        public bool Box { get; set; }

        public Interface.TextFontSize Size { get; set; }
        public string Background { get; set; }

        public bool Bold { get; set; }
        public bool Italic { get; set; }
    }
}
