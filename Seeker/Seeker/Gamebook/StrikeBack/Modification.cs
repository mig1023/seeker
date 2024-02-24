using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.StrikeBack
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "Transformation")
            {
                List<string> transformation = ValueString
                    .Split(',')
                    .Select(x => x.Trim())
                    .ToList();

                Character.Protagonist.Creature = transformation[0];

                Character.Protagonist.MaxAttack = int.Parse(transformation[1]);
                Character.Protagonist.MaxDefence = int.Parse(transformation[2]);
                Character.Protagonist.MaxEndurance = int.Parse(transformation[3]);

                Character.Protagonist.Attack = Character.Protagonist.MaxAttack;
                Character.Protagonist.Defence = Character.Protagonist.MaxDefence;
                Character.Protagonist.Endurance = Character.Protagonist.MaxEndurance;
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
    }
}