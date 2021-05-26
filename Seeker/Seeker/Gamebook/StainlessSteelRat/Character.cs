using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.StainlessSteelRat
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.StainlessSteelRat.Character();
    }
}
