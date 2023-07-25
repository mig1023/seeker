using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seeker.Gamebook.FaithfulSwordOfTheKing
{
    class Services
    {
        public static string ToEcu(int ecu) =>
            String.Format("{0:f2}", (double)ecu / 100).TrimEnd('0').TrimEnd(',').Replace(',', '.');

        public static bool NoMoreEnemies(List<Character> enemies, bool EnemyWoundsLimit) =>
            enemies.Where(x => x.Strength > (EnemyWoundsLimit ? 2 : 0)).Count() == 0;

        public static bool LuckyHit(out int dice, int? roll = null)
        {
            dice = Game.Dice.Roll();
            return (roll ?? dice) % 2 == 0;
        }

        public static bool EnemyWound(Character protagonist, ref Character enemy, List<Character> FightEnemies,
            int roll, int WoundsToWin, ref int enemyWounds, ref List<string> fight, bool EnemyWoundsLimit, bool dagger = false)
        {
            bool swordAndDagger = protagonist.MeritalArt == Character.MeritalArts.SwordAndDagger;
            enemy.Strength -= (swordAndDagger && LuckyHit(out _, roll) && !dagger ? 3 : 2);

            enemyWounds += 1;

            bool enemyLost = NoMoreEnemies(FightEnemies, EnemyWoundsLimit);

            if (enemyLost || ((WoundsToWin > 0) && (WoundsToWin <= enemyWounds)))
            {
                fight.Add(String.Empty);
                fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");
                return true;
            }
            else
                return false;
        }

    }
}
