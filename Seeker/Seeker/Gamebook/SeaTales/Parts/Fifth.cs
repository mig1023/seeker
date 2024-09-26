using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.SeaTales.Parts
{
    class Fifth : IParts
    {
        public List<string> Status() =>
            new List<string> { $"Героизм: {Character.Protagonist.Heroism}" };

        public List<string> AdditionalStatus() => null;

        public List<string> Representer(Actions action)
        {
            return new List<string> { $"МОРСКОЕ СРАЖЕНИЕ\nпротив {action.Enemy}" };
        }

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            if (Character.Protagonist.Heroism >= 100)
            {
                toEndParagraph = 1484;
                Character.Protagonist.Heroism = 0;

                toEndText = "Это была завечательная история, " +
                    "но она подошла к концу. Тут начинается " +
                    "совсем другое приключение...";

                return true;
            }
            else
            {
                toEndParagraph = 0;
                toEndText = String.Empty;

                return false;
            }
        }

        public List<string> RandomOption() =>
            new List<string>();

        private static int CountInCombination(List<int> combination, int dice)
        {
            Dictionary<int, int> counts = combination
                .GroupBy(x => x)
                .ToDictionary(k => k.Key, v => v.Count());

            return (counts.ContainsKey(dice) ? counts[dice] : 0);
        }

        private static void Success(Actions action, ref List<string> fight)
        {
            List<string> success = action.Success
                .Split(';')
                .Select(x => x.Trim())
                .ToList();

            Character.Protagonist.Heroism += int.Parse(success[0]);

            fight.Add($"BIG|{success[1]}");
        }

        private string ToTxt(int num, bool all = false) =>
            all ? Constants.AllNumTexts[num] : Constants.NumTexts[num];

        public List<string> Test(Actions action)
        {
            List<string> fight = new List<string>();

            Dictionary<int, string> rangeType = Constants.GetBattleTypes;

            List<int> myCombination = new List<int>();
            List<string> myCombinationLine = new List<string>();

            for (int i = 0; i < 6; i++)
            {
                int dice = Game.Dice.Roll();
                myCombination.Add(dice);
                myCombinationLine.Add(Game.Dice.Symbol(dice));
            }

            string combination = String.Join(String.Empty, myCombinationLine.ToArray());
            fight.Add($"Ваша комбинация: {combination}");

            List<int> enemyCombination = new List<int>();
            List<string> enemyCombinationLine = new List<string>();

            foreach (string dice in action.Combination.Split(','))
            {
                int enemyNumber = int.Parse(dice);
                enemyCombination.Add(enemyNumber);
                enemyCombinationLine.Add(Game.Dice.Symbol(enemyNumber));
            }

            string lineCombination = String.Join(String.Empty, enemyCombinationLine.ToArray());
            fight.Add($"Его комбинация: {lineCombination}");

            while (true)
            {
                if (myCombination.Contains(1))
                {
                    fight.Add(String.Empty);
                    fight.Add("BOLD|МАНЕВРИРОВАНИЕ");

                    int maneuvers = CountInCombination(myCombination, 1);
                    bool failManeuvers = true;

                    foreach (int dice in new int[] { 6, 5, 4 })
                    {
                        for (int i = 0; i < enemyCombination.Count; i++)
                        {
                            if ((enemyCombination[i] == dice) && (maneuvers > 0))
                            {
                                fight.Add($"Убираем у противника {ToTxt(dice)} за ваше маневрирование");
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
                            fight.Add("BIG|GOOD|Вы взяли противника на абордаж! :)");

                            Success(action, ref fight);

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
                            fight.Add("BIG|BAD|Противник взял вас на абордаж :(");
                            Character.Protagonist.Heroism = 0;
                            return fight;
                        }
                        else
                        {
                            fight.Add("BAD|Противник накрыл вас огнём");
                        }
                    }
                    else
                    {
                        fight.Add(range == 4 ? "Взаимные манёвры:" : "Стрельба:");

                        while (roundResult == 0)
                        {
                            string bonuses = String.Empty;

                            int myDice = Game.Dice.Roll();
                            int myBonus = CountInCombination(myCombination, range);
                            int myAttack = myDice + myBonus;

                            if (myBonus > 0)
                                bonuses = $", +{myBonus} за {ToTxt(range, all: true)}, итого {myAttack}";

                            fight.Add($"Ваша атака: {Game.Dice.Symbol(myDice)}{bonuses}");

                            bonuses = String.Empty;

                            int enemyDice = Game.Dice.Roll();
                            int enemyBonus = CountInCombination(enemyCombination, range);
                            int enemyAttack = enemyDice + enemyBonus;

                            if (enemyBonus > 0)
                                bonuses = $", +{enemyBonus} за {ToTxt(range, all: true)}, итого {enemyAttack}";


                            fight.Add($"Атака противника: {Game.Dice.Symbol(enemyDice)}{bonuses}");

                            if ((myAttack > enemyAttack) && (range == 4))
                            {
                                fight.Add("BIG|GOOD|Вы взяли противника на абордаж! :)");

                                Success(action, ref fight);

                                return fight;
                            }
                            else if ((myAttack < enemyAttack) && (range == 4))
                            {
                                fight.Add("BIG|BAD|Противник взял вас на абордаж :(");
                                Character.Protagonist.Heroism = 0;
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
                        int myBonus = CountInCombination(myCombination, 3);
                        int myPenalty = CountInCombination(enemyCombination, 2);
                        int enemyEvasion = myDice + myBonus - myPenalty;

                        if (myBonus > 0)
                            bonuses = $", +{myBonus} за ваши тройки";

                        if (myPenalty > 0)
                            penalties = $", -{myPenalty} за его двойки";

                        fight.Add($"Противник пытется уклониться: " +
                            $"{Game.Dice.Symbol(myDice)}{bonuses}{penalties}, " +
                            $"итого {enemyEvasion} - это " +
                            $"{Game.Services.Сomparison(enemyEvasion, 2)} " +
                            $"порогового значения 'два'");

                        if (enemyEvasion > 2)
                        {
                            fight.Add("BIG|GOOD|Вы уничтожили противника :)");

                            Success(action, ref fight);

                            return fight;
                        }
                        else
                        {
                            fight.Add("Противник смог уклониться");
                        }
                    }
                    else if (roundResult == -1)
                    {
                        string bonuses = String.Empty, penalties = String.Empty;

                        int enemyDice = Game.Dice.Roll();
                        int enemyBonus = CountInCombination(enemyCombination, 3);
                        int enemyPenalty = CountInCombination(myCombination, 2);
                        int myEvasion = enemyDice + enemyBonus - enemyPenalty;

                        if (enemyBonus > 0)
                            bonuses = $", +{enemyBonus} за его тройки";

                        if (enemyPenalty > 0)
                            penalties = $", -{enemyPenalty} за ваши двойки";

                        fight.Add($"Вы пытется уклониться: " +
                            $"{Game.Dice.Symbol(enemyDice)}{bonuses}{penalties}, " +
                            $"итого {myEvasion} - это " +
                            $"{Game.Services.Сomparison(myEvasion, 2)} " +
                            $"порогового значения 'два'");

                        if (myEvasion > 2)
                        {
                            fight.Add("BIG|BAD|Противник уничтожил вас :(");
                            Character.Protagonist.Heroism = 0;
                            return fight;
                        }
                        else
                        {
                            fight.Add("Вы смогли уклониться");
                        }
                    }
                }

                fight.Add("BOLD|Бой окончился ничьёй");

                return fight;
            }
        }
    }
}
