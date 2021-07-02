using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.VWeapons
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "Diary")
                ModificationByTrigger("E", () => Character.Protagonist.Suspicions += 1);

            else
                InnerDo(Character.Protagonist);
        }
    }
}
