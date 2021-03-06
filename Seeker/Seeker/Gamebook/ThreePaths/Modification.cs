﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.ThreePaths
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public bool Init { get; set; }

        public override void Do()
        {
            if (Name == "Trigger")
                Game.Option.Trigger(Value.ToString());

            else if (Name == "Time")
            {
                if (Init)
                    Character.Protagonist.Time = 0;
                else
                    Character.Protagonist.Time += Value;
            }

            else if (Name == "RemoveSpell")
                Character.Protagonist.Spells.RemoveAt(Character.Protagonist.Spells.IndexOf(ValueString));
        }
    }
}
