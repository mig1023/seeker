using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.SilentSchool
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            Character hero = Character.Protagonist;

            int woundsBonus = (Game.Data.Triggers.Contains("Кайф") ? 2 : 1);

            if (Name == "Trigger")
                Game.Option.Trigger(Value.ToString());

            else if (Name == "Change")
                hero.ChangeDecision = Value;

            else if (Name == "Weapon")
                hero.Weapon = ValueString;

            else if (Name == "RemoveWeapon")
                hero.Weapon = String.Empty;

            else if (Name == "WoundsByWeapon")
            {
                if (hero.Weapon == "Черенок от швабры")
                    hero.Life -= 4 * woundsBonus;
                else
                    hero.Life -= (String.IsNullOrEmpty(hero.Weapon) ? 8 : 6) * woundsBonus;
            }

            else if (Name == "WoundsByWeapon2")
                hero.Life -= (String.IsNullOrEmpty(hero.Weapon) ? 4 : 2) * woundsBonus;

            else if (Name == "WoundsByWeapon3")
                hero.Life -= (hero.Weapon == "Гантеля" ? 3 : 6) * woundsBonus;

            else if (Name == "WoundsByWeapon4")
                hero.Life -= (hero.Weapon == "Флейта" ? 1 : 4)* woundsBonus;
            
            else if (Name == "WoundsByBody")
                hero.Life -= (Game.Data.Triggers.Contains("Толстяк") ? 4 : 6) * woundsBonus;

            else
            {
                int currentValue = (int)hero.GetType().GetProperty(Name).GetValue(hero, null);

                currentValue += Value * ((woundsBonus > 1) && (Value < 1) ? woundsBonus : 1);

                hero.GetType().GetProperty(Name).SetValue(hero, currentValue);
            }

            if (hero.Life < 0)
                hero.Life = 0;
        }
    }
}
