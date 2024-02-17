using System;

namespace Seeker.Gamebook.DeathOfAntiquary
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public new static Character Protagonist = new Character();
        public new static Character GetInstance() => Protagonist;
    }
}
