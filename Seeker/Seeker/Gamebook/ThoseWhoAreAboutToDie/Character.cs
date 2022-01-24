﻿using System;

namespace Seeker.Gamebook.ThoseWhoAreAboutToDie
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();

        private int _reaction;
        public int Reaction
        {
            get => _reaction;
            set => _reaction = Game.Param.Setter(value, max: 12, _reaction);
        }

        private int _strength;
        public int Strength
        {
            get => _strength;
            set => _strength = Game.Param.Setter(value, max: 12, _strength);
        }

        private int _endurance;
        public int Endurance
        {
            get => _endurance;
            set => _endurance = Game.Param.Setter(value, max: 12, _endurance);
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
            Reaction, Strength, Endurance
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
