using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.LegendsAlwaysLie
{
    class Fights
    {
        public static bool EnemyLost(List<Character> FightEnemies, ref List<string> fight, bool connery = false)
        {
            if (FightEnemies.Where(x => x.Hitpoints > 0).Count() > 0)
            {
                return false;
            }
            else
            {
                fight.Add(String.Empty);

                if (connery)
                {
                    fight.Add("BIG|GOOD|Коннери его добил, вы ПОБЕДИЛИ :)");
                }
                else
                {
                    fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");
                }

                return true;
            }
        }

        public static List<string> Lost(List<string> fight)
        {
            fight.Add(String.Empty);
            fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");

            return fight;
        }
    }
}
