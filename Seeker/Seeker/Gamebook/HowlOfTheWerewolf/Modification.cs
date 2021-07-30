using System;

namespace Seeker.Gamebook.HowlOfTheWerewolf
{
    class Modification : Prototypes.Modification, Abstract.IModification
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
