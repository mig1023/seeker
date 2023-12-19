using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.HowlOfTheWerewolf.Personages
{
    class Ulrich
    {
        public static int Fight(string enemyName, ref List<string> fight, int enemyHitStrength)
        {
            int ulrichMastery = Constants.GetUlrichMastery();

            fight.Add(string.Empty);

            Game.Dice.DoubleRoll(out int ulrichRollFirst, out int ulrichRollSecond);
            int ulrichHitStrength = ulrichRollFirst + ulrichRollSecond + ulrichMastery;

            fight.Add($"Сила удара Ульриха: " +
                $"{Game.Dice.Symbol(ulrichRollFirst)} + " +
                $"{Game.Dice.Symbol(ulrichRollSecond)} + " +
                $"{ulrichMastery} = {ulrichHitStrength}");

            if (ulrichHitStrength > enemyHitStrength)
            {
                fight.Add($"GOOD|{enemyName} ранен");
                return 2;
            }
            else
            {
                fight.Add("BOLD|Ульрих не смог ранить врага");
                return 0;
            }
        }
    }
}
