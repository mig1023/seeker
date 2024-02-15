using System;

namespace Seeker.Gamebook.LandOfUnwaryBears
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
        public static Character GetInstance() => Protagonist;
    }
}
