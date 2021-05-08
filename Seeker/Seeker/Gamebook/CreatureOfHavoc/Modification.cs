using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.CreatureOfHavoc
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public bool Restore { get; set; }

        public override void Do()
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
