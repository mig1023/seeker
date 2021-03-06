﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Game
{
    class Dice
    {
        private static Random rand = new Random();

        private static Dictionary<int, string> dices = new Dictionary<int, string>
        {
            [1] = "⚀",
            [2] = "⚁",
            [3] = "⚂",
            [4] = "⚃",
            [5] = "⚄",
            [6] = "⚅",
        };

        public static int Roll(int dices = 1)
        {
            int result = 0;

            for (int i = 0; i < dices; i++)
                result += rand.Next(6) + 1;

            return result;
        }

        public static string Symbol(int dice) => dices[dice];
    }
}
