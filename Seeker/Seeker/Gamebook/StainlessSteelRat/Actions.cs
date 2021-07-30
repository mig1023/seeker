using System;

namespace Seeker.Gamebook.StainlessSteelRat
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
    }
}
