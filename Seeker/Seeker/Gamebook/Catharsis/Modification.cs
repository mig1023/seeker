using System;

namespace Seeker.Gamebook.Catharsis
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do() => InnerDo(Character.Protagonist);
    }
}
