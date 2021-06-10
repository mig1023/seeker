using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.LegendsAlwaysLie
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public bool Init { get; set; }
        public int WizardWoundsPenalty { get; set; }
        public int ThrowerWoundsPenalty { get; set; }

        public override void Do()
        {
            bool injuries = ModificationByName("InjuriesBySpells", () => Actions.InjuriesBySpells());

            bool footwrapsDeadly = ModificationByName("FootwrapsNeedReplacingDeadly",
                () => Character.Protagonist.Hitpoints -= (Game.Data.Triggers.Contains("Legs") ? 4 : 2));

            bool footwrapsNeed = ModificationByName("FootwrapsNeedReplacing", () => Game.Option.Trigger("Legs"));

            if (injuries || footwrapsDeadly || footwrapsNeed)
            {
                // nothing to do here
            }
            else if (Name == "Offering")
            {
                if (Game.Data.Triggers.Contains("RingWithRuby"))
                    Game.Option.Trigger("RingWithRuby", remove: true);

                else if (Game.Data.Triggers.Contains("NecklaceWithEmerald"))
                    Game.Option.Trigger("NecklaceWithEmerald", remove: true);

                else
                    Character.Protagonist.Gold -= 10;
            }
            else
            {
                int currentValue = (int)Character.Protagonist.GetType().GetProperty(Name).GetValue(Character.Protagonist, null);

                if (Empty)
                    currentValue = 0;
                else
                {
                    currentValue += Value;

                    if (Init && (Name == "Hitpoints"))
                        currentValue = 30;

                    if ((WizardWoundsPenalty != 0) && (Character.Protagonist.Specialization == Character.SpecializationType.Wizard))
                        currentValue += WizardWoundsPenalty;

                    if ((ThrowerWoundsPenalty != 0) && (Character.Protagonist.Specialization == Character.SpecializationType.Thrower))
                        currentValue += ThrowerWoundsPenalty;
                }

                Character.Protagonist.GetType().GetProperty(Name).SetValue(Character.Protagonist, currentValue);
            }
        }
    }
}
