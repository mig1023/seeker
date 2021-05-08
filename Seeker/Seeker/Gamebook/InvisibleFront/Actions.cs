using System;
using System.Collections.Generic;
using System.Text;


namespace Seeker.Gamebook.InvisibleFront
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public override List<string> Status() => new List<string> {
            String.Format("Недовольство резидента: {0}", Character.Protagonist.Dissatisfaction),
            String.Format("Вербовка: {0}", Character.Protagonist.Recruitment)
        };

        public static bool CheckOnlyIf(string option)
        {
            string[] options = option.Split('|', ',');

            foreach (string oneOption in options)
            {
                if (oneOption.Contains(">") || oneOption.Contains("<"))
                {
                    int level = int.Parse(oneOption.Split('>', '=')[1]);

                    if (oneOption.Contains("НЕДОВОЛЬСТВО >") && (level >= Character.Protagonist.Dissatisfaction))
                        return false;
                    else if (oneOption.Contains("НЕДОВОЛЬСТВО <=") && (level < Character.Protagonist.Dissatisfaction))
                        return false;
                    else if (oneOption.Contains("ВЕРБОВКА >") && (level >= Character.Protagonist.Recruitment))
                        return false;
                    else if (oneOption.Contains("ВЕРБОВКА <=") && (level < Character.Protagonist.Recruitment))
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
