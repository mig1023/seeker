using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.SeaTales
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public string Dices { get; set; }

        public int Throws { get; set; }

        public int Heat { get; set; }

        public string Level { get; set; }

        public string Success { get; set; }

        public override List<string> Status()
        {
            if (Constants.ThisIsFirstPart())
            {
                string negativ = Character.Protagonist.Heat < 0 ? "—" : String.Empty;
                return new List<string> { $"Теплота: {negativ}{Math.Abs(Character.Protagonist.Heat)} ℃" };
            }
            else
            {
                return new List<string> { $"Брехня: {Character.Protagonist.Nonsense}" };
            }
        }

        private List<int> GetTragetDices(string dices, out string dicesLine)
        {
            List<int> targets = dices
                .Split(',')
                .Select(x => int.Parse(x))
                .ToList();

            dicesLine = String.Empty;

            if (targets.Count < 2)
            {
                dicesLine = Game.Dice.Symbol(targets[0]);
            }
            else
            {
                for (int i = 0; i < targets.Count; i += 1)
                {
                    if (i > 0)
                        dicesLine += i == targets.Count - 1 ? " или " : ", ";

                    dicesLine += Game.Dice.Symbol(targets[i]);
                }
            }

            return targets;
        }

        private string GetCredibilityLevel(string level, out string levelNamePart)
        {
            switch (level)
            {
                case "Difficult":
                    levelNamePart = "сложн";
                    return "6";
                case "Standard":
                    levelNamePart = "стандартн";
                    return "5,6";
                default:
                    levelNamePart = "лёгк";
                    return "4,5,6";
            }
        }

        public override List<string> Representer()
        {
            if (Constants.ThisIsFirstPart())
            {
                List<int> targetDices = GetTragetDices(Dices, out string dicesLine);

                string bonus = Heat > 0 ? $"если получится, то +{Heat} Тепла" :
                    "если получится, то игра продолжится";

                return new List<string> { $"Нужно выбросить {dicesLine}\n" +
                    $"за {Constants.Throws[Throws]}\n{bonus}" };
            }
            else
            {
                List<int> _ = GetTragetDices(
                    GetCredibilityLevel(Level, out string levelNamePart), out string dicesLine);

                return new List<string> { $"{levelNamePart.ToUpper()}АЯ ПРОВЕРКА\n" +
                    $"Нужно выбросить {dicesLine}" };
            }
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText)
        {
            if (Constants.ThisIsFirstPart())
            {
                toEndParagraph = 0;

                if (Character.Protagonist.Heat <= -100)
                {
                    toEndText = "Температура снизилась слишком сильно... Ваше плавание провалено... Надо начинать игру сначала...";
                    return true;
                }
                else if (Character.Protagonist.Gameover)
                {
                    toEndText = "Ваше плавание подошло к печальному концу... Надо начинать игру сначала...";
                    return true;
                }
                else if (Character.Protagonist.Heat >= 100)
                {
                    toEndText = "Температура достаточно повысилась! Вы победили! Остаётся только начать с начала!";
                    return true;
                }
                else
                {
                    toEndText = String.Empty;
                    return false;
                }
            }
            else
            {
                toEndParagraph = 0;

                if ((Character.Protagonist.Nonsense >= 100) && (Game.Data.CurrentParagraphID == 544))
                {
                    toEndText = Output.Constants.GAMEOVER_TEXT;
                    return true;
                }
                else if (Character.Protagonist.Nonsense >= 100)
                {
                    toEndParagraph = 544;
                    toEndText = "Индекс брехни зашкалил!";
                    return true;
                }
                else
                {
                    toEndText = String.Empty;
                    return false;
                }
            }
        }

        public List<string> Test()
        {
            List<string> test = new List<string>();

            List<int> targetDices = GetTragetDices(Dices, out string dicesLine);

            test.Add($"BOLD|Пробуем выбросить {dicesLine} за {Constants.Throws[Throws]}\n");

            for (int i = 1; i <= Throws; i += 1)
            {
                string num = String.Empty;

                if (Throws > 1)
                    num = $", попытка {i}";

                int dice = Game.Dice.Roll();
                test.Add($"Бросаем{num}: {Game.Dice.Symbol(dice)}");

                if (targetDices.Contains(dice))
                {
                    test.Add("BIG|GOOD|BOLD|Успех! :)");

                    if (Heat > 0)
                    {
                        Character.Protagonist.Heat += Heat;
                        test.Add($"Вы получаете {Heat} Тепла!");
                    }
                    else
                    {
                        test.Add($"Ваше путешествие продолжается!");
                    }
                   
                    return test;
                }
                else
                {
                    string fail = i > 1 ? "Опять" : "Нет,";
                    test.Add($"{fail} выпал неправильный кубик...\n");
                }
            }

            test.Add("BIG|BAD|BOLD|Неудачно :(");

            if (Heat <= 0)
            {
                test.Add($"Здесь ваше путешествие заканчивается...");
                Character.Protagonist.Gameover = true;
            }

            return test;
        }

        public List<string> Credibility()
        {
            List<string> test = new List<string>();

            if (!Constants.ThisIsFirstPart())
                Character.Protagonist.NeedCredibilityCheck = false;

            List<int> targetDices = GetTragetDices(
                GetCredibilityLevel(Level, out string levelNamePart), out string dicesLine);

            test.Add($"BOLD|Нужно выдержать {levelNamePart}ую проверку!\nПробуем выбросить {dicesLine}\n");

            int dice = Game.Dice.Roll();
            test.Add($"Бросаем кубик: {Game.Dice.Symbol(dice)}");

            if (targetDices.Contains(dice))
            {
                test.Add("BIG|GOOD|BOLD|Слушают! :)");
                test.Add(Success);
                return test;
            }
            else
            {
                Character.Protagonist.Nonsense = 0;

                test.Add("BIG|BAD|BOLD|Никто не верит :(");
                test.Add("\nРассказчик наплёл совсем уж околесицу - в такое никто не поверит! " +
                    "Его перебивает другой выпивоха и начинает свой рассказ.");
                test.Add("BOLD|Пункты Брехни обнуляются.");
            }

            return test;
        }
    }
}
