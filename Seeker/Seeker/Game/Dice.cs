using System;

namespace Seeker.Game
{
    class Dice
    {
        private static Random rand = new Random();

        private static string DiceSymbols = "⚀⚁⚂⚃⚄⚅";

        public static int Roll(int dices = 1, int size = 6)
        {
            int result = 0;

            for (int i = 0; i < dices; i++)
                result += rand.Next(size) + 1;

            return result;
        }

        public static void DoubleRoll(out int firstDice, out int secondDice)
        {
            firstDice = Dice.Roll();
            secondDice = Dice.Roll();
        }

        public static string Symbol(int dice) => DiceSymbols[dice-1].ToString();
    }
}
