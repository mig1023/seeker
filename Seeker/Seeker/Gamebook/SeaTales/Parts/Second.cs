using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.SeaTales.Parts
{
    class Second : IParts
    {
        public List<string> Status() =>
            new List<string> { $"Брехня: {Character.Protagonist.Nonsense}" };

        public List<string> AdditionalStatus() => null;

        public List<string> Representer(Actions action)
        {
            string dices = GetCredibilityLevel(action.Level, out string levelNamePart);
            List<int> _ = action.GetTragetDices(dices, out string dicesLine);

            return new List<string> {
                $"{levelNamePart.ToUpper()}АЯ ПРОВЕРКА\n" +
                $"Нужно выбросить {dicesLine}" };
        }

        public bool GameOver(out int toEndParagraph, out string toEndText)
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

        private string GetCredibilityLevel(string level, out string levelNamePart)
        {
            switch (level)
            {
                case "Difficult":
                    levelNamePart = "сложн";
                    return "4,6";
                case "Standard":
                    levelNamePart = "стандартн";
                    return "4,5,6";
                default:
                    levelNamePart = "лёгк";
                    return "3,4,5,6";
            }
        }

        public List<string> RandomOption() =>
            new List<string>();

        public List<string> Test(Actions action)
        {
            List<string> test = new List<string>();

            Character.Protagonist.NeedCredibilityCheck = false;

            List<int> targetDices = action.GetTragetDices(
                GetCredibilityLevel(action.Level, out string levelNamePart), out string dicesLine);

            test.Add($"BOLD|Нужно выдержать {levelNamePart}ую проверку!\nПробуем выбросить {dicesLine}\n");

            int dice = Game.Dice.Roll();
            test.Add($"Бросаем кубик: {Game.Dice.Symbol(dice)}");

            if (targetDices.Contains(dice))
            {
                test.Add("BIG|GOOD|BOLD|Слушают! :)");
                test.Add(action.Success);
            }
            else
            {
                Character.Protagonist.Nonsense = 0;

                test.Add("BIG|BAD|BOLD|Никто не верит :(");
                test.Add("\nРассказчик наплёл совсем уж околесицу - в такое никто не поверит! " +
                    "Его перебивает другой выпивоха и начинает свой рассказ.");
                test.Add("BOLD|Пункты Брехни обнуляются.");
            }

            Game.Buttons.Rename("Пропустить", "Далее");

            return test;
        }
    }
}
