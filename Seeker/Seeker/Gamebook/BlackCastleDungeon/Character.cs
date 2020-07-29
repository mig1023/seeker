using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.BlackCastleDungeon
{
    class Character : Interfaces.ICharacter
    {
        public static Character Protagonist = new Gamebook.BlackCastleDungeon.Character();

        public string Name { get; set; }

        public int Mastery { get; set; }
        public int Endurance { get; set; }
        public int Luck { get; set; }
        public int Gold { get; set; }
        public List<string> Spells { get; set; }
        public int SpellSlots { get; set; }

        public void Init()
        {
            Mastery = Game.Dice.Roll() + 6;
            Endurance = Game.Dice.Roll(dices: 2) + 12;
            Luck = Game.Dice.Roll() + 6;
            Gold = 15;
            SpellSlots = 10;
            Spells = new List<string>();
        }
    }
}
