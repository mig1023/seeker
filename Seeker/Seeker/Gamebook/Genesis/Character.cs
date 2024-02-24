using System;

namespace Seeker.Gamebook.Genesis
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist { get; set; }
        public override void Set(object character) =>
            Protagonist = (Character)character;

        private int _life;
        public int MaxLife { get; set; }
        public int Life
        {
            get => _life;
            set => _life = Game.Param.Setter(value, max: MaxLife, _life, this);
        }

        private int _aura;
        public int Aura
        {
            get => _aura;
            set => _aura = Game.Param.Setter(value, _aura, this);
        }

        private int _skill;
        public int Skill
        {
            get => _skill;
            set => _skill = Game.Param.Setter(value, _skill, this);
        }

        private int _weapon;
        public int Weapon
        {
            get => _weapon;
            set => _weapon = Game.Param.Setter(value, _weapon, this);
        }

        private int _stealth;
        public int Stealth
        {
            get => _stealth;
            set => _stealth = Game.Param.Setter(value, _stealth, this);
        }

        public int Bonuses { get; set; }

        public override void Init()
        {
            base.Init();

            MaxLife = 40;
            Life = MaxLife;
            Aura = 5;
            Skill = 30;
            Weapon = 15;
            Stealth = 3;
            Bonuses = 5;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            MaxLife = this.MaxLife,
            Life = this.Life,
            Aura = this.Aura,
            Skill = this.Skill,
            Weapon = this.Weapon,
            Stealth = this.Stealth,
            Bonuses = this.Bonuses,
        };

        public override string Save() => String.Join("|",
            MaxLife, Life, Aura, Skill, Weapon, Stealth, Bonuses);

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            MaxLife = int.Parse(save[0]);
            Life = int.Parse(save[1]);
            Aura = int.Parse(save[2]);
            Skill = int.Parse(save[3]);
            Weapon = int.Parse(save[4]);
            Stealth = int.Parse(save[5]);
            Bonuses = int.Parse(save[6]);

            IsProtagonist = true;
        }
    }
}
