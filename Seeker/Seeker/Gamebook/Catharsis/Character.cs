﻿using System;

namespace Seeker.Gamebook.Catharsis
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();

        private int _life;
        public int MaxLife { get; set; }
        public int Life
        {
            get => _life;
            set => _life = Game.Param.Setter(value, max: MaxLife);
        }

        public int Aura { get; set; }

        private int _fight;
        public int Fight
        {
            get => _fight;
            set => _fight = Game.Param.Setter(value);
        }

        private int _accuracy;
        public int Accuracy
        {
            get => _accuracy;
            set => _accuracy = Game.Param.Setter(value);
        }

        private int _stealth;
        public int Stealth
        {
            get => _stealth;
            set => _stealth = Game.Param.Setter(value);
        }

        public int Bonuses { get; set; }

        public override void Init()
        {
            Name = String.Empty;
            MaxLife = 100;
            Life = MaxLife;
            Aura = 1;
            Fight = 10;
            Accuracy = 10;
            Stealth = 3;
            Bonuses = 5;

            Game.Healing.Add(name: "Использовать аптечку", healing: 10, portions: 5);
        }

        public Character Clone() => new Character()
        {
            Name = this.Name,
            MaxLife = this.MaxLife,
            Life = this.Life,
            Aura = this.Aura,
            Fight = this.Fight,
            Accuracy = this.Accuracy,
            Stealth = this.Stealth,
            Bonuses = this.Bonuses,
        };

        public override string Save() => String.Join("|",
            MaxLife,
            Life,
            Aura,
            Fight,
            Accuracy,
            Stealth,
            Bonuses
        );

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            MaxLife = int.Parse(save[0]);
            Life = int.Parse(save[1]);
            Aura = int.Parse(save[2]);
            Fight = int.Parse(save[3]);
            Accuracy = int.Parse(save[4]);
            Stealth = int.Parse(save[5]);
            Bonuses = int.Parse(save[6]);
        }
    }
}
