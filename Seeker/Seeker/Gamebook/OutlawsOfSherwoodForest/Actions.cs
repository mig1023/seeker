using System;

namespace Seeker.Gamebook.OutlawsOfSherwoodForest
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        public static Actions GetInstance() => StaticInstance;
    }
}
