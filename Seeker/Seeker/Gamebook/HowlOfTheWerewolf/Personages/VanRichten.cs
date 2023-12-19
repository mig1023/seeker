using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.HowlOfTheWerewolf.Personages
{
    class VanRichten
    {
        public static int Fight(string enemyName, ref List<string> fight, int enemyHitStrength)
        {
            int vanRichtenMastery = Constants.GetVanRichtenMastery();

            fight.Add(string.Empty);

            Game.Dice.DoubleRoll(out int vanRichtenRollFirst, out int vanRichtenRollSecond);
            int vanRichtenHitStrength = vanRichtenRollFirst + vanRichtenRollSecond + vanRichtenMastery;

            fight.Add($"Сила удара Ван Рихтена: " +
                $"{Game.Dice.Symbol(vanRichtenRollFirst)} + " +
                $"{Game.Dice.Symbol(vanRichtenRollSecond)} + " +
                $"{vanRichtenMastery} = {vanRichtenHitStrength}");

            if (vanRichtenHitStrength > enemyHitStrength)
            {
                fight.Add($"GOOD|{enemyName} ранен");
                return 2;
            }
            else
            {
                Character.Protagonist.VanRichten -= 2;

                if (Character.Protagonist.VanRichten <= 0)
                {
                    fight.Add("BIG|BAD|Ван Рихтен погиб, дальше вам придётся одному :(");
                }
                else
                {
                    fight.Add("BAD|Ван Рихтен ранен");
                }

                return 0;
            }
        }
    }
}
