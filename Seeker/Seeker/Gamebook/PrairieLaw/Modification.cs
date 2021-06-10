using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.PrairieLaw
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        delegate void ParamMod();

        public override void Do()
        {
            if (Name == "StrengthByPoison")
                ModificationChange("Противоядие", () => Character.Protagonist.Strength -= 10, not: true);

            else if (Name == "Skin")
                ModificationChange("Нож", () => Character.Protagonist.AnimalSkins.Add(ValueString));

            else
                InnerDo(Character.Protagonist);
        }

        private void ModificationChange(string trigger, ParamMod doModification, bool not = false)
        {
            if ((!not && Game.Data.Triggers.Contains(trigger)) || (not && !Game.Data.Triggers.Contains(trigger)))
                doModification();
        }
    }
}
