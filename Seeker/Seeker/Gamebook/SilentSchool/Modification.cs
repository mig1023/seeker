using System;

namespace Seeker.Gamebook.SilentSchool
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        private static Character protogonist = Character.Protagonist;

        public override void Do()
        {
            int woundsBonus = (Game.Data.Triggers.Contains("Кайф") ? 2 : 1);

            bool trigger = ModificationByName("Trigger", () => Game.Option.Trigger(Value.ToString()));
            bool change = ModificationByName("Change", () => protogonist.ChangeDecision = Value);
            bool weapon = ModificationByName("Weapon", () => protogonist.Weapon = ValueString);
            bool removeWeapon = ModificationByName("RemoveWeapon", () => protogonist.Weapon = String.Empty);

            bool woundsByWeapon2 = ModificationByName("WoundsByWeapon2",
                () => protogonist.Life -= (String.IsNullOrEmpty(protogonist.Weapon) ? 4 : 2) * woundsBonus);
            bool woundsByWeapon3 = ModificationByName("WoundsByWeapon3",
                () => protogonist.Life -= (protogonist.Weapon == "Гантеля" ? 3 : 6) * woundsBonus);
            bool woundsByWeapon4 = ModificationByName("WoundsByWeapon4",
                () => protogonist.Life -= (protogonist.Weapon == "Флейта" ? 1 : 4) * woundsBonus);
            bool woundsByBody = ModificationByName("WoundsByBody",
                () => protogonist.Life -= (Game.Data.Triggers.Contains("Толстяк") ? 4 : 6) * woundsBonus);

            if (trigger || change || weapon || removeWeapon || woundsByWeapon2 || woundsByWeapon3 || woundsByWeapon4 || woundsByBody)
                return;

            else if (Name == "WoundsByWeapon")
            {
                if (protogonist.Weapon == "Черенок от швабры")
                    protogonist.Life -= 4 * woundsBonus;
                else
                    protogonist.Life -= (String.IsNullOrEmpty(protogonist.Weapon) ? 8 : 6) * woundsBonus;
            }

            else
            {
                int currentValue = (int)protogonist.GetType().GetProperty(Name).GetValue(protogonist, null);

                currentValue += Value * ((woundsBonus > 1) && (Value < 1) ? woundsBonus : 1);

                protogonist.GetType().GetProperty(Name).SetValue(protogonist, currentValue);
            }
        }
    }
}
