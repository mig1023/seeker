using System;

namespace Seeker.Gamebook.MadameGuillotine
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
    }
}
