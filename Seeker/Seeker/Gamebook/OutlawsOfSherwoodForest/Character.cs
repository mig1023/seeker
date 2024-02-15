using System;

namespace Seeker.Gamebook.OutlawsOfSherwoodForest
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
        public static Character GetInstance() => Protagonist;
    }
}
