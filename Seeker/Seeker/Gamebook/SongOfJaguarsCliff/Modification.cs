using System;

namespace Seeker.Gamebook.SongOfJaguarsCliff
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "Weapons")
            {
                Character.Protagonist.Weapons.Add(new Weapon(ValueString));
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
            
    }
}
