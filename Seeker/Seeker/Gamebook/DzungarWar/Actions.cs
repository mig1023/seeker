using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.DzungarWar
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();

        public string RemoveTrigger { get; set; }

        public string Text { get; set; }
        public string Stat { get; set; }
        public int StatStep { get; set; }
        public bool StatToMax { get; set; }
        public int Level { get; set; }
        public int Price { get; set; }

        public string TriggerTestPenalty { get; set; }

        public Modification Benefit { get; set; }

        static bool NextTestWithTincture = false, NextTestWithGinseng = false;

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

        public override List<string> Representer()
        {
            if (!String.IsNullOrEmpty(Stat) && !StatToMax)
            {
                int currentStat = (int)Character.Protagonist.GetType().GetProperty(Stat).GetValue(Character.Protagonist, null);

                string diffLine = (currentStat > 1 ? String.Format(" (+{0})", (currentStat - 1)) : String.Empty);

                return new List<string> { String.Format("{0}{1}", Text, diffLine) };
            }

            else if (ActionName == "TestAll")
                return new List<string> { String.Format("Проверить по совокупному уровню {0}", Level) };

            else if (Level > 0)
                return new List<string> { String.Format("Проверка {0}, уровень {1}",
                    Constants.StatNames()[Stat], TestLevelWithPenalty(Level, out List<string> _)) };

            else if (!String.IsNullOrEmpty(Text))
                return new List<string> { Text };

            else
                return new List<string> { };
        }

        public override List<string> Status()
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

        public override List<string> AdditionalStatus()
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

        public override List<string> StaticButtons()
        {
            List<string> staticButtons = new List<string> { };

            if (Game.Checks.ExistsInParagraph(actionName: "TEST") && (Character.Protagonist.Tincture > 0) && !NextTestWithTincture)
                staticButtons.Add("ВЫПИТЬ НАСТОЙКИ");

            if (Game.Checks.ExistsInParagraph(actionName: "TEST") && (Character.Protagonist.Ginseng > 0) && !NextTestWithGinseng)
                staticButtons.Add("ВЫПИТЬ ОТВАР ЖЕНЬШЕНЯ");

            return staticButtons;
        }

        public override bool StaticAction(string action)
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

        public override bool GameOver(out int toEndParagraph, out string toEndText)
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

        public override bool IsButtonEnabled()
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

        public override bool CheckOnlyIf(string option)
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
                        int level = int.Parse(oneOption.Split('>', '=')[1]);

                        if (oneOption.Contains("ТАНЬГА >=") && (level > Character.Protagonist.Tanga))
                            return false;
                        else if (oneOption.Contains("ОПАСНОСТЬ >") && (level >= Character.Protagonist.Danger))
                            return false;
                        else if (oneOption.Contains("ОПАСНОСТЬ <=") && (level < Character.Protagonist.Danger))
                            return false;
                        else if (oneOption.Contains("БЛАГОСКЛОННОСТЬ >") && (level >= Character.Protagonist.Favour))
                            return false;
                        else if (oneOption.Contains("БЛАГОСКЛОННОСТЬ <=") && (level < Character.Protagonist.Favour))
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
                allStats += (int)Character.Protagonist.GetType().GetProperty(test).GetValue(Character.Protagonist, null);

            testLines.Add(String.Format("Сумма всех параметров Алдара: {0}", allStats));

            double approximateStatUnit = (double)Level / (double)allStats;

            testLines.Add(String.Format("Условная средняя единица: {0} / {1} = {2:f1}", Level, allStats, approximateStatUnit));

            foreach (string test in tests)
            {
                int currentStat = (int)Character.Protagonist.GetType().GetProperty(test).GetValue(Character.Protagonist, null);
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
    }
}
