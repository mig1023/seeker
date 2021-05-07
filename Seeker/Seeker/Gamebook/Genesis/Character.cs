using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.Genesis
{
    class Character : Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.Genesis.Character();

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

        private int _skill;
        public int Skill
        {
            get => _skill;
            set
            {
                if (value < 0)
                    _skill = 0;
                else
                    _skill = value;
            }
        }

        private int _steelArms;
        public int SteelArms
        {
            get => _steelArms;
            set
            {
                if (value < 0)
                    _steelArms = 0;
                else
                    _steelArms = value;
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

        public void Init()
        {
            Name = String.Empty;
            MaxLife = 40;
            Life = MaxLife;
            Aura = 5;
            Skill = 30;
            SteelArms = 15;
            Stealth = 3;
        }

        public Character Clone() => new Character()
        {
            Name = this.Name,
            MaxLife = this.MaxLife,
            Life = this.Life,
            Aura = this.Aura,
            Skill = this.Skill,
            SteelArms = this.SteelArms,
            Stealth = this.Stealth,
        };

        public string Save() => String.Join("|", MaxLife, Life, Aura, Skill, SteelArms, Stealth);

        public void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            MaxLife = int.Parse(save[0]);
            Life = int.Parse(save[1]);
            Aura = int.Parse(save[2]);
            Skill = int.Parse(save[3]);
            SteelArms = int.Parse(save[4]);
            Stealth = int.Parse(save[5]);
        }
    }
}
