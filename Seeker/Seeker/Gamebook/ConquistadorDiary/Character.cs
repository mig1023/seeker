using System;

namespace Seeker.Gamebook.ConquistadorDiary
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
    }
}
