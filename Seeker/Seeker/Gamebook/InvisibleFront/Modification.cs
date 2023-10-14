using System;

namespace Seeker.Gamebook.InvisibleFront
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "Meeting")
            {
                Game.Option.Trigger(Game.Dice.Roll() > 3 ? "предатель" : "не предатель");
            }
            else if (Name == "Apartment")
            {
                Game.Option.Trigger(Constants.GetApartments[Game.Dice.Roll() - 1]);
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
    }
}
