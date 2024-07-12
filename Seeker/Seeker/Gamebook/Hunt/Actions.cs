using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.Hunt
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public override List<string> Status()
        {
            string zombies = new string('x', Character.Protagonist.Bitten).Replace("x", "🧟");
            string bitten = String.IsNullOrEmpty(zombies) ? "ни одного" : zombies;
            return new List<string>{ $"Укушенные: {bitten}" };
        }

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
            else if (!option.Contains(","))
            {
                if (option.Contains("ЦИКЛ >"))
                {
                    return Character.Protagonist.Cycle > Game.Services.LevelParse(option);
                }
                else if (option.Contains("УКУШЕННЫЕ >="))
                {
                    return Character.Protagonist.Bitten >= Game.Services.LevelParse(option);
                }
                else if (option.Contains("УКУШЕННЫЕ <"))
                {
                    return Character.Protagonist.Bitten < Game.Services.LevelParse(option);
                }
                else
                {
                    return AvailabilityTrigger(option);
                }
            }
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains("!"))
                    {
                        if (Game.Option.IsTriggered(oneOption.Replace("!", String.Empty).Trim()))
                            return false;
                    }
                    else if (!Game.Option.IsTriggered(oneOption.Trim()))
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}
