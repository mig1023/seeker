using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.ConquistadorDiary
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public override List<string> Status()
        {
            List<string> score = new List<string>();

            if (protagonist.Score != null)
                score.Add($"Баллы: {protagonist.Score}");

            return score;
        }
        
        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains(","))
            {
                bool isTriggered = option
                    .Split(',')
                    .Select(x => x.Trim().Trim('!'))
                    .Any(x => Game.Option.IsTriggered(x));

                bool negative = option.Contains("!");

                return negative ? !isTriggered : isTriggered;
            }
            else if (option.Contains(">"))
            {
                return protagonist.Score > Game.Services.LevelParse(option);
            }
            else if (option.Contains("<"))
            {
                return protagonist.Score < Game.Services.LevelParse(option);
            }
            else
            {
                return AvailabilityTrigger(option.Trim());
            }
        }

        public List<string> RollCoin()
        {
            bool coin = Game.Dice.Roll() % 2 == 0;

            if (coin)
                return new List<string> { $"BIG|GOOD|На монетке выпал ОРЁЛ" };
            else
                return new List<string> { $"BIG|BAD|На монетке выпала РЕШКА" };
        }
    }
}
