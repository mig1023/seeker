using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.MentorsAlwaysRight
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
        public int MaxHitpoints { get; set; }
        public int Hitpoints
        {
            get => _hitpoints;
            set => _hitpoints = Game.Param.Setter(value, max: MaxHitpoints, _hitpoints, this);
        }

        private int _magicpoints;
        public int Magicpoints
        {
            get => _magicpoints;
            set => _magicpoints = Game.Param.Setter(value, _magicpoints, this);
        }

        private int _transformation;
        public int Transformation
        {
            get => _transformation;
            set => _transformation = Game.Param.Setter(value, _transformation, this);
        }

        private int _gold;
        public int Gold
        {
            get => _gold;
            set => _gold = Game.Param.Setter(value, _gold, this);
        }

        public List<string> Spells { get; set; }
        public List<string> SpellsReplica { get; set; }

        public int Elixir { get; set; }

        public SpecializationType Specialization { get; set; }

        public override void Init()
        {
            base.Init();

            Strength = 12;
            MaxHitpoints = 30;
            Hitpoints = MaxHitpoints;
            Magicpoints = 1;
            Transformation = 1;
            Gold = 15;
            Elixir = 1;
            Specialization = SpecializationType.Nope;
            Spells = new List<string>();
            SpellsReplica = new List<string>();
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            Strength = this.Strength,
            MaxHitpoints = this.MaxHitpoints,
            Hitpoints = this.Hitpoints,
            Magicpoints = this.Magicpoints,
            Transformation = this.Transformation,
            Gold = this.Gold,
            Elixir = this.Elixir,
            Specialization = this.Specialization,
        };

        public override string Save() => String.Join("|",
            Strength, MaxHitpoints, Hitpoints, Magicpoints, Transformation, Gold, Elixir, Specialization,
            String.Join(",", Spells).TrimEnd(','), String.Join(",", SpellsReplica).TrimEnd(','));

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Strength = int.Parse(save[0]);
            MaxHitpoints = int.Parse(save[1]);
            Hitpoints = int.Parse(save[2]);
            Magicpoints = int.Parse(save[3]);
            Transformation = int.Parse(save[4]);
            Gold = int.Parse(save[5]);
            Elixir = int.Parse(save[6]);

            bool success = Enum.TryParse(save[7], out SpecializationType value);
            Specialization = (success ? value : SpecializationType.Nope);

            Spells = save[8].Split(',').ToList();
            SpellsReplica = save[9].Split(',').ToList();

            IsProtagonist = true;
        }
    }
}
