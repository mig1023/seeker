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

            bool trigger = ModificationByName("Trigger", () => Game.Option.Trigger(Value.ToString()));
            bool change = ModificationByName("Change", () => hero.ChangeDecision = Value);
            bool weapon = ModificationByName("Weapon", () => hero.Weapon = ValueString);
            bool removeWeapon = ModificationByName("RemoveWeapon", () => hero.Weapon = String.Empty);
            bool woundsByWeapon2 = ModificationByName("WoundsByWeapon2", () => hero.Life -= (String.IsNullOrEmpty(hero.Weapon) ? 4 : 2) * woundsBonus);
            bool woundsByWeapon3 = ModificationByName("WoundsByWeapon3", () => hero.Life -= (hero.Weapon == "Гантеля" ? 3 : 6) * woundsBonus);
            bool woundsByWeapon4 = ModificationByName("WoundsByWeapon4", () => hero.Life -= (hero.Weapon == "Флейта" ? 1 : 4) * woundsBonus);
            bool woundsByBody = ModificationByName("WoundsByBody", () => hero.Life -= (Game.Data.Triggers.Contains("Толстяк") ? 4 : 6) * woundsBonus);

            if (trigger || change || weapon || removeWeapon || woundsByWeapon2 || woundsByWeapon3 || woundsByWeapon4 || woundsByBody)
            {
                // nothing to do here
            }

            else if (Name == "WoundsByWeapon")
            {
                if (hero.Weapon == "Черенок от швабры")
                    hero.Life -= 4 * woundsBonus;
                else
                    hero.Life -= (String.IsNullOrEmpty(hero.Weapon) ? 8 : 6) * woundsBonus;
            }

            else
            {
                int currentValue = (int)hero.GetType().GetProperty(Name).GetValue(hero, null);

                currentValue += Value * ((woundsBonus > 1) && (Value < 1) ? woundsBonus : 1);

                hero.GetType().GetProperty(Name).SetValue(hero, currentValue);
            }
        }
    }
}
