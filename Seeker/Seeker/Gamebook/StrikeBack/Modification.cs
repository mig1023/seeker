using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.StrikeBack
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        private static Character protagonist = Character.Protagonist;

        public override void Do()
        {
            if (Name == "Transformation")
            {
                List<string> transformation = ValueString
                    .Split(',')
                    .Select(x => x.Trim())
                    .ToList();

                protagonist.Creature = transformation[0];

                protagonist.MaxAttack = int.Parse(transformation[1]);
                protagonist.MaxDefence = int.Parse(transformation[2]);
                protagonist.MaxEndurance = int.Parse(transformation[3]);

                protagonist.Attack = protagonist.MaxAttack;
                protagonist.Defence = protagonist.MaxDefence;
                protagonist.Endurance = protagonist.MaxEndurance;
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
    }
}