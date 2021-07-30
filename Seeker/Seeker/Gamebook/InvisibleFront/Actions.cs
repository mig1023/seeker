using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.InvisibleFront
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protogonist = Character.Protagonist;

        public override List<string> Status() => new List<string> {
            String.Format("Недовольство резидента: {0}", protogonist.Dissatisfaction),
            String.Format("Вербовка: {0}", protogonist.Recruitment)
        };

        public override bool CheckOnlyIf(string option)
        {
            string[] options = option.Split('|', ',');

            foreach (string oneOption in options)
            {
                if (oneOption.Contains(">") || oneOption.Contains("<"))
                {
                    int level = Game.Other.LevelParse(oneOption);

                    if (oneOption.Contains("НЕДОВОЛЬСТВО >") && (level >= protogonist.Dissatisfaction))
                        return false;
                    else if (oneOption.Contains("НЕДОВОЛЬСТВО <=") && (level < protogonist.Dissatisfaction))
                        return false;
                    else if (oneOption.Contains("ВЕРБОВКА >") && (level >= protogonist.Recruitment))
                        return false;
                    else if (oneOption.Contains("ВЕРБОВКА <=") && (level < protogonist.Recruitment))
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
