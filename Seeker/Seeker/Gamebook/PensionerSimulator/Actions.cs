﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.PensionerSimulator
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static bool CheckOnlyIf(string option) => true;
    }
}
