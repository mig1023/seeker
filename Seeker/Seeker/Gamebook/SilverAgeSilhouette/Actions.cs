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
                return option
                    .Split('|')
                    .Where(x => Game.Option.IsTriggered(x.Trim()))
                    .Count() > 0;
            }
            else if (option.Contains("ОЦЕНКА"))
            {
                bool logic = option.Contains("!");

                List<string> ratings = option
                    .Replace("!", String.Empty)
                    .Split(',')
                    .Select(x => x.Trim())
                    .ToList();

                foreach (string rating in ratings)
                {
                    int level = Game.Services.LevelParse(rating);

                    if (rating.Contains("ОЦЕНКА >=") && (level > protagonist.Rating))
                        return logic;

                    if (rating.Contains("ОЦЕНКА <") && (level <= protagonist.Rating))
                        return logic;
                }

                return !logic;
            }
            else
            {
                return AvailabilityTrigger(option);
            }
        }
    }
}
