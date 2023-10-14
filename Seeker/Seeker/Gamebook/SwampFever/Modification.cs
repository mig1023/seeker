using System;

namespace Seeker.Gamebook.SwampFever
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public bool Multiplication { get; set; }
        public bool Division { get; set; }

        public override void Do()
        {
            int currentValue = GetProperty(Character.Protagonist, Name);

            if (Multiplication)
            {
                currentValue *= Value;
            }
            else if (Division)
            {
                currentValue /= Value;
            }
            else
            {
                currentValue += Value;
            }

            SetProperty(Character.Protagonist, Name, currentValue);
        }
    }
}
