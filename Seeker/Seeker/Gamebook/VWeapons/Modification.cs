using System;

namespace Seeker.Gamebook.VWeapons
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        private static Character protogonist = Character.Protagonist;

        public override void Do()
        {
            if (Name == "Diary")
                DoByTrigger("E", () => protogonist.Suspicions += 1);

            else if (Name == "Shadow")
                DoByTrigger("V", () => protogonist.Suspicions += 1);
            
            else if (Name == "Davis")
            {
                DoByTrigger("D", () => protogonist.Suspicions += 2);
                DoByTrigger("D", () => protogonist.Suspicions += 1, not: true);
            }

            else if (Name == "RemoveTrigger")
                Game.Option.Trigger(ValueString, remove: true);

            else
                base.Do(protogonist);
        }
    }
}
