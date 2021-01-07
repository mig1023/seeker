using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.OctopusIsland
{
    class Actions : Abstract.IActions
    {
        public string ActionName { get; set; }
        public string ButtonName { get; set; }
        public string Aftertext { get; set; }
        public string Trigger { get; set; }

        public List<Character> Enemies { get; set; }

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
            List<string> enemies = new List<string>();

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
                enemies.Add(String.Format("{0}\nловкость {1}  жизни {2}", enemy.Name, enemy.Skill, enemy.Hitpoint));

            return enemies;
        }

        public List<string> Status()
        {
            List<string> statusLines = new List<string>
            {
                String.Format("Серж: {0}/{1}", Character.Protagonist.SergeSkill, Character.Protagonist.SergeHitpoint),
                String.Format("Ксалотл: {0}/{1}", Character.Protagonist.XolotlSkill, Character.Protagonist.XolotlHitpoint),
                String.Format("Тибо: {0}/{1}", Character.Protagonist.ThibautSkill, Character.Protagonist.ThibautHitpoint),
                String.Format("Суи: {0}/{1}", Character.Protagonist.SouhiSkill, Character.Protagonist.SouhiHitpoint),
            };

            return statusLines;
        }

        public List<string> StaticButtons() => new List<string> { };

        public bool StaticAction(string action) => false;

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = String.Empty;

            return false;
        }

        public bool IsButtonEnabled() => true;

        public static bool CheckOnlyIf(string option)
        {
            if (option.Contains("|"))
            {
                string[] options = option.Split('|');

                foreach (string oneOption in options)
                    if (Game.Data.Triggers.Contains(oneOption.Trim()))
                        return true;

                return false;
            }
            else
            {
                string[] options = option.Split(',');

                foreach (string oneOption in options)
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

        private bool NoMoreEnemies(List<Character> enemies)
        {
            foreach (Character enemy in enemies)
                if (enemy.Hitpoint > 0)
                    return false;

            return true;
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            int round = 1;

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
                    int heroSkill = Character.Protagonist.ThibautSkill;
                    int protagonistHitStrength = protagonistRollFirst + protagonistRollSecond + heroSkill;

                    fight.Add(String.Format("Мощность вашего удара: {0} ⚄ + {1} ⚄ + {2} = {3}",
                        protagonistRollFirst, protagonistRollSecond, heroSkill, protagonistHitStrength
                    ));

                    int enemyRollFirst = Game.Dice.Roll();
                    int enemyRollSecond = Game.Dice.Roll();
                    int enemyHitStrength = enemyRollFirst + enemyRollSecond + enemy.Skill;

                    fight.Add(String.Format("Мощность его удара: {0} ⚄ + {1} ⚄ + {1} = {2}",
                        enemyRollFirst, enemyRollSecond, enemy.Skill, enemyHitStrength
                    ));

                    if (protagonistHitStrength > enemyHitStrength)
                    {
                        fight.Add(String.Format("GOOD|{0} ранен", enemy.Name));

                        enemy.Hitpoint -= 2;

                        if (enemy.Hitpoint <= 0)
                            enemy.Hitpoint = 0;

                        bool enemyLost = NoMoreEnemies(FightEnemies);

                        if (enemyLost)
                        {
                            fight.Add(String.Empty);
                            fight.Add(String.Format("BIG|GOOD|Вы ПОБЕДИЛИ :)"));
                            return fight;
                        }
                    }
                    else if (protagonistHitStrength < enemyHitStrength)
                    {
                        fight.Add(String.Format("BAD|{0} ранил вас", enemy.Name));

                        hero.ThibautHitpoint -= 2;

                        if (hero.ThibautHitpoint < 0)
                            hero.ThibautHitpoint = 0;

                        if (hero.ThibautHitpoint <= 0)
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
    }
}
