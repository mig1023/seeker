using System;

namespace Seeker.Gamebook.VWeapons
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        private static Character protagonist = Character.Protagonist;

        public override void Do()
        {
            if (Name == "Diary")
            {
                DoByTrigger("E", () => protagonist.Suspicions += 1);
            }
            else if (Name == "Shadow")
            {
                DoByTrigger("V", () => protagonist.Suspicions += 1);
            }
            else if (Name == "Davis")
            {
                DoByTrigger("D", () => protagonist.Suspicions += 2);
                DoByTrigger("D", () => protagonist.Suspicions += 1, not: true);
            }
            else if (Name == "Untrigger")
            {
                Game.Option.Trigger(ValueString, remove: true);
            }
            else
            {
                base.Do(protagonist);
            }
        }
    }
}
