using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.RendezVous
{
    class Actions : Interfaces.IActions
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

        public List<string> Representer()
        {
            return new List<string> { };
        }

        public List<string> Status()
        {
            List<string> statusLines = new List<string>
            {
                String.Format("Осознание: {0}", Character.Protagonist.Awareness),
            };

            return statusLines;
        }

        public List<string> StaticButtons() => new List<string> { };

        public bool StaticAction(string action) => false;

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = "Начать с начала...";

            return false;
        }

        public bool IsButtonEnabled()
        {
            return true;
        }

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
                        if (oneOption.Contains("ОСОЗНАНИЕ >") && (int.Parse(oneOption.Split('>')[1]) >= Character.Protagonist.Awareness))
                            return false;
                        else if (oneOption.Contains("ОСОЗНАНИЕ <=") && (int.Parse(oneOption.Split('=')[1]) < Character.Protagonist.Awareness))
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
