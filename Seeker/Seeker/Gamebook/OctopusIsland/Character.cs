using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.OctopusIsland
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.OctopusIsland.Character();

        private int _sergeHitpoint;
        public int SergeHitpoint
        {
            get => _sergeHitpoint;
            set
            {
                if (value < 0)
                    _sergeHitpoint = 0;
                else if (value > 20)
                    _sergeHitpoint = 20;
                else
                    _sergeHitpoint = value;
            }
        }

        public int SergeSkill { get; set; }

        private int _xolotlHitpoint;
        public int XolotlHitpoint
        {
            get => _xolotlHitpoint;
            set
            {
                if (value < 0)
                    _xolotlHitpoint = 0;
                else if (value > 20)
                    _xolotlHitpoint = 20;
                else
                    _xolotlHitpoint = value;
            }
        }

        public int XolotlSkill { get; set; }

        private int _thibautHitpoint;
        public int ThibautHitpoint
        {
            get => _thibautHitpoint;
            set
            {
                if (value < 0)
                    _thibautHitpoint = 0;
                else if (value > 20)
                    _thibautHitpoint = 20;
                else
                    _thibautHitpoint = value;
            }
        }

        public int ThibautSkill { get; set; }

        private int _souhiHitpoint;
        public int SouhiHitpoint
        {
            get => _souhiHitpoint;
            set
            {
                if (value < 0)
                    _souhiHitpoint = 0;
                else if (value > 20)
                    _souhiHitpoint = 20;
                else
                    _souhiHitpoint = value;
            }
        }

        public int SouhiSkill { get; set; }

        public int Food { get; set; }
        public int LifeGivingOintment { get; set; }

        private int _hitpoint;
        public int Hitpoint
        {
            get => _hitpoint;
            set
            {
                if (value < 0)
                    _hitpoint = 0;
                else
                    _hitpoint = value;
            }
        }

        public int Skill { get; set; }

        public override void Init()
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
            LifeGivingOintment = 40;
        }

        public Character Clone() => new Character()
        {
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
            LifeGivingOintment = this.LifeGivingOintment,
        };

        public override string Save() => String.Join("|", ThibautHitpoint, ThibautSkill, SergeHitpoint, SergeSkill,
            XolotlHitpoint, XolotlSkill, SouhiHitpoint, SouhiSkill, Food, LifeGivingOintment);

        public override void Load(string saveLine)
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
            LifeGivingOintment = int.Parse(save[9]);
        }
    }
}
