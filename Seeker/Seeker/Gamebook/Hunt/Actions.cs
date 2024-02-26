using System;

namespace Seeker.Gamebook.Hunt
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
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
