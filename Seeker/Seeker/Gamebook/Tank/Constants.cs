﻿using System.Collections.Generic;

namespace Seeker.Gamebook.Tank
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Dictionary<string, string> CrewNames { get; set; }

        public static Dictionary<int, string> HitNames { get; set; }
    }
}
