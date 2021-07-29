using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.YounglingTournament
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
    }
}
