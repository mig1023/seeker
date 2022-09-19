using System;

namespace Seeker.Gamebook.ChooseCthulhu
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();

        private int _initiation;
        public int Initiation
        {
            get => _initiation;
            set => _initiation = Game.Param.Setter(value, _initiation, this);
        }

        public override void Init()
        {
            base.Init();

            Initiation = 0;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            Initiation = this.Initiation,
        };

        public override string Save() => Initiation.ToString();

        public override void Load(string saveLine)
        {
            Initiation = int.Parse(saveLine);
            IsProtagonist = true;
        }
    }
}
