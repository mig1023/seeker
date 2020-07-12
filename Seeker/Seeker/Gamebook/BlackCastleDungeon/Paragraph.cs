using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.BlackCastleDungeon
{
    class Paragraph
    {
        public List<Game.Option> Options { get; set; }

        public List<Actions> Actions { get; set; }

        public Interfaces.IModification Modification { get; set; }

        public string OpenOption { get; set; }
    }
}
