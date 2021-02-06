using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.SwampFever
{
    class Modification : Abstract.IModification
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public bool Multiplication { get; set; }
        public bool Division { get; set; }

        public void Do()
        {
            int currentValue = (int)Character.Protagonist.GetType().GetProperty(Name).GetValue(Character.Protagonist, null);

            if (Multiplication)
                currentValue *= Value;
            else if (Division)
                currentValue /= Value;
            else
                currentValue += Value;

            Character.Protagonist.GetType().GetProperty(Name).SetValue(Character.Protagonist, currentValue);
        }
    }
}
