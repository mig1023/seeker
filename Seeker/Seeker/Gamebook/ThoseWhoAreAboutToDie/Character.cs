using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.ThoseWhoAreAboutToDie
{
    class Character : Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.ThoseWhoAreAboutToDie.Character();

        public string Name { get; set; }
        public int Reaction { get; set; }
        public int Strength { get; set; }
        public int Endurance { get; set; }

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
