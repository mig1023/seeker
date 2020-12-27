using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.InvisibleFront
{
    class Modification : Abstract.IModification
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string ValueString { get; set; }

        public void Do()
        {
            
            if (Name == "Trigger")
                Game.Option.Trigger(ValueString);

            else if (Name == "Meeting")
                Game.Option.Trigger(Game.Dice.Roll() > 3 ? "предатель" : "не предатель");

            else if (Name == "Apartment")
            {
                List<string> apartments = new List<string> { "один", "два", "три", "один", "два", "три" }; 
                Game.Option.Trigger(apartments[Game.Dice.Roll() - 1]);
            }
   
            else
            {
                int currentValue = (int)Character.Protagonist.GetType().GetProperty(Name).GetValue(Character.Protagonist, null);

                currentValue += Value;

                Character.Protagonist.GetType().GetProperty(Name).SetValue(Character.Protagonist, currentValue);
            }
        }
    }
}
