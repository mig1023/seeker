using System;

namespace Seeker.Gamebook.OctopusIsland
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do() => InnerDo(Character.Protagonist);
    }
}
