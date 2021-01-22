using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.CreatureOfHavoc
{
    class Modification : Abstract.IModification
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public void Do()
        {
            if (Name == "EnduranceRestore")
                Character.Protagonist.Endurance = Character.Protagonist.MaxEndurance;

            else if (Name == "MasteryRestore")
                Character.Protagonist.Mastery = Character.Protagonist.MaxMastery;

            else if (Name == "LuckRestore")
                Character.Protagonist.Luck = Character.Protagonist.MaxLuck;

            else
            {
                int currentValue = (int)Character.Protagonist.GetType().GetProperty(Name).GetValue(Character.Protagonist, null);

                currentValue += Value;

                Character.Protagonist.GetType().GetProperty(Name).SetValue(Character.Protagonist, currentValue);
            }
        }
    }
}
