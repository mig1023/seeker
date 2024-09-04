using System;

namespace Seeker.Gamebook.SeaTales
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "ReInit")
            {
                Character.Protagonist.Init();
            }
            else if (Name == "Redirect")
            {
                // nothing to do here
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
    }
}
