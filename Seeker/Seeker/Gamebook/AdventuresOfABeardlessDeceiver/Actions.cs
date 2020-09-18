using System;
using System.Collections.Generic;
using System.Text;


namespace Seeker.Gamebook.AdventuresOfABeardlessDeceiver
{
    class Actions : Interfaces.IActions
    {
        public string ActionName { get; set; }
        public string ButtonName { get; set; }
        public string Aftertext { get; set; }
        public string OpenOption { get; set; }

        public string Text { get; set; }
        public string Stat { get; set; }
        public int TestLevel { get; set; }

        public List<string> Do(out bool reload, string action = "", bool openOption = false)
        {
            if (openOption)
                Game.Option.OpenOption(OpenOption);

            string actionName = (String.IsNullOrEmpty(action) ? ActionName : action);
            List<string> actionResult = typeof(Actions).GetMethod(actionName).Invoke(this, new object[] { }) as List<string>;

            reload = ((actionResult.Count >= 1) && (actionResult[0] == "RELOAD") ? true : false);

            return actionResult;
        }

        public List<string> Status()
        {
            List<string> statusLines = new List<string>
            {
                String.Format("Сила: {0}", Character.Protagonist.Strength),
                String.Format("Ловкость: {0}", Character.Protagonist.Skill),
                String.Format("Мудрость: {0}", Character.Protagonist.Wisdom),
                String.Format("Хитрость: {0}", Character.Protagonist.Cunning),
                String.Format("Красноречие: {0}", Character.Protagonist.Oratory),
                String.Format("Популярность: {0}", Character.Protagonist.Popularity),
                String.Format("Деньги: {0}", Character.Protagonist.Tanga)
            };

            return statusLines;
        }

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 150;
            toEndText = "Задуматься о судьбе";

            return (Character.Protagonist.Popularity <= 0 ? true : false);
        }

        public bool IsButtonEnabled()
        {
            return (Character.Protagonist.StatBonuses > 0);
        }

        public static bool CheckOnlyIf(string option)
        {
            if (option.Contains(","))
            {
                foreach (string oneOption in option.Split(','))
                    if (!Game.Data.OpenedOption.Contains(oneOption.Trim()))
                        return false;

                return true;
            }
            else if (option.Contains("ТАНЬГА >="))
                return int.Parse(option.Split('=')[1]) <= Character.Protagonist.Tanga;
            else
                return Game.Data.OpenedOption.Contains(option);
        }

        public List<string> Test()
        {
            int firstDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();

            int currentStat = (int)Character.Protagonist.GetType().GetProperty(Stat).GetValue(Character.Protagonist, null);
            bool testIsOk = (firstDice + secondDice) + currentStat >= TestLevel;

            Dictionary<string, string> statNames = new Dictionary<string, string>
            {
                ["Strength"] = "силы",
                ["Skill"] = "ловкости",
                ["Wisdom"] = "мудрости",
                ["Cunning"] = "хитрости",
                ["Oratory"] = "красноречия",
                ["Popularity"] = "популярности",
            };

            List<string> testLines = new List<string> { String.Format(
                "Проверка {0}: {1} ⚄ + {2} ⚄ + {3} {4} {5}", statNames[Stat], firstDice, secondDice, currentStat, (testIsOk ? ">=" : "<"), TestLevel
            ) };

            testLines.Add(testIsOk ? "BIG|GOOD|АЛДАР СПРАВИЛСЯ :)" : "BIG|BAD|АЛДАР НЕ СПРАВИЛСЯ :(");

            return testLines;
        }

        public List<string> Get()
        {
            if (Character.Protagonist.StatBonuses >= 0)
            {
                int currentStat = (int)Character.Protagonist.GetType().GetProperty(Stat).GetValue(Character.Protagonist, null);

                currentStat += 1;

                Character.Protagonist.GetType().GetProperty(Stat).SetValue(Character.Protagonist, currentStat);

                Character.Protagonist.StatBonuses -= 1;
            }

            return new List<string> { "RELOAD" };
        }
    }
}
