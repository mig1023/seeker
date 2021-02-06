using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.BloodfeudOfAltheus
{
    class Actions : Abstract.IActions
    {
        public string ActionName { get; set; }
        public string ButtonName { get; set; }
        public string Aftertext { get; set; }
        public string Trigger { get; set; }
        public int Dices { get; set; }

        public List<Character> Enemies { get; set; }
        public bool FightToDeath { get; set; }
        public bool LastWound { get; set; }
        public bool YourRacing { get; set; }

        public List<string> Do(out bool reload, string action = "", bool trigger = false)
        {
            if (trigger)
                Game.Option.Trigger(Trigger);

            string actionName = (String.IsNullOrEmpty(action) ? ActionName : action);
            List<string> actionResult = typeof(Actions).GetMethod(actionName).Invoke(this, new object[] { }) as List<string>;

            reload = (actionResult.Count >= 1) && (actionResult[0] == "RELOAD");

            return actionResult;
        }

        public List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
                enemies.Add(String.Format("{0}\nсила {1}  защита {2}", enemy.Name.ToUpper(), enemy.Strength, enemy.Defence));

            return enemies;
        }

        public List<string> Status()
        {
            List<string> statusLines = new List<string>
            {
                String.Format("Сила: {0}", Character.Protagonist.Strength),
                String.Format("Защита: {0}", Character.Protagonist.Defence),
                String.Format("Слава: {0}", Character.Protagonist.Glory),
                String.Format("Позор: {0}", Character.Protagonist.Shame),
            };

            return statusLines;
        }

        public List<string> AdditionalStatus()
        {
            List<string> statusLines = new List<string>();

            Character.Protagonist.GetWeapons(out string name, out int strength, out int defence);

            statusLines.Add(String.Format("Оружие: {0} (сила {1}, защита {2})", name, strength, defence));
            statusLines.Add(String.Format("Покровитель: {0}", Character.Protagonist.Patron));

            if (statusLines.Count <= 0)
                return null;

            return statusLines;
        }

        public List<string> StaticButtons()
        {
            List<string> staticButtons = new List<string> { };

            if (Constants.GetParagraphsWithoutStaticsButtons().Contains(Game.Data.CurrentParagraphID))
                return staticButtons;

            if (Character.Protagonist.Resurrection > 0)
                return staticButtons;

            staticButtons.Add("ВОЗЗВАТЬ К ЗЕВСУ ЗА СЛАВОЙ");
            staticButtons.Add("ВОЗЗВАТЬ К ЗЕВСУ ЗА РАВНОДУШИЕМ");

            return staticButtons;
        }

        public bool StaticAction(string action)
        {
            if (action == "ВОЗЗВАТЬ К ЗЕВСУ ЗА СЛАВОЙ")
            {
                Character.Protagonist.Resurrection += 1;

                if (Character.Protagonist.Glory == 0)
                    Character.Protagonist.Glory = 1;
                else
                    Character.Protagonist.Glory += Game.Dice.Roll();

                return true;
            }

            if (action == "ВОЗЗВАТЬ К ЗЕВСУ ЗА РАВНОДУШИЕМ")
            {
                Character.Protagonist.Resurrection += 1;
                Character.Protagonist.FellIntoFavor(String.Empty, indifferentToAll: true);

                return true;
            }

            return false;
        }

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = "Позор Альтея невыносим, лучше начать сначала";
            
            return Character.Protagonist.Shame > Character.Protagonist.Glory;
        }

        public bool IsButtonEnabled()
        {
            bool disabledByGlory = (ActionName == "DiceSpendGlory") && ((Character.Protagonist.Glory - Character.Protagonist.Shame) < 6);

            return !disabledByGlory;
        }

        public static bool CheckOnlyIf(string option)
        {
            if (option == "selectOnly")
                return true;

            if (option.Contains("!ПОКРОВИТЕЛЬ"))
                return Character.Protagonist.Patron != option.Split(' ')[1];
            else if (option.Contains("ПОКРОВИТЕЛЬ"))
                return Character.Protagonist.Patron == option.Split(' ')[1];

            if (option.Contains("БЕЗРАЗЛИЧЕН"))
                return !Character.Protagonist.IsGodsFavor(option.Split(' ')[1]) && !Character.Protagonist.IsGodsDisFavor(option.Split(' ')[1]);

            if (option.Contains("!БЛАГОСКЛОНЕН"))
                return !Character.Protagonist.IsGodsFavor(option.Split(' ')[1]);
            else if (option.Contains("БЛАГОСКЛОНЕН"))
                return Character.Protagonist.IsGodsFavor(option.Split(' ')[1]);

            if (option.Contains("!НЕМИЛОСТИВ"))
                return !Character.Protagonist.IsGodsDisFavor(option.Split(' ')[1]);
            else if (option.Contains("НЕМИЛОСТИВ"))
                return Character.Protagonist.IsGodsDisFavor(option.Split(' ')[1]);

            if (option.Contains("ВОСКРЕШЕНИЕ"))
                return Character.Protagonist.Resurrection <= 0;

            if (option.Contains("СЛАВА >"))
                return int.Parse(option.Split('>')[1]) < Character.Protagonist.Glory;
            else if (option.Contains("СЛАВА <="))
                return int.Parse(option.Split('=')[1]) >= Character.Protagonist.Glory;

            if (option.Contains("ПОЗОР >"))
                return int.Parse(option.Split('>')[1]) < Character.Protagonist.Shame;
            else if (option.Contains("ПОЗОР <="))
                return int.Parse(option.Split('=')[1]) >= Character.Protagonist.Shame;

            if (option.Contains("!"))
            {
                if (Game.Data.Triggers.Contains(option.Replace("!", String.Empty).Trim()))
                    return false;
            }
            else if (!Game.Data.Triggers.Contains(option.Trim()))
                return false;

            return true;
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

            diceCheck.Add(String.Format("Разница между Славой и Позором: {0} - {1} = {2}", hero.Glory, hero.Shame, difference));


            diceCheck.Add(sum > difference ? "BIG|BAD|БОЛЬШЕ или РАВНО :(" : "BIG|GOOD|МЕНЬШЕ :)");

            return diceCheck;
        }

        public List<string> LtlDiceSpendGlory() => DiceSpendGlory(ltlDice: true);

        public List<string> LtlDiceSpendGloryWithOne() => DiceSpendGlory(ltlDice: true, addOne: true);

        public List<string> DiceSpendGlory(bool ltlDice = false, bool addOne = false)
        {
            List<string> spendGlory = new List<string>();

            int dice = Game.Dice.Roll();

            spendGlory.Add(String.Format("Кубики: {0}{1}", Game.Dice.Symbol(dice), (addOne ? " + 1" : String.Empty)));

            Dictionary<int, int> ltlDices = new Dictionary<int, int> { [1] = 1, [2] = 1, [3] = 2, [4] = 2, [5] = 3, [6] = 3 };

            dice = (ltlDice ? ltlDices[dice] : dice);

            if (addOne)
                dice += 1;

            Character.Protagonist.Glory -= dice;

            spendGlory.Add(String.Format("BIG|BAD|Вы потратили очков Славы: {0} :(", dice));

            return spendGlory;
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
                        racing.Add(
                            winner == 2 ? "BIG|RED|Вы ПОБЕДИЛИ, Красная команда пришла первой! :)" :
                            String.Format("BIG|{0}Вы проиграли, победила {0} команда :(", teamsColor[winner], names[winner])
                        );
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
            Dictionary<int, string> healthLine = new Dictionary<int, string>
            {
                [0] = "мертв",
                [1] = "тяжело ранен",
                [2] = "ранен",
                [3] = "здоров",
            };

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

                    fight.Add(String.Format("Вы: {0}ы, {1}: {2}", healthLine[hero.Health], enemy.Name, healthLine[enemy.Health]));

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

                    fight.Add(String.Format("Мощность вашего удара: {0}{1} + {2} Силы + {3} {4}{5} = {6}",
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

                    fight.Add(String.Format("Мощность его удара: {0}{1} + {2}{3} = {4}",
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
