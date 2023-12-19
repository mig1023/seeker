using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.Moonrunner
{
    class Fights
    {
         public static bool NoMoreEnemies(List<Character> enemies, int WoundsLimit) =>
            enemies.Where(x => x.Endurance > (WoundsLimit > 0 ? WoundsLimit : 0)).Count() == 0;

        public static List<int> TripleDiceRoll(out int failIndex)
        {
            List<int> dices = new List<int>();

            for (int i = 0; i < 3; i++)
                dices.Add(Game.Dice.Roll());

            failIndex = dices.IndexOf(dices.Min());

            return dices;
        }
    }
}
