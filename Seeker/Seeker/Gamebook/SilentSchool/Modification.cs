using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.SilentSchool
{
    class Modification : Abstract.IModification
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public void Do()
        {
            if (Name == "Change")
                Character.Protagonist.ChangeDecision = Value;

            else
            {
                int currentValue = (int)Character.Protagonist.GetType().GetProperty(Name).GetValue(Character.Protagonist, null);

                currentValue += Value;

                Character.Protagonist.GetType().GetProperty(Name).SetValue(Character.Protagonist, currentValue);
            }
        }
    }
}
