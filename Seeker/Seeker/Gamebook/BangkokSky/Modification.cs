using System;

namespace Seeker.Gamebook.BangkokSky
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "HalfMoney")
            {
                Character.Protagonist.Money /= 2;
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
    }
}
