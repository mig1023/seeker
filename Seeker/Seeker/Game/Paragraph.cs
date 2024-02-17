using System.Collections.Generic;

namespace Seeker.Game
{
    class Paragraph
    {     
        public List<Output.Text> Texts { get; set; }

        public List<Option> Options { get; set; }

        public List<Abstract.IActions> Actions { get; set; }

        public List<Abstract.IModification> Modification { get; set; }

        public Dictionary<string, string> Images { get; set; }

        public string Trigger { get; set; }

        public string LateTrigger { get; set; }

        public string Untrigger { get; set; }
    }
}
