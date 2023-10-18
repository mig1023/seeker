using System;

namespace Seeker.Gamebook.AlamutFortress
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
    }
}
