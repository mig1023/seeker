using System;

namespace Seeker.Gamebook.OrcsDay
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "Back")
                Character.Protagonist.WayBack = Value;

            else if (Name == "RestoreHitpoints")
                Character.Protagonist.Hitpoints = 5;

            else
                base.Do(Character.Protagonist);
        }
    }
}
