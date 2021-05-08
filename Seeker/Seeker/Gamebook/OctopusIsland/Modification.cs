using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.OctopusIsland
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            int currentValue = (int)Character.Protagonist.GetType().GetProperty(Name).GetValue(Character.Protagonist, null);

            currentValue += Value;

            if (currentValue < 0)
                currentValue = 0;

            Character.Protagonist.GetType().GetProperty(Name).SetValue(Character.Protagonist, currentValue);
        }
    }
}
