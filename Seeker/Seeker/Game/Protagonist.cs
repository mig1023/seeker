using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Game
{
    class Protagonist
    {
        public static int Mastery { get; set; }
        public static int Endurance { get; set; }
        public static int Luck { get; set; }

        public static void Init()
        {
            Mastery = Dice.Roll() + 6;
            Endurance = Dice.Roll(dices: 2) + 12;
            Luck = Dice.Roll() + 6;
        }
    }
}
