﻿using System;

namespace Seeker.Gamebook.CommunityOfWorms
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
    }
}
