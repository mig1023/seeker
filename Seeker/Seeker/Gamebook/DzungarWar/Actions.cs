using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.DzungarWar
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public string Untrigger { get; set; }

        public string Stat { get; set; }
        public int StatStep { get; set; }
        public bool StatToMax { get; set; }
        public int Level { get; set; }
        public string TriggerTestPenalty { get; set; }

        static bool NextTestWithTincture = false, NextTestWithGinseng = false, NextTestWithAirag = false;

        public override List<string> Representer()
        {
            if (Type == "TestAll")
            {
                return new List<string> { $"Проверить по совокупному уровню {Level}" };
            }
            else if (Level > 0)
            {
                int testResult = Tests.LevelWithPenalty(Level, out List<string> _,
                    ref NextTestWithTincture, ref NextTestWithGinseng, ref NextTestWithAirag, TriggerTestPenalty);

                return new List<string> { $"Проверка {Constants.StatNames[Stat]}, уровень {testResult}" };
            }
            else if (!String.IsNullOrEmpty(Stat) && !StatToMax)
            {
                int currentStat = GetProperty(Character.Protagonist, Stat);
                string diffLine = String.Empty;

                if (currentStat > 11)
                {
                    diffLine = " (максимум)";
                }
                else if (currentStat > 1)
                {
                    diffLine = $" (+{currentStat - 1})";
                }

                return new List<string> { $"{Head}{diffLine}" };
            }
            else if (Price > 0)
            {
                return new List<string> { $"{Head}, {Price} таньга" };
            }
            else if (!String.IsNullOrEmpty(Head))
            {
                return new List<string> { Head };
            }
            else
            {
                return new List<string> { };
            }
        }

        public override List<string> Status()
        {
            List<string> statusLines = new List<string>();

            if (Character.Protagonist.Tanga > 0)
                statusLines.Add($"Деньги: {Character.Protagonist.Tanga}");

            if (Character.Protagonist.Danger != null)
                statusLines.Add($"Опасность: {Character.Protagonist.Danger}");

            return statusLines;
        }

        public override List<string> AdditionalStatus()
        {
            List<string> statusLines = new List<string>();

            if (Character.Protagonist.Favour != null)
                statusLines.Add($"Благосклонность: {Character.Protagonist.Favour}");

            if (Character.Protagonist.Strength > 1)
                statusLines.Add($"Сила: {Character.Protagonist.Strength}");

            if (Character.Protagonist.Skill > 1)
                statusLines.Add($"Ловкость: {Character.Protagonist.Skill}");

            if (Character.Protagonist.Wisdom > 1)
                statusLines.Add($"Мудрость: {Character.Protagonist.Wisdom}");

            if (Character.Protagonist.Cunning > 1)
                statusLines.Add($"Хитрость: {Character.Protagonist.Cunning}");

            if (Character.Protagonist.Oratory > 1)
                statusLines.Add($"Красноречие: {Character.Protagonist.Oratory}");

            if (Character.Protagonist.Ginseng > 0)
                statusLines.Add($"Отвар: {Character.Protagonist.Ginseng}");

            if (Character.Protagonist.Tincture > 0)
                statusLines.Add($"Настойка: {Character.Protagonist.Tincture}");

            return statusLines.Count <= 0 ? null : statusLines;
        }

        public override List<string> StaticButtons()
        {
            List<string> staticButtons = new List<string> { };

            if (!Game.Buttons.ExistsInParagraph(actionName: "TEST"))
                return staticButtons;

            if ((Character.Protagonist.Tincture > 0) && !NextTestWithTincture)
                staticButtons.Add("ВЫПИТЬ НАСТОЙКИ");

            if ((Character.Protagonist.Ginseng > 0) && !NextTestWithGinseng)
                staticButtons.Add("ВЫПИТЬ ОТВАР ЖЕНЬШЕНЯ");

            if ((Character.Protagonist.Airag > 0) && !NextTestWithAirag)
                staticButtons.Add("ВЫПИТЬ АЙРАГА");

            return staticButtons;
        }

        public override bool StaticAction(string action)
        {
            if (action == "ВЫПИТЬ НАСТОЙКИ")
            {
                Character.Protagonist.Tincture -= 1;
                NextTestWithTincture = true;
            }
            else if (action == "ВЫПИТЬ ОТВАР ЖЕНЬШЕНЯ")
            {
                Character.Protagonist.Ginseng -= 1;
                NextTestWithGinseng = true;
            }
            else if (action == "ВЫПИТЬ АЙРАГА")
            {
                Character.Protagonist.Airag -= 1;
                NextTestWithAirag = true;
            }
            else
            {
                return false;
            }

            return true;
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

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            if (Level > 0)
            {
                return true;
            }
            else if (Used)
            {
                return false;
            }
            else if (Type == "Brother")
            {
                return Character.Protagonist.Brother <= 0;
            }
            else if (StatToMax)
            {
                return Character.Protagonist.MaxBonus > 0;
            }
            else if (!String.IsNullOrEmpty(Stat))
            {
                int stat = GetProperty(Character.Protagonist, Stat);

                if (secondButton)
                    return (stat > 1) && (stat < 12);
                else 
                    return ((Character.Protagonist.StatBonuses > 0) && (stat < 12));
            }
            else if (Price >= 0)
            {
                return (Character.Protagonist.Tanga >= Price);
            }
            else
            {
                return true;
            }
        }

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains("|"))
            {
                return option.Split('|').Where(x => Game.Option.IsTriggered(x.Trim())).Count() > 0;
            }
            else if (option.Contains(";"))
            {
                string[] options = option.Split(';');

                int optionMustBe = int.Parse(options[0]);
                int optionCount = options.Where(x => Game.Option.IsTriggered(x.Trim())).Count();

                return optionCount >= optionMustBe;
            }
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains(">") || oneOption.Contains("<"))
                    {
                        int level = Game.Services.LevelParse(oneOption);

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
                        if (Game.Option.IsTriggered(oneOption.Replace("!", String.Empty).Trim()))
                            return false;
                    }
                    else if (!Game.Option.IsTriggered(oneOption.Trim()))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public List<string> Test()
        {
            List<string> testLines = new List<string>();

            Tests.Param(Stat, Level, out bool testIsOk, out List<string> result,
                ref NextTestWithTincture, ref NextTestWithGinseng, ref NextTestWithAirag,
                GetProperty(Character.Protagonist, Stat), TriggerTestPenalty);

            testLines.AddRange(result);
            testLines.Add(Result(testIsOk, "АЛДАР СПРАВИЛСЯ", "АЛДАР НЕ СПРАВИЛСЯ"));

            Game.Buttons.Disable(testIsOk, "В случае успеха, Обе проверки успешны", "В случае провала");

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
                allStats += GetProperty(Character.Protagonist, test);

            testLines.Add($"Сумма всех параметров Алдара: {allStats}");

            double approximateStatUnit = (double)Level / (double)allStats;

            testLines.Add($"Условная средняя единица: {Level} / {allStats} = {approximateStatUnit:f1}");

            foreach (string test in tests)
            {
                int currentStat = GetProperty(Character.Protagonist, test);
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

                testLines.Add($"Проверка {Constants.StatNames[test]}: " +
                    $"{currentStat} x {approximateStatUnit:f1} = " +
                    $"{approximateLevel} {approximateFix} коррекция, " +
                    $"итого {finalLevel}");
            }

            testLines.Add(String.Empty);
            testLines.Add("BOLD|Проходим проверки:");

            foreach (string test in tests)
            {
                Tests.Param(test, levels[test], out bool thisTestIsOk, out List<string> result,
                    ref NextTestWithTincture, ref NextTestWithGinseng, ref NextTestWithAirag,
                    GetProperty(Character.Protagonist, test), TriggerTestPenalty);

                testLines.AddRange(result);
                testLines.Add(thisTestIsOk ? "GOOD|Алдар справился" : "BAD|Алдар не справился");

                if (!thisTestIsOk)
                    testIsOk = false;
            }

            testLines.Add(Result(testIsOk, "АЛДАР СПРАВИЛСЯ", "АЛДАР НЕ СПРАВИЛСЯ"));

            Game.Buttons.Disable(testIsOk, "Все четыре проверки успешны");

            return testLines;
        }

        public List<string> Brother()
        {
            Character.Protagonist.Brother += 1;

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
            if ((Price > 0) && (Character.Protagonist.Tanga >= Price))
            {
                Character.Protagonist.Tanga -= Price;

                Game.Option.Trigger(Untrigger, remove: true);

                if (Benefit != null)
                    Benefit.Do();
            }
            else if (StatToMax && (Character.Protagonist.MaxBonus > 0))
            {
                SetProperty(Character.Protagonist, Stat, 12);
                Character.Protagonist.MaxBonus = 0;
            }
            else if (Character.Protagonist.StatBonuses >= 0)
            {
                SetProperty(Character.Protagonist, Stat, GetProperty(Character.Protagonist, Stat) + (StatStep > 1 ? StatStep : 1));
                Character.Protagonist.StatBonuses -= 1;
            }

            return new List<string> { "RELOAD" };
        }

        public List<string> Decrease() =>
            ChangeProtagonistParam(Stat, Character.Protagonist, "StatBonuses", decrease: true);
    }
}
