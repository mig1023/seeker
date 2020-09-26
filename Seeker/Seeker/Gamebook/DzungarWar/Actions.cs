using System;
using System.Collections.Generic;
using System.Text;


namespace Seeker.Gamebook.DzungarWar
{
    class Actions : Interfaces.IActions
    {
        public string ActionName { get; set; }
        public string ButtonName { get; set; }
        public string Aftertext { get; set; }
        public string OpenOption { get; set; }

        public string Text { get; set; }
        public string Stat { get; set; }
        public int Level { get; set; }
        public int Price { get; set; }

        public List<string> Do(out bool reload, string action = "", bool openOption = false)
        {
            if (openOption)
                Game.Option.OpenOption(OpenOption);

            string actionName = (String.IsNullOrEmpty(action) ? ActionName : action);
            List<string> actionResult = typeof(Actions).GetMethod(actionName).Invoke(this, new object[] { }) as List<string>;

            reload = ((actionResult.Count >= 1) && (actionResult[0] == "RELOAD") ? true : false);

            return actionResult;
        }

        public List<string> Representer()
        {
            return (String.IsNullOrEmpty(Text) ? new List<string> { } : new List<string> { Text });
        }

        public List<string> Status()
        {
            List<string> statusLines = new List<string>();

            if (Character.Protagonist.Strength > 1)
                statusLines.Add(String.Format("Сила: {0}", Character.Protagonist.Strength));

            if (Character.Protagonist.Skill > 1)
                statusLines.Add(String.Format("Ловкость: {0}", Character.Protagonist.Skill));

            if (Character.Protagonist.Wisdom > 1)
                statusLines.Add(String.Format("Мудрость: {0}", Character.Protagonist.Wisdom));

            if (Character.Protagonist.Cunning > 1)
                statusLines.Add(String.Format("Хитрость: {0}", Character.Protagonist.Cunning));

            if (Character.Protagonist.Oratory > 1)
                statusLines.Add(String.Format("Красноречие: {0}", Character.Protagonist.Oratory));

            if (Character.Protagonist.Tanga > 0)
                statusLines.Add(String.Format("Деньги: {0}", Character.Protagonist.Tanga));

            statusLines.Add(String.Format("Популярность: {0}", Character.Protagonist.Popularity));

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
            if (Level > 0)
                return true;
            else if (Price <= 0)
                return (Character.Protagonist.StatBonuses > 0);
            else
                return (Character.Protagonist.Tanga >= Price);
        }

        public static bool CheckOnlyIf(string option)
        {
            string[] options = option.Split(',');

            foreach (string oneOption in options)
            {
                if (oneOption.Contains(">") || oneOption.Contains("<"))
                {
                    if (oneOption.Contains("ТАНЬГА >=") && (int.Parse(oneOption.Split('=')[1]) > Character.Protagonist.Tanga))
                        return false;
                    else if (oneOption.Contains("ПОПУЛЯРНОСТЬ >") && (int.Parse(oneOption.Split('>')[1]) >= Character.Protagonist.Popularity))
                        return false;
                }
                else if (oneOption.Contains("!"))
                {
                    if (Game.Data.OpenedOption.Contains(oneOption.Replace("!", String.Empty).Trim()))
                        return false;
                }
                else if (!Game.Data.OpenedOption.Contains(oneOption.Trim()))
                    return false;
            }

            return true;
        }

        public List<string> Test()
        {
            int firstDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();

            int currentStat = (int)Character.Protagonist.GetType().GetProperty(Stat).GetValue(Character.Protagonist, null);
            bool testIsOk = (firstDice + secondDice) + currentStat >= Level;

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
                "Проверка {0}: {1} ⚄ + {2} ⚄ + {3} {4} {5}", statNames[Stat], firstDice, secondDice, currentStat, (testIsOk ? ">=" : "<"), Level
            ) };

            testLines.Add(testIsOk ? "BIG|GOOD|АЛДАР СПРАВИЛСЯ :)" : "BIG|BAD|АЛДАР НЕ СПРАВИЛСЯ :(");

            return testLines;
        }

        public List<string> Get()
        {
            if ((Price > 0) && (Character.Protagonist.Tanga >= Price))
                Character.Protagonist.Tanga -= Price;
            else if (Character.Protagonist.StatBonuses >= 0)
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
