using System;

namespace Seeker.Gamebook.ConquistadorDiary
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
    }
}
