﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.PrairieLaw
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();

        public List<Character> Enemies { get; set; }
        public bool Firefight { get; set; }
        public bool HeroWoundsLimit { get; set; }
        public bool EnemyWoundsLimit { get; set; }

        public int Dices { get; set; }
        public bool Roulette { get; set; }
        public string SellPrices { get; set; }
        public string RemoveTrigger { get; set; }

        private string ToDollars(int cents) => String.Format("{0:f2}", (double)cents / 100).TrimEnd('0').TrimEnd(',').Replace(',', '.');

        public override List<string> Status() => new List<string>
        {
            String.Format("Ловкость: {0}", Character.Protagonist.Skill),
            String.Format("Сила: {0}", Character.Protagonist.Strength),
            String.Format("Обаяние: {0}", Character.Protagonist.Charm),
        };

        public override List<string> AdditionalStatus() => new List<string>
        {
            String.Format("Патронов: {0}", Character.Protagonist.Cartridges),
            String.Format("Долларов: {0:f2}", ToDollars(Character.Protagonist.Cents))
        };

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(Character.Protagonist.Strength, out toEndParagraph, out toEndText);

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Price > 0)
                return new List<string> { String.Format("{0}, {1:f2}$", Text, ToDollars(Price)) };

            else if (!String.IsNullOrEmpty(Text))
                return new List<string> { Text };

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
            {
                string line = String.Format("{0}\nловкость {1}  сила {2}", enemy.Name, enemy.Skill, enemy.Strength);

                if (Firefight)
                    line += String.Format("  патроны {0}", enemy.Cartridges);

                enemies.Add(line);
            }

            return enemies;
        }

        public override bool CheckOnlyIf(string option)
        {
            if (option.Contains("|"))
                return option.Split('|').Where(x => Game.Data.Triggers.Contains(x.Trim())).Count() > 0;

            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains("="))
                    {
                        int level = Game.Other.LevelParse(oneOption);

                        if (option.Contains("ЦЕНТОВ >=") && (level > Character.Protagonist.Cents))
                            return false;

                        else if (option.Contains("САМОРОДКОВ >=") && (level > Character.Protagonist.Nuggets))
                            return false;

                        else if (option.Contains("ПАТРОНОВ >=") && (level > Character.Protagonist.Cartridges))
                            return false;

                        else if (option.Contains("ШКУР >=") && (level > Character.Protagonist.AnimalSkins.Count))
                            return false;
                    }
                    else if (!Game.Data.Triggers.Contains(oneOption.Trim()))
                        return false;
                }

                return true;
            }
        }

        private string LuckNumbers()
        {
            string luckListShow = String.Empty;

            for (int i = 1; i < 7; i++)
                luckListShow += String.Format("{0} ", Character.Protagonist.Luck[i] ? Constants.LuckList()[i] : Constants.LuckList()[i + 10]);

            return luckListShow;
        }

        public List<string> Luck()
        {
            List<string> luckCheck = new List<string>
            {
                "Цифры удачи:",
                "BIG|" + LuckNumbers()
            };

            int goodLuck = Game.Dice.Roll();

            luckCheck.Add(String.Format("Проверка удачи: {0} - {1}зачёркунтый",
                Game.Dice.Symbol(goodLuck), (Character.Protagonist.Luck[goodLuck] ? "не " : String.Empty)));

            luckCheck.Add(Character.Protagonist.Luck[goodLuck] ? "BIG|GOOD|УСПЕХ :)" : "BIG|BAD|НЕУДАЧА :(");

            Character.Protagonist.Luck[goodLuck] = !Character.Protagonist.Luck[goodLuck];

            return luckCheck;
        }

        public List<string> LuckRecovery()
        {
            List<string> luckRecovery = new List<string> { "Восстановление удачи:" };

            bool success = false;

            for (int i = 1; i < 7; i++)
                if (!Character.Protagonist.Luck[i])
                {
                    luckRecovery.Add(String.Format("GOOD|Цифра {0} восстановлена!", i));
                    Character.Protagonist.Luck[i] = true;
                    success = true;

                    break;
                }

            if (!success)
                luckRecovery.Add("BAD|Все цифры и так счастливые!");

            luckRecovery.Add("Цифры удачи теперь:");
            luckRecovery.Add("BIG|" + LuckNumbers());

            return luckRecovery;
        }

        public List<string> Charm()
        {
            int fisrtDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();

            bool goodCharm = (fisrtDice + secondDice) <= Character.Protagonist.Charm;

            List<string> luckCheck = new List<string> { String.Format(
                "Проверка обаяния: {0} + {1} {2} {3}",
                Game.Dice.Symbol(fisrtDice), Game.Dice.Symbol(secondDice), (goodCharm ? "<=" : ">"), Character.Protagonist.Charm) };

            if (goodCharm)
            {
                luckCheck.Add("BIG|GOOD|УСПЕХ :)");
                luckCheck.Add("Вы увеличили своё обаяние на единицу");

                Character.Protagonist.Charm += 1;
            }
            else
            {
                luckCheck.Add("BIG|BAD|НЕУДАЧА :(");

                if (Character.Protagonist.Charm > 2)
                {
                    luckCheck.Add("Вы уменьшили своё обаяние на единицу");
                    Character.Protagonist.Charm -= 1;
                }
            }

            return luckCheck;
        }

        public List<string> Skill()
        {
            int fisrtDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();

            bool goodSkill = (fisrtDice + secondDice) <= Character.Protagonist.Skill;

            List<string> luckCheck = new List<string> { String.Format(
                "Проверка ловкости: {0} + {1} {2} {3}",
                Game.Dice.Symbol(fisrtDice), Game.Dice.Symbol(secondDice), (goodSkill ? "<=" : ">"), Character.Protagonist.Skill) };

            luckCheck.Add(goodSkill ? "BIG|GOOD|УСПЕХ :)" : "BIG|BAD|НЕУДАЧА :(");

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
                diceCheck.Add(String.Format("На {0} выпало: {1}", i, Game.Dice.Symbol(dice)));
            }

            Character.Protagonist.Strength -= dices;

            diceCheck.Add(String.Format("BIG|BAD|Вы потеряли жизней: {0}", dices));

            return diceCheck;
        }

        public override bool IsButtonEnabled()
        {
            bool disabledByUsed = (Price > 0) && Used;
            bool disabledByPrice = (Price > 0) && (Character.Protagonist.Cents < Price);
            bool disabledBySkins = (Name == "SellSkins") && (Character.Protagonist.AnimalSkins.Count == 0);
            bool disabledByNuggets = (Name == "SellNuggets") && (Character.Protagonist.Nuggets == 0);
            bool disabledByGame = Roulette && (Character.Protagonist.Cents < 100);

            return !(disabledByUsed || disabledByPrice || disabledBySkins || disabledByNuggets || disabledByGame);
        }

        public List<string> Get()
        {
            if (!String.IsNullOrEmpty(RemoveTrigger))
                Game.Option.Trigger(RemoveTrigger, remove: true);

            if ((Price > 0) && (Character.Protagonist.Cents >= Price))
            {
                Character.Protagonist.Cents -= Price;

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

            foreach (string skin in Character.Protagonist.AnimalSkins)
            {
                if (prices.ContainsKey(skin) || anySkin)
                {
                    int price = (anySkin ? prices["Любая шкура"] : prices[skin]);

                    salesReport.Add(String.Format("{0} - купил за {1:f2}$", skin, ToDollars(price)));
                    cents += price;
                    saledIndexes.Add(index);
                    sold += 1;
                }
                else
                    salesReport.Add(String.Format("{0} - её не купит", skin));

                index += 1;
            }

            saledIndexes.Reverse();

            foreach (int removeIndex in saledIndexes)
                Character.Protagonist.AnimalSkins.RemoveAt(removeIndex);

            salesReport.Add(String.Empty);
            salesReport.Add("BIG|ИТОГО:");
            salesReport.Add(String.Format("Вы продали шкур: {0}", sold));
            salesReport.Add(String.Format("GOOD|Вы получили: {0:f2}$", ToDollars(cents)));

            Character.Protagonist.Cents += cents;

            return salesReport;
        }
        
        public List<string> SellNuggets()
        {
            List<string> salesReport = new List<string>();

            int price = int.Parse(SellPrices);
            int cents = Character.Protagonist.Nuggets * price;
            Character.Protagonist.Cents += cents;

            salesReport.Add(String.Format("Вы продали самородков: {0}", Character.Protagonist.Nuggets));
            salesReport.Add(String.Format("Цена за один: {0:f2}$", ToDollars(price)));
            salesReport.Add(String.Format("GOOD|Вы получили: {0:f2}$", ToDollars(cents)));

            Character.Protagonist.Nuggets = 0;

            return salesReport;
        }

        public List<string> RedOrBlackGame()
        {
            List<string> gameReport = new List<string>();

            bool red = (Game.Dice.Roll() > 3);
            int dice = Game.Dice.Roll();
            bool even = (dice % 2 == 0);

            gameReport.Add(String.Format("Вы поставили на {0}", (red ? "красное (чёт)" : "чёрное (нечет)")));
            gameReport.Add(String.Format("На рулетке выпало: {0} - {1}", Game.Dice.Symbol(dice), (even ? "красное" : "чёрное")));

            if (red == even)
            {
                gameReport.Add("GOOD|Вы ВЫИГРАЛИ и получили 1$ :)");
                Character.Protagonist.Cents += 100;
            }
            else
            {
                gameReport.Add("BAD|Вы ПРОИГРАЛИ и потеряли 1$ :(");
                Character.Protagonist.Cents -= 100;
            }

            return gameReport;
        }

        public List<string> DoubleGame()
        {
            List<string> gameReport = new List<string>();

            int firstDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();

            gameReport.Add(String.Format("На рулетке выпали: {0} и {1}", Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice)));

            if (firstDice == secondDice)
            {
                gameReport.Add("GOOD|Цифры совпали - вы ВЫИГРАЛИ и получили 5$ :)");
                Character.Protagonist.Cents += 500;
            }
            else
            {
                gameReport.Add("BAD|Вы ПРОИГРАЛИ и потеряли 1$ :(");
                Character.Protagonist.Cents -= 100;
            }

            return gameReport;
        }

        public List<string> DiceLtlGame()
        {
            List<string> gameReport = new List<string>();

            int dice = Game.Dice.Roll();
            bool even = (dice % 2 == 0);

            gameReport.Add(String.Format("На кубике выпало: {0} - {1}", Game.Dice.Symbol(dice), (even ? "чётное" : "нечётное")));

            if (even)
            {
                gameReport.Add("GOOD|Вы ВЫИГРАЛИ и получаете 1$ :)");
                Character.Protagonist.Cents += 100;
            }
            else
            {
                gameReport.Add("BAD|Вы ПРОИГРАЛИ и потеряли 1$ :(");
                Character.Protagonist.Cents -= 100;
            }

            return gameReport;
        }

        public List<string> DiceGame()
        {
            List<string> gameReport = new List<string>();

            int dice = Game.Dice.Roll();
            bool even = (dice % 2 == 0);
            bool nuggetsGame = Game.Data.Triggers.Contains("Игра на самородок");

            gameReport.Add(String.Format("На кубике выпало: {0} - {1}", Game.Dice.Symbol(dice), (even ? "чётное" : "нечётное")));

            if (even)
            {
                gameReport.Add("GOOD|Вы ВЫИГРАЛИ :)");

                if (nuggetsGame)
                {
                    gameReport.Add("Самородок теперь ваш.");
                    Character.Protagonist.Nuggets += 1;
                }
                else
                {
                    gameReport.Add("Вы выиграли 3 доллара.");
                    Character.Protagonist.Cents += 300;
                }
            }
            else
            {
                gameReport.Add("BAD|Вы ПРОИГРАЛИ :(");

                if (nuggetsGame)
                {
                    gameReport.Add("Вы потеряли 1$");
                    Character.Protagonist.Cents -= 100;
                }
                else
                {
                    gameReport.Add("Вы потеряли 3$");
                    Character.Protagonist.Cents -= 300;
                }
            }

            return gameReport;
        }

        private bool NoMoreEnemies(List<Character> enemies) => enemies.Where(x => x.Strength > (EnemyWoundsLimit ? 2 : 0)).Count() == 0;

        private bool FirefightContinue(List<Character> enemies, ref List<string> fight, bool firefight)
        {
            if (!firefight)
                return false;

            if (Character.Protagonist.Cartridges > 0)
                return true;

            if (enemies.Where(x => x.Cartridges > 0).Count() > 0)
                return true;

            if (firefight)
            {
                fight.Add("BOLD|У всех закончились патроны, дальше бой продолжится на кулаках");
                fight.Add(String.Empty);
            }

            return false;
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            int round = 1;
            bool firefight = Firefight;

            Character hero = Character.Protagonist;

            while (true)
            {
                firefight = FirefightContinue(FightEnemies, ref fight, firefight);

                fight.Add(String.Format("HEAD|Раунд: {0}", round));

                bool attackAlready = false;
                int heroHitStrength = 0, enemyHitStrength = 0;

                foreach (Character enemy in FightEnemies)
                {
                    if (enemy.Strength <= 0)
                        continue;

                    string cartridgesLine = (enemy.Cartridges > 0 ? String.Format(", патронов {0}", enemy.Cartridges) : String.Empty);
                    fight.Add(String.Format("{0} (сила {1}{2})", enemy.Name, enemy.Strength, cartridgesLine));

                    bool noCartridges = Character.Protagonist.Cartridges <= 0;

                    if (!attackAlready && (!firefight || !noCartridges))
                    {
                        int heroRollFirst = Game.Dice.Roll();
                        int heroRollSecond = Game.Dice.Roll();
                        heroHitStrength = heroRollFirst + heroRollSecond + hero.Skill;

                        string heroHitLine = (firefight ? "Ваш выстрел" : "Мощность вашего удара");

                        fight.Add(String.Format("{0}: {1} + {2} + {3} = {4}",
                            heroHitLine, Game.Dice.Symbol(heroRollFirst), Game.Dice.Symbol(heroRollSecond), hero.Skill, heroHitStrength));

                        if (firefight)
                            Character.Protagonist.Cartridges -= 1;
                    }

                    if (!firefight || (enemy.Cartridges > 0))
                    {
                        int enemyRollFirst = Game.Dice.Roll();
                        int enemyRollSecond = Game.Dice.Roll();
                        enemyHitStrength = enemyRollFirst + enemyRollSecond + enemy.Skill;

                        string enemyHitLine = (firefight ? "Его выстрел" : "Мощность его удара");

                        fight.Add(String.Format("{0}: {1} + {2} + {3} = {4}",
                            enemyHitLine, Game.Dice.Symbol(enemyRollFirst), Game.Dice.Symbol(enemyRollSecond), enemy.Skill, enemyHitStrength));

                        if (firefight)
                            enemy.Cartridges -= 1;
                    }
                    else
                        enemyHitStrength = 0;

                    if ((heroHitStrength == 0) && (enemyHitStrength == 0))
                    { 
                        // nothing to do here
                    }
                    else if ((heroHitStrength > enemyHitStrength) && !attackAlready)
                    {
                        fight.Add(String.Format("GOOD|{0} ранен", enemy.Name));

                        enemy.Strength -= (firefight ? 3 : 2);

                        bool enemyLost = NoMoreEnemies(FightEnemies);

                        if (enemyLost)
                        {
                            fight.Add(String.Empty);
                            fight.Add(String.Format("BIG|GOOD|Вы ПОБЕДИЛИ :)"));
                            return fight;
                        }
                    }
                    else if (heroHitStrength > enemyHitStrength)
                    {
                        fight.Add(String.Format("BOLD|{0} не смог вас ранить", enemy.Name));
                    }
                    else if (heroHitStrength < enemyHitStrength)
                    {
                        fight.Add(String.Format("BAD|{0} ранил вас", enemy.Name));

                        hero.Strength -= (firefight ? 3 : 2);

                        if ((hero.Strength <= 0) || (HeroWoundsLimit && (hero.Strength <= 2)))
                        {
                            fight.Add(String.Empty);
                            fight.Add(String.Format("BIG|BAD|Вы ПРОИГРАЛИ :("));
                            return fight;
                        }
                    }
                    else
                        fight.Add(String.Format("BOLD|Ничья в раунде"));

                    attackAlready = true;

                    fight.Add(String.Empty);
                }

                round += 1;
            }
        }

        public override bool IsHealingEnabled() => Character.Protagonist.Strength < Character.Protagonist.MaxStrength;

        public override void UseHealing(int healingLevel)
        {
            if (healingLevel == -1)
                Character.Protagonist.Strength = Character.Protagonist.MaxStrength;
            else
                Character.Protagonist.Strength += healingLevel;
        }
    }
}
