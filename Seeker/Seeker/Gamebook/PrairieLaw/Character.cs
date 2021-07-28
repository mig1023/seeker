using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seeker.Gamebook.PrairieLaw
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.PrairieLaw.Character();

        private int _skill;
        public int MaxSkill { get; set; }
        public int Skill
        {
            get => _skill;
            set
            {
                if (value > MaxSkill)
                    _skill = MaxSkill;
                else if (value < 0)
                    _skill = 0;
                else
                    _skill = value;
            }
        }

        private int _strength;
        public int MaxStrength { get; set; }
        public int Strength
        {
            get => _strength;
            set
            {
                if (value > MaxStrength)
                    _strength = MaxStrength;
                else if (value < 0)
                    _strength = 0;
                else
                    _strength = value;
            }
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
            set
            {
                if (value < 0)
                    _cents = 0;
                else
                    _cents = value;
            }
        }
        
        private int _nuggets;
        public int Nuggets
        {
            get => _nuggets;
            set
            {
                if (value < 0)
                    _nuggets = 0;
                else
                    _nuggets = value;
            }
        }
        
        private int _cartridges;
        public int Cartridges
        {
            get => _cartridges;
            set
            {
                if (value < 0)
                    _cartridges = 0;
                else
                    _cartridges = value;
            }
        }

        public Dictionary<int, bool> Luck { get; set; }

        public List<string> AnimalSkins { get; set; }

        public override void Init()
        {
            int dice = Game.Dice.Roll(dices: 2);

            MaxSkill = Constants.Skills()[dice];
            Skill = MaxSkill;
            MaxStrength = Constants.Strengths()[dice];
            Strength = MaxStrength;
            Charm = Constants.Charms()[dice];

            Cents = 1500;
            Cartridges = 6;
            Nuggets = 0;

            AnimalSkins = new List<string>();

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
            MaxSkill = this.MaxSkill,
            Skill = this.Skill,
            MaxStrength = this.MaxStrength,
            Strength = this.Strength,
            Charm = this.Charm,
            Cents = this.Cents,
            Nuggets = this.Nuggets,
            Cartridges = this.Cartridges,
        };

        public override string Save()
        {
            List<string> lucks = new List<string>();

            foreach (bool luck in Luck.Values.ToList())
                lucks.Add(luck ? "1" : "0");

            return String.Join("|", MaxSkill, Skill, MaxStrength, Strength, Charm, Cents, Cartridges,
                String.Join(",", lucks), String.Join(",", AnimalSkins).TrimEnd(','), Nuggets);
        }

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

            string[] lucks = save[7].Split(',');

            for (int i = 0; i < 6; i++)
                Luck[i + 1] = (lucks[i] == "1");

            AnimalSkins = save[8].Split(',').ToList();
            Cartridges = int.Parse(save[9]);
        }
    }
}
