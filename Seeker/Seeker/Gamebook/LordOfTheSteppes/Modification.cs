using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.LordOfTheSteppes
{
    class Modification : Prototypes.ModificationExtended, Abstract.IModification
    {
        public override void Do()
        {
            if (Enum.TryParse(Name, out Character.SpecialTechniques value))
            {
                Character.Protagonist.SpecialTechnique.Add(value);
                return;
            }
            else
                InnerDo(Character.Protagonist);
        }
    }
}
