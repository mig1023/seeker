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
        public SpecializationType Specialization { get; set; }
        public List<string> Spells { get; set; }

        public void Init()
        {
            Name = String.Empty;
            Strength = 12;
            Hitpoints = 13;
            Magicpoints = 1;
            Spellpoints = 2;
            Gold = 20;
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
                Gold = this.Gold,
                Specialization = this.Specialization,
                Spells = new List<string>(),
            };
        }
    }
}
