using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.SilentSchool
{
    class Modification : Abstract.IModification
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string ValueString { get; set; }

        public void Do()
        {
            Character hero = Character.Protagonist;

            if (Name == "Change")
                hero.ChangeDecision = Value;

            else if (Name == "Weapon")
                hero.Weapon = ValueString;

            else if (Name == "RemoveWeapon")
                hero.Weapon = String.Empty;

            else if (Name == "WoundsByWeapon")
            {
                if (hero.Weapon == "Черенок от швабры")
                    hero.Life -= 4;
                else
                    hero.Life -= (String.IsNullOrEmpty(hero.Weapon) ? 8 : 6);

                LifeNormalization();
            }

            else if (Name == "WoundsByWeapon2")
            {
                hero.Life -= (String.IsNullOrEmpty(hero.Weapon) ? 4 : 2);

                LifeNormalization();
            }

            else
            {
                int currentValue = (int)hero.GetType().GetProperty(Name).GetValue(hero, null);

                currentValue += Value;

                hero.GetType().GetProperty(Name).SetValue(hero, currentValue);
            }
        }

        private void LifeNormalization()
        {
            if (Character.Protagonist.Life < 0)
                Character.Protagonist.Life = 0;
        }
    }
}
