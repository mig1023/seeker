using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.YounglingTournament
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.YounglingTournament.Character();
    }
}
