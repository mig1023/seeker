using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.BloodfeudOfAltheus
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public int Dices { get; set; }

        public List<Character> Enemies { get; set; }
        public bool FightToDeath { get; set; }
        public bool Wound { get; set; }
        public bool LastWound { get; set; }
        public bool YourRacing { get; set; }
        public bool Ichor { get; set; }

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
                enemies.Add($"{enemy.Name.ToUpper()}\nсила {enemy.Strength}  защита {enemy.Defence}");

            return enemies;
        }

        public override List<string> Status() => new List<string>
        {
            $"Сила: {Character.Protagonist.Strength}",
            $"Защита: {Character.Protagonist.Defence}",
            $"Слава: {Character.Protagonist.Glory}",
            $"Позор: {Character.Protagonist.Shame}",
        };

        public override List<string> AdditionalStatus()
        {
            List<string> statusLines = new List<string>();

            statusLines.Add($"Покровитель: {Character.Protagonist.Patron}");

            Character.Protagonist.GetWeapons(out string name, out int strength, out int defence);
            statusLines.Add($"Оружие: {name} (сила {strength}, защита {defence})");

            Character.Protagonist.GetArmour(out int armour, out string armourLine, status: true);
            
            if (armour > 0)
                statusLines.Add($"Броня: {armourLine} (защита {armour})");

            return statusLines.Count > 0 ? statusLines : null;
        }

        public override List<string> StaticButtons()
        {
            List<string> staticButtons = new List<string> { };

            if (Game.Data.Constants.GetParagraphsWithoutStaticsButtons().Contains(Game.Data.CurrentParagraphID))
                return staticButtons;

            if (Resurrection.IsPosible())
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
                {
                    Character.Protagonist.Resurrection -= 1;
                }

                if (Character.Protagonist.Glory == 0)
                {
                    Character.Protagonist.Glory = 1;
                }
                else
                {
                    Character.Protagonist.Glory += Game.Dice.Roll();
                }

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

        public override bool IsButtonEnabled(bool secondButton = false) =>
            !((Type == "DiceSpendGlory") && ((Character.Protagonist.Glory - Character.Protagonist.Shame) < 6));

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option) || (option == "selectOnly") || (option == null) || String.IsNullOrEmpty(option))
            {
                return true;
            }
            else
            {
                string[] values = option.Split(' ');
                string value = (values.Length > 1 ? values[1] : "nope");

                if (option.Contains("ПОКРОВИТЕЛЯ НЕТ"))
                {
                    return String.IsNullOrEmpty(Character.Protagonist.Patron.Trim());
                }
                else if (option.Contains("!ПОКРОВИТЕЛЬ"))
                {
                    return Character.Protagonist.Patron != value;
                }
                else if (option.Contains("ПОКРОВИТЕЛЬ"))
                {
                    return Character.Protagonist.Patron == value;
                }

                if (option.Contains("БЕЗРАЗЛИЧЕН"))
                    return !Character.Protagonist.IsGodsFavor(value) && !Character.Protagonist.IsGodsDisFavor(value);

                if (option.Contains("!БЛАГОСКЛОНЕН"))
                {
                    return !Character.Protagonist.IsGodsFavor(value);
                }
                else if (option.Contains("БЛАГОСКЛОНЕН"))
                {
                    return Character.Protagonist.IsGodsFavor(value);
                }

                if (option.Contains("!НЕМИЛОСТИВ"))
                {
                    return !Character.Protagonist.IsGodsDisFavor(value);
                }
                else if (option.Contains("НЕМИЛОСТИВ"))
                {
                    return Character.Protagonist.IsGodsDisFavor(value);
                }
                    
                if (option.Contains("ВОСКРЕШЕНИЕ"))
                    return Resurrection.IsPosible();

                values = option.Split('>', '=');
                int level = (values.Length > 1 ? int.Parse(values[1]) : 0);

                if (option.Contains("СЛАВА >"))
                {
                    return level < Character.Protagonist.Glory;
                }
                else if (option.Contains("СЛАВА <="))
                {
                    return level >= Character.Protagonist.Glory;
                }
                    
                if (option.Contains("ПОЗОР >"))
                {
                    return level < Character.Protagonist.Shame;
                }
                else if (option.Contains("ПОЗОР <="))
                {
                    return level >= Character.Protagonist.Shame;
                }

                return AvailabilityTrigger(option);
            }
        }

        public List<string> DiceCheck()
        {
            List<string> diceCheck = new List<string>();

            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            int sum = firstDice + secondDice;

            diceCheck.Add($"Кубики: {Game.Dice.Symbol(firstDice)} " +
                $"+ {Game.Dice.Symbol(secondDice)} = {sum}");

            int difference = Character.Protagonist.Glory - Character.Protagonist.Shame;

            string comparison = Game.Services.Сomparison(sum, difference);

            diceCheck.Add($"Разница между Славой и Позором: " +
                $"{Character.Protagonist.Glory} - {Character.Protagonist.Shame} = {difference}");

            diceCheck.Add(sum > difference ? 
                $"BIG|BAD|{comparison.ToUpper()} :(" : "BIG|GOOD|МЕНЬШЕ :)");

            return diceCheck;
        }

        public List<string> LtlDiceSpendGlory() =>
            DiceSpendGlory(ltlDice: true);

        public List<string> LtlDiceSpendGloryWithOne() =>
            DiceSpendGlory(ltlDice: true, addOne: true);

        public List<string> DiceSpendGlory(bool ltlDice = false, bool addOne = false)
        {
            List<string> spendGlory = new List<string>();

            int dice = Game.Dice.Roll();
            string add = addOne ? " + 1" : String.Empty;

            spendGlory.Add($"Кубик: {Game.Dice.Symbol(dice)}{add}");

            Dictionary<int, int> ltlDices = new Dictionary<int, int> {
                [1] = 1, [2] = 1, [3] = 2, [4] = 2, [5] = 3, [6] = 3 };

            dice = (ltlDice ? ltlDices[dice] : dice);

            if (addOne)
                dice += 1;

            Character.Protagonist.Glory -= dice;

            spendGlory.Add($"BIG|BAD|Вы потратили очков Славы: {dice} :(");

            return spendGlory;
        }
        
        public List<string> LanceDice()
        {
            List<string> lance = new List<string>();

            int dice = Game.Dice.Roll();

            lance.Add($"Кубик: {Game.Dice.Symbol(dice)}");

            if (dice <= 4)
            {
                Character.Protagonist.Shame += 1;

                lance.Add("BIG|BAD|Вы промахнулись :(");
                lance.Add("Вы получаете одно очко Позора");
            }
            else
            {
                lance.Add("BIG|GOOD|Бросок достиг цели :)");
            }
                
            return lance;
        }

        public List<string> RollDice()
        {
            List<string> roll = new List<string>();

            int dice = Game.Dice.Roll();

            roll.Add($"Кубик: {Game.Dice.Symbol(dice)}");

            if (dice >= 4)
            {
                Character.Protagonist.Ichor += 1;
                roll.Add("BIG|GOOD|Вам удалось :)");
            }
            else
            {
                roll.Add("BIG|BAD|Вам не удалось :(");
            }

            return roll;
        }

        public List<string> WithBareHands()
        {
            List<string> bareHands = new List<string>();

            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            int sum = firstDice + secondDice;
            bool success = (sum < Character.Protagonist.Strength);
            string over = success ? "больше!" : "меньше или равна...";

            bareHands.Add($"Кубики: {Game.Dice.Symbol(firstDice)} + {Game.Dice.Symbol(secondDice)} = {sum}");
            bareHands.Add($"Ваша Сила: {Character.Protagonist.Strength} - {over}");

            if (!success)
            {
                Character.Protagonist.Shame += 1;

                bareHands.Add("BIG|BAD|Вам не хватило Сил :(");
                bareHands.Add("Вы получаете одно очко Позора");
            }
            else
            {
                bareHands.Add("BIG|GOOD|Вам хватило Силы :)");
            }

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
                Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
                bool diceDouble = (firstDice == secondDice);
                bool nobodyCantForward = (diceDouble && (teams[firstDice] == -1)) ||
                    ((teams[firstDice] == -1) && (teams[secondDice] == -1));

                racing.Add(String.Empty);
                racing.Add($"BOLD|Следующий бросок: {Game.Dice.Symbol(firstDice)} " +
                    $"и {Game.Dice.Symbol(secondDice)}");

                if ((firstDice == 6) && diceDouble)
                {
                    racing.Add("BAD|Произошло столкновение!");

                    int crashDice = Game.Dice.Roll();

                    racing.Add($"Кубик столкновения: {Game.Dice.Symbol(crashDice)}");

                    if (crashDice < 5)
                    {
                        racing.Add($"BOLD|{names[crashDice]} команда выбывает из гонки!");
                        teams[crashDice] = -1;
                    }
                    else if (crashDice == 6)
                    {
                        racing.Add(String.Empty);
                        racing.Add("BIG|BAD|Произошла серьёзная авария, " +
                            "все колесницы выбывают, гонка остановлена :(");

                        return racing;
                    }
                    else
                    {
                        racing.Add("Происшествие было несерьёзным, все колесницы продолжают гонку");
                    }
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
                else if (YourRacing && Character.Protagonist.IsGodsDisFavor("Посейдон") && ((firstDice == 6) || (secondDice == 6)))
                {
                    racing.Add("Все команды продвинулись вперёд, кроме вашей - " +
                        "сам Посейдон выказывает вам свою немилость!");

                    foreach (int i in new List<int> { 1, 3, 4 })
                        teams[i] += teams[i] >= 0 ? 1 : 0;
                }
                else if ((firstDice == 6) || (secondDice == 6))
                {
                    racing.Add("Все команды продвинулись вперёд");

                    foreach (int i in new List<int> { 1, 2, 3, 4 })
                        teams[i] += teams[i] >= 0 ? 1 : 0;
                }
                else if (firstDice == secondDice)
                {
                    racing.Add($"{names[firstDice]} команда продвинулась сразу на два сектора!");
                    teams[firstDice] += 2;
                }
                else
                {
                    foreach (int i in new List<int> { firstDice, secondDice })
                    {
                        if (teams[i] >= 0)
                        {
                            racing.Add($"{names[i]} команда продвинулась вперёд");
                            teams[i] += 1;
                        }
                    }
                }

                int maxSector = 0;
                bool doubleMaxSector = false;
                int winner = 0;

                racing.Add(String.Empty);

                foreach (int i in new List<int> { 1, 2, 3, 4 })
                {
                    if (teams[i] < 0)
                    {
                        racing.Add($"{teamsColor[i]}{names[i]} команда выбыла из гонки");
                    }
                    else
                    {
                        string path = String.Empty;

                        for (int p = 0; p < teams[i]; p++)
                            path += "|";

                        racing.Add($"{teamsColor[i]}{path}█");

                        if (teams[i] == maxSector)
                        {
                            doubleMaxSector = true;
                        }
                        else if (teams[i] > maxSector)
                        {
                            maxSector = teams[i];
                            doubleMaxSector = false;
                            winner = i;
                        }
                    }
                }

                if ((maxSector >= distance) && !doubleMaxSector)
                {
                    racing.Add(String.Empty);

                    if (YourRacing)
                    {
                        string other = $"BIG|{teamsColor[winner]}Вы проиграли, победила {names[winner]} команда :(";
                        racing.Add(winner == 2 ? "BIG|RED|Вы ПОБЕДИЛИ, Красная команда пришла первой! :)" : other);
                    }
                    else
                    {
                        racing.Add($"BIG|{teamsColor[winner]}Гонка окончена, {names[winner]} команда победила!");
                    }

                    return racing;
                }
            }
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone(Fights.WoundConverter(Wound, LastWound)));

            int round = 1;

            if (Game.Option.IsTriggered("Wounded"))
            {
                Character.Protagonist.Health = 2;
                Game.Option.Trigger("Wounded", remove: true);
            }
            else
            {
                Character.Protagonist.Health = 3;
            }

            Character.Protagonist.GetWeapons(out string weaponName, out int weaponStrength, out int _);

            while (true)
            {
                fight.Add($"HEAD|BOLD|Раунд: {round}");

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

                        fight.Add($"Противник теряет 3 Силы и 3 Защиты из-за вытекающего ихора! " +
                            $"Теперь его Сила равна {enemy.Strength}, а Защита - {enemy.Defence}!");
                    }

                    fight.Add($"Вы: {Constants.HealthLine[Character.Protagonist.Health]}ы, {enemy.Name}: " +
                        $"{Constants.HealthLine[enemy.Health]}");

                    int protagonistRollFirst = Game.Dice.Roll();
                    int protagonistRollSecond = 0;
                    string secondRollLine = String.Empty;
                    bool autoFail = false;

                    if ((Character.Protagonist.Health > 1) || Fights.EveryoneIsSeriouslyWounded(Character.Protagonist, FightEnemies))
                    {
                        protagonistRollSecond = Game.Dice.Roll();
                        secondRollLine = $" + {Game.Dice.Symbol(protagonistRollSecond)}";
                        autoFail = (protagonistRollFirst + protagonistRollSecond) < 4;
                    }
                    else
                    {
                        Fights.OnlyOneDice(ref fight);
                        autoFail = (protagonistRollFirst == 1);
                    }

                    bool autoHit = (protagonistRollFirst + protagonistRollSecond) > 10;

                    int protagonistHitStrength = protagonistRollFirst + protagonistRollSecond +
                        weaponStrength + Character.Protagonist.Strength;

                    string useGloryLine = String.Empty;

                    int useGlory = Fights.UseGloryInFight(enemy,
                        protagonistHitStrength, autoHit, autoFail, ref fight);

                    if (useGlory > 0)
                    {
                        useGloryLine = $" + {useGlory} Славы";
                        protagonistHitStrength += useGlory;
                    }

                    fight.Add($"Мощность вашего удара: {Game.Dice.Symbol(protagonistRollFirst)}" +
                        $"{secondRollLine} + {Character.Protagonist.Strength} Сила + {weaponStrength} " +
                        $"{weaponName}{useGloryLine} = {protagonistHitStrength}");

                    if (autoHit)
                    {
                        fight.Add("GRAY|На кубиках выпало больше 10, вы попадаете авоматически!");
                    }
                    else if (autoFail)
                    {
                        fight.Add("GRAY|На кубиках выпало слишком мало, вы промахиваетесь авоматически!");
                    }
                    else
                    {
                        fight.Add($"Его защита: {enemy.Defence}");
                    }

                    if ((autoHit || (protagonistHitStrength > enemy.Defence)) && !autoFail)
                    {
                        fight.Add($"BOLD|GOOD|{enemy.Name} ранен");

                        enemy.Health -= 1;

                        bool enemyLost = Fights.NoMoreEnemies(FightEnemies, noHealthy: !FightToDeath);

                        if (enemyLost)
                        {
                            fight.Add(String.Empty);
                            fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");
                            return fight;
                        }
                    }
                    else
                    {
                        fight.Add($"BOLD|Вы не смогли ранить противника");
                    }

                    int enemyRollFirst = Game.Dice.Roll();
                    int enemyRollSecond = 0;
                    string ememySecondRollLine = String.Empty;

                    if ((enemy.Health > 1) || Fights.EveryoneIsSeriouslyWounded(Character.Protagonist, FightEnemies))
                    {
                        enemyRollSecond = Game.Dice.Roll();
                        ememySecondRollLine = $" + {Game.Dice.Symbol(enemyRollSecond)}";
                        autoFail = (enemyRollFirst + enemyRollSecond) < 4;
                    }
                    else
                    {
                        Fights.OnlyOneDice(ref fight);
                        autoFail = (enemyRollFirst == 1);
                    }

                    autoHit = (enemyRollFirst + enemyRollSecond) > 10;

                    int enemyHitStrength = enemyRollFirst + enemyRollSecond + enemy.Strength;

                    int comradesBonus = Fights.ComradeBonus(FightEnemies, currentEnemy);
                    string comradesBonusLine = String.Empty;

                    if (comradesBonus > 0)
                    {
                        enemyHitStrength += comradesBonus;
                        comradesBonusLine = $" + {comradesBonus} за товарищей";
                    }

                    fight.Add($"Мощность его удара: {Game.Dice.Symbol(enemyRollFirst)}" +
                        $"{ememySecondRollLine} + {enemy.Strength} Сила{comradesBonusLine} = " +
                        $"{enemyHitStrength}");

                    Character.Protagonist.GetArmour(out int armourDefence, out string armourLine);

                    string needTotal = String.IsNullOrEmpty(armourLine) ?
                        String.Empty : $" = {Character.Protagonist.Defence + armourDefence}";

                    if (autoHit)
                    {
                        fight.Add("GRAY|На кубиках выпало больше 10, он попадает авоматически!");
                    }
                    else if (autoFail)
                    {
                        fight.Add("GRAY|На кубиках выпало слишком мало, он промахиваетесь авоматически!");
                    }
                    else
                    {
                        fight.Add($"Ваша защита: {Character.Protagonist.Defence}{armourLine}{needTotal}");
                    }
                        
                    if ((autoHit || (enemyHitStrength > (Character.Protagonist.Defence + armourDefence))) && !autoFail)
                    {
                        fight.Add($"BOLD|BAD|{enemy.Name} ранил вас");

                        if (Game.Option.IsTriggered("FirstWoundProtection"))
                        {
                            fight.Add("GOOD|Асклепий защитил вас от этого удара!");
                            Game.Option.Trigger("FirstWoundProtection", remove: true);
                        }
                        else
                        {
                            Character.Protagonist.Health -= 1;
                        }

                        if (Character.Protagonist.Health <= 0)
                        {
                            fight.Add(String.Empty);
                            fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");
                            return fight;
                        }
                    }
                    else
                    {
                        fight.Add("BOLD|Противник не смог ранить вас");
                    }

                    fight.Add(String.Empty);
                }

                round += 1;
            }
        }
    }
}
