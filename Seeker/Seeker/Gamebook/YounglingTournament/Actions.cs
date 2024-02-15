using System;
using System.Collections.Generic;
using System.Linq;
using static Seeker.Gamebook.YounglingTournament.Character;

namespace Seeker.Gamebook.YounglingTournament
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        public static Actions GetInstance() => StaticInstance;
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
        

        public override List<string> Status() => new List<string>
        {
            $"Cветлая сторона: {protagonist.LightSide}",
            $"Тёмная сторона: {protagonist.DarkSide}",
        };

        public override List<string> AdditionalStatus()
        {
            List<string> newStatuses = new List<string>();

            newStatuses.Add($"Выносливость: {protagonist.Hitpoints}/{protagonist.MaxHitpoints}");

            if (protagonist.SecondPart == 0)
            {
                newStatuses.Add($"Взлом: {protagonist.Hacking}");
                newStatuses.Add($"Пилот: {protagonist.Pilot}");
                newStatuses.Add($"Меткость: {protagonist.Accuracy}");
            }
            else
            {
                if ((protagonist.Thrust > 0) || (protagonist.EnemyThrust > 0))
                    newStatuses.Add($"Уколов: {protagonist.Thrust} vs {protagonist.EnemyThrust}");

                newStatuses.Add($"Понимание Силы: {protagonist.ForceTechniques.Values.Sum()}");
                newStatuses.Add($"Форма {Fights.GetSwordSkillName(Fights.GetSwordType())}");
            }

            return newStatuses;
        }
            
        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains("|"))
            {
                return option.Split('|').Where(x => Game.Option.IsTriggered(x.Trim())).Count() > 0;
            }
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    bool thisIsTechnique = Enum.TryParse(oneOption, out Character.ForcesTypes techniqueType);

                    if (thisIsTechnique && (protagonist.ForceTechniques[techniqueType] == 0))
                    {
                        return false;
                    }
                    else if (oneOption.Contains(">") || oneOption.Contains("<"))
                    {
                        int level = Game.Services.LevelParse(option);

                        if (oneOption.Contains("ПИЛОТ >") && (level >= protagonist.Pilot))
                            return false;

                        if (oneOption.Contains("БИБЛИОТЕКА <=") && (level < protagonist.Reading))
                            return false;

                        if (oneOption.Contains("УКОЛОВ >") && (level >= protagonist.Thrust))
                            return false;

                        if (oneOption.Contains("УКОЛОВ У ВРАГА >") && (level >= protagonist.EnemyThrust))
                            return false;
                    }
                    else if (oneOption.Contains("!"))
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

        public override List<string> Representer()
        {
            if (Level > 0)
                return new List<string> { $"Пройдите проверку Понимания Силы, сложностью {Level}" };

            List<string> enemies = new List<string>();

            if ((Enemies == null) || (Type == "EnemyDiceWounds"))
                return enemies;

            foreach (Character enemy in Enemies)
            {
                string accuracy = (enemy.Accuracy > 0 ? $"  меткость {enemy.Accuracy}  " : String.Empty);
                string firepower = (enemy.Firepower > 5 ? $"  сила выстрела {enemy.Firepower}" : String.Empty);
                string shield = (enemy.Shield > 0 ? $"  энергощит {enemy.Shield}" : String.Empty);
                string skill = (enemy.Skill > 0 ? $"  ловкость {enemy.Skill}" : String.Empty);
                string technique = String.Empty, noStrikeBack = String.Empty;

                if (enemy.Rang > 0)
                {
                    bool anotherTechnique = Enum.TryParse(enemy.SwordTechnique,
                        out SwordTypes currectSwordTechniques);

                    if (!anotherTechnique)
                        currectSwordTechniques = SwordTypes.Rivalry;

                    technique = $"\nиспользует Форму " +
                        $"{Fights.GetSwordSkillName(currectSwordTechniques, rang: enemy.Rang)}";
                }

                if (NoStrikeBack)
                    noStrikeBack = "\nзнает защиту от Встречного удара";

                enemies.Add($"{enemy.Name}\n{accuracy}выносливость " +
                    $"{enemy.GetHitpoints(EnemyHitpointsPenalty)}" +
                    $"{firepower}{shield}{skill}{technique}{noStrikeBack}");
            }

            return enemies;
        }

        public List<string> DiceWounds()
        {
            List<string> diceCheck = new List<string> { };

            int dice = Game.Dice.Roll();

            diceCheck.Add($"На кубике выпало: {Game.Dice.Symbol(dice)}");

            protagonist.Hitpoints -= dice;

            diceCheck.Add($"BIG|BAD|Вы потеряли жизней: {dice}");

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
                bonusLine = $" + {bonus} за ранг";
            }

            diceCheck.Add($"На кубике выпало: {Game.Dice.Symbol(dice)}{bonusLine}");

            dice += bonus;

            Character.SetHitpoints(enemy[0], dice, int.Parse(enemy[1]));

            diceCheck.Add($"BIG|GOOD|{enemy[0]} потерял жизней: {dice}");

            return diceCheck;
        }

        public List<string> ForceTest()
        {
            List<string> test = new List<string>();

            int testDice = Game.Dice.Roll();
            int forceLevel = protagonist.ForceTechniques.Values.Sum();
            bool testPassed = testDice + forceLevel >= Level;
            string testLine = testPassed ? ">=" : "<";

            test.Add($"Проверка Понимания: " +
                $"{Game.Dice.Symbol(testDice)} + " +
                $"{forceLevel} {testLine} {Level}");

            test.Add(Result(testPassed, "ПРОВЕРКА ПРОЙДЕНА|ПРОВЕРКА ПРОВАЛЕНА"));

            return test;
        }

        public List<string> MixedFightAttack()
        {
            List<string> attackCheck = new List<string> { };

            int deflecting = 4 + protagonist.SwordTechniques[SwordTypes.Rivalry];

            attackCheck.Add("Выстрел: 10 (сила выстрела) x 9 (меткость) = 90");

            attackCheck.Add($"Отражение: 4 + " +
                $"{protagonist.SwordTechniques[SwordTypes.Rivalry]} " +
                $"ранг = {deflecting}");

            int result = 90 / deflecting;

            attackCheck.Add($"Результат: " +
                $"90 выстрел / {deflecting} отражение = {result}");

            if (result > 0)
            {
                protagonist.Hitpoints -= result;
                attackCheck.Add($"BIG|BAD|Вы потеряли жизней: {result}");
            }
            else
            {
                attackCheck.Add("BIG|GOOD|Вам удалось отразить " +
                    "выстрел противника в него самого!");
            }
                
            return attackCheck;
        }

        public List<string> MixedFightDefence()
        {
            List<string> defenseCheck = new List<string> { };

            int deflecting = 4 + protagonist.SwordTechniques[SwordTypes.Rivalry];

            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            int shoot = firstDice + secondDice + 19;

            defenseCheck.Add($"Выстрел: " +
                $"{Game.Dice.Symbol(firstDice)} + " +
                $"{Game.Dice.Symbol(secondDice)} + " +
                $"10 (сила выстрела) + 9 (меткость) = {shoot}");

            defenseCheck.Add($"Отражение: 4 + " +
                $"{protagonist.SwordTechniques[SwordTypes.Rivalry]} " +
                $"ранг = {deflecting}");

            int result = shoot / deflecting;

            defenseCheck.Add($"Результат: " +
                $"{shoot} выстрел / {deflecting} " +
                $"отражение = {result}");

            protagonist.Hitpoints -= result;

            defenseCheck.Add($"BIG|BAD|Вы потеряли жизней: {result}");

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
                fight.Add($"HEAD|BOLD|Раунд: {round}");

                Game.Dice.DoubleRoll(out int protagonistFirstDice,
                    out int protagonistSecondDice);

                int shotAccuracy = protagonist.Accuracy + 
                    protagonistFirstDice + protagonistSecondDice + AccuracyBonus;

                string bonus = (AccuracyBonus > 0 ? $" + {AccuracyBonus} бонус" : String.Empty);

                fight.Add($"Ваш выстрел: " +
                    $"{protagonist.Accuracy} меткость{bonus} + " +
                    $"{Game.Dice.Symbol(protagonistFirstDice)} + " +
                    $"{Game.Dice.Symbol(protagonistSecondDice)} = {shotAccuracy}");

                foreach (Character enemy in EnemiesList)
                {
                    if (enemy.Hitpoints <= 0)
                    {
                        FightEnemies[enemy] = -1;
                    }
                    else
                    {
                        Game.Dice.DoubleRoll(out int enemyFirstDice, out int enemySecondDice);
                        FightEnemies[enemy] = enemy.Accuracy + enemyFirstDice + enemySecondDice;

                        fight.Add($"{enemy.Name} стреляет: " +
                            $"{enemy.Accuracy} + " +
                            $"{Game.Dice.Symbol(enemyFirstDice)} + " +
                            $"{Game.Dice.Symbol(enemySecondDice)} = {FightEnemies[enemy]}");
                    }
                }

                bool protaganistMakeShoot = false;

                foreach (KeyValuePair<Character, int> shooter in FightEnemies.OrderBy(x => x.Value))
                {
                    if (shooter.Value <= 0)
                    {
                        continue;
                    }
                    else if ((shooter.Value < shotAccuracy) && !protaganistMakeShoot)
                    {
                        protaganistMakeShoot = true;

                        if (shooter.Key.Shield > 0)
                        {
                            int damage = (protagonist.Firepower - shooter.Key.Shield);

                            if (damage <= 0)
                            {
                                fight.Add($"GOOD|Вы подстрелили {shooter.Key.Name}, " +
                                    $"но его энергощит полностью поглотил урон");

                                shooter.Key.Shield -= protagonist.Firepower;
                            }
                            else
                            {
                                fight.Add($"GOOD|Вы подстрелили " +
                                    $"{shooter.Key.Name}, его энергощит " +
                                    $"поглотил {shooter.Key.Shield} ед.урона, " +
                                    $"в результате он потерял {damage} " +
                                    $"ед.выносливости");

                                shooter.Key.Hitpoints -= damage;
                                shooter.Key.Shield = 0;
                            }
                        }
                        else
                        {
                            shooter.Key.Hitpoints -= protagonist.Firepower;
                            fight.Add($"GOOD|Вы подстрелили {shooter.Key.Name}, " +
                                $"он потерял {protagonist.Firepower} ед.выносливости");
                        }
                    }
                    else if (shooter.Value > shotAccuracy)
                    {
                        protagonist.Hitpoints -= shooter.Key.Firepower;

                        fight.Add($"BAD|{shooter.Key.Name} " +
                            $"подстрелил вас, вы потерял " +
                            $"{shooter.Key.Firepower} ед.выносливости " +
                            $"(осталось {protagonist.Hitpoints})");
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

        public List<string> SwordFight()
        {
            List<string> fight = new List<string>();

            Dictionary<Character, int> FightEnemies = new Dictionary<Character, int>();
            List<Character> EnemiesList = new List<Character>();

            foreach (Character enemy in Enemies)
            {
                Character newEnemy = enemy.Clone().SetHitpoints(EnemyHitpointsPenalty);
                FightEnemies.Add(newEnemy, 0);
                EnemiesList.Add(newEnemy);
            }

            SwordTypes currectSwordTechniques = Fights.GetSwordType();

            fight.Add($"Вы выбрали для боя Форму " +
                $"{Fights.GetSwordSkillName(currectSwordTechniques)}");

            int skill = Fights.SwordSkills(currectSwordTechniques, out string detail);

            fight.Add($"Ваша Ловкость в этом бою: {skill} (по формуле: {detail})");
            fight.Add(String.Empty);

            int round = 1, heroRoundWin = 0, enemyRoundWin = 0;
            bool speedActivate = false, irresistibleAttack = false, rapidAttack = false;
            bool strikeBack = NoStrikeBack;

            while (true)
            {
                fight.Add($"HEAD|BOLD|Раунд: {round}");

                if (Fights.UseForcesInFight(ref fight, ref speedActivate, EnemiesList, SpeedActivate, WithoutTechnique))
                {
                    int enemyLimit = (EnemyHitpointsLimith > 0 ? EnemyHitpointsLimith : 0);

                    if ((FightEnemies.Keys.Where(x => x.Hitpoints > enemyLimit).Count() == 0))
                    {
                        fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");
                        return fight;
                    }
                }

                if (Game.Option.IsTriggered("Скоростная атака") && (round > 3) && !rapidAttack)
                {
                    Character target = EnemiesList.Where(x => x.Hitpoints > 0).FirstOrDefault();

                    irresistibleAttack = Fights.AdditionalAttack(ref fight, target,
                        "Вы проводите Скоростную атаку!", "Урон от атаки");
                }                  

                int protagonistFirstDice = Game.Dice.Roll();
                int protagonistSecondDice = Game.Dice.Roll();
                int hitSkill = skill + protagonist.SwordTechniques[currectSwordTechniques] + 
                    protagonistFirstDice + protagonistSecondDice;

                fight.Add($"Ваша скорость удара: {skill} ловкость + " +
                    $"{protagonist.SwordTechniques[currectSwordTechniques]} " +
                    $"ранг + {Game.Dice.Symbol(protagonistFirstDice)} + " +
                    $"{Game.Dice.Symbol(protagonistSecondDice)} = {hitSkill}");

                foreach (Character enemy in EnemiesList)
                {
                    if (enemy.Hitpoints <= 0)
                    {
                        FightEnemies[enemy] = -1;
                    }
                    else
                    {
                        Game.Dice.DoubleRoll(out int enemyFirstDice, out int enemySecondDice);
                        FightEnemies[enemy] = enemy.Skill + enemy.Rang + enemyFirstDice + enemySecondDice;

                        fight.Add($"Скорость удара {enemy.Name}: " +
                            $"{enemy.Skill} ловкость + {enemy.Rang} ранг + " +
                            $"{Game.Dice.Symbol(enemyFirstDice)} + " +
                            $"{Game.Dice.Symbol(enemySecondDice)} = {FightEnemies[enemy]}");
                    }
                }

                bool protaganistMakeHit = false;

                foreach (KeyValuePair<Character, int> enemy in FightEnemies.OrderBy(x => x.Value))
                {
                    if (enemy.Value <= 0)
                    {
                        continue;
                    }
                    else if ((enemy.Value < hitSkill) && !protaganistMakeHit)
                    {
                        protaganistMakeHit = true;

                        enemy.Key.Hitpoints -= 3;
                        heroRoundWin += 1;

                        fight.Add($"GOOD|Вы ранили {enemy.Key.Name}, " +
                            $"он потерял 3 ед.выносливости " +
                            $"(осталось {enemy.Key.Hitpoints})");

                        if ((heroRoundWin >= 3) && !irresistibleAttack && Game.Option.IsTriggered("Неотразимая атака"))
                        {
                            irresistibleAttack = Fights.AdditionalAttack(ref fight, enemy.Key,
                                "Вы проводите Неотразимую атаку!", "Урон от атаки");
                        }
                    }

                    else if (enemy.Value > hitSkill)
                    {
                        protagonist.Hitpoints -= 3;
                        enemyRoundWin += 1;

                        fight.Add($"BAD|{enemy.Key.Name} ранил вас, " +
                            $"вы потеряли 3 ед.выносливости " +
                            $"(осталось {protagonist.Hitpoints})");

                        if ((enemyRoundWin >= 3) && !strikeBack && Game.Option.IsTriggered("Встречный удар"))
                        {
                            strikeBack = Fights.AdditionalAttack(ref fight, enemy.Key,
                                "Вы проводите Встречный удар!", "Урон от удара");
                        }
                    }
                    else
                    {
                        fight.Add("BOLD|Вы парировали удары друг друга");
                    }
                }

                if (speedActivate)
                {
                    Fights.SpeedFightHitpointsLoss(ref fight, protagonist);

                    foreach (Character enemy in EnemiesList.Where(x => x.Hitpoints > 0))
                        Fights.SpeedFightHitpointsLoss(ref fight, enemy);
                }

                fight.Add(String.Empty);

                bool enemyRound = (EnemyRoundWin > 0) && (enemyRoundWin >= EnemyRoundWin);
                int hitpointsLimit = (HeroHitpointsLimith > 0 ? HeroHitpointsLimith : 0);

                if ((protagonist.Hitpoints <= hitpointsLimit) || enemyRound)
                {
                    if (enemyRound)
                    {
                        fight.Add($"BIG|BAD|Вы проиграли {enemyRoundWin} раунда :(");
                    }
                    else
                    {
                        fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");
                    }

                    return fight;
                }

                bool heroRound = (HeroRoundWin > 0) && (heroRoundWin >= HeroRoundWin);
                hitpointsLimit = (EnemyHitpointsLimith > 0 ? EnemyHitpointsLimith : 0);

                if ((FightEnemies.Keys.Where(x => x.Hitpoints > hitpointsLimit).Count() == 0) || heroRound)
                {
                    if (heroRound)
                    {
                        fight.Add($"BIG|GOOD|Вы выиграли {heroRoundWin} раундов :)");
                    }
                    else
                    {
                        fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");
                    }

                    return fight;
                }

                round += 1;
            }
        }
    }
}
