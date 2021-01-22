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

        private int _endurance;
        public int MaxEndurance { get; set; }
        public int Endurance
        {
            get
            {
                return _endurance;
            }
            set
            {
                if (value > MaxEndurance)
                    _endurance = MaxEndurance;
                else if (value < 0)
                    _endurance = 0;
                else
                    _endurance = value;
            }
        }

        public int Luck { get; set; }

        public void Init()
        {
            Mastery = Game.Dice.Roll() + 6;
            MaxEndurance = Game.Dice.Roll() + 6;
            Endurance = MaxEndurance;
            Endurance = Game.Dice.Roll(dices: 2) + 12;
            Luck = Game.Dice.Roll() + 6;
        }

        public Character Clone()
        {
            return new Character() {
                Name = this.Name,
                Mastery = this.Mastery,
                Endurance = this.Endurance,
                MaxEndurance = this.MaxEndurance,
                Luck = this.Luck,
            };
        }

        public string Save()
        {
            return String.Format(
                "{0}|{1}|{2}|{3}",
                Mastery, Endurance, MaxEndurance, Luck
            );
        }

        public void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Mastery = int.Parse(save[0]);
            Endurance = int.Parse(save[1]);
            MaxEndurance = int.Parse(save[2]);
            Luck = int.Parse(save[3]);
        }
    }
}
