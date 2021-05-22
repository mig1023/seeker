using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.StringOfWorlds
{
    class Modification : Prototypes.ModificationExtended, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "StrengthRestore")
                Character.Protagonist.Strength = Character.Protagonist.MaxStrength;

            else
                InnerDo(Character.Protagonist);
        }
    }
}
