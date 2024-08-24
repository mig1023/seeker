using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.SeaTales.Parts
{
    class Third : IParts
    {
        public List<string> Status() => null;

        public List<string> AdditionalStatus() => new List<string>
        {
            $"Героизм: {Character.Protagonist.Heroism}",
            $"Алкоголизм: {Character.Protagonist.Alcoholism}",
            $"Авантюризм: {Character.Protagonist.Adventurism}",
            $"Плавание: {Character.Protagonist.Travel}",
            $"Репутация: {Character.Protagonist.Reputation}",
        };

        public List<string> Representer(Actions action)
        {
            return new List<string>();
        }

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;

            if (Character.Protagonist.Alcoholism >= 10)
            {
                toEndParagraph = 720;
                toEndText = "Алкоголизм зашкалил...";
            }
            else if (Character.Protagonist.Heroism >= 110)
            {
                toEndParagraph = 940;
                toEndText = "Героизм зашкалил!!";
            }
            else if (Character.Protagonist.Adventurism <= 0)
            {
                toEndParagraph = 1050;
                toEndText = "Авантюризм зашкалил...";
            }
            else if (Character.Protagonist.Adventurism >= 10)
            {
                toEndParagraph = 830;
                toEndText = "Авантюризм зашкалил!!";
            }
            else if (Character.Protagonist.Travel >= 100)
            {
                toEndParagraph = 755;
                toEndText = "Плавание достигло своей цели!";
            }
            else if ((Character.Protagonist.Travel <= -100) && !Game.Option.IsTriggered("Параграф 888"))
            {
                toEndParagraph = 888;
                toEndText = "Плавание занесло куда-то не туда...";
            }
            else if (Character.Protagonist.Reputation <= -100)
            {
                toEndParagraph = 1160;
                toEndText = "Репутация зашкалила...";
            }
            else
            {
                toEndText = String.Empty;
                return false;
            }

            return true;
        }

        private string GetTestLevel(out string heroism)
        {
            if (Character.Protagonist.Heroism > 100)
            {
                heroism = "выше 100";
                return "2,3,4,5,6";
            }
            else if (Character.Protagonist.Heroism > 75)
            {
                heroism = "выше 75";
                return "3,4,5,6";
            }
            else if (Character.Protagonist.Heroism > 50)
            {
                heroism = "выше 50";
                return "4,5,6";
            }
            else if (Character.Protagonist.Heroism > 25)
            {
                heroism = "выше 25";
                return "5,6";
            }
            else
            {
                heroism = "не превышает 25";
                return "6";
            }
        }

        public List<string> Test(Actions action)
        {
            List<string> test = new List<string>();

            string dices = GetTestLevel(out string heroism);
            List<int> targetDices = action.GetTragetDices(dices, out string dicesLine);

            test.Add($"BOLD|Нужно выдержать проверку!\n" +
                $"Ввиду того, что Героизм {heroism} нужно выбросить {dicesLine}\n");

            int dice = Game.Dice.Roll();
            test.Add($"Бросаем кубик: {Game.Dice.Symbol(dice)}");

            if (targetDices.Contains(dice))
            {
                test.Add("BIG|GOOD|BOLD|Проверка успешно пройдена! :)");
            }
            else
            {
                test.Add("BIG|BAD|BOLD|Проверка провалена :(");
            }

            return test;
        }
    }
}
