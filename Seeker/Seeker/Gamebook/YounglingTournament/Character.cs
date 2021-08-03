using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.YounglingTournament
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();

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

        public override void Init()
        {
            Name = String.Empty;
            LightSide = 100;
            DarkSide = 0;
            MaxHitpoints = 30;
            Hitpoints = MaxHitpoints;
            Pilot = 1;
            Stealth = 1;
            Hacking = 1;
        }

        public Character Clone() => new Character()
        {
            Name = this.Name,
            LightSide = this.LightSide,
            DarkSide = this.DarkSide,
            MaxHitpoints = this.MaxHitpoints,
            Hitpoints = this.Hitpoints,
            Pilot = this.Pilot,
            Stealth = this.Stealth,
            Hacking = this.Hacking,
        };

        public override string Save() => String.Join("|",
            LightSide,
            DarkSide,
            MaxHitpoints,
            Hitpoints,
            Pilot,
            Stealth,
            Hacking
        );

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            LightSide = int.Parse(save[0]);
            DarkSide = int.Parse(save[1]);
            MaxHitpoints = int.Parse(save[2]);
            Hitpoints = int.Parse(save[3]);
            Pilot = int.Parse(save[4]);
            Stealth = int.Parse(save[5]);
            Hacking = int.Parse(save[6]);
        }
    }
}
