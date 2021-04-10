using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.SwampFever
{
    class Character : Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.SwampFever.Character();

        public string Name { get; set; }

        private int _fury;
        public int Fury
        {
            get => _fury;
            set
            {
                if (value < -2)
                    _fury = -2;
                else if (value > 2)
                    _fury = 2;
                else
                    _fury = value;
            }
        }

        private int _creds;
        public int Creds
        {
            get => _creds;
            set
            {
                if (value < 0)
                    _creds = 0;
                else
                    _creds = value;
            }
        }

        private int _stigon;
        public int Stigon
        {
            get => _stigon;
            set
            {
                if (value < 0)
                    _stigon = 0;
                else if (value > 6)
                    _stigon = 6;
                else
                    _stigon = value;
            }
        }

        private int _rate;
        public int Rate
        {
            get => _rate;
            set
            {
                if (value < 5)
                    _rate = 5;
                else
                    _rate = value;
            }
        }

        public int Hitpoints { get; set; }

        public int SecondEngine { get; set; }
        public int Stealth { get; set; }
        public int Radar { get; set; }
        public int CircularSaw { get; set; }
        public int Flamethrower { get; set; }
        public int PlasmaCannon { get; set; }
        public int Harmonizer { get; set; }

        public int AcousticMembrane { get; set; }
        public int LiveMucus { get; set; }

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
            AcousticMembrane = 0;
            LiveMucus = 0;
        }

        public Character Clone() => new Character() {
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
            AcousticMembrane = this.AcousticMembrane,
            LiveMucus = this.LiveMucus,
        };

        public string Save() => String.Format("|", Fury, Creds, Stigon, Rate, Hitpoints, SecondEngine, Stealth, Radar,
                CircularSaw, Flamethrower, PlasmaCannon, Harmonizer, AcousticMembrane, LiveMucus);

        public void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Fury = int.Parse(save[0]);
            Creds = int.Parse(save[1]);
            Stigon = int.Parse(save[2]);
            Rate = int.Parse(save[3]);
            Hitpoints = int.Parse(save[4]);
            SecondEngine = int.Parse(save[5]);
            Stealth = int.Parse(save[6]);
            Radar = int.Parse(save[7]);
            CircularSaw = int.Parse(save[8]);
            Flamethrower = int.Parse(save[9]);
            PlasmaCannon = int.Parse(save[10]);
            Harmonizer = int.Parse(save[11]);
            AcousticMembrane = int.Parse(save[12]);
            LiveMucus = int.Parse(save[13]);
        }
    }
}
