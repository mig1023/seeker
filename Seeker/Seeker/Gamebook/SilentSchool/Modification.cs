using System;

namespace Seeker.Gamebook.SilentSchool
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        private static Character protagonist = Character.Protagonist;

        public override void Do()
        {
            int woundsBonus = (Game.Data.Triggers.Contains("Кайф") ? 2 : 1);

            bool trigger = DoByName("Trigger", () => Game.Option.Trigger(ValueString));
            bool change = DoByName("Change", () => protagonist.ChangeDecision = Value);
            bool weapon = DoByName("Weapon", () => protagonist.Weapon = ValueString);
            bool removeWeapon = DoByName("RemoveWeapon", () => protagonist.Weapon = String.Empty);

            bool woundsByWeapon2 = DoByName("WoundsByWeapon2",
                () => protagonist.Life -= (String.IsNullOrEmpty(protagonist.Weapon) ? 4 : 2) * woundsBonus);
            bool woundsByWeapon3 = DoByName("WoundsByWeapon3",
                () => protagonist.Life -= (protagonist.Weapon == "Гантеля" ? 3 : 6) * woundsBonus);
            bool woundsByWeapon4 = DoByName("WoundsByWeapon4",
                () => protagonist.Life -= (protagonist.Weapon == "Флейта" ? 1 : 4) * woundsBonus);
            bool woundsByBody = DoByName("WoundsByBody",
                () => protagonist.Life -= (Game.Data.Triggers.Contains("Толстяк") ? 4 : 6) * woundsBonus);

            if (trigger || change || weapon || removeWeapon || woundsByWeapon2 || woundsByWeapon3 || woundsByWeapon4 || woundsByBody)
                return;

            else if (Name == "Back")
                Character.Protagonist.WayBack = Value;

            else if (Name == "WoundsByWeapon")
            {
                if (protagonist.Weapon == "Черенок от швабры")
                    protagonist.Life -= 4 * woundsBonus;
                else
                    protagonist.Life -= (String.IsNullOrEmpty(protagonist.Weapon) ? 8 : 6) * woundsBonus;
            }

            else
            {
                int currentValue = GetProperty(protagonist, Name);

                currentValue += Value * ((woundsBonus > 1) && (Value < 1) ? woundsBonus : 1);

                SetProperty(protagonist, Name, currentValue);
            }
        }
    }
}
