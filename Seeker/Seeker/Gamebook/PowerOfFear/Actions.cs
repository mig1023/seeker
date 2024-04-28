using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.PowerOfFear
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public string Skill { get; set; }
        public int Level { get; set; }

        public override List<string> Status() => new List<string>
        {
            $"Здоровье: {Character.Protagonist.Hitpoints}",
        };

        public override List<string> AdditionalStatus()
        {
            List<string> statusLines = new List<string>();

            if (Character.Protagonist.Hunting > 0)
                statusLines.Add($"Охота: {Character.Protagonist.Hunting}");

            if (Character.Protagonist.Agility > 0)
                statusLines.Add($"Ловкость: {Character.Protagonist.Agility}");

            if (Character.Protagonist.Luck > 0)
                statusLines.Add($"Удача: {Character.Protagonist.Luck}");

            if (Character.Protagonist.Weapon > 0)
                statusLines.Add($"Владение оружием: {Character.Protagonist.Weapon}");

            if (Character.Protagonist.Theft > 0)
                statusLines.Add($"Взлом: {Character.Protagonist.Theft}");

            if (Character.Protagonist.Stealth > 0)
                statusLines.Add($"Скрытность: {Character.Protagonist.Stealth}");

            if (Character.Protagonist.Knowledge > 0)
                statusLines.Add($"Знания: {Character.Protagonist.Knowledge}");

            if (Character.Protagonist.Strength > 0)
                statusLines.Add($"Сила: {Character.Protagonist.Strength}");

            return statusLines.Count <= 0 ? null : statusLines;
        }

        public override List<string> Representer()
        {
            if (Type == "Test")
            {
                return new List<string> { $"ПРОВЕРКА ПО НАВЫКУ " +
                    $"{Constants.PropertiesNames[Skill].ToUpper()}" };
            }
            else if (!String.IsNullOrEmpty(Skill))
            {
                return new List<string> { $"{Head}\n(текущее значение: " +
                    $"{GetProperty(Character.Protagonist, Skill)})" };
            }

            return new List<string>();
        }

        private int GetPropertiesCountByLevel(int level = 0)
        {
            int count = 0;

            foreach (string name in Constants.PropertiesNames.Keys)
            {
                bool anyLevel = level == 0;
                int current = GetProperty(Character.Protagonist, name);

                if (!anyLevel && (current == level))
                    count += 1;

                if (anyLevel && (current > 0))
                    count += 1;
            }

            return count;
        }

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            bool disabled = false;

            if (Type == "Get-Decrease")
            {
                int value = GetProperty(Character.Protagonist, Skill);
                bool min = secondButton && (value <= 0);
                bool max = !secondButton && (value >= 8);

                int countMax = GetPropertiesCountByLevel(8);
                bool alreadyAll = (GetPropertiesCountByLevel() >= 5) && (value <= 0);
                bool alreadyMax = (countMax >= 3) && (GetPropertiesCountByLevel(7) >= 2);
                bool alreadyMiddle = (countMax >= 3) && (value >= 7);
                bool count = (alreadyAll || alreadyMax || alreadyMiddle) && !secondButton;

                disabled = (min || max || count);
            }

            if (Type == "Test")
            {
                disabled = GetProperty(Character.Protagonist, Skill) <= 0;
            }

            return !disabled;
        }

        private int GetSteps(int current)
        {
            if (current == 0)
                return 7;

            if (current == 7)
                return 1;

            return 0;
        }

        public List<string> Get()
        {
            int current = GetProperty(Character.Protagonist, Skill);
            int bonus = GetSteps(current);
            SetProperty(Character.Protagonist, Skill, current + bonus);

            return new List<string> { "RELOAD" };
        }

        private int DecreaseSteps(int current)
        {
            if (current == 8)
                return 1;

            if (current == 7)
                return 7;

            return 0;
        }

        public List<string> Decrease()
        {
            int current = GetProperty(Character.Protagonist, Skill);
            int decrease = DecreaseSteps(current);
            SetProperty(Character.Protagonist, Skill, current - decrease);

            return new List<string> { "RELOAD" };
        }

        public List<string> Test()
        {
            List<string> lines = new List<string>();

            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            int result = firstDice + secondDice;

            lines.Add($"Кубики: {Game.Dice.Symbol(firstDice)} + " +
                $"{Game.Dice.Symbol(secondDice)} = {result}");

            int level = GetProperty(Character.Protagonist, Skill);

            lines.Add($"Уровень навыка «{Constants.PropertiesNames[Skill]}» равен {level}");

            if (result > level)
            {
                lines.Add($"Выпавшее значение {result} больше уровня навыка!");
                lines.Add($"BIG|BAD|BOLD|Проверка провалена :(");

                Game.Buttons.Disable("Проверка удачна, Вам повезло");
            }
            else
            {
                lines.Add($"Выпавшее значение {result} непревышает уровня навыка!");
                lines.Add($"BIG|GOOD|BOLD|Проверка успешно пройдена :)");

                Game.Buttons.Disable("Проверка неудачна или же навык отсутствует, " +
                    "Удача от вас отвернулась, Не получилось");
            }

            return lines;
        }
    }
}
