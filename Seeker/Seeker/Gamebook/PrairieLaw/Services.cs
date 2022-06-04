using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.PrairieLaw
{
    class Services
    {
        public static string ToDollars(int cents) =>
            String.Format("{0:f2}", (double)cents / 100).TrimEnd('0').TrimEnd(',').Replace(',', '.');

        public static string LuckNumbers()
        {
            string luckListShow = String.Empty;

            for (int i = 1; i < 7; i++)
                luckListShow += String.Format("{0} ", Constants.LuckList[Character.Protagonist.Luck[i] ? i : i + 10]);

            return luckListShow;
        }

        public static bool NoMoreEnemies(List<Character> enemies, bool EnemyWoundsLimit) =>
            enemies.Where(x => x.Strength > (EnemyWoundsLimit ? 2 : 0)).Count() == 0;

        public static bool FirefightContinue(List<Character> enemies, ref List<string> fight, bool firefight)
        {
            if (!firefight)
                return false;

            if (Character.Protagonist.Cartridges > 0)
                return true;

            if (enemies.Where(x => x.Cartridges > 0).Count() > 0)
                return true;

            if (firefight)
            {
                fight.Add("BOLD|У всех закончились патроны, дальше бой продолжится на кулаках");
                fight.Add(String.Empty);
            }

            return false;
        }
    }
}
