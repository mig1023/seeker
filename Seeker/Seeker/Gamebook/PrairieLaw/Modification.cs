using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.PrairieLaw
{
    class Modification : Prototypes.ModificationExtended, Abstract.IModification
    {
        public override void Do() => InnerDo(Character.Protagonist);
    }
}
