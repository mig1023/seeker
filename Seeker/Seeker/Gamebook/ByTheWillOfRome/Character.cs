using System;

namespace Seeker.Gamebook.ByTheWillOfRome
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
    }
}
