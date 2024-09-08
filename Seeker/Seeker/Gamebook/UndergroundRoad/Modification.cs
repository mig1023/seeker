using System;

namespace Seeker.Gamebook.UndergroundRoad
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do() =>
            base.Do(Character.Protagonist);
    }
}
