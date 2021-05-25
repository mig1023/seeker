using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.CaptainSheltonsSecret
{
    class Modification : Prototypes.ModificationExtended, Abstract.IModification
    {
        public override void Do() => InnerDo(Character.Protagonist);
    }
}
