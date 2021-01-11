using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.OctopusIsland
{
    class Character : Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.OctopusIsland.Character();

        public string Name { get; set; }

        public int SergeHitpoint { get; set; }
        public int SergeSkill { get; set; }
        public int XolotlHitpoint { get; set; }
        public int XolotlSkill { get; set; }
        public int ThibautHitpoint { get; set; }
        public int ThibautSkill { get; set; }
        public int SouhiHitpoint { get; set; }
        public int SouhiSkill { get; set; }
        public int Food { get; set; }

        public int Hitpoint { get; set; }
        public int Skill { get; set; }

        public void Init()
        {
            Name = String.Empty;

            ThibautHitpoint = 20;
            SergeHitpoint = 20;
            XolotlHitpoint = 20;
            SouhiHitpoint = 20;

            ThibautSkill = Game.Dice.Roll() + 6;
            SergeSkill = ThibautSkill - 1;
            XolotlSkill = SergeSkill;
            SouhiSkill = XolotlSkill;

            Food = 4;
        }

        public Character Clone()
        {
            return new Character() {

                ThibautHitpoint = this.ThibautHitpoint,
                ThibautSkill = this.ThibautSkill,
                SergeHitpoint = this.SergeHitpoint,
                SergeSkill = this.SergeSkill,
                XolotlHitpoint = this.XolotlHitpoint,
                XolotlSkill = this.XolotlSkill,
                SouhiHitpoint = this.SouhiHitpoint,
                SouhiSkill = this.SouhiSkill,

                Name = this.Name,
                Hitpoint = this.Hitpoint,
                Skill = this.Skill,
                Food = this.Food,
            };
        }

        public string Save()
        {
            return String.Format(
                "{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}",
                ThibautHitpoint, ThibautSkill, SergeHitpoint, SergeSkill,
                XolotlHitpoint, XolotlSkill, SouhiHitpoint, SouhiSkill, Food
            );
        }

        public void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            ThibautHitpoint = int.Parse(save[0]);
            ThibautSkill = int.Parse(save[1]);
            SergeHitpoint = int.Parse(save[2]);
            SergeSkill = int.Parse(save[3]);
            XolotlHitpoint = int.Parse(save[4]);
            XolotlSkill = int.Parse(save[5]);
            SouhiHitpoint = int.Parse(save[6]);
            SouhiSkill = int.Parse(save[7]);
            Food = int.Parse(save[8]);
        }
    }
}
