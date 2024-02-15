using System;

namespace Seeker.Gamebook.MadameGuillotine
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
        public static Character GetInstance() => Protagonist;

        public int Hitpoints { get; set; }
        private int _wounds;
        public int Wounds
        {
            get => _wounds;
            set => _wounds = Game.Param.Setter(value, max: Hitpoints, _wounds, this);
        }

        private int _strength;
        public int Strength
        {
            get => _strength;
            set => _strength = Game.Param.Setter(value, min: 2, max: 12, _strength, this);
        }

        private int _agility;
        public int Agility
        {
            get => _agility;
            set => _agility = Game.Param.Setter(value, min: 2, max: 12, _agility, this);
        }

        private int _luck;
        public int Luck
        {
            get => _luck;
            set => _luck = Game.Param.Setter(value, min: 2, max: 12, _luck, this);
        }

        private int _speech;
        public int Speech
        {
            get => _speech;
            set => _speech = Game.Param.Setter(value, min: 2, max: 12, _speech, this);
        }

        private int _firearms;
        public int Firearms
        {
            get => _firearms;
            set => _firearms = Game.Param.Setter(value, min: 2, max: 12, _firearms, this);
        }

        private int _fencing;
        public int Fencing
        {
            get => _fencing;
            set => _fencing = Game.Param.Setter(value, min: 2, max: 12, _fencing, this);
        }

        private int _horseRiding;
        public int HorseRiding
        {
            get => _horseRiding;
            set => _horseRiding = Game.Param.Setter(value, min: 2, max: 12, _horseRiding, this);
        }

        public int StatBonuses { get; set; }
        public string Weapon { get; set; }
        public int Skill { get; set; }

        public override void Init()
        {
            base.Init();

            Hitpoints = 1;
            Wounds = 0;
            Strength = 2;
            Agility = 2;
            Luck = 2;
            Speech = 2;
            Firearms = 2;
            Fencing = 2;
            HorseRiding = 2;
            StatBonuses = 36;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            Hitpoints = this.Hitpoints,
            Wounds = this.Wounds,
            Strength = this.Strength,
            Agility = this.Agility,
            Luck = this.Luck,
            Speech = this.Speech,
            Firearms = this.Firearms,
            Fencing = this.Fencing,
            HorseRiding = this.HorseRiding,
            Weapon = this.Weapon,
            Skill = this.Skill,
            StatBonuses = this.StatBonuses,
        };

        public override string Save() => String.Join("|",
            Hitpoints, Wounds, Strength, Agility, Luck,
            Speech, Firearms, Fencing, HorseRiding, StatBonuses);

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Hitpoints = int.Parse(save[0]);
            Wounds = int.Parse(save[1]);
            Strength = int.Parse(save[2]);
            Agility = int.Parse(save[3]);
            Luck = int.Parse(save[4]);
            Speech = int.Parse(save[5]);
            Firearms = int.Parse(save[6]);
            Fencing = int.Parse(save[7]);
            HorseRiding = int.Parse(save[8]);
            StatBonuses = int.Parse(save[9]);

            IsProtagonist = true;
        }
    }
}
