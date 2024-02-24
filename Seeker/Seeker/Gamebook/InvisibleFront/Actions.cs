using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.InvisibleFront
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
         public override List<string> Status() => new List<string> {
            $"Недовольство резидента: {Character.Protagonist.Dissatisfaction}",
            $"Вербовка: {Character.Protagonist.Recruitment}",
        };

        public override bool Availability(string option)
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
                        int level = Game.Services.LevelParse(oneOption);

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
