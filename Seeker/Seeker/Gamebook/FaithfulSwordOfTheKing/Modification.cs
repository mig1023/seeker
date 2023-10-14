using System;

namespace Seeker.Gamebook.FaithfulSwordOfTheKing
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            Character protagonist = Character.Protagonist;

            if (Name == "Day")
            {
                if (protagonist.HadFoodToday <= 0)
                {
                    protagonist.Strength -= 3;
                }
                else
                {
                    protagonist.HadFoodToday = 0;
                }
            }

            base.Do(protagonist);
        }
    }
}
