﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.FaithfulSwordOfTheKing
{
    class Actions : Abstract.IActions
    {
        public string ActionName { get; set; }
        public string ButtonName { get; set; }
        public string Aftertext { get; set; }
        public string Trigger { get; set; }

        public List<Character> Enemies { get; set; }
        public int RoundsToWin { get; set; }
        public int WoundsToWin { get; set; }
        public int SkillPenalty { get; set; }

        public string Text { get; set; }
        public int Price { get; set; }
        public bool Used { get; set; }
        public bool Multiple { get; set; }
        public Modification Benefit { get; set; }
        public Character.MeritalArts? MeritalArt { get; set; }

        public List<string> Do(out bool reload, string action = "", bool trigger = false)
        {
            if (trigger)
                Game.Option.Trigger(Trigger);

            string actionName = (String.IsNullOrEmpty(action) ? ActionName : action);
            List<string> actionResult = typeof(Actions).GetMethod(actionName).Invoke(this, new object[] { }) as List<string>;

            reload = ((actionResult.Count >= 1) && (actionResult[0] == "RELOAD") ? true : false);

            return actionResult;
        }

        public List<string> Status()
        {
            List<string> statusLines = new List<string>
            {
                String.Format("Ловкость: {0}", Character.Protagonist.Skill),
                String.Format("Сила: {0}", Character.Protagonist.Strength),
                String.Format("Честь: {0}", Character.Protagonist.Honor),
                String.Format("День: {0}", Character.Protagonist.Day),
                String.Format("Экю: {0:f2}", (double)Character.Protagonist.Ecu / 100)
            };

            return statusLines;
        }

        public List<string> AdditionalStatus() => null;

        public List<string> StaticButtons() => new List<string> { };

        public bool StaticAction(string action) => false;

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            if (Character.Protagonist.Strength <= 0)
            {
                toEndParagraph = 0;
                toEndText = "Начать сначала";

                return true;
            }
            else if (Character.Protagonist.Honor <= 0)
            {
                toEndParagraph = 150;
                toEndText = "Задуматься о чести";

                Character.Protagonist.Strength = 0;

                return true;
            }
            else
            {
                toEndParagraph = 0;
                toEndText = String.Empty;

                return false;
            }
        }
        public bool IsButtonEnabled()
        {
            bool disabledMeritalArtButton = (MeritalArt != Character.MeritalArts.Nope) && (Character.Protagonist.MeritalArt != Character.MeritalArts.Nope);
            bool disabledGetOptions = (Price > 0) && Used;
            bool disabledByPrice = (Price > 0) && (Character.Protagonist.Ecu < Price);

            return !(disabledMeritalArtButton || disabledGetOptions || disabledByPrice);
        }

        public static bool CheckOnlyIf(string option)
        {
            if (option == Character.Protagonist.MeritalArt.ToString())
                return true;
            else
                return Game.Data.Triggers.Contains(option);
        }

        public List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (ActionName == "Get")
                return new List<string> { Text };

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
                enemies.Add(String.Format("{0}\nловкость {1}  сила {2}", enemy.Name, enemy.Skill, enemy.Strength));

            return enemies;
        }

        public List<string> Luck()
        {
            int luckDice = Game.Dice.Roll();

            bool goodLuck = luckDice % 2 == 0;

            List<string> luckCheck = new List<string> { String.Format(
                "Проверка удачи: {0} ⚄ - {1}", luckDice, (goodLuck ? "чётное" : "нечётное")
            ) };

            luckCheck.Add(goodLuck ? "BIG|GOOD|УСПЕХ :)" : "BIG|BAD|НЕУДАЧА :(");

            return luckCheck;
        }

        public List<string> Skill()
        {
            int firstDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();

            bool goodSkill = (firstDice + secondDice) <= Character.Protagonist.Skill;

            List<string> skillCheck = new List<string> { String.Format(
                "Проверка ловкости: {0} ⚄ + {1} ⚄ {2} {3} ловкость", firstDice, secondDice, (goodSkill ? "<=" : ">"), Character.Protagonist.Skill
            ) };

            skillCheck.Add(goodSkill ? "BIG|GOOD|ЛОВКОСТИ ХВАТИЛО :)" : "BIG|BAD|ЛОВКОСТИ НЕ ХВАТИЛО :(");

            return skillCheck;
        }

        public List<string> DicesDoubles()
        {
            List<string> doubleCheck = new List<string>();

            bool doubleFail = false;

            bool firstDicesDouble = Game.Dice.Roll() == Game.Dice.Roll();
            bool secondDicesDouble = Game.Dice.Roll() == Game.Dice.Roll();

            for (int i = 0; i < 2; i++)
            {
                int firstDice = Game.Dice.Roll();
                int secondDice = Game.Dice.Roll();

                bool fail = firstDice == secondDice;

                doubleCheck.Add(String.Format("Бросок: {0} ⚄ и {1} ⚄ - {2}дубль", firstDice, secondDice, (fail ? String.Empty : "НЕ ") ) );

                if (fail)
                    doubleFail = true;
            }

            doubleCheck.Add(doubleFail ? "BIG|BAD|ВЫПАЛИ :(" : "BIG|GOOD|НЕ ВЫПАЛИ :)");

            return doubleCheck;
        }

        public List<string> Pursuit()
        {
            List<string> pursuit = new List<string>();

            int threeTimesInRow = 0;
            int theyWin = 0;
            
            for (int i = 1; i < 12; i++)
            {
                int heroSpeed = Game.Dice.Roll();
                int enemiesSpeed = Game.Dice.Roll();

                pursuit.Add(String.Format("Ваша скорость: {0} ⚄  <-->  Их скорость: {1} ⚄", heroSpeed, enemiesSpeed));

                if (heroSpeed > enemiesSpeed)
                {
                    pursuit.Add("GOOD|Вы быстрее");
                    threeTimesInRow = 0;
                }
                else
                {
                    pursuit.Add("BAD|Они быстрее");
                    threeTimesInRow += 1;
                    theyWin += 1;

                    if (threeTimesInRow >= 3)
                    {
                        pursuit.Add(String.Empty);
                        pursuit.Add("BIG|BAD|Они без труда догнали вас :(");

                        return pursuit;
                    }
                }
            }

            pursuit.Add(String.Empty);

            if (theyWin > 6)
                pursuit.Add("BIG|BAD|Они догнали вас :(");
            else
                pursuit.Add("BIG|GOOD|Они не догнали вас :)");

            return pursuit;
        }

        public List<string> Get()
        {
            if ((MeritalArt != null) && (Character.Protagonist.MeritalArt == Character.MeritalArts.Nope))
                Character.Protagonist.MeritalArt = MeritalArt ?? Character.MeritalArts.Nope;
            else if ((Price > 0) && (Character.Protagonist.Ecu >= Price))
            {
                Character.Protagonist.Ecu -= Price;

                if (!Multiple)
                    Used = true;

                if (Benefit != null)
                    Benefit.Do();
            }

            return new List<string> { "RELOAD" };
        }

        private bool NoMoreEnemies(List<Character> enemies)
        {
            foreach (Character enemy in enemies)
                if (enemy.Strength > 0)
                    return false;

            return true;
        }

        private bool LuckyHit(int? roll = null) => (roll ?? Game.Dice.Roll()) % 2 == 0;

        private bool EnemyWound(Character hero, ref Character enemy, List<Character> FightEnemies,
            int roll, int WoundsToWin, ref int enemyWounds, ref List<string> fight, bool dagger = false)
        {
            enemy.Strength -= ((hero.MeritalArt == Character.MeritalArts.SwordAndDagger) && LuckyHit(roll) && !dagger ? 3 : 2);

            if (enemy.Strength <= 0)
                enemy.Strength = 0;

            enemyWounds += 1;

            bool enemyLost = NoMoreEnemies(FightEnemies);

            if (enemyLost || ((WoundsToWin > 0) && (WoundsToWin <= enemyWounds)))
            {
                fight.Add(String.Empty);
                fight.Add(String.Format("BIG|GOOD|Вы ПОБЕДИЛИ :)"));
                return true;
            }
            else
                return false;
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            int round = 1;
            int enemyWounds = 0;
            int shoots = 0;

            Character hero = Character.Protagonist;

            if ((hero.MeritalArt == Character.MeritalArts.TwoPistols) && (hero.Pistols > 1) && (hero.BulletsAndGubpowder > 1))
                shoots = 2;
            else if ((hero.Pistols > 0) && (hero.BulletsAndGubpowder > 0))
                shoots = 1;

            for (int pistol = 1; pistol <= shoots; pistol++)
            {
                if (NoMoreEnemies(FightEnemies))
                    continue;

                bool hit = LuckyHit();

                hero.BulletsAndGubpowder -= 1;

                fight.Add(String.Format("Выстрел из {0}пистолета: {1}", (shoots > 1 ? String.Format("{0} ", pistol) : ""), (hit ? "попал" : "промах")));

                if (hit)
                {
                    foreach (Character enemy in FightEnemies)
                        if (enemy.Strength > 0)
                        {
                            fight.Add(String.Format("GOOD|{0} убит", enemy.Name));
                            enemy.Strength = 0;
                            break;
                        }
                }
            }

            if (NoMoreEnemies(FightEnemies))
                return fight;

            if (shoots > 0)
                fight.Add(String.Empty);

            while (true)
            {
                fight.Add(String.Format("HEAD|Раунд: {0}", round));

                if ((hero.MeritalArt == Character.MeritalArts.SecretBlow) && (round == 1))
                    foreach (Character enemy in FightEnemies)
                        if (enemy.Strength > 0)
                        {
                            {
                                enemy.Strength -= 4;
                                fight.Add(String.Format("Тайный удар шпагой: {0} теряет 4 силы, у него осталось {1}", enemy.Name, enemy.Strength));
                                fight.Add(String.Empty);
                                break;
                            }
                        }

                foreach (Character enemy in FightEnemies)
                {
                    if (enemy.Strength <= 0)
                        continue;

                    Character enemyInFight = enemy;
                    fight.Add(String.Format("{0} (сила {1})", enemy.Name, enemy.Strength));

                    int protagonistRoll = Game.Dice.Roll();
                    int protagonistSkill = hero.Skill - SkillPenalty - (hero.MeritalArt == Character.MeritalArts.LefthandFencing ? 0 : enemy.LeftHandPenalty);
                    int protagonistHitStrength = protagonistRoll + protagonistSkill;
                    fight.Add(String.Format("Мощность вашего удара: {0} ⚄ x 2 + {1} = {2}", protagonistRoll, protagonistSkill, protagonistHitStrength));

                    int enemyRoll = Game.Dice.Roll();
                    int enemyHitStrength = enemyRoll + enemy.Skill;
                    fight.Add(String.Format("Мощность его удара: {0} ⚄ x 2 + {1} = {2}", enemyRoll, enemy.Skill, enemyHitStrength));

                    if (protagonistHitStrength > enemyHitStrength)
                    {
                        if ((enemy.Chainmail > 0) && (protagonistRoll == 3))
                            fight.Add(String.Format("BOLD|Кольчуга отразила удар!"));
                        else
                        {
                            fight.Add(String.Format("GOOD|{0} ранен", enemy.Name));

                            if (EnemyWound(hero, ref enemyInFight, FightEnemies, protagonistRoll, WoundsToWin, ref enemyWounds, ref fight))
                                return fight;
                        }
                    }
                    else if (protagonistHitStrength < enemyHitStrength)
                    {
                        if ((hero.Chainmail > 0) && (enemyRoll == 6))
                            fight.Add(String.Format("BOLD|Кольчуга отразила удар!"));
                        else
                        {
                            fight.Add(String.Format("BAD|{0} ранил вас", enemy.Name));
                            hero.Strength -= 2;

                            if (hero.Strength < 0)
                                hero.Strength = 0;

                            if (hero.Strength <= 0)
                            {
                                fight.Add(String.Empty);
                                fight.Add(String.Format("BIG|BAD|Вы ПРОИГРАЛИ :("));
                                return fight;
                            }

                            if ((hero.MeritalArt == Character.MeritalArts.SwordAndDagger) && LuckyHit(protagonistRoll))
                            {
                                fight.Add(String.Format("GOOD|{0} ранен вашим кинжалом", enemy.Name));

                                if (EnemyWound(hero, ref enemyInFight, FightEnemies, protagonistRoll, WoundsToWin, ref enemyWounds, ref fight, dagger: true))
                                    return fight;
                            }
                        }
                    }
                    else
                        fight.Add(String.Format("BOLD|Ничья в раунде"));

                    if ((RoundsToWin > 0) && (RoundsToWin <= round))
                    {
                        fight.Add(String.Empty);
                        fight.Add(String.Format("BAD|Отведённые на победу раунды истекли.", RoundsToWin));
                        fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");
                        return fight;
                    }

                    fight.Add(String.Empty);
                }

                round += 1;
            }
        }
    }
}
