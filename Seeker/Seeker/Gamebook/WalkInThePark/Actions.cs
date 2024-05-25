using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.WalkInThePark
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public List<Character> Enemies { get; set; }

        public bool Authority { get; set; }
        public bool FourRounds { get; set; }
        public bool StrengthBonus { get; set; }

        private static Random rand = new Random();

        private static bool IfThisIsFirstPart() =>
            Game.Data.CurrentParagraphID <= Constants.FirstPartSize;

        public override List<string> Status()
        {
            if (IfThisIsFirstPart())
            {
                return new List<string>
                {
                    $"Сила: {Character.Protagonist.Strength}",
                    $"Выносливость: {(double)Character.Protagonist.Endurance / 10}",
                    $"Удача: {Character.Protagonist.Luck}",
                };
            }
            else
            {
                return new List<string>
                {
                    $"Самочувствие: {Character.Protagonist.Health} / {Character.Protagonist.MaxHealth}",
                    $"Мочесть: {(double)Character.Protagonist.Strength}",
                    $"Тихопопость: {Character.Protagonist.Stealth}",
                    $"Фавор: {Character.Protagonist.Fortune}",
                };
            }   
        }

        public override List<string> AdditionalStatus()
        {
            if (IfThisIsFirstPart())
            {
                return new List<string>
                {
                    $"Оружие: {Character.Protagonist.Weapon} (урон {(double)Character.Protagonist.Damage / 10})",
                    $"Деньги: {Character.Protagonist.Money} руб",
                };
            }
            else
            {
                return new List<string>
                {
                    $"Оружие: {Character.Protagonist.Weapon} (урон {Character.Protagonist.Damage})",
                    $"Деньги: {Character.Protagonist.Money} руб",
                    $"Рейтинг: {Character.Protagonist.Rating}",
                    $"Частей карты: {Character.Protagonist.MapParts}",
                };
            }
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText)
        {
            if (IfThisIsFirstPart())
            {
                return GameOverBy(Character.Protagonist.Endurance, out toEndParagraph, out toEndText);
            }
            else if (Game.Data.CurrentParagraphID == 200)
            {
                toEndParagraph = 0;
                toEndText = Output.Constants.GAMEOVER_TEXT;
                return true;
            }
            else
            {
                toEndParagraph = 200;
                toEndText = "Финита ля комедия";
                return GameOverBy(Character.Protagonist.Health, out _, out _);
            }
        }

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Price > 0)
            {
                string price = Game.Services.CoinsNoun(Price, "рубль", "рубля", "рублей");
                return new List<string> { $"{Head}, {Price} {price}" };
            }

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
            {
                if (IfThisIsFirstPart())
                {
                    enemies.Add($"{enemy.Name}\nсила {enemy.Strength}  " +
                        $"выносливость {enemy.Endurance}  урон {(double)enemy.Damage / 10}");
                }
                else
                {
                    enemies.Add($"{enemy.Name}\nсамочувствие {enemy.Health}  " +
                        $"мочность {enemy.Strength}  урон {(double)enemy.Damage}");
                }
            }

            return enemies;
        }

        public List<string> Luck()
        {
            List<string> luck = Test(Character.Protagonist.Luck,
                "Проверка фарта", "ФАРТАНУЛО", "НЕ ФАРТАНУЛО", "Повезло", "Не повезло", out _);

            if (Character.Protagonist.Luck > 0)
            {
                Character.Protagonist.Luck -= 1;
                luck.Add("Уровень удачи снижен на единицу");
            }

            return luck;
        }

        public List<string> Fortune()
        {
            List<string> luck = Test(Character.Protagonist.Fortune,
                "Проверка фавора", "ВЫ В ФАВОРЕ", "ВЫ НЕ В ФАВОРЕ",
                "В фаворе, Под пытками он во всем сознался", "Не в фаворе, Он ничего не рассказал", out _);

            if (Character.Protagonist.Fortune > 0)
            {
                Character.Protagonist.Fortune -= 1;
                luck.Add("Уровень фавора снижен на единицу");
            }

            return luck;
        }

        public List<string> Stealth()
        {
            List<string> luck = Test(Character.Protagonist.Stealth,
                "Проверка Тихопопости", "ПОЛНЫЙ УСПЕХ в ТИХОПОПОСТИ", "ПОЛНЫЙ ПРОВАЛ в ТИХОПОПОСТИ",
                "Проканало, Выгорело, Она вынесла все твои пытки и козни",
                "Не проканало, Не выгорело, Откинулась в мучениях", out bool success);

            if (success)
            {
                Character.Protagonist.Stealth += 1;
                luck.Add("Уровень Тихопопости вырос на единицу");
            }
            else if (Character.Protagonist.Stealth > 1)
            {
                Character.Protagonist.Stealth -= 1;
                luck.Add("Уровень Тихопопости снижен на единицу");
            }

            return luck;
        }

        public List<string> Dodge()
        {
            int dice = Game.Dice.Roll();

            List<string> dodge = new List<string> { $"BIG|Уклоняемся: {Game.Dice.Symbol(dice)}" };

            if (dice < Character.Protagonist.Strength)
            {
                dodge.Add($"Это меньше, чем уровень МОЧЕСТИ в {Character.Protagonist.Strength} ед.!");
                dodge.Add("GOOD|BIG|BOLD|Извернулся!");
            }
            else if (dice > Character.Protagonist.Strength)
            {
                Character.Protagonist.Health -= 4;

                dodge.Add($"Это больше, чем уровень МОЧЕСТИ в {Character.Protagonist.Strength} ед.!");
                dodge.Add("BAD|BIG|BOLD|Гвоздь попал прямо в рыло!");
                dodge.Add("BAD|Теряешь 4 ед. Самочувствия...!");
            }
            else
            {
                dodge.Add($"Это равно уровню МОЧЕСТИ в {Character.Protagonist.Strength} ед.");
                dodge.Add("BOLD|BIG|Пёс его знает чего случилось, вроде пронесло...");
            }

            return dodge;
        }

        public List<string> Test(int param, string test, string testOk, string testFail,
            string buttonOk, string buttonFail, out bool testPassed)
        {
            int dice = Game.Dice.Roll();

            testPassed = dice <= param;
            string testLine = testPassed ? "<=" : ">";

            List<string> testResult = new List<string> {
                $"{test}: {Game.Dice.Symbol(dice)} " +
                $"{testLine} {param}" };

            testResult.Add(testPassed ? $"BIG|GOOD|{testOk} :)" : $"BIG|BAD|{testFail} :(");

            Game.Buttons.Disable(testPassed, buttonOk, buttonFail);

            return testResult;
        }

        public List<string> Rating()
        {
            List<string> ratingReport = new List<string>();
            Dictionary<string, List<int>> rating = new Dictionary<string, List<int>>();

            foreach (KeyValuePair<string, string> ratingLine in Constants.Rating)
            {
                string name = ratingLine.Key;
                string[] values = ratingLine.Value.Split(':');
                rating.Add(name, new List<int> { int.Parse(values[0]), int.Parse(values[1]) });
            }

            foreach (KeyValuePair<string, List<int>> line in rating.OrderByDescending(x => x.Value[0]))
            {
                int rated = Character.Protagonist.Rating;
                int min = line.Value[0];
                int max = line.Value[1];
                bool selected = (rated >= min) && (rated <= max);
                string selectedLine = selected ? "BIG|BOLD|" : "GRAY|";
                string minLine = min < -41 ? "от дна" : $"от {min}";
                string maxLine = max > 51 ? "до бесконечности" : $"до {max}";

                ratingReport.Add($"{selectedLine}{line.Key} ({minLine} {maxLine})");
            }

            return ratingReport;
        }

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            bool disabledGetOptions = (Price > 0) && Used;
            bool disabledByPrice = (Price > 0) && (Character.Protagonist.Money < Price);

            return !(disabledGetOptions || disabledByPrice);
        }

        public List<string> Get()
        {
            if ((Price > 0) && (Character.Protagonist.Money >= Price))
            {
                Character.Protagonist.Money -= Price;

                if (!Multiple)
                    Used = true;

                if (Benefit != null)
                {
                    Benefit.Do();
                }
                else if (BenefitList != null)
                {
                    foreach (Modification modification in BenefitList)
                        modification.Do();
                }
            }

            return new List<string> { "RELOAD" };
        }

        public override bool Availability(string option)
        {
            if (option == "пиво")
            {
                foreach (string trigger in Game.Data.Triggers)
                {
                    if (trigger.StartsWith("пиво"))
                        return true;
                }

                return false;
            }
            else
            {
                return AvailabilityTrigger(option);
            }
        }
            

        private static bool NoMoreEnemies(List<Character> enemies)
        {
            bool part1 = IfThisIsFirstPart();

            int enemiesCount = enemies
                .Where(x => x.GetHealth(part1) > 0)
                .Count();

            return enemiesCount == 0;
        }

        private static string Hit() =>
            $"{Constants.What[rand.Next(Constants.What.Count)]} {Constants.Where[rand.Next(Constants.Where.Count)]}";

        public List<string> EpicFight()
        {
            List<string> fight = new List<string>();

            int popCount = 25;
            int rockCount = 25;
            int round = 0;
            bool isFortune = Character.Protagonist.Fortune > 0;
            bool popSide = Game.Option.IsTriggered("за попсу");
            bool rockSide = Game.Option.IsTriggered("за рокеров");

            while ((popCount > 0) && (rockCount > 0))
            {
                round += 1;
                fight.Add($"HEAD|BOLD|\n*  *  *  РАУНД {round} *  *  *\n");

                int rockDice = Game.Dice.Roll();
                fight.Add($"Кубик рокеров: {Game.Dice.Symbol(rockDice)}");

                int popDice = Game.Dice.Roll();
                fight.Add($"Кубик попсы: {Game.Dice.Symbol(popDice)}");

                if ((rockDice < popDice) && isFortune && rockSide)
                {
                    fight.Add($"Ты добавляешь рокерам свой фавор: +{Character.Protagonist.Fortune}");
                    rockDice += Character.Protagonist.Fortune;
                }
                else if ((rockDice > popDice) && isFortune && popSide)
                {
                    fight.Add($"Ты добавляешь попсе свой фавор: +{Character.Protagonist.Fortune}");
                    popDice += Character.Protagonist.Fortune;
                }

                if (rockDice < popDice)
                {
                    fight.Add($"{popDice} у попсы больше {rockDice} у рокеров");

                    rockCount -= 5;

                    string goodOrBad = popSide ? "GOOD" : "BAD";
                    fight.Add($"BOLD|{goodOrBad}|РОКЕРЫ ОГРЕБЛИ!!");
                    fight.Add($"{goodOrBad}|Их стало на 5 рыл меньше!");

                    if (rockCount > 0)
                        fight.Add($"Всего рокеров осталось: {rockCount} рыл");
                }
                else if (rockDice > popDice)
                {
                    fight.Add($"{rockDice} у рокеров больше {popDice} у попсы");

                    popCount -= 5;

                    string goodOrBad = rockSide ? "GOOD" : "BAD";
                    fight.Add($"BOLD|{goodOrBad}|ПОПСА ОГРЕБЛА!!");
                    fight.Add($"{goodOrBad}|Их стало на 5 харь меньше!");

                    if (popCount > 0)
                        fight.Add($"Всего попсы осталось: {popCount} харь");
                }
                else
                {
                    fight.Add($"Рокеры и попса выкинули одинаковые кубики!");
                    fight.Add($"BOLD|В этом раунде они просто чутка подрихтовали друг другу морды!");
                }
            }

            fight.Add($"BIG|BOLD|БИТВА ОКОНЧЕНА!");

            if ((rockCount > 0) && rockSide)
            {
                fight.Add("GOOD|BIG|Твои победили! :)");
                Game.Buttons.Disable("Победили попсисты (а ты был за них), Твои проиграли (за кого бы ни был)");
            }
            else if ((popCount > 0) && popSide)
            {
                fight.Add("GOOD|BIG|Твои победили! :)");
                Game.Buttons.Disable("Победили рокеры (и при этом ты за них), Твои проиграли (за кого бы ни был)");
            }
            else
            {
                fight.Add("BAD|BIG|Твои продули! :(");
                Game.Buttons.Disable("Победили рокеры (и при этом ты за них), Победили попсисты (а ты был за них)");
            }

            return fight;
        }

        public List<string> Fight()
        {
            bool part1 = IfThisIsFirstPart();

            List<string> fight = new List<string>();
            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            int round = 1;
            bool deviceUsed = false;
            bool armour = Game.Option.IsTriggered("бронированный полушубок");

            while (true)
            {
                fight.Add($"HEAD|BOLD|Раунд: {round}");

                foreach (Character enemy in FightEnemies)
                {
                    if (enemy.GetHealth(part1) <= 0)
                        continue;

                    if (part1)
                        fight.Add($"{enemy.Name} (выносливость {(double)enemy.Endurance / 10})");
                    else
                        fight.Add($"{enemy.Name} (самочувствие {enemy.Health})");

                    int protagonistRoll = Game.Dice.Roll();
                    int protagonistHitStrength = protagonistRoll + Character.Protagonist.Strength;
                    string strength = part1 ? "Сила" : "Охрененность";
                    string penalty = String.Empty;

                    if (armour)
                    {
                        protagonistHitStrength -= 1;
                        penalty = " - 1 за полушубок";
                    }

                    fight.Add($"{strength} твоего удара: {Game.Dice.Symbol(protagonistRoll)} + " +
                        $"{Character.Protagonist.Strength}{penalty} = {protagonistHitStrength}");

                    int enemyRoll = Game.Dice.Roll();
                    int enemyHitStrength = enemyRoll + enemy.Strength;
                    string bonus = String.Empty;

                    if (StrengthBonus)
                    {
                        enemyHitStrength += 2;
                        bonus = " + 2 за неуравновешенность";
                    }

                    fight.Add($"{strength} противника: " +
                        $"{Game.Dice.Symbol(enemyRoll)} + {enemy.Strength}{bonus} = {enemyHitStrength}");

                    if (protagonistHitStrength > enemyHitStrength)
                    {
                        fight.Add($"GOOD|BOLD|{enemy.Name} {Hit()}");

                        if (part1)
                            fight.Add($"Противник теряет {(double)Character.Protagonist.Damage / 10} ед. Выносливости");
                        else
                            fight.Add($"Противник теряет {Character.Protagonist.Damage} ед. Самочувствия");

                        enemy.SetHealth(part1, enemy.GetHealth(part1) - Character.Protagonist.Damage);

                        if (Game.Option.IsTriggered("чукотский прибор") && !deviceUsed)
                        {
                            enemy.Health -= 1;
                            deviceUsed = true;

                            fight.Add($"GOOD|Плюс к тому, противник теряет 1 ед. Самочувствия " +
                                $"от меткого попадания складного лезвия чукотского прибора!");
                        }

                        if (NoMoreEnemies(FightEnemies))
                        {
                            if (Benefit != null)
                            {
                                Benefit.Do();
                            }
                            else if (BenefitList != null)
                            {
                                foreach (Modification modification in BenefitList)
                                    modification.Do();
                            }

                            fight.Add(String.Empty);
                            fight.Add("BIG|GOOD|Ты ПОБЕДИЛ :)");

                            if (FourRounds && (round <= 4))
                            {
                                Game.Buttons.Disable("Одержал победу за более чем 4 раунда");
                            }
                            else if (FourRounds && (round > 4))
                            {
                                Game.Buttons.Disable("Ты одержал победу за 4 или менее раундов битвы");
                            }

                            return fight;
                        }
                    }
                    else if (protagonistHitStrength < enemyHitStrength)
                    {
                        fight.Add($"BAD|BOLD|{enemy.Name} заехал тебе по морде");

                        int damage = enemy.Damage;
                        string damagePenelty = String.Empty;

                        if (armour)
                        {
                            damage -= 1;
                            damagePenelty = " (урон уменьшен полушубком!)";
                        }

                        if (part1)
                            fight.Add($"Ты теряешь {(double)damage / 10} ед. Выносливости");
                        else
                            fight.Add($"Ты теряешь {damage} ед. Самочувствия{damagePenelty}");

                        Character.Protagonist.SetHealth(part1,
                            Character.Protagonist.GetHealth(part1) - damage);

                        if (Character.Protagonist.GetHealth(part1) <= 0)
                        {
                            fight.Add(String.Empty);
                            fight.Add("BIG|BAD|Ты ПРОИГРАЛ :(");
                            return fight;
                        }
                    }
                    else
                    {
                        fight.Add("BOLD|Ничья в раунде");
                    }

                    fight.Add(String.Empty);
                }

                if (Authority)
                {
                    int dice = Game.Dice.Roll();

                    fight.Add($"Кидаем кубик за ихний наезд: {Game.Dice.Symbol(dice)}");

                    if (dice == 6)
                    {
                        Character.Protagonist.Fortune -= 1;

                        fight.Add("BAD|Они задавили тебя авторитетом :(");
                        fight.Add("BOLD|Фавор снижается на единицу...");
                    }
                    else
                    {
                        fight.Add("BOLD|Их наезды пошли лесом!");
                    }
                }

                round += 1;
            }
        }

        public override bool IsHealingEnabled() =>
            true;

        public override void UseHealing(int healingLevel) =>
            Character.Protagonist.Endurance += 1;
    }
}
