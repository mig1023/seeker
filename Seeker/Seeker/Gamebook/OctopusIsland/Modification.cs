using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.OctopusIsland
{
    class Modification : Abstract.IModification
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public void Do()
        {
            int currentValue = (int)Character.Protagonist.GetType().GetProperty(Name).GetValue(Character.Protagonist, null);

            currentValue += Value;

            if (currentValue < 0)
                currentValue = 0;

            Character.Protagonist.GetType().GetProperty(Name).SetValue(Character.Protagonist, currentValue);
        }
    }
}
