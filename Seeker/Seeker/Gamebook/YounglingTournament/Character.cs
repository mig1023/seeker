using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.YounglingTournament
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();

        public enum Techniques { Speed, Push, Attraction, Jump, Foresight, Conceal, Sight }

        private int _lightSide;
        public int LightSide
        {
            get => _lightSide;
            set => _lightSide = Game.Param.Setter(value);
        }

        private int _darkSide;
        public int DarkSide
        {
            get => _darkSide;
            set => _darkSide = Game.Param.Setter(value);
        }

        private int _hitpoints;
        public int MaxHitpoints { get; set; }
        public int Hitpoints
        {
            get => _hitpoints;
            set => _hitpoints = Game.Param.Setter(value, max: MaxHitpoints);
        }

        private int _accuracy;
        public int Accuracy
        {
            get => _accuracy;
            set => _accuracy = Game.Param.Setter(value);
        }

        private int _pilot;
        public int Pilot
        {
            get => _pilot;
            set => _pilot = Game.Param.Setter(value);
        }

        private int _stealth;
        public int Stealth
        {
            get => _stealth;
            set => _stealth = Game.Param.Setter(value);
        }

        private int _hacking;
        public int Hacking
        {
            get => _hacking;
            set => _hacking = Game.Param.Setter(value);
        }

        private int _shield;
        public int Shield
        {
            get => _shield;
            set => _shield = Game.Param.Setter(value);
        }

        public SortedDictionary<Techniques, int> ForceTechniques { get; set; }

        public override void Init()
        {
            Name = String.Empty;
            LightSide = 100;
            DarkSide = 0;
            MaxHitpoints = 30;
            Hitpoints = MaxHitpoints;
            Accuracy = 10;
            Pilot = 1;
            Stealth = 1;
            Hacking = 1;
            Shield = 0;

            ForceTechniques = new SortedDictionary<Techniques, int>
            {
                [Techniques.Speed] = 1,
                [Techniques.Push] = 1,
                [Techniques.Attraction] = 1,
                [Techniques.Jump] = 1,
                [Techniques.Foresight] = 1,
                [Techniques.Conceal] = 1,
                [Techniques.Sight] = 1,
            };
        }

        public Character Clone() => new Character()
        {
            Name = this.Name,
            LightSide = this.LightSide,
            DarkSide = this.DarkSide,
            MaxHitpoints = this.MaxHitpoints,
            Hitpoints = this.Hitpoints,
            Accuracy = this.Accuracy,
            Pilot = this.Pilot,
            Stealth = this.Stealth,
            Hacking = this.Hacking,
            Shield = this.Shield,
        };

        public override string Save() => String.Join("|",
            LightSide,
            DarkSide,
            MaxHitpoints,
            Hitpoints,
            Accuracy,
            Pilot,
            Stealth,
            Hacking,
            String.Join(",", ForceTechniques.Values)
        );

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            LightSide = int.Parse(save[0]);
            DarkSide = int.Parse(save[1]);
            MaxHitpoints = int.Parse(save[2]);
            Hitpoints = int.Parse(save[3]);
            Accuracy = int.Parse(save[4]);
            Pilot = int.Parse(save[5]);
            Stealth = int.Parse(save[6]);
            Hacking = int.Parse(save[7]);

            string [] forces = save[8].Split(',');

            ForceTechniques = new SortedDictionary<Techniques, int>
            {
                [Techniques.Speed] = int.Parse(forces[0]),
                [Techniques.Push] = int.Parse(forces[1]),
                [Techniques.Attraction] = int.Parse(forces[2]),
                [Techniques.Jump] = int.Parse(forces[3]),
                [Techniques.Foresight] = int.Parse(forces[4]),
                [Techniques.Conceal] = int.Parse(forces[5]),
                [Techniques.Sight] = int.Parse(forces[6]),
            };
        }
    }
}
