using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.LegendsAlwaysLie
{
    class Character : Interfaces.ICharacter
    {
        public static Character Protagonist = new Gamebook.LegendsAlwaysLie.Character();

        public enum SpecializationType { Warrior, Wizard, Thrower, Nope };

        public string Name { get; set; }

        public int Strength { get; set; }
        public int Hitpoints { get; set; }
        public int Magicpoints { get; set; }
        public int Spellpoints { get; set; }
        public int Gold { get; set; }
        public int Footwraps { get; set; }

        public int ConneryHitpoints { get; set; }
        public int ConneryTrust { get; set; }

        public SpecializationType Specialization { get; set; }

        public List<string> Spells { get; set; }

        public bool FoodIsDivided { get; set; }


        public void Init()
        {
            Name = String.Empty;
            Strength = 12;
            Hitpoints = 30;
            Magicpoints = 1;
            Spellpoints = 2;
            FoodIsDivided = false;
            Gold = 20;
            Footwraps = 0;
            ConneryHitpoints = 30;
            ConneryTrust = 5;
            Specialization = SpecializationType.Nope;
            Spells = new List<string>();
        }

        public Character Clone()
        {
            return new Character() {
                Name = this.Name,
                Strength = this.Strength,
                Hitpoints = this.Hitpoints,
                Magicpoints = this.Magicpoints,
                Spellpoints = this.Spellpoints,
                FoodIsDivided = this.FoodIsDivided,
                Gold = this.Gold,
                Footwraps = this.Footwraps,
                ConneryHitpoints = this.ConneryHitpoints,
                ConneryTrust = this.ConneryTrust,
                Specialization = this.Specialization,
                Spells = new List<string>(),
            };
        }
    }
}
