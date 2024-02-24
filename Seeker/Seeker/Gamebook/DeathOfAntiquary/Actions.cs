using System;

namespace Seeker.Gamebook.DeathOfAntiquary
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public override bool Availability(string option) =>
            AvailabilityTrigger(option);
    }
}
