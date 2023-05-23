using System;

namespace Seeker.Gamebook.ChooseCthulhu
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "CursedForever")
                Character.Protagonist.Cursed();
            else
                base.Do(Character.Protagonist);
        }
    }
}
