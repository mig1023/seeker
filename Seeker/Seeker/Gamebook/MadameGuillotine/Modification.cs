using System;

namespace Seeker.Gamebook.MadameGuillotine
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        private static Character protagonist = Character.Protagonist;

        public override void Do()
        {
            if (Name == "InitHitpoints")
            {
                double hitpoint = (double)protagonist.Strength / 2;
                protagonist.Hitpoints = (int)Math.Ceiling(hitpoint);
                protagonist.Wounds = 0;
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
    }
}
