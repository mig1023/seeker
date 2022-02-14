using System;

namespace Seeker.Gamebook.Sheriff
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;
    }
}
