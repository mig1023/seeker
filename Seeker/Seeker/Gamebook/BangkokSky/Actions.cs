using System;
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
        public int Fighter { get; set; }


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
            else if (Fighter > 0)
            {
                string[] lines = Head.Split(':');
                return new List<string> { $"{lines[0]}\n{lines[1]}" };
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
            else if (Fighter > 0)
            {
                return !Game.Option.IsTriggered("бой");
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

        public List<string> FiveDiceTest()
        {
            List<string> lines = new List<string>();

            for (int i = 1; i < 6; i += 1)
            {
                int dice = Game.Dice.Roll();
                lines.Add($"На {i} кубике выпало: {Game.Dice.Symbol(dice)}");

                if (dice == 1)
                {
                    lines.Add("BIG|BAD|Выпала единица! :(");
                    return lines;
                }
            }

            lines.Add("BIG|GOOD|Ни одной единицы не выпало! :)");
            return lines;
        }

        private void FightResult(bool good, ref List<string> lines)
        {
            if (good)
            {
                lines.Add($"GOOD|Вы выиграли 1000 бат и вернули свою ставку! :)");
                Character.Protagonist.Money += 1000;
            }
            else
            {
                lines.Add($"BAD|Вы проиграли 1000 бат! :(");
                Character.Protagonist.Money -= 1000;
            }
        }

        public List<string> Fight()
        {
            if (Game.Option.IsTriggered("бой"))
            {
                return new List<string> { "Бой уже закончился" };
            }

            Game.Option.Trigger("бой");

            List<string> lines = new List<string> { "БОЙ НАЧИНАЕТСЯ:" };

            int round = 0;
            int firstStrength = 3;
            int secondStrength = 4;

            while (true)
            {
                round += 1;
                lines.Add(String.Empty);
                lines.Add($"HEAD|BOLD|*  *  *    РАУНД: {round}    *  *  * ");

                Game.Dice.DoubleRoll(out int firstDice, out int secondDice);

                int firstHit = firstDice + 3;
                lines.Add($"Удар первого: {Game.Dice.Symbol(firstDice)} + 3 = {firstHit}");

                int secondHit = secondDice + 2;
                lines.Add($"Удар второго: {Game.Dice.Symbol(secondDice)} + 2 = {secondHit}");
                
                if (firstHit > secondHit)
                {
                    lines.Add("Первый боец отоварил второго!");
                    secondStrength -= 1;
                    lines.Add($"У второго бойца осталось выносливости: {secondStrength}");
                }
                else if (secondHit > firstHit)
                {
                    lines.Add("Второй боец отоварил первого!");
                    firstStrength -= 1;
                    lines.Add($"У первого бойца осталось выносливости: {firstStrength}");
                }
                else
                {
                    lines.Add("Ничья в этом раунде!");
                }

                if ((firstStrength <= 0) || (secondStrength <= 0))
                {
                    lines.Add(String.Empty);

                    if (firstStrength <= 0)
                    {
                        lines.Add($"BIG|BOLD|Второй боец выиграл!");
                        FightResult(Fighter == 2, ref lines);
                    }
                    else
                    {
                        lines.Add($"BIG|BOLD|Первый боец выиграл!");
                        FightResult(Fighter == 1, ref lines);
                    }
                    
                    return lines;
                }
            }
        }

        public override bool IsHealingEnabled() =>
            Character.Protagonist.Wounds > 0;

        public override void UseHealing(int healingLevel) =>
            Character.Protagonist.Wounds -= healingLevel;
    }
}
