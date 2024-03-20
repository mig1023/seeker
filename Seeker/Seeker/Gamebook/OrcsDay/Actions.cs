using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.OrcsDay
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public List<Character> Enemies { get; set; }

        public string Stat { get; set; }
        public int Level { get; set; }
        public bool LevelNull { get; set; }
        public bool OrcishnessTest { get; set; }
        public bool ZombiePotionTest { get; set; }
        public bool MortimerFight { get; set; }
        public bool LateHelp { get; set; }
        public bool GirlHelp { get; set; }
        public bool OrcsHelp { get; set; }
        public bool SecondGame { get; set; }

        public override List<string> Status() => new List<string>
        {
            $"Оркишность: {Game.Services.NegativeMeaning(Character.Protagonist.Orcishness)}",
            $"Здоровье: {Character.Protagonist.Hitpoints}/5",
        };

        public override List<string> AdditionalStatus() => new List<string>
        {
            $"Мышцы: {Game.Services.NegativeMeaning(Character.Protagonist.Muscle)}",
            $"Мозги: {Game.Services.NegativeMeaning(Character.Protagonist.Wits)}",
            $"Смелость: {Game.Services.NegativeMeaning(Character.Protagonist.Courage)}",
            $"Удача: {Game.Services.NegativeMeaning(Character.Protagonist.Luck)}",
            $"Деньги: {Character.Protagonist.Money}",
        };

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (OrcishnessTest && (Level > 0))
            {
                return new List<string> { $"Проверка {Constants.StatNames[Stat]}\n" +
                    $"уровень Оркишность ({Character.Protagonist.Orcishness}) + {Level}" };
            }
            else if (OrcishnessTest)
            {
                return new List<string> { $"Проверка {Constants.StatNames[Stat]}\n" +
                    $"по уровню Оркишности" };
            }
            else if (Level > 0)
            {
                return new List<string> { $"Проверка {Constants.StatNames[Stat]}, " +
                    $"уровень {Level}" };
            }
            else if (!String.IsNullOrEmpty(Stat))
            {
                return new List<string> { $"{Head}\n(текущее значение: " +
                    $"{Game.Services.NegativeMeaning(GetProperty(Character.Protagonist, Stat))})" };
            }
            else if (Price > 0)
            {
                return new List<string> { Head };
            }
            else if (Enemies == null)
            {
                return enemies;
            }

            foreach (Character enemy in Enemies)
            {
                enemies.Add($"{enemy.Name}\nатака " +
                    $"{enemy.Attack}  защита " +
                    $"{enemy.Defense}  здоровье " +
                    $"{enemy.Hitpoints}");
            }

            return enemies;
        }

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            if (ZombiePotionTest && !Game.Option.IsTriggered("Зелье управление зомби"))
            {
                return false;
            }
            else if (Used)
            {
                return false;
            }
            else if (Stat == "Bet")
            {
                if (secondButton)
                {
                    return Character.Protagonist.Bet > 1;
                }
                else
                {
                    return Character.Protagonist.Bet < 5;
                }
            }
            else
            {
                return String.IsNullOrEmpty(Stat) || (Character.Protagonist.StatBonuses > 0) ||
                    (Level > 0) || LevelNull || secondButton;
            }
        }

        public List<string> Get()
        {
            if (!String.IsNullOrEmpty(Stat))
                ChangeProtagonistParam(Stat, Character.Protagonist, "StatBonuses");

            if (Benefit != null)
                Benefit.Do();

            if (Price > 0)
                Used = true;

            return new List<string> { "RELOAD" };
        }

        public List<string> Decrease() =>
            ChangeProtagonistParam(Stat, Character.Protagonist, "StatBonuses", decrease: true);

        public List<string> OrcishnessInit() =>
            Calculations.Orcishness();

        public List<string> Test()
        {
            List<string> testLines = new List<string>();

            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            int currentStat = GetProperty(Character.Protagonist, Stat);

            bool okResult = false;

            if (OrcishnessTest)
            {
                okResult = (firstDice + secondDice) + currentStat >= Character.Protagonist.Orcishness + Level;
                string compareLine = okResult ? ">=" : "<";
                string level = Level > 0 ? $" + {Level}" : String.Empty;

                testLines.Add($"Проверка на {Constants.StatNames[Stat]}: " +
                    $"{Game.Dice.Symbol(firstDice)} + " +
                    $"{Game.Dice.Symbol(secondDice)} + {currentStat} " +
                    $"{compareLine} {Character.Protagonist.Orcishness}{level}");
            }
            else
            {
                okResult = (firstDice + secondDice) + currentStat >= Level;
                string compareLine = okResult ? ">=" : "<";

                testLines.Add($"Проверка на {Constants.StatNames[Stat]}: " +
                    $"{Game.Dice.Symbol(firstDice)} + " +
                    $"{Game.Dice.Symbol(secondDice)} + {currentStat} " +
                    $"{compareLine} {Level}");
            }

            testLines.Add(Result(okResult, "УСПЕШНО", "НЕУДАЧНО"));

            if (ZombiePotionTest && okResult)
            {
                testLines.Add("Теперь ты контролируешь Зомби-тролля!");
                Game.Option.Trigger("Зомби-тролль");
            }

            Game.Buttons.Disable(okResult,
                "Успешно," +
                "Ты смог убедить орков идти вместе с тобой биться с Мортимером," +
                "Твой разум победил орочью натуру и вы попытаетесь договориться," +
                "Повезло," +
                "При успехе приключенцы присоединяются к тебе",
                "Провально," +
                "В обратном случае орки нападают на тебя," +
                "Перейти к подсчёту результатов," +
                "Не повезло," +
                "В противном случае орк тебе не поверит," +
                "В случае провала приключенцы присоединяются к Мортимеру");

            return testLines;
        }

        public List<string> CourageTest()
        {
            List<string> testLines = new List<string>();

            bool okResult = Character.Protagonist.Courage >= Character.Protagonist.Orcishness + Character.Protagonist.Wits;
            string compareLine = okResult ? ">=" : "<";

            testLines.Add($"Проверка: {Character.Protagonist.Courage} " +
                $"Смелость {compareLine} {Character.Protagonist.Orcishness} " +
                $"Оркишность + {Character.Protagonist.Wits} Мозги");

            testLines.Add(Result(okResult, "УСПЕШНО", "НЕУДАЧНО"));

            Game.Buttons.Disable(okResult,
                "Успешно",
                "Если ты провалился, в тебе ещё слишком много от орка, чтобы отказаться от его предложения");

            return testLines;
        }

        public List<string> CardGames()
        {
            List<string> gameLines = new List<string>();

            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);

            int gameResult = (firstDice + secondDice) + Character.Protagonist.Luck;

            gameLines.Add($"Выпавшие карты: " +
                $"{Game.Dice.Symbol(firstDice)} + " +
                $"{Game.Dice.Symbol(secondDice)} + " +
                $"{Character.Protagonist.Luck} = {gameResult}");

            if (gameResult >= 15)
            {
                Character.Protagonist.Money += Character.Protagonist.Bet;
                gameLines.Add("GOOD|BIG|Получилось! Ты не только вернул ставку, но и выиграл столько же!");
            }
            else if (((gameResult < 12) && !SecondGame) || ((gameResult < 10) && SecondGame))
            {
                Character.Protagonist.Money -= Character.Protagonist.Bet;
                gameLines.Add("BAD|BIG|Провал! Ты потерял свою ставку!");
            }
            else
            {
                gameLines.Add("BIG|Выиграть не получилось, но ставка осталась при тебе!");
            }

            return gameLines;
        }

        public override bool Availability(string option) =>
            AvailabilityTrigger(option);

        public List<string> Fight()
        {
            List<string> fight = new List<string>();
            Character enemy = Enemies[0];

            bool otherOrcs = false;
            int otherOrcsHitpoints = 3, girlWounds = 3;
            bool magicPotion = GirlHelp && !Fights.VsAdvanturer(enemy.Name);

            if (OrcsHelp || Game.Option.IsTriggered("Много орков помогают"))
            {
                otherOrcs = true;
                otherOrcsHitpoints = (OrcsHelp ? 5 : 3);
            }

            if (MortimerFight && (otherOrcs || Game.Option.IsTriggered("Несколько орков помогают")))
            {
                fight.Add("BOLD|-2 к Атаке и Защите противника из-за помощи других орков\n");
                Fights.Bonus(enemy, sub: true);
            }
            
            if (Game.Option.IsTriggered("Зомби-тролль"))
            {
                fight.Add("BOLD|-3 к Атаке и Защите противника из-за помощи Зомби-тролля\n");
                Fights.Bonus(enemy, sub: true, bonusLevel: 3);
            }

            int round = 1;

            while (true)
            {
                fight.Add($"HEAD|BOLD|Раунд: {round}");
                fight.Add($"BOLD|{enemy.Name} нападает:");

                bool otherOrcsUnderAttack = false, girlUnderAttack = false, enemyAttackFail = false;

                if (MortimerFight && otherOrcs)
                {
                    int whoUnderAttack = Game.Dice.Roll();
                    otherOrcsUnderAttack = whoUnderAttack < 3;

                    fight.Add($"Кого атакует Мортимер: {Game.Dice.Symbol(whoUnderAttack)}");

                    if (!otherOrcsUnderAttack)
                        fight.Add("BOLD|Он атакует тебя");
                }
                else if (GirlHelp || OrcsHelp)
                {
                    int whoUnderAttack = Game.Dice.Roll();

                    if (GirlHelp)
                    {
                        girlUnderAttack = whoUnderAttack < 4;
                    }
                    else
                    {
                        otherOrcsUnderAttack = whoUnderAttack < 4;
                    }

                    fight.Add($"Кого атакует противник: {Game.Dice.Symbol(whoUnderAttack)}");

                    if (!girlUnderAttack && !otherOrcsUnderAttack)
                        fight.Add("BOLD|Он атакует тебя");
                }

                if (!otherOrcsUnderAttack && !girlUnderAttack)
                {
                    Game.Dice.DoubleRoll(out int enemyRollFirst, out int enemyRollSecond);
                    int protection = Fights.Protection(ref fight);

                    enemyAttackFail = (enemyRollFirst + enemyRollSecond) + protection >= enemy.Attack;
                    string attackFail = enemyAttackFail ? ">=" : "<";

                    fight.Add($"Удар врага: " +
                        $"{Game.Dice.Symbol(enemyRollFirst)} + " +
                        $"{Game.Dice.Symbol(enemyRollSecond)} + " +
                        $"{protection} {attackFail} {enemy.Attack}");
                }

                if (girlUnderAttack)
                {
                    girlWounds -= 1;

                    fight.Add($"BOLD|Противник атакует девушку!\n" +
                        $"Она теряет 1 Здоровье, осталось {girlWounds}");

                    if (girlWounds <= 0)
                    {
                        fight.Add("BAD|\nПротивник убил девушку!");

                        if (magicPotion)
                        {
                            fight.Add("GOOD|BOLD|Ты использовал целительное снадобье и " +
                                "её здоровье восстановлено!");

                            girlWounds = 3;
                            magicPotion = false;
                        }
                        else
                        {
                            fight.Add("BOLD|Дальше драться придётся тебе одному!");
                            GirlHelp = false;
                            Game.Option.Trigger("Девушка погибла");

                            if (Fights.VsAdvanturer(enemy.Name))
                            {
                                fight.Add("Приключенцы получают бонус к Атаке и Защите");
                                Fights.Bonus(enemy);
                            }
                        }
                    }
                }
                else if (otherOrcsUnderAttack)
                {
                    otherOrcsHitpoints -= 1;

                    fight.Add($"BOLD|Он атакует других орков!\n" +
                        $"Они теряют 1 Здоровье, осталось {otherOrcsHitpoints}");

                    if (otherOrcsHitpoints <= 0)
                    {
                        fight.Add("BAD|\nПротивник победил остальных орков и ты теряешь их помощь");
                        fight.Add("BOLD|Дальше драться придётся тебе одному\n");

                        OrcsHelp = false;
                        otherOrcs = false;
                        Fights.Bonus(enemy);
                    }
                }
                else if (enemyAttackFail)
                {
                    fight.Add("Ты отбил удар!");
                }
                else
                {
                    Character.Protagonist.Hitpoints -= 1;
                    fight.Add($"BAD|BOLD|{enemy.Name} ранил тебя");
                    fight.Add($"Твоё здоровье стало равно {Character.Protagonist.Hitpoints}");

                    if ((Character.Protagonist.Hitpoints == 2) && LateHelp)
                    {
                        fight.Add("BOLD|Другие орки присоединяются к бою!\n-2 к Атаке и Защите противника!");
                        Fights.Bonus(enemy, sub: true);
                    }
                }

                if (Character.Protagonist.Hitpoints <= 0)
                {
                    if (magicPotion)
                    {
                        fight.Add("GOOD|Ты использовал целительное снадобье и получаешь +3 к здоровью!\n");
                        Character.Protagonist.Hitpoints += 3;
                        magicPotion = false;
                    }
                    else
                    {
                        fight.Add(String.Empty);
                        fight.Add("BIG|BAD|Ты ПРОИГРАЛ :(");
                        return fight;
                    }
                }

                fight.Add(String.Empty);
                fight.Add("BOLD|Ты нападаешь:");

                Game.Dice.DoubleRoll(out int protRollFirst, out int protRollSecond);
                int protagonistAttack = (protRollFirst + protRollSecond) + Character.Protagonist.Muscle + Character.Protagonist.Weapon;
                bool protagonistAttackWin = protagonistAttack >= enemy.Defense;
                string weapon = Character.Protagonist.Weapon > 0 ? $" + {Character.Protagonist.Weapon} меч" : String.Empty;
                string compareLine = protagonistAttackWin ? ">=" : "<";

                fight.Add($"Твой удар: " +
                    $"{Game.Dice.Symbol(protRollFirst)} + " +
                    $"{Game.Dice.Symbol(protRollSecond)} + " +
                    $"{Character.Protagonist.Muscle}{weapon} {compareLine} {enemy.Defense}");

                if (protagonistAttackWin)
                {
                    enemy.Hitpoints -= 1;
                    fight.Add("GOOD|BOLD|Ты ранил противника!");
                    fight.Add($"Его здоровье стало равно {enemy.Hitpoints}");
                }
                else
                {
                    fight.Add("Противник отбил твой удар");
                }

                if (enemy.Hitpoints <= 0)
                {
                    Fights.WinTriggers(enemy.Name, GirlHelp);

                    fight.Add(String.Empty);
                    fight.Add("BIG|GOOD|Ты ПОБЕДИЛ :)");
                    return fight;
                }
                
                fight.Add(String.Empty);

                round += 1;
            }
        }

        public List<string> Calculation()
        {
            List<string> results = new List<string> { "BOLD|CЧИТАЕМ:" };

            int result = 0, lines = 0;

            if (Character.Protagonist.Orcishness <= 0)
            {
                results.Add("GOOD|+1 за то, что твоя Оркишность упала до нуля или ниже");

                result += 1;
                lines += 1;

                bool candidate = Game.Option.IsTriggered("Кандидат в Властелины");
                bool lord = Game.Option.IsTriggered("Тёмный Властелин");

                if (candidate && !lord)
                {
                    results.Add("GOOD|+1 за то, что стал новым Темным Властелином");
                    result += 1;
                }
            }

            foreach (KeyValuePair<string, string> trigger in Constants.ResultCalculation)
            {
                bool add = trigger.Value.Contains("+");
                string color = (add ? "GOOD" : "BAD");

                if (!Calculations.Condition(trigger.Key))
                    continue;

                results.Add($"{color}|{trigger.Value}");
                result += (add ? 1 : -1);
                lines += 1;
            }

            if (lines == 0)
                results.Add("Да уж, вообще нечего вспомнить...");

            results.Add($"BIG|BOLD|ИТОГО: {Game.Services.NegativeMeaning(result)}");

            return results;
        }

        public List<string> OvercomeOrcishness()
        {
            List<string> overcome = new List<string> { "BOLD|CЧИТАЕМ:" };

            while (true)
            {
                int sense = Character.Protagonist.Courage + Character.Protagonist.Wits;
                int orcishness = Character.Protagonist.Muscle + Character.Protagonist.Orcishness + 5;

                overcome.Add("BOLD|Борьба:");

                overcome.Add($"С одной стороны: {Character.Protagonist.Courage} (смелость) + " +
                    $"{Character.Protagonist.Wits} (мозги) = {sense}");

                overcome.Add($"С другой: {Character.Protagonist.Muscle} (мышцы) + " +
                    $"{Character.Protagonist.Orcishness} (оркишность) + 5 = {orcishness}");

                overcome.Add($"В результате: {sense} " +
                    $"{Game.Services.Сomparison(sense, orcishness)} {orcishness}");

                if (sense >= orcishness)
                {
                    overcome.Add("BOLD|GOOD|Ты выиграл!");
                    overcome.Add("Оркишность снизилась на единицу!");

                    Character.Protagonist.Orcishness -= 1;

                    if (Character.Protagonist.Orcishness <= 0)
                    {
                        overcome.Add("BIG|GOOD|Ты освободился от своей Оркской природы! :)");
                        overcome.Add("Ты получаешь за это дополнительно 3 единицы Смелости!");

                        Character.Protagonist.Courage += 3;

                        if (Game.Option.IsTriggered("Кандидат в Властелины"))
                        {
                            overcome.Add("\nBOLD|Ты стал Тёмным Властелином!");
                            Game.Option.Trigger("Тёмный Властелин");
                        }

                        return overcome;
                    }
                }
                else
                {
                    overcome.Add("BOLD|BAD|Ты проиграл!");
                    overcome.Add("Смелость снизилась на единицу!");

                    Character.Protagonist.Courage -= 1;

                    if (Character.Protagonist.Courage <= 0)
                    {
                        overcome.Add("BIG|BAD|Ты остался рабом своей Оркской природы :(");
                        return overcome;
                    }
                }
            }
        }

        public override bool IsHealingEnabled() =>
            Character.Protagonist.Hitpoints < 5;

        public override void UseHealing(int healingLevel) =>
            Character.Protagonist.Hitpoints += healingLevel;
    }
}
