using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.DzungarWar
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public string RemoveTrigger { get; set; }

        public string Stat { get; set; }
        public int StatStep { get; set; }
        public bool StatToMax { get; set; }
        public int Level { get; set; }
        public string TriggerTestPenalty { get; set; }

        static bool NextTestWithTincture = false, NextTestWithGinseng = false, NextTestWithAirag = false;

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

            if (NextTestWithAirag)
            {
                level -= 2;
                penaltyLine.Add("Бонус в -2 к уровню проверки за айраг");
            }

            if (String.IsNullOrEmpty(TriggerTestPenalty))
                return level;

            string[] penalties = TriggerTestPenalty.Split(';');

            foreach (string eachPenalty in penalties)
            {
                string[] penalty = eachPenalty.Split(',');

                if (penalty[0].Trim() == "Вино")
                {
                    int bottles = Game.Data.Triggers.Where(x => x == "Кувшин вина").Count();

                    if (bottles > 0)
                    {
                        level += (bottles * -1);
                        penaltyLine.Add(String.Format("Пенальти -{0} за покупку {0} кувшинов", bottles));
                    }
                }
                else if (Game.Data.Triggers.Contains(penalty[0].Trim()))
                {
                    level += int.Parse(penalty[1].Trim());
                    penaltyLine.Add(String.Format("Пенальти {0} к уровню проверки за ключевое слово {1}", penalty[1].Trim(), penalty[0].Trim()));
                }
            }

            if (level < 0)
            {
                level = 0;
            }

            return level;
        }

        public override List<string> Representer()
        {
            if (Name == "TestAll")
            {
                return new List<string> { String.Format("Проверить по совокупному уровню {0}", Level) };
            }
            else if (Level > 0)
            {
                return new List<string> { String.Format("Проверка {0}, уровень {1}",
                    Constants.StatNames()[Stat], TestLevelWithPenalty(Level, out List<string> _)) };
            }
            else if (!String.IsNullOrEmpty(Stat) && !StatToMax)
            {
                int currentStat = GetProperty(protagonist, Stat);
                string diffLine = (currentStat > 1 ? String.Format(" (+{0})", (currentStat - 1)) : String.Empty);

                return new List<string> { String.Format("{0}{1}", Text, diffLine) };
            }
            else if (Price > 0)
            {
                return new List<string> { String.Format("{0}, {1} таньга", Text, Price) };
            }
            else if (!String.IsNullOrEmpty(Text))
            {
                return new List<string> { Text };
            }
            else
            {
                return new List<string> { };
            }
        }

        public override List<string> Status()
        {
            List<string> statusLines = new List<string>();

            if (protagonist.Tanga > 0)
                statusLines.Add(String.Format("Деньги: {0}", protagonist.Tanga));

            if (protagonist.Danger != null)
                statusLines.Add(String.Format("Опасность: {0}", protagonist.Danger));

            return statusLines;
        }

        public override List<string> AdditionalStatus()
        {
            List<string> statusLines = new List<string>();

            if (protagonist.Tincture > 0)
                statusLines.Add(String.Format("Настойка: {0}", protagonist.Tincture));

            if (protagonist.Ginseng > 0)
                statusLines.Add(String.Format("Отвар: {0}", protagonist.Ginseng));

            if (protagonist.Oratory > 1)
                statusLines.Add(String.Format("Красноречие: {0}", protagonist.Oratory));

            if (protagonist.Cunning > 1)
                statusLines.Add(String.Format("Хитрость: {0}", protagonist.Cunning));

            if (protagonist.Wisdom > 1)
                statusLines.Add(String.Format("Мудрость: {0}", protagonist.Wisdom));

            if (protagonist.Skill > 1)
                statusLines.Add(String.Format("Ловкость: {0}", protagonist.Skill));

            if (protagonist.Strength > 1)
                statusLines.Add(String.Format("Сила: {0}", protagonist.Strength));

            if (protagonist.Favour != null)
                statusLines.Add(String.Format("Благосклонность: {0}", protagonist.Favour));

            if (statusLines.Count <= 0)
                return null;

            return statusLines;
        }

        public override List<string> StaticButtons()
        {
            List<string> staticButtons = new List<string> { };

            if (!Game.Checks.ExistsInParagraph(actionName: "TEST"))
                return staticButtons;

            if ((protagonist.Tincture > 0) && !NextTestWithTincture)
                staticButtons.Add("ВЫПИТЬ НАСТОЙКИ");

            if ((protagonist.Ginseng > 0) && !NextTestWithGinseng)
                staticButtons.Add("ВЫПИТЬ ОТВАР ЖЕНЬШЕНЯ");

            if ((protagonist.Airag > 0) && !NextTestWithAirag)
                staticButtons.Add("ВЫПИТЬ АЙРАГА");

            return staticButtons;
        }

        public override bool StaticAction(string action)
        {
            if (action == "ВЫПИТЬ НАСТОЙКИ")
            {
                protagonist.Tincture -= 1;
                NextTestWithTincture = true;
            }

            else if (action == "ВЫПИТЬ ОТВАР ЖЕНЬШЕНЯ")
            {
                protagonist.Ginseng -= 1;
                NextTestWithGinseng = true;
            }

            else if (action == "ВЫПИТЬ АЙРАГА")
            {
                protagonist.Airag -= 1;
                NextTestWithAirag = true;
            }

            else
                return false;

            return true;
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText)
        {
            bool dangerEnd = protagonist.Danger >= 12;

            if ((Game.Data.CurrentParagraphID == 106) || (Game.Data.CurrentParagraphID == 148))
            {
                toEndParagraph = 122;
                toEndText = "Далее";

                protagonist.Danger = null;
            }
            else
            {
                toEndParagraph = 150;
                toEndText = "Стало слишком опасно";
            }

            return dangerEnd;
        }

        public override bool IsButtonEnabled()
        {
            if (Level > 0)
            {
                return true;
            }
            else if (Used)
            {
                return false;
            }
            else if (Name == "Brother")
            {
                return protagonist.Brother <= 0;
            }
            else if (StatToMax)
            {
                return protagonist.MaxBonus > 0;
            }
            else if (!String.IsNullOrEmpty(Stat))
            {
                return ((protagonist.StatBonuses > 0) && (GetProperty(protagonist, Stat) < 12));
            }
            else if (Price >= 0)
            {
                return (protagonist.Tanga >= Price);
            }
            else
            {
                return true;
            }
        }

        public override bool CheckOnlyIf(string option)
        {
            if (option.Contains("|"))
            {
                return option.Split('|').Where(x => Game.Data.Triggers.Contains(x.Trim())).Count() > 0;
            }
            else if (option.Contains(";"))
            {
                string[] options = option.Split(';');

                int optionMustBe = int.Parse(options[0]);
                int optionCount = options.Where(x => Game.Data.Triggers.Contains(x.Trim())).Count();

                return optionCount >= optionMustBe;
            }
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains(">") || oneOption.Contains("<"))
                    {
                        int level = Game.Other.LevelParse(oneOption);

                        if (oneOption.Contains("ТАНЬГА >=") && (level > protagonist.Tanga))
                            return false;

                        else if (oneOption.Contains("ОПАСНОСТЬ >") && (level >= protagonist.Danger))
                            return false;

                        else if (oneOption.Contains("ОПАСНОСТЬ <=") && (level < protagonist.Danger))
                            return false;

                        else if (oneOption.Contains("БЛАГОСКЛОННОСТЬ >") && (level >= protagonist.Favour))
                            return false;

                        else if (oneOption.Contains("БЛАГОСКЛОННОСТЬ <=") && (level < protagonist.Favour))
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
            NextTestWithAirag = false;

            resultLine.AddRange(penalties);

            int firstDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();
            int currentStat = GetProperty(protagonist, stat);

            result = (firstDice + secondDice) + currentStat >= level;

            resultLine.Add(String.Format(
                "Проверка {0}: {1} + {2} + {3} {4} {5}",
                Constants.StatNames()[stat], Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice),
                currentStat, (result ? ">=" : "<"), level));
        }

        public List<string> Test()
        {
            List<string> testLines = new List<string>();

            TestParam(Stat, Level, out bool testIsOk, out List<string> result);

            testLines.AddRange(result);
            testLines.Add(testIsOk ? "BIG|GOOD|АЛДАР СПРАВИЛСЯ :)" : "BIG|BAD|АЛДАР НЕ СПРАВИЛСЯ :(");

            if ((Benefit != null) && testIsOk)
                Benefit.Do();

            return testLines;
        }

        public List<string> TestAll()
        {
            bool testIsOk = true;
            List<string> testLines = new List<string>();
            List<string> tests = Stat.Split(',').Select(x => x.Trim()).ToList();
            Dictionary<string, int> levels = new Dictionary<string, int>();

            testLines.Add("BOLD|Определяем уровни проверок:");

            int allStats = 0;

            foreach (string test in tests)
                allStats += GetProperty(protagonist, test);

            testLines.Add(String.Format("Сумма всех параметров Алдара: {0}", allStats));

            double approximateStatUnit = (double)Level / (double)allStats;

            testLines.Add(String.Format("Условная средняя единица: {0} / {1} = {2:f1}", Level, allStats, approximateStatUnit));

            foreach (string test in tests)
            {
                int currentStat = GetProperty(protagonist, test);
                int approximateLevel = (int)Math.Floor(currentStat * approximateStatUnit);

                int finalLevel = 0;
                string approximateFix = String.Empty;

                if (currentStat == 12)
                {
                    finalLevel = approximateLevel - 6;
                    approximateFix = "- 6";
                }
                else
                {
                    finalLevel = approximateLevel + 2;
                    approximateFix = "+ 2";
                }

                levels.Add(test, finalLevel);

                testLines.Add(String.Format("Проверка {0}: {1} x {2:f1} = {3} {4} коррекция, итого {5}",
                    Constants.StatNames()[test], currentStat, approximateStatUnit, approximateLevel, approximateFix, finalLevel));
            }

            testLines.Add(String.Empty);
            testLines.Add("BOLD|Проходим проверки:");

            foreach (string test in tests)
            {
                TestParam(test, levels[test], out bool thisTestIsOk, out List<string> result);

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
            protagonist.Brother += 1;

            return new List<string> { "RELOAD" };
        }

        public List<string> GetSomething()
        {
            if (Benefit != null)
                Benefit.Do();

            Used = true;

            return new List<string> { "RELOAD" };
        }

        public List<string> Get()
        {
            if ((Price > 0) && (protagonist.Tanga >= Price))
            {
                protagonist.Tanga -= Price;

                Game.Option.Trigger(RemoveTrigger, remove: true);

                if (Benefit != null)
                    Benefit.Do();
            }
            else if (StatToMax && (protagonist.MaxBonus > 0))
            {
                SetProperty(protagonist, Stat, 12);
                protagonist.MaxBonus = 0;
            }
            else if (protagonist.StatBonuses >= 0)
            {
                int currentStat = GetProperty(protagonist, Stat);

                currentStat += (StatStep > 1 ? StatStep : 1);

                protagonist.GetType().GetProperty(Stat).SetValue(protagonist, currentStat);
                protagonist.StatBonuses -= 1;
            }

            return new List<string> { "RELOAD" };
        }
    }
}
