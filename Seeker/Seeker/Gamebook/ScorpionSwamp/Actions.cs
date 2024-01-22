using System;

namespace Seeker.Gamebook.ScorpionSwamp
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
    }
}
