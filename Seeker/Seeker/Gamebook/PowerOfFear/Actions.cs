using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.PowerOfFear
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public string Name { get; set; }
        public string Skill { get; set; }
        public int Level { get; set; }
        public int Dices { get; set; }
        public bool AdditionalPenalty { get; set; }
        public bool HitpointsLoss { get; set; }
        public bool NoHitpointsLoss { get; set; }

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
                statusLines.Add($"Физическая сила: {Character.Protagonist.Strength}");

            return statusLines.Count <= 0 ? null : statusLines;
        }

        public override List<string> Representer()
        {
            if ((Type == "Test") && (Skill == "Random"))
            {
                return new List<string> { "ПРОВЕРКА ПО СЛУЧАЙНОМУ НАВЫКУ" };
            }
            else if (Type == "Test")
            {
                return new List<string> { $"ПРОВЕРКА ПО НАВЫКУ\n" +
                    $"{Constants.PropertiesNames[Skill].ToUpper()}" };
            }
            else if (Type == "Fight")
            {
                return new List<string> { $"{Name}\n" +
                    $"Атакует на {Dices} кубиках" };
            }
            else if (!String.IsNullOrEmpty(Skill))
            {
                int count = GetProperty(Character.Protagonist, Skill);
                string line = Game.Services.CoinsNoun(count, "единица", "единицы", "единицы");

                return new List<string> { $"{Head}\n" +
                    $"текущее значение: {count} {line})" };
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

            if ((Type == "Test") && (Skill == "Random"))
            {
                return true;
            }
            else if (Type == "Test")
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

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (!option.Contains('!') && Constants.PropertiesNames.ContainsKey(option))
            {
                return GetProperty(Character.Protagonist, option) > 0;
            }
            else if (Constants.PropertiesNames.ContainsKey(option.Replace("!", String.Empty)))
            {
                return GetProperty(Character.Protagonist, option.Replace("!", String.Empty)) == 0;
            }
            else if (option == "Время ещё есть")
            {
                return Character.Protagonist.Time < 3;
            }
            else if (option == "Время кончилось")
            {
                return Character.Protagonist.Time >= 3;
            }
            else
            {
                return AvailabilityTrigger(option);
            }
        }

        public List<string> Test()
        {
            List<string> lines = new List<string>();

            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            int result = firstDice + secondDice;

            lines.Add($"Кубики: {Game.Dice.Symbol(firstDice)} + " +
                $"{Game.Dice.Symbol(secondDice)} = {result}");

            if (Skill == "Random")
            {
                Dictionary<string, int> allSkills = new Dictionary<string, int>();

                foreach (string skill in Constants.PropertiesNames.Keys)
                    allSkills.Add(skill, GetProperty(Character.Protagonist, skill));

                Skill = allSkills.OrderByDescending(x => x.Value).First().Key;

                lines.Add($"GRAY|Для теста выбран навык «{Constants.PropertiesNames[Skill]}»");
            }

            int level = GetProperty(Character.Protagonist, Skill);

            lines.Add($"Уровень навыка «{Constants.PropertiesNames[Skill]}» равен {level}");

            if (Game.Option.IsTriggered("Муки совести"))
            {
                level -= 1;
                lines.Add($"BAD|Из-за мук совести уровень навыка при проверки снижается на единицу и будет равен {level}");
            }

            if (result > level)
            {
                lines.Add($"Выпавшее значение {result} больше уровня навыка!");
                lines.Add($"BIG|BAD|BOLD|Проверка провалена :(");

                Game.Buttons.Disable("Проверка удачна, Вам повезло");

                if (HitpointsLoss)
                {
                    Character.Protagonist.Hitpoints -= 1;
                    lines.Add("BAD|Кроме того, вы теряете 1 ед. Здоровья");
                }
            }
            else
            {
                lines.Add($"Выпавшее значение {result} не превышает уровня навыка!");
                lines.Add($"BIG|GOOD|BOLD|Проверка успешно пройдена :)");

                Game.Buttons.Disable("Проверка неудачна, " +
                    "Удача от вас отвернулась, Не получилось");

                if (HitpointsLoss)
                {
                    Character.Protagonist.Strength += 1;
                    lines.Add("GOOD|Кроме того, вы получаете 1 ед. навыка «Физическая сила»");
                }
            }

            return lines;
        }

        private void AttackDices(int dicesCount, out int dices,
            out string dicesLines, out bool doubleDice, out int smaller)
        {
            dices = 0;
            dicesLines = String.Empty;
            smaller = 6;

            List<int> allDices = new List<int>();

            for (int i = 1; i <= dicesCount; i += 1)
            {
                if (dices > 0)
                    dicesLines += " +";

                int dice = Game.Dice.Roll();
                dices += dice;
                dicesLines += $" {Game.Dice.Symbol(dice)}";

                allDices.Add(dice);

                if (dice < smaller)
                    smaller = dice;
            }

            int doubles = allDices
                .GroupBy(x => x)
                .Where(x => x.Count() > 1)
                .Sum(x => x.Count());

            doubleDice = doubles > 0;
        }

        public List<string> Fight()
        {
            List<string> lines = new List<string> { "BIG|BOLD|БОЙ:" };

            if (Game.Option.IsTriggered("Жестокий удар"))
            {
                Character.Protagonist.Hitpoints -= 1;
                Game.Option.Trigger("Жестокий удар", remove: true);

                lines.Add("GOOD|BOLD|Враг повержен «Жестоким ударом»!");
                lines.Add("Вы только лишь потеряли 1 ед. Здоровья по условиям применения этого состояния");
                lines.Add("GOOD|BIG|Вы победили :)");

                return lines;
            }

            if (Game.Option.IsTriggered("Злость"))
            {
                Character.Protagonist.Hitpoints -= 1;
                lines.Add($"BAD|Теряете 1 ед. Здоровья за состояние «Злость»");
            }

            int attackCount = 3;
            bool alreadyRerolled = false;
            int hiptoints = Character.Protagonist.Hitpoints;
            string attackCountLine = "Кол-во ваших кубиков атаки: 3 (базовое)";

            if (Character.Protagonist.Weapon > 0)
            {
                attackCount += 1;
                attackCountLine += " + 1 (за Владение оружием)";
            }

            if (Game.Option.IsTriggered("Меч"))
            {
                attackCount += 1;
                attackCountLine += " + 1 (за меч)";
            }

            lines.Add(attackCountLine);
            lines.Add($"Кол-во кубиков атаки противника: {Dices}");

            while (true)
            {
                AttackDices(Dices, out int enemyAttack, out string enemyLine, out bool doubleDice, out _);
                lines.Add($"Кубики противника:{enemyLine}");

                AttackDices(attackCount, out int heroAttack, out string heroLine, out bool _, out int smaller);
                lines.Add($"Ваши кубики:{heroLine}");

                bool addPenalty = doubleDice && AdditionalPenalty;
                bool needToReroll = enemyAttack > heroAttack;
                bool needWithPenalty = addPenalty && (enemyAttack + 2 > heroAttack);

                if (needToReroll || needWithPenalty)
                {
                    if (Game.Option.IsTriggered("Шестое чувство") && !alreadyRerolled)
                    {
                        lines.Add("GRAY|Умение «Шестое чувство» позволяет вам перебросить кубики!");

                        AttackDices(attackCount, out heroAttack, out heroLine, out bool _, out _);
                        lines.Add($"Теперь ваши кубики:{heroLine}");

                        alreadyRerolled = true;
                    }
                    else if (Game.Option.IsTriggered("Первобытная ярость"))
                    {
                        lines.Add("GRAY|Умение «Первобытная ярость» позволяет вам перебросить наименьший кубик!");
                        lines.Add($"Наименьший выпавший кубик: {Game.Dice.Symbol(smaller)}");

                        int newDice = Game.Dice.Roll();
                        lines.Add($"Перебрасываем его: {Game.Dice.Symbol(newDice)}");
                        heroAttack += newDice - smaller;
                    }
                }

                if (addPenalty)
                {
                    lines.Add("Противник выкинул дубль!");

                    if (Game.Option.IsTriggered("Уворот"))
                    {
                        lines.Add("GRAY|Но это ничего ему не даст из-за вашего умерия «Уворот»!");
                    }
                    else
                    {
                        heroAttack -= 2;

                        lines.Add($"По специальному правилу от силы вашей атаки " +
                            $"отнимается 2, теперь она равна {heroAttack}");
                    }
                }

                if (Game.Option.IsTriggered("Знание трав"))
                {
                    enemyAttack -= 2;
                    lines.Add($"GRAY|Из силы атаки противника вычитается 2 ед. за состояние «Знание трав»!");
                }

                if (enemyAttack > heroAttack)
                {
                    lines.Add($"Атака противника ({enemyAttack} ед.) сильнее вашей ({heroAttack} ед.)!");
                    lines.Add("BAD|Противник вас ранил!");

                    Character.Protagonist.Hitpoints -= 1;

                    if (doubleDice && !AdditionalPenalty)
                    {
                        lines.Add("Противник выкинул дубль!");

                        if (Game.Option.IsTriggered("Уворот"))
                        {
                            lines.Add("GRAY|Но это ничего ему не даст из-за вашего умерия «Уворот»!");
                        }
                        else
                        {
                            lines.Add("BAD|Противник наносит вам дополнительный урон!");
                            Character.Protagonist.Hitpoints -= 1;
                        }
                    }

                    if (NoHitpointsLoss)
                        Character.Protagonist.Hitpoints = hiptoints;

                    lines.Add("BAD|BOLD|Противник победил!");
                    lines.Add("BAD|BIG|Вы проиграли :(");
                    return lines;
                }
                else if (enemyAttack < heroAttack)
                {
                    if (NoHitpointsLoss)
                        Character.Protagonist.Hitpoints = hiptoints;

                    lines.Add($"Ваша атака ({heroAttack} ед.) сильнее, чем у противника ({enemyAttack} ед.)!");
                    lines.Add("GOOD|BOLD|Противник повержен!");
                    lines.Add("GOOD|BIG|Вы победили :)");
                    return lines;
                }
                else
                {
                    lines.Add("BOLD|Ничья!");
                    lines.Add("Вам необходимо сойтись в ещё одном раунде противостояния!");
                }
            }
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = Character.Protagonist.GameoverPath;

            toEndText = Character.Protagonist.GameoverPath > 0 ?
                "Силы оставили вас..." : Output.Constants.GAMEOVER_TEXT;

            return Character.Protagonist.Hitpoints <= 0;
        }

        public override bool IsHealingEnabled() =>
            Character.Protagonist.Hitpoints < 10;

        public override void UseHealing(int healingLevel) =>
            Character.Protagonist.Hitpoints = 10;
    }
}
