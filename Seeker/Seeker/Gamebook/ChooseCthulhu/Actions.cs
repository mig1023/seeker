﻿using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.ChooseCthulhu
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public override List<string> Status() =>
            new List<string> { String.Format("Посвящение: {0}", protagonist.Initiation) };

        public override bool Availability(string option) =>
            AvailabilityTrigger(option);
    }
}
