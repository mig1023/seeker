using System;

namespace Seeker.Gamebook.Cyberpunk
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();

        private int _planning;
        public int Planning
        {
            get => _planning;
            set => _planning = Game.Param.Setter(value, _planning, this);
        }

        private int _preparation;
        public int Preparation
        {
            get => _preparation;
            set => _preparation = Game.Param.Setter(value, _preparation, this);
        }

        private int _luck;
        public int Luck
        {
            get => _luck;
            set => _luck = Game.Param.Setter(value, _luck, this);
        }

        private int _cybernetics;
        public int Cybernetics
        {
            get => _cybernetics;
            set => _cybernetics = Game.Param.Setter(value, max: 99, _cybernetics, this);
        }

        private int _morality;
        public int Morality
        {
            get => _morality;
            set
            {
                _morality = Game.Param.Setter(value, max: 100, _morality, this);

                if (_morality > 0)
                    _careerism = 100 - _morality;
            }
        }

        private int _careerism;
        public int Careerism
        {
            get => _careerism;
            set
            {
                _careerism = Game.Param.Setter(value, max: 100, _careerism, this);

                if (_careerism > 0)
                    _morality = 100 - _careerism;
            }
        }

        public override void Init()
        {
            base.Init();

            Planning = 0;
            Preparation = 0;
            Luck = 0;
            Cybernetics = 1;
            Morality = 0;
            Careerism = 0;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            Planning = this.Planning,
            Preparation = this.Preparation,
            Luck = this.Luck,
            Cybernetics = this.Cybernetics,
            Morality = this.Morality,
            Careerism = this.Careerism,
        };

        public override string Save() => String.Join("|",
            Planning, Preparation, Luck, Cybernetics, Morality, Careerism);

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Planning = int.Parse(save[0]);
            Preparation = int.Parse(save[1]);
            Luck = int.Parse(save[2]);
            Cybernetics = int.Parse(save[3]);
            Morality = int.Parse(save[4]);
            Careerism = int.Parse(save[5]);

            IsProtagonist = true;
        }
    }
}
