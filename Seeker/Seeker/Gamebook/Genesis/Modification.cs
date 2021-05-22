using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.Genesis
{
    class Modification : Prototypes.ModificationExtended, Abstract.IModification
    {
        public override void Do() => InnerDo(Character.Protagonist);
    }
}
