using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.SilverAgeSilhouette
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public List<string> Verse() => protagonist.Verse;

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains("|"))
            {
                return option.Split('|').Where(x => Game.Option.IsTriggered(x.Trim())).Count() > 0;
            }
            else if (option.Contains("ОЦЕНКА"))
            {
                int level = Game.Services.LevelParse(option);

                if (option.Contains("ОЦЕНКА >=") && (level > protagonist.Rating))
                    return false;

                if (option.Contains("ОЦЕНКА <") && (level <= protagonist.Rating))
                    return false;

                return true;
            }
            else
            {
                return AvailabilityTrigger(option);
            }
        }
    }
}
