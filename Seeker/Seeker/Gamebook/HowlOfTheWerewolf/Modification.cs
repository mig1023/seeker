using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.HowlOfTheWerewolf
{
    class Modification : Prototypes.ModificationExtended, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "Back")
                Character.Protagonist.WayBack = Value;

            else if (Name == "VanRichtenIsDead")
                Character.Protagonist.VanRichten = 0;

            else
                InnerDo(Character.Protagonist);
        }
    }
}
