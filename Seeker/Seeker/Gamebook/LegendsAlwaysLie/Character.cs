﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seeker.Gamebook.LegendsAlwaysLie
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.LegendsAlwaysLie.Character();

        public enum SpecializationType { Warrior, Wizard, Thrower, Nope };

        private int _strength;
        public int Strength
        {
            get => _strength;
            set
            {
                if (value < 0)
                    _strength = 0;
                else
                    _strength = value;
            }
        }

        private int _hitpoints;
        public int Hitpoints
        {
            get => _hitpoints;
            set
            {
                if (value < 0)
                    _hitpoints = 0;
                else if (value > 30)
                    _hitpoints = 30;
                else
                    _hitpoints = value;
            }
        }

        private int _magicpoints;
        public int Magicpoints
        {
            get => _magicpoints;
            set
            {
                if (value < 0)
                    _magicpoints = 0;
                else
                    _magicpoints = value;
            }
        }

        private int _gold;
        public int Gold
        {
            get => _gold;
            set
            {
                if (value < 0)
                    _gold = 0;
                else
                    _gold = value;
            }
        }

        public int Footwraps { get; set; }
        public int TimeForReading { get; set; }
        public int Elixir { get; set; }

        private int _conneryHitpoints;
        public int ConneryHitpoints
        {
            get => _conneryHitpoints;
            set
            {
                if (value < 0)
                    _conneryHitpoints = 0;
                else if (value > 30)
                    _conneryHitpoints = 30;
                else
                    _conneryHitpoints = value;
            }
        }
        private int _conneryTrust;
        public int ConneryTrust
        {
            get => _conneryTrust;
            set
            {
                if (value < 0)
                    _conneryTrust = 0;
                else
                    _conneryTrust = value;
            }
        }

        public SpecializationType Specialization { get; set; }

        public bool FoodIsDivided { get; set; }
        public int PoisonBlade { get; set; }
        public int HealingSpellLost { get; set; }


        public override void Init()
        {
            Name = String.Empty;
            Strength = 12;
            Hitpoints = 30;
            Magicpoints = 0;
            Gold = 20;
            Footwraps = 0;
            TimeForReading = 60;
            Elixir = 1;
            ConneryHitpoints = 30;
            ConneryTrust = 5;
            Specialization = SpecializationType.Nope;

            FoodIsDivided = false;
            PoisonBlade = 0;
            HealingSpellLost = 0;
        }

        public Character Clone() => new Character()
        {
            Name = this.Name,
            Strength = this.Strength,
            Hitpoints = this.Hitpoints,
            Magicpoints = this.Magicpoints,
            Gold = this.Gold,
            Footwraps = this.Footwraps,
            TimeForReading = this.TimeForReading,
            Elixir = this.Elixir,
            ConneryHitpoints = this.ConneryHitpoints,
            ConneryTrust = this.ConneryTrust,
            Specialization = this.Specialization,

            FoodIsDivided = this.FoodIsDivided,
            PoisonBlade = this.PoisonBlade,
            HealingSpellLost = this.HealingSpellLost,
        };

        public override string Save() => String.Join("|", Strength, Hitpoints, Magicpoints, (FoodIsDivided ? 1 : 0),
            PoisonBlade, Gold, Footwraps, TimeForReading, ConneryHitpoints, ConneryTrust, Specialization, Elixir, HealingSpellLost);

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Strength = int.Parse(save[0]);
            Hitpoints = int.Parse(save[1]);
            Magicpoints = int.Parse(save[2]);
            FoodIsDivided = (save[3] == "1");
            PoisonBlade = int.Parse(save[4]);
            Gold = int.Parse(save[5]);
            Footwraps = int.Parse(save[6]);
            TimeForReading = int.Parse(save[7]);
            ConneryHitpoints = int.Parse(save[8]);
            ConneryTrust = int.Parse(save[9]);
            Elixir = int.Parse(save[11]);
            HealingSpellLost = int.Parse(save[12]);

            bool success = Enum.TryParse(save[10], out SpecializationType value);
            Specialization = (success ? value : SpecializationType.Nope);
        }
    }
}
