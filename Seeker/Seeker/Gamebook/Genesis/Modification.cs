using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.Genesis
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do() => InnerDo(Character.Protagonist);
    }
}
