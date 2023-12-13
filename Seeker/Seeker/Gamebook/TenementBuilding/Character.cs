using System;

namespace Seeker.Gamebook.TenementBuilding
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
    }
}
