﻿using System;

namespace Seeker.Gamebook.WalkInThePark
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist { get; set; }
        public override void Set(object character) =>
            Protagonist = (Character)character;

        private int _strength;
        public int Strength
        {
            get => _strength;
            set => _strength = Game.Param.Setter(value, _strength, this);
        }

        public int StartStrength { get; set; }

        private int _endurance;
        public int Endurance
        {
            get => _endurance;
            set => _endurance = Game.Param.Setter(value, _endurance, this);
        }

        private int _luck;
        public int Luck
        {
            get => _luck;
            set => _luck = Game.Param.Setter(value, _luck, this);
        }

        private int _money;
        public int Money
        {
            get => _money;
            set => _money = Game.Param.Setter(value, _money, this);
        }

        private int _damage;
        public int Damage
        {
            get => _damage;
            set => _damage = Game.Param.Setter(value, _damage, this);
        }

        public string Weapon { get; set; }

        private int _health;
        public int Health
        {
            get => _health;
            set => _health = Game.Param.Setter(value, _health, this);
        }

        private int _stealth;
        public int Stealth
        {
            get => _stealth;
            set => _stealth = Game.Param.Setter(value, _stealth, this);
        }

        private int _fortune;
        public int Fortune
        {
            get => _fortune;
            set => _fortune = Game.Param.Setter(value, _fortune, this);
        }

        public int GetHealth(bool part1) =>
            part1 ? Endurance : Health;

        public void SetHealth(bool part1, int value)
        {
            if (part1)
                Endurance = value;
            else
                Health = value;
        }

        public override void Init()
        {
            base.Init();

            Strength = Game.Dice.Roll();
            StartStrength = Strength;
            Endurance = 10 * Game.Dice.Roll();
            Luck = Game.Dice.Roll();
            Money = 0;
            Damage = 1;
            Weapon = "кулаки";

            Health = 6 + Game.Dice.Roll();
            Stealth = Game.Dice.Roll();
            Fortune = Game.Dice.Roll(dices: 2);

        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            Strength = this.Strength,
            StartStrength = this.StartStrength,
            Endurance = this.Endurance,
            Luck = this.Luck,
            Money = this.Money,
            Damage = this.Damage,
            Weapon = this.Weapon,
            Health = this.Health,
            Stealth = this.Stealth,
            Fortune = this.Fortune,
        };

        public override string Save() => String.Join("|",
            Strength, StartStrength, Endurance, Luck, Money,
            Damage, Weapon, Health, Stealth, Fortune);

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Strength = int.Parse(save[0]);
            StartStrength = int.Parse(save[1]);
            Endurance = int.Parse(save[2]);
            Luck = int.Parse(save[3]);
            Money = int.Parse(save[4]);
            Damage = int.Parse(save[5]);
            Weapon = save[6];
            Health = int.Parse(save[7]);
            Stealth = int.Parse(save[8]);
            Fortune = int.Parse(save[9]);

            IsProtagonist = true;
        }
    }
}
