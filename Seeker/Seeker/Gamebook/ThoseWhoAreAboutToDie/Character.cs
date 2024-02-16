using System;

namespace Seeker.Gamebook.ThoseWhoAreAboutToDie
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public new static Character Protagonist = new Character();
        public new static Character GetInstance() => Protagonist;

        private int _reaction;
        public int Reaction
        {
            get => _reaction;
            set => _reaction = Game.Param.Setter(value, max: 12, _reaction, this);
        }

        private int _strength;
        public int Strength
        {
            get => _strength;
            set => _strength = Game.Param.Setter(value, max: 12, _strength, this);
        }

        private int _endurance;
        public int Endurance
        {
            get => _endurance;
            set => _endurance = Game.Param.Setter(value, max: 12, _endurance, this);
        }

        public override void Init()
        {
            base.Init();

            Reaction = Game.Dice.Roll(dices: 2);
            Strength = Game.Dice.Roll(dices: 2);
            Endurance = Game.Dice.Roll(dices: 2);
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            Reaction = this.Reaction,
            Strength = this.Strength,
            Endurance = this.Endurance,
        };

        public override string Save() => String.Join("|",
            Reaction, Strength, Endurance);

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Reaction = int.Parse(save[0]);
            Strength = int.Parse(save[1]);
            Endurance = int.Parse(save[2]);

            IsProtagonist = true;
        }
    }
}
