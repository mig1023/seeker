using System;

namespace Seeker.Gamebook.PowerOfFear
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist { get; set; }
        public override void Set(object character) =>
            Protagonist = (Character)character;

        private int _hitpoints;
        public int Hitpoints
        {
            get => _hitpoints;
            set => _hitpoints = Game.Param.Setter(value, _hitpoints, this);
        }

        private int _hunting;
        public int Hunting
        {
            get => _hunting;
            set => _hunting = Game.Param.Setter(value, max: 10);
        }

        private int _agility;
        public int Agility
        {
            get => _agility;
            set => _agility = Game.Param.Setter(value, max: 10);
        }

        private int _luck;
        public int Luck
        {
            get => _luck;
            set => _luck = Game.Param.Setter(value, max: 10);
        }

        private int _weapon;
        public int Weapon
        {
            get => _weapon;
            set => _weapon = Game.Param.Setter(value, max: 10);
        }

        private int _theft;
        public int Theft
        {
            get => _theft;
            set => _theft = Game.Param.Setter(value, max: 10);
        }

        private int _stealth;
        public int Stealth
        {
            get => _stealth;
            set => _stealth = Game.Param.Setter(value, max: 10);
        }

        private int _knowledge;
        public int Knowledge
        {
            get => _knowledge;
            set => _knowledge = Game.Param.Setter(value, max: 10);
        }

        private int _strength;
        public int Strength
        {
            get => _strength;
            set => _strength = Game.Param.Setter(value, max: 10);
        }

        public override void Init()
        {
            base.Init();

            Hitpoints = 10;
            Hunting = 0;
            Agility = 0;
            Luck = 0;
            Weapon = 0;
            Theft = 0;
            Stealth = 0;
            Knowledge = 0;
            Strength = 0;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            Hitpoints = this.Hitpoints,
            Hunting = this.Hunting,
            Agility = this.Agility,
            Luck = this.Luck,
            Weapon = this.Weapon,
            Theft = this.Theft,
            Stealth = this.Stealth,
            Knowledge = this.Knowledge,
            Strength = this.Strength,
        };

        public override string Save() => String.Join("|",
            Hitpoints, Hunting, Agility, Luck, Weapon, Theft, Stealth, Knowledge, Strength);

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Hitpoints = int.Parse(save[0]);
            Hunting = int.Parse(save[1]);
            Agility = int.Parse(save[2]);
            Luck = int.Parse(save[3]);
            Weapon = int.Parse(save[4]);
            Theft = int.Parse(save[5]);
            Stealth = int.Parse(save[6]);
            Knowledge = int.Parse(save[7]);
            Strength = int.Parse(save[8]);
            
            IsProtagonist = true;
        }
    }
}
