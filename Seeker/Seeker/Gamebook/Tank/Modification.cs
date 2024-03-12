using System;

namespace Seeker.Gamebook.Tank
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "NewTankCrew")
            {
                Character.Protagonist.Driver = 4;
                Character.Protagonist.Shooter = 2;
                Character.Protagonist.Gunner = 3;
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
    }
}
