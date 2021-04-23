using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seeker.Gamebook.HeartOfIce
{
    class Character : Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.HeartOfIce.Character();

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

        public List<string> Skills { get; set; }
        public int SkillsValue { get; set; }
        public bool Chosen { get; set; }

        public void Init()
        {
            Name = String.Empty;

            MaxLife = 10;
            Life = MaxLife;
            Money = 30;
            Chosen = false;
            SkillsValue = 4;

            Skills = new List<string>();
        }

        public Character Clone() => new Character()
        {
            Name = this.Name,
            MaxLife = this.MaxLife,
            Life = this.Life,
            Money = this.Money,
            SkillsValue = this.SkillsValue,
        };

        public string Save() => String.Join("|", MaxLife, Life, Money, SkillsValue, (Chosen ? 1 : 0), String.Join(":", Skills));

        public void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            MaxLife = int.Parse(save[0]);
            Life = int.Parse(save[1]);
            Money = int.Parse(save[2]);
            SkillsValue = int.Parse(save[3]);
            Chosen = (int.Parse(save[4]) == 1);

            Skills = save[5].Split(':').ToList();
        }
    }
}
