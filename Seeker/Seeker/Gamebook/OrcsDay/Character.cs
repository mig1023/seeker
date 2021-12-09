using System;

namespace Seeker.Gamebook.OrcsDay
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
    }
}
