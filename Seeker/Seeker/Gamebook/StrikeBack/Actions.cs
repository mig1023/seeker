using System;

namespace Seeker.Gamebook.StrikeBack
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
    }
}
