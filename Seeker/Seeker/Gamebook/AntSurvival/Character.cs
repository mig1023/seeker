using System;

namespace Seeker.Gamebook.AntSurvival
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
    }
}
