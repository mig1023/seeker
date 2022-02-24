using System;

namespace Seeker.Gamebook.Moonrunner
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();

        public override void Init()
        {
            base.Init();
        }

        public Character Clone() => new Character()
        {
            IsProtagonist = this.IsProtagonist,
            Name = this.Name,
        };

        public override string Save() => String.Empty;

        public override void Load(string saveLine)
        {
            IsProtagonist = true;
        }
    }
}
