using System;

namespace Seeker.Gamebook.PresidentSimulator
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "ModsByArmyForces")
            {
                Character.Protagonist.Rating += Game.Dice.Roll(Character.Protagonist.Army) + 1;
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
    }
}
