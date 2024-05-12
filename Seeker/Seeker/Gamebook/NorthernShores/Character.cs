using System;

namespace Seeker.Gamebook.NorthernShores
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist { get; set; }
        public override void Set(object character) =>
            Protagonist = (Character)character;

        private int _heat;
        public int Heat
        {
            get => _heat;
            set => _heat = Game.Param.Setter(value, _heat, this);
        }

        public override void Init()
        {
            base.Init();
            Heat = 0;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            Heat = this.Heat,
        };

        public override string Save() => Heat.ToString();

        public override void Load(string saveLine)
        {
            Heat = int.Parse(saveLine);
            IsProtagonist = true;
        }
    }
}
