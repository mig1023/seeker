using System;
using System.Linq;

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
            else if (Name == "WoundsByWeapons")
            {
                bool heavyWeapons = Character.Protagonist.Weapons
                    .Where(x => (x.Name == "Винчестер") || (x.Name == "Кольт"))
                    .Count() > 0;

                if (!heavyWeapons)
                {
                    Character.Protagonist.Wounds += 1;
                    Character.Protagonist.Time += 1;
                }
            }
            else if (Name == "TimeByHorse")
            {
                Character.Protagonist.Time += Game.Option.IsTriggered("Л") ? 1 : 2;
            }
            else
            {
                base.Do(Character.Protagonist);
            }
        }
            
    }
}
