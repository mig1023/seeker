﻿using System.Collections.Generic;

namespace Seeker.Gamebook.BangkokSky
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Dictionary<string, string> StatNames { get; set; }

        public static List<string> TestLevelNames { get; set; }
    }
}
