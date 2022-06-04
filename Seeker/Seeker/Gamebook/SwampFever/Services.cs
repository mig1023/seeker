using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.SwampFever
{
    class Services
    {
        private static Character protagonist = Character.Protagonist;

        public static string GetUpgratesValues(int index, int part) => Constants.GetUpgrates[index].Split('|')[part - 1];

        public static int CountInCombination(List<int> combination, int dice)
        {
            Dictionary<int, int> counts = combination.GroupBy(x => x).ToDictionary(k => k.Key, v => v.Count());
            return (counts.ContainsKey(dice) ? counts[dice] : 0);
        }

        public static void PurchasesHeads(ref List<string> purchasesReport, bool affordable, bool? prevAffordable)
        {
            if (affordable && (prevAffordable == null))
                purchasesReport.Add("BOLD|ВАМ ДОСТУПНО:");

            else if (prevAffordable != affordable)
                purchasesReport.Add("\nBOLD|ВАМ ПОКА ЕЩЁ НЕ ДОСТУПНО:");
        }

        public static List<string> PursuitWin(List<string> pursuitReport)
        {
            protagonist.Stigon += 1;

            pursuitReport.Add("BIG|GOOD|Вы настигли шар :)");
            return pursuitReport;
        }

        public static List<string> PursuitFail(List<string> pursuitReport)
        {
            protagonist.Fury += 1;

            pursuitReport.Add("BIG|BAD|Вы упустили куст :(");
            return pursuitReport;
        }

        public static int ThinkAboutMovement(int myPosition, int step, List<int> bombs, ref List<string> cavityReport)
        {
            int myMovementType = 0;

            if (!bombs.Contains(myPosition + 4) && !bombs.Contains(myPosition + 3) && !bombs.Contains(myPosition + 5))
            {
                cavityReport.Add("Думаем: попробуем рвануть на гусеницах");
                myMovementType = 6;
            }
            else if (!bombs.Contains(myPosition + 2) && (!bombs.Contains(myPosition + 1) || !bombs.Contains(myPosition + 3)))
            {
                cavityReport.Add("Думаем: попробуем тихонечко, на гребных винтах");
                myMovementType = 1;
            }
            else if ((step > 2))
            {
                cavityReport.Add("Думаем: опасно, но нужно срочно прорываться, иначе накроет лава!");
                myMovementType = 6;
            }
            else
                cavityReport.Add("Думаем: лучше постоим нафиг");

            if (bombs.Contains(myPosition) && (myMovementType == 0))
            {
                cavityReport.Add("Думаем: сейчас на вас упадёт вулканическая бомба - нужно рвать когти!");
                myMovementType = Game.Dice.Roll();
            }

            return myMovementType;
        }
    }
}
