using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.SwampFever
{
    class Character : Interfaces.ICharacter
    {
        public static Character Protagonist = new Gamebook.SwampFever.Character();

        public string Name { get; set; }
        public int Fury { get; set; }
        public int Creds { get; set; }
        public int Stigon { get; set; }
        public int Rate { get; set; }
        public int Hitpoints { get; set; }

        public int SecondEngine { get; set; }
        public int Stealth { get; set; }
        public int Radar { get; set; }
        public int CircularSaw { get; set; }
        public int Flamethrower { get; set; }
        public int PlasmaCannon { get; set; }
        public int Harmonizer { get; set; }

        public void Init()
        {
            Name = String.Empty;
            Fury = 0;
            Creds = 0;
            Stigon = 0;
            Rate = 100;
            Hitpoints = 1;
            SecondEngine = 0;
            Stealth = 0;
            Radar = 0;
            CircularSaw = 0;
            Flamethrower = 0;
            PlasmaCannon = 0;
            Harmonizer = 0;
        }

        public Character Clone()
        {
            return new Character() {
                Name = this.Name,
                Fury = this.Fury,
                Creds = this.Creds,
                Stigon = this.Stigon,
                Rate = this.Rate,
                Hitpoints = this.Hitpoints,
                SecondEngine = this.SecondEngine,
                Stealth = this.Stealth,
                Radar = this.Radar,
                CircularSaw = this.CircularSaw,
                Flamethrower = this.Flamethrower,
                PlasmaCannon = this.PlasmaCannon,
                Harmonizer = this.Harmonizer,
            };
        }
    }
}
