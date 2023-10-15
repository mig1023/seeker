using System;

namespace Seeker.Gamebook.DinosaurIsland
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
    }
}
