using System;

namespace Seeker.Gamebook.ThoseWhoAreAboutToDie
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();

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

        public override void Init()
        {
            Name = String.Empty;
            Reaction = Game.Dice.Roll(dices: 2);
            Strength = Game.Dice.Roll(dices: 2);
            Endurance = Game.Dice.Roll(dices: 2);
        }

        public Character Clone() => new Character() {
            Name = this.Name,
            Reaction = this.Reaction,
            Strength = this.Strength,
            Endurance = this.Endurance,
        };

        public override string Save() => String.Join("|",
            Reaction,
            Strength,
            Endurance
        );

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Reaction = int.Parse(save[0]);
            Strength = int.Parse(save[1]);
            Endurance = int.Parse(save[2]);
        }
    }
}
