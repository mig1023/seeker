using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.SwampFever
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public string EnemyName { get; set; }
        public string EnemyCombination { get; set; }

        public int Level { get; set; }
        public bool Birds { get; set; }
        public bool DeathTest { get; set; }

        public override List<string> Representer()
        {
            if (Price > 0)
            {
                string creds = Game.Services.CoinsNoun(Price, "кредит", "кредита", "кредитов");
                return new List<string> { $"{Head}, {Price} {creds}" };
            }
            else if (Level > 0)
            {
                return new List<string> { $"Ментальная проверка, уровень {Level}" };
            }
            else if (!String.IsNullOrEmpty(EnemyName))
            {
                return new List<string> { EnemyName };
            }
            else
            {
                return new List<string> { };
            }
        }

        public override List<string> AdditionalStatus() => new List<string>
        {
            $"Ярость: {Constants.GetFuryLevel[Character.Protagonist.Fury]}",
            $"Креды: {Character.Protagonist.Creds}",
            $"Стигон: {Character.Protagonist.Stigon}/6",
            $"Котировка: 1:{Character.Protagonist.Rate}",
        };

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(Character.Protagonist.HarversterDestroyed, out toEndParagraph, out toEndText);

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            bool byPrice = (Price > 0) && (Character.Protagonist.Creds < Price);          
            bool byMembrane = (Type == "SellAcousticMembrane") && (Character.Protagonist.AcousticMembrane <= 0);
            bool byMucus = (Type == "SellLiveMucus") && (Character.Protagonist.LiveMucus <= 0);

            bool byUsed = false;

            if ((Type == "Get") && (Head == "Серенитатин"))
            {
                return (GetProperty(Character.Protagonist, Benefit.Name) > -2) && (Character.Protagonist.Creds > 0);
            }
            else
            {
                bool already = Benefit != null && (GetProperty(Character.Protagonist, Benefit.Name) > 0);
                byUsed = (String.IsNullOrEmpty(EnemyName) && (Benefit != null) && already);
            }

            return !(byPrice || byUsed || byMembrane || byMucus);
        }

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains("!"))
                    {
                        if (Game.Option.IsTriggered(oneOption.Replace("!", String.Empty).Trim()))
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

        public List<string> MentalTest()
        {
            int mentalDice = Game.Dice.Roll();
            int fury = Character.Protagonist.Fury;
            int mentalAndFury = mentalDice + fury;
            int level = Level;
            string furyLine = fury < 0 ? "-" : "+";

            List<string> mentalCheck = new List<string>
            {
                $"Ментальная проверка (по уровню {level}):",
                $"1. Бросок кубика: {Game.Dice.Symbol(mentalDice)}",
                $"2. {furyLine}{Math.Abs(fury)} к броску за уровень Ярости",
            };

            int ord = 3;

            if (Character.Protagonist.Harmonizer > 0)
            {
                level += 1;
                ord += 1;
                mentalCheck.Add($"3. +1 к уровню проверки за Гармонизатор (теперь уровень {level})");
            }

            bool success = level > mentalAndFury;
            string not = success ? String.Empty : "не ";

            mentalCheck.Add($"{ord}. " +
                $"Итого получаем {mentalAndFury}, что " +
                $"{not}меньше {level} уровня проверки");

            mentalCheck.Add(Result(success, "УСПЕХ|НЕУДАЧА"));

            if (DeathTest && !success)
            {
                mentalCheck.Add("\nBOLD|Ваш харвестер подбит и уничтожен :(");
                Character.Protagonist.HarversterDestroyed = true;
            }

            return mentalCheck;
        }

        public List<string> Get()
        {
            if ((Price > 0) && (Character.Protagonist.Creds >= Price))
            {
                Character.Protagonist.Creds -= Price;

                if (Benefit != null)
                    Benefit.Do();
            }

            return new List<string> { "RELOAD" };
        }

        private bool Upgrade(ref List<int> myCombination, ref List<string> myCombinationLine, ref List<string> fight)
        {
            int upgrades = 0;

            bool upgradeInAction = false;

            for (int i = 1; i <= Constants.GetUpgrates.Count; i++)
                upgrades += GetProperty(Character.Protagonist, Details.GetUpgratesValues(i, part: 1));

            if (upgrades == 0)
                return upgradeInAction;

            fight.Add(String.Empty);

            int upgradeDice = Game.Dice.Roll();

            fight.Add($"Кубик проверки апгрейда: {Game.Dice.Symbol(upgradeDice)}");

            for (int i = 1; i <= Constants.GetUpgrates.Count; i++)
            {
                if (GetProperty(Character.Protagonist, Details.GetUpgratesValues(i, part: 1)) == 0)
                    continue;

                bool inAction = upgradeDice == i;
                string good = inAction ? "GOOD|" : String.Empty;
                string action = inAction ? "В ДЕЙСТВИИ!" : "нет";

                fight.Add($"{good}{Details.GetUpgratesValues(i, part: 2)} - {action}");

                if (inAction)
                {
                    myCombination.Add(upgradeDice);
                    myCombinationLine.Add(Game.Dice.Symbol(upgradeDice));
                    upgradeInAction = true;
                }
            }

            fight.Add(String.Empty);

            return upgradeInAction;
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            Dictionary<int, string> rangeType = Constants.GetRangeTypes;

            List<int> myCombination = new List<int>();
            List<string> myCombinationLine = new List<string>();

            int combinationLength = 6 + Character.Protagonist.Fury;

            for (int i = 0; i < combinationLength; i++)
            {
                int dice = Game.Dice.Roll();
                myCombination.Add(dice);
                myCombinationLine.Add(Game.Dice.Symbol(dice));
            }

            string combination = String.Join(String.Empty, myCombinationLine.ToArray());
            fight.Add($"Ваша комбинация: {combination}");

            List<int> enemyCombination = new List<int>();
            List<string> enemyCombinationLine = new List<string>();

            foreach (string dice in EnemyCombination.Split('-'))
            {
                int enemyNumber = int.Parse(dice);
                enemyCombination.Add(enemyNumber);
                enemyCombinationLine.Add(Game.Dice.Symbol(enemyNumber));
            }

            string lineCombination = String.Join(String.Empty, enemyCombinationLine.ToArray());
            fight.Add($"Его комбинация: {lineCombination}");

            if (Upgrade(ref myCombination, ref myCombinationLine, ref fight))
            {
                string newCombination = String.Join(String.Empty, myCombinationLine.ToArray());
                fight.Add($"Теперь ваша комбинация: {newCombination}");
            }

            bool birds = Birds;

            while (true)
            {
                if (myCombination.Contains(1))
                {
                    fight.Add(String.Empty);
                    fight.Add("BOLD|МАНЕВРИРОВАНИЕ");

                    int maneuvers = Details.CountInCombination(myCombination, 1);
                    bool failManeuvers = true;

                    foreach (int dice in new int[] { 6, 5, 4 })
                    {
                        for (int i = 0; i < enemyCombination.Count; i++)
                        {
                            if ((enemyCombination[i] == dice) && (maneuvers > 0))
                            {
                                fight.Add($"Убираем у противника {dice}-ку за ваше маневрирование");
                                enemyCombination[i] = 0;
                                maneuvers -= 1;
                                failManeuvers = false;
                            }
                        }
                    }

                    if (failManeuvers)
                    {
                        fight.Add("Маневрирование ничего не дало противникам");
                    }
                }

                foreach (int range in new int[] { 6, 5, 4 })
                {
                    fight.Add(String.Empty);
                    fight.Add($"BOLD|{rangeType[range]}");

                    int roundResult = 0;

                    if (!myCombination.Contains(range) && !enemyCombination.Contains(range))
                    {
                        fight.Add("Противникам нечего друг другу противопоставить");
                    }
                    else if (myCombination.Contains(range) && !enemyCombination.Contains(range))
                    {
                        roundResult = 1;

                        if (range == 4)
                        {
                            fight.Add("BIG|GOOD|Вы уничтожили противника тараном, оружием героев :)");

                            if (Benefit != null)
                                Benefit.Do();

                            return fight;
                        }
                        else
                        {
                            fight.Add("GOOD|Вы накрыли противника огнём");
                        }
                    }
                    else if (!myCombination.Contains(range) && enemyCombination.Contains(range))
                    {
                        roundResult = -1;

                        if (range == 4)
                        {
                            fight.Add("BIG|BAD|Противник уничтожил вас тараном :(");
                            Character.Protagonist.HarversterDestroyed = true;
                            return fight;
                        }
                        else
                        {
                            fight.Add("BAD|Противник накрыл вас огнём");
                        }
                    }
                    else
                    {
                        fight.Add(range == 4 ? "Взаимные манёвры:" : "Перестрелка:");

                        while (roundResult == 0)
                        {
                            string bonuses = String.Empty;

                            int myDice = Game.Dice.Roll();
                            int myBonus = Details.CountInCombination(myCombination, range);
                            int myAttack = myDice + myBonus;

                            if (myBonus > 0)
                                bonuses = $", +{myBonus} за {range}-ки, итого {myAttack}";

                            fight.Add($"Ваша атака: {Game.Dice.Symbol(myDice)}{bonuses}");

                            bonuses = String.Empty;

                            int enemyDice = Game.Dice.Roll();
                            int enemyBonus = Details.CountInCombination(enemyCombination, range);
                            int enemyAttack = enemyDice + enemyBonus;

                            if (enemyBonus > 0)
                                bonuses = $", +{enemyBonus} за {range}-ки, итого {enemyAttack}";


                            fight.Add($"Атака противника: {Game.Dice.Symbol(enemyDice)}{bonuses}");

                            if ((myAttack > enemyAttack) && (range == 4))
                            {
                                fight.Add("BIG|GOOD|Вы уничтожили противника тараном, оружием героев :)");

                                if (Benefit != null)
                                    Benefit.Do();

                                return fight;
                            }
                            else if ((myAttack < enemyAttack) && (range == 4))
                            {
                                fight.Add("BIG|BAD|Противник уничтожил вас тараном :(");
                                Character.Protagonist.HarversterDestroyed = true;
                                return fight;
                            }
                            else if (myAttack > enemyAttack)
                            {
                                roundResult = 1;
                                fight.Add("GOOD|Вы подавили противника огнём");
                            }
                            else if (myAttack < enemyAttack)
                            {
                                roundResult = -1;
                                fight.Add("BAD|Противник подавил вас огнём");
                            }
                            else
                            {
                                fight.Add("Перестрелка продолжается:");
                            }
                        }
                    }

                    if (roundResult == 1)
                    {
                        string bonuses = String.Empty, penalties = String.Empty;

                        int myDice = Game.Dice.Roll();
                        int myBonus = Details.CountInCombination(myCombination, 3);
                        int myPenalty = Details.CountInCombination(enemyCombination, 2);
                        int enemyEvasion = myDice + myBonus - myPenalty;

                        if (myBonus > 0)
                            bonuses = $", +{myBonus} за ваши 3-ки";

                        if (myPenalty > 0)
                            penalties = $", -{myPenalty} за его 2-ки";

                        fight.Add($"Противник пытется уклониться: " +
                            $"{Game.Dice.Symbol(myDice)}{bonuses}{penalties}, " +
                            $"итого {enemyEvasion} - это " +
                            $"{Game.Services.Сomparison(enemyEvasion, 2)} " +
                            $"порогового значения '2'");

                        if (enemyEvasion > 2)
                        {
                            if (birds)
                            {
                                fight.Add("GOOD|Вы уничтожили одну из птиц");

                                birds = false;

                                foreach (int dice in new int[] { 5, 4, 3 })
                                    enemyCombination.RemoveAt(dice);
                            }
                            else
                            {
                                fight.Add("BIG|GOOD|Вы уничтожили противника :)");

                                if (Benefit != null)
                                    Benefit.Do();

                                return fight;
                            }
                        }
                        else
                            fight.Add("Противник смог уклониться");
                    }
                    else if (roundResult == -1)
                    {
                        string bonuses = String.Empty, penalties = String.Empty;

                        int enemyDice = Game.Dice.Roll();
                        int enemyBonus = Details.CountInCombination(enemyCombination, 3);
                        int enemyPenalty = Details.CountInCombination(myCombination, 2);
                        int myEvasion = enemyDice + enemyBonus - enemyPenalty;

                        if (enemyBonus > 0)
                            bonuses = $", +{enemyBonus} за его 3-ки";

                        if (enemyPenalty > 0)
                            penalties = $", -{enemyPenalty} за ваши 2-ки";

                        fight.Add($"Вы пытется уклониться: " +
                            $"{Game.Dice.Symbol(enemyDice)}{bonuses}{penalties}, " +
                            $"итого {myEvasion} - это " +
                            $"{Game.Services.Сomparison(myEvasion, 2)} " +
                            $"порогового значения '2'");

                        if (myEvasion > 2)
                        {
                            fight.Add("BIG|BAD|Противник уничтожил вас :(");
                            Character.Protagonist.HarversterDestroyed = true;
                            return fight;
                        }
                        else
                            fight.Add("Вы смогли уклониться");
                    }
                }

                fight.Add("BOLD|Бой окончился ничьёй");

                return fight;
            }
        }

        public List<string> SellStigon()
        {
            List<string> accountingReport = new List<string>();

            int soldStigon = Character.Protagonist.Stigon;
            int earnedCreds = 0, sellNum = 0;

            accountingReport.Add($"В вашем грузовом отсеке {Character.Protagonist.Stigon} кубометров стигона");
            accountingReport.Add($"Курс стигона на начало продажи: 1:{Character.Protagonist.Rate}"); 

            while (Character.Protagonist.Stigon > 0)
            {
                sellNum += 1;
                accountingReport.Add(String.Empty);

                Character.Protagonist.Stigon -= 1;
                earnedCreds += Character.Protagonist.Rate;
                accountingReport.Add($"BOLD|Продажа {sellNum} кубометра стигона: +{Character.Protagonist.Rate} кредов");
                accountingReport.Add($"Подытог к зачислению: {earnedCreds} кредов");

                if (Character.Protagonist.Rate > 5)
                {
                    Character.Protagonist.Rate -= 5;
                    accountingReport.Add($"Курс стигона упал до: {Character.Protagonist.Rate} кредов");
                }
                else
                {
                    accountingReport.Add("Курсу стигона уже некуда падать...");
                }
            }

            accountingReport.Add(String.Empty);
            accountingReport.Add("BIG|ИТОГО:");
            accountingReport.Add($"Вы продали: {soldStigon} кубометров стигона");
            accountingReport.Add($"BOLD|GOOD|Вы получили по плавающему курсу: {earnedCreds} кредов");

            Character.Protagonist.Creds += earnedCreds;

            return accountingReport;
        }

        public List<string> VariousPurchases()
        {
            List<string> purchasesReport = new List<string>();

            bool affordable = false, anything = false;
            bool? prevAffordable = null;

           
            if (Character.Protagonist.Creds >= 1500)
            {
                purchasesReport.Add("GOOD|BOLD|ВАМ ДОСТУПНО ВСЁ!! :)\n");
                anything = true;
            }
            else if (Character.Protagonist.Creds < 5)
            {
                purchasesReport.Add("BAD|BOLD|ВАМ НЕ ДОСТУПНО НИЧЕГО!! :(\n");
                anything = true;
            }
                
            foreach (KeyValuePair<string, int> purchase in Constants.GetPurchases.OrderBy(x => x.Value))
            {
                affordable = (purchase.Value <= Character.Protagonist.Creds) && !anything;
                string affLine = (affordable ? "GOOD|BOLD|" : String.Empty);

                if (!anything)
                    Details.PurchasesHeads(ref purchasesReport, affordable, prevAffordable);

                purchasesReport.Add($"{affLine}{purchase.Key} — {purchase.Value} кредов.");

                prevAffordable = affordable;
            }

            return purchasesReport;
        }

        public List<string> SellAcousticMembrane()
        {
            Character.Protagonist.Creds += 100;
            Character.Protagonist.AcousticMembrane -= 1;

            return new List<string> { "RELOAD" };
        }

        public List<string> SellLiveMucus()
        {
            Character.Protagonist.Creds += 100;
            Character.Protagonist.LiveMucus -= 1;

            return new List<string> { "RELOAD" };
        }

        public List<string> TrackPull()
        {
            List<string> pullReport = new List<string>();

            int thrust = 0;

            for (int i = 0; i < 4; i++)
            {
                int pull = Game.Dice.Roll();

                pullReport.Add($"Тяга гусениц: {Game.Dice.Symbol(pull)}");

                thrust += pull;
            }

            pullReport.Add($"Итого, вы развили тягу: {thrust}");
            pullReport.Add(Result(thrust >= 14, "Вы вытащили ялик|Трос оборвался и ялик утонул"));

            return pullReport;
        }

        public List<string> PropellersPull()
        {
            List<string> pullReport = new List<string>();

            int thrust = 0;

            for (int i = 0; i < 4; i++)
            {
                int pull = Game.Dice.Roll();

                if (pull > 2)
                {
                    pullReport.Add($"Тяга гребных винтов: " +
                        $"{Game.Dice.Symbol(pull)}, -2 за винты, итого {pull - 2}");

                    thrust += (pull - 2);
                }
                else
                {
                    pullReport.Add($"Тяга гребных винтов: " +
                        $"{Game.Dice.Symbol(pull)}, +1 бонусный бросок");

                    thrust += pull;
                    i -= 1;
                }

                if (thrust >= 14)
                    break;
            }

            pullReport.Add($"Итого, вы развили тягу: {thrust}");
            pullReport.Add(Result(thrust >= 14, "Вы вытащили ялик|Трос оборвался и ялик утонул"));

            return pullReport;
        }

        public List<string> TugOfWar()
        {
            List<string> warReport = new List<string>();

            int position = 0;
            bool battleCry = false; 

            while ((position > -3) && (position < 3))
            {
                if (position != 0)
                {
                    string positionType = (position > 0 ? "побеждаете" : "проигрываете");
                    warReport.Add($"BOLD|ПОЛОЖЕНИЕ: вы {positionType} на {Math.Abs(position)} шаг");
                }
                else
                {
                    warReport.Add("BOLD|ПОЛОЖЕНИЕ: на исходной точке");
                }

                bool twoStep = false;
                int myСhoice = 0;

                int yatiForce = 10 + (Math.Abs(position) * 2);
                warReport.Add($"Яти тянет: {yatiForce}");

                int erikForce = Game.Dice.Roll();
                warReport.Add($"Эрик тянет: {Game.Dice.Symbol(erikForce)}");

                int jonyForce = Game.Dice.Roll();
                warReport.Add($"Джонни тянет: {Game.Dice.Symbol(jonyForce)}");

                int myForce = Game.Dice.Roll();
                warReport.Add($"Вы тянете: {Game.Dice.Symbol(myForce)}");

                int totalForce = erikForce + jonyForce + myForce;

                if (battleCry)
                {
                    totalForce += 1;
                    warReport.Add($"+1 к тяге за боевой клич на прошлом этапе, " +
                        $"итого тяга: {totalForce}");
                } 
                   
                battleCry = false;

                do
                {
                    myСhoice = Game.Dice.Roll();
                }
                while (myСhoice > 4);

                switch(myСhoice)
                {
                    case 1:
                        warReport.Add("Ваша тактика: «Силовой отход»");
                        twoStep = true;
                        break;

                    case 2:
                        warReport.Add("Ваша тактика: «Боевой клич»");
                        battleCry = true;
                        break;

                    case 3:
                        totalForce += 2;

                        warReport.Add("Ваша тактика: «Резкий рывок»");
                        warReport.Add($"+2 к вашей тяге за рывок, итого тяга: {totalForce}");
                        break;

                    case 4:
                        warReport.Add("Ваша тактика: «Синергия»");
                        
                        if ((myForce == erikForce) || (myForce == jonyForce))
                        {
                            string force = myForce == erikForce ? "Эрика" : "Джонни";
                            string coincidence = erikForce == jonyForce ? 
                                "у всех разом" : $"со значением {force}";

                            warReport.Add($"Значения тяги совпало {coincidence}, " +
                                $"общая тяга умножается вдвое!");

                            totalForce *= 2;
                        }
                        else
                        {
                            warReport.Add("Значения тяги не совпали, общая тяга не изменилась...");
                        }

                        break;
                }

                warReport.Add($"Общая тяга: {totalForce}");

                if (totalForce > yatiForce)
                {
                    string twoLine = twoStep ? " дважды" : String.Empty;
                    warReport.Add($"GOOD|Вы пересилили яти! Он шагнул вперёд{twoLine}!");
                    position += (twoStep ? 2 : 1);
                }
                else if (totalForce < yatiForce)
                {
                    warReport.Add("BAD|Яти вас пересилил! Вы шагнул вперёд!");
                    position -= 1;
                }
                else
                {
                    warReport.Add("BOLD|Ничья на этом этапе.");
                }

                warReport.Add(String.Empty);
            }

            warReport.Add(Result(position > 0, "Вы выиграли|Вы проиграли"));

            return warReport;
        }

        public List<string> Hunt()
        {
            List<string> huntReport = new List<string>();

            int myPosition = 0, targetPosition = 0;
            bool skipStepAfterShot = false;

            while ((myPosition < 18) && (targetPosition < 18))
            {
                targetPosition += 3;
                huntReport.Add($"BOLD|Зверь убежал на клетку {targetPosition}");

                if (skipStepAfterShot)
                {
                    huntReport.Add($"Вы остаётесь на клетке {myPosition}, т.к. стреляли");
                }
                else if (targetPosition <= myPosition)
                {
                    huntReport.Add($"Вы остаётесь на клетке {myPosition}, чтобы подстеречь зверя");
                }
                else
                {
                    int forwarding = Game.Dice.Roll();
                    myPosition += forwarding;

                    huntReport.Add($"Вы догоняете и проезжаете " +
                        $"{Game.Dice.Symbol(forwarding)} до клетки {myPosition}");
                }

                skipStepAfterShot = false;

                int distance = Math.Abs(myPosition - targetPosition);

                if (distance <= 1)
                {
                    string distanceLine = distance == 0 ? "4, 5 или 6" : "5 или 6";
                    huntReport.Add("Зверь рядом и вы принимаете решение стрелять.");
                    huntReport.Add($"Для попадания необходимо выкинуть {distanceLine}");

                    int shot = Game.Dice.Roll();
                    huntReport.Add($"Ваш выстрел: {Game.Dice.Symbol(shot)}");

                    if (((distance == 0) && (shot > 3)) || ((distance > 0) && (shot > 4)))
                    {
                        Character.Protagonist.Stigon += 1;

                        huntReport.Add("BIG|GOOD|Вы подстрелили зверя :)");
                        return huntReport;
                    }
                    else
                    {
                        huntReport.Add("BAD|Вы промахнулись");
                        skipStepAfterShot = true;
                    }
                }

                huntReport.Add(String.Empty);
            }

            huntReport.Add("BIG|BAD|Вы упустили зверя :(");

            return huntReport;
        }

        public List<string> Pursuit()
        {
            List<string> pursuitReport = new List<string>();

            while (true)
            {
                bool reRoll = false;

                Game.Dice.DoubleRoll(out int tumbleweedDirection, out int tumbleweedSpeed);

                pursuitReport.Add($"BOLD|Направление движения куста: " +
                    $"{Game.Dice.Symbol(tumbleweedDirection)}, " +
                    $"скорость: {Game.Dice.Symbol(tumbleweedSpeed)}");

                int myDirection = Game.Dice.Roll();
                int mySpeed = Game.Dice.Roll();

                pursuitReport.Add($"Ваше направление: " +
                    $"{Game.Dice.Symbol(myDirection)}, " +
                    $"скорость: {Game.Dice.Symbol(mySpeed)}");

                if ((myDirection == tumbleweedDirection) && (mySpeed == tumbleweedSpeed))
                    return Details.PursuitWin(pursuitReport);

                if (myDirection == tumbleweedDirection)
                {
                    reRoll = true;

                    mySpeed = Game.Dice.Roll();

                    pursuitReport.Add($"Вы почти настигли куст и меняете скорость: " +
                        $"{Game.Dice.Symbol(mySpeed)}");

                    if (mySpeed == tumbleweedSpeed)
                        return Details.PursuitWin(pursuitReport);
                }
                else if (mySpeed == tumbleweedSpeed)
                {
                    reRoll = true;

                    myDirection = Game.Dice.Roll();
                    pursuitReport.Add($"Вы почти настигли куст и меняете направление: " +
                        $"{Game.Dice.Symbol(myDirection)}");

                    if (myDirection == tumbleweedDirection)
                        return Details.PursuitWin(pursuitReport);
                }

                pursuitReport.Add("BAD|Настигнуть куст не удалось");

                if ((tumbleweedDirection + tumbleweedSpeed) <= (myDirection + mySpeed))
                {
                    pursuitReport.Add("Преследование продолжается");
                }
                else if (reRoll)
                {
                    return Details.PursuitFail(pursuitReport);
                }
                else
                {
                    if (myDirection > mySpeed)
                    {
                        mySpeed = Game.Dice.Roll();
                        pursuitReport.Add($"Вы пытаетесь резко ускориться: " +
                            $"{Game.Dice.Symbol(mySpeed)}");
                    }
                    else
                    {
                        myDirection = Game.Dice.Roll();
                        pursuitReport.Add($"Вы пытаетесь резко сменить курс: " +
                            $"{Game.Dice.Symbol(myDirection)}");
                    }

                    if ((tumbleweedDirection + tumbleweedSpeed) <= (myDirection + mySpeed))
                    {
                        pursuitReport.Add("Преследование продолжается");
                    }
                    else
                    {
                        return Details.PursuitFail(pursuitReport);
                    }
                }

                pursuitReport.Add(String.Empty);
            }
        }

        private static int ThinkAboutMovement(int myPosition, int step, List<int> bombs, ref List<string> cavityReport)
        {
            int myMovementType = 0;

            if (!bombs.Contains(myPosition + 4) && !bombs.Contains(myPosition + 3) && !bombs.Contains(myPosition + 5))
            {
                cavityReport.Add("Думаем: попробуем рвануть на гусеницах");
                myMovementType = 6;
            }
            else if (!bombs.Contains(myPosition + 2) && (!bombs.Contains(myPosition + 1) || !bombs.Contains(myPosition + 3)))
            {
                cavityReport.Add("Думаем: попробуем тихонечко, на гребных винтах");
                myMovementType = 1;
            }
            else if ((step > 2))
            {
                cavityReport.Add("Думаем: опасно, но нужно срочно прорываться, иначе накроет лава!");
                myMovementType = 6;
            }
            else
            {
                cavityReport.Add("Думаем: лучше постоим нафиг");
            }

            if (bombs.Contains(myPosition) && (myMovementType == 0))
            {
                cavityReport.Add("Думаем: сейчас на вас упадёт вулканическая бомба - нужно рвать когти!");
                myMovementType = Game.Dice.Roll();
            }

            return myMovementType;
        }

        public List<string> SulfurCavity()
        {
            List<string> cavityReport = new List<string>();

            int myPosition = 0;

            for (int step = 1; step <= 4; step++)
            {
                cavityReport.Add($"BOLD|Ход № {step}");

                List<int> bombs = new List<int>();

                for (int bomb = 0; bomb < 3; bomb++)
                    bombs.Add(Game.Dice.Roll());

                cavityReport.Add($"Вулканические бомбы бьют по клеткам: " +
                    $"{Game.Dice.Symbol(bombs[0])}, " +
                    $"{Game.Dice.Symbol(bombs[1])} и " +
                    $"{Game.Dice.Symbol(bombs[2])}");

                int myMovementType = ThinkAboutMovement(myPosition, step, bombs, ref cavityReport);
                int myMove = 0;

                if (myMovementType > 3)
                {
                    myMove = Game.Dice.Roll();
                    cavityReport.Add($"Движение на гусеницах, дальность: {Game.Dice.Symbol(myMove)}");
                }
                else if (myMovementType > 0)
                {
                    myMove = Game.Dice.Roll();

                    if (myMove > 2)
                    {
                        cavityReport.Add($"Движение на гребных винтах, " +
                            $"дальность: {Game.Dice.Symbol(myMove)}, " +
                            $"-2 за винты, итого {myMove - 2}");

                        myMove -= 2;
                    }
                    else
                    {
                        int propBonus = Game.Dice.Roll();

                        if (propBonus > 2)
                            propBonus -= 2;

                        cavityReport.Add($"Движение на гребных винтах, " +
                            $"дальность: {Game.Dice.Symbol(myMove)}, " +
                            $"+бонусный бросок: {Game.Dice.Symbol(propBonus)}, " +
                            $"итого {myMove + propBonus}");

                        myMove += propBonus;
                    }
                }

                myPosition += myMove;

                string move = myMovementType == 0 ? "остаётесь" : "останавливаетесь";
                cavityReport.Add($"Вы {move} на клетке {myPosition}");

                if (bombs.Contains(myPosition))
                {
                    cavityReport.Add("BIG|BAD|Вы уничтожены вулканической бомбой :(");
                    return cavityReport;
                }
                    
                if (myPosition > 6)
                {
                    cavityReport.Add("BIG|GOOD|Вы прорвались :)");
                    return cavityReport;
                }

                cavityReport.Add(String.Empty);
            }

            cavityReport.Add("BIG|BAD|Вас накрыло потоком лавы :(");
            return cavityReport;
        }
    }
}
