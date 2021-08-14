using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.YounglingTournament
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public List<Character> Enemies { get; set; }

        public int Level { get; set; }

        public override List<string> Status() => new List<string>
        {
            String.Format("Cветлая сторона: {0}", protagonist.LightSide),
            String.Format("Тёмная сторона: {0}", protagonist.DarkSide),
        };

        public override List<string> AdditionalStatus() => new List<string>
        {
            String.Format("Понимание Силы: {0}", protagonist.ForceTechniques.Values.Sum()),
            String.Format("Взлом: {0}", protagonist.Hacking),
            String.Format("Скрытность: {0}", protagonist.Stealth),
            String.Format("Пилот: {0}", protagonist.Pilot),
            String.Format("Меткость: {0}", protagonist.Accuracy),
            String.Format("Выносливость: {0}", protagonist.Hitpoints),
        };

        public override bool CheckOnlyIf(string option)
        {
            if (option.Contains("|"))
                return option.Split('|').Where(x => Game.Data.Triggers.Contains(x.Trim())).Count() > 0;

            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains(">") || oneOption.Contains("<"))
                    {
                        int level = Game.Other.LevelParse(option);

                        if (oneOption.Contains("ПИЛОТ >") && (level <= protagonist.Pilot))
                            return false;
                    }

                    else if (oneOption.Contains("!"))
                    {
                        if (Game.Data.Triggers.Contains(oneOption.Replace("!", String.Empty).Trim()))
                            return false;
                    }
                    else if (!Game.Data.Triggers.Contains(oneOption.Trim()))
                        return false;
                }

                return true;
            }
        }

        public override List<string> Representer()
        {
            if (Level > 0)
                return new List<string> { String.Format("Пройдите проверку Понимания Силы, сложностью {0}", Level) };

            List<string> enemies = new List<string>();

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
            {
                string firepower = (enemy.Firepower > 5 ? String.Format("  сила выстрела {0}", enemy.Firepower) : String.Empty);
                string shield = (enemy.Shield > 0 ? String.Format("  энергощит {0}", enemy.Shield) : String.Empty);

                enemies.Add(String.Format("{0}\nметкость {1}  выносливость {2}{3}{4}",
                    enemy.Name, enemy.Accuracy, enemy.Hitpoints, firepower, shield));
            }

            return enemies;
        }

        public List<string> ForceTest()
        {
            List<string> test = new List<string>();

            int testDice = Game.Dice.Roll();
            int forceLevel = protagonist.ForceTechniques.Values.Sum();
            bool testPassed = testDice + forceLevel >= Level;

            test.Add(String.Format("Проверка Понимания: {0} + {1} {2} {3}",
                Game.Dice.Symbol(testDice), forceLevel, (testPassed ? ">=" : "<"), Level));

            test.Add(testPassed ? "BIG|GOOD|ПРОВЕРКА ПРОЙДЕНА :)" : "BIG|BAD|ПРОВЕРКА ПРОВАЛЕНА :(");

            return test;
        }

        public List<string> FireFight()
        {
            List<string> fight = new List<string>();

            Dictionary<Character, int> FightEnemies = new Dictionary<Character, int>();
            List<Character> EnemiesList = new List<Character>();

            foreach (Character enemy in Enemies)
            {
                Character newEnemy = enemy.Clone();
                FightEnemies.Add(newEnemy, 0);
                EnemiesList.Add(newEnemy);
            }
                
            int round = 1;

            while (true)
            {
                fight.Add(String.Format("HEAD|Раунд: {0}", round));

                int protagonistFirstDice = Game.Dice.Roll();
                int protagonistSecondDice = Game.Dice.Roll();
                int shotAccuracy = protagonist.Accuracy + protagonistFirstDice + protagonistSecondDice;

                fight.Add(String.Format("Ваш выстрел: {0} + {1} + {2} = {3}",
                    protagonist.Accuracy, Game.Dice.Symbol(protagonistFirstDice),
                    Game.Dice.Symbol(protagonistSecondDice), shotAccuracy));

                foreach (Character enemy in EnemiesList)
                {
                    if (enemy.Hitpoints <= 0)
                        FightEnemies[enemy] = -1;

                    else
                    {
                        int enemyFirstDice = Game.Dice.Roll();
                        int enemySecondDice = Game.Dice.Roll();
                        FightEnemies[enemy] = protagonist.Accuracy + enemyFirstDice + enemySecondDice;

                        fight.Add(String.Format("{0} стреляет: {1} + {2} + {3} = {4}",
                            enemy.Name, protagonist.Accuracy, Game.Dice.Symbol(enemyFirstDice),
                            Game.Dice.Symbol(enemySecondDice), FightEnemies[enemy]));
                    }
                }

                bool protaganistMakeShoot = false;

                foreach (KeyValuePair<Character, int> shooter in FightEnemies.OrderBy(x => x.Value))
                {
                    if (shooter.Value <= 0)
                        continue;

                    else if ((shooter.Value < shotAccuracy) && !protaganistMakeShoot)
                    {
                        protaganistMakeShoot = true;

                        if (shooter.Key.Shield > 0)
                        {
                            int damage = (protagonist.Firepower - shooter.Key.Shield);

                            if (damage <= 0)
                            {
                                fight.Add(String.Format("BAD|Вы подстрелили {0}, но его энергощит полностью поглотил урон", shooter.Key.Name));

                                shooter.Key.Shield -= protagonist.Firepower;
                            }
                            else
                            {
                                fight.Add(String.Format("BAD|Вы подстрелили {0}, его энергощит поглотил {1} ед.урона, " +
                                    "в результате он потерял {2} ед.выносливости", shooter.Key.Name, shooter.Key.Shield, damage));

                                shooter.Key.Hitpoints -= damage;
                                shooter.Key.Shield = 0;
                            }
                        }
                        else
                        {
                            shooter.Key.Hitpoints -= protagonist.Firepower;
                            fight.Add(String.Format("GOOD|Вы подстрелили {0}, он потерял {1} ед.выносливости",
                                shooter.Key.Name, protagonist.Firepower));
                        }
                    }

                    else if (shooter.Value > shotAccuracy)
                    {
                        protagonist.Hitpoints -= shooter.Key.Firepower;
                        fight.Add(String.Format("BAD|{0} подстрелил вас, вы потерял {1} единиц выносливости (осталось {2})",
                            shooter.Key.Name, shooter.Key.Firepower, protagonist.Hitpoints));
                    }
                }

                fight.Add(String.Empty);

                if (protagonist.Hitpoints <= 0)
                {
                    fight.Add(String.Format("BIG|BAD|Вы ПРОИГРАЛИ :("));
                    return fight;
                }

                if (FightEnemies.Keys.Where(x => x.Hitpoints > 0).Count() == 0)
                {
                    fight.Add(String.Format("BIG|GOOD|Вы ПОБЕДИЛИ :)"));
                    return fight;
                }

                round += 1;
            }
        }
    }
}
