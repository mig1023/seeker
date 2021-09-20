using System;

namespace Seeker.Gamebook.GoingToLaughter
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
    }
}
