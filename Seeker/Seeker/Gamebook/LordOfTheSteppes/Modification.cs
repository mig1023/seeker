using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.LordOfTheSteppes
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public bool Restore { get; set; }
        public bool Empty { get; set; }

        public override void Do()
        {
            if (Enum.TryParse(Name, out Character.SpecialTechniques value))
            {
                Character.Protagonist.SpecialTechnique.Add(value);
                return;
            }
            else if (Name == "Healing")
            {
                Game.Healing.Add(ValueString);
                return;
            }

            int currentValue = (int)Character.Protagonist.GetType().GetProperty(Name).GetValue(Character.Protagonist, null);

            if (Restore)
                currentValue = (int)Character.Protagonist.GetType().GetProperty("Max" + Name).GetValue(Character.Protagonist, null);

            else if (Empty)
                currentValue = 0;

            if (Name.StartsWith("Max"))
            {
                string normalParam = Name.Remove(0, 3);

                int normalValue = (int)Character.Protagonist.GetType().GetProperty(normalParam).GetValue(Character.Protagonist, null);

                if ((normalValue + Value) > currentValue)
                    Character.Protagonist.GetType().GetProperty(Name).SetValue(Character.Protagonist, currentValue + Value);

                Character.Protagonist.GetType().GetProperty(normalParam).SetValue(Character.Protagonist, currentValue + Value);
            }
            else
            {
                currentValue += Value;

                Character.Protagonist.GetType().GetProperty(Name).SetValue(Character.Protagonist, currentValue);
            }
        }
    }
}
