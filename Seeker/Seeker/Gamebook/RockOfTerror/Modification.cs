using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.RockOfTerror
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

            int injuryModificator = ((Name == "Time") && (Character.Protagonist.Injury > 0) ? 2 : 1);

            currentValue += Value * injuryModificator;

            Character.Protagonist.GetType().GetProperty(Name).SetValue(Character.Protagonist, currentValue);
        }
    }
}
