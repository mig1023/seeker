﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.OctopusIsland
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();

        public List<Character> Enemies { get; set; }
        public int WoundsToWin { get; set; }
        public int DinnerHitpointsBonus { get; set; }

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
                enemies.Add(String.Format("{0}\nловкость {1}  жизни {2}", enemy.Name, enemy.Skill, enemy.Hitpoint));

            return enemies;
        }

        public override List<string> Status() => new List<string>
        {
            String.Format("Обедов: {0}", Character.Protagonist.Food),
            String.Format("Животворная мазь: {0}", Character.Protagonist.LifeGivingOintment),
        };

        public override List<string> AdditionalStatus() => new List<string>
        {
            String.Format("Серж: {0}/{1}", Character.Protagonist.SergeSkill, Character.Protagonist.SergeHitpoint),
            String.Format("Ксолотл: {0}/{1}", Character.Protagonist.XolotlSkill, Character.Protagonist.XolotlHitpoint),
            String.Format("Тибо: {0}/{1}", Character.Protagonist.ThibautSkill, Character.Protagonist.ThibautHitpoint),
            String.Format("Суи: {0}/{1}", Character.Protagonist.SouhiSkill, Character.Protagonist.SouhiHitpoint),
        };

        public override List<string> StaticButtons()
        {
            List<string> staticButtons = new List<string> { };

            if (Character.Protagonist.LifeGivingOintment <= 0)
                return staticButtons;

            if (Character.Protagonist.SergeHitpoint < 20)
                staticButtons.Add("ВЫЛЕЧИТЬ СЕРЖА");

            if (Character.Protagonist.XolotlHitpoint < 20)
                staticButtons.Add("ВЫЛЕЧИТЬ КСОЛОТЛА");

            if (Character.Protagonist.ThibautHitpoint < 20)
                staticButtons.Add("ВЫЛЕЧИТЬ ТИБО");

            if (Character.Protagonist.SouhiHitpoint < 20)
                staticButtons.Add("ВЫЛЕЧИТЬ СУИ");

            return staticButtons;
        }

        private int LifeGivingOintmentFor(int protagonistHitpoint)
        {
            while ((Character.Protagonist.LifeGivingOintment > 0) && (protagonistHitpoint < 20))
            {
                Character.Protagonist.LifeGivingOintment -= 1;
                protagonistHitpoint += 1;
            }

            return protagonistHitpoint;
        }

        public override bool StaticAction(string action)
        {
            if (action.Contains("СЕРЖА"))
                Character.Protagonist.SergeHitpoint = LifeGivingOintmentFor(Character.Protagonist.SergeHitpoint);

            else if (action.Contains("КСОЛОТЛА"))
                Character.Protagonist.XolotlHitpoint = LifeGivingOintmentFor(Character.Protagonist.XolotlHitpoint);

            else if (action.Contains("ТИБО"))
                Character.Protagonist.ThibautHitpoint = LifeGivingOintmentFor(Character.Protagonist.ThibautHitpoint);

            else if (action.Contains("СУИ"))
                Character.Protagonist.SouhiHitpoint = LifeGivingOintmentFor(Character.Protagonist.SouhiHitpoint);

            else
                return false;

            return true;
        }

        public override bool IsButtonEnabled() => !((DinnerHitpointsBonus > 0) && (Character.Protagonist.Food <= 0));

        public override bool CheckOnlyIf(string option)
        {
            if (option.Contains("|"))
                return option.Split('|').Where(x => Game.Data.Triggers.Contains(x.Trim())).Count() > 0;

            else
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
        }

        private bool NoMoreEnemies(List<Character> enemies) => enemies.Where(x => x.Hitpoint > 0).Count() == 0;

        private void SaveCurrentWarriorHitPoints()
        {
            Character hero = Character.Protagonist;

            if (String.IsNullOrEmpty(hero.Name))
                return;

            if (hero.Name == "Тибо")
                hero.ThibautHitpoint = hero.Hitpoint;

            else if (hero.Name == "Ксолотл")
                hero.XolotlHitpoint = hero.Hitpoint;

            else if (hero.Name == "Серж")
                hero.SergeHitpoint = hero.Hitpoint;

            else
                hero.SouhiHitpoint = hero.Hitpoint;
        }

        private bool SetCurrentWarrior(ref List<string> fight, bool fightStart = false)
        {
            Character hero = Character.Protagonist;

            if (hero.Hitpoint > 3)
                return true;

            SaveCurrentWarriorHitPoints();

            if (hero.ThibautHitpoint > 3)
            {
                hero.Name = "Тибо";
                hero.Skill = hero.ThibautSkill;
                hero.Hitpoint = hero.ThibautHitpoint;
            }
            else if (hero.XolotlHitpoint > 3)
            {
                hero.Name = "Ксолотл";
                hero.Skill = hero.XolotlSkill;
                hero.Hitpoint = hero.XolotlHitpoint;
            }
            else if (hero.SergeHitpoint > 3)
            {
                hero.Name = "Серж";
                hero.Skill = hero.SergeSkill;
                hero.Hitpoint = hero.SergeHitpoint;
            }
            else if (hero.SouhiHitpoint > 3)
            {
                hero.Name = "Суи";
                hero.Skill = hero.SouhiSkill;
                hero.Hitpoint = hero.SouhiHitpoint;
            }
            else
                return false;

            if (!fightStart)
                fight.Add(String.Empty);

            fight.Add(String.Format("BOLD|В бой вступает {0}", hero.Name));

            if (fightStart)
                fight.Add(String.Empty);

            return true;
        } 

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            int round = 1, enemyWounds = 0;

            SetCurrentWarrior(ref fight, fightStart: true);

            Character hero = Character.Protagonist;

            while (true)
            {
                fight.Add(String.Format("HEAD|Раунд: {0}", round));

                foreach (Character enemy in FightEnemies)
                {
                    if (enemy.Hitpoint <= 0)
                        continue;

                    Character enemyInFight = enemy;
                    fight.Add(String.Format("{0} (жизнь {1})", enemy.Name, enemy.Hitpoint));

                    int protagonistRollFirst = Game.Dice.Roll();
                    int protagonistRollSecond = Game.Dice.Roll();
                    int protagonistHitStrength = protagonistRollFirst + protagonistRollSecond + hero.Skill;

                    fight.Add(String.Format("{0}: мощность удара: {1} + {2} + {3} = {4}",
                        hero.Name, Game.Dice.Symbol(protagonistRollFirst), Game.Dice.Symbol(protagonistRollSecond),
                        hero.Skill, protagonistHitStrength));

                    int enemyRollFirst = Game.Dice.Roll();
                    int enemyRollSecond = Game.Dice.Roll();
                    int enemyHitStrength = enemyRollFirst + enemyRollSecond + enemy.Skill;

                    fight.Add(String.Format("{0}: мощность удара: {1} + {2} + {3} = {4}",
                        enemy.Name, Game.Dice.Symbol(enemyRollFirst), Game.Dice.Symbol(enemyRollSecond), enemy.Skill, enemyHitStrength));

                    if (protagonistHitStrength > enemyHitStrength)
                    {
                        fight.Add(String.Format("GOOD|{0} ранен", enemy.Name));

                        enemy.Hitpoint -= 2;
                        enemyWounds += 1;

                        bool enemyLost = NoMoreEnemies(FightEnemies);

                        if (enemyLost || ((WoundsToWin > 0) && (WoundsToWin <= enemyWounds)))
                        {
                            fight.Add(String.Empty);
                            fight.Add(String.Format("BIG|GOOD|Вы ПОБЕДИЛИ :)"));

                            SaveCurrentWarriorHitPoints();

                            return fight;
                        }
                    }
                    else if (protagonistHitStrength < enemyHitStrength)
                    {
                        fight.Add(String.Format("BAD|{0} ранил {1}", enemy.Name, hero.Name));

                        hero.Hitpoint -= 2;

                        if (!SetCurrentWarrior(ref fight))
                        {
                            fight.Add(String.Empty);
                            fight.Add(String.Format("BIG|BAD|Вы ПРОИГРАЛИ :("));
                            return fight;
                        }
                    }
                    else
                        fight.Add(String.Format("BOLD|Ничья в раунде"));

                    fight.Add(String.Empty);
                }

                round += 1;
            }
        }

        public List<string> Dinner()
        {
            Character.Protagonist.Food -= 1;

            Character.Protagonist.SouhiHitpoint += DinnerHitpointsBonus;
            Character.Protagonist.SergeHitpoint += DinnerHitpointsBonus;
            Character.Protagonist.ThibautHitpoint += DinnerHitpointsBonus;
            Character.Protagonist.XolotlHitpoint += DinnerHitpointsBonus;

            return new List<string> { "RELOAD" };
        }
    }
}
