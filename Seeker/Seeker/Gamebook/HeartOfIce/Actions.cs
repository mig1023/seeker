using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.HeartOfIce
{
    class Actions : Abstract.IActions
    {
        public string ActionName { get; set; }
        public string ButtonName { get; set; }
        public string Aftertext { get; set; }
        public string Trigger { get; set; }
        public string Text { get; set; }
        public string Skill { get; set; }
        public bool Choice { get; set; }

        public List<Modification> Benefit { get; set; }

        public List<string> Do(out bool reload, string action = "", bool trigger = false)
        {
            if (trigger)
                Game.Option.Trigger(Trigger);

            string actionName = (String.IsNullOrEmpty(action) ? ActionName : action);
            List<string> actionResult = typeof(Actions).GetMethod(actionName).Invoke(this, new object[] { }) as List<string>;

            reload = (actionResult.Count >= 1) && (actionResult[0] == "RELOAD");

            return actionResult;
        }

        public List<string> Representer()
        {
            if (!String.IsNullOrEmpty(Text))
                return new List<string> { Text };

            else if (!String.IsNullOrEmpty(Skill))
                return new List<string> { Skill };

            else
                return new List<string>();
        }

        public List<string> Status()
        {
            List<string> statusLines = new List<string>
            {
                String.Format("Здоровье: {0}", Character.Protagonist.Life),
                String.Format("Деньги: {0}", Character.Protagonist.Money),
            };

            return statusLines;
        }

        public List<string> AdditionalStatus() => null;

        public List<string> StaticButtons() => new List<string> { };

        public bool StaticAction(string action) => false;

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = "Начать сначала";

            return Character.Protagonist.Life <= 0;
        }

        public List<string> Get()
        {
            if (Choice)
                Character.Protagonist.Chosen = true;

            if (!String.IsNullOrEmpty(Skill))
            {
                Character.Protagonist.Skills.Add(Skill);
                Character.Protagonist.SkillsValue -= 1;
            }

            if (Benefit != null)
                foreach (Modification modification in Benefit)
                    modification.Do();

            return new List<string> { "RELOAD" };
        }

        public bool IsButtonEnabled()
        {
            bool disbledByChoice = (Choice && Character.Protagonist.Chosen);
            bool disabledBySkills = (!String.IsNullOrEmpty(Skill) &&
                ((Character.Protagonist.SkillsValue <= 0) || Character.Protagonist.Skills.Contains(Skill)));

            return !(disbledByChoice || disabledBySkills);
        }

        public static bool CheckOnlyIf(string option)
        {
            if (option.Contains("|"))
            {
                foreach (string oneOption in option.Split('|'))
                {
                    if (Character.Protagonist.Skills.Contains(oneOption.Trim()))
                        return true;

                    if (Game.Data.Triggers.Contains(oneOption.Trim()))
                        return true;
                }

                return false;
            }
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (!Character.Protagonist.Skills.Contains(oneOption.Trim()))
                        return false;

                    if (!Game.Data.Triggers.Contains(oneOption.Trim()))
                        return false;
                }

                return true;
            }
        }

        public bool IsHealingEnabled() => false;

        public void UseHealing(int healingLevel) => Game.Other.DoNothing();

        public string TextByOptions(string option) => String.Empty;
    }
}
