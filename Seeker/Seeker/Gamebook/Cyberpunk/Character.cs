﻿using System;

namespace Seeker.Gamebook.Cyberpunk
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
    }
}
