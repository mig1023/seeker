using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.SwampFever
{
    class Actions : Interfaces.IActions
    {
        public string ActionName { get; set; }
        public string ButtonName { get; set; }
        public string Text { get; set; }
        public string Aftertext { get; set; }
        public string Trigger { get; set; }

        public string EnemyName { get; set; }
        public string EnemyCombination { get; set; }

        public int Level { get; set; }
        public int Price { get; set; }
        public Modification Benefit { get; set; }


        public List<string> Do(out bool reload, string action = "", bool trigger = false)
        {
            if (trigger)
                Game.Option.Trigger(Trigger);

            string actionName = (String.IsNullOrEmpty(action) ? ActionName : action);
            List<string> actionResult = typeof(Actions).GetMethod(actionName).Invoke(this, new object[] { }) as List<string>;

            reload = ((actionResult.Count >= 1) && (actionResult[0] == "RELOAD") ? true : false);

            return actionResult;
        }

        public List<string> Representer()
        {
            if (Price > 0)
                return new List<string> { Text };
            else if (Level > 0)
                return new List<string> { String.Format("Ментальная проверка, уровень {0}", Level) };
            else if (!String.IsNullOrEmpty(EnemyName))
                return new List<string> { EnemyName };
            else
                return new List<string> { };
        }

        public List<string> Status()
        {
            List<string> statusLines = new List<string>
            {
                String.Format("Шкала ярости: {0}", Character.Protagonist.Fury),
                String.Format("Креды: {0}", Character.Protagonist.Creds),
                String.Format("Стигон: {0}", Character.Protagonist.Stigon),
                String.Format("Котировка: 1:{0}", Character.Protagonist.Rate),
            };

            return statusLines;
        }

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = "Начать с начала...";

            return (Character.Protagonist.Hitpoints > 0 ? false : true);
        }

        public bool IsButtonEnabled()
        {
            bool disabledByPrice = (Price > 0) && (Character.Protagonist.Creds < Price);
            bool disabledByUsed = (String.IsNullOrEmpty(EnemyName) && (Benefit != null) &&
                ((int)Character.Protagonist.GetType().GetProperty(Benefit.Name).GetValue(Character.Protagonist, null) > 0)
            );

            return (disabledByPrice || disabledByUsed ? false : true);
        }

        public static bool CheckOnlyIf(string option)
        {
            string[] options = option.Split(',');

            foreach (string oneOption in options)
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
                String.Format("1. Бросок кубика: {0} ⚄", mentalDice),
                String.Format("2. {0}{1} к броску за уровень Ярости", (fury < 0 ? "-" : "+"), Math.Abs(fury)),
            };

            int ord = 3;

            if (Character.Protagonist.Harmonizer > 0)
            {
                level += 1;
                ord += 1;
                mentalCheck.Add(String.Format("3. +1 к уровню проверки за Гармонизатор (теперь уровень {0})", level));
            }

            mentalCheck.Add(String.Format(
                "{0}. Итого получаем {1}, что {2}меньше {3} уровня проверки",
                ord, mentalAndFury, (level > mentalAndFury ? String.Empty : "не "), level
            ));

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

        private bool Upgrade(ref List<int> myCombination, ref List<string> fight)
        {
            int upgrades = 0;

            bool upgradeInAction = false;

            for (int i = 1; i <= Constants.GetUpgrates().Count; i++)
            {
                string tmp = Constants.GetUpgrates()[i]["name"];
                upgrades += (int)Character.Protagonist.GetType().GetProperty(Constants.GetUpgrates()[i]["name"]).GetValue(Character.Protagonist, null);
            }

            if (upgrades == 0)
                return upgradeInAction;

            fight.Add(String.Empty);

            int upgradeDice = Game.Dice.Roll();

            fight.Add(String.Format("Кубик проверки апгрейда: {0} ⚄", upgradeDice));

            for (int i = 1; i <= Constants.GetUpgrates().Count; i++)
            {
                if ((int)Character.Protagonist.GetType().GetProperty(Constants.GetUpgrates()[i]["name"]).GetValue(Character.Protagonist, null) == 0)
                    continue;

                bool inAction = (upgradeDice == i);

                fight.Add(String.Format(
                    "{0}{1} - {2}", (inAction ? "GOOD|" : String.Empty), Constants.GetUpgrates()[i]["output"], (inAction ? "В ДЕЙСТВИИ!" : "нет")
                ));

                if (inAction)
                {
                    myCombination.Add(upgradeDice);
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

            int combinationLength = 6 + Character.Protagonist.Fury;

            for (int i = 0; i < combinationLength; i++)
                myCombination.Add(Game.Dice.Roll());

            fight.Add(String.Format("Ваша комбинация: {0} ⚄", String.Join(" ⚄ - ", myCombination.ToArray())));

            List<int> enemyCombination = new List<int>();

            foreach (string dice in EnemyCombination.Split('-'))
                enemyCombination.Add(int.Parse(dice));

            fight.Add(String.Format("Его комбинация: {0}", String.Join(" - ", enemyCombination.ToArray())));

            if (Upgrade(ref myCombination, ref fight))
                fight.Add(String.Format("Теперь ваша комбинация: {0} ⚄", String.Join(" ⚄ - ", myCombination.ToArray())));

            while (true)
            {
                if (myCombination.Contains(1))
                {
                    fight.Add(String.Empty);
                    fight.Add("BOLD|МАНЕВРИРОВАНИЕ");

                    int maneuvers = CountInCombination(myCombination, 1);

                    foreach (int dice in new int[] { 6, 5, 4 })
                        for (int i = 0; i < enemyCombination.Count; i++)
                            if ((enemyCombination[i] == dice) && (maneuvers > 0))
                            {
                                fight.Add(String.Format("Убираем у противника {0}-ку за ваше маневрирование", dice));
                                enemyCombination[i] = 0;
                                maneuvers -= 1;
                            }
                }

                foreach (int range in new int[] { 6, 5, 4 })
                {
                    fight.Add(String.Empty);
                    fight.Add(String.Format("BOLD|{0}", rangeType[range]));

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
                            int myDice = Game.Dice.Roll();
                            int myBonus = CountInCombination(myCombination, range);
                            int myAttack = myDice + myBonus;
                            fight.Add(String.Format("Ваша атака: {0} ⚄, +{1} за {2}-ки, итого {3}", myDice, myBonus, range, myAttack));

                            int enemyDice = Game.Dice.Roll();
                            int enemyBonus = CountInCombination(enemyCombination, range);
                            int enemyAttack = enemyDice + enemyBonus;
                            fight.Add(String.Format("Атака противника: {0} ⚄, +{1} за {2}-ки, итого {3}", enemyDice, enemyBonus, range, enemyAttack));

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
                        int myDice = Game.Dice.Roll();
                        int myBonus = CountInCombination(myCombination, 3);
                        int myPenalty = CountInCombination(enemyCombination, 2);
                        int enemyEvasion = myDice + myBonus - myPenalty;
                        fight.Add(String.Format(
                            "Противник пытется уклониться: {0} ⚄, +{1} за ваши 3-ки, -{2} за его 2-ки, итого {3} - {4} порогового значения в 2",
                            myDice, myBonus, myPenalty, enemyEvasion, (enemyEvasion > 2 ? "больше" : "меньше или равно")
                        ));

                        if (enemyEvasion > 2)
                        {
                            fight.Add("BIG|GOOD|Вы уничтожили противника :)");

                            if (Benefit != null)
                                Benefit.Do();

                            return fight;
                        }
                        else
                            fight.Add("Противник смог уклониться");
                    }
                    else if (roundResult == -1)
                    {
                        int enemyDice = Game.Dice.Roll();
                        int enemyBonus = CountInCombination(enemyCombination, 3);
                        int enemyPenalty = CountInCombination(myCombination, 2);
                        int myEvasion = enemyDice + enemyBonus - enemyPenalty;
                        fight.Add(String.Format(
                            "Вы пытется уклониться: {0} ⚄, +{1} за его 3ки, -{2} за ваши 2-ки, итого {3} - {4} порогового значения 2",
                            enemyDice, enemyBonus, enemyPenalty, myEvasion, (myEvasion > 2 ? "больше" : "меньше или равно")
                        ));

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

                Character.Protagonist.Rate -= 5;
                accountingReport.Add(String.Format("Курс стигона упал до: {0} кредов", Character.Protagonist.Rate));
            }

            accountingReport.Add(String.Empty);
            accountingReport.Add("BIG|ИТОГО:");
            accountingReport.Add(String.Format("Вы продали: {0} кубометров стигона", soldStigon));
            accountingReport.Add(String.Format("GOOD|Вы получили по плавающему курсу: {0} кредов", earnedCreds));

            Character.Protagonist.Creds += earnedCreds;

            return accountingReport;
        }

        public List<string> ContinuousTrackPull()
        {
            List<string> pullReport = new List<string>();

            int thrust = 0;

            for (int i = 0; i < 4; i++)
            {
                int pull = Game.Dice.Roll();

                pullReport.Add(String.Format("Тяга гусениц: {0} ⚄", pull));

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
                    pullReport.Add(String.Format("Тяга гребных винтов: {0} ⚄, -2 за винты, итого {1}", pull, (pull - 2)));

                    thrust += (pull - 2);
                }
                else
                {
                    pullReport.Add(String.Format("Тяга гребных винтов: {0} ⚄, +1 бонусный бросок", pull));

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
    }
}
