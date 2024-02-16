using System;

namespace Seeker.Gamebook.OctopusIsland
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public new static Character Protagonist = new Character();
        public new static Character GetInstance() => Protagonist;


        private int _sergeHitpoint;
        public int SergeHitpoint
        {
            get => _sergeHitpoint;
            set => _sergeHitpoint = Game.Param.Setter(value, max: 20, _sergeHitpoint, this);
        }

        public int SergeSkill { get; set; }

        private int _xolotlHitpoint;
        public int XolotlHitpoint
        {
            get => _xolotlHitpoint;
            set => _xolotlHitpoint = Game.Param.Setter(value, max: 20, _xolotlHitpoint, this);
        }

        public int XolotlSkill { get; set; }

        private int _thibautHitpoint;
        public int ThibautHitpoint
        {
            get => _thibautHitpoint;
            set => _thibautHitpoint = Game.Param.Setter(value, max: 20, _thibautHitpoint, this);
        }

        public int ThibautSkill { get; set; }

        private int _souhiHitpoint;
        public int SouhiHitpoint
        {
            get => _souhiHitpoint;
            set => _souhiHitpoint = Game.Param.Setter(value, max: 20, _souhiHitpoint, this);
        }

        public int SouhiSkill { get; set; }

        private int _food;
        public int Food
        {
            get => (StolenStuffs == 0 ? _food : 0);
            set => _food = Game.Param.Setter(value, _food, this);
        }

        private int _lifeGivingOintment;
        public int LifeGivingOintment
        {
            get => (StolenStuffs == 0 ? _lifeGivingOintment : 0);
            set => _lifeGivingOintment = Game.Param.Setter(value, _lifeGivingOintment, this);
        }
        public int StolenStuffs { get; set; }

        private int _hitpoint;
        public int Hitpoint
        {
            get => _hitpoint;
            set => _hitpoint = Game.Param.Setter(value);
        }

        public int Skill { get; set; }

        public override void Init()
        {
            base.Init();

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
            StolenStuffs = 0;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,

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
            StolenStuffs = this.StolenStuffs,
        };

        public override string Save() => String.Join("|",
            ThibautHitpoint, ThibautSkill, SergeHitpoint, SergeSkill, XolotlHitpoint,
            XolotlSkill, SouhiHitpoint, SouhiSkill, Food, LifeGivingOintment, StolenStuffs);

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
            StolenStuffs = int.Parse(save[10]);

            IsProtagonist = true;
        }
    }
}
