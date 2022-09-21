using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.ChooseCthulhu
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public override List<string> Status() =>
            new List<string> { String.Format("Посвящение: {0}", protagonist.Initiation) };

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
                return true;

            else if (option.Contains(">"))
                return protagonist.Initiation > Game.Services.LevelParse(option);

            else if (option.Contains("<"))
                return protagonist.Initiation < Game.Services.LevelParse(option);

            else
                return AvailabilityTrigger(option.Trim());
        }
    }
}
