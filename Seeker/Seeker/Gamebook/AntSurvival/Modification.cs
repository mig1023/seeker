using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.AntSurvival
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        private static Character protagonist = Character.Protagonist;

        public override void Do()
        {
            if (Name == "Dice")
            {
                protagonist.Dice[Game.Dice.Roll()] = true;
            }
            else if (Name == "Undice")
            {
                protagonist.Dice = new List<bool> { false, false, false, false, false, false, false };
            }
            else if (Name == "EnemyDiceWound")
            {
                protagonist.Enemy -= Game.Dice.Roll();
            }
            else if (Name == "UseDefence")
            {
                protagonist.Defence -= 1;
            }
            else if (Name == "TossCoin")
            {
                if (Game.Dice.Roll(size: 2) == 1)
                    Game.Option.Trigger("Решка");
            }
            else
            {
                base.Do(protagonist);
            }
        }
    }
}
