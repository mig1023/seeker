using System;

namespace Seeker.Gamebook.VWeapons
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "Diary")
            {
                DoByTrigger("E", () => Character.Protagonist.Suspicions += 1);
            }
            else if (Name == "Shadow")
            {
                DoByTrigger("V", () => Character.Protagonist.Suspicions += 1);
            }
            else if (Name == "Davis")
            {
                DoByTrigger("D", () => Character.Protagonist.Suspicions += 2);
                DoByTrigger("D", () => Character.Protagonist.Suspicions += 1, not: true);
            }
            else if (Name == "Untrigger")
            {
                Game.Option.Trigger(ValueString, remove: true);
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
    }
}
