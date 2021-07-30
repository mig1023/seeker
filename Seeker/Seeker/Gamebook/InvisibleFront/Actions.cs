using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.InvisibleFront
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();

        public override List<string> Status() => new List<string> {
            String.Format("Недовольство резидента: {0}", Character.Protagonist.Dissatisfaction),
            String.Format("Вербовка: {0}", Character.Protagonist.Recruitment)
        };

        public override bool CheckOnlyIf(string option)
        {
            string[] options = option.Split('|', ',');
            Character hero = Character.Protagonist;

            foreach (string oneOption in options)
            {
                if (oneOption.Contains(">") || oneOption.Contains("<"))
                {
                    int level = Game.Other.LevelParse(oneOption);

                    if (oneOption.Contains("НЕДОВОЛЬСТВО >") && (level >= hero.Dissatisfaction))
                        return false;
                    else if (oneOption.Contains("НЕДОВОЛЬСТВО <=") && (level < hero.Dissatisfaction))
                        return false;
                    else if (oneOption.Contains("ВЕРБОВКА >") && (level >= hero.Recruitment))
                        return false;
                    else if (oneOption.Contains("ВЕРБОВКА <=") && (level < hero.Recruitment))
                        return false;
                }
                else if (oneOption.Contains("!"))
                {
                    if (Game.Data.Triggers.Contains(oneOption.Replace("!", String.Empty).Trim()))
                        return false;
                }
                else if (!Game.Data.Triggers.Contains(oneOption.Trim()))
                    return false;
            }

            return true;
        }
    }
}
