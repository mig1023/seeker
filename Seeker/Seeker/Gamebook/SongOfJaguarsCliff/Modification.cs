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
            else if (Name == "DollarsDuty")
            {
                if (Character.Protagonist.Dollars < 50)
                {
                    Game.Option.Trigger("DollarsDuty");
                }
                else
                {
                    Character.Protagonist.Dollars -= 50;
                }
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
            
    }
}
