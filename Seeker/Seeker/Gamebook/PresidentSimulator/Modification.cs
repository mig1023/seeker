using System;

namespace Seeker.Gamebook.PresidentSimulator
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        private static Character protagonist = Character.Protagonist;

        public override void Do()
        {
            if (Name == "ModsByArmyForces")
            {
                protagonist.Rating += Game.Dice.Roll(protagonist.Army) + 1;
            }
            else
            {
                base.Do(protagonist);
            }
        }
    }
}
