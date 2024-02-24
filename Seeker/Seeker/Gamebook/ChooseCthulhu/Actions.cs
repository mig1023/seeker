using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.ChooseCthulhu
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public override List<string> Status()
        {
            string cursed = Character.Protagonist.IsCursed() ? " (проклят)" : String.Empty;
            return new List<string> { $"Посвящение: {Character.Protagonist.Initiation}{cursed}" };
        }

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains(">"))
            {
                return Character.Protagonist.Initiation > Game.Services.LevelParse(option);
            }
            else if (option.Contains("<"))
            {
                return Character.Protagonist.Initiation < Game.Services.LevelParse(option);
            }
            else
            {
                return AvailabilityTrigger(option.Trim());
            }
        }
    }
}
