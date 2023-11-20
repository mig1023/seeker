using System;

namespace Seeker.Gamebook.MadameGuillotine
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
    }
}
