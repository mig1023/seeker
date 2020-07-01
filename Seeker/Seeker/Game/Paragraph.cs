using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Game
{
    class Paragraph
    {
        public string Title { get; set; }
        public string Text { get; set; }
        
        public List<Option> Options { get; set; }

        public Seeker.Interfaces.IActions Action { get; set; }

        public string OpenOption { get; set; }
        public bool GoodLuckCheck { get; set; }
    }
}
