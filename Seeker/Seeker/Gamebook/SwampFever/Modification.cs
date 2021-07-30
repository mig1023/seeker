using System;

namespace Seeker.Gamebook.SwampFever
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public bool Multiplication { get; set; }
        public bool Division { get; set; }

        public override void Do()
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
