using System;

namespace Seeker.Gamebook.TenementBuilding
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
    }
}
