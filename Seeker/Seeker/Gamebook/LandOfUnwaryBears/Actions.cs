using System;

namespace Seeker.Gamebook.LandOfUnwaryBears
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        public static Actions GetInstance() => StaticInstance;
    }
}
