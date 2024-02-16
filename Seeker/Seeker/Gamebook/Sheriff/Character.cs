using System;

namespace Seeker.Gamebook.Sheriff
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public new static Character Protagonist = new Character();
        public new static Character GetInstance() => Protagonist;

        private int _whoosh;
        public int Whoosh
        {
            get => _whoosh;
            set => _whoosh = Game.Param.Setter(value);
        }

        public override void Init()
        {
            base.Init();

            Whoosh = 0;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            Whoosh = this.Whoosh,
        };

        public override string Save() => Whoosh.ToString();

        public override void Load(string saveLine)
        {
            Whoosh = int.Parse(saveLine);
            IsProtagonist = true;
        }
    }
}
