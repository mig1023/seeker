using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Game
{
    class Paragraph
    {     
        public List<Option> Options { get; set; }

        public Seeker.Interfaces.IActions Action { get; set; }

        public string OpenOption { get; set; }
    }
}
