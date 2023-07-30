using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.StringOfWorlds
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public int RoundsToWin { get; set; }
        public bool HeroWoundsLimit { get; set; }
        public bool EnemyWoundsLimit { get; set; }
        public bool DevastatingAttack { get; set; }
        public bool DarknessPenalty { get; set; }
        public string Equipment { get; set; }

        public List<Character> Enemies { get; set; }

        public override List<string> Status() => new List<string>
        {
            $"Ловкость: {protagonist.Skill}",
            $"Сила: {protagonist.Strength}/{protagonist.MaxStrength}",
            $"Обаяние: {protagonist.Charm}",
        };

        public override List<string> StaticButtons()
        {
            List<string> staticButtons = new List<string> { };

            if (Game.Data.Constants.GetParagraphsWithoutStaticsButtons().Contains(Game.Data.CurrentParagraphID))
                return staticButtons;

            if ((protagonist.Equipment == "Тюбик") && (protagonist.Strength < protagonist.MaxStrength))
                staticButtons.Add("СЪЕСТЬ ПАСТУ");

            return staticButtons;
        }

        public override bool StaticAction(string action)
        {
            if (action == "СЪЕСТЬ ПАСТУ")
            {
                protagonist.Equipment = String.Empty;
                protagonist.Strength = protagonist.MaxStrength;

                return true;
            }

            return false;
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(protagonist.Strength, out toEndParagraph, out toEndText);

        public override bool IsButtonEnabled(bool secondButton = false) =>
            !(!String.IsNullOrEmpty(Equipment) && !String.IsNullOrEmpty(protagonist.Equipment));

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else
            {
                string[] values = option.Split(new string[] { ">=", "<", " " }, StringSplitOptions.RemoveEmptyEntries);
                int level = (values.Length > 1 ? int.Parse(values[1]) : 0);

                if (option.Contains("БЛАСТЕР >="))
                    return level <= protagonist.Blaster;

                else if (option.Contains("МОНЕТ >="))
                    return level <= protagonist.Coins;

                else if (option.Contains("БЛАСТЕР <"))
                    return level > protagonist.Blaster;

                else if (option.Contains("ОЧКИ"))
                    return protagonist.Equipment == "Очки";

                else if (option.Contains("ЗАЖИГАЛКА"))
                    return protagonist.Equipment == "Зажигалка";

                else
                    return AvailabilityTrigger(option);
            }
        }

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (!String.IsNullOrEmpty(Head))
                return new List<string> { Head };

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
                enemies.Add($"{enemy.Name}\nловкость {enemy.Skill}  сила {enemy.Strength}");

            return enemies;
        }

        public List<string> Luck()
        {
            List<string> luckCheck = new List<string>
            {
                "Цифры удачи:",
                "BIG|" + Services.LuckNumbers()
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

            bool success = false;

            for (int i = 1; i < 7; i++)
            {
                if (!protagonist.Luck[i])
                {
                    luckRecovery.Add(String.Format("GOOD|Цифра {0} восстановлена!", i));
                    protagonist.Luck[i] = true;
                    success = true;

                    break;
                }
            }

            if (!success)
                luckRecovery.Add("BAD|Все цифры и так счастливые!");

            luckRecovery.Add("Цифры удачи теперь:");
            luckRecovery.Add("BIG|" + Services.LuckNumbers());

            return luckRecovery;
        }

        public List<string> Charm()
        {
            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            bool goodCharm = (firstDice + secondDice) <= protagonist.Charm;

            List<string> luckCheck = new List<string> { String.Format(
                "Проверка обаяния: {0} + {1} {2} {3}",
                Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), (goodCharm ? "<=" : ">"), protagonist.Charm) };

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

            List<string> luckCheck = new List<string> { String.Format(
                "Проверка ловкости: {0} + {1} {2} {3}",
                Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), (goodSkill ? "<=" : ">"), protagonist.Skill) };

            luckCheck.Add(goodSkill ? "BIG|GOOD|УСПЕХ :)" : "BIG|BAD|НЕУДАЧА :(");

            return luckCheck;
        }

        public List<string> DiceWounds()
        {
            List<string> diceCheck = new List<string> { };

            int dices = 0;

            for (int i = 1; i <= 2; i++)
            {
                int dice = Game.Dice.Roll();
                dices += dice;
                diceCheck.Add(String.Format("На {0} выпало: {1}", i, Game.Dice.Symbol(dice)));
            }

            protagonist.Strength -= dices;

            diceCheck.Add(String.Format("BIG|BAD|Вы потеряли жизней: {0}", dices));

            return diceCheck;
        }
        
        public List<string> GameOfDice()
        {
            List<string> diceGame = new List<string> { };

            int myResult, enemyResult;

            do
            {
                Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
                myResult = firstDice + secondDice;

                diceGame.Add(String.Format("Вы бросили: {0} + {1} = {2}",
                    Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), myResult));

                Game.Dice.DoubleRoll(out int hisFirstDice, out int hisSecondDice);
                enemyResult = hisFirstDice + hisSecondDice;

                diceGame.Add(String.Format("Он бросил: {0} + {1} = {2}",
                    Game.Dice.Symbol(hisFirstDice), Game.Dice.Symbol(hisSecondDice), enemyResult));

                diceGame.Add(String.Empty);
            }
            while (myResult == enemyResult);

            diceGame.Add(Result(myResult > enemyResult, "ВЫИГРАЛИ|ПРОИГРАЛИ"));

            return diceGame;
        }

        public List<string> Break()
        {
            List<string> breakingDoor = new List<string> { "Ломаете дверь:" };

            bool succesBreaked = false;

            while (!succesBreaked && (protagonist.Strength > 0))
            {
                Game.Dice.DoubleRoll(out int firstDice, out int secondDice);

                if (firstDice == secondDice)
                    succesBreaked = true;
                else
                    protagonist.Strength -= 1;

                string result = (succesBreaked ? "удачный, дверь поддалась!" : "неудачный, -1 сила");

                breakingDoor.Add(String.Format("Удар: {0} + {1} = {2}",
                    Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), result));
            }

            breakingDoor.Add(Result(succesBreaked, "ДВЕРЬ ВЗЛОМАНА|ВЫ УБИЛИСЬ ОБ ДВЕРЬ"));

            return breakingDoor;
        }

        public List<string> Bargain()
        {
            List<string> bargain = new List<string>();

            int numberOfDeals = 0;

            foreach (string thing in new List<string> { "Веер", "Полоска ароматической смолы", "Мягкая серебристая шкура" })
            {
                if (Game.Option.IsTriggered(thing))
                {
                    bargain.Add(String.Format("{0} - графиня покупает за монету", thing));
                    numberOfDeals += 1;
                }
            }

            if (numberOfDeals > 0)
            {
                string coins = Game.Services.CoinsNoun(numberOfDeals, "монету", "монеты", "монеты");
                bargain.Add(String.Format("BIG|ИТОГО: вы получили {0} {1}", numberOfDeals, coins));
            }
            else
                bargain.Add("BIG|Вам нечего предложить графини :(");

            protagonist.Coins += numberOfDeals;

            return bargain;
        }

        public List<string> Get()
        {
            if (!String.IsNullOrEmpty(Equipment))
                protagonist.Equipment = Equipment;

            return new List<string> { "RELOAD" };
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            int round = 1, skillPenalty = 0;

            if (DarknessPenalty && (protagonist.Equipment != "Очки"))
                skillPenalty += 1;

            while (true)
            {
                fight.Add(String.Format("HEAD|BOLD|Раунд: {0}", round));

                bool attackAlready = false;
                int protagonistHitStrength = 0;

                foreach (Character enemy in FightEnemies)
                {
                    if (enemy.Strength <= 0)
                        continue;

                    fight.Add(String.Format("{0} (сила {1})", enemy.Name, enemy.Strength));

                    if (!attackAlready)
                    {
                        Game.Dice.DoubleRoll(out int protagonistRollFirst, out int protagonistRollSecond);
                        int protagonistSkill = (protagonist.Skill - skillPenalty);
                        protagonistHitStrength = protagonistRollFirst + protagonistRollSecond + protagonistSkill;

                        fight.Add(String.Format("Мощность вашего удара: {0} + {1} + {2} = {3}",
                            Game.Dice.Symbol(protagonistRollFirst), Game.Dice.Symbol(protagonistRollSecond),
                            protagonistSkill, protagonistHitStrength));
                    }

                    Game.Dice.DoubleRoll(out int enemyRollFirst, out int enemyRollSecond);
                    int enemyHitStrength = enemyRollFirst + enemyRollSecond + enemy.Skill;

                    fight.Add(String.Format("Мощность его удара: {0} + {1} + {2} = {3}",
                        Game.Dice.Symbol(enemyRollFirst), Game.Dice.Symbol(enemyRollSecond), enemy.Skill, enemyHitStrength));

                    if ((protagonistHitStrength > enemyHitStrength) && !attackAlready)
                    {
                        fight.Add(String.Format("GOOD|{0} ранен", enemy.Name));

                        enemy.Strength -= 2;

                        bool enemyLost = Services.NoMoreEnemies(FightEnemies, EnemyWoundsLimit);

                        if (enemyLost)
                        {
                            fight.Add(String.Empty);
                            fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");
                            return fight;
                        }
                    }
                    else if (protagonistHitStrength > enemyHitStrength)
                    {
                        fight.Add(String.Format("BOLD|{0} не смог вас ранить", enemy.Name));
                    }
                    else if (protagonistHitStrength < enemyHitStrength)
                    {
                        fight.Add(String.Format("BAD|{0} ранил вас", enemy.Name));

                        protagonist.Strength -= (DevastatingAttack ? 3 : 2);

                        if ((protagonist.Strength <= 0) || (HeroWoundsLimit && (protagonist.Strength <= 2)))
                        {
                            fight.Add(String.Empty);
                            fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");
                            return fight;
                        }
                    }
                    else
                        fight.Add("BOLD|Ничья в раунде");

                    attackAlready = true;

                    if ((RoundsToWin > 0) && (RoundsToWin <= round))
                    {
                        fight.Add(String.Empty);
                        fight.Add("BAD|Отведённые на победу раунды истекли.");
                        fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");
                        return fight;
                    }

                    fight.Add(String.Empty);
                }

                round += 1;
            }
        }

        public override bool IsHealingEnabled() =>
            protagonist.Strength < protagonist.MaxStrength;

        public override void UseHealing(int healingLevel) =>
            protagonist.Strength += healingLevel;
    }
}
