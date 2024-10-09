using System;

namespace Seeker.Gamebook.SeasOfBlood
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "Logbook")
            {
                Character.Protagonist.Logbook += Value;
                Character.Protagonist.Endurance += 1;
            }
            else if (Name == "CyclopsInit")
            {
                Character.Protagonist.Cyclops = 16;
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
    }
}
