using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.ChooseCthulhu
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        public static Actions GetInstance() => StaticInstance;
        private static Character protagonist = Character.Protagonist;

        public override List<string> Status()
        {
            string cursed = Character.Protagonist.IsCursed() ? " (проклят)" : String.Empty;
            return new List<string> { $"Посвящение: {protagonist.Initiation}{cursed}" };
        }

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains(">"))
            {
                return protagonist.Initiation > Game.Services.LevelParse(option);
            }
            else if (option.Contains("<"))
            {
                return protagonist.Initiation < Game.Services.LevelParse(option);
            }
            else
            {
                return AvailabilityTrigger(option.Trim());
            }
        }
    }
}
