using System;

namespace Seeker.Gamebook.Damanskiy
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
    }
}
