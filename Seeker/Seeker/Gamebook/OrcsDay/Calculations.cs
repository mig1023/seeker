using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.OrcsDay
{
    class Calculations
    {
        public static bool Condition(string conditionParam)
        {
            string[] conditions = conditionParam.Split(',');

            foreach (string condition in conditions)
            {
                bool mustBeFalse = condition.Contains("!");
                bool isTriggered = Game.Option.IsTriggered(condition.Replace("!", String.Empty).Trim());

                if (mustBeFalse == isTriggered)
                    return false;
            }

            return true;
        }

        private static string OrcishnessChange(string line)
        {
            if (line.StartsWith("+"))
            {
                Character.Protagonist.Orcishness += 1;
                return $"BAD|{line}";
            }
            else
            {
                Character.Protagonist.Orcishness -= 1;
                return $"GOOD|{line}";
            }
        }

        public static List<string> Orcishness()
        {
            Character orc = Character.Protagonist;

            orc.Orcishness = 6;

            List<string> orcishness = new List<string> { "BOLD|Изначальное значение: 6" };

            if ((orc.Muscle < 0) || (orc.Wits < 0) || (orc.Courage < 0) || (orc.Luck < 0))
                orcishness.Add(OrcishnessChange(Constants.Orcishness["Negative"]));

            if (orc.Wits > orc.Muscle)
                orcishness.Add(OrcishnessChange(Constants.Orcishness["Wits"]));

            if (orc.Luck > 0)
                orcishness.Add(OrcishnessChange(Constants.Orcishness["Luck"]));

            if ((orc.Muscle > orc.Wits) && (orc.Muscle > orc.Courage) || (orc.Muscle > orc.Luck))
                orcishness.Add(OrcishnessChange(Constants.Orcishness["Muscle"]));

            if (orc.Courage > orc.Wits)
                orcishness.Add(OrcishnessChange(Constants.Orcishness["Courage"]));

            if (orc.Courage > 2)
                orcishness.Add(OrcishnessChange(Constants.Orcishness["TooMuch"]));

            orcishness.Add($"BIG|BOLD|Итоговая Оркишность: {orc.Orcishness}");

            return orcishness;
        }
    }
}
