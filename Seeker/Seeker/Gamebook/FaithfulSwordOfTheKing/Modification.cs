using System;

namespace Seeker.Gamebook.FaithfulSwordOfTheKing
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            Character hero = Character.Protagonist;

            if (Name == "Day")
            {
                if (hero.HadFoodToday <= 0)
                    hero.Strength -= 3;
                else
                    hero.HadFoodToday = 0;
            }

            InnerDo(hero);
        }
    }
}
