using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.CreatureOfHavoc
{
    class Character : Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.CreatureOfHavoc.Character();

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

        public Character Clone()
        {
            return new Character() {
                Name = this.Name,
                Mastery = this.Mastery,
                Endurance = this.Endurance,
                Luck = this.Luck,
            };
        }

        public string Save()
        {
            return String.Format(
                "{0}|{1}|{2}",
                Mastery, Endurance, Luck
            );
        }

        public void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Mastery = int.Parse(save[0]);
            Endurance = int.Parse(save[1]);
            Luck = int.Parse(save[2]);
        }
    }
}
