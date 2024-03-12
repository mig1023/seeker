using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.StringOfWorlds
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public int RoundsToWin { get; set; }
        public bool HeroWoundsLimit { get; set; }
        public bool EnemyWoundsLimit { get; set; }
        public bool DevastatingAttack { get; set; }
        public bool DarknessPenalty { get; set; }
        public string Equipment { get; set; }

        public List<Character> Enemies { get; set; }

        public override List<string> Status() => new List<string>
        {
            $"Ловкость: {Character.Protagonist.Skill}",
            $"Сила: {Character.Protagonist.Strength}/{Character.Protagonist.MaxStrength}",
            $"Обаяние: {Character.Protagonist.Charm}",
        };

        public override List<string> StaticButtons()
        {
            List<string> staticButtons = new List<string> { };

            if (Game.Data.Constants.GetParagraphsWithoutStaticsButtons().Contains(Game.Data.CurrentParagraphID))
                return staticButtons;

            if ((Character.Protagonist.Equipment == "Тюбик") && (Character.Protagonist.Strength < Character.Protagonist.MaxStrength))
                staticButtons.Add("СЪЕСТЬ ПАСТУ");

            return staticButtons;
        }

        public override bool StaticAction(string action)
        {
            if (action == "СЪЕСТЬ ПАСТУ")
            {
                Character.Protagonist.Equipment = String.Empty;
                Character.Protagonist.Strength = Character.Protagonist.MaxStrength;

                return true;
            }

            return false;
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(Character.Protagonist.Strength, out toEndParagraph, out toEndText);

        public override bool IsButtonEnabled(bool secondButton = false) =>
            !(!String.IsNullOrEmpty(Equipment) && !String.IsNullOrEmpty(Character.Protagonist.Equipment));

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
                    return level <= Character.Protagonist.Blaster;

                else if (option.Contains("МОНЕТ >="))
                    return level <= Character.Protagonist.Coins;

                else if (option.Contains("БЛАСТЕР <"))
                    return level > Character.Protagonist.Blaster;

                else if (option.Contains("ОЧКИ"))
                    return Character.Protagonist.Equipment == "Очки";

                else if (option.Contains("ЗАЖИГАЛКА"))
                    return Character.Protagonist.Equipment == "Зажигалка";

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
                "BIG|" + Luckiness.Numbers()
            };

            int goodLuck = Game.Dice.Roll();
            bool isLuck = Character.Protagonist.Luck[goodLuck];
            string not = isLuck ? "не " : String.Empty;

            luckCheck.Add($"Проверка удачи: {Game.Dice.Symbol(goodLuck)} - {not}зачёркунтый");

            luckCheck.Add(Result(isLuck, "УСПЕХ", "НЕУДАЧА"));

            Game.Buttons.Disable(isLuck, "Повезло", "Не повезло");

            Character.Protagonist.Luck[goodLuck] = !Character.Protagonist.Luck[goodLuck];

            return luckCheck;
        }

        public List<string> LuckRecovery()
        {
            List<string> luckRecovery = new List<string> { "Восстановление удачи:" };

            bool success = false;

            for (int i = 1; i < 7; i++)
            {
                if (!Character.Protagonist.Luck[i])
                {
                    luckRecovery.Add($"GOOD|Цифра {i} восстановлена!");
                    Character.Protagonist.Luck[i] = true;
                    success = true;

                    break;
                }
            }

            if (!success)
                luckRecovery.Add("BAD|Все цифры и так счастливые!");

            luckRecovery.Add("Цифры удачи теперь:");
            luckRecovery.Add("BIG|" + Luckiness.Numbers());

            return luckRecovery;
        }

        public List<string> Charm()
        {
            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            bool goodCharm = (firstDice + secondDice) <= Character.Protagonist.Charm;
            string charmLine = goodCharm ? "<=" : ">";

            List<string> luckCheck = new List<string> {
                $"Проверка обаяния: " +
                $"{Game.Dice.Symbol(firstDice)} + " +
                $"{Game.Dice.Symbol(secondDice)} " +
                $"{charmLine} {Character.Protagonist.Charm}" };

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
            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            bool goodSkill = (firstDice + secondDice) <= Character.Protagonist.Skill;
            string skillLine = goodSkill ? "<=" : ">";

            List<string> luckCheck = new List<string> {
                $"Проверка ловкости: " +
                $"{Game.Dice.Symbol(firstDice)} + " +
                $"{Game.Dice.Symbol(secondDice)} " +
                $"{skillLine} {Character.Protagonist.Skill}" };

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
                diceCheck.Add($"На {i} выпало: {Game.Dice.Symbol(dice)}");
            }

            Character.Protagonist.Strength -= dices;

            diceCheck.Add($"BIG|BAD|Вы потеряли жизней: {dices}");

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

                diceGame.Add($"Вы бросили: " +
                    $"{Game.Dice.Symbol(firstDice)} + " +
                    $"{Game.Dice.Symbol(secondDice)} = {myResult}");

                Game.Dice.DoubleRoll(out int hisFirstDice, out int hisSecondDice);
                enemyResult = hisFirstDice + hisSecondDice;

                diceGame.Add($"Он бросил: " +
                    $"{Game.Dice.Symbol(hisFirstDice)} + " +
                    $"{Game.Dice.Symbol(hisSecondDice)} = {enemyResult}");

                diceGame.Add(String.Empty);
            }
            while (myResult == enemyResult);

            diceGame.Add(Result(myResult > enemyResult, "ВЫИГРАЛИ", "ПРОИГРАЛИ"));

            return diceGame;
        }

        public List<string> Break()
        {
            List<string> breakingDoor = new List<string> { "Ломаете дверь:" };

            bool succesBreaked = false;

            while (!succesBreaked && (Character.Protagonist.Strength > 0))
            {
                Game.Dice.DoubleRoll(out int firstDice, out int secondDice);

                if (firstDice == secondDice)
                {
                    succesBreaked = true;
                }
                else
                {
                    Character.Protagonist.Strength -= 1;
                }

                string result = succesBreaked ?
                    "удачный, дверь поддалась!" : "неудачный, -1 сила";

                breakingDoor.Add($"Удар: " +
                    $"{Game.Dice.Symbol(firstDice)} + " +
                    $"{Game.Dice.Symbol(secondDice)} = {result}");
            }

            breakingDoor.Add(Result(succesBreaked, "ДВЕРЬ ВЗЛОМАНА", "ВЫ УБИЛИСЬ ОБ ДВЕРЬ"));

            return breakingDoor;
        }

        public List<string> Bargain()
        {
            List<string> bargain = new List<string>();

            int numberOfDeals = 0;
            List<string> things = new List<string>
            {
                "Веер",
                "Полоска ароматической смолы",
                "Мягкая серебристая шкура"
            };

            foreach (string thing in things)
            {
                if (Game.Option.IsTriggered(thing))
                {
                    bargain.Add($"{thing} - графиня покупает за монету");
                    numberOfDeals += 1;
                }
            }

            if (numberOfDeals > 0)
            {
                string coins = Game.Services.CoinsNoun(numberOfDeals, "монету", "монеты", "монеты");
                bargain.Add($"BIG|ИТОГО: вы получили {numberOfDeals} {coins}");
            }
            else
            {
                bargain.Add("BIG|Вам нечего предложить графини :(");
            }

            Character.Protagonist.Coins += numberOfDeals;

            return bargain;
        }

        public List<string> Get()
        {
            if (!String.IsNullOrEmpty(Equipment))
                Character.Protagonist.Equipment = Equipment;

            return new List<string> { "RELOAD" };
        }

        private static bool NoMoreEnemies(List<Character> enemies, bool EnemyWoundsLimit) =>
            enemies.Where(x => x.Strength > (EnemyWoundsLimit ? 2 : 0)).Count() == 0;

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            int round = 1, skillPenalty = 0;

            if (DarknessPenalty && (Character.Protagonist.Equipment != "Очки"))
                skillPenalty += 1;

            while (true)
            {
                fight.Add($"HEAD|BOLD|Раунд: {round}");

                bool attackAlready = false;
                int protagonistHitStrength = 0;

                foreach (Character enemy in FightEnemies)
                {
                    if (enemy.Strength <= 0)
                        continue;

                    fight.Add($"{enemy.Name} (сила {enemy.Strength})");

                    if (!attackAlready)
                    {
                        Game.Dice.DoubleRoll(out int protagonistRollFirst, out int protagonistRollSecond);
                        int protagonistSkill = (Character.Protagonist.Skill - skillPenalty);
                        protagonistHitStrength = protagonistRollFirst + protagonistRollSecond + protagonistSkill;

                        fight.Add($"Мощность вашего удара: " +
                            $"{Game.Dice.Symbol(protagonistRollFirst)} + " +
                            $"{Game.Dice.Symbol(protagonistRollSecond)} + " +
                            $"{protagonistSkill} = {protagonistHitStrength}");
                    }

                    Game.Dice.DoubleRoll(out int enemyRollFirst, out int enemyRollSecond);
                    int enemyHitStrength = enemyRollFirst + enemyRollSecond + enemy.Skill;

                    fight.Add($"Мощность его удара: " +
                        $"{Game.Dice.Symbol(enemyRollFirst)} + " +
                        $"{Game.Dice.Symbol(enemyRollSecond)} + " +
                        $"{enemy.Skill} = {enemyHitStrength}");

                    if ((protagonistHitStrength > enemyHitStrength) && !attackAlready)
                    {
                        fight.Add($"GOOD|{enemy.Name} ранен");

                        enemy.Strength -= 2;

                        bool enemyLost = NoMoreEnemies(FightEnemies, EnemyWoundsLimit);

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

                        Character.Protagonist.Strength -= (DevastatingAttack ? 3 : 2);

                        if ((Character.Protagonist.Strength <= 0) || (HeroWoundsLimit && (Character.Protagonist.Strength <= 2)))
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
            Character.Protagonist.Strength < Character.Protagonist.MaxStrength;

        public override void UseHealing(int healingLevel) =>
            Character.Protagonist.Strength += healingLevel;
    }
}
