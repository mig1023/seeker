using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.Ants
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        private static Character protagonist = Character.Protagonist;

        public override void Do()
        {
            if (!AntsModification())
                base.Do(protagonist);
        }

        private bool AntsModification()
        {
            switch (Name)
            {
                case "Dice":
                    protagonist.Dice[Game.Dice.Roll()] = true;
                    return true;

                case "Undice":
                    protagonist.Dice = new List<bool> { false, false, false, false, false, false, false };
                    return true;

                case "Enemy":
                    List<string> enemy = ValueString.Split(',').ToList();
                    protagonist.EnemyName = enemy[0].Trim();
                    protagonist.EnemyHitpoints = int.Parse(enemy[1].Trim());
                    return true;

                case "EnemyDiceWound":
                    protagonist.EnemyHitpoints -= Game.Dice.Roll();
                    return true;

                case "NoMoreEnemy":
                    protagonist.EnemyName = String.Empty;
                    protagonist.EnemyHitpoints = 0;
                    return true;

                case "UseDefence":
                    protagonist.Defence -= 1;
                    return true;

                case "TossCoin":
                    if (Game.Dice.Roll(size: 2) == 1)
                        Game.Option.Trigger("Решка");
                    return true;

                case "QuantityIncrease":
                    protagonist.Quantity += protagonist.Increase;
                    return true;

                case "HeadChange":
                    foreach (string head in Constants.Government.Keys)
                        Game.Option.Trigger(head, remove: true);
                    return true;

                default:
                    return false;
            }
        }
    }
}
