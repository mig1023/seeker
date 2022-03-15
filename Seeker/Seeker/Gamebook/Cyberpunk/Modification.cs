using System;

namespace Seeker.Gamebook.Cyberpunk
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do() => base.Do(Character.Protagonist);
    }
}
