using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.LordOfTheSteppes
{
    class Modification : Abstract.IModification
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public bool Restore { get; set; }

        public void Do()
        {
            int currentValue = (int)Character.Protagonist.GetType().GetProperty(Name).GetValue(Character.Protagonist, null);

            if (Restore)
                currentValue = (int)Character.Protagonist.GetType().GetProperty("Max" + Name).GetValue(Character.Protagonist, null);
            else
                currentValue += Value;

            Character.Protagonist.GetType().GetProperty(Name).SetValue(Character.Protagonist, currentValue);
        }
    }
}
