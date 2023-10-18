using System;

namespace Seeker.Gamebook.AlamutFortress
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
    }
}
