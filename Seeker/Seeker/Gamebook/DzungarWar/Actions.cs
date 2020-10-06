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
        public string Trigger { get; set; }

        public string Text { get; set; }
        public string Stat { get; set; }
        public int StatStep { get; set; }
        public bool StatToMax { get; set; }
        public int Level { get; set; }
        public int Price { get; set; }


        Dictionary<string, string> statNames = new Dictionary<string, string>
        {
            ["Strength"] = "силы",
            ["Skill"] = "ловкости",
            ["Wisdom"] = "мудрости",
            ["Cunning"] = "хитрости",
            ["Oratory"] = "красноречия",
            ["Danger"] = "опасности",
        };

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
            if (ActionName == "TestAll")
                return new List<string> { String.Format("Проверить по совокупному уровню {0}", Level) };
            else if (Level > 0)
                return new List<string> { String.Format("Проверка {0}, уровень {1}", statNames[Stat], Level) };
            else if (!String.IsNullOrEmpty(Text))
                return new List<string> { Text };
            else
                return new List<string> { };
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

            statusLines.Add(String.Format("Опасность: {0}", Character.Protagonist.Danger));

            return statusLines;
        }

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 150;
            toEndText = "Стало слишком опасно";

            return (Character.Protagonist.Danger >= 12 ? true : false);
        }

        public bool IsButtonEnabled()
        {
            if (Level > 0)
                return true;

            else if (StatToMax)
                return Character.Protagonist.MaxBonus > 0;

            else if (Price <= 0)
            {
                int currentStat = (int)Character.Protagonist.GetType().GetProperty(Stat).GetValue(Character.Protagonist, null);

                return ((Character.Protagonist.StatBonuses > 0) && (currentStat < 12));
            }

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

        private void TestParam(string stat, int level, out bool result, out string resultLine)
        {
            int firstDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();
            int currentStat = (int)Character.Protagonist.GetType().GetProperty(stat).GetValue(Character.Protagonist, null);

            result = (firstDice + secondDice) + currentStat >= level;

            resultLine = String.Format(
                "Проверка {0}: {1} ⚄ + {2} ⚄ + {3} {4} {5}", statNames[stat], firstDice, secondDice, currentStat, (result ? ">=" : "<"), level
            );
        }

        public List<string> Test()
        {
            List<string> testLines = new List<string>();

            TestParam(Stat, Level, out bool testIsOk, out string result);

            testLines.Add(result);
            testLines.Add(testIsOk ? "BIG|GOOD|АЛДАР СПРАВИЛСЯ :)" : "BIG|BAD|АЛДАР НЕ СПРАВИЛСЯ :(");

            return testLines;
        }

        public List<string> TestAll()
        {
            bool testIsOk = true;
            List<string> testLines = new List<string>();

            string[] tests = Stat.Split(',');

            int level = (int)Math.Ceiling((double)Level / (double)tests.Length); 

            foreach (string test in tests)
            {
                TestParam(test.Trim(), level, out bool thisTestIsOk, out string result);

                testLines.Add(result);
                testLines.Add(thisTestIsOk ? "GOOD|Алдар справился" : "BAD|Алдар не справился");

                if (!thisTestIsOk)
                    testIsOk = false;
            }

            testLines.Add(testIsOk ? "BIG|GOOD|АЛДАР СПРАВИЛСЯ :)" : "BIG|BAD|АЛДАР НЕ СПРАВИЛСЯ :(");

            return testLines;
        }

        public List<string> Get()
        {
            if ((Price > 0) && (Character.Protagonist.Tanga >= Price))
            {
                Character.Protagonist.Tanga -= Price;
            }
            else if (StatToMax && (Character.Protagonist.MaxBonus > 0))
            {
                Character.Protagonist.GetType().GetProperty(Stat).SetValue(Character.Protagonist, 12);

                Character.Protagonist.MaxBonus = 0;
            }
            else if (Character.Protagonist.StatBonuses >= 0)
            {
                int currentStat = (int)Character.Protagonist.GetType().GetProperty(Stat).GetValue(Character.Protagonist, null);

                currentStat += (StatStep > 1 ? StatStep : 1);

                Character.Protagonist.GetType().GetProperty(Stat).SetValue(Character.Protagonist, currentStat);

                Character.Protagonist.StatBonuses -= 1;
            }

            return new List<string> { "RELOAD" };
        }
    }
}
