using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook
{
    class Character
    {
        public string Name { get; set; }

        public int Mastery { get; set; }
        public int Endurance { get; set; }
        public int Luck { get; set; }

        public void Init()
        {
            Mastery = Game.Dice.Roll() + 6;
            Endurance = Game.Dice.Roll(dices: 2) + 12;
            Luck = Game.Dice.Roll() + 6;
        }
    }
}
