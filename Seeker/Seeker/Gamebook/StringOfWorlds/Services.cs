﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.StringOfWorlds
{
    class Services
    {
        public static string LuckNumbers()
        {
            string luckListShow = String.Empty;

            for (int i = 1; i < 7; i++)
                luckListShow += String.Format("{0} ", Constants.LuckList[Character.Protagonist.Luck[i] ? i : i + 10]);

            return luckListShow;
        }

        public static bool NoMoreEnemies(List<Character> enemies, bool EnemyWoundsLimit) =>
            enemies.Where(x => x.Strength > (EnemyWoundsLimit ? 2 : 0)).Count() == 0;
    }
}
