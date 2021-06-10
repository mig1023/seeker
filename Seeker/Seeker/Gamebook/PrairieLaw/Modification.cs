using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.PrairieLaw
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "StrengthByPoison")
                ModificationByTrigger("Противоядие", () => Character.Protagonist.Strength -= 10, not: true);

            else if (Name == "Skin")
                ModificationByTrigger("Нож", () => Character.Protagonist.AnimalSkins.Add(ValueString));

            else
                InnerDo(Character.Protagonist);
        }
    }
}
