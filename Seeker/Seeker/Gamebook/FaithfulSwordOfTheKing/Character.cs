using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.FaithfulSwordOfTheKing
{
    class Character : Interfaces.ICharacter
    {
        public static Character Protagonist = new Gamebook.FaithfulSwordOfTheKing.Character();

        public enum MeritalArts { SecretBlow, SwordAndDagger, TwoPistols, LefthandFencing, Swimming, Nope };

        public string Name { get; set; }

        public int Skill { get; set; }
        public int Strength { get; set; }
        public int Honor { get; set; }
        public int Day { get; set; }
        public int Ecu { get; set; }
        public MeritalArts MeritalArt { get; set; }
        public int Horses { get; set; }
        public int Pistols { get; set; }
        public int BulletsAndGubpowder { get; set; }
        public int Daggers { get; set; }
        public int Food { get; set; }
        public int HadFoodToday { get; set; }
        public int Chainmail { get; set; }
        public int LeftHandPenalty { get; set; }

        public void Init()
        {
            Dictionary<int, int> Skills = new Dictionary<int, int>
            {
                [1] = 12,
                [2] = 8,
                [3] = 10,
                [4] = 7,
                [5] = 9,
                [6] = 11
            };

            Dictionary<int, int> Strengths = new Dictionary<int, int>
            {
                [1] = 22,
                [2] = 18,
                [3] = 14,
                [4] = 24,
                [5] = 16,
                [6] = 20
            };

            Skill = Skills[Game.Dice.Roll()];
            Strength = Strengths[Game.Dice.Roll()];
            Honor = 3;
            Day = 1;
            Ecu = 1500;
            MeritalArt = MeritalArts.Nope;
            Horses = 1;
            Pistols = 1;
            BulletsAndGubpowder = 5;
            Daggers = 0;
            Food = 2;
            HadFoodToday = 0;
            Chainmail = 0;
        }

        public Character Clone()
        {
            Character newCharacter = new Character();

            newCharacter.Name = this.Name;
            newCharacter.Skill = this.Skill;
            newCharacter.Strength = this.Strength;
            newCharacter.Honor = this.Honor;
            newCharacter.Day = this.Day;
            newCharacter.Ecu = this.Ecu;
            newCharacter.LeftHandPenalty = this.LeftHandPenalty;

            return newCharacter;
        }
    }
}
