using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.AlamutFortress
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public List<Character> Enemies { get; set; }

        public int Count { get; set; }
        public bool Double { get; set; }
        public bool Odd { get; set; }
        public bool DivisibleByThree { get; set; }
        public bool SubWound { get; set; }
        public bool SubStrength { get; set; }
        public bool HalfResult { get; set; }

        public override List<string> Status() => new List<string>
        {
            $"Сила: {Character.Protagonist.Strength}",
            $"Здоровье: {Character.Protagonist.Hitpoints}/{Character.Protagonist.MaxHitpoints}",
            $"Золото: {Character.Protagonist.Gold}"
        };

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
                enemies.Add($"{enemy.Name}\nсила {enemy.Strength}  здоровье {enemy.Hitpoints}");

            return enemies;
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(Character.Protagonist.Hitpoints, out toEndParagraph, out toEndText);

        public List<string> Dices()
        {
            List<string> diceCheck = new List<string> { };

            int firstDice = Game.Dice.Roll();
            int dicesResult = firstDice;
            bool isDouble = false;

            if (Count < 2)
            {
                diceCheck.Add($"BIG|На кубикe выпало: {Game.Dice.Symbol(firstDice)}");
            }
            else
            {
                int secondDice = Game.Dice.Roll();
                dicesResult += secondDice;

                diceCheck.Add($"BIG|На кубиках выпало: " +
                    $"{Game.Dice.Symbol(firstDice)} + " +
                    $"{Game.Dice.Symbol(secondDice)} = {dicesResult}");

                isDouble = firstDice == secondDice;
            }

            if (HalfResult)
            {
                double half = (double)dicesResult / 2;
                diceCheck.Add($"{dicesResult} делим на 2 = {half}");

                if (half == Math.Floor(half))
                {
                    dicesResult = (int)half;
                }
                else
                {
                    dicesResult = (int)Math.Floor(half);
                    diceCheck.Add($"Округляем в меньшую сторону до {dicesResult}");
                }
            }

            string pointsLine = Game.Services.CoinsNoun(Math.Abs(dicesResult), "очко", "очка", "очков");

            if (DivisibleByThree)
            {
                diceCheck.Add(dicesResult % 3 == 0 ? "BIG|ДЕЛИТСЯ на ТРИ!" : "BIG|НЕ делится на три!");
            }
            else if (Double)
            {
                diceCheck.Add(isDouble ? "BIG|GOOD|ВЫПАЛ ДУБЛЬ!" : "BIG|BAD|Выпал НЕ дубль!");
            }
            else if (Odd)
            {
                diceCheck.Add(dicesResult % 2 == 0 ? "BIG|ЧЁТНОЕ ЧИСЛО!" : "BIG|НЕЧЁТНОЕ ЧИСЛО!");
            }
            else if (SubWound)
            {
                Character.Protagonist.Hitpoints -= dicesResult;
                diceCheck.Add($"BIG|BAD|Потеряно {dicesResult} {pointsLine} Здоровья");
            }
            else if (SubStrength)
            {
                Character.Protagonist.Strength -= dicesResult;
                diceCheck.Add($"BIG|BAD|Потеряно {dicesResult} {pointsLine} Силы");
            }

            return diceCheck;
        }

        public static bool NoMoreEnemies(List<Character> enemies) =>
            enemies.Where(x => x.Hitpoints > 0).Count() == 0;

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            int round = 1;

            while (true)
            {
                fight.Add($"HEAD|BOLD|Раунд: {round}");

                bool attackAlready = false;
                bool godsJudgment = false;
                int protagonistHitStrength = 0;

                foreach (Character enemy in FightEnemies)
                {
                    if (enemy.Hitpoints <= 0)
                        continue;

                    fight.Add($"{enemy.Name} (здоровье {enemy.Hitpoints})");

                    if (!attackAlready)
                    {
                        Game.Dice.DoubleRoll(out int protagonistRollFirst, out int protagonistRollSecond);
                        protagonistHitStrength = protagonistRollFirst + protagonistRollSecond + Character.Protagonist.Strength;

                        fight.Add($"Сила вашей атаки: " +
                            $"{Game.Dice.Symbol(protagonistRollFirst)} + " +
                            $"{Game.Dice.Symbol(protagonistRollSecond)} + " +
                            $"{Character.Protagonist.Strength} = {protagonistHitStrength}");

                        godsJudgment = (protagonistRollFirst == 6) &&
                            (protagonistRollFirst == protagonistRollSecond);
                    }

                    if (godsJudgment)
                    {
                        fight.Add($"GOOD|BOLD|Свершился БОЖИЙ СУД! {enemy.Name} убит!");
                        enemy.Hitpoints = 0;

                        if (NoMoreEnemies(FightEnemies))
                        {
                            fight.Add(String.Empty);
                            fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");
                            return fight;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    Game.Dice.DoubleRoll(out int enemyRollFirst, out int enemyRollSecond);
                    int enemyHitStrength = enemyRollFirst + enemyRollSecond + enemy.Strength;

                    fight.Add($"Сила его атаки: " +
                        $"{Game.Dice.Symbol(enemyRollFirst)} + " +
                        $"{Game.Dice.Symbol(enemyRollSecond)} + " +
                        $"{enemy.Strength} = {enemyHitStrength}");

                    bool diabolicalMeanness = (enemyRollFirst == 1) &&
                        (enemyRollFirst == enemyRollSecond);

                    if (diabolicalMeanness)
                    {
                        fight.Add($"BAD|BOLD|Дьявольская атака! {enemy.Name} убил вас!");
                        fight.Add(String.Empty);
                        fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");

                        Character.Protagonist.Hitpoints = 0;
                        return fight;
                    }

                    int hitPointsLoses = protagonistHitStrength - enemyHitStrength;
                    string losesLine = Game.Services.CoinsNoun(Math.Abs(hitPointsLoses), "очко", "очка", "очков");

                    if ((hitPointsLoses > 0) && !attackAlready)
                    {
                        fight.Add($"GOOD|{enemy.Name} ранен");
                        fight.Add($"Он теряет {hitPointsLoses} {losesLine} Здоровья");

                        enemy.Hitpoints -= hitPointsLoses;

                        if (NoMoreEnemies(FightEnemies))
                        {
                            fight.Add(String.Empty);
                            fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");
                            return fight;
                        }
                    }
                    else if (hitPointsLoses > 0)
                    {
                        fight.Add($"BOLD|{enemy.Name} не смог вас ранить");
                    }
                    else if (hitPointsLoses < 0)
                    {
                        fight.Add($"BAD|{enemy.Name} ранил вас");
                        fight.Add($"Вы теряете {Math.Abs(hitPointsLoses)} {losesLine} Здоровья");

                        Character.Protagonist.Hitpoints += hitPointsLoses;

                        if (Character.Protagonist.Hitpoints <= 0)
                        {
                            fight.Add(String.Empty);
                            fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");
                            return fight;
                        }
                    }
                    else
                    {
                        fight.Add("BOLD|Ничья в раунде");
                    }

                    attackAlready = true;

                    fight.Add(String.Empty);
                }

                round += 1;
            }
        }
    }
}
