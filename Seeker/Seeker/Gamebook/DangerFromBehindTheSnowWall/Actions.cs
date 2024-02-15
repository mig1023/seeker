using System;

namespace Seeker.Gamebook.DangerFromBehindTheSnowWall
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        public static Actions GetInstance() => StaticInstance;
    }
}
