using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.DangerFromBehindTheSnowWall
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public new static Character Protagonist = new Character();
        public new static Character GetInstance() => Protagonist;

        private int _skill;
        public int MaxSkill { get; set; }
        public int Skill
        {
            get => _skill;
            set => _skill = Game.Param.Setter(value, max: MaxSkill, _skill, this);
        }

        private int _strength;
        public int MaxStrength { get; set; }
        public int Strength
        {
            get => _strength;
            set => _strength = Game.Param.Setter(value, max: MaxStrength, _strength, this);
        }

        public int Damage { get; set; }

        private int _money;
        public int Money
        {
            get => _money;
            set => _money = Game.Param.Setter(value, _money, this);
        }

        private int _observation;
        public int Observation
        {
            get => _observation;
            set => _observation = Game.Param.Setter(value, _observation, this);
        }

        private int _magic;
        public int Magic
        {
            get => _magic;
            set => _magic = Game.Param.Setter(value, _magic, this);
        }

        public List<bool> Luck { get; set; }

        public override void Init()
        {
            base.Init();

            int dice = Game.Dice.Roll();

            MaxSkill = Constants.Skills[dice];
            Skill = MaxSkill;
            MaxStrength = Constants.Strengths[dice];
            Strength = MaxStrength;
            Damage = Constants.Damage[dice];
            Observation = Constants.Observation[dice];
            Money = Constants.Money[dice];

            Magic = 100;

            Luck = new List<bool> { false, false, false, false, false, false, false };

            for (int i = 0; i < 4; i++)
                Luck[Game.Dice.Roll()] = true;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            MaxSkill = this.MaxSkill,
            Skill = this.Skill,
            MaxStrength = this.MaxStrength,
            Strength = this.Strength,
            Damage = this.Damage,
            Observation = this.Observation,
            Money = this.Money,
            Magic = this.Magic,
        };

        public override string Save() => String.Join("|",
            MaxSkill, Skill, MaxStrength, Strength, Damage, Observation, Money, Magic,
            String.Join(",", Luck.Select(x => x ? "1" : "0")));

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            MaxSkill = int.Parse(save[0]);
            Skill = int.Parse(save[1]);
            MaxStrength = int.Parse(save[2]);
            Strength = int.Parse(save[3]);
            Damage = int.Parse(save[4]);
            Observation = int.Parse(save[5]);
            Money = int.Parse(save[6]);
            Magic = int.Parse(save[7]);

            Luck = save[8].Split(',').Select(x => x == "1").ToList();

            IsProtagonist = true;
        }
    }
}
