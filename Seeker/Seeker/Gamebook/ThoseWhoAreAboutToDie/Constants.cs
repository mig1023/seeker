﻿using System.Collections.Generic;

namespace Seeker.Gamebook.ThoseWhoAreAboutToDie
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();
        public static Constants GetInstance() => StaticInstance;
    }
}
