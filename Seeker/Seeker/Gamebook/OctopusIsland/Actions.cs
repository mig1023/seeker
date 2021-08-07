using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.OctopusIsland
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protogonist = Character.Protagonist;

        public List<Character> Enemies { get; set; }
        public int WoundsToWin { get; set; }
        public int DinnerHitpointsBonus { get; set; }
        public bool DinnerAlready { get; set; }

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
            String.Format("Обедов: {0}", protogonist.Food),
            String.Format("Животворная мазь: {0}", protogonist.LifeGivingOintment),
        };

        public override List<string> AdditionalStatus() => new List<string>
        {
            String.Format("Серж: {0}/{1}", protogonist.SergeSkill, protogonist.SergeHitpoint),
            String.Format("Ксолотл: {0}/{1}", protogonist.XolotlSkill, protogonist.XolotlHitpoint),
            String.Format("Тибо: {0}/{1}", protogonist.ThibautSkill, protogonist.ThibautHitpoint),
            String.Format("Суи: {0}/{1}", protogonist.SouhiSkill, protogonist.SouhiHitpoint),
        };

        public override List<string> StaticButtons()
        {
            List<string> staticButtons = new List<string> { };

            if (protogonist.LifeGivingOintment <= 0)
                return staticButtons;

            if (protogonist.SergeHitpoint < 20)
                staticButtons.Add("ВЫЛЕЧИТЬ СЕРЖА");

            if (protogonist.XolotlHitpoint < 20)
                staticButtons.Add("ВЫЛЕЧИТЬ КСОЛОТЛА");

            if (protogonist.ThibautHitpoint < 20)
                staticButtons.Add("ВЫЛЕЧИТЬ ТИБО");

            if (protogonist.SouhiHitpoint < 20)
                staticButtons.Add("ВЫЛЕЧИТЬ СУИ");

            return staticButtons;
        }

        private int LifeGivingOintmentFor(int protagonistHitpoint)
        {
            while ((protogonist.LifeGivingOintment > 0) && (protagonistHitpoint < 20))
            {
                protogonist.LifeGivingOintment -= 1;
                protagonistHitpoint += 1;
            }

            return protagonistHitpoint;
        }

        public override bool StaticAction(string action)
        {
            if (action.Contains("СЕРЖА"))
                protogonist.SergeHitpoint = LifeGivingOintmentFor(protogonist.SergeHitpoint);

            else if (action.Contains("КСОЛОТЛА"))
                protogonist.XolotlHitpoint = LifeGivingOintmentFor(protogonist.XolotlHitpoint);

            else if (action.Contains("ТИБО"))
                protogonist.ThibautHitpoint = LifeGivingOintmentFor(protogonist.ThibautHitpoint);

            else if (action.Contains("СУИ"))
                protogonist.SouhiHitpoint = LifeGivingOintmentFor(protogonist.SouhiHitpoint);

            else
                return false;

            return true;
        }

        public override bool IsButtonEnabled() => !((DinnerHitpointsBonus > 0) && ((protogonist.Food <= 0) || DinnerAlready));

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
            Character protagonist = protogonist;

            if (String.IsNullOrEmpty(protagonist.Name))
                return;

            if (protagonist.Name == "Тибо")
                protagonist.ThibautHitpoint = protagonist.Hitpoint;

            else if (protagonist.Name == "Ксолотл")
                protagonist.XolotlHitpoint = protagonist.Hitpoint;

            else if (protagonist.Name == "Серж")
                protagonist.SergeHitpoint = protagonist.Hitpoint;

            else
                protagonist.SouhiHitpoint = protagonist.Hitpoint;
        }

        private bool SetCurrentWarrior(ref List<string> fight, bool fightStart = false)
        {
            Character protagonist = protogonist;

            if (protagonist.Hitpoint > 3)
                return true;

            SaveCurrentWarriorHitPoints();

            if (protagonist.ThibautHitpoint > 3)
            {
                protagonist.Name = "Тибо";
                protagonist.Skill = protagonist.ThibautSkill;
                protagonist.Hitpoint = protagonist.ThibautHitpoint;
            }
            else if (protagonist.XolotlHitpoint > 3)
            {
                protagonist.Name = "Ксолотл";
                protagonist.Skill = protagonist.XolotlSkill;
                protagonist.Hitpoint = protagonist.XolotlHitpoint;
            }
            else if (protagonist.SergeHitpoint > 3)
            {
                protagonist.Name = "Серж";
                protagonist.Skill = protagonist.SergeSkill;
                protagonist.Hitpoint = protagonist.SergeHitpoint;
            }
            else if (protagonist.SouhiHitpoint > 3)
            {
                protagonist.Name = "Суи";
                protagonist.Skill = protagonist.SouhiSkill;
                protagonist.Hitpoint = protagonist.SouhiHitpoint;
            }
            else
                return false;

            if (!fightStart)
                fight.Add(String.Empty);

            fight.Add(String.Format("BOLD|В бой вступает {0}", protagonist.Name));

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

            Character protagonist = protogonist;

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
                    int protagonistHitStrength = protagonistRollFirst + protagonistRollSecond + protagonist.Skill;

                    fight.Add(String.Format("{0}: мощность удара: {1} + {2} + {3} = {4}",
                        protagonist.Name, Game.Dice.Symbol(protagonistRollFirst), Game.Dice.Symbol(protagonistRollSecond),
                        protagonist.Skill, protagonistHitStrength));

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
                        fight.Add(String.Format("BAD|{0} ранил {1}", enemy.Name, protagonist.Name));

                        protagonist.Hitpoint -= 2;

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
            protogonist.Food -= 1;

            protogonist.SouhiHitpoint += DinnerHitpointsBonus;
            protogonist.SergeHitpoint += DinnerHitpointsBonus;
            protogonist.ThibautHitpoint += DinnerHitpointsBonus;
            protogonist.XolotlHitpoint += DinnerHitpointsBonus;

            DinnerAlready = true;

            return new List<string> { "RELOAD" };
        }
    }
}
