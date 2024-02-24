using System;

namespace Seeker.Gamebook.SilentSchool
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            int woundsBonus = (Game.Option.IsTriggered("Кайф") ? 2 : 1);

            bool trigger = DoByName("Trigger", () => Game.Option.Trigger(ValueString));
            bool change = DoByName("Change", () => Character.Protagonist.ChangeDecision = Value);
            bool weapon = DoByName("Weapon", () => Character.Protagonist.Weapon = ValueString);
            bool removeWeapon = DoByName("RemoveWeapon", () => Character.Protagonist.Weapon = String.Empty);

            bool byWeapon2 = DoByName("WoundsByWeapon2",
                () => Character.Protagonist.Life -= (String.IsNullOrEmpty(Character.Protagonist.Weapon) ? 4 : 2) * woundsBonus);

            bool byWeapon3 = DoByName("WoundsByWeapon3",
                () => Character.Protagonist.Life -= (Character.Protagonist.Weapon == "Гантеля" ? 3 : 6) * woundsBonus);

            bool byWeapon4 = DoByName("WoundsByWeapon4",
                () => Character.Protagonist.Life -= (Character.Protagonist.Weapon == "Флейта" ? 1 : 4) * woundsBonus);

            bool byBody = DoByName("WoundsByBody",
                () => Character.Protagonist.Life -= (Game.Option.IsTriggered("Толстяк") ? 4 : 6) * woundsBonus);

            if (trigger || change || weapon || removeWeapon || byWeapon2 || byWeapon3 || byWeapon4 || byBody)
            {
                return;
            }
            else if (Name == "WayBack")
            {
                Character.Protagonist.WayBack = Value;
            }
            else if (Name == "WoundsByWeapon")
            {
                if (Character.Protagonist.Weapon == "Черенок от швабры")
                {
                    Character.Protagonist.Life -= 4 * woundsBonus;
                }
                else
                {
                    Character.Protagonist.Life -= (String.IsNullOrEmpty(Character.Protagonist.Weapon) ? 8 : 6) * woundsBonus;
                }
            }
            else
            {
                int currentValue = GetProperty(Character.Protagonist, Name);
                currentValue += Value * ((woundsBonus > 1) && (Value < 1) ? woundsBonus : 1);

                SetProperty(Character.Protagonist, Name, currentValue);
            }
        }
    }
}
