﻿using System.Collections.Generic;

namespace Seeker.Gamebook.TenementBuilding
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public new static Constants StaticInstance = new Constants();
        public new static Constants GetInstance() => StaticInstance;

        public static Dictionary<int, string> LuckList { get; set; }
    }
}
