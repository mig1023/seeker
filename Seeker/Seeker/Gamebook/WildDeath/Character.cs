using System;

namespace Seeker.Gamebook.WildDeath
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
        public static Character GetInstance() => Protagonist;
    }
}
