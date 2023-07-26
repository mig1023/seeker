using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.LegendsAlwaysLie
{
    class Services
    {
        public static bool GoodReaction(ref List<string> reaction)
        {
            int reactionLevel = (int)Math.Floor((double)Character.Protagonist.Hitpoints / 5);
            reaction.Add($"Уровнь реакции: {Character.Protagonist.Hitpoints} / 5 = {reactionLevel}");

            if (Game.Option.IsTriggered("EvilEye"))
            {
                reactionLevel -= 1;
                reaction.Add($"Из-за сглаза уровнь реакции снижается на единицу: {reactionLevel}");
            }

            int reactionDice = Game.Dice.Roll();
            bool goodReaction = reactionDice <= reactionLevel;
            reaction.Add(String.Format("Реакция: {0} {1} {2}", Game.Dice.Symbol(reactionDice), (goodReaction ? "<=" : ">"), reactionLevel));

            return goodReaction;
        }

        public static bool EnemyLostFight(List<Character> FightEnemies, ref List<string> fight, bool connery = false)
        {
            if (FightEnemies.Where(x => x.Hitpoints > 0).Count() > 0)
                return false;
            else
            {
                fight.Add(String.Empty);

                if (connery)
                    fight.Add("BIG|GOOD|Коннери его добил, вы ПОБЕДИЛИ :)");
                else
                    fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");

                return true;
            }
        }

        public static List<string> LostFight(List<string> fight)
        {
            fight.Add(String.Empty);
            fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");

            return fight;
        }
    }
}
