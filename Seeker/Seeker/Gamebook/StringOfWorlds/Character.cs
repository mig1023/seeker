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

        public int Skill { get; set; }
        public int Strength { get; set; }
        public int Charm { get; set; }
        public int Blaster { get; set; }
        public int GateCode { get; set; }
        public string Equipment { get; set; }
        public int StrengthAtStart { get; set; }
        public Dictionary<int, bool> Luck { get; set; }

        public void Init()
        {
            Dictionary<int, int> Skills = new Dictionary<int, int>
            {
                [2] = 8,
                [3] = 10,
                [4] = 12,
                [5] = 9,
                [6] = 11,
                [7] = 9,
                [8] = 10,
                [9] = 8,
                [10] = 9,
                [11] = 10,
                [12] = 11
            };

            Dictionary<int, int> Strengths = new Dictionary<int, int>
            {
                [2] = 22,
                [3] = 20,
                [4] = 16,
                [5] = 18,
                [6] = 20,
                [7] = 20,
                [8] = 16,
                [9] = 24,
                [10] = 22,
                [11] = 18,
                [12] = 20
            };

            Dictionary<int, int> Charms = new Dictionary<int, int>
            {
                [2] = 8,
                [3] = 6,
                [4] = 5,
                [5] = 8,
                [6] = 6,
                [7] = 7,
                [8] = 7,
                [9] = 7,
                [10] = 6,
                [11] = 7,
                [12] = 5
            };

            int dice = Game.Dice.Roll(dices: 2);

            Skill = Skills[dice];
            Strength = Strengths[dice];
            Charm = Charms[dice];
            StrengthAtStart = Strength;

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

        public Character Clone()
        {
            return new Character()
            {
                Name = this.Name,
                Skill = this.Skill,
                Strength = this.Strength,
                Charm = this.Charm,
                Blaster = this.Blaster,
                GateCode = this.GateCode,
                Equipment = this.Equipment,
                StrengthAtStart = this.StrengthAtStart,
            };
        }

        public string Save()
        {
            List<string> lucks = new List<string>();

            foreach (bool luck in Luck.Values.ToList())
                lucks.Add(luck ? "1" : "0");

            return String.Format(
                "{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}",
                Skill, Strength, Charm, Blaster, GateCode, Equipment, StrengthAtStart, String.Join(",", lucks)
            );
        }

        public void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Skill = int.Parse(save[0]);
            Strength = int.Parse(save[1]);
            Charm = int.Parse(save[2]);
            Blaster = int.Parse(save[3]);
            GateCode = int.Parse(save[4]);
            Equipment = save[5];
            StrengthAtStart = int.Parse(save[6]);

            string[] lucks = save[7].Split(',');

            for (int i = 0; i < 6; i++)
                Luck[i + 1] = (lucks[i] == "1" ? true : false);
        }
    }
}
