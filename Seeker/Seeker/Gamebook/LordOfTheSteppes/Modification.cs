using System;

namespace Seeker.Gamebook.LordOfTheSteppes
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Enum.TryParse(Name, out Character.SpecialTechniques value))
            {
                Character.Protagonist.SpecialTechnique.Add(value);
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
    }
}
