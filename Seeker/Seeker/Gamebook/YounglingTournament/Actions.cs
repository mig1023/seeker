using System;
using System.Collections.Generic;
using System.Linq;
using static Seeker.Gamebook.YounglingTournament.Character;

namespace Seeker.Gamebook.YounglingTournament
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public List<Character> Enemies { get; set; }
        public string Enemy { get; set; }
        public int HeroHitpointsLimith { get; set; }
        public int EnemyHitpointsLimith { get; set; }
        public int HeroRoundWin { get; set; }
        public int EnemyRoundWin { get; set; }
        public bool SpeedActivate { get; set; }
        public bool WithoutTechnique { get; set; }
        public bool NoStrikeBack { get; set; }
        public int EnemyHitpointsPenalty { get; set; }
        public string BonusTechnique { get; set; }

        public int AccuracyBonus { get; set; }
        public int Level { get; set; }


        private static List<int> ForceTechniquesAlreadyUsed { get; set; }

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
                    bool thisIsTechnique = Enum.TryParse(oneOption, out Character.ForcesTypes techniqueType);

                    if (thisIsTechnique && (protagonist.ForceTechniques[techniqueType] == 0))
                        return false;

                    else if (oneOption.Contains(">") || oneOption.Contains("<"))
                    {
                        int level = Game.Other.LevelParse(option);

                        if (oneOption.Contains("ПИЛОТ >") && (level <= protagonist.Pilot))
                            return false;

                        if (oneOption.Contains("УКОЛОВ >") && (level <= protagonist.Thrust))
                            return false;

                        if (oneOption.Contains("УКОЛОВ У ВРАГА >") && (level <= protagonist.EnemyThrust))
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
                string technique = String.Empty, noStrikeBack = String.Empty;

                if (enemy.Rang > 0)
                {
                    bool anotherTechnique = Enum.TryParse(enemy.SwordTechnique, out SwordTypes currectSwordTechniques);

                    if (!anotherTechnique)
                        currectSwordTechniques = SwordTypes.Rivalry;

                    technique = String.Format("\nиспользует Форму {0} - {1} ранга",
                        Constants.SwordSkillsNames()[currectSwordTechniques], enemy.Rang);
                }

                if (NoStrikeBack)
                    noStrikeBack = "\nзнает защиту от Встречного удара";

                enemies.Add(String.Format("{0}\n{1}выносливость {2}{3}{4}{5}{6}{7}",
                    enemy.Name, accuracy, enemy.GetHitpoints(EnemyHitpointsPenalty),
                    firepower, shield, skill, technique, noStrikeBack));
            }

            return enemies;
        }

        public List<string> SimpleDice() => new List<string> { String.Format("BIG|Кубик: {0}", Game.Dice.Symbol(Game.Dice.Roll())) };

        public List<string> DiceWounds()
        {
            List<string> diceCheck = new List<string> { };

            int dice = Game.Dice.Roll();

            diceCheck.Add(String.Format("На кубике выпало: {0}", Game.Dice.Symbol(dice)));

            protagonist.Hitpoints -= dice;

            diceCheck.Add(String.Format("BIG|BAD|Вы потеряли жизней: {0}", dice));

            return diceCheck;
        }

        public List<string> EnemyDiceWounds()
        {
            List<string> diceCheck = new List<string> { };

            int dice = Game.Dice.Roll();

            int bonus = 0;
            string bonusLine = String.Empty;
            string[] enemy = Enemy.Split(',');

            bool withBonus = Enum.TryParse(BonusTechnique, out Character.ForcesTypes techniqueType);
            
            if (withBonus)
            {
                bonus = protagonist.ForceTechniques[techniqueType];
                bonusLine = String.Format(" + {0} за ранг", bonus);
            }

            diceCheck.Add(String.Format("На кубике выпало: {0}{1}", Game.Dice.Symbol(dice), bonusLine));

            dice += bonus;

            Character.SetHitpoints(enemy[0], dice, int.Parse(enemy[1]));

            diceCheck.Add(String.Format("BIG|GOOD|{0} потерял жизней: {1}", enemy[0], dice));

            return diceCheck;
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

        public List<string> MixedFightAttack()
        {
            List<string> attackCheck = new List<string> { };

            int deflecting = 4 + protagonist.SwordTechniques[SwordTypes.Rivalry];

            attackCheck.Add("Выстрел: 10 (сила выстрела) x 9 (меткость) = 90");

            attackCheck.Add(String.Format("Отражение: 4 + {0} ранг = {1}",
                protagonist.SwordTechniques[SwordTypes.Rivalry], deflecting));

            int result = 90 / deflecting;

            attackCheck.Add(String.Format("Результат: 90 выстрел / {0} отражение = {1}", deflecting, result));

            if (result > 0)
            {
                protagonist.Hitpoints -= result;
                attackCheck.Add(String.Format("BIG|BAD|Вы потеряли жизней: {0}", result));
            }
            else
                attackCheck.Add("BIG|GOOD|Вам удалось отразить выстрел противника в него самого!");

            return attackCheck;
        }

        public List<string> MixedFightDefence()
        {
            List<string> defenseCheck = new List<string> { };

            int deflecting = 4 + protagonist.SwordTechniques[SwordTypes.Rivalry];

            int firestDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();

            int shoot = firestDice + secondDice + 19;

            defenseCheck.Add(String.Format("Выстрел: {0} + {1} + 10 (сила выстрела) + 9 (меткость) = {2}",
                Game.Dice.Symbol(firestDice), Game.Dice.Symbol(secondDice), shoot));

            defenseCheck.Add(String.Format("Отражение: 4 + {0} ранг = {1}",
                protagonist.SwordTechniques[SwordTypes.Rivalry], deflecting));

            int result = shoot / deflecting;

            defenseCheck.Add(String.Format("Результат: {0} выстрел / {1} отражение = {2}",
                shoot, deflecting, result));

            protagonist.Hitpoints -= result;

            defenseCheck.Add(String.Format("BIG|BAD|Вы потеряли жизней: {0}", result));

            return defenseCheck;
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
                fight.Add(String.Format("HEAD|BOLD|Раунд: {0}", round));

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
                        FightEnemies[enemy] = enemy.Accuracy + enemyFirstDice + enemySecondDice;

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
                                fight.Add(String.Format("GOOD|Вы подстрелили {0}, но его энергощит полностью поглотил урон", shooter.Key.Name));

                                shooter.Key.Shield -= protagonist.Firepower;
                            }
                            else
                            {
                                fight.Add(String.Format("GOOD|Вы подстрелили {0}, его энергощит поглотил {1} ед.урона, " +
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
                    detail = String.Format("4 + {0} ранг", rang);
                    return 4 + rang;

                case Character.SwordTypes.Rivalry:
                    detail = String.Format("4 + (2 x {0} ранг)", rang);
                    return 4 + (2 * rang);

                case Character.SwordTypes.Perseverance:
                    detail = String.Format("8 + {0} ранг", rang);
                    return 8 + rang;

                case Character.SwordTypes.Aggressiveness:
                    detail = String.Format("12 + (2 x {0} ранг)", rang);
                    return 12 + (2 * rang);

                case Character.SwordTypes.Confidence:
                    detail = String.Format("12 + (3 x {0} ранг)", rang);
                    return 12 + (3 * rang);

                case Character.SwordTypes.Vaapad:
                    detail = String.Format("12 + (4 x {0} ранг)", rang);
                    return 12 + (4 * rang);
                
                case Character.SwordTypes.JarKai:
                    detail = String.Format("12 + (3 x {0} ранг)", rang);
                    return 12 + (3 * rang);

                default:
                case Character.SwordTypes.Decisiveness:
                    detail = String.Format("1 x {0} ранг", rang);
                    return rang;
            }
        }

        private bool AdditionalAttack(ref List<string> fight, Character enemy, string head, string body)
        {
            fight.Add(String.Format("BOLD|{0}", head));

            int strikeWound = Game.Dice.Roll();

            fight.Add(String.Format("{0}: {1}", body, Game.Dice.Symbol(strikeWound)));

            enemy.Hitpoints -= strikeWound;

            fight.Add(String.Format("GOOD|Вы ранили {0}, он потерял {1} ед.выносливости (осталось {2})",
                enemy.Name, strikeWound, enemy.Hitpoints));

            return true;
        }

        private void SpeedFightHitpointsLoss(ref List<string> fight, Character character)
        {
            bool isProtagonist = (character == protagonist);

            int technique = (isProtagonist ? protagonist.ForceTechniques[ForcesTypes.Speed] : character.Speed);
            int wound = (5 - technique);
            character.Hitpoints -= wound;

            if (isProtagonist)
                fight.Add(String.Format("BAD|Из-за применения Скорости Силы вы теряете {0} ед.выносливости (осталось {1})",
                    wound, protagonist.Hitpoints));
            else
                fight.Add(String.Format("GOOD|Из-за применения Скорости Силы {0} теряет {1} ед.выносливости (осталось {2})",
                    character.Name, wound, character.Hitpoints));
        }

        private bool UseForcesСhance() => Game.Dice.Roll() % 2 == 0;

        private bool SpeedActivation(ref List<string> fight, ref bool speedActivate, List<Character> EnemiesList)
        {
            fight.Add(String.Format("BOLD|Вы активируете Скорость Силы {0} ранга!",
                protagonist.ForceTechniques[ForcesTypes.Speed]));

            foreach (Character enemy in EnemiesList)
                fight.Add(String.Format("{0} активирует Скорость Силы {1} ранга.", enemy.Name, enemy.Speed));

            speedActivate = true;

            return false;
        }

        private bool UseForcesInFight(ref List<string> fight, ref bool speedActivate,
            List<Character> EnemiesList, out Character target)
        {
            target = EnemiesList.Where(x => x.Hitpoints > 0).FirstOrDefault();

            if (SpeedActivate && !speedActivate)
                return SpeedActivation(ref fight, ref speedActivate, EnemiesList);

            if (WithoutTechnique || speedActivate || !UseForcesСhance())
                return false;

            int forceTechniques = Game.Dice.Roll();

            if (ForceTechniquesAlreadyUsed.Contains(forceTechniques))
                return false;
            else
                ForceTechniquesAlreadyUsed.Add(forceTechniques);

            switch (forceTechniques)
            {
                case 1:
                    return SpeedActivation(ref fight, ref speedActivate, EnemiesList);

                case 2:
                    fight.Add("BOLD|Вы применяете Толчок Силы!");

                    int pushWound = Game.Dice.Roll();
                    target.Hitpoints -= (pushWound + protagonist.ForceTechniques[ForcesTypes.Push]);

                    fight.Add(String.Format("GOOD|{0} теряет {1} + {2} (за ранг техники) ед.выносливости (осталось {3})",
                        target.Name, Game.Dice.Symbol(pushWound),
                        protagonist.ForceTechniques[ForcesTypes.Push], target.Hitpoints));

                    return true;

                case 3:
                    fight.Add("BOLD|Вы применяете Прыжок Силы!");

                    int jump = Game.Dice.Roll();
                    int technique = protagonist.ForceTechniques[ForcesTypes.Jump];
                    bool success = (jump + technique) > 6;

                    fight.Add(String.Format("Прыжок: {0} + {1} {2} 6 - {3}",
                        Game.Dice.Symbol(jump), technique, Game.Other.Сomparison(jump + technique, 6),
                        (success ? "прыжок удался!" : "прыжок не получился...")));

                    if (success)
                    {
                        target.Hitpoints -= jump;

                        fight.Add(String.Format("GOOD|{0} теряет {1} ед.выносливости (осталось {2})",
                            target.Name, jump, target.Hitpoints));
                    }

                    return true;

                case 4:
                    if (protagonist.ForceTechniques[ForcesTypes.Suffocation] == 0)
                        return false;

                    fight.Add("BOLD|Вы применяете Удушение Силы!");

                    protagonist.DarkSide += 50;
                    Game.Option.Trigger("Темная сторона");
                    fight.Add("Вы получаете +50 к очкам Тёмной стороны и ключевое слово 'Тёмная сторона'.");

                    int suffWound = Game.Dice.Roll();
                    target.Hitpoints -= suffWound;

                    fight.Add(String.Format("GOOD|{0} теряет {1} ед.выносливости (осталось {2})",
                        target.Name, Game.Dice.Symbol(suffWound), target.Hitpoints));

                    return true;

                default:
                    return false;
            }
        }

        public List<string> SwordFight()
        {
            List<string> fight = new List<string>();

            Dictionary<Character, int> FightEnemies = new Dictionary<Character, int>();
            List<Character> EnemiesList = new List<Character>();

            ForceTechniquesAlreadyUsed = new List<int>();

            foreach (Character enemy in Enemies)
            {
                Character newEnemy = enemy.Clone().SetHitpoints(EnemyHitpointsPenalty);
                FightEnemies.Add(newEnemy, 0);
                EnemiesList.Add(newEnemy);
            }

            SwordTypes currectSwordTechniques = GetSwordType();

            fight.Add(String.Format("Вы выбрали для боя Форму {0} ({1} ранг)",
                Constants.SwordSkillsNames()[currectSwordTechniques], protagonist.SwordTechniques[currectSwordTechniques]));

            int skill = SwordSkills(currectSwordTechniques, out string detail);
            fight.Add(String.Format("Ваша Ловкость в этом бою: {0} (по формуле: {1})", skill, detail));

            fight.Add(String.Empty);

            int round = 1, heroRoundWin = 0, enemyRoundWin = 0;
            bool speedActivate = false, irresistibleAttack = false;
            bool strikeBack = NoStrikeBack;

            while (true)
            {
                fight.Add(String.Format("HEAD|BOLD|Раунд: {0}", round));

                if (UseForcesInFight(ref fight, ref speedActivate, EnemiesList, out Character target))
                {
                    round += 1;

                    int enemyLimit = (EnemyHitpointsLimith > 0 ? EnemyHitpointsLimith : 0);

                    if ((FightEnemies.Keys.Where(x => x.Hitpoints > enemyLimit).Count() == 0))
                    {
                        fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");
                        return fight;
                    }
                    else
                        continue;
                }

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

                        fight.Add(String.Format("GOOD|Вы ранили {0}, он потерял 3 ед.выносливости (осталось {1})",
                            enemy.Key.Name, enemy.Key.Hitpoints));

                        if ((heroRoundWin >= 3) && !irresistibleAttack && Game.Data.Triggers.Contains("Неотразимая атака"))
                            irresistibleAttack = AdditionalAttack(ref fight, enemy.Key, "Вы проводите Неотразимую атаку!", "Урон от атаки");
                    }

                    else if (enemy.Value > hitSkill)
                    {
                        protagonist.Hitpoints -= 3;
                        enemyRoundWin += 1;

                        fight.Add(String.Format("BAD|{0} ранил вас, вы потеряли 3 ед.выносливости (осталось {1})",
                            enemy.Key.Name, protagonist.Hitpoints));

                        if ((enemyRoundWin >= 3) && !strikeBack && Game.Data.Triggers.Contains("Встречный удар"))
                            strikeBack = AdditionalAttack(ref fight, enemy.Key, "Вы проводите Встречный удар!", "Урон от удара");
                    }

                    else
                        fight.Add("BOLD|Вы парировали удары друг друга");
                }

                if (speedActivate)
                {
                    SpeedFightHitpointsLoss(ref fight, protagonist);

                    foreach (Character enemy in EnemiesList.Where(x => x.Hitpoints > 0))
                        SpeedFightHitpointsLoss(ref fight, enemy);
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
