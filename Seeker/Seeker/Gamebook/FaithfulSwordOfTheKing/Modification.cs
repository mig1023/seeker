using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.FaithfulSwordOfTheKing
{
    class Modification : Abstract.IModification
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public bool Empty { get; set; }

        public void Do()
        {
            if (Name == "StrengthRestore")
                Character.Protagonist.Strength = Character.Protagonist.MaxStrength;

            else
            {
                int currentValue = (int)Character.Protagonist.GetType().GetProperty(Name).GetValue(Character.Protagonist, null);

                currentValue += Value;

                if (Empty)
                    currentValue = 0;

                Character.Protagonist.GetType().GetProperty(Name).SetValue(Character.Protagonist, currentValue);

                if (Name == "Day")
                {
                    if (Character.Protagonist.HadFoodToday <= 0)
                    {
                        Character.Protagonist.Strength -= 3;

                        if (Character.Protagonist.Strength < 0)
                            Character.Protagonist.Strength = 0;
                    }
                    else
                        Character.Protagonist.HadFoodToday = 0;
                }
            }
        }
    }
}
