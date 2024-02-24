using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.SwampFever
{
    class Details
    {
        public static string GetUpgratesValues(int index, int part) =>
            Constants.GetUpgrates[index].Split('|')[part - 1];

        public static int CountInCombination(List<int> combination, int dice)
        {
            Dictionary<int, int> counts = combination.GroupBy(x => x).ToDictionary(k => k.Key, v => v.Count());
            return (counts.ContainsKey(dice) ? counts[dice] : 0);
        }

        public static void PurchasesHeads(ref List<string> purchasesReport, bool affordable, bool? prevAffordable)
        {
            if (affordable && (prevAffordable == null))
            {
                purchasesReport.Add("BOLD|ВАМ ДОСТУПНО:");
            }
            else if (prevAffordable != affordable)
            {
                purchasesReport.Add("\nBOLD|ВАМ ПОКА ЕЩЁ НЕ ДОСТУПНО:");
            }
        }

        public static List<string> PursuitWin(List<string> pursuitReport)
        {
            Character.Protagonist.Stigon += 1;

            pursuitReport.Add("BIG|GOOD|Вы настигли шар :)");
            return pursuitReport;
        }

        public static List<string> PursuitFail(List<string> pursuitReport)
        {
            Character.Protagonist.Fury += 1;

            pursuitReport.Add("BIG|BAD|Вы упустили куст :(");
            return pursuitReport;
        }
    }
}
