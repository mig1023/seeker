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

            else if (Name == "Shadow")
                ModificationByTrigger("V", () => Character.Protagonist.Suspicions += 1);
            
            else if (Name == "Davis")
            {
                ModificationByTrigger("D", () => Character.Protagonist.Suspicions += 2);
                ModificationByTrigger("D", () => Character.Protagonist.Suspicions += 1, not: true);
            }

            else if (Name == "RemoveTrigger")
                Game.Option.Trigger(ValueString, remove: true);

            else
                InnerDo(Character.Protagonist);
        }
    }
}
