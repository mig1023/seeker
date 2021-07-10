using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.MentorsAlwaysRight
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.MentorsAlwaysRight.Character();

        public enum SpecializationType { Warrior, Wizard, Thrower, Nope };

        private int _strength;
        public int Strength
        {
            get => _strength;
            set
            {
                if (value < 0)
                    _strength = 0;
                else
                    _strength = value;
            }
        }

        private int _hitpoints;
        public int Hitpoints
        {
            get => _hitpoints;
            set
            {
                if (value < 0)
                    _hitpoints = 0;
                else if (value > 30)
                    _hitpoints = 30;
                else
                    _hitpoints = value;
            }
        }

        private int _magicpoints;
        public int Magicpoints
        {
            get => _magicpoints;
            set
            {
                if (value < 0)
                    _magicpoints = 0;
                else
                    _magicpoints = value;
            }
        }

        private int _transformation;
        public int Transformation
        {
            get => _transformation;
            set
            {
                if (value < 0)
                    _transformation = 0;
                else
                    _transformation = value;
            }
        }

        private int _gold;
        public int Gold
        {
            get => _gold;
            set
            {
                if (value < 0)
                    _gold = 0;
                else
                    _gold = value;
            }
        }

        public int Elixir { get; set; }

        public SpecializationType Specialization { get; set; }

        public override void Init()
        {
            Name = String.Empty;
            Strength = 12;
            Hitpoints = 30;
            Magicpoints = 1;
            Transformation = 1;
            Gold = 15;
            Elixir = 1;
            Specialization = SpecializationType.Nope;
        }

        public Character Clone() => new Character()
        {
            Name = this.Name,
            Strength = this.Strength,
            Hitpoints = this.Hitpoints,
            Magicpoints = this.Magicpoints,
            Transformation = this.Transformation,
            Gold = this.Gold,
            Elixir = this.Elixir,
            Specialization = this.Specialization,
        };

        public override string Save() => String.Join("|", Strength, Hitpoints, Magicpoints, Transformation, Gold, Specialization, Elixir);

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Strength = int.Parse(save[0]);
            Hitpoints = int.Parse(save[1]);
            Magicpoints = int.Parse(save[2]);
            Transformation = int.Parse(save[3]);
            Gold = int.Parse(save[4]);
            Elixir = int.Parse(save[6]);

            bool success = Enum.TryParse(save[5], out SpecializationType value);
            Specialization = (success ? value : SpecializationType.Nope);
        }
    }
}
