using System;

namespace Seeker.Gamebook.Sheriff
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            base.Do(Character.Protagonist);
        }
    }
}
