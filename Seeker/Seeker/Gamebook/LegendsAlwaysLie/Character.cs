﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seeker.Gamebook.LegendsAlwaysLie
{
    class Character : Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.LegendsAlwaysLie.Character();

        public enum SpecializationType { Warrior, Wizard, Thrower, Nope };

        public string Name { get; set; }

        public int Strength { get; set; }
        public int Hitpoints { get; set; }
        public int Magicpoints { get; set; }
        public int Gold { get; set; }
        public int Footwraps { get; set; }
        public int TimeForReading { get; set; }

        public int ConneryHitpoints { get; set; }
        public int ConneryTrust { get; set; }

        public SpecializationType Specialization { get; set; }

        public bool FoodIsDivided { get; set; }
        public int PoisonBlade { get; set; }


        public void Init()
        {
            Name = String.Empty;
            Strength = 12;
            Hitpoints = 30;
            Magicpoints = 0;
            FoodIsDivided = false;
            PoisonBlade = 0;
            Gold = 20;
            Footwraps = 0;
            TimeForReading = 60;
            ConneryHitpoints = 30;
            ConneryTrust = 5;
            Specialization = SpecializationType.Nope;
        }

        public Character Clone()
        {
            return new Character()
            {
                Name = this.Name,
                Strength = this.Strength,
                Hitpoints = this.Hitpoints,
                Magicpoints = this.Magicpoints,
                FoodIsDivided = this.FoodIsDivided,
                PoisonBlade = this.PoisonBlade,
                Gold = this.Gold,
                Footwraps = this.Footwraps,
                TimeForReading = this.TimeForReading,
                ConneryHitpoints = this.ConneryHitpoints,
                ConneryTrust = this.ConneryTrust,
                Specialization = this.Specialization,
            };
        }

        public string Save()
        {
            return String.Format(
                "{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}",
                Strength, Hitpoints, Magicpoints, (FoodIsDivided ? 1 : 0),
                PoisonBlade, Gold, Footwraps, TimeForReading, ConneryHitpoints,
                ConneryTrust, Specialization
            );
        }

        public void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Strength = int.Parse(save[0]);
            Hitpoints = int.Parse(save[1]);
            Magicpoints = int.Parse(save[2]);
            FoodIsDivided = (save[3] == "1" ? true : false);
            PoisonBlade = int.Parse(save[4]);
            Gold = int.Parse(save[5]);
            Footwraps = int.Parse(save[6]);
            TimeForReading = int.Parse(save[7]);
            ConneryHitpoints = int.Parse(save[8]);
            ConneryTrust = int.Parse(save[9]);

            bool success = Enum.TryParse(save[10], out SpecializationType value);
            Specialization = (success ? value : SpecializationType.Nope);
        }
    }
}
