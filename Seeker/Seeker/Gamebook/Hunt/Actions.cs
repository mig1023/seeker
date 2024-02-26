using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.Hunt
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public override List<string> Status() => new List<string>
        {
            $"Укушенные: {Character.Protagonist.Bitten}",
        };

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else
            {
                return AvailabilityTrigger(option);
            }
        }
    }
}
