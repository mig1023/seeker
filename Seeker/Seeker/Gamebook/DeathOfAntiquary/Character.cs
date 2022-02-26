using System;

namespace Seeker.Gamebook.DeathOfAntiquary
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();

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
