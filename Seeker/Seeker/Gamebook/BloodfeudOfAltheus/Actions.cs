using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.BloodfeudOfAltheus
{
    class Actions : Abstract.IActions
    {
        public string ActionName { get; set; }
        public string ButtonName { get; set; }
        public string Aftertext { get; set; }
        public string Trigger { get; set; }
        public int Dices { get; set; }

        public List<Character> Enemies { get; set; }


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
                enemies.Add(String.Format("{0}\nсила {1}  защита {2}", enemy.Name.ToUpper(), enemy.Strength, enemy.Defence));

            return enemies;
        }

        public List<string> Status()
        {
            List<string> statusLines = new List<string>
            {
                String.Format("Сила: {0}", Character.Protagonist.Strength),
                String.Format("Защита: {0}", Character.Protagonist.Defence),
                String.Format("Слава: {0}", Character.Protagonist.Glory),
                String.Format("Позор: {0}", Character.Protagonist.Shame),
            };

            return statusLines;
        }

        public List<string> AdditionalStatus()
        {
            List<string> statusLines = new List<string>();

            statusLines.Add(String.Format("Оружие: {0}", Character.Protagonist.WeaponName));
            statusLines.Add(String.Format("Покровитель: {0}", Character.Protagonist.Patron));

            if (statusLines.Count <= 0)
                return null;

            return statusLines;
        }

        public List<string> StaticButtons() => new List<string> { };

        public bool StaticAction(string action) => false;

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = "Позор Альтея невыносим, лучше начать сначала";
            
            return Character.Protagonist.Shame > Character.Protagonist.Glory;
        }

        public bool IsButtonEnabled() => true;

        public static bool CheckOnlyIf(string option)
        {
            if (option == "selectOnly")
                return true;

            if (option.Contains("!"))
            {
                if (Game.Data.Triggers.Contains(option.Replace("!", String.Empty).Trim()))
                    return false;
            }
            else if (!Game.Data.Triggers.Contains(option.Trim()))
                return false;

            return true;
        }

        private bool NoMoreEnemies(List<Character> enemies)
        {
            foreach (Character enemy in enemies)
                if (enemy.Health > 0)
                    return false;

            return true;
        }

        public List<string> Fight()
        {
            Dictionary<int, string> healthLine = new Dictionary<int, string>
            {
                [0] = "мёртв",
                [1] = "тяжело ранен",
                [2] = "ранен",
                [3] = "здоров",
            };

            List<string> fight = new List<string>();

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            int round = 1;

            Character hero = Character.Protagonist;

            hero.Health = 3;

            while (true)
            {
                fight.Add(String.Format("HEAD|Раунд: {0}", round));

                foreach (Character enemy in FightEnemies)
                {
                    if (enemy.Health <= 0) 
                        continue;

                    fight.Add(String.Format("Вы: {0}, {1}: {2}", healthLine[hero.Health], enemy.Name, healthLine[enemy.Health]));

                    int protagonistRollFirst = Game.Dice.Roll();
                    int protagonistRollSecond = Game.Dice.Roll();
                    int heroStrength = Character.Protagonist.Strength;
                    int protagonistHitStrength = protagonistRollFirst + protagonistRollSecond + heroStrength;

                    fight.Add(String.Format("Мощность вашего удара: {0} + {1} + {2} = {3}",
                        Game.Dice.Symbol(protagonistRollFirst), Game.Dice.Symbol(protagonistRollSecond), heroStrength, protagonistHitStrength
                    ));

                    bool autoHit = (protagonistRollFirst + protagonistRollSecond) > 10;
                    bool autoFail = (protagonistRollFirst + protagonistRollSecond) < 4;

                    if ((autoHit || (protagonistHitStrength > enemy.Defence)) && !autoFail)
                    {
                        fight.Add(String.Format("GOOD|{0} ранен", enemy.Name));

                        enemy.Health -= 1;

                        bool enemyLost = NoMoreEnemies(FightEnemies);

                        if (enemyLost)
                        {
                            fight.Add(String.Empty);
                            fight.Add(String.Format("BIG|GOOD|Вы ПОБЕДИЛИ :)"));
                            return fight;
                        }
                    }
                    else
                        fight.Add(String.Format("BOLD|Вы не смогли ранить противника", enemy.Name));

                    int enemyRollFirst = Game.Dice.Roll();
                    int enemyRollSecond = Game.Dice.Roll();
                    int enemyHitStrength = enemyRollFirst + enemyRollSecond + enemy.Strength;

                    fight.Add(String.Format("Мощность его удара: {0} + {1} + {2} = {3}",
                        Game.Dice.Symbol(enemyRollFirst), Game.Dice.Symbol(enemyRollSecond), enemy.Strength, enemyHitStrength
                    ));

                    autoHit = (enemyRollFirst + enemyRollSecond) > 10;
                    autoFail = (enemyRollFirst + enemyRollSecond) < 4;

                    if ((autoHit || (protagonistHitStrength < enemyHitStrength)) && !autoFail)
                    {
                        fight.Add(String.Format("BAD|{0} ранил вас", enemy.Name));

                        hero.Health -= 1;

                        if (hero.Health <= 0)
                        {
                            fight.Add(String.Empty);
                            fight.Add(String.Format("BIG|BAD|Вы ПРОИГРАЛИ :("));
                            return fight;
                        }
                    }
                    else
                        fight.Add(String.Format("BOLD|Противник не смог ранить вас", enemy.Name));

                    fight.Add(String.Empty);
                }

                round += 1;
            }
        }
    }
}
