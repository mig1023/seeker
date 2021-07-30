using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.DzungarWar
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protogonist = Character.Protagonist;

        public string RemoveTrigger { get; set; }

        public string Stat { get; set; }
        public int StatStep { get; set; }
        public bool StatToMax { get; set; }
        public int Level { get; set; }

        public string TriggerTestPenalty { get; set; }

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
            if (Name == "TestAll")
                return new List<string> { String.Format("Проверить по совокупному уровню {0}", Level) };

            else if (Level > 0)
                return new List<string> { String.Format("Проверка {0}, уровень {1}",
                    Constants.StatNames()[Stat], TestLevelWithPenalty(Level, out List<string> _)) };

            else if (!String.IsNullOrEmpty(Stat) && !StatToMax)
            {
                int currentStat = (int)protogonist.GetType().GetProperty(Stat).GetValue(protogonist, null);
                string diffLine = (currentStat > 1 ? String.Format(" (+{0})", (currentStat - 1)) : String.Empty);

                return new List<string> { String.Format("{0}{1}", Text, diffLine) };
            }
            else if (Price > 0)
                return new List<string> { String.Format("{0}, {1} таньга", Text, Price) };

            else if (!String.IsNullOrEmpty(Text))
                return new List<string> { Text };

            else
                return new List<string> { };
        }

        public override List<string> Status()
        {
            List<string> statusLines = new List<string>();

            if (protogonist.Tanga > 0)
                statusLines.Add(String.Format("Деньги: {0}", protogonist.Tanga));

            if (protogonist.Tincture > 0)
                statusLines.Add(String.Format("Настойка: {0}", protogonist.Tincture));

            if (protogonist.Ginseng > 0)
                statusLines.Add(String.Format("Отвар: {0}", protogonist.Ginseng));

            if (protogonist.Favour != null)
                statusLines.Add(String.Format("Благосклонность: {0}", protogonist.Favour));

            if (protogonist.Danger != null)
                statusLines.Add(String.Format("Опасность: {0}", protogonist.Danger));

            return statusLines;
        }

        public override List<string> AdditionalStatus()
        {
            List<string> statusLines = new List<string>();

            if (protogonist.Strength > 1)
                statusLines.Add(String.Format("Сила: {0}", protogonist.Strength));

            if (protogonist.Skill > 1)
                statusLines.Add(String.Format("Ловкость: {0}", protogonist.Skill));

            if (protogonist.Wisdom > 1)
                statusLines.Add(String.Format("Мудрость: {0}", protogonist.Wisdom));

            if (protogonist.Cunning > 1)
                statusLines.Add(String.Format("Хитрость: {0}", protogonist.Cunning));

            if (protogonist.Oratory > 1)
                statusLines.Add(String.Format("Красноречие: {0}", protogonist.Oratory));

            if (statusLines.Count <= 0)
                return null;

            return statusLines;
        }

        public override List<string> StaticButtons()
        {
            List<string> staticButtons = new List<string> { };

            if (Game.Checks.ExistsInParagraph(actionName: "TEST") && (protogonist.Tincture > 0) && !NextTestWithTincture)
                staticButtons.Add("ВЫПИТЬ НАСТОЙКИ");

            if (Game.Checks.ExistsInParagraph(actionName: "TEST") && (protogonist.Ginseng > 0) && !NextTestWithGinseng)
                staticButtons.Add("ВЫПИТЬ ОТВАР ЖЕНЬШЕНЯ");

            return staticButtons;
        }

        public override bool StaticAction(string action)
        {
            if (action == "ВЫПИТЬ НАСТОЙКИ")
            {
                protogonist.Tincture -= 1;
                NextTestWithTincture = true;
            }

            else if (action == "ВЫПИТЬ ОТВАР ЖЕНЬШЕНЯ")
            {
                protogonist.Ginseng -= 1;
                NextTestWithGinseng = true;
            }
            else
                return false;

            return true;
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText)
        {
            bool dangerEnd = protogonist.Danger >= 12;

            if ((Game.Data.CurrentParagraphID == 106) || (Game.Data.CurrentParagraphID == 148))
            {
                toEndParagraph = 122;
                toEndText = "Далее";

                protogonist.Danger = null;
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

            else if (Used)
                return false;

            else if (Name == "Brother")
                return protogonist.Brother <= 0;

            else if (StatToMax)
                return protogonist.MaxBonus > 0;

            else if (!String.IsNullOrEmpty(Stat))
            {
                int currentStat = (int)protogonist.GetType().GetProperty(Stat).GetValue(protogonist, null);
                return ((protogonist.StatBonuses > 0) && (currentStat < 12));
            }

            else if (Price >= 0)
                return (protogonist.Tanga >= Price);

            else
                return true;
        }

        public override bool CheckOnlyIf(string option)
        {
            if (option.Contains("|"))
                return option.Split('|').Where(x => Game.Data.Triggers.Contains(x.Trim())).Count() > 0;

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

                        if (oneOption.Contains("ТАНЬГА >=") && (level > protogonist.Tanga))
                            return false;

                        else if (oneOption.Contains("ОПАСНОСТЬ >") && (level >= protogonist.Danger))
                            return false;

                        else if (oneOption.Contains("ОПАСНОСТЬ <=") && (level < protogonist.Danger))
                            return false;

                        else if (oneOption.Contains("БЛАГОСКЛОННОСТЬ >") && (level >= protogonist.Favour))
                            return false;

                        else if (oneOption.Contains("БЛАГОСКЛОННОСТЬ <=") && (level < protogonist.Favour))
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
            int currentStat = (int)protogonist.GetType().GetProperty(stat).GetValue(protogonist, null);

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
                allStats += (int)protogonist.GetType().GetProperty(test).GetValue(protogonist, null);

            testLines.Add(String.Format("Сумма всех параметров Алдара: {0}", allStats));

            double approximateStatUnit = (double)Level / (double)allStats;

            testLines.Add(String.Format("Условная средняя единица: {0} / {1} = {2:f1}", Level, allStats, approximateStatUnit));

            foreach (string test in tests)
            {
                int currentStat = (int)protogonist.GetType().GetProperty(test).GetValue(protogonist, null);
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
            protogonist.Brother += 1;

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
            if ((Price > 0) && (protogonist.Tanga >= Price))
            {
                protogonist.Tanga -= Price;

                Game.Option.Trigger(RemoveTrigger, remove: true);

                if (Benefit != null)
                    Benefit.Do();
            }
            else if (StatToMax && (protogonist.MaxBonus > 0))
            {
                protogonist.GetType().GetProperty(Stat).SetValue(protogonist, 12);
                protogonist.MaxBonus = 0;
            }
            else if (protogonist.StatBonuses >= 0)
            {
                int currentStat = (int)protogonist.GetType().GetProperty(Stat).GetValue(protogonist, null);

                currentStat += (StatStep > 1 ? StatStep : 1);

                protogonist.GetType().GetProperty(Stat).SetValue(protogonist, currentStat);
                protogonist.StatBonuses -= 1;
            }

            return new List<string> { "RELOAD" };
        }
    }
}
