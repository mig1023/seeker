using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.PrairieLaw
{
    class Luckiness
    {
        public static string Numbers()
        {
            string luckListShow = String.Empty;

            for (int i = 1; i < 7; i++)
            {
                string luck = Constants.LuckList[Character.Protagonist.Luck[i] ? i : i + 10];
                luckListShow += $"{luck} ";
            }

            return luckListShow;
        }

        public static bool Recovery(List<string> luckRecovery)
        {
            for (int i = 1; i < 7; i++)
            {
                if (!Character.Protagonist.Luck[i])
                {
                    luckRecovery.Add($"GOOD|Цифра {i} восстановлена!");
                    Character.Protagonist.Luck[i] = true;

                    return true;
                }
            }

            return false;
        }
    }
}
