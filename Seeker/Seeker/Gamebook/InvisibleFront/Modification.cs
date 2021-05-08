using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.InvisibleFront
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            
            if (Name == "Trigger")
                Game.Option.Trigger(ValueString);

            else if (Name == "Meeting")
                Game.Option.Trigger(Game.Dice.Roll() > 3 ? "предатель" : "не предатель");

            else if (Name == "Apartment")
                Game.Option.Trigger(Constants.GetApartments()[Game.Dice.Roll() - 1]);
   
            else
            {
                int currentValue = (int)Character.Protagonist.GetType().GetProperty(Name).GetValue(Character.Protagonist, null);

                currentValue += Value;

                Character.Protagonist.GetType().GetProperty(Name).SetValue(Character.Protagonist, currentValue);
            }
        }
    }
}
