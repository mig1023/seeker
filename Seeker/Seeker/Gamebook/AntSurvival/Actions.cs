using System;
using System.Linq;

namespace Seeker.Gamebook.AntSurvival
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

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
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains(">") || oneOption.Contains("="))
                    {
                        int level = Game.Services.LevelParse(option);

                        if (oneOption.Contains("ДАЙС =") && !protagonist.Dice[level])
                            return false;
                    }
                    else if (oneOption.Contains("!"))
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
