using System;

namespace Seeker.Gamebook.Sheriff
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
    }
}
