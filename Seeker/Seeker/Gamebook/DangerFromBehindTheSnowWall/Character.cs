using System;

namespace Seeker.Gamebook.DangerFromBehindTheSnowWall
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
        public static Character GetInstance() => Protagonist;
    }
}
