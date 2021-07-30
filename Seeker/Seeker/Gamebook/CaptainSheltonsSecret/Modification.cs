using System;

namespace Seeker.Gamebook.CaptainSheltonsSecret
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do() => InnerDo(Character.Protagonist);
    }
}
