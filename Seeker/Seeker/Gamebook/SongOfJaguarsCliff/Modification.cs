using System;

namespace Seeker.Gamebook.SongOfJaguarsCliff
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do() =>
            base.Do(Character.Protagonist);
    }
}
