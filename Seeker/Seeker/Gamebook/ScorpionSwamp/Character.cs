using System;

namespace Seeker.Gamebook.ScorpionSwamp
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
    }
}
