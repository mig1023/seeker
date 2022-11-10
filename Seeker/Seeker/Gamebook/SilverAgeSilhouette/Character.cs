using System;

namespace Seeker.Gamebook.SilverAgeSilhouette
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
    }
}
