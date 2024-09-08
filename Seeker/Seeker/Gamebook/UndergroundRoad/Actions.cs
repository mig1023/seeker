using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.UndergroundRoad
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public override List<string> Status() =>
            new List<string> { $"Ранений: {Character.Protagonist.Wounds}" };

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains("НЕ РАНЕН ДВАЖДЫ"))
            {
                return Character.Protagonist.Wounds < 2;
            }
            else if (option.Contains("НЕ РАНЕН"))
            {
                return Character.Protagonist.Wounds < 1;
            }
            else if (option.Contains("РАНЕН ДВАЖДЫ"))
            {
                return Character.Protagonist.Wounds == 2;
            }
            else if (option.Contains("РАНЕН"))
            {
                return Character.Protagonist.Wounds > 0;
            }
            else
            {
                return base.Availability(option);
            }
        }
    }
}
