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
        public int HeroHitpointsLimith { get; set; }
        public int EnemyHitpointsLimith { get; set; }
        public int HeroRoundWin { get; set; }
        public int EnemyRoundWin { get; set; }

        public int AccuracyBonus { get; set; }
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
                string accuracy = (enemy.Accuracy > 0 ? String.Format("  меткость {0}  ", enemy.Accuracy) : String.Empty);
                string firepower = (enemy.Firepower > 5 ? String.Format("  сила выстрела {0}", enemy.Firepower) : String.Empty);
                string shield = (enemy.Shield > 0 ? String.Format("  энергощит {0}", enemy.Shield) : String.Empty);
                string skill = (enemy.Skill > 0 ? String.Format("  ловкость {0}", enemy.Skill) : String.Empty);
                string rang = (enemy.Rang > 0 ? String.Format("  ранг {0}", enemy.Rang) : String.Empty);

                enemies.Add(String.Format("{0}\n{1}выносливость {2}{3}{4}{5}{6}",
                    enemy.Name, accuracy, enemy.GetHitpoints(), firepower, shield, skill, rang));
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
                int shotAccuracy = protagonist.Accuracy + protagonistFirstDice + protagonistSecondDice + AccuracyBonus;

                string bonus = (AccuracyBonus > 0 ? String.Format(" + {0} бонус", AccuracyBonus) : String.Empty);

                fight.Add(String.Format("Ваш выстрел: {0} меткость{1} + {2} + {3} = {4}",
                    protagonist.Accuracy, bonus, Game.Dice.Symbol(protagonistFirstDice),
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
                            enemy.Name, enemy.Accuracy, Game.Dice.Symbol(enemyFirstDice),
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
                        fight.Add(String.Format("BAD|{0} подстрелил вас, вы потерял {1} ед.выносливости (осталось {2})",
                            shooter.Key.Name, shooter.Key.Firepower, protagonist.Hitpoints));
                    }
                }

                fight.Add(String.Empty);

                if (protagonist.Hitpoints <= 0)
                {
                    fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");
                    return fight;
                }

                if (FightEnemies.Keys.Where(x => x.Hitpoints > 0).Count() == 0)
                {
                    fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");
                    return fight;
                }

                round += 1;
            }
        }

        private static Character.SwordTypes GetSwordType()
        {
            int max = protagonist.SwordTechniques[Character.SwordTypes.Decisiveness];
            Character.SwordTypes swordTechniques = Character.SwordTypes.Decisiveness;

            foreach (Character.SwordTypes swordType in protagonist.SwordTechniques.Keys)
            {
                if (protagonist.SwordTechniques[swordType] <= 0)
                    continue;

                int swordResult = SwordSkills(swordType, out string _);

                if (swordResult > max)
                {
                    max = swordResult;
                    swordTechniques = swordType;
                }
            }

            return swordTechniques;
        }

        private static int SwordSkills(Character.SwordTypes skill, out string detail)
        {
            int rang = protagonist.SwordTechniques[skill];

            switch (skill)
            {
                case Character.SwordTypes.Elasticity:
                    detail = String.Format("4 + {0}", rang);
                    return 4 + rang;

                case Character.SwordTypes.Rivalry:
                    detail = String.Format("4 + (2 x {0})", rang);
                    return 4 + (2 * rang);

                case Character.SwordTypes.Perseverance:
                    detail = String.Format("8 + {0}", rang);
                    return 8 + rang;

                case Character.SwordTypes.Aggressiveness:
                    detail = String.Format("12 + (2 x {0})", rang);
                    return 12 + (2 * rang);

                case Character.SwordTypes.Confidence:
                    detail = String.Format("12 + (3 x {0})", rang);
                    return 12 + (3 * rang);

                case Character.SwordTypes.Vaapad:
                    detail = String.Format("12 + (4 x {0})", rang);
                    return 12 + (4 * rang);
                
                case Character.SwordTypes.JarKai:
                    detail = String.Format("12 + (3 x {0})", rang);
                    return 12 + (3 * rang);

                default:
                case Character.SwordTypes.Decisiveness:
                    detail = String.Format("1 x {0}", rang);
                    return rang;
            }
        }

        public List<string> SwordFight()
        {
            List<string> fight = new List<string>();

            Dictionary<Character, int> FightEnemies = new Dictionary<Character, int>();
            List<Character> EnemiesList = new List<Character>();

            foreach (Character enemy in Enemies)
            {
                Character newEnemy = enemy.Clone().SetHitpoints();
                FightEnemies.Add(newEnemy, 0);
                EnemiesList.Add(newEnemy);
            }

            Character.SwordTypes currectSwordTechniques = GetSwordType();
            fight.Add(String.Format("Вы выбрали для боя Форму {0} ({1} ранг)",
                Constants.SwordSkillsNames()[currectSwordTechniques], protagonist.SwordTechniques[currectSwordTechniques]));

            int skill = SwordSkills(currectSwordTechniques, out string detail);
            fight.Add(String.Format("Ваша Ловкость в этом бою: {0} (по формуле: {1})", skill, detail));

            fight.Add(String.Empty);

            int round = 1, heroRoundWin = 0, enemyRoundWin = 0;

            while (true)
            {
                fight.Add(String.Format("HEAD|Раунд: {0}", round));

                int protagonistFirstDice = Game.Dice.Roll();
                int protagonistSecondDice = Game.Dice.Roll();
                int hitSkill = skill + protagonist.SwordTechniques[currectSwordTechniques] + protagonistFirstDice + protagonistSecondDice;             

                fight.Add(String.Format("Ваша скорость удара: {0} ловкость + {1} ранг + {2} + {3} = {4}",
                    skill, protagonist.SwordTechniques[currectSwordTechniques], Game.Dice.Symbol(protagonistFirstDice),
                    Game.Dice.Symbol(protagonistSecondDice), hitSkill));

                foreach (Character enemy in EnemiesList)
                {
                    if (enemy.Hitpoints <= 0)
                        FightEnemies[enemy] = -1;

                    else
                    {
                        int enemyFirstDice = Game.Dice.Roll();
                        int enemySecondDice = Game.Dice.Roll();
                        FightEnemies[enemy] = enemy.Skill + enemy.Rang + enemyFirstDice + enemySecondDice;

                        fight.Add(String.Format("Скорость удара {0}: {1} ловкость + {2} ранг + {3} + {4} = {5}",
                            enemy.Name, enemy.Skill, enemy.Rang, Game.Dice.Symbol(enemyFirstDice),
                            Game.Dice.Symbol(enemySecondDice), FightEnemies[enemy]));
                    }
                }

                bool protaganistMakeHit = false;

                foreach (KeyValuePair<Character, int> enemy in FightEnemies.OrderBy(x => x.Value))
                {
                    if (enemy.Value <= 0)
                        continue;

                    else if ((enemy.Value < hitSkill) && !protaganistMakeHit)
                    {
                        protaganistMakeHit = true;

                        enemy.Key.Hitpoints -= 3;
                        heroRoundWin += 1;

                        enemy.Key.SaveHitpoints();

                        fight.Add(String.Format("GOOD|Вы ранили {0}, он потерял 3 ед.выносливости (осталось {1})",
                            enemy.Key.Name, enemy.Key.Hitpoints));
                    }

                    else if (enemy.Value > hitSkill)
                    {
                        protagonist.Hitpoints -= 3;
                        enemyRoundWin += 1;

                        fight.Add(String.Format("BAD|{0} ранил вас, вы потеряли 3 единиц выносливости (осталось {1})",
                            enemy.Key.Name, protagonist.Hitpoints));
                    }

                    else
                        fight.Add("BOLD|Вы парировали удары друг друга");
                }

                fight.Add(String.Empty);

                bool enemyRound = (EnemyRoundWin > 0) && (enemyRoundWin >= EnemyRoundWin);
                int hitpointsLimit = (HeroHitpointsLimith > 0 ? HeroHitpointsLimith : 0);

                if ((protagonist.Hitpoints <= hitpointsLimit) || enemyRound)
                {
                    if (enemyRound)
                        fight.Add(String.Format("BAD|Вы проиграли {0} раунда...", enemyRoundWin));

                    fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");
                    return fight;
                }

                bool heroRound = (HeroRoundWin > 0) && (heroRoundWin >= HeroRoundWin);
                hitpointsLimit = (EnemyHitpointsLimith > 0 ? EnemyHitpointsLimith : 0);

                if ((FightEnemies.Keys.Where(x => x.Hitpoints > hitpointsLimit).Count() == 0) || heroRound)
                {
                    if (heroRound)
                        fight.Add(String.Format("GOOD|Вы выиграли {0} раундов...", heroRoundWin));

                    fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");
                    return fight;
                }

                round += 1;
            }
        }
    }
}
