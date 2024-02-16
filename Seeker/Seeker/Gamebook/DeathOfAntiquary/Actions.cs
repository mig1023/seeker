using System;

namespace Seeker.Gamebook.DeathOfAntiquary
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public new static Actions StaticInstance = new Actions();
        public new static Actions GetInstance() => StaticInstance;

        public override bool Availability(string option) =>
            AvailabilityTrigger(option);
    }
}
