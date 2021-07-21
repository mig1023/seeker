﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.MentorsAlwaysRight
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "Spell")
                Character.Protagonist.Spells.Remove(ValueString);

            else if (Name == "RestoreSpells")
                Character.Protagonist.Spells = new List<string>(Character.Protagonist.SpellsReplica);
            
            else if (Name == "Transformation")
            {
                Character.Protagonist.Transformation -= 1;
                Character.Protagonist.Hitpoints -= 2;
            }
                

            else
                InnerDo(Character.Protagonist);
        }
    }
}
