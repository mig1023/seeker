using System;

namespace Seeker.Gamebook.DeathOfAntiquary
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();

        public override bool Availability(string option) => AvailabilityTrigger(option);
    }
}
