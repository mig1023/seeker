using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.DzungarWar
{
    class Character : Interfaces.ICharacter
    {
        public static Character Protagonist = new Gamebook.DzungarWar.Character();

        public string Name { get; set; }

        public int Strength { get; set; }
        public int Skill { get; set; }
        public int Wisdom { get; set; }
        public int Cunning { get; set; }
        public int Oratory { get; set; }
        public int Danger { get; set; }
        public int Tanga { get; set; }

        public int StatBonuses { get; set; }
        public bool MaxBonus { get; set; }

        public void Init()
        {
            Strength = 1;
            Skill = 1;
            Wisdom = 1;
            Cunning = 1;
            Oratory = 1;
            Danger = 0;
            Tanga = 150;
            StatBonuses = 4;
            MaxBonus = false;
        }

        public Character Clone()
        {
            Character newCharacter = new Character();

            newCharacter.Strength = this.Strength;
            newCharacter.Skill = this.Skill;
            newCharacter.Wisdom = this.Wisdom;
            newCharacter.Cunning = this.Cunning;
            newCharacter.Oratory = this.Oratory;
            newCharacter.Danger = this.Danger;
            newCharacter.Tanga = this.Tanga;
            newCharacter.StatBonuses = this.StatBonuses;
            newCharacter.MaxBonus = this.MaxBonus;

            return newCharacter;
        }
    }
}
