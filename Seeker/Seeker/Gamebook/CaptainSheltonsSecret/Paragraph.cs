﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.CaptainSheltonsSecret
{
    class Paragraph
    {
        public List<Game.Option> Options { get; set; }

        public List<Actions> Actions { get; set; }

        public List<Modification> Modification { get; set; }

        public string Trigger { get; set; }
    }
}
