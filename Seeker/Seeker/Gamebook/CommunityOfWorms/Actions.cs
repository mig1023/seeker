﻿using System;

namespace Seeker.Gamebook.CommunityOfWorms
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
    }
}
