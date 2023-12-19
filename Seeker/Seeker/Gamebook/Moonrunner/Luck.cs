using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.Moonrunner
{
    class Luck
    {
        public static List<string> Check(out bool goodLuck)
        {
            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);

            goodLuck = (firstDice + secondDice) <= Character.Protagonist.Luck;
            string luckLine = goodLuck ? "<=" : ">";

            List<string> luckCheck = new List<string> { $"Проверка удачи: " +
                $"{Game.Dice.Symbol(firstDice)} + {Game.Dice.Symbol(secondDice)} " +
                $"{luckLine} {Character.Protagonist.Luck}" };

            luckCheck.Add(goodLuck ? "BIG|GOOD|УСПЕХ :)" : "BIG|BAD|НЕУДАЧА :(");

            if (Character.Protagonist.Luck > 1)
            {
                Character.Protagonist.Luck -= 1;
                luckCheck.Add("Уровень удачи снижен на единицу");
            }

            return luckCheck;
        }
    }
}
