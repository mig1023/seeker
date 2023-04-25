using System;
using System.Collections.Generic;
using System.Linq;

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
            else if (Name == "Enemy")
            {
                List<string> enemy = ValueString.Split(',').ToList();
                protagonist.EnemyName = enemy[0].Trim();
                protagonist.EnemyHitpoints = int.Parse(enemy[1].Trim());
            }
            else if (Name == "EnemyDiceWound")
            {
                protagonist.EnemyHitpoints -= Game.Dice.Roll();
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
            else if (Name == "QuantityIncrease")
            {
                protagonist.Quantity += protagonist.Increase;
            }
            else
            {
                base.Do(protagonist);
            }
        }
    }
}
