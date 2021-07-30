using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.HeartOfIce
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();

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

        private int _money;
        public int Money
        {
            get => _money;
            set
            {
                if (value < 0)
                    _money = 0;
                else
                    _money = value;
            }
        }

        private int _food;
        public int Food
        {
            get => _food;
            set
            {
                if (value < 0)
                    _food = 0;
                else
                    _food = value;
            }
        }

        private int _shots;
        public int Shots
        {
            get => _shots;
            set
            {
                if (value < 0)
                    _shots = 0;
                else
                    _shots = value;
            }
        }

        public List<string> Skills { get; set; }
        public int SkillsValue { get; set; }
        public bool Chosen { get; set; }
        public int Split { get; set; }

        public override void Init()
        {
            Name = String.Empty;

            MaxLife = 10;
            Life = MaxLife;
            Money = 30;
            Food = 0;
            Shots = 0;
            Chosen = false;
            SkillsValue = 4;
            Split = 0;

            Skills = new List<string>();
        }

        public Character Clone() => new Character()
        {
            Name = this.Name,
            MaxLife = this.MaxLife,
            Life = this.Life,
            Money = this.Money,
            Food = this.Food,
            Shots = this.Shots,
            SkillsValue = this.SkillsValue,
            Split = this.Split,
        };

        public override string Save() => String.Join("|",
            MaxLife,
            Life,
            Money,
            Food,
            Shots,
            SkillsValue,
            Split,
            (Chosen ? 1 : 0),
            String.Join(":", Skills).TrimEnd(':')
        );

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            MaxLife = int.Parse(save[0]);
            Life = int.Parse(save[1]);
            Money = int.Parse(save[2]);
            Food = int.Parse(save[3]);
            Shots = int.Parse(save[4]);
            SkillsValue = int.Parse(save[5]);
            Split = int.Parse(save[6]);
            Chosen = (int.Parse(save[7]) == 1);

            Skills = save[8].Split(':').ToList();
        }
    }
}
