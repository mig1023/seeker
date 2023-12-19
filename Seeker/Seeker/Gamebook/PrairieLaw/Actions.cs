﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.PrairieLaw
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public List<Character> Enemies { get; set; }
        public bool Firefight { get; set; }
        public bool HeroWoundsLimit { get; set; }
        public bool EnemyWoundsLimit { get; set; }

        public int Dices { get; set; }
        public bool Roulette { get; set; }
        public string SellPrices { get; set; }
        public string Untrigger { get; set; }

        public override List<string> Status() => new List<string>
        {
            $"Ловкость: {protagonist.Skill}",
            $"Сила: {protagonist.Strength}/{protagonist.MaxStrength}",
            $"Обаяние: {protagonist.Charm}",
        };

        public override List<string> AdditionalStatus() => new List<string>
        {
            $"Долларов: {ToDollars(protagonist.Cents)}",
            $"Патронов: {protagonist.Cartridges}",
        };

        private static string ToDollars(int cents)
        {
            double dollars = (double)cents / 100;
            return $"{dollars:f2}".Replace(',', '.');
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(protagonist.Strength, out toEndParagraph, out toEndText);

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Price > 0)
            {
                return new List<string> { $"{Head}, {ToDollars(Price)}$" };
            }
            else if (!String.IsNullOrEmpty(Head))
            {
                return new List<string> { Head };
            }

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
            {
                string line = $"{enemy.Name}\nловкость {enemy.Skill}  сила {enemy.Strength}";

                if (Firefight)
                    line += $"  патроны {enemy.Cartridges}";

                enemies.Add(line);
            }

            return enemies;
        }

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains("|"))
            {
                int count = option
                    .Split('|')
                    .Where(x => Game.Option.IsTriggered(x.Trim()))
                    .Count();

                return count > 0;
            }
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains("="))
                    {
                        int level = Game.Services.LevelParse(oneOption);

                        if (option.Contains("ЦЕНТОВ >=") && (level > protagonist.Cents))
                            return false;

                        else if (option.Contains("САМОРОДКОВ >=") && (level > protagonist.Nuggets))
                            return false;

                        else if (option.Contains("ПАТРОНОВ >=") && (level > protagonist.Cartridges))
                            return false;

                        else if (option.Contains("ШКУР >=") && (level > protagonist.AnimalSkins.Count))
                            return false;
                    }
                    else if (!Game.Option.IsTriggered(oneOption.Trim()))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public List<string> Luck()
        {
            List<string> luckCheck = new List<string>
            {
                "Цифры удачи:",
                "BIG|" + Luckiness.Numbers()
            };

            int goodLuck = Game.Dice.Roll();
            string not = protagonist.Luck[goodLuck] ? "не " : String.Empty;

            luckCheck.Add($"Проверка удачи: {Game.Dice.Symbol(goodLuck)} - {not}зачёркунтый");
            luckCheck.Add(Result(protagonist.Luck[goodLuck], "УСПЕХ|НЕУДАЧА"));

            protagonist.Luck[goodLuck] = !protagonist.Luck[goodLuck];

            return luckCheck;
        }

        public List<string> LuckRecovery()
        {
            List<string> luckRecovery = new List<string> { "Восстановление удачи:" };

            bool success = Luckiness.Recovery(luckRecovery);

            if (!success)
            {
                luckRecovery.Add("BAD|Все цифры и так счастливые!");
            }

            luckRecovery.Add("Цифры удачи теперь:");
            luckRecovery.Add("BIG|" + Luckiness.Numbers());

            return luckRecovery;
        }

        public List<string> Charm()
        {
            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            bool goodCharm = (firstDice + secondDice) <= protagonist.Charm;
            string charmLine = goodCharm ? "<=" : ">";

            List<string> luckCheck = new List<string> { $"Проверка обаяния: " +
                $"{Game.Dice.Symbol(firstDice)} + " +
                $"{Game.Dice.Symbol(secondDice)} " +
                $"{charmLine} {protagonist.Charm}" };

            if (goodCharm)
            {
                luckCheck.Add("BIG|GOOD|УСПЕХ :)");
                luckCheck.Add("Вы увеличили своё обаяние на единицу");

                protagonist.Charm += 1;
            }
            else
            {
                luckCheck.Add("BIG|BAD|НЕУДАЧА :(");

                if (protagonist.Charm > 2)
                {
                    luckCheck.Add("Вы уменьшили своё обаяние на единицу");
                    protagonist.Charm -= 1;
                }
            }

            return luckCheck;
        }

        public List<string> Skill()
        {
            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            bool goodSkill = (firstDice + secondDice) <= protagonist.Skill;
            string skillLine = goodSkill ? "<=" : ">";

            List<string> luckCheck = new List<string> { $"Проверка ловкости: " +
                $"{Game.Dice.Symbol(firstDice)} + " +
                $"{Game.Dice.Symbol(secondDice)} {skillLine} " +
                $"{protagonist.Skill}" };

            luckCheck.Add(Result(goodSkill, "УСПЕХ|НЕУДАЧА"));

            return luckCheck;
        }

        public List<string> DiceWounds()
        {
            List<string> diceCheck = new List<string> { };

            int dicesCount = (Dices == 0 ? 1 : Dices);
            int dices = 0;

            for (int i = 1; i <= dicesCount; i++)
            {
                int dice = Game.Dice.Roll();
                dices += dice;
                diceCheck.Add($"На {i} выпало: {Game.Dice.Symbol(dice)}");
            }

            protagonist.Strength -= dices;

            diceCheck.Add($"BIG|BAD|Вы потеряли жизней: {dices}");

            return diceCheck;
        }

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            bool byUsed = (Price > 0) && Used;
            bool byPrice = (Price > 0) && (protagonist.Cents < Price);
            bool bySkins = (Type == "SellSkins") && (protagonist.AnimalSkins.Count == 0);
            bool byNuggets = (Type == "SellNuggets") && (protagonist.Nuggets == 0);
            bool byGame = Roulette && (protagonist.Cents < 100);

            return !(byUsed || byPrice || bySkins || byNuggets || byGame);
        }

        public List<string> Get()
        {
            if (!String.IsNullOrEmpty(Untrigger))
                Game.Option.Trigger(Untrigger, remove: true);

            if ((Price > 0) && (protagonist.Cents >= Price))
            {
                protagonist.Cents -= Price;

                if (!Multiple)
                    Used = true;

                if (Benefit != null)
                    Benefit.Do();
            }

            return new List<string> { "RELOAD" };
        }

        public List<string> SellSkins()
        {
            List<string> salesReport = new List<string>();

            int cents = 0, sold = 0, index = 0;

            Dictionary<string, int> prices = new Dictionary<string, int>();
            List<int> saledIndexes = new List<int>();

            foreach(string price in SellPrices.Split(',').ToList())
            {
                string[] valuePrice = price.Split('=');
                prices.Add(valuePrice[0].Trim(), int.Parse(valuePrice[1]));
            }

            bool anySkin = prices.ContainsKey("Любая шкура");

            foreach (string skin in protagonist.AnimalSkins)
            {
                if (prices.ContainsKey(skin) || anySkin)
                {
                    int price = (anySkin ? prices["Любая шкура"] : prices[skin]);

                    salesReport.Add($"{skin} - купил за {ToDollars(price)}$");
                    cents += price;
                    saledIndexes.Add(index);
                    sold += 1;
                }
                else
                {
                    salesReport.Add($"{skin} - её не купит");
                }

                index += 1;
            }

            saledIndexes.Reverse();

            foreach (int removeIndex in saledIndexes)
                protagonist.AnimalSkins.RemoveAt(removeIndex);

            salesReport.Add(String.Empty);
            salesReport.Add("BIG|ИТОГО:");
            salesReport.Add($"Вы продали шкур: {sold}");
            salesReport.Add($"GOOD|Вы получили: {ToDollars(cents)}$");

            protagonist.Cents += cents;

            return salesReport;
        }
        
        public List<string> SellNuggets()
        {
            List<string> salesReport = new List<string>();

            int price = int.Parse(SellPrices);
            int cents = protagonist.Nuggets * price;
            protagonist.Cents += cents;

            salesReport.Add($"Вы продали самородков: {protagonist.Nuggets}");
            salesReport.Add($"Цена за один: {ToDollars(price)}$");
            salesReport.Add($"GOOD|Вы получили: {ToDollars(cents)}$");

            protagonist.Nuggets = 0;

            return salesReport;
        }

        public List<string> RedOrBlackGame()
        {
            List<string> gameReport = new List<string>();

            bool red = (Game.Dice.Roll() > 3);
            int dice = Game.Dice.Roll();
            bool even = (dice % 2 == 0);
            string redLine = red ? "красное (чёт)" : "чёрное (нечет)";
            string evenLine = even ? "красное" : "чёрное";

            gameReport.Add($"Вы поставили на {redLine}");
            gameReport.Add($"На рулетке выпало: {Game.Dice.Symbol(dice)} - {evenLine}");

            if (red == even)
            {
                gameReport.Add("GOOD|Вы ВЫИГРАЛИ и получили 1$ :)");
                protagonist.Cents += 100;
            }
            else
            {
                gameReport.Add("BAD|Вы ПРОИГРАЛИ и потеряли 1$ :(");
                protagonist.Cents -= 100;
            }

            return gameReport;
        }

        public List<string> DoubleGame()
        {
            List<string> gameReport = new List<string>();

            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);

            gameReport.Add($"На рулетке выпали: " +
                $"{Game.Dice.Symbol(firstDice)} и {Game.Dice.Symbol(secondDice)}");

            if (firstDice == secondDice)
            {
                gameReport.Add("GOOD|Цифры совпали - вы ВЫИГРАЛИ и получили 5$ :)");
                protagonist.Cents += 500;
            }
            else
            {
                gameReport.Add("BAD|Вы ПРОИГРАЛИ и потеряли 1$ :(");
                protagonist.Cents -= 100;
            }

            return gameReport;
        }

        public List<string> DiceLtlGame()
        {
            List<string> gameReport = new List<string>();

            int dice = Game.Dice.Roll();
            bool even = (dice % 2 == 0);
            string evelLine = even ? "чётное" : "нечётное";

            gameReport.Add($"На кубике выпало: {Game.Dice.Symbol(dice)} - {evelLine}");

            if (even)
            {
                gameReport.Add("GOOD|Вы ВЫИГРАЛИ и получаете 1$ :)");
                protagonist.Cents += 100;
            }
            else
            {
                gameReport.Add("BAD|Вы ПРОИГРАЛИ и потеряли 1$ :(");
                protagonist.Cents -= 100;
            }

            return gameReport;
        }

        public List<string> DiceGame()
        {
            List<string> gameReport = new List<string>();

            int dice = Game.Dice.Roll();
            bool even = (dice % 2 == 0);
            bool nuggetsGame = Game.Option.IsTriggered("Игра на самородок");
            string evelLine = even ? "чётное" : "нечётное";

            gameReport.Add($"На кубике выпало: {Game.Dice.Symbol(dice)} - {evelLine}");

            if (even)
            {
                gameReport.Add("GOOD|Вы ВЫИГРАЛИ :)");

                if (nuggetsGame)
                {
                    gameReport.Add("Самородок теперь ваш.");
                    protagonist.Nuggets += 1;
                }
                else
                {
                    gameReport.Add("Вы выиграли 3 доллара.");
                    protagonist.Cents += 300;
                }
            }
            else
            {
                gameReport.Add("BAD|Вы ПРОИГРАЛИ :(");

                if (nuggetsGame)
                {
                    gameReport.Add("Вы потеряли 1$");
                    protagonist.Cents -= 100;
                }
                else
                {
                    gameReport.Add("Вы потеряли 3$");
                    protagonist.Cents -= 300;
                }
            }

            return gameReport;
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            int round = 1;
            bool firefight = Firefight;

            while (true)
            {
                firefight = Fights.FirefightContinue(FightEnemies, ref fight, firefight);

                fight.Add($"HEAD|BOLD|Раунд: {round}");

                bool attackAlready = false;
                int protagonistHitStrength = 0, enemyHitStrength = 0;

                foreach (Character enemy in FightEnemies)
                {
                    if (enemy.Strength <= 0)
                        continue;

                    string cartLine = enemy.Cartridges > 0 ? $", патронов {enemy.Cartridges}" : String.Empty;
                    fight.Add($"{enemy.Name} (сила {enemy.Strength}{cartLine})");

                    bool noCartridges = protagonist.Cartridges <= 0;

                    if (!attackAlready && (!firefight || !noCartridges))
                    {
                        Game.Dice.DoubleRoll(out int protagonistRollFirst, out int protagonistRollSecond);
                        protagonistHitStrength = protagonistRollFirst + protagonistRollSecond + protagonist.Skill;

                        string protagonistHitLine = (firefight ? "Ваш выстрел" : "Мощность вашего удара");

                        fight.Add($"{protagonistHitLine}: " +
                            $"{Game.Dice.Symbol(protagonistRollFirst)} + " +
                            $"{Game.Dice.Symbol(protagonistRollSecond)} + " +
                            $"{protagonist.Skill} = {protagonistHitStrength}");

                        if (firefight)
                            protagonist.Cartridges -= 1;
                    }

                    if (!firefight || (enemy.Cartridges > 0))
                    {
                        Game.Dice.DoubleRoll(out int enemyRollFirst, out int enemyRollSecond);
                        enemyHitStrength = enemyRollFirst + enemyRollSecond + enemy.Skill;

                        string enemyHitLine = (firefight ? "Его выстрел" : "Мощность его удара");

                        fight.Add($"{enemyHitLine}: " +
                            $"{Game.Dice.Symbol(enemyRollFirst)} + " +
                            $"{Game.Dice.Symbol(enemyRollSecond)} + " +
                            $"{enemy.Skill} = {enemyHitStrength}");

                        if (firefight)
                            enemy.Cartridges -= 1;
                    }
                    else
                    {
                        enemyHitStrength = 0;
                    }

                    if ((protagonistHitStrength == 0) && (enemyHitStrength == 0))
                    { 
                        // nothing to do here
                    }
                    else if ((protagonistHitStrength > enemyHitStrength) && !attackAlready)
                    {
                        fight.Add($"GOOD|{enemy.Name} ранен");

                        enemy.Strength -= (firefight ? 3 : 2);

                        bool enemyLost = Fights.NoMoreEnemies(FightEnemies, EnemyWoundsLimit);

                        if (enemyLost)
                        {
                            fight.Add(String.Empty);
                            fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");
                            return fight;
                        }
                    }
                    else if (protagonistHitStrength > enemyHitStrength)
                    {
                        fight.Add($"BOLD|{enemy.Name} не смог вас ранить");
                    }
                    else if (protagonistHitStrength < enemyHitStrength)
                    {
                        fight.Add($"BAD|{enemy.Name} ранил вас");

                        protagonist.Strength -= (firefight ? 3 : 2);

                        if ((protagonist.Strength <= 0) || (HeroWoundsLimit && (protagonist.Strength <= 2)))
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

        public override bool IsHealingEnabled() =>
            protagonist.Strength < protagonist.MaxStrength;

        public override void UseHealing(int healingLevel)
        {
            if (healingLevel == -1)
            {
                protagonist.Strength = protagonist.MaxStrength;
            }
            else
            {
                protagonist.Strength += healingLevel;
            }
        }
    }
}
