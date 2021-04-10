using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seeker.Gamebook.CaptainSheltonsSecret
{
    class Character : Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.CaptainSheltonsSecret.Character();

        public string Name { get; set; }

        private int _mastery;
        public int MaxMastery { get; set; }
        public int Mastery
        {
            get => _mastery;
            set
            {
                if (value > MaxMastery)
                    _mastery = MaxMastery;
                else if (value < 0)
                    _mastery = 0;
                else
                    _mastery = value;
            }
        }

        private int _endurance;
        public int MaxEndurance { get; set; }
        public int Endurance
        {
            get => _endurance;
            set
            {
                if (value > MaxEndurance)
                    _endurance = MaxEndurance;
                else if (value < 0)
                    _endurance = 0;
                else
                    _endurance = value;
            }
        }

        public int Gold { get; set; }
        public int ExtendedDamage { get; set; }
        public int MasteryDamage { get; set; }
        public bool SeaArmour { get; set; }
        public Dictionary<int, bool> Luck { get; set; }

        private static Dictionary<string, int> EnduranceLoss = new Dictionary<string, int>();

        public void Init()
        {
            int dice = Game.Dice.Roll(dices: 2);

            Name = "Главный герой";
            MaxMastery = Constants.Masterys()[dice];
            Mastery = MaxMastery;
            MaxEndurance = Constants.Endurances()[dice];
            Endurance = MaxEndurance;
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

        public Character Clone() => new Character()
        {
            Name = this.Name,
            MaxMastery = this.MaxMastery,
            Mastery = this.Mastery,
            MaxEndurance = this.MaxEndurance,
            Endurance = this.Endurance,
            Gold = this.Gold,
            ExtendedDamage = this.ExtendedDamage,
            MasteryDamage = this.MasteryDamage,
            SeaArmour = this.SeaArmour,
        };

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

        public int GetEndurance() => (EnduranceLoss.ContainsKey(this.Name) ? EnduranceLoss[this.Name] : this.Endurance);

        public string Save()
        {
            List<string> lucks = new List<string>();

            foreach(bool luck in Luck.Values.ToList())
                lucks.Add(luck ? "1" : "0");

            return String.Join("|", MaxMastery, Mastery, MaxEndurance, Endurance, Gold, ExtendedDamage, MasteryDamage,
                (SeaArmour ? 1 : 0), String.Join(",", lucks));
        }

        public void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            MaxMastery = int.Parse(save[0]);
            Mastery = int.Parse(save[1]);
            MaxEndurance = int.Parse(save[2]);
            Endurance = int.Parse(save[3]);
            Gold = int.Parse(save[4]);
            ExtendedDamage = int.Parse(save[5]);
            MasteryDamage = int.Parse(save[6]);
            SeaArmour = (save[7] == "1");

            string[] lucks = save[8].Split(',');

            for (int i = 0; i < 6; i++)
                Luck[i+1] = (lucks[i] == "1");
        }
    }
}
