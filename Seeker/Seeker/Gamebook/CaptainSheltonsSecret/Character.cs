using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.CaptainSheltonsSecret
{
    class Character : Interfaces.ICharacter
    {
        public static Character Protagonist = new Gamebook.CaptainSheltonsSecret.Character();

        public string Name { get; set; }

        public int Mastery { get; set; }
        public int Endurance { get; set; }
        public int Gold { get; set; }
        public int ExtendedDamage { get; set; }
        public int MasteryDamage { get; set; }
        public bool SeaArmour { get; set; }
        public Dictionary<int, bool> Luck { get; set; }

        private static Dictionary<string, int> EnduranceLoss = new Dictionary<string, int>();

        public void Init()
        {
            Dictionary<int, int> Masterys = new Dictionary<int, int>
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

            Dictionary<int, int> Endurances = new Dictionary<int, int>
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

            int dice = Game.Dice.Roll(dices: 2);

            Name = "ГЛАВГЕРОЙ";
            Mastery = Masterys[dice];
            Endurance = Endurances[dice];
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
            newCharacter.Mastery = this.Mastery;
            newCharacter.Endurance = this.Endurance;
            newCharacter.Gold = this.Gold;
            newCharacter.ExtendedDamage = this.ExtendedDamage;
            newCharacter.MasteryDamage = this.MasteryDamage;
            newCharacter.SeaArmour = this.SeaArmour;

            return newCharacter;
        }

        public Character SetEndurance()
        {
            if (EnduranceLoss.ContainsKey(this.Name))
                this.Endurance = EnduranceLoss[this.Name];

            return this;
        }

        public void SaveEndurance()
        {
            EnduranceLoss[this.Name] = this.Endurance;
        }

        public int GetEndurance()
        {
            return (EnduranceLoss.ContainsKey(this.Name) ? EnduranceLoss[this.Name] : this.Endurance);
        }
    }
}
