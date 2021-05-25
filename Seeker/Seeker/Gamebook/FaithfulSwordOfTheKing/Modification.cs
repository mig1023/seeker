using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.FaithfulSwordOfTheKing
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "Day")
            {
                if (Character.Protagonist.HadFoodToday <= 0)
                    Character.Protagonist.Strength -= 3;
                else
                    Character.Protagonist.HadFoodToday = 0;
            }

            InnerDo(Character.Protagonist);
        }
    }
}
