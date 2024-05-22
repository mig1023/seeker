using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.WrongWayGoBack
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public override List<string> Status()
        {
            if (Character.Protagonist.Time > 0)
            {
                TimeSpan time = TimeSpan.FromSeconds(Character.Protagonist.Time);
                return new List<string> { "Оставшееся время:" + time.ToString(@"hh\:mm\:ss") };
            }
            else
            {
                return new List<string> { "Оставшееся время: 00:00:00" };
            }
        }

        public override List<string> AdditionalStatus() => new List<string>
        {
            $"Мастерство: {Character.Protagonist.Skill}",
            $"Выносливость: {Character.Protagonist.Hitpoints}",
            $"Удача: {Character.Protagonist.Luck}",
        };

        public override bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 33;
            toEndText = String.Empty;

            if (Game.Data.CurrentParagraphID == 33)
            {
                return false;
            }
            else if (Character.Protagonist.Time <= 0)
            {
                toEndText = "Время истекло...";
                return true;
            }
            else if (Character.Protagonist.Hitpoints <= 0)
            {
                toEndText = "Начать сначала";
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<string> Skill()
        {
            List<string> lines = new List<string>();

            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            int result = firstDice + secondDice;

            lines.Add($"Кубики: {Game.Dice.Symbol(firstDice)} + " +
                $"{Game.Dice.Symbol(secondDice)}");

            if (result <= Character.Protagonist.Skill)
            {
                lines.Add($"Выпавшее значение {result} не превышает уровень Мастерства!");
                lines.Add($"BIG|GOOD|BOLD|Проверка успешно пройдена :)");
               
                Game.Buttons.Disable("Промахнулся");
            }
            else
            {
                lines.Add($"Выпавшее значение {result} больше уровня Мастерства!");
                lines.Add($"BIG|BAD|BOLD|Проверка провалена :(");

                Game.Buttons.Disable("Ты попал");
            }

            return lines;
        }

        public List<string> Luck()
        {
            List<string> lines = new List<string>();

            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            int result = firstDice + secondDice;

            lines.Add($"Кубики: {Game.Dice.Symbol(firstDice)} + " +
                $"{Game.Dice.Symbol(secondDice)}");

            if (result <= Character.Protagonist.Luck)
            {
                lines.Add($"Выпавшее значение {result} не превышает уровень Удачи!");
                lines.Add($"BIG|GOOD|BOLD|Удача на вашей стороне :)");

                Game.Buttons.Disable("Она подвела тебя");
            }
            else
            {
                lines.Add($"Выпавшее значение {result} больше уровня Удачи!");
                lines.Add($"BIG|BAD|BOLD|Удача отвернулась от вас :(");

                Game.Buttons.Disable("Она на твоей стороне");
            }

            if (Character.Protagonist.Luck > 2)
            {
                Character.Protagonist.Luck -= 1;
                lines.Add("Уровень Удачи снижен на единицу");
            }

            return lines;
        }
    }
}
