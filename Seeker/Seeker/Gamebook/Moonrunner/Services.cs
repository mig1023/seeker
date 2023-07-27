using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.Moonrunner
{
    class Services
    {
        public static List<string> Luck(out bool goodLuck)
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

        public static List<string> Luck() =>
            Luck(out bool _);

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
