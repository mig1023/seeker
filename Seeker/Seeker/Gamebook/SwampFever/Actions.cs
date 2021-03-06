﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.SwampFever
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();

        public string EnemyName { get; set; }
        public string EnemyCombination { get; set; }

        public int Level { get; set; }
        public bool Birds { get; set; }

        public override List<string> Representer()
        {
            if (Price > 0)
            {
                string creds = Game.Other.CoinsNoun(Price, "кредит", "кредита", "кредитов");
                return new List<string> { String.Format("{0}, {1} {2}", Text, Price, creds) };
            }

            else if (Level > 0)
                return new List<string> { String.Format("Ментальная проверка, уровень {0}", Level) };

            else if (!String.IsNullOrEmpty(EnemyName))
                return new List<string> { EnemyName };

            else
                return new List<string> { };
        }

        public override List<string> Status() => new List<string>
        {
            String.Format("Шкала ярости: {0}", Character.Protagonist.Fury),
            String.Format("Креды: {0}", Character.Protagonist.Creds),
            String.Format("Стигон: {0}", Character.Protagonist.Stigon),
            String.Format("Котировка: 1:{0}", Character.Protagonist.Rate),
        };

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(Character.Protagonist.Hitpoints, out toEndParagraph, out toEndText);

        public override bool IsButtonEnabled()
        {
            bool disabledByPrice = (Price > 0) && (Character.Protagonist.Creds < Price);
            bool disabledByUsed = (String.IsNullOrEmpty(EnemyName) && (Benefit != null) &&
                ((int)Character.Protagonist.GetType().GetProperty(Benefit.Name).GetValue(Character.Protagonist, null) > 0));

            bool disabledSellingMembrane = (Name == "SellAcousticMembrane") && (Character.Protagonist.AcousticMembrane <= 0);
            bool disabledSellingMucus = (Name == "SellLiveMucus") && (Character.Protagonist.LiveMucus <= 0);

            return !(disabledByPrice || disabledByUsed || disabledSellingMembrane || disabledSellingMucus);
        }

        public override bool CheckOnlyIf(string option)
        {
            foreach (string oneOption in option.Split(','))
            {
                if (oneOption.Contains("!"))
                {
                    if (Game.Data.Triggers.Contains(oneOption.Replace("!", String.Empty).Trim()))
                        return false;
                }
                else if (!Game.Data.Triggers.Contains(oneOption.Trim()))
                    return false;
            }

            return true;
        }

        public List<string> MentalTest()
        {
            int mentalDice = Game.Dice.Roll();
            int fury = Character.Protagonist.Fury;
            int mentalAndFury = mentalDice + fury;
            int level = Level;

            List<string> mentalCheck = new List<string> {
                String.Format("Ментальная проверка (по уровню {0}):", level),
                String.Format("1. Бросок кубика: {0}", Game.Dice.Symbol(mentalDice)),
                String.Format("2. {0}{1} к броску за уровень Ярости", (fury < 0 ? "-" : "+"), Math.Abs(fury)),
            };

            int ord = 3;

            if (Character.Protagonist.Harmonizer > 0)
            {
                level += 1;
                ord += 1;
                mentalCheck.Add(String.Format("3. +1 к уровню проверки за Гармонизатор (теперь уровень {0})", level));
            }

            mentalCheck.Add(String.Format("{0}. Итого получаем {1}, что {2}меньше {3} уровня проверки",
                ord, mentalAndFury, (level > mentalAndFury ? String.Empty : "не "), level));

            mentalCheck.Add(level > mentalAndFury ? "BIG|GOOD|УСПЕХ :)" : "BIG|BAD|НЕУДАЧА :(");

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

            for (int i = 1; i <= Constants.GetUpgrates().Count; i++)
                upgrades += (int)Character.Protagonist.GetType().GetProperty(Constants.GetUpgrates()[i]["name"]).GetValue(Character.Protagonist, null);

            if (upgrades == 0)
                return upgradeInAction;

            fight.Add(String.Empty);

            int upgradeDice = Game.Dice.Roll();

            fight.Add(String.Format("Кубик проверки апгрейда: {0}", Game.Dice.Symbol(upgradeDice)));

            for (int i = 1; i <= Constants.GetUpgrates().Count; i++)
            {
                if ((int)Character.Protagonist.GetType().GetProperty(Constants.GetUpgrates()[i]["name"]).GetValue(Character.Protagonist, null) == 0)
                    continue;

                bool inAction = (upgradeDice == i);

                fight.Add(String.Format("{0}{1} - {2}",
                    (inAction ? "GOOD|" : String.Empty), Constants.GetUpgrates()[i]["output"], (inAction ? "В ДЕЙСТВИИ!" : "нет")));

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

            Dictionary<int, string> rangeType = Constants.GetRangeTypes();

            List<int> myCombination = new List<int>();
            List<string> myCombinationLine = new List<string>();

            int combinationLength = 6 + Character.Protagonist.Fury;

            for (int i = 0; i < combinationLength; i++)
            {
                int dice = Game.Dice.Roll();
                myCombination.Add(dice);
                myCombinationLine.Add(Game.Dice.Symbol(dice));
            }

            fight.Add(String.Format("Ваша комбинация: {0}", String.Join(String.Empty, myCombinationLine.ToArray())));

            List<int> enemyCombination = new List<int>();
            List<string> enemyCombinationLine = new List<string>();

            foreach (string dice in EnemyCombination.Split('-'))
                enemyCombinationLine.Add(Game.Dice.Symbol(int.Parse(dice)));

            fight.Add(String.Format("Его комбинация: {0}", String.Join(String.Empty, enemyCombinationLine.ToArray())));

            if (Upgrade(ref myCombination, ref myCombinationLine, ref fight))
                fight.Add(String.Format("Теперь ваша комбинация: {0}", String.Join(String.Empty, myCombinationLine.ToArray())));

            bool birds = Birds;

            while (true)
            {
                if (myCombination.Contains(1))
                {
                    fight.Add(String.Empty);
                    fight.Add("BOLD|МАНЕВРИРОВАНИЕ");

                    int maneuvers = CountInCombination(myCombination, 1);
                    bool failManeuvers = true;

                    foreach (int dice in new int[] { 6, 5, 4 })
                        for (int i = 0; i < enemyCombination.Count; i++)
                            if ((enemyCombination[i] == dice) && (maneuvers > 0))
                            {
                                fight.Add(String.Format("Убираем у противника {0}-ку за ваше маневрирование", dice));
                                enemyCombination[i] = 0;
                                maneuvers -= 1;
                                failManeuvers = false;
                            }

                    if (failManeuvers)
                        fight.Add("Маневрирование ничего не дало противникам");
                }

                foreach (int range in new int[] { 6, 5, 4 })
                {
                    fight.Add(String.Empty);
                    fight.Add(String.Format("BOLD|{0}", rangeType[range]));

                    int roundResult = 0;

                    if (!myCombination.Contains(range) && !enemyCombination.Contains(range))
                        fight.Add("Противникам нечего друг другу противопоставить");

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
                            fight.Add("GOOD|Вы накрыли противника огнём");
                    }
                    else if (!myCombination.Contains(range) && enemyCombination.Contains(range))
                    {
                        roundResult = -1;

                        if (range == 4)
                        {
                            fight.Add("BIG|BAD|Противник уничтожил вас тараном :(");
                            Character.Protagonist.Hitpoints = 0;
                            return fight;
                        }
                        else
                            fight.Add("BAD|Противник накрыл вас огнём");
                    }
                    else
                    {
                        fight.Add(range == 4 ? "Взаимные манёвры:" : "Перестрелка:");

                        while (roundResult == 0)
                        {
                            string bonuses = String.Empty;

                            int myDice = Game.Dice.Roll();
                            int myBonus = CountInCombination(myCombination, range);
                            int myAttack = myDice + myBonus;

                            if (myBonus > 0)
                                bonuses = String.Format(", +{0} за {1}-ки, итого {2}", myBonus, range, myAttack);

                            fight.Add(String.Format("Ваша атака: {0}{1}", Game.Dice.Symbol(myDice), bonuses));

                            bonuses = String.Empty;

                            int enemyDice = Game.Dice.Roll();
                            int enemyBonus = CountInCombination(enemyCombination, range);
                            int enemyAttack = enemyDice + enemyBonus;

                            if (enemyBonus > 0)
                                bonuses = String.Format(", +{0} за {1}-ки, итого {2}", enemyBonus, range, enemyAttack);


                            fight.Add(String.Format("Атака противника: {0}{1}", Game.Dice.Symbol(enemyDice), bonuses));

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
                                Character.Protagonist.Hitpoints = 0;
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
                                fight.Add("Перестрелка продолжается:");
                        }
                    }

                    if (roundResult == 1)
                    {
                        string bonuses = String.Empty, penalties = String.Empty;

                        int myDice = Game.Dice.Roll();
                        int myBonus = CountInCombination(myCombination, 3);
                        int myPenalty = CountInCombination(enemyCombination, 2);
                        int enemyEvasion = myDice + myBonus - myPenalty;

                        if (myBonus > 0)
                            bonuses = String.Format(", +{0} за ваши 3-ки", myBonus);

                        if (myPenalty > 0)
                            penalties = String.Format(", -{0} за его 2-ки", myPenalty);

                        fight.Add(String.Format("Противник пытется уклониться: {0}{1}{2}, итого {3} - это {4} порогового значения '2'",
                            Game.Dice.Symbol(myDice), bonuses, penalties, enemyEvasion, Game.Other.Сomparison(enemyEvasion, 2)));

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
                        int enemyBonus = CountInCombination(enemyCombination, 3);
                        int enemyPenalty = CountInCombination(myCombination, 2);
                        int myEvasion = enemyDice + enemyBonus - enemyPenalty;

                        if (enemyBonus > 0)
                            bonuses = String.Format(", +{0} за его 3-ки", enemyBonus);

                        if (enemyPenalty > 0)
                            penalties = String.Format(", -{0} за ваши 2-ки", enemyPenalty);

                        fight.Add(String.Format("Вы пытется уклониться: {0}{1}{2}, итого {3} - это {4} порогового значения '2'",
                            Game.Dice.Symbol(enemyDice), bonuses, penalties, myEvasion, Game.Other.Сomparison(myEvasion, 2)));

                        if (myEvasion > 2)
                        {
                            fight.Add("BIG|BAD|Противник уничтожил вас :(");
                            Character.Protagonist.Hitpoints = 0;
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

        private int CountInCombination(List<int> combination, int dice)
        {
            Dictionary<int, int> counts = combination.GroupBy(x => x).ToDictionary(k => k.Key, v => v.Count());

            return (counts.ContainsKey(dice) ? counts[dice] : 0);
        }

        public List<string> SellStigon()
        {
            List<string> accountingReport = new List<string>();

            int soldStigon = Character.Protagonist.Stigon;
            int earnedCreds = 0;

            accountingReport.Add(String.Format("В вашем грузовом отсеке {0} кубометров стигона", Character.Protagonist.Stigon));
            accountingReport.Add(String.Format("Курс стигона на начало продажи: 1:{0}", Character.Protagonist.Rate)); 

            while (Character.Protagonist.Stigon > 0)
            {
                accountingReport.Add(String.Empty);

                Character.Protagonist.Stigon -= 1;
                earnedCreds += Character.Protagonist.Rate;
                accountingReport.Add(String.Format("Продажа кубометра стигона: +{0} кредов", Character.Protagonist.Rate));
                accountingReport.Add(String.Format("GOOD|Итого к зачислению: {0} кредов", earnedCreds));

                if (Character.Protagonist.Rate > 5)
                {
                    Character.Protagonist.Rate -= 5;
                    accountingReport.Add(String.Format("Курс стигона упал до: {0} кредов", Character.Protagonist.Rate));
                }
                else
                    accountingReport.Add("Курсу стигона уже некуда падать...");
            }

            accountingReport.Add(String.Empty);
            accountingReport.Add("BIG|ИТОГО:");
            accountingReport.Add(String.Format("Вы продали: {0} кубометров стигона", soldStigon));
            accountingReport.Add(String.Format("GOOD|Вы получили по плавающему курсу: {0} кредов", earnedCreds));

            Character.Protagonist.Creds += earnedCreds;

            return accountingReport;
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

                pullReport.Add(String.Format("Тяга гусениц: {0}", Game.Dice.Symbol(pull)));

                thrust += pull;
            }

            pullReport.Add(String.Format("Итого, вы развили тягу: {0}", thrust));
            pullReport.Add(thrust >= 14 ? "BIG|GOOD|Вы вытащили ялик :)" : "BIG|BAD|Трос оборвался и ялик утонул :(");

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
                    pullReport.Add(String.Format("Тяга гребных винтов: {0}, -2 за винты, итого {1}", Game.Dice.Symbol(pull), (pull - 2)));
                    thrust += (pull - 2);
                }
                else
                {
                    pullReport.Add(String.Format("Тяга гребных винтов: {0}, +1 бонусный бросок", Game.Dice.Symbol(pull)));
                    thrust += pull;
                    i -= 1;
                }

                if (thrust >= 14)
                    break;
            }

            pullReport.Add(String.Format("Итого, вы развили тягу: {0}", thrust));
            pullReport.Add(thrust >= 14 ? "BIG|GOOD|Вы вытащили ялик :)" : "BIG|BAD|Трос оборвался и ялик утонул :(");

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
                    warReport.Add(String.Format("BOLD|ПОЛОЖЕНИЕ: вы {0} на {1} шаг", positionType, Math.Abs(position)));
                }
                else
                    warReport.Add("BOLD|ПОЛОЖЕНИЕ: на исходной точке");


                bool twoStep = false;
                int myСhoice = 0;

                int yatiForce = 10 + (Math.Abs(position) * 2);
                warReport.Add(String.Format("Яти тянет: {0}", yatiForce));

                int erikForce = Game.Dice.Roll();
                warReport.Add(String.Format("Эрик тянет: {0}", Game.Dice.Symbol(erikForce)));

                int jonyForce = Game.Dice.Roll();
                warReport.Add(String.Format("Джонни тянет: {0}", Game.Dice.Symbol(jonyForce)));

                int myForce = Game.Dice.Roll();
                warReport.Add(String.Format("Вы тянете: {0}", Game.Dice.Symbol(myForce)));

                int totalForce = erikForce + jonyForce + jonyForce;

                if (battleCry)
                {
                    totalForce += 1;
                    warReport.Add(String.Format("+1 к тяге за боевой клич на прошлом этапе, итого тяга: {0}", totalForce));
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
                        myForce += 2;

                        warReport.Add("Ваша тактика: «Резкий рывок»");
                        warReport.Add(String.Format("+2 к вашей тяге за рывок, итого тяга: {0}", totalForce));
                        break;

                    case 4:
                        warReport.Add("Ваша тактика: «Синергия»");
                        
                        if ((myForce == erikForce) || (myForce == jonyForce))
                        {
                            string coincidence = (erikForce == jonyForce ? "у всех разом" : "со значением " + (myForce == erikForce ? "Эрика" : "Джонни"));
                            warReport.Add(String.Format("Значения тяги совпало {0}, общая тяга умножается вдвое!", coincidence));

                            totalForce *= 2;
                        }
                        else
                            warReport.Add("Значения тяги не совпали, общая тяга не изменилась...");

                        break;
                }

                warReport.Add(String.Format("Общая тяга: {0}", totalForce));

                if (totalForce > yatiForce)
                {
                    warReport.Add(String.Format("GOOD|Вы пересилили яти! Он шагнул вперёд{0}!", twoStep ? " дважды" : String.Empty));
                    position += (twoStep ? 2 : 1);
                }
                else if (totalForce < yatiForce)
                {
                    warReport.Add("BAD|Яти вас пересилил! Вы шагнул вперёд!");
                    position -= 1;
                }
                else
                    warReport.Add("BOLD|Ничья на этом этапе.");

                warReport.Add(String.Empty);
            }

            warReport.Add(position > 0 ? "BIG|GOOD|Вы выиграли :)" : "BIG|BAD|Вы проиграли :(");

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
                huntReport.Add(String.Format("BOLD|Зверь убежал на клетку {0}", targetPosition));

                if (skipStepAfterShot)
                    huntReport.Add(String.Format("Вы остаётесь на клетке {0}, т.к. стреляли", myPosition));

                else if (targetPosition <= myPosition)
                    huntReport.Add(String.Format("Вы остаётесь на клетке {0}, чтобы подстеречь зверя", myPosition));

                else
                {
                    int forwarding = Game.Dice.Roll();
                    myPosition += forwarding;

                    huntReport.Add(String.Format("Вы догоняете и проезжаете {0} до клетки {1}", Game.Dice.Symbol(forwarding), myPosition));
                }

                skipStepAfterShot = false;

                int distance = Math.Abs(myPosition - targetPosition);

                if (distance <= 1)
                {
                    huntReport.Add("Зверь рядом и вы принимаете решение стрелять.");
                    huntReport.Add(String.Format("Для попадания необходимо выкинуть {0}", (distance == 0 ? "4, 5 или 6" : "5 или 6")));

                    int shot = Game.Dice.Roll();
                    huntReport.Add(String.Format("Ваш выстрел: {0}", Game.Dice.Symbol(shot)));

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

        private List<string> PursuitWin(List<string> pursuitReport)
        {
            Character.Protagonist.Stigon += 1;

            pursuitReport.Add("BIG|GOOD|Вы настигли шар :)");
            return pursuitReport;
        }

        private List<string> PursuitFail(List<string> pursuitReport)
        {
            Character.Protagonist.Fury += 1;

            pursuitReport.Add("BIG|BAD|Вы упустили куст :(");
            return pursuitReport;
        }

        public List<string> Pursuit()
        {
            List<string> pursuitReport = new List<string>();

            while (true)
            {
                bool reRoll = false;

                int tumbleweedDirection = Game.Dice.Roll();
                int tumbleweedSpeed = Game.Dice.Roll();

                pursuitReport.Add(String.Format("BOLD|Направление движения куста: {0}, скорость: {1}",
                    Game.Dice.Symbol(tumbleweedDirection), Game.Dice.Symbol(tumbleweedSpeed)));

                int myDirection = Game.Dice.Roll();
                int mySpeed = Game.Dice.Roll();

                pursuitReport.Add(String.Format("Ваше направление: {0}, скорость: {1}",
                    Game.Dice.Symbol(myDirection), Game.Dice.Symbol(mySpeed)));

                if ((myDirection == tumbleweedDirection) && (mySpeed == tumbleweedSpeed))
                    return PursuitWin(pursuitReport);

                if (myDirection == tumbleweedDirection)
                {
                    reRoll = true;

                    mySpeed = Game.Dice.Roll();
                    pursuitReport.Add(String.Format("Вы почти настигли куст и меняете скорость: {0}", Game.Dice.Symbol(mySpeed)));

                    if (mySpeed == tumbleweedSpeed)
                        return PursuitWin(pursuitReport);
                }
                else if (mySpeed == tumbleweedSpeed)
                {
                    reRoll = true;

                    myDirection = Game.Dice.Roll();
                    pursuitReport.Add(String.Format("Вы почти настигли куст и меняете направление: {0}", Game.Dice.Symbol(myDirection)));

                    if (myDirection == tumbleweedDirection)
                        return PursuitWin(pursuitReport);
                }

                pursuitReport.Add("BAD|Настигнуть куст не удалось");

                if ((tumbleweedDirection + tumbleweedSpeed) <= (myDirection + mySpeed))
                    pursuitReport.Add("Преследование продолжается");

                else if (reRoll)
                    return PursuitFail(pursuitReport);

                else
                {
                    if (myDirection > mySpeed)
                    {
                        mySpeed = Game.Dice.Roll();
                        pursuitReport.Add(String.Format("Вы пытаетесь резко ускориться: {0}", Game.Dice.Symbol(mySpeed)));
                    }
                    else
                    {
                        myDirection = Game.Dice.Roll();
                        pursuitReport.Add(String.Format("Вы пытаетесь резко сменить курс: {0}", Game.Dice.Symbol(myDirection)));
                    }

                    if ((tumbleweedDirection + tumbleweedSpeed) <= (myDirection + mySpeed))
                        pursuitReport.Add("Преследование продолжается");
                    else
                        return PursuitFail(pursuitReport);
                }

                pursuitReport.Add(String.Empty);
            }
        }

        private int ThinkAboutMovement(int myPosition, int step, List<int> bombs, ref List<string> cavityReport)
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
                cavityReport.Add("Думаем: лучше постоим нафиг");

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
                cavityReport.Add(String.Format("BOLD|Ход № {0}", step));

                List<int> bombs = new List<int>();

                for (int bomb = 0; bomb < 3; bomb++)
                    bombs.Add(Game.Dice.Roll());

                cavityReport.Add(String.Format("Вулканические бомбы бьют по клеткам: {0}, {1} и {2}",
                    Game.Dice.Symbol(bombs[0]), Game.Dice.Symbol(bombs[1]), Game.Dice.Symbol(bombs[2])));

                int myMovementType = ThinkAboutMovement(myPosition, step, bombs, ref cavityReport);
                int myMove = 0;

                if (myMovementType > 3)
                {
                    myMove = Game.Dice.Roll();
                    cavityReport.Add(String.Format("Движение на гусеницах, дальность: {0}", Game.Dice.Symbol(myMove)));
                }
                else if (myMovementType > 0)
                {
                    myMove = Game.Dice.Roll();

                    if (myMove > 2)
                    {
                        cavityReport.Add(String.Format("Движение на гребных винтах, дальность: {0}, -2 за винты, итого {1}",
                            Game.Dice.Symbol(myMove), (myMove - 2)));

                        myMove -= 2;
                    }
                    else
                    {
                        int propBonus = Game.Dice.Roll();

                        if (propBonus > 2)
                            propBonus -= 2;

                        cavityReport.Add(String.Format("Движение на гребных винтах, дальность: {0}, +бонусный бросок: {1}, итого {2}",
                            Game.Dice.Symbol(myMove), Game.Dice.Symbol(propBonus), (myMove + propBonus)));

                        myMove += propBonus;
                    }
                }

                myPosition += myMove;
                cavityReport.Add(String.Format("Вы {0} на клетке {1}", (myMovementType == 0 ? "остаётесь" : "останавливаетесь"), myPosition));

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
