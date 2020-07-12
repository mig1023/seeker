﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Game
{
    class Paragraph
    {     
        public List<Option> Options { get; set; }

        public List<Interfaces.IActions> Actions { get; set; }

        public Interfaces.IModification Modification { get; set; }

        public string OpenOption { get; set; }
    }
}
