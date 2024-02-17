using System;

namespace Seeker.Gamebook.PensionerSimulator
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public new static Character Protagonist = new Character();
        public new static Character GetInstance() => Protagonist;
    }
}
