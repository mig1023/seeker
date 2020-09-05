using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.CaptainSheldonsSecret
{
    class Character : Interfaces.ICharacter
    {
        public static Character Protagonist = new Gamebook.CaptainSheldonsSecret.Character();

        public string Name { get; set; }

        public int Skill { get; set; }
        public int Strength { get; set; }
        public int Charm { get; set; }
        public int Gold { get; set; }
        public Dictionary<int, bool> Luck { get; set; }

        public void Init()
        {
            Dictionary<int, int> Skills = new Dictionary<int, int>
            {
                [2] = 8,
                [3] = 10,
                [4] = 12,
                [5] = 9,
                [6] = 11,
                [7] = 9,
                [8] = 10,
                [9] = 8,
                [10] = 9,
                [11] = 10,
                [12] = 11
            };

            Dictionary<int, int> Strengths = new Dictionary<int, int>
            {
                [2] = 22,
                [3] = 20,
                [4] = 16,
                [5] = 18,
                [6] = 20,
                [7] = 20,
                [8] = 16,
                [9] = 24,
                [10] = 22,
                [11] = 18,
                [12] = 20
            };

            Dictionary<int, int> Charms = new Dictionary<int, int>
            {
                [2] = 8,
                [3] = 6,
                [4] = 5,
                [5] = 8,
                [6] = 6,
                [7] = 7,
                [8] = 7,
                [9] = 7,
                [10] = 6,
                [11] = 7,
                [12] = 5
            };

            Name = "ГЛАВГЕРОЙ";
            Skill = Skills[Game.Dice.Roll(dices: 2)];
            Strength = Strengths[Game.Dice.Roll(dices: 2)];
            Charm = Charms[Game.Dice.Roll(dices: 2)];
            Gold = 15;

            Luck = new Dictionary<int, bool>
            {
                [1] = true,
                [2] = true,
                [3] = true,
                [4] = true,
                [5] = true,
                [6] = true,
            };

            for (int i = 0; i < 2; i++)
                Luck[Game.Dice.Roll()] = false;
        }

        public Character Clone()
        {
            Character newCharacter = new Character();

            newCharacter.Name = this.Name;
            newCharacter.Skill = this.Skill;
            newCharacter.Strength = this.Strength;
            newCharacter.Charm = this.Charm;
            newCharacter.Gold = this.Gold;

            return newCharacter;
        }
    }
}
