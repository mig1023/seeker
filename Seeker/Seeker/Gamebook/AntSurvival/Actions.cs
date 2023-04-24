using System;

namespace Seeker.Gamebook.AntSurvival
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
    }
}
