﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.BangkokSky
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public string Stat { get; set; }
        public int Level { get; set; }
        public string Skill { get; set; }
        public string Traits { get; set; }
        public string Bonuses { get; set; }
        public bool Free { get; set; }


        public override List<string> Status() => new List<string>
        {
            $"Ранений: {Character.Protagonist.Wounds} / 6",
            $"Уважение: {Character.Protagonist.Respect}",
            $"Деньги: {Character.Protagonist.Money} бат",
        };

        public override List<string> AdditionalStatus()
        {
            List<string> statusLines = new List<string>();

            if (Character.Protagonist.MartialArts > 0)
                statusLines.Add($"Боевые искусства: {Character.Protagonist.MartialArts}");

            if (Character.Protagonist.Physical > 0)
                statusLines.Add($"Физподготовка: {Character.Protagonist.Physical}");

            if (Character.Protagonist.Driving > 0)
                statusLines.Add($"Вождение: {Character.Protagonist.Driving}");

            if (Character.Protagonist.Firearms > 0)
                statusLines.Add($"Огнестрел: {Character.Protagonist.Firearms}");

            if (Character.Protagonist.Diplomacy > 0)
                statusLines.Add($"Дипломатия: {Character.Protagonist.Diplomacy}");

            if (Character.Protagonist.ConcreteJungle > 0)
                statusLines.Add($"Бетонные джунгли: {Character.Protagonist.ConcreteJungle}");

            return statusLines.Count <= 0 ? null : statusLines;
        }

        public override List<string> Representer()
        {
            if (!String.IsNullOrEmpty(Skill))
            {
                var allBonuses = Bonuses.Split(',').Select(x => x.Substring(0, x.IndexOf(':'))).ToList();
                string bonuses = String.Join(", ", allBonuses);

                return new List<string> { $"ПРОВЕРКА ПО НАВЫКУ {Constants.StatNames[Skill].ToUpper()}\n" +
                    $"Уровень сложности: -{Level}\nБонусы за: {bonuses}" };
            }
            else if (!String.IsNullOrEmpty(Stat))
            {
                return new List<string> { $"{Head}\n(текущее значение: " +
                    $"{GetProperty(Character.Protagonist, Stat)})" };
            }
            else if (Price > 0) 
            {
                return new List<string> { $"{Head}, {Price} бат" };
            }
            else if (Free)
            {
                return new List<string> { Head };
            }

            return new List<string>();
        }

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            bool getAvantages = (Type == "Get") && String.IsNullOrEmpty(Stat);
            bool triggeredAlready = getAvantages && Game.Option.IsTriggered(Trigger);
            bool advantagesCount = getAvantages && (Game.Data.Triggers.Count >= 4);

            bool min = false, max = false;

            if (Type == "Get-Decrease")
            {
                int value = GetProperty(Character.Protagonist, Stat);
                min = secondButton && (value < 1);
                max = !secondButton && ((value > 2) || (Character.Protagonist.StatBonuses <= 0));
            }
            else if (Price > 0)
            {
                return !Used && (Character.Protagonist.Money >= Price);
            }
            else if (Free)
            {
                int triggers = Game.Data.Triggers
                    .Where(x => x == "выбор")
                    .Count();

                return !Used && (triggers < 2);
            }

            return !(triggeredAlready || advantagesCount || min || max);
        }

        public List<string> Get()
        {
            if ((Price > 0) && (Character.Protagonist.Money >= Price))
            {
                Character.Protagonist.Money -= Price;

                if (!Multiple)
                    Used = true;

                if (Benefit != null)
                    Benefit.Do();
            }
            else if (Free)
            {
                Used = true;

                if (Benefit != null)
                    Benefit.Do();
            }
            else if (!String.IsNullOrEmpty(Stat))
            {
                ChangeProtagonistParam(Stat, Character.Protagonist, "StatBonuses");
            }

            return new List<string> { "RELOAD" };
        }

        public List<string> Decrease() =>
            ChangeProtagonistParam(Stat, Character.Protagonist, "StatBonuses", decrease: true);

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains("РАНЕНИЙ >="))
            {
                return Character.Protagonist.Wounds >= Game.Services.LevelParse(option);
            }
            else if (option.Contains("РАНЕНИЙ <"))
            {
                return Character.Protagonist.Wounds < Game.Services.LevelParse(option);
            }
            else if (option.Contains("УВАЖЕНИЕ >="))
            {
                return Character.Protagonist.Respect >= Game.Services.LevelParse(option);
            }
            else if (option.Contains("УВАЖЕНИЕ <"))
            {
                return Character.Protagonist.Respect < Game.Services.LevelParse(option);
            }
            else if (option.Contains("ДЕНЕГ >="))
            {
                return Character.Protagonist.Money >= Game.Services.LevelParse(option);
            }
            else
            {
                return AvailabilityTrigger(option.Trim());
            }
        }

        public List<string> Test()
        {
            List<string> lines = new List<string>();

            Game.Dice.DoubleRoll(out int positiveDice, out int negativeDice);

            lines.Add($"Кубик положительный: {Game.Dice.Symbol(positiveDice)}");
            lines.Add($"Кубик отрицательный: {Game.Dice.Symbol(negativeDice)}");

            int result = positiveDice - negativeDice;

            lines.Add($"Итого на кубиках получаем: {positiveDice} - {negativeDice} = {result}");

            int modificator = GetProperty(Character.Protagonist, Skill);

            if (modificator > 0)
            {
                int oldResult = result;
                result += modificator;

                lines.Add($"Добавляем модификатор {modificator} по способности {Constants.StatNames[Skill]}: " +
                    $"{oldResult} + {modificator} = {result}");
            }
            else
            {
                lines.Add($"Способность {Constants.StatNames[Skill]} равна нулю и не даёт никаких бонусов...");
            }

            List<string> bonuses = Bonuses.Split(',').ToList();

            foreach (string bonus in bonuses)
            {
                List<string> bonusParts = bonus.Trim().Split(':').ToList();
                string name = bonusParts[0];
                string value = bonusParts[1];

                if (Game.Option.IsTriggered(name))
                {
                    int oldResult = result;
                    result += int.Parse(value);

                    lines.Add($"Бонус {value} за то, что {name}: " +
                        $"{oldResult} + {value} = {result}");
                }
            }

            bool testIsOk = (result - Level) > 0;

            lines.Add($"Считаем по сложности {Constants.TestLevelNames[Level]}: " +
                $"{result} - {Level} = {result - Level}");

            if (testIsOk)
            {
                lines.Add("Результат равен или больше нуля!");
                lines.Add("GOOD|BIG|BOLD|Проверка пройдена!");
            }
            else
            {
                lines.Add("Результат ниже нуля!");
                lines.Add("BAD|BIG|BOLD|Проверка проавлена...");
            }

            Game.Buttons.Disable(testIsOk, "Успех", "Провал");

            return lines;
        }

        public List<string> RandomWounds()
        {
            List<string> lines = new List<string>();

            int dice = Game.Dice.Roll();
            string wounds = Game.Services.CoinsNoun(dice, "ранение", "ранения", "ранений");

            lines.Add($"BIG|BOLD|На кубике выпало: {Game.Dice.Symbol(dice)}");
            lines.Add($"BAD|Вы получили {dice} {wounds}");

            Character.Protagonist.Wounds += dice;

            return lines;
        }

        public override bool IsHealingEnabled() =>
            Character.Protagonist.Wounds > 0;

        public override void UseHealing(int healingLevel) =>
            Character.Protagonist.Wounds -= healingLevel;
    }
}
