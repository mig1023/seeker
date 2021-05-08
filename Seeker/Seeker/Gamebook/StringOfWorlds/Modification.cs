using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.StringOfWorlds
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "StrengthRestore")
                Character.Protagonist.Strength = Character.Protagonist.MaxStrength;

            else
            {
                int currentValue = (int)Character.Protagonist.GetType().GetProperty(Name).GetValue(Character.Protagonist, null);

                currentValue += Value;

                Character.Protagonist.GetType().GetProperty(Name).SetValue(Character.Protagonist, currentValue);
            }
        }
    }
}
