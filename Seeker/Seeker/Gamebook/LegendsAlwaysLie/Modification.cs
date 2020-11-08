using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.LegendsAlwaysLie
{
    class Modification : Interfaces.IModification
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string ValueString { get; set; }
        public bool Empty { get; set; }
        public bool Init { get; set; }

        public void Do()
        {
            int currentValue = (int)Character.Protagonist.GetType().GetProperty(Name).GetValue(Character.Protagonist, null);

            if (Empty)
                currentValue = 0;
            else
            {
                currentValue += Value;

                if (currentValue < 0)
                    currentValue = 0;
            }

            Character.Protagonist.GetType().GetProperty(Name).SetValue(Character.Protagonist, currentValue);
        }
    }
}
