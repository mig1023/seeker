using System;

namespace Seeker.Gamebook.ColdHeartOfDalrok
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "StrengthRestore")
            {
                Character.Protagonist.Strength = Character.Protagonist.MaxStrength;
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
    }
}
