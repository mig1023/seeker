using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seeker.Gamebook.StringOfWorlds
{
    class Character : Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.StringOfWorlds.Character();

        public string Name { get; set; }

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

        public int Blaster { get; set; }
        public int GateCode { get; set; }
        public string Equipment { get; set; }
        public Dictionary<int, bool> Luck { get; set; }

        public void Init()
        {
            int dice = Game.Dice.Roll(dices: 2);

            MaxSkill = Constants.Skills()[dice];
            Skill = MaxSkill;
            MaxStrength = Constants.Strengths()[dice];
            Strength = MaxStrength;
            Charm = Constants.Charms()[dice];

            Blaster = 1;
            GateCode = 0;
            Equipment = String.Empty;

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
            Blaster = this.Blaster,
            GateCode = this.GateCode,
            Equipment = this.Equipment,
        };

        public string Save()
        {
            List<string> lucks = new List<string>();

            foreach (bool luck in Luck.Values.ToList())
                lucks.Add(luck ? "1" : "0");

            return String.Format("|", MaxSkill, Skill, MaxStrength, Strength, Charm,
                Blaster, GateCode, Equipment, String.Join(",", lucks));
        }

        public void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            MaxSkill = int.Parse(save[0]);
            Skill = int.Parse(save[1]);
            MaxStrength = int.Parse(save[2]);
            Strength = int.Parse(save[3]);
            Charm = int.Parse(save[4]);
            Blaster = int.Parse(save[5]);
            GateCode = int.Parse(save[6]);
            Equipment = save[7];

            string[] lucks = save[8].Split(',');

            for (int i = 0; i < 6; i++)
                Luck[i + 1] = (lucks[i] == "1");
        }
    }
}
