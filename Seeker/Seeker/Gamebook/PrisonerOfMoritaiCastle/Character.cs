using System;

namespace Seeker.Gamebook.PrisonerOfMoritaiCastle
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist { get; set; }
        public override void Set(object character) =>
            Protagonist = (Character)character;

        private int _hitpoints;
        public int Hitpoints
        {
            get => _hitpoints;
            set => _hitpoints = Game.Param.Setter(value, _hitpoints, this);
        }

        public override void Init()
        {
            base.Init();

            Hitpoints = 5;
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
            Hitpoints = this.Hitpoints,
        };

        public override string Save() =>
            Hitpoints.ToString();

        public override void Load(string saveLine)
        {
            Hitpoints = int.Parse(saveLine);
            IsProtagonist = true;
        }
    }
}
