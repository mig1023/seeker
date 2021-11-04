using System;

namespace Seeker.Gamebook.ByTheWillOfRome
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do() => base.Do(Character.Protagonist);
    }
}
