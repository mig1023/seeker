using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.AdventuresOfABeardlessDeceiver
{
    class Character : Interfaces.ICharacter
    {
        public static Character Protagonist = new Gamebook.AdventuresOfABeardlessDeceiver.Character();

        public string Name { get; set; }

        public int Strength { get; set; }
        public int Skill { get; set; }
        public int Wisdom { get; set; }
        public int Cunning { get; set; }
        public int Oratory { get; set; }
        public int Popularity { get; set; }
        public int Tanga { get; set; }

        public int StatBonuses { get; set; }

        public void Init()
        {
            Strength = 1;
            Skill = 1;
            Wisdom = 1;
            Cunning = 1;
            Oratory = 1;
            Popularity = 3;
            Tanga = 15;
            StatBonuses = 4;
        }

        public Character Clone()
        {
            Character newCharacter = new Character();

            newCharacter.Strength = this.Strength;
            newCharacter.Skill = this.Skill;
            newCharacter.Wisdom = this.Wisdom;
            newCharacter.Cunning = this.Cunning;
            newCharacter.Oratory = this.Oratory;
            newCharacter.Popularity = this.Popularity;
            newCharacter.Tanga = this.Tanga;
            newCharacter.StatBonuses = this.StatBonuses;

            return newCharacter;
        }
    }
}
