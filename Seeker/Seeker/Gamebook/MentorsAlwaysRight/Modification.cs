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
            
            else if (Name == "HealingByVessel")
                Game.Healing.Add("Выпить отвар из бурдюка,8");

            else if (Name == "Transformation")
            {
                Character.Protagonist.Transformation -= 1;
                Character.Protagonist.Hitpoints -= (Game.Data.Triggers.Contains("PainfulTransformation") ? 3 : 2);
            }

            else if (Name == "NoMoreMagic")
            {
                Character.Protagonist.Spells.Clear();
                Character.Protagonist.Transformation = 0;
            }

            else
                InnerDo(Character.Protagonist);
        }
    }
}
