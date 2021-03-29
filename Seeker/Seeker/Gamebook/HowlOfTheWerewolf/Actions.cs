﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.HowlOfTheWerewolf
{
    class Actions : Abstract.IActions
    {
        public enum Specifics { Nope, ElectricDamage, WitchFight, Ulrich, BlackWidow };

        public string ActionName { get; set; }
        public string ButtonName { get; set; }
        public string Aftertext { get; set; }
        public string Trigger { get; set; }

        public List<Character> Enemies { get; set; }
        public int RoundsToWin { get; set; }
        public int RoundsToFight { get; set; }
        public int WoundsToWin { get; set; }
        public int WoundsForTransformation { get; set; }
        public int WoundsLimit { get; set; }
        public int HitStrengthBonus { get; set; }
        public int ExtendedDamage { get; set; }
        public Specifics Specificity { get; set; }

        public List<string> Do(out bool reload, string action = "", bool trigger = false)
        {
            if (trigger)
                Game.Option.Trigger(Trigger);

            string actionName = (String.IsNullOrEmpty(action) ? ActionName : action);
            List<string> actionResult = typeof(Actions).GetMethod(actionName).Invoke(this, new object[] { }) as List<string>;

            reload = (actionResult.Count >= 1) && (actionResult[0] == "RELOAD");

            return actionResult;
        }

        public List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
                enemies.Add(String.Format("{0}\nмастерство {1}  выносливость {2}", enemy.Name, enemy.Mastery, enemy.Endurance));

            return enemies;
        }

        public List<string> Status()
        {
            List<string> statusLines = new List<string>
            {
                String.Format("Мастерство: {0}", Character.Protagonist.Mastery),
                String.Format("Выносливость: {0}", Character.Protagonist.Endurance),
                String.Format("Удача: {0}", Character.Protagonist.Luck),
                String.Format("Изменение: {0}", Character.Protagonist.Change)
            };

            return statusLines;
        }

        public List<string> AdditionalStatus() => null;

        public List<string> StaticButtons() => new List<string> { };

        public bool StaticAction(string action) => false;

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = "Начать сначала";

            return Character.Protagonist.Endurance <= 0;
        }

        public List<string> Luck()
        {
            int fisrtDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();

            bool goodLuck = (fisrtDice + secondDice) <= Character.Protagonist.Luck;

            List<string> luckCheck = new List<string> { String.Format(
                    "Проверка удачи: {0} + {1} {2} {3}",
                    Game.Dice.Symbol(fisrtDice), Game.Dice.Symbol(secondDice), (goodLuck ? "<=" : ">"), Character.Protagonist.Luck
            ) };

            luckCheck.Add(goodLuck ? "BIG|GOOD|УСПЕХ :)" : "BIG|BAD|НЕУДАЧА :(");

            if (Character.Protagonist.Luck > 2)
            {
                Character.Protagonist.Luck -= 1;
                luckCheck.Add("Уровень удачи снижен на единицу");
            }

            return luckCheck;
        }

        public List<string> Mastery()
        {
            int fisrtDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();

            bool masteryOk = (fisrtDice + secondDice) <= Character.Protagonist.Mastery;

            List<string> masteryCheck = new List<string> { String.Format(
                    "Проверка удачи: {0} + {1} {2} {3}",
                    Game.Dice.Symbol(fisrtDice), Game.Dice.Symbol(secondDice), (masteryOk ? "<=" : ">"), Character.Protagonist.Mastery
            ) };

            masteryCheck.Add(masteryOk ? "BIG|GOOD|Мастерства ХВАТИЛО :)" : "BIG|BAD|Мастерства НЕ хватило :(");

            return masteryCheck;
        }

        public List<string> Transformation()
        {
            int fisrtDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();

            bool changeOk = (fisrtDice + secondDice) > Character.Protagonist.Change;

            List<string> changeCheck = new List<string> { String.Format(
                    "Проверка удачи: {0} + {1} {2} {3}",
                    Game.Dice.Symbol(fisrtDice), Game.Dice.Symbol(secondDice), (changeOk ? ">" : "<="), Character.Protagonist.Change
            ) };

            changeCheck.Add(changeOk ? "BIG|GOOD|Победил ЧЕЛОВЕК :)" : "BIG|BAD|Победил ВОЛК :(");

            return changeCheck;
        }

        public List<string> Dice() => new List<string> { String.Format("BIG|На кубике выпало: {0}", Game.Dice.Symbol(Game.Dice.Roll())) };

        public List<string> DicesEndurance()
        {
            List<string> diceCheck = new List<string> { };

            int result = 0;

            for (int i = 1; i <= 3; i++)
            {
                int dice = Game.Dice.Roll();
                result += dice;
                diceCheck.Add(String.Format("На {0} выпало: {1}", i, Game.Dice.Symbol(dice)));
            }

            diceCheck.Add(String.Format("BIG|Сумма на кубиках: {0}", result));

            diceCheck.Add(result < Character.Protagonist.Endurance ? "BIG|GOOD|Меньше! :)" : "BIG|BAD|Больше :(");

            return diceCheck;
        }

        public List<string> DicesRestore()
        {
            List<string> diceRestore = new List<string> { };

            int dice = Game.Dice.Roll();

            diceRestore.Add(String.Format("На кубике выпало: {0}", Game.Dice.Symbol(dice)));

            string line = String.Empty;

            if (dice < 3)
            {
                Character.Protagonist.Mastery = Character.Protagonist.MaxMastery;
                line = "о Мастерство";
            }
            else if (dice > 4)
            {
                Character.Protagonist.Luck = Character.Protagonist.MaxLuck;
                line = "а Удача";
            }
            else
            {
                Character.Protagonist.Endurance = Character.Protagonist.MaxEndurance;
                line = "а Выносливость";
            }

            diceRestore.Add(String.Format("BIG|GOOD|Восстановлен{0}", line));

            return diceRestore;
        }

        public List<string> DiceWounds()
        {
            List<string> diceCheck = new List<string> { };

            int dice = Game.Dice.Roll();

            diceCheck.Add(String.Format("На кубике выпало: {0}", Game.Dice.Symbol(dice)));

            Character.Protagonist.Endurance -= dice;

            diceCheck.Add(String.Format("BIG|BAD|Вы потеряли жизней: {0}", dice));

            return diceCheck;
        }

        public List<string> DiceGold()
        {
            List<string> diceCheck = new List<string> { };

            int dice = Game.Dice.Roll();

            diceCheck.Add(String.Format("На кубике выпало: {0} + ещё 12", Game.Dice.Symbol(dice)));

            dice += 12;

            Character.Protagonist.Gold -= dice;

            diceCheck.Add(String.Format("BIG|GOOD|Вы нашли золотых: {0}", dice));

            return diceCheck;
        }

        public bool IsButtonEnabled() => true;

        public static bool CheckOnlyIf(string option) => true;

        private bool EnemyWound(List<Character> FightEnemies, ref int enemyWounds, ref List<string> fight)
        {
            enemyWounds += 1;

            bool enemyLost = true;

            foreach (Character e in FightEnemies)
                if (e.Endurance > (WoundsLimit > 0 ? WoundsLimit : 0))
                    enemyLost = false;

            if (enemyLost || ((WoundsToWin > 0) && (WoundsToWin <= enemyWounds)))
            {
                fight.Add(String.Empty);
                fight.Add(String.Format("BIG|GOOD|Вы ПОБЕДИЛИ :)"));
                return true;
            }
            else
                return false;
        }

        private int UlrichFight(string enemyName, ref List<string> fight, int enemyHitStrength)
        {
            int ulrichMastery = Constants.GetUlrichMastery();

            fight.Add(String.Empty);

            int ulrichRollFirst = Game.Dice.Roll();
            int ulrichRollSecond = Game.Dice.Roll();
            int ulrichHitStrength = ulrichRollFirst + ulrichRollSecond + ulrichMastery;

            fight.Add(String.Format("Сила удара Ульриха: {0} + {1} + {2} = {3}",
                Game.Dice.Symbol(ulrichRollFirst), Game.Dice.Symbol(ulrichRollSecond), ulrichMastery, ulrichHitStrength
            ));

            if (ulrichHitStrength > enemyHitStrength)
            {
                fight.Add(String.Format("GOOD|{0} ранен", enemyName));
                return 2;
            }
            else
            {
                fight.Add("BOLD|Ульрих не смог ранить врага");
                return 0;
            }
        }

        private void ElectricDamage(ref Character hero, ref List<string> fight)
        {
            int electric = Game.Dice.Roll();

            fight.Add(String.Format("Кубик электрического разряда: {0}", Game.Dice.Symbol(electric)));

            if (electric >= 5)
            {
                hero.Endurance -= 3;
                fight.Add("Вы потеряли ещё 3 Выносливость от разряда.");
            }
            else
                fight.Add("Разряд прошёл мимо.");
        }
        private void WitchFight(ref Character hero, ref List<string> fight)
        {
            int witchAttack = Game.Dice.Roll();

            fight.Add(String.Format("Кубик атаки: {0}", Game.Dice.Symbol(witchAttack)));

            if (witchAttack < 3)
            {
                hero.Endurance -= 2;
                fight.Add("Вы потеряли 2 Выносливости");
            }
            else if ((witchAttack == 3) || (witchAttack == 4))
            {
                hero.Endurance -= 3;
                fight.Add("Вы потеряли 3 Выносливости");
            }
            else if (witchAttack == 5)
            {
                hero.Endurance -= 2;
                hero.Luck -= 1;
                fight.Add("Вы потеряли 2 Выносливости и 1 Удачу");
            }
            else
            {
                hero.Endurance -= 2;
                hero.Change += 1;
                fight.Add(String.Format("Вы потеряли 2 Выносливости и Трансформация продолжилась (Изменение достигло {0})", hero.Change));
            }
        }

        private int BlackWidow(ref Character hero, ref List<string> fight)
        {
            int witchAttack = Game.Dice.Roll();

            fight.Add(String.Format("Кубик атаки: {0}", Game.Dice.Symbol(witchAttack)));

            if (witchAttack < 3)
            {
                hero.Endurance -= 2;
                fight.Add("Удар когтями: вы потеряли 2 Выносливости");
            }
            else if (witchAttack == 3)
            {
                hero.Endurance -= 3;
                fight.Add("Сильный удар: в потеряли 3 Выносливости и в следующий раунд не сможете атаковать пытаясь подняться на ноги");

                return 3;
            }
            else if (witchAttack == 4)
            {
                fight.Add("Плевок паутиной: вы не ранены, но следующий Раунд Атаки не можете защититься");

                return 4;
            }
            else if (witchAttack == 4)
            {
                hero.Endurance -=  4;
                fight.Add("Ядовитый укус: вы потеряли 4 Выносливости");
            }
            else
            {
                int spiders = Game.Dice.Roll();
                hero.Endurance -= spiders;

                fight.Add(String.Format("Стая пауков: вы теряете {0}, но и она теряет 2 Выносливости", Game.Dice.Symbol(spiders)));

                return 6;
            }

            return 0;
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            int round = 1, heroWounds = 0, enemyWounds = 0;
            int blackWidowLastAttack = 0;

            Character hero = Character.Protagonist;

            while (true)
            {
                fight.Add(String.Format("HEAD|Раунд: {0}", round));

                bool attackAlready = false;
                int protagonistHitStrength = 0;

                foreach (Character enemy in FightEnemies)
                {
                    if (enemy.Endurance <= 0)
                        continue;

                    fight.Add(String.Format("{0} (выносливость {1})", enemy.Name, enemy.Endurance));

                    if (blackWidowLastAttack == 4)
                    {
                        protagonistHitStrength = 0;
                        attackAlready = true;
                    }
                        
                    if (!attackAlready)
                    {
                        int protagonistRollFirst = Game.Dice.Roll();
                        int protagonistRollSecond = Game.Dice.Roll();
                        protagonistHitStrength = protagonistRollFirst + protagonistRollSecond + hero.Mastery + HitStrengthBonus;

                        string bonus = String.Empty;

                        if (HitStrengthBonus > 0)
                            bonus = String.Format(" + {0} бонус", HitStrengthBonus);

                        else if (HitStrengthBonus < 0)
                            bonus = String.Format(" - {0} пенальти", HitStrengthBonus);

                        else if (blackWidowLastAttack == 4)
                        {
                            bonus = " - 1 пенальти за паутину";
                            protagonistHitStrength -= 1;
                        }

                        fight.Add(String.Format("Сила вашего удара: {0} + {1} + {2}{3} = {4}",
                            Game.Dice.Symbol(protagonistRollFirst), Game.Dice.Symbol(protagonistRollSecond),
                            hero.Mastery, bonus, protagonistHitStrength
                        ));
                    }

                    int enemyRollFirst = Game.Dice.Roll();
                    int enemyRollSecond = Game.Dice.Roll();
                    int enemyHitStrength = enemyRollFirst + enemyRollSecond + enemy.Mastery;

                    fight.Add(String.Format("Сила его удара: {0} + {1} + {2} = {3}",
                        Game.Dice.Symbol(enemyRollFirst), Game.Dice.Symbol(enemyRollSecond), enemy.Mastery, enemyHitStrength
                    ));

                    if ((protagonistHitStrength > enemyHitStrength) && !attackAlready && (blackWidowLastAttack == 4))
                    {
                        fight.Add(String.Format("GOOD|{0} ранен", enemy.Name));

                        enemy.Endurance -= 2;

                        if (EnemyWound(FightEnemies, ref enemyWounds, ref fight))
                            return fight;
                    }
                    else if (protagonistHitStrength > enemyHitStrength)
                    {
                        fight.Add(String.Format("BOLD|{0} не смог вас ранить", enemy.Name));
                    }
                    else if (protagonistHitStrength < enemyHitStrength)
                    {
                        fight.Add(String.Format("BAD|{0} ранил вас", enemy.Name));

                        if (Specificity == Specifics.WitchFight)
                            WitchFight(ref hero, ref fight);

                        else if (Specificity == Specifics.BlackWidow)
                        {
                            blackWidowLastAttack = BlackWidow(ref hero, ref fight);

                            if (blackWidowLastAttack == 6)
                            {
                                enemy.Endurance -= 2;

                                if (EnemyWound(FightEnemies, ref enemyWounds, ref fight))
                                    return fight;
                            }
                        }
                        else
                            hero.Endurance -= (ExtendedDamage > 0 ? ExtendedDamage : 2);

                        if (Specificity == Specifics.ElectricDamage)
                            ElectricDamage(ref hero, ref fight);

                        heroWounds += 1;

                        if (heroWounds == WoundsForTransformation)
                        {
                            hero.Change += 1;

                            fight.Add(String.Empty);
                            fight.Add("BIG|BAD|Трансформация продолжается!");
                            fight.Add(String.Format("BAD|Изменение увеличилось на единицу и достигло {0}", hero.Change));
                        }

                        if (hero.Endurance <= 0)
                        {
                            fight.Add(String.Empty);
                            fight.Add(String.Format("BIG|BAD|Вы ПРОИГРАЛИ :("));
                            return fight;
                        }
                    }
                    else
                        fight.Add(String.Format("BOLD|Ничья в раунде"));

                    attackAlready = true;

                    if (Specificity == Specifics.Ulrich) 
                    {
                        enemy.Endurance -= UlrichFight(enemy.Name, ref fight, enemyHitStrength);

                        if (EnemyWound(FightEnemies, ref enemyWounds, ref fight))
                            return fight;
                    }

                    if (((RoundsToWin > 0) && (RoundsToWin <= round)) || ((RoundsToFight > 0) && (RoundsToFight <= round)))
                    {
                        fight.Add(String.Empty);

                        if (RoundsToWin > 0)
                        {
                            fight.Add(String.Format("BAD|Отведённые на победу раунды истекли.", RoundsToWin));
                            fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");
                        }
                        else
                            fight.Add(String.Format("GOOD|Отведённые на бой раунды истекли.", RoundsToFight));

                        return fight;
                    }

                    fight.Add(String.Empty);
                }

                round += 1;
            }
        }

        public bool IsHealingEnabled() => false;

        public void UseHealing(int healingLevel) => Game.Other.DoNothing();
    }
}
