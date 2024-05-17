using System;

namespace Seeker.Gamebook.ScorpionSwamp
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "HalfEndurance")
            {
                Character.Protagonist.Endurance /= 2;
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
    }
}
