using System;

namespace Seeker.Gamebook.SeaTales
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist { get; set; }
        public override void Set(object character) =>
            Protagonist = (Character)character;

        public int Heat { get; set; }

        private int _nonsense;
        public int Nonsense
        {
            get => _nonsense;
            set => _nonsense = Game.Param.Setter(value);
        }

        public bool Gameover { get; set; }

        public bool NeedCredibilityCheck { get; set; }

        public int Heroism { get; set; }

        private int _alcoholism;
        public int Alcoholism
        {
            get => _alcoholism;
            set => _alcoholism = Game.Param.Setter(value);
        }
        
        private int _inspiration;
        public int Inspiration
        {
            get => _inspiration;
            set => _inspiration = Game.Param.Setter(value);
        }

        private int _adventurism;
        public int Adventurism
        {
            get => _adventurism;
            set
            {
                if (value < 0)
                    _adventurism = 0;
                else if (value > 10)
                    _adventurism = 10;
                else
                    _adventurism = value;
            }
        }

        private int _travel;
        public int Travel
        {
            get => _travel;
            set
            {
                if (value < -100)
                    _travel = -100;
                else if (value > 100)
                    _travel = 100;
                else
                    _travel = value;
            }
        }

        public int Reputation { get; set; }

        public int NarrativePoints { get; set; }

        public override void Init()
        {
            base.Init();
            Heat = 0;
            Nonsense = 0;
            NeedCredibilityCheck = false;
            Gameover = false;

            Heroism = 0;
            Alcoholism = 0;
            Adventurism = 5;
            Inspiration = 1;
            Travel = 0;
            Reputation = 0;
            NarrativePoints = 0;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            Heat = this.Heat,
            Nonsense = this.Nonsense,
            NeedCredibilityCheck = this.NeedCredibilityCheck,
            Gameover = this.Gameover,

            Heroism = this.Heroism,
            Alcoholism = this.Alcoholism,
            Adventurism = this.Adventurism,
            Inspiration = this.Inspiration,
            Travel = this.Travel,
            Reputation = this.Reputation,
            NarrativePoints = this.NarrativePoints,
        };

        public override string Save() => String.Join("|",
            Heat, Nonsense, NeedCredibilityCheck ? 1 : 0, Gameover ? 1 : 0,
            Heroism, Alcoholism, Adventurism, Inspiration, Travel, Reputation,
            NarrativePoints);

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Heat = Game.Xml.IntParse(save[0]);
            Nonsense = Game.Xml.IntParse(save[1]);
            NeedCredibilityCheck = save[2] == "1";
            Gameover = save[3] == "1";

            Heroism = Game.Xml.IntParse(save[4]);
            Alcoholism = Game.Xml.IntParse(save[5]);
            Adventurism = Game.Xml.IntParse(save[6]);
            Inspiration = Game.Xml.IntParse(save[7]);
            Travel = Game.Xml.IntParse(save[8]);
            Reputation = Game.Xml.IntParse(save[9]);
            NarrativePoints = Game.Xml.IntParse(save[10]);

            IsProtagonist = true;
        }
    }
}
