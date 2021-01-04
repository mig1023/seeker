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

            reload = ((actionResult.Count >= 1) && (actionResult[0] == "RELOAD") ? true : false);

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
                string[] options = option.Split('|');

                foreach (string oneOption in options)
                    if (Game.Data.Triggers.Contains(oneOption.Trim()))
                        return true;

                return false;
            }
            else
            {
                string[] options = option.Split(',');

                foreach (string oneOption in options)
                {
                    if (oneOption.Contains(">") || oneOption.Contains("<"))
                    {
                        if (ParamFail("СИЛА", oneOption, Character.Protagonist.Strength))
                            return false;
                        else if (ParamFail("РЕАКЦИЯ", oneOption, Character.Protagonist.Reaction))
                            return false;
                        else if (ParamFail("ВЫНОСЛИВОСТЬ", oneOption, Character.Protagonist.Endurance))
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

        private static bool ParamFail(string paramName, string option, int param)
        {
            if (option.Contains(String.Format("{0} >", paramName)) && (int.Parse(option.Split('>')[1]) >= param))
                return true;
            else if (option.Contains(String.Format("{0} <=", paramName)) && (int.Parse(option.Split('=')[1]) < param))
                return true;
            else
                return false;
        }

        public List<string> DiceCheck()
        {
            List<string> diceCheck = new List<string> { };

            int firstDice = Game.Dice.Roll();
            int dicesResult = firstDice;

            if (Dices == 1)
                diceCheck.Add(String.Format("На кубикe выпало: {0} ⚄", firstDice));
            else
            {
                int secondDice = Game.Dice.Roll();
                dicesResult += secondDice;
                diceCheck.Add(String.Format("На кубиках выпало: {0} ⚄ + {1} ⚄ = {2}", firstDice, secondDice, (firstDice + secondDice)));
            }

            diceCheck.Add(dicesResult % 2 == 0 ? "BIG|ЧЁТНОЕ ЧИСЛО!" : "BIG|НЕЧЁТНОЕ ЧИСЛО!");

            return diceCheck;
        }
    }
}
