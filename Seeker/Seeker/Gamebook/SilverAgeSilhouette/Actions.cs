using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Seeker.Gamebook.SilverAgeSilhouette
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public List<string> Verse() =>
            Character.Protagonist.Verse.Select(x => Regex.Unescape(x)).ToList();

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option == "Нет издания или неудачник")
            {
                return AvailabilitySpecialTrigger();
            }
            else if (option.Contains("||"))
            {
                return AvailabilityExclusiveTrigger(option);
            }
            else if (option.Contains("|"))
            {
                return AvailabilityMultiplesTrigger(option);
            }
            else if (option.Contains("ОЦЕНКА"))
            {
                return AvailabilityRating(option);
            }
            else
            {
                return AvailabilityTrigger(option);
            }
        }

        private bool AvailabilityRating(string option)
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

                if (rating.Contains("ОЦЕНКА >=") && (level > Character.Protagonist.Rating))
                    return logic;

                if (rating.Contains("ОЦЕНКА <") && (level <= Character.Protagonist.Rating))
                    return logic;
            }

            return !logic;
        }

        private bool AvailabilityMultiplesTrigger(string option)
        {
            bool triggers = option
                .Split('|')
                .Where(x => Game.Option.IsTriggered(x.Trim()))
                .Count() > 0;

            return triggers;
        }
        
        private bool AvailabilityExclusiveTrigger(string option)
        {
            List<string> triggers = option
                .Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToList();

            return Game.Option.IsTriggered(triggers[0]) && !Game.Option.IsTriggered(triggers[1]);
        }

        private bool AvailabilitySpecialTrigger()
        {
            if (!Game.Option.IsTriggered("Собственное издание"))
            {
                return true;
            }
            else if (Game.Option.IsTriggered("Неудачник"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
