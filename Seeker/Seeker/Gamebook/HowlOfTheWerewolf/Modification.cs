using System;

namespace Seeker.Gamebook.HowlOfTheWerewolf
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "WayBack")
            {
                Character.Protagonist.WayBack = Value;
            }
            else if (Name == "VanRichtenIsDead")
            {
                Character.Protagonist.VanRichten = 0;
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
    }
}
