using System;

namespace Seeker.Gamebook.AdventuresOfABeardlessDeceiver
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
        public static Character GetInstance() => Protagonist;

        private int _strength;
        public int Strength
        {
            get => _strength;
            set => _strength = Game.Param.Setter(value, max: 12, _strength, this);
        }

        private int _skill;
        public int Skill
        {
            get => _skill;
            set => _skill = Game.Param.Setter(value, max: 12, _skill, this);
        }

        private int _wisdom;
        public int Wisdom
        {
            get => _wisdom;
            set => _wisdom = Game.Param.Setter(value, max: 12, _wisdom, this);
        }

        private int _cunning;
        public int Cunning
        {
            get => _cunning;
            set => _cunning = Game.Param.Setter(value, max: 12, _cunning, this);
        }

        private int _oratory;
        public int Oratory
        {
            get => _oratory;
            set => _oratory = Game.Param.Setter(value, max: 12, _oratory, this);
        }

        private int _popularity;
        public int Popularity
        {
            get => _popularity;
            set => _popularity = Game.Param.Setter(value, _popularity, this);
        }

        private int _kumis;
        public int Kumis
        {
            get => _kumis;
            set => _kumis = Game.Param.Setter(value, max: 1, _kumis, this);
        }

        private int _tanga;
        public int Tanga
        {
            get => _tanga;
            set => _tanga = Game.Param.Setter(value, _tanga, this);
        }

        public int? AkynGlory { get; set; }
        public int? UnitOfTime { get; set; }

        public int StatBonuses { get; set; }

        public override void Init()
        {
            base.Init();

            Strength = 1;
            Skill = 1;
            Wisdom = 1;
            Cunning = 1;
            Oratory = 1;
            Popularity = 3;
            Kumis = 1;
            Tanga = 15;
            StatBonuses = 4;
            AkynGlory = null;
            UnitOfTime = null;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Strength = this.Strength,
            Skill = this.Skill,
            Wisdom = this.Wisdom,
            Cunning = this.Cunning,
            Oratory = this.Oratory,
            Popularity = this.Popularity,
            Kumis = this.Kumis,
            Tanga = this.Tanga,
            StatBonuses = this.StatBonuses,
            AkynGlory = this.AkynGlory,
            UnitOfTime = this.UnitOfTime,
        };

        public override string Save() => String.Join("|",
            Strength, Skill, Wisdom, Cunning, Oratory, Popularity, Kumis, Tanga, StatBonuses,
            (AkynGlory == null ? -1 : AkynGlory), (UnitOfTime == null ? -1 : UnitOfTime)
        );
 
        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Strength = int.Parse(save[0]);
            Skill = int.Parse(save[1]);
            Wisdom = int.Parse(save[2]);
            Cunning = int.Parse(save[3]);
            Oratory = int.Parse(save[4]);
            Popularity = int.Parse(save[5]);
            Kumis = int.Parse(save[6]);
            Tanga = int.Parse(save[7]);
            StatBonuses = int.Parse(save[8]);
            AkynGlory = Game.Continue.IntNullableParse(save[9]);
            UnitOfTime = Game.Continue.IntNullableParse(save[10]);

            IsProtagonist = true;
        }
    }
}
