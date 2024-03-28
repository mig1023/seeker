using System;

namespace Seeker.Gamebook.Insight
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist { get; set; }
        public override void Set(object character) =>
            Protagonist = (Character)character;

        private int _life;
        public int Life
        {
            get => _life;
            set => _life = Game.Param.Setter(value, max: 22, _life, this);
        }

        public int Aura { get; set; }

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

        private int _memory;
        public int Memory
        {
            get => _memory;
            set => _memory = Game.Param.Setter(value, max: 8, _memory, this);
        }

        private int _time;
        public int Time
        {
            get => _time;
            set => _time = Game.Param.Setter(value, max: 30, _time, this);
        }

        public int Bonuses { get; set; }

        public override void Init()
        {
            base.Init();

            Life = 16;
            Aura = 2;
            Skill = 10;
            Weapon = 3;
            Memory = 0;
            Time = 0;
            Bonuses = 4;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            Life = this.Life,
            Aura = this.Aura,
            Skill = this.Skill,
            Weapon = this.Weapon,
            Memory = this.Memory,
            Time = this.Time,
            Bonuses = this.Bonuses,
        };

        public override string Save() => String.Join("|",
            Life, Aura, Skill, Weapon, Memory, Time, Bonuses);

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Life = int.Parse(save[0]);
            Aura = int.Parse(save[1]);
            Skill = int.Parse(save[2]);
            Weapon = int.Parse(save[3]);
            Memory = int.Parse(save[4]);
            Time = int.Parse(save[5]);
            Bonuses = int.Parse(save[6]);

            IsProtagonist = true;
        }
    }
}
