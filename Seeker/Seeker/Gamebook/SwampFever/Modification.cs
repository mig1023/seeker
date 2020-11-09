using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.SwampFever
{
    class Modification : Interfaces.IModification
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public bool Multiplication { get; set; }
        public bool Division { get; set; }

        public void Do()
        {
            if (Name == "Fury")
            {
                Character.Protagonist.Fury += Value;

                if (Character.Protagonist.Fury > 2)
                    Character.Protagonist.Fury = 2;

                if (Character.Protagonist.Fury < -2)
                    Character.Protagonist.Fury = -2;
            }
            else
            {
                int currentValue = (int)Character.Protagonist.GetType().GetProperty(Name).GetValue(Character.Protagonist, null);

                if (Multiplication)
                    currentValue *= Value;
                else if (Division)
                    currentValue /= Value;
                else
                    currentValue += Value;

                if ((Name == "Stigon") && (currentValue > 6))
                    currentValue = 6;

                Character.Protagonist.GetType().GetProperty(Name).SetValue(Character.Protagonist, currentValue);
            }
        }
    }
}
