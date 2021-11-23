using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.InvisibleFront
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public override List<string> Status() => new List<string> {
            String.Format("Недовольство резидента: {0}", protagonist.Dissatisfaction),
            String.Format("Вербовка: {0}", protagonist.Recruitment)
        };

        public override bool CheckOnlyIf(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else
            {
                string[] options = option.Split('|', ',');

                foreach (string oneOption in options)
                {
                    if (oneOption.Contains(">") || oneOption.Contains("<"))
                    {
                        int level = Game.Other.LevelParse(oneOption);

                        if (oneOption.Contains("НЕДОВОЛЬСТВО >") && (level >= protagonist.Dissatisfaction))
                        {
                            return false;
                        }
                        else if (oneOption.Contains("НЕДОВОЛЬСТВО <=") && (level < protagonist.Dissatisfaction))
                        {
                            return false;
                        }
                        else if (oneOption.Contains("ВЕРБОВКА >") && (level >= protagonist.Recruitment))
                        {
                            return false;
                        }
                        else if (oneOption.Contains("ВЕРБОВКА <=") && (level < protagonist.Recruitment))
                        {
                            return false;
                        }
                    }
                    else if (oneOption.Contains("!"))
                    {
                        if (Game.Option.IsTriggered(oneOption.Replace("!", String.Empty).Trim()))
                            return false;
                    }
                    else if (!Game.Option.IsTriggered(oneOption.Trim()))
                        return false;
                }

                return true;
            }
        }
    }
}
