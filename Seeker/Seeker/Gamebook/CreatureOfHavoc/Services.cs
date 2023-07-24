using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.CreatureOfHavoc
{
    class Services
    {
        public static bool WoundAndDeath(ref List<string> fight, ref Character protagonist, string enemy, int wounds = 2)
        {
            if (wounds == 2)
                fight.Add($"BAD|{enemy} ранил вас");

            protagonist.Endurance -= wounds;

            if (protagonist.Endurance <= 0)
            {
                fight.Add(String.Empty);
                fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool NoMoreEnemies(List<Character> enemies) =>
            enemies.Where(x => x.Endurance > 0).Count() == 0;
    }
}
