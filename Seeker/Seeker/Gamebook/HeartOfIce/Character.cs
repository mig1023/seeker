using System;
using System.Collections.Generic;
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

        public void Init()
        {
            Name = String.Empty;

            MaxLife = 10;
            Life = MaxLife;
            Money = 30;
        }

        public Character Clone() => new Character()
        {
            Name = this.Name,
            MaxLife = this.MaxLife,
            Life = this.Life,
            Money = this.Money,
        };

        public string Save() => String.Join("|", MaxLife, Life, Money);

        public void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            MaxLife = int.Parse(save[0]);
            Life = int.Parse(save[1]);
            Money = int.Parse(save[2]);
        }
    }
}
