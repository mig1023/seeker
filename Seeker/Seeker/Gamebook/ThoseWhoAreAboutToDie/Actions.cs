using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.ThoseWhoAreAboutToDie
{
    class Actions : Abstract.IActions
    {
        public string ActionName { get; set; }
        public string ButtonName { get; set; }
        public string Aftertext { get; set; }
        public string Trigger { get; set; }
        public int Dices { get; set; }


        public List<string> Do(out bool reload, string action = "", bool trigger = false)
        {
            if (trigger)
                Game.Option.Trigger(Trigger);

            string actionName = (String.IsNullOrEmpty(action) ? ActionName : action);
            List<string> actionResult = typeof(Actions).GetMethod(actionName).Invoke(this, new object[] { }) as List<string>;

            reload = (actionResult.Count >= 1) && (actionResult[0] == "RELOAD");

            return actionResult;
        }

        public List<string> Representer() => new List<string> { };

        public List<string> Status()
        {
            List<string> statusLines = new List<string>
            {
                String.Format("Реакция: {0}", Character.Protagonist.Reaction),
                String.Format("Сила: {0}", Character.Protagonist.Strength),
                String.Format("Выносливость: {0}", Character.Protagonist.Endurance),
            };

            return statusLines;
        }

        public List<string> AdditionalStatus() => null;

        public List<string> StaticButtons() => new List<string> { };

        public bool StaticAction(string action) => false;

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = String.Empty;

            return Character.Protagonist.Endurance <= 0;
        }

        public bool IsButtonEnabled() => true;

        public static bool CheckOnlyIf(string option)
        {
            if (option.Contains("|"))
            {
                foreach (string oneOption in option.Split('|'))
                    if (!OneParamFail(oneOption))
                        return true;
                    else if (Game.Data.Triggers.Contains(oneOption.Trim()))
                        return true;

                return false;
            }
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains(">") || oneOption.Contains("<"))
                    {
                        if (OneParamFail(oneOption))
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

        private static bool OneParamFail(string oneOption)
        {
            if (ParamFail("СИЛА", oneOption, Character.Protagonist.Strength))
                return true;
            else if (ParamFail("РЕАКЦИЯ", oneOption, Character.Protagonist.Reaction))
                return true;
            else if (ParamFail("ВЫНОСЛИВОСТЬ", oneOption, Character.Protagonist.Endurance))
                return true;
            else
                return false;
        }

        private static bool ParamFail(string paramName, string option, int param)
        {
            if (option.Contains(String.Format("{0} >", paramName)) && (int.Parse(option.Split('>')[1]) >= param))
                return true;
            else if (option.Contains(String.Format("{0} <=", paramName)) && (int.Parse(option.Split('=')[1]) < param))
                return true;
            else
                return false;
        }

        public List<string> TryToWound()
        {
            List<string> report = new List<string>();

            int dice = Game.Dice.Roll();
            report.Add(String.Format("На кубике выпало: {0}", Game.Dice.Symbol(dice)));

            if (dice > 4)
            {
                Character.Protagonist.Reaction += 3;
                report.Add("BIG|GOOD|+3 к Реакции! :)");
            }
            else
                report.Add("BIG|BAD|Не повезло :(");

            return report;
        }

        public bool IsHealingEnabled() => false;

        public void UseHealing(int healingLevel) => Game.Other.DoNothing();

        public string TextByOptions(string option) => String.Empty;
    }
}
