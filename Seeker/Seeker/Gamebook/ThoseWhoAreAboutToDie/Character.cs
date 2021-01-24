using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.ThoseWhoAreAboutToDie
{
    class Character : Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.ThoseWhoAreAboutToDie.Character();

        public string Name { get; set; }

        private int _reaction;
        public int Reaction
        {
            get => _reaction;
            set
            {
                if (value > 12)
                    _reaction = 12;
                else if (value < 0)
                    _reaction = 0;
                else
                    _reaction = value;
            }
        }

        private int _strength;
        public int Strength
        {
            get => _strength;
            set
            {
                if (value > 12)
                    _strength = 12;
                else if (value < 0)
                    _strength = 0;
                else
                    _strength = value;
            }
        }

        private int _endurance;
        public int Endurance
        {
            get => _endurance;
            set
            {
                if (value > 12)
                    _endurance = 12;
                else if (value < 0)
                    _endurance = 0;
                else
                    _endurance = value;
            }
        }

        public void Init()
        {
            Name = String.Empty;
            Reaction = Game.Dice.Roll(dices: 2);
            Strength = Game.Dice.Roll(dices: 2);
            Endurance = Game.Dice.Roll(dices: 2);
        }

        public Character Clone()
        {
            return new Character() {
                Name = this.Name,
                Reaction = this.Reaction,
                Strength = this.Strength,
                Endurance = this.Endurance,
            };
        }

        public string Save()
        {
            return String.Format("{0}|{1}|{2}", Reaction, Strength, Endurance);
        }

        public void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Reaction = int.Parse(save[0]);
            Strength = int.Parse(save[1]);
            Endurance = int.Parse(save[2]);
        }
    }
}
