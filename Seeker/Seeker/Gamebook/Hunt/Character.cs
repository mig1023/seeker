using System;

namespace Seeker.Gamebook.Hunt
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist { get; set; }
        public override void Set(object character) =>
            Protagonist = (Character)character;

        private int _bitten;
        public int Bitten
        {
            get => _bitten;
            set => _bitten = Game.Param.Setter(value, _bitten, this);
        }

        private int _cycle;
        public int Cycle
        {
            get => _cycle;
            set => _cycle = Game.Param.Setter(value, _cycle, this);
        }

        public override void Init()
        {
            base.Init();

            Bitten = 0;
            Cycle = 0;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Bitten = this.Bitten,
            Cycle = this.Cycle,
        };

        public override string Save() =>
            String.Join("|", Bitten, Cycle);

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Bitten = int.Parse(save[0]);
            Cycle = int.Parse(save[1]);

            IsProtagonist = true;
        }
    }
}
