using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.TenementBuilding
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        public static Actions GetInstance() => StaticInstance;
        private static Character protagonist = Character.Protagonist;
        public static string LuckNumbers()
        {
            string luckListShow = String.Empty;

            for (int i = 1; i < 7; i++)
            {
                string luck = Constants.LuckList[Character.Protagonist.Luck[i] ? i : i + 10];
                luckListShow += $"{luck} ";
            }

            return luckListShow;
        }

        public List<string> Goodluck()
        {
            List<string> luckCheck = new List<string>
            {
                "Цифры удачи:",
                "BIG|" + LuckNumbers()
            };

            int goodLuck = Game.Dice.Roll();

            string luckLine = protagonist.Luck[goodLuck] ? "не " : String.Empty;
            luckCheck.Add($"Проверка удачи: {Game.Dice.Symbol(goodLuck)} - {luckLine}зачёркунтый");

            if (protagonist.Luck[goodLuck])
            {
                luckCheck.Add("BIG|GOOD|УСПЕХ :)");
                luckCheck.Add($"GRAY|Цифра {goodLuck} теперь зачёркнута");

                protagonist.Luck[goodLuck] = false;
            }
            else
            {
                luckCheck.Add("BIG|BAD|НЕУДАЧА :(");
            }

            return luckCheck;
        }

        public List<string> LuckRecovery()
        {
            List<string> luckRecovery = new List<string> { "Восстановление удачи:" };

            bool success = false;

            for (int i = 1; i < 7; i++)
            {
                if (!protagonist.Luck[i])
                {
                    luckRecovery.Add($"GOOD|Цифра {i} восстановлена!");
                    protagonist.Luck[i] = true;
                    success = true;

                    break;
                }
            }

            if (!success)
                luckRecovery.Add("BAD|Все цифры и так счастливые!");

            luckRecovery.Add("Цифры удачи теперь:");
            luckRecovery.Add("BIG|" + LuckNumbers());

            return luckRecovery;
        }

        public override bool Availability(string option) =>
            AvailabilityTrigger(option);
    }
}
