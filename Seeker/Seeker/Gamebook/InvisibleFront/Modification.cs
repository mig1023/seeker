using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.InvisibleFront
{
    class Modification : Prototypes.ModificationExtended, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "Meeting")
                Game.Option.Trigger(Game.Dice.Roll() > 3 ? "предатель" : "не предатель");

            else if (Name == "Apartment")
                Game.Option.Trigger(Constants.GetApartments()[Game.Dice.Roll() - 1]);

            else
                InnerDo(Character.Protagonist);
        }
    }
}
