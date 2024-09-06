using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.SeaTales.Parts
{
    class First : IParts
    {
        public List<string> Status()
        {
            string negativ = Character.Protagonist.Heat < 0 ? "—" : String.Empty;
            return new List<string> { $"Теплота: {negativ}{Math.Abs(Character.Protagonist.Heat)} ℃" };
        }

        public List<string> AdditionalStatus() => null;

        public bool Availability(string option) => true;

        public bool IsButtonEnabled(Actions action) => true;

        public List<string> Representer(Actions action)
        {
            List<int> targetDices = action.GetTragetDices(action.Dices, out string dicesLine);

            string bonus = action.Heat > 0 ? $"если получится, то +{action.Heat} Тепла" :
                "если получится, то игра продолжится";

            return new List<string> { $"Нужно выбросить {dicesLine}\n" +
                $"за {Constants.Throws[action.Throws]}\n{bonus}" };
        }

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;

            if (Character.Protagonist.Heat <= -100)
            {
                toEndText = "Температура снизилась слишком сильно... " +
                    "Ваше плавание провалено... Надо начинать игру сначала...";

                return true;
            }
            else if (Character.Protagonist.Gameover)
            {
                toEndText = "Ваше плавание подошло к печальному концу... " +
                    "Надо начинать игру сначала...";

                return true;
            }
            else if (Character.Protagonist.Heat >= 100)
            {
                toEndText = "Температура достаточно повысилась! Вы победили! " +
                    "Остаётся только начать с начала!";

                return true;
            }
            else
            {
                toEndText = String.Empty;
                return false;
            }
        }

        public List<string> RandomOption() =>
            new List<string>();

        public List<string> Test(Actions action)
        {
            List<string> test = new List<string>();

            List<int> targetDices = action.GetTragetDices(action.Dices, out string dicesLine);

            test.Add($"BOLD|Пробуем выбросить {dicesLine} за {Constants.Throws[action.Throws]}\n");

            for (int i = 1; i <= action.Throws; i += 1)
            {
                string num = String.Empty;

                if (action.Throws > 1)
                    num = $", попытка {i}";

                int dice = Game.Dice.Roll();
                test.Add($"Бросаем{num}: {Game.Dice.Symbol(dice)}");

                if (targetDices.Contains(dice))
                {
                    test.Add("BIG|GOOD|BOLD|Успех! :)");

                    if (action.Heat > 0)
                    {
                        Character.Protagonist.Heat += action.Heat;
                        test.Add($"BOLD|Вы получаете {action.Heat} Тепла!");
                    }
                    else
                    {
                        test.Add($"BOLD|Ваше путешествие продолжается!");
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

            if (action.Heat <= 0)
            {
                test.Add($"Здесь ваше путешествие заканчивается...");
                Character.Protagonist.Gameover = true;
            }

            return test;
        }
    }
}
