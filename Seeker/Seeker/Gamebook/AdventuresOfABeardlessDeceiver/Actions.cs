using System;
using System.Collections.Generic;
using System.Text;


namespace Seeker.Gamebook.AdventuresOfABeardlessDeceiver
{
    class Actions : Abstract.IActions
    {
        public string ActionName { get; set; }
        public string ButtonName { get; set; }
        public string Aftertext { get; set; }
        public string Trigger { get; set; }

        public string Text { get; set; }
        public string Stat { get; set; }
        public int Level { get; set; }
        public int Price { get; set; }
        public bool GreatKhanSpecialCheck { get; set; }
        public bool GuessBonus { get; set; }

        public Modification Benefit { get; set; }

        static bool NextTestWithKumis = false;

        Dictionary<string, string> statNames = new Dictionary<string, string>
        {
            ["Strength"] = "силы",
            ["Skill"] = "ловкости",
            ["Wisdom"] = "мудрости",
            ["Cunning"] = "хитрости",
            ["Oratory"] = "красноречия",
            ["Popularity"] = "популярности",
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
            if (Level > 0)
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

            if (Character.Protagonist.AkynGlory != null)
                statusLines.Add(String.Format("Слава акына: {0}", Character.Protagonist.AkynGlory));

            if (Character.Protagonist.UnitOfTime != null)
                statusLines.Add(String.Format("Ед.времени: {0}", Character.Protagonist.UnitOfTime));

            if (Character.Protagonist.Kumis > 0)
                statusLines.Add(String.Format("Кумыс: {0}", Character.Protagonist.Kumis));

            statusLines.Add(String.Format("Популярность: {0}", Character.Protagonist.Popularity));

            return statusLines;
        }

        public List<string> StaticButtons()
        {
            List<string> staticButtons = new List<string> { };

            if ((Character.Protagonist.Kumis > 0) && !NextTestWithKumis)
                staticButtons.Add("ВЫПИТЬ КУМЫСА");

            return staticButtons;
        }

        public bool StaticAction(string action)
        {
            if (action == "ВЫПИТЬ КУМЫСА")
            {
                Character.Protagonist.Kumis -= 1;
                NextTestWithKumis = true;
                return true;
            }

            return false;
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
                    else if (oneOption.Contains("СЛАВА_АКЫНА >=") && (int.Parse(oneOption.Split('=')[1]) > Character.Protagonist.AkynGlory))
                        return false;
                    else if (oneOption.Contains("ПОПУЛЯРНОСТЬ >") && (int.Parse(oneOption.Split('>')[1]) >= Character.Protagonist.Popularity))
                        return false;
                    else if (oneOption.Contains("ЕДИНИЦЫ_ВРЕМЕНИ >") && (int.Parse(oneOption.Split('>')[1]) >= Character.Protagonist.UnitOfTime))
                        return false;
                    else if (oneOption.Contains("ЕДИНИЦЫ_ВРЕМЕНИ <=") && (int.Parse(oneOption.Split('=')[1]) < Character.Protagonist.UnitOfTime))
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

        public List<string> Test()
        {
            int firstDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();

            if (GuessBonus && Game.Data.Triggers.Contains("guess"))
                Level = 7;

            if (GreatKhanSpecialCheck)
                Level -= (Character.Protagonist.Popularity + (Game.Data.Triggers.Contains("KhansRing") ? 3 : 0));

            if (NextTestWithKumis)
                Level -= 2;

            int currentStat = (int)Character.Protagonist.GetType().GetProperty(Stat).GetValue(Character.Protagonist, null);
            bool testIsOk = (firstDice + secondDice) + currentStat >= Level;

            List<string> testLines = new List<string> { String.Format(
                "Проверка {0}: {1} ⚄ + {2} ⚄ + {3} {4} {5}", statNames[Stat], firstDice, secondDice, currentStat, (testIsOk ? ">=" : "<"), Level
            ) };

            if (GuessBonus && Game.Data.Triggers.Contains("guess"))
                testLines.Insert(0, "Бонус за догадку: -4");

            if (GreatKhanSpecialCheck)
            {
                if (Game.Data.Triggers.Contains("KhansRing"))
                    testLines.Insert(0, "Бонус за ханское кольцо: -3");

                if (Character.Protagonist.Popularity > 0)
                    testLines.Insert(0, String.Format("Бонус за популярность: -{0}", Character.Protagonist.Popularity));
            }

            if (NextTestWithKumis)
                testLines.Insert(0, "Бонус за кумыс: -2");

            testLines.Add(testIsOk ? "BIG|GOOD|АЛДАР СПРАВИЛСЯ :)" : "BIG|BAD|АЛДАР НЕ СПРАВИЛСЯ :(");

            NextTestWithKumis = false;

            return testLines;
        }

        public List<string> Get()
        {
            if ((Price > 0) && (Character.Protagonist.Tanga >= Price))
            {
                Character.Protagonist.Tanga -= Price;

                if (Benefit != null)
                    Benefit.Do();
            }

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
