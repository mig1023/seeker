using System;

namespace Seeker.Gamebook.LegendsAlwaysLie
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public new static Character Protagonist = new Character();
        public new static Character GetInstance() => Protagonist;

        public enum SpecializationType { Warrior, Wizard, Thrower, Nope };

        private int _strength;
        public int Strength
        {
            get => _strength;
            set => _strength = Game.Param.Setter(value, _strength, this);
        }

        private int _hitpoints;
        public int Hitpoints
        {
            get => _hitpoints;
            set => _hitpoints = Game.Param.Setter(value, max: 30, _hitpoints, this);
        }

        private int _magicpoints;
        public int Magicpoints
        {
            get => _magicpoints;
            set => _magicpoints = Game.Param.Setter(value, _magicpoints, this);
        }

        private int _gold;
        public int Gold
        {
            get => _gold;
            set => _gold = Game.Param.Setter(value, _gold, this);
        }

        public int Footwraps { get; set; }
        public int TimeForReading { get; set; }
        public int Elixir { get; set; }

        private int _conneryHitpoints;
        public int ConneryHitpoints
        {
            get => _conneryHitpoints;
            set => _conneryHitpoints = Game.Param.Setter(value, max: 30, _conneryHitpoints, this);
        }

        private int _conneryTrust;
        public int ConneryTrust
        {
            get => _conneryTrust;
            set => _conneryTrust = Game.Param.Setter(value, _conneryTrust, this);
        }

        public SpecializationType Specialization { get; set; }

        public override void Init()
        {
            base.Init();

            Strength = 12;
            Hitpoints = 30;
            Magicpoints = 0;
            Gold = 20;
            Footwraps = 0;
            TimeForReading = 60;
            Elixir = 1;
            ConneryHitpoints = 30;
            ConneryTrust = 5;
            Specialization = SpecializationType.Nope;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            Strength = this.Strength,
            Hitpoints = this.Hitpoints,
            Magicpoints = this.Magicpoints,
            Gold = this.Gold,
            Footwraps = this.Footwraps,
            TimeForReading = this.TimeForReading,
            Elixir = this.Elixir,
            ConneryHitpoints = this.ConneryHitpoints,
            ConneryTrust = this.ConneryTrust,
            Specialization = this.Specialization,
        };

        public override string Save() => String.Join("|",
            Strength, Hitpoints, Magicpoints, Gold, Footwraps, TimeForReading,
            ConneryHitpoints, ConneryTrust, Specialization, Elixir);

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Strength = int.Parse(save[0]);
            Hitpoints = int.Parse(save[1]);
            Magicpoints = int.Parse(save[2]);
            Gold = int.Parse(save[3]);
            Footwraps = int.Parse(save[4]);
            TimeForReading = int.Parse(save[5]);
            ConneryHitpoints = int.Parse(save[6]);
            ConneryTrust = int.Parse(save[7]);
            Elixir = int.Parse(save[9]);

            bool success = Enum.TryParse(save[8], out SpecializationType value);
            Specialization = (success ? value : SpecializationType.Nope);

            IsProtagonist = true;
        }
    }
}
