using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.UndergroundRoad
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public override List<string> Status() =>
            new List<string> { $"Ранений: {Character.Protagonist.Wounds}" };
    }
}
