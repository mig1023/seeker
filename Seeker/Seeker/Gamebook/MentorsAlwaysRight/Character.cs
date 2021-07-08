using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.MentorsAlwaysRight
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.MentorsAlwaysRight.Character();
    }
}
