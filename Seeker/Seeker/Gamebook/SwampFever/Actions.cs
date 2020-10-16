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
        public string Aftertext { get; set; }
        public string Trigger { get; set; }

        public string EnemyName { get; set; }
        public string EnemyCombination { get; set; }


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
            return new List<string> { EnemyName };
        }

        public List<string> Status()
        {
            List<string> statusLines = new List<string> { String.Empty };

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
            return true;
        }

        public static bool CheckOnlyIf(string option)
        {
            return Game.Data.Triggers.Contains(option.Trim());
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            List<int> myCombination = new List<int>();

            for (int i = 0; i < 6; i++)
                myCombination.Add(Game.Dice.Roll());

            fight.Add(String.Format("Ваша комбинация: {0}",String.Join("-", myCombination.ToArray())));

            List<int> enemyCombination = new List<int>();

            foreach (string dice in EnemyCombination.Split('-'))
                enemyCombination.Add(int.Parse(dice));

            fight.Add(String.Format("Его комбинация: {0}", String.Join("-", enemyCombination.ToArray())));

            while (true)
            {
                if (myCombination.Contains(1))
                {
                    fight.Add("HEAD|МАНЕВРИРОВАНИЕ");

                    int maneuvers = CountInCombination(myCombination, 1);

                    foreach (int dice in new int[] { 6, 5, 4 })
                        for (int i = 0; i < enemyCombination.Count; i++)
                            if ((enemyCombination[i] == dice) && (maneuvers > 0))
                            {
                                fight.Add(String.Format("Убираем у противника {0}ку", dice));
                                enemyCombination[i] = 0;
                                maneuvers -= 1;
                            }
                }

                Dictionary<int, string> rangeType = new Dictionary<int, string>
                {
                    [6] = "ДАЛЬНЯЯ ДИСТАНЦИЯ",
                    [5] = "СРЕДНЯЯ ДИСТАНЦИЯ",
                    [4] = "БЛИЖНЯЯ ДИСТАНЦИЯ",
                };

                foreach (int range in new int[] { 6, 5, 4 })
                {
                    fight.Add(String.Format("HEAD|{0}", rangeType[range]));

                    int roundResult = 0;

                    if (myCombination.Contains(6) && !enemyCombination.Contains(6))
                    {
                        roundResult = 1;
                        fight.Add(range == 4 ? "GOOD|Вы идёте на противника тараном" : "GOOD|Вы накрыли противника огнём");
                    }
                    else if (!myCombination.Contains(6) && enemyCombination.Contains(6))
                    {
                        roundResult = -1;
                        fight.Add(range == 4 ? "BAD|Противник идёт на вас тараном" : "BAD|Противник накрыл вас огнём");
                    }
                    else
                    {
                        fight.Add(range == 4 ? "Манёвры:" : "Перестрелка:");

                        while (roundResult == 0)
                        {
                            int myDice = Game.Dice.Roll();
                            int myBonus = CountInCombination(myCombination, range);
                            int myAttack = myDice + myBonus;
                            fight.Add(String.Format("Ваша атака: {0} + {1} за {2}ки, итого {3}", myDice, myBonus, range, myAttack));

                            int enemyDice = Game.Dice.Roll();
                            int enemyBonus = CountInCombination(enemyCombination, range);
                            int enemyAttack = enemyDice + enemyBonus;
                            fight.Add(String.Format("Атака противника: {0} + {1} за {2}ки, итого {3}", enemyDice, enemyBonus, range, enemyAttack));

                            if ((myAttack > enemyAttack) && (range == 4))
                            {
                                fight.Add("BIG|GOOD|Вы уничтожили противника тараном, оружием героев :)");
                                return fight;
                            }
                            else if ((myAttack < enemyAttack) && (range == 4))
                            {
                                fight.Add("BIG|BAD|Противник уничтожил вас тараном :(");
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
                        int myDice = Game.Dice.Roll();
                        int myBonus = CountInCombination(myCombination, 3);
                        int myPenalty = CountInCombination(enemyCombination, 2);
                        int enemyEvasion = myDice + myBonus - myPenalty;
                        fight.Add(String.Format(
                            "Противник пытется уклониться от попадания: {0} +{1} за ваши 3ки -{2} за его 2ки, итого {3} {4} 2",
                            myDice, myBonus, myPenalty, enemyEvasion, (enemyEvasion > 2 ? ">" : "<=")
                        ));

                        if (enemyEvasion > 2)
                        {
                            fight.Add("BIG|GOOD|Вы уничтожили противника :)");
                            return fight;
                        }
                        else
                            fight.Add("Противник смог уклониться");
                    }
                    else if (roundResult == -1)
                    {
                        int enemyDice = Game.Dice.Roll();
                        int enemyBonus = CountInCombination(myCombination, 3);
                        int enemyPenalty = CountInCombination(enemyCombination, 2);
                        int myEvasion = enemyDice + enemyBonus - enemyPenalty;
                        fight.Add(String.Format(
                            "Вы пытется уклониться от попадания: {0} +{1} за ваши 3ки -{2} за его 2ки, итого {3} {4} 2",
                            enemyDice, enemyBonus, enemyPenalty, myEvasion, (myEvasion > 2 ? ">" : "<=")
                        ));

                        if (myEvasion > 2)
                        {
                            fight.Add("BIG|BAD|Противник уничтожил вас :(");
                            return fight;
                        }
                        else
                            fight.Add("Вы смогли уклониться");
                    }
                }

                fight.Add("HEAD|Бой окончился ничьёй");

                return fight;
            }
        }

        private int CountInCombination(List<int> combination, int dice)
        {
            Dictionary<int, int> counts = combination.GroupBy(x => x).ToDictionary(k => k.Key, v => v.Count());

            return (counts.ContainsKey(dice) ? counts[dice] : 0);
        }
    }
}
