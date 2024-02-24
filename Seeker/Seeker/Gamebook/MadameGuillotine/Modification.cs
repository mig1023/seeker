using System;

namespace Seeker.Gamebook.MadameGuillotine
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "InitHitpoints")
            {
                double hitpoint = (double)Character.Protagonist.Strength / 2;
                Character.Protagonist.Hitpoints = (int)Math.Ceiling(hitpoint);
                Character.Protagonist.Wounds = 0;
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
    }
}
