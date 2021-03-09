using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.FaithfulSwordOfTheKing
{
    class Modification : Abstract.IModification
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string ValueString { get; set; }
        public bool Empty { get; set; }
        public bool Restore { get; set; }

        public void Do()
        {
            int currentValue = (int)Character.Protagonist.GetType().GetProperty(Name).GetValue(Character.Protagonist, null);

            if (Empty)
                currentValue = 0;

            else if (Restore)
                currentValue = (int)Character.Protagonist.GetType().GetProperty("Max" + Name).GetValue(Character.Protagonist, null);

            else
                currentValue += Value;

            Character.Protagonist.GetType().GetProperty(Name).SetValue(Character.Protagonist, currentValue);

            if (Name == "Healing")
                Game.Healing.Add(ValueString);

            if (Name == "Day")
            {
                if (Character.Protagonist.HadFoodToday <= 0)
                    Character.Protagonist.Strength -= 3;
                else
                    Character.Protagonist.HadFoodToday = 0;
            }
    
            if (Name.StartsWith("Max"))
            {
                Modification additionalMod = new Modification
                {
                    Name = Name.Remove(0, 3),
                    Value = this.Value,
                };

                additionalMod.Do();
            }
        }
    }
}
