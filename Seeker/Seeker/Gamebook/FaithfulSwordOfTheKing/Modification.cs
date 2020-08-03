using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.FaithfulSwordOfTheKing
{
    class Modification : Interfaces.IModification
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string ValueString { get; set; }

        public void Do()
        {
            double currentValue = (double)Character.Protagonist.GetType().GetProperty(Name).GetValue(Character.Protagonist, null);

            currentValue += Value;

            if (currentValue < 0)
                currentValue = 0;

            Character.Protagonist.GetType().GetProperty(Name).SetValue(Character.Protagonist, currentValue);
        }
    }
}
