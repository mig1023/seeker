using System;

namespace Seeker.Gamebook.StrikeBack
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
    }
}
