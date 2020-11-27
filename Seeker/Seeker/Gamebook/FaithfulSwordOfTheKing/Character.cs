using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.FaithfulSwordOfTheKing
{
    class Character : Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.FaithfulSwordOfTheKing.Character();

        public enum MeritalArts { SecretBlow, SwordAndDagger, TwoPistols, LefthandFencing, Swimming, Nope };

        public string Name { get; set; }

        public int Skill { get; set; }
        public int Strength { get; set; }
        public int Honor { get; set; }
        public int Day { get; set; }
        public int Ecu { get; set; }
        public MeritalArts MeritalArt { get; set; }
        public int Horses { get; set; }
        public int Pistols { get; set; }
        public int BulletsAndGubpowder { get; set; }
        public int Daggers { get; set; }
        public int Food { get; set; }
        public int HadFoodToday { get; set; }
        public int Chainmail { get; set; }
        public int LeftHandPenalty { get; set; }

        public void Init()
        {
            Dictionary<int, int> Skills = new Dictionary<int, int>
            {
                [1] = 12,
                [2] = 8,
                [3] = 10,
                [4] = 7,
                [5] = 9,
                [6] = 11
            };

            Dictionary<int, int> Strengths = new Dictionary<int, int>
            {
                [1] = 22,
                [2] = 18,
                [3] = 14,
                [4] = 24,
                [5] = 16,
                [6] = 20
            };

            Skill = Skills[Game.Dice.Roll()];
            Strength = Strengths[Game.Dice.Roll()];
            Honor = 3;
            Day = 1;
            Ecu = 1500;
            MeritalArt = MeritalArts.Nope;
            Horses = 1;
            Pistols = 1;
            BulletsAndGubpowder = 5;
            Daggers = 0;
            Food = 2;
            HadFoodToday = 0;
            Chainmail = 0;
            LeftHandPenalty = 0;
        }

        public Character Clone()
        {
            return new Character()
            {
                Name = this.Name,
                Skill = this.Skill,
                Strength = this.Strength,
                Honor = this.Honor,
                Day = this.Day,
                Ecu = this.Ecu,
                MeritalArt = this.MeritalArt,
                Horses = this.Horses,
                Pistols = this.Pistols,
                BulletsAndGubpowder = this.BulletsAndGubpowder,
                Daggers = this.Daggers,
                Food = this.Food,
                HadFoodToday = this.HadFoodToday,
                Chainmail = this.Chainmail,
                LeftHandPenalty = this.LeftHandPenalty,

            };
        }

        public string Save()
        {
            return String.Format(
                "{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}",
                Skill, Strength, Honor, Day, Ecu, MeritalArt,
                Horses, Pistols, BulletsAndGubpowder, Daggers, Food,
                HadFoodToday, Chainmail, LeftHandPenalty
            );
        }

        public void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Skill = int.Parse(save[0]);
            Strength = int.Parse(save[1]);
            Honor = int.Parse(save[2]);
            Day = int.Parse(save[3]);
            Ecu = int.Parse(save[4]);
            Horses = int.Parse(save[6]);
            Pistols = int.Parse(save[7]);
            BulletsAndGubpowder = int.Parse(save[8]);
            Daggers = int.Parse(save[9]);
            Food = int.Parse(save[10]);
            HadFoodToday = int.Parse(save[11]);
            Chainmail = int.Parse(save[12]);
            LeftHandPenalty = int.Parse(save[13]);

            bool success = Enum.TryParse(save[5], out MeritalArts value);
            MeritalArt = (success ? value : MeritalArts.Nope);
        }
    }
}
