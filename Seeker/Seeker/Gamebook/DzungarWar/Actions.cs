using System;
using System.Collections.Generic;
using System.Text;


namespace Seeker.Gamebook.DzungarWar
{
    class Actions : Abstract.IActions
    {
        public string ActionName { get; set; }
        public string ButtonName { get; set; }
        public string Aftertext { get; set; }
        public string Trigger { get; set; }
        public string RemoveTrigger { get; set; }

        public string Text { get; set; }
        public string Stat { get; set; }
        public int StatStep { get; set; }
        public bool StatToMax { get; set; }
        public int Level { get; set; }
        public int Price { get; set; }

        public string TriggerTestPenalty { get; set; }
        
        public Modification Benefit { get; set; }

        static bool NextTestWithTincture = false;
        static bool NextTestWithGinseng = false;


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

            reload = (actionResult.Count >= 1) && (actionResult[0] == "RELOAD");

            return actionResult;
        }

        private int TestLevelWithPenalty(int level, out List<string> penaltyLine)
        {
            penaltyLine = new List<string> { };

            if (NextTestWithTincture)
            {
                level -= 4;
                penaltyLine.Add("Бонус в -4 к уровню проверки за настойку");
            }
            
            if (NextTestWithGinseng)
            {
                level -= 8;
                penaltyLine.Add("Бонус в -8 к уровню проверки за отвар женьшеня");
            }

            if (String.IsNullOrEmpty(TriggerTestPenalty))
                return level;

            string[] penalties = TriggerTestPenalty.Split(';');

            foreach (string eachPenalty in penalties)
            {
                string[] penalty = eachPenalty.Split(',');

                if (Game.Data.Triggers.Contains(penalty[0].Trim()))
                {
                    level += int.Parse(penalty[1].Trim());
                    penaltyLine.Add(String.Format("Пенальти {0} к уровню проверки за ключевое слово {1}", penalty[1].Trim(), penalty[0].Trim()));
                }
            }

            if (level < 0)
                level = 0;

            return level;
        }

        public List<string> Representer()
        {
            if (ActionName == "TestAll")
                return new List<string> { String.Format("Проверить по совокупному уровню {0}", Level) };
            else if (Level > 0)
                return new List<string> { String.Format("Проверка {0}, уровень {1}", statNames[Stat], TestLevelWithPenalty(Level, out List<string> _)) };
            else if (!String.IsNullOrEmpty(Text))
                return new List<string> { Text };
            else
                return new List<string> { };
        }

        public List<string> Status()
        {
            List<string> statusLines = new List<string>();

            if (Character.Protagonist.Tanga > 0)
                statusLines.Add(String.Format("Деньги: {0}", Character.Protagonist.Tanga));

            if (Character.Protagonist.Tincture > 0)
                statusLines.Add(String.Format("Настойка: {0}", Character.Protagonist.Tincture));

            if (Character.Protagonist.Ginseng > 0)
                statusLines.Add(String.Format("Отвар: {0}", Character.Protagonist.Ginseng));

            if (Character.Protagonist.Favour != null)
                statusLines.Add(String.Format("Благосклонность: {0}", Character.Protagonist.Favour));

            if (Character.Protagonist.Danger != null)
                statusLines.Add(String.Format("Опасность: {0}", Character.Protagonist.Danger));

            return statusLines;
        }

        public List<string> AdditionalStatus()
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

            if (statusLines.Count <= 0)
                return null;

