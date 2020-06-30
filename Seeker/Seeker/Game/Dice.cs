using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Game
{
    class Dice
    {
        private static Random rand = new Random();

        public static int Roll(int dices = 1)
        {
            int result = 0;

            for (int i = 0; i < dices; i++)
                result += rand.Next(6) + 1;

            return result;
        }
    }
}
