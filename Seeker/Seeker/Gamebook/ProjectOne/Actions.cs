using System;

namespace Seeker.Gamebook.ProjectOne
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
    }
}
