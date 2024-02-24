using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.Ants
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (!AntsModification())
                base.Do(Character.Protagonist);
        }

        private bool AntsModification()
        {
            switch (Name)
            {
                case "Dice":
                    Character.Protagonist.Dice[Game.Dice.Roll()] = true;
                    return true;

                case "Undice":
                    Character.Protagonist.Dice = new List<bool> { false, false, false, false, false, false, false };
                    return true;

                case "Enemy":
                    List<string> enemy = ValueString.Split(',').ToList();
                    Character.Protagonist.EnemyName = enemy[0].Trim();
                    Character.Protagonist.EnemyHitpoints = int.Parse(enemy[1].Trim());
                    return true;

                case "EnemyDiceWound":
                    Character.Protagonist.EnemyHitpoints -= Game.Dice.Roll();
                    return true;

                case "NoMoreEnemy":
                    Character.Protagonist.EnemyName = String.Empty;
                    Character.Protagonist.EnemyHitpoints = 0;
                    return true;

                case "UseDefence":
                    Character.Protagonist.Defence -= 1;
                    return true;

                case "TossCoin":
                    if (Game.Dice.Roll(size: 2) == 1)
                        Game.Option.Trigger("Решка");
                    return true;

                case "QuantityIncrease":
                    Character.Protagonist.Quantity += Character.Protagonist.Increase;
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