            return statusLines;
        }

        public List<string> StaticButtons()
        {
            List<string> staticButtons = new List<string> { };

            if (Game.Checks.ExistsInParagraph(actionName: "TEST") && (Character.Protagonist.Tincture > 0) && !NextTestWithTincture)
                staticButtons.Add("ВЫПИТЬ НАСТОЙКИ");

            if (Game.Checks.ExistsInParagraph(actionName: "TEST") && (Character.Protagonist.Ginseng > 0) && !NextTestWithGinseng)
                staticButtons.Add("ВЫПИТЬ ОТВАР ЖЕНЬШЕНЯ");

            return staticButtons;
        }

        public bool StaticAction(string action)
        {
            if (action == "ВЫПИТЬ НАСТОЙКИ")
            {
                Character.Protagonist.Tincture -= 1;
                NextTestWithTincture = true;
                return true;
            }

            if (action == "ВЫПИТЬ ОТВАР ЖЕНЬШЕНЯ")
            {
                Character.Protagonist.Ginseng -= 1;
                NextTestWithGinseng = true;
                return true;
            }

            return false;
        }

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            bool dangerEnd = Character.Protagonist.Danger >= 12;

            if ((Game.Data.CurrentParagraphID == 106) || (Game.Data.CurrentParagraphID == 148))
            {
                toEndParagraph = 122;
                toEndText = "Далее";

                Character.Protagonist.Danger = null;
            }
            else
            {
                toEndParagraph = 150;
                toEndText = "Стало слишком опасно";
            }

            return dangerEnd;
        }

        public bool IsButtonEnabled()
        {
            if (Level > 0)
                return true;

            else if (ActionName == "Brother")
                return Character.Protagonist.Brother <= 0;

            else if (StatToMax)
                return Character.Protagonist.MaxBonus > 0;

            else if (!String.IsNullOrEmpty(Stat))
            {
                int currentStat = (int)Character.Protagonist.GetType().GetProperty(Stat).GetValue(Character.Protagonist, null);
                return ((Character.Protagonist.StatBonuses > 0) && (currentStat < 12));
            }

            else if (Price >= 0)
                return (Character.Protagonist.Tanga >= Price);

            else
                return true;
        }

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

                int optionMustBe = int.Parse(options[0]);
                int optionCount = 0;

                foreach (string oneOption in options)
                    if (Game.Data.Triggers.Contains(oneOption.Trim()))
                        optionCount += 1;

                return optionCount >= optionMustBe;
            }
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains(">") || oneOption.Contains("<"))
                    {
                        if (oneOption.Contains("ТАНЬГА >=") && (int.Parse(oneOption.Split('=')[1]) > Character.Protagonist.Tanga))
                            return false;
                        else if (oneOption.Contains("ОПАСНОСТЬ >") && (int.Parse(oneOption.Split('>')[1]) >= Character.Protagonist.Danger))
                            return false;
                        else if (oneOption.Contains("ОПАСНОСТЬ <=") && (int.Parse(oneOption.Split('=')[1]) < Character.Protagonist.Danger))
                            return false;
                        else if (oneOption.Contains("БЛАГОСКЛОННОСТЬ >") && (int.Parse(oneOption.Split('>')[1]) >= Character.Protagonist.Favour))
                            return false;
                        else if (oneOption.Contains("БЛАГОСКЛОННОСТЬ <=") && (int.Parse(oneOption.Split('=')[1]) < Character.Protagonist.Favour))
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

        private void TestParam(string stat, int level, out bool result, out List<string> resultLine)
        {
            resultLine = new List<string>();

            level = TestLevelWithPenalty(level, out List<string> penalties);

            NextTestWithTincture = false;
            NextTestWithGinseng = false;

            resultLine.AddRange(penalties);

            int firstDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();
            int currentStat = (int)Character.Protagonist.GetType().GetProperty(stat).GetValue(Character.Protagonist, null);

            result = (firstDice + secondDice) + currentStat >= level;

            resultLine.Add( String.Format(
                "Проверка {0}: {1} + {2} + {3} {4} {5}",
                statNames[stat], Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), currentStat, (result ? ">=" : "<"), level
            ));
        }

        public List<string> Test()
        {
            List<string> testLines = new List<string>();

            TestParam(Stat, Level, out bool testIsOk, out List<string> result);

            testLines.AddRange(result);
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
                TestParam(test.Trim(), level, out bool thisTestIsOk, out List<string> result);

                testLines.AddRange(result);
                testLines.Add(thisTestIsOk ? "GOOD|Алдар справился" : "BAD|Алдар не справился");

                if (!thisTestIsOk)
                    testIsOk = false;
            }

            testLines.Add(testIsOk ? "BIG|GOOD|АЛДАР СПРАВИЛСЯ :)" : "BIG|BAD|АЛДАР НЕ СПРАВИЛСЯ :(");

            return testLines;
        }

        public List<string> Brother()
        {
            Character.Protagonist.Brother += 1;

            return new List<string> { "RELOAD" };
        }

        public List<string> Get()
        {
            if ((Price > 0) && (Character.Protagonist.Tanga >= Price))
            {
                Character.Protagonist.Tanga -= Price;

                Game.Option.Trigger(RemoveTrigger, remove: true);

                if (Benefit != null)
                    Benefit.Do();
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

        public bool IsHealingEnabled() => false;

        public void UseHealing(int healingLevel) => Game.Other.DoNothing();

        public string TextByOptions(string option) => String.Empty;
    }
}
