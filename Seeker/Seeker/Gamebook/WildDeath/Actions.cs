using System;

namespace Seeker.Gamebook.WildDeath
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
    }
}
