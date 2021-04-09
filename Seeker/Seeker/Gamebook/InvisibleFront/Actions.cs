using System;
using System.Collections.Generic;
using System.Text;


namespace Seeker.Gamebook.InvisibleFront
{
    class Actions : Abstract.IActions
    {
        public string ActionName { get; set; }
        public string ButtonName { get; set; }
        public string Aftertext { get; set; }
        public string Trigger { get; set; }

        public List<string> Do(out bool reload, string action = "", bool trigger = false)
        {
            if (trigger)
                Game.Option.Trigger(Trigger);

            string actionName = (String.IsNullOrEmpty(action) ? ActionName : action);
            List<string> actionResult = typeof(Actions).GetMethod(actionName).Invoke(this, new object[] { }) as List<string>;

            reload = (actionResult.Count >= 1) && (actionResult[0] == "RELOAD");

            return actionResult;
        }

        public List<string> Status()
        {
            return new List<string> {
                String.Format("Недовольство резидента: {0}", Character.Protagonist.Dissatisfaction),
                String.Format("Вербовка: {0}", Character.Protagonist.Recruitment)
            };
        }

        public List<string> AdditionalStatus() => null;

        public List<string> StaticButtons() => new List<string> { };

        public bool StaticAction(string action) => false;

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = String.Empty;

            return false;
        }

        public bool IsButtonEnabled() => true;

        public static bool CheckOnlyIf(string option)
        {
            string[] options = option.Split('|', ',');

            foreach (string oneOption in options)
            {
                if (oneOption.Contains(">") || oneOption.Contains("<"))
                {
                    if (oneOption.Contains("НЕДОВОЛЬСТВО >") && (int.Parse(oneOption.Split('>')[1]) >= Character.Protagonist.Dissatisfaction))
                        return false;
                    else if (oneOption.Contains("НЕДОВОЛЬСТВО <=") && (int.Parse(oneOption.Split('=')[1]) < Character.Protagonist.Dissatisfaction))
                        return false;
                    else if (oneOption.Contains("ВЕРБОВКА >") && (int.Parse(oneOption.Split('>')[1]) >= Character.Protagonist.Recruitment))
                        return false;
                    else if (oneOption.Contains("ВЕРБОВКА <=") && (int.Parse(oneOption.Split('=')[1]) < Character.Protagonist.Recruitment))
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

        public List<string> Representer() => new List<string> { };

        public bool IsHealingEnabled() => false;

        public void UseHealing(int healingLevel) => Game.Other.DoNothing();

        public string TextByOptions(string option) => String.Empty;
    }
}
