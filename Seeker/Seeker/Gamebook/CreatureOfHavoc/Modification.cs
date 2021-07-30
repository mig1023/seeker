using System;

namespace Seeker.Gamebook.CreatureOfHavoc
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do() => InnerDo(Character.Protagonist);
    }
}
