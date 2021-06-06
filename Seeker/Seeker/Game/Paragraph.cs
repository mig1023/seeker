using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Game
{
    class Paragraph
    {     
        public List<Option> Options { get; set; }

        public List<Abstract.IActions> Actions { get; set; }

        public List<Abstract.IModification> Modification { get; set; }

        public Dictionary<string, string> Images { get; set; }

        public string Trigger { get; set; }

        public string LateTrigger { get; set; }

        public string RemoveTrigger { get; set; }
    }
}
