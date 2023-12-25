using System;

namespace Seeker.Gamebook.PresidentSimulator
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
    }
}
