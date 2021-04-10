using System;
using System.Collections.Generic;
using System.Text;


namespace Seeker.Gamebook.SilentSchool
{
    class Actions : Abstract.IActions
    {
        public string ActionName { get; set; }
        public string ButtonName { get; set; }
        public string Aftertext { get; set; }
        public string Trigger { get; set; }
        public string Text { get; set; }
        public int HarmedMyself { get; set; }
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

        public List<string> Status()
        {
            List<string> statusLines = new List<string> { String.Format("Жизнь: {0}", Character.Protagonist.Life) };

            if (Character.Protagonist.Grail > 0)
                statusLines.Add(String.Format("Грааль: {0}", Character.Protagonist.Grail));

            if (!String.IsNullOrEmpty(Character.Protagonist.Weapon))
                statusLines.Add(String.Format("Оружие: {0}", Character.Protagonist.Weapon));

            return statusLines;
        }

        public List<string> AdditionalStatus() => null;

        public List<string> StaticButtons()
        {
            List<string> staticButtons = new List<string> { };

            if (Game.Data.Triggers.Contains("Шоколадка"))
                staticButtons.Add("Съесть шоколадку");

            return staticButtons;
        }

        public bool StaticAction(string action)
        {
            Game.Option.Trigger("Шоколадка", remove: true);

            Character.Protagonist.Life += 3;

            return true;
        }

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = "Начать сначала";

            return Character.Protagonist.Life <= 0;
        }

        public bool IsButtonEnabled() => !((HarmedMyself > 0) && ((Character.Protagonist.HarmSelfAlready > 0) ||
            (Character.Protagonist.Life <= HarmedMyself)));

        public static bool CheckOnlyIf(string option)
        {
            if (option.Contains("|"))
            {
                foreach (string oneOption in option.Split('|'))
                    if (Game.Data.Triggers.Contains(oneOption.Trim()))
                        return true;

                return false;
            }
            else if (option.Contains(";"))
            {
                string[] options = option.Split(';');

                bool not = options[0].Contains("!");
                int optionMustBe = int.Parse(options[0].Replace("!", String.Empty));
                int optionCount = 0;

                foreach (string oneOption in options)
                    if (Game.Data.Triggers.Contains(oneOption.Trim()))
                        optionCount += 1;

                if (not)
                    return optionCount < optionMustBe;
                else
                    return optionCount >= optionMustBe;
            }
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains(">") || oneOption.Contains("<"))
                    {
                        if (oneOption.Contains("ГРААЛЬ >=") && (int.Parse(oneOption.Split('=')[1]) > Character.Protagonist.Grail))
                            return false;

                        if (oneOption.Contains("РАНА >=") && (int.Parse(oneOption.Split('=')[1]) > Character.Protagonist.HarmSelfAlready))
                            return false;

                        if (oneOption.Contains("РАНА <") && (int.Parse(oneOption.Split('<')[1]) <= Character.Protagonist.HarmSelfAlready))
                            return false;
                    }
                    else if (oneOption.Contains("ОРУЖИЕ"))
                    {
                        if (oneOption.Contains("!") && (oneOption.Split('=')[1].Trim() == Character.Protagonist.Weapon))
                            return false;
                        else if (!oneOption.Contains("!") && (oneOption.Split('=')[1].Trim() != Character.Protagonist.Weapon))
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

        public List<string> Representer() => String.IsNullOrEmpty(Text) ? new List<string> { } : new List<string> { Text.ToUpper() };

        public List<string> Get()
        {
            if (HarmedMyself > 0)
            {
                Character.Protagonist.Life -= HarmedMyself;
                Character.Protagonist.HarmSelfAlready = HarmedMyself;
            }
            else
                Character.Protagonist.Weapon = Text;

            return new List<string> { "RELOAD" };
        }

        public List<string> DiceWounds()
        {
            List<string> diceCheck = new List<string> { };

            int dicesCount = (Dices == 0 ? 1 : Dices);
            int dices = 0;

            for (int i = 1; i <= dicesCount; i++)
            {
                int dice = Game.Dice.Roll();
                dices += dice;
                diceCheck.Add(String.Format("На {0} выпало: {1}", i, Game.Dice.Symbol(dice)));
            }

            Character.Protagonist.Life -= dices;

            if (Character.Protagonist.Life < 0)
                Character.Protagonist.Life = 0;

            diceCheck.Add(String.Format("BIG|BAD|Я потерял жизней: {0}", Game.Dice.Symbol(dices)));

            return diceCheck;
        }

        public bool IsHealingEnabled() => false;

        public void UseHealing(int healingLevel) => Game.Other.DoNothing();

        public string TextByOptions(string option) => String.Empty;
    }
}
