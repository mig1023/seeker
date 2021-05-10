using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.Catharsis
{
    class Character : Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.Catharsis.Character();

        public string Name { get; set; }

        private int _life;
        public int MaxLife { get; set; }
        public int Life
        {
            get => _life;
            set
            {
                if (value > MaxLife)
                    _life = MaxLife;
                else if (value < 0)
                    _life = 0;
                else
                    _life = value;
            }
        }

        public int Aura { get; set; }

        private int _fight;
        public int Fight
        {
            get => _fight;
            set
            {
                if (value < 0)
                    _fight = 0;
                else
                    _fight = value;
            }
        }

        private int _accuracy;
        public int Accuracy
        {
            get => _accuracy;
            set
            {
                if (value < 0)
                    _accuracy = 0;
                else
                    _accuracy = value;
            }
        }

        private int _stealth;
        public int Stealth
        {
            get => _stealth;
            set
            {
                if (value < 0)
                    _stealth = 0;
                else
                    _stealth = value;
            }
        }

        public int Bonuses { get; set; }

        public void Init()
        {
            Name = String.Empty;
            MaxLife = 100;
            Life = MaxLife;
            Aura = 1;
            Fight = 10;
            Accuracy = 10;
            Stealth = 3;
            Bonuses = 5;

            Game.Healing.Add(name: "Использовать аптечку", healing: 10, portions: 5);
        }

        public Character Clone() => new Character()
        {
            Name = this.Name,
            MaxLife = this.MaxLife,
            Life = this.Life,
            Aura = this.Aura,
            Fight = this.Fight,
            Accuracy = this.Accuracy,
            Stealth = this.Stealth,
            Bonuses = this.Bonuses,
        };

        public string Save() => String.Join("|", MaxLife, Life, Aura, Fight, Accuracy, Stealth, Bonuses);

        public void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            MaxLife = int.Parse(save[0]);
            Life = int.Parse(save[1]);
            Aura = int.Parse(save[2]);
            Fight = int.Parse(save[3]);
            Accuracy = int.Parse(save[4]);
            Stealth = int.Parse(save[5]);
            Bonuses = int.Parse(save[6]);
        }
    }
}
