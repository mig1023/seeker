using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.PrairieLaw
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
        public static Character GetInstance() => Protagonist;

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

        private int _charm;
        public int Charm
        {
            get => _charm;
            set
            {
                if (value < 2)
                    _charm = 2;
                else
                    _charm = value;
            }
        }

        private int _cents;
        public int Cents
        {
            get => _cents;
            set => _cents = Game.Param.Setter(value, _cents, this);
        }

        private int _nuggets;
        public int Nuggets
        {
            get => _nuggets;
            set => _nuggets = Game.Param.Setter(value);
        }

        private int _cartridges;
        public int Cartridges
        {
            get => _cartridges;
            set => _cartridges = Game.Param.Setter(value, _cartridges, this);
        }

        public List<bool> Luck { get; set; }

        public List<string> AnimalSkins { get; set; }

        public override void Init()
        {
            base.Init();

            int dice = Game.Dice.Roll(dices: 2);

            MaxSkill = Constants.Skills[dice];
            Skill = MaxSkill;
            MaxStrength = Constants.Strengths[dice];
            Strength = MaxStrength;
            Charm = Constants.Charms[dice];

            Cents = 1500;
            Cartridges = 6;
            Nuggets = 0;

            AnimalSkins = new List<string>();

            Luck = new List<bool> { false, true, true, true, true, true, true };

            for (int i = 0; i < 2; i++)
                Luck[Game.Dice.Roll()] = false;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            MaxSkill = this.MaxSkill,
            Skill = this.Skill,
            MaxStrength = this.MaxStrength,
            Strength = this.Strength,
            Charm = this.Charm,
            Cents = this.Cents,
            Nuggets = this.Nuggets,
            Cartridges = this.Cartridges,
        };

        public override string Save() => String.Join("|",
            MaxSkill, Skill, MaxStrength, Strength, Charm, Cents, Cartridges,
            String.Join(",", Luck.Select(x => x ? "1" : "0")),
            String.Join(",", AnimalSkins).TrimEnd(','), Nuggets);

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            MaxSkill = int.Parse(save[0]);
            Skill = int.Parse(save[1]);
            MaxStrength = int.Parse(save[2]);
            Strength = int.Parse(save[3]);
            Charm = int.Parse(save[4]);
            Cents = int.Parse(save[5]);
            Cartridges = int.Parse(save[6]);

            Luck = save[7].Split(',').Select(x => x == "1").ToList();
            AnimalSkins = save[8].Split(',').ToList();

            Cartridges = int.Parse(save[9]);
            IsProtagonist = true;
        }
    }
}
