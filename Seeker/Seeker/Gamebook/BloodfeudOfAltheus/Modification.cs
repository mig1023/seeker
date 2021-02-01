using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.BloodfeudOfAltheus
{
    class Modification : Abstract.IModification
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string ValueString { get; set; }

        public void Do()
        {
            if (!String.IsNullOrEmpty(ValueString) && (Name == "Patron"))
                Character.Protagonist.Patron = ValueString;

            else if (!String.IsNullOrEmpty(ValueString) && (Name == "Weapon"))
                Character.Protagonist.AddWeapons(ValueString);

            else
            {
                int currentValue = (int)Character.Protagonist.GetType().GetProperty(Name).GetValue(Character.Protagonist, null);

                currentValue += Value;

                Character.Protagonist.GetType().GetProperty(Name).SetValue(Character.Protagonist, currentValue);
            }
        }
    }
}
