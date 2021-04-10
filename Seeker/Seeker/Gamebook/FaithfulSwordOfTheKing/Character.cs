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
                else if (value< 0)
                    _strength = 0;
                else
                    _strength = value;
            }
        }

        private int _honor;
        public int Honor
        {
            get => _honor;
            set
            {
                if (value < 0)
                    _honor = 0;
                else
                    _honor = value;
            }
        }

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
            MaxSkill = Constants.Skills[Game.Dice.Roll()];
            Skill = MaxSkill;
            MaxStrength = Constants.Strengths[Game.Dice.Roll()];
            Strength = MaxStrength;
            Honor = 3;
            Day = 1;
            Ecu = 1500;
            MeritalArt = MeritalArts.Nope;
            Horses = 1;
            Pistols = 1;
            BulletsAndGubpowder = 5;
            Daggers = 0;
            HadFoodToday = 0;
            Chainmail = 0;
            LeftHandPenalty = 0;

            Game.Healing.Add(name: "Поесть", healing: 2, portions: 2);
        }

        public Character Clone()
        {
            return new Character()
            {
                Name = this.Name,
                MaxSkill = this.MaxSkill,
                Skill = this.Skill,
                MaxStrength = this.MaxStrength,
                Strength = this.Strength,
                Honor = this.Honor,
                Day = this.Day,
                Ecu = this.Ecu,
                MeritalArt = this.MeritalArt,
                Horses = this.Horses,
                Pistols = this.Pistols,
                BulletsAndGubpowder = this.BulletsAndGubpowder,
                Daggers = this.Daggers,
                HadFoodToday = this.HadFoodToday,
                Chainmail = this.Chainmail,
                LeftHandPenalty = this.LeftHandPenalty,

            };
        }

        public string Save()
        {
            return String.Join("|", MaxSkill, Skill, MaxStrength, Strength, Honor, Day,
                Ecu, MeritalArt, Horses, Pistols, BulletsAndGubpowder, Daggers, HadFoodToday,
                Chainmail, LeftHandPenalty);

            //return String.Format(
            //    "{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}",
            //    MaxSkill, Skill, MaxStrength, Strength, Honor, Day, Ecu, MeritalArt,
            //    Horses, Pistols, BulletsAndGubpowder, Daggers, HadFoodToday,
            //    Chainmail, LeftHandPenalty
            //);
        }

        public void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            MaxSkill = int.Parse(save[0]);
            Skill = int.Parse(save[1]);
            MaxStrength = int.Parse(save[2]);
            Strength = int.Parse(save[3]);
            Honor = int.Parse(save[4]);
            Day = int.Parse(save[5]);
            Ecu = int.Parse(save[6]);
            Horses = int.Parse(save[8]);
            Pistols = int.Parse(save[9]);
            BulletsAndGubpowder = int.Parse(save[10]);
            Daggers = int.Parse(save[11]);
            HadFoodToday = int.Parse(save[12]);
            Chainmail = int.Parse(save[13]);
            LeftHandPenalty = int.Parse(save[14]);

            bool success = Enum.TryParse(save[7], out MeritalArts value);
            MeritalArt = (success ? value : MeritalArts.Nope);
        }
    }
}
