﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.BloodfeudOfAltheus
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();

        public int Dices { get; set; }

        public List<Character> Enemies { get; set; }
        public bool FightToDeath { get; set; }
        public bool LastWound { get; set; }
        public bool YourRacing { get; set; }
        public bool Ichor { get; set; }

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
                enemies.Add(String.Format("{0}\nсила {1}  защита {2}", enemy.Name.ToUpper(), enemy.Strength, enemy.Defence));

            return enemies;
        }

        public override List<string> Status() => new List<string>
        {
            String.Format("Сила: {0}", Character.Protagonist.Strength),
            String.Format("Защита: {0}", Character.Protagonist.Defence),
            String.Format("Слава: {0}", Character.Protagonist.Glory),
            String.Format("Позор: {0}", Character.Protagonist.Shame),
        };

        public override List<string> AdditionalStatus()
        {
            List<string> statusLines = new List<string>();

            Character.Protagonist.GetWeapons(out string name, out int strength, out int defence);

            statusLines.Add(String.Format("Оружие: {0} (сила {1}, защита {2})", name, strength, defence));
            statusLines.Add(String.Format("Покровитель: {0}", Character.Protagonist.Patron));

            return (statusLines.Count > 0 ? statusLines : null);
        }

        public override List<string> StaticButtons()
        {
            List<string> staticButtons = new List<string> { };

            if (Constants.GetParagraphsWithoutStaticsButtons().Contains(Game.Data.CurrentParagraphID))
                return staticButtons;

            if (IsPosibleResurrection())
                staticButtons.Add("ВОЗЗВАТЬ К ЗЕВСУ ЗА РАВНОДУШИЕМ");

            if (Character.Protagonist.Resurrection > 0)
                staticButtons.Add("ВОЗЗВАТЬ К ЗЕВСУ ЗА СЛАВОЙ");

            return staticButtons;
        }

        public override bool StaticAction(string action)
        {
            if (action == "ВОЗЗВАТЬ К ЗЕВСУ ЗА СЛАВОЙ")
            {
                if ((Character.Protagonist.Resurrection <= 0) && (Character.Protagonist.BroochResurrection > 0))
                {
                    Character.Protagonist.BroochResurrection -= 1;
                    Character.Protagonist.Glory -= 10;
                }
                else
                    Character.Protagonist.Resurrection -= 1;

                if (Character.Protagonist.Glory == 0)
                    Character.Protagonist.Glory = 1;
                else
                    Character.Protagonist.Glory += Game.Dice.Roll();

                return true;
            }

            if (action == "ВОЗЗВАТЬ К ЗЕВСУ ЗА РАВНОДУШИЕМ")
            {
                Character.Protagonist.Resurrection -= 1;
                Character.Protagonist.FellIntoFavor(String.Empty, indifferentToAll: true);

                return true;
            }

            return false;
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = "Позор Альтея невыносим, лучше начать сначала";
            
            return Character.Protagonist.Shame > Character.Protagonist.Glory;
        }

        public override bool IsButtonEnabled() =>
            !((Name == "DiceSpendGlory") && ((Character.Protagonist.Glory - Character.Protagonist.Shame) < 6));

        private static bool IsPosibleResurrection()
        {
            bool normal = (Character.Protagonist.Resurrection > 0);
            bool brooch = (Character.Protagonist.BroochResurrection > 0) && ((Character.Protagonist.Glory - Character.Protagonist.Shame) >= 10);

            return (normal || brooch);
        }

        public override bool CheckOnlyIf(string option)
        {
            if (option == "selectOnly")
                return true;

            string[] values = option.Split(' ');
            string value = (values.Length > 1 ? values[1] : "nope");

            if (option.Contains("!ПОКРОВИТЕЛЬ"))
                return Character.Protagonist.Patron != value;
            else if (option.Contains("ПОКРОВИТЕЛЬ"))
                return Character.Protagonist.Patron == value;

            if (option.Contains("БЕЗРАЗЛИЧЕН"))
                return !Character.Protagonist.IsGodsFavor(value) && !Character.Protagonist.IsGodsDisFavor(value);

            if (option.Contains("!БЛАГОСКЛОНЕН"))
                return !Character.Protagonist.IsGodsFavor(value);
            else if (option.Contains("БЛАГОСКЛОНЕН"))
                return Character.Protagonist.IsGodsFavor(value);

            if (option.Contains("!НЕМИЛОСТИВ"))
                return !Character.Protagonist.IsGodsDisFavor(value);
            else if (option.Contains("НЕМИЛОСТИВ"))
                return Character.Protagonist.IsGodsDisFavor(value);

            if (option.Contains("ВОСКРЕШЕНИЕ"))
                return IsPosibleResurrection();

            values = option.Split('>', '=');
            int level = (values.Length > 1 ? int.Parse(values[1]) : 0);

            if (option.Contains("СЛАВА >"))
                return level < Character.Protagonist.Glory;
            else if (option.Contains("СЛАВА <="))
                return level >= Character.Protagonist.Glory;

            if (option.Contains("ПОЗОР >"))
                return level < Character.Protagonist.Shame;
            else if (option.Contains("ПОЗОР <="))
                return level >= Character.Protagonist.Shame;

            return CheckOnlyIfTrigger(option);
        }

        public List<string> DiceCheck()
        {
            List<string> diceCheck = new List<string>();

            int firstDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();
            int sum = firstDice + secondDice;

            diceCheck.Add(String.Format("Кубики: {0} + {1} = {2}", Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), sum));

            Character hero = Character.Protagonist;
            int difference = hero.Glory - hero.Shame;

            string comparison = Game.Other.Сomparison(sum, difference);

            diceCheck.Add(String.Format("Разница между Славой и Позором: {0} - {1} = {2}", hero.Glory, hero.Shame, difference));
            diceCheck.Add(sum > difference ? String.Format("BIG|BAD|{0} :(", comparison.ToUpper()) : "BIG|GOOD|МЕНЬШЕ :)");

            return diceCheck;
        }

        public List<string> LtlDiceSpendGlory() => DiceSpendGlory(ltlDice: true);

        public List<string> LtlDiceSpendGloryWithOne() => DiceSpendGlory(ltlDice: true, addOne: true);

        public List<string> DiceSpendGlory(bool ltlDice = false, bool addOne = false)
        {
            List<string> spendGlory = new List<string>();

            int dice = Game.Dice.Roll();

            spendGlory.Add(String.Format("Кубик: {0}{1}", Game.Dice.Symbol(dice), (addOne ? " + 1" : String.Empty)));

            Dictionary<int, int> ltlDices = new Dictionary<int, int> { [1] = 1, [2] = 1, [3] = 2, [4] = 2, [5] = 3, [6] = 3 };

            dice = (ltlDice ? ltlDices[dice] : dice);

            if (addOne)
                dice += 1;

            Character.Protagonist.Glory -= dice;

            spendGlory.Add(String.Format("BIG|BAD|Вы потратили очков Славы: {0} :(", dice));

            return spendGlory;
        }
        
        public List<string> LanceDice()
        {
            List<string> lance = new List<string>();

            int dice = Game.Dice.Roll();

            lance.Add(String.Format("Кубик: {0}", Game.Dice.Symbol(dice)));

            if (dice <= 4)
            {
                Character.Protagonist.Shame += 1;

                lance.Add("BIG|BAD|Вы промахнулись :(");
                lance.Add("Вы получаете одно очко Позора");
            }
            else
                lance.Add("BIG|GOOD|Бросок достиг цели :)");

            return lance;
        }

        public List<string> RollDice()
        {
            List<string> roll = new List<string>();

            int dice = Game.Dice.Roll();

            roll.Add(String.Format("Кубик: {0}", Game.Dice.Symbol(dice)));

            if (dice >= 4)
            {
                Character.Protagonist.Ichor += 1;
                roll.Add("BIG|GOOD|Вам удалось :)");
            }
            else
                roll.Add("BIG|BAD|Вам не удалось :(");

            return roll;
        }

        public List<string> WithBareHands()
        {
            List<string> bareHands = new List<string>();

            int firstDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();
            int sum = firstDice + secondDice;
            bool success = (sum < Character.Protagonist.Strength);

            bareHands.Add(String.Format("Кубики: {0} + {1} = {2}", Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), sum));
            bareHands.Add(String.Format("Ваша Сила: {0} - {1}", Character.Protagonist.Strength, (success ? "больше!" : "меньше или равна...")));

            if (!success)
            {
                Character.Protagonist.Shame += 1;

                bareHands.Add("BIG|BAD|Вам не хватило Сил :(");
                bareHands.Add("Вы получаете одно очко Позора");
            }
            else
                bareHands.Add("BIG|GOOD|Вам хватило Силы :)");

            return bareHands;
        }

        public List<string> Racing()
        {
            List<string> racing = new List<string> { "ГОНКА НАЧИНАЕТСЯ!" };

            int[] teams = { 0, 0, 0, 0, 0, 0, 0 };
            string[] teamsColor = { String.Empty, "BLUE|", "RED|", "YELLOW|", "GREEN|" };
            string[] names = { String.Empty, "Cиняя", "Красная", "Жёлтая", "Зелёная" };

            int distance = (YourRacing ? 20 : 10);

            while (true)
            {
                int firstDice = Game.Dice.Roll();
                int secondDice = Game.Dice.Roll();

                bool diceDouble = (firstDice == secondDice);
                bool nobodyCantForward = (diceDouble && (teams[firstDice] == -1)) || ((teams[firstDice] == -1) && (teams[secondDice] == -1));

                racing.Add(String.Empty);
                racing.Add(String.Format(
                    "BOLD|Следующий бросок: {0} и {1}", Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice)
                ));

                if ((firstDice == 6) && diceDouble)
                {
                    racing.Add("BAD|Произошло столкновение!");

                    int crashDice = Game.Dice.Roll();

                    racing.Add(String.Format("Кубик столкновения: {0}", Game.Dice.Symbol(crashDice)));

                    if (crashDice < 5)
                    {
                        racing.Add(String.Format("BOLD|{0} команда выбывает из гонки!", names[crashDice]));
                        teams[crashDice] = -1;
                    }
                    else if (crashDice == 6)
                    {
                        racing.Add(String.Empty);
                        racing.Add("BIG|BAD|Произошла серьёзная авария, все колесницы выбывают, гонка остановлена :(");

                        return racing;
                    }
                    else
                        racing.Add("Происшествие было несерьёзным, все колесницы продолжают гонку");
                }
                else if (YourRacing && (Character.Protagonist.Patron == "Посейдон") && ((firstDice == 5) || (secondDice == 5)))
                {
                    racing.Add("Сам Посейдон помогает вам: Красная команда продвинулась вперёд!");
                    teams[2] += 1;
                }
                else if ((firstDice == 5) || (secondDice == 5) || nobodyCantForward)
                {
                    racing.Add("Никто не смог продвинуться вперёд");
                }
                else if (YourRacing && Character.Protagonist.IsGodsDisFavor("Посейдон") &&(firstDice == 6) || (secondDice == 6))
                {
                    racing.Add("Все команды продвинулись вперёд, кроме вашей - сам Посейдон выказывает вам свою немилость!");

                    foreach (int i in new List<int> { 1, 3, 4 })
                        if (teams[i] >= 0)
                            teams[i] += 1;
                }
                else if ((firstDice == 6) || (secondDice == 6))
                {
                    racing.Add("Все команды продвинулись вперёд");

                    foreach (int i in new List<int> { 1, 2, 3, 4 })
                        if (teams[i] >= 0)
                            teams[i] += 1;
                }
                else if (firstDice == secondDice)
                {
                    racing.Add(String.Format("{0} команда продвинулась сразу на два сектора!", names[firstDice]));
                    teams[firstDice] += 2;
                }
                else
                {
                    foreach (int i in new List<int> { firstDice, secondDice })
                        if (teams[i] >= 0)
                        {
                            racing.Add(String.Format("{0} команда продвинулась вперёд", names[i]));
                            teams[i] += 1;
                        }
                }

                int maxSector = 0;
                bool doubleMaxSector = false;
                int winner = 0;

                racing.Add(String.Empty);

                foreach (int i in new List<int> { 1, 2, 3, 4 })
                    if (teams[i] < 0)
                        racing.Add(String.Format("{0}{1} команда выбыла из гонки", teamsColor[i], names[i]));
                    else
                    {
                        string path = String.Empty;

                        for (int p = 0; p < teams[i]; p++)
                            path += "|";

                        racing.Add(String.Format("{0}{1}█", teamsColor[i], path));

                        if (teams[i] == maxSector)
                            doubleMaxSector = true;
                        else if (teams[i] > maxSector)
                        {
                            maxSector = teams[i];
                            doubleMaxSector = false;
                            winner = i;
                        }
                    }

                if ((maxSector >= distance) && !doubleMaxSector)
                {
                    racing.Add(String.Empty);

                    if (YourRacing)
                    {
                        string other = String.Format("BIG|{0}Вы проиграли, победила {0} команда :(", teamsColor[winner], names[winner]);
                        racing.Add(winner == 2 ? "BIG|RED|Вы ПОБЕДИЛИ, Красная команда пришла первой! :)" : other);
                    }
                    else
                        racing.Add(String.Format("BIG|{0}Гонка окончена, {1} команда победила!", teamsColor[winner], names[winner]));

                    return racing;
                }
                else
                    racing.Add("Гонка продолжается");
            }
        }

        private int UseGloryInFight(Character enemy, ref List<string> fight)
        {
            bool graveInjury = (Character.Protagonist.Health < 2);
            bool cantFightOtherwise = (Character.Protagonist.Strength + (graveInjury ? 6 : 12) < enemy.Defence);

            int availableGlory = (Character.Protagonist.Glory - Character.Protagonist.Shame);

            if (cantFightOtherwise && (availableGlory < 1))
            {
                fight.Add("Кажется, что положение безнадёжно...");
                return -1;
            }

            if (!cantFightOtherwise)
                return 0;

            int needGlory = (enemy.Defence - Character.Protagonist.Strength + (graveInjury ? 6 : 12) + 2);

            if (needGlory > availableGlory)
            {
                fight.Add("Не хватит очков Славы, чтобы что-то исправить...");
                return -1;
            }
            else
            {
                fight.Add("Вам придётся использовать Славу!");

                Character.Protagonist.Glory -= needGlory;
                return needGlory;
            }

        }

        private bool NoMoreEnemies(List<Character> enemies, bool noHealthy = false)
        {
            foreach (Character enemy in enemies)
            {
                if (!noHealthy && enemy.Health > 0)
                    return false;

                if (noHealthy && (enemy.Health > 1))
                    return false;
            }
               
            return true;
        }

        private int ComradeBonus(List<Character> enemies, int currentEnemy)
        {
            int bonus = 0;

            if ((currentEnemy + 1) == enemies.Count)
                return bonus;

            for (int i = currentEnemy + 1; i < enemies.Count; i++)
                if (enemies[i].Health > 1)
                    bonus += 1;

            return bonus;
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone(LastWound));

            int round = 1;

            Character hero = Character.Protagonist;

            hero.Health = 3;

            Character.Protagonist.GetWeapons(out string weaponName, out int weaponStrength, out int _);

            while (true)
            {
                fight.Add(String.Format("HEAD|Раунд: {0}", round));

                int currentEnemy = -1;

                foreach (Character enemy in FightEnemies)
                {
                    currentEnemy += 1;

                    if (enemy.Health <= 0) 
                        continue;

                    if (Ichor && (Character.Protagonist.Ichor > 0))
                    {
                        enemy.Strength -= 3;
                        enemy.Defence -= 3;

                        fight.Add(String.Format(
                            "Противник теряет 3 Силы и 3 Защиты из-за вытекающего ихора! Теперь его Сила равна {0}, а Защита - {1}!",
                            enemy.Strength, enemy.Defence));
                    }

                    fight.Add(String.Format("Вы: {0}ы, {1}: {2}",
                        Constants.HealthLine()[hero.Health], enemy.Name, Constants.HealthLine()[enemy.Health]));

                    int protagonistRollFirst = Game.Dice.Roll();
                    int protagonistRollSecond = 0;
                    string secondRollLine = String.Empty;
                    bool autoFail = false;

                    int useGlory = UseGloryInFight(enemy, ref fight);
                    string useGloryLine = (useGlory > 0 ? String.Format(" + {0} Славы", useGlory) : String.Empty);

                    if ((hero.Health > 1) || NoMoreEnemies(FightEnemies, noHealthy: true))
                    {
                        protagonistRollSecond = Game.Dice.Roll();
                        secondRollLine = String.Format(" + {0}", Game.Dice.Symbol(protagonistRollSecond));
                        autoFail = (protagonistRollFirst + protagonistRollSecond) < 4;
                    }
                    else
                        autoFail = (protagonistRollFirst == 1);

                    bool autoHit = (protagonistRollFirst + protagonistRollSecond) > 10;

                    int protagonistHitStrength = protagonistRollFirst + protagonistRollSecond + weaponStrength +
                        useGlory + Character.Protagonist.Strength;

                    fight.Add(String.Format("Мощность вашего удара: {0}{1} + {2} Сила + {3} {4}{5} = {6}",
                        Game.Dice.Symbol(protagonistRollFirst), secondRollLine, Character.Protagonist.Strength,
                        weaponStrength, weaponName, useGloryLine, protagonistHitStrength
                    ));

                    fight.Add(String.Format("Его защита: {0}", enemy.Defence));

                    if ((autoHit || (protagonistHitStrength > enemy.Defence)) && !autoFail)
                    {
                        fight.Add(String.Format("BOLD|GOOD|{0} ранен", enemy.Name));

                        enemy.Health -= 1;

                        bool enemyLost = NoMoreEnemies(FightEnemies, noHealthy: !FightToDeath);

                        if (enemyLost)
                        {
                            fight.Add(String.Empty);
                            fight.Add(String.Format("BIG|GOOD|Вы ПОБЕДИЛИ :)"));
                            return fight;
                        }
                    }
                    else
                        fight.Add(String.Format("BOLD|Вы не смогли ранить противника", enemy.Name));

                    int enemyRollFirst = Game.Dice.Roll();
                    int enemyRollSecond = 0;
                    string ememySecondRollLine = String.Empty;

                    if ((enemy.Health > 1) || NoMoreEnemies(FightEnemies, noHealthy: true))
                    {
                        enemyRollSecond = Game.Dice.Roll();
                        ememySecondRollLine = String.Format(" + {0}", Game.Dice.Symbol(enemyRollSecond));
                        autoFail = (enemyRollFirst + enemyRollSecond) < 4;
                    }
                    else
                        autoFail = (enemyRollFirst == 1);

                    autoHit = (enemyRollFirst + enemyRollSecond) > 10;

                    int enemyHitStrength = enemyRollFirst + enemyRollSecond + enemy.Strength;

                    int comradesBonus = ComradeBonus(FightEnemies, currentEnemy);
                    string comradesBonusLine = String.Empty;

                    if (comradesBonus > 0)
                    {
                        enemyHitStrength += comradesBonus;
                        comradesBonusLine = String.Format(" + {0} за товарищей", comradesBonus);
                    }

                    fight.Add(String.Format("Мощность его удара: {0}{1} + {2} Сила{3} = {4}",
                        Game.Dice.Symbol(enemyRollFirst), ememySecondRollLine, enemy.Strength, comradesBonusLine, enemyHitStrength
                    ));

                    Character.Protagonist.GetArmour(out int armourDefence, out string armourLine);

                    string needTotal = (String.IsNullOrEmpty(armourLine) ? String.Empty : String.Format(" = {0}", (hero.Defence + armourDefence)));

                    fight.Add(String.Format("Ваша защита: {0}{1}{2}", hero.Defence, armourLine, needTotal));

                    if ((autoHit || (enemyHitStrength > (hero.Defence + armourDefence))) && !autoFail)
                    {
                        fight.Add(String.Format("BOLD|BAD|{0} ранил вас", enemy.Name));

                        hero.Health -= 1;

                        if (hero.Health <= 0)
                        {
                            fight.Add(String.Empty);
                            fight.Add(String.Format("BIG|BAD|Вы ПРОИГРАЛИ :("));
                            return fight;
                        }
                    }
                    else
                        fight.Add(String.Format("BOLD|Противник не смог ранить вас", enemy.Name));

                    fight.Add(String.Empty);
                }

                round += 1;
            }
        }
    }
}
