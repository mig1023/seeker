using System;

namespace Seeker.Gamebook.ProjectOne
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
    }
}
