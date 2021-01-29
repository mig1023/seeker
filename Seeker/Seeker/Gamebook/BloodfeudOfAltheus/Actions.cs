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

        private int UseGloryInFight(Character enemy, ref List<string> fight)
        {
            bool graveInjury = (Character.Protagonist.Health < 2);
            bool cantFightOtherwise = (Character.Protagonist.Strength + (graveInjury ? 6 : 12) < enemy.Defence);

            int availableGlory = (Character.Protagonist.Glory - Character.Protagonist.Shame);

            if (cantFightOtherwise && (availableGlory < 1))
            {
                fight.Add("Кажется, что положение безнадёжно...");
                return -1;
            }

            if (!cantFightOtherwise)
                return 0;

            int needGlory = (enemy.Defence - Character.Protagonist.Strength + (graveInjury ? 6 : 12) + 2);

            if (needGlory > availableGlory)
            {
                fight.Add("Не хватит очков Славы, чтобы что-то исправить...");
                return -1;
            }
            else
            {
                fight.Add("Вам придётся использовать Славу!");

                Character.Protagonist.Glory -= needGlory;
                return needGlory;
            }

        }

        private bool NoMoreEnemies(List<Character> enemies, bool noHealthy = false)
        {
            foreach (Character enemy in enemies)
            {
                if (!noHealthy && enemy.Health > 0)
                    return false;

                if (noHealthy && (enemy.Health > 1))
                    return false;
            }
               
            return true;
        }

        public List<string> Fight()
        {
            Dictionary<int, string> healthLine = new Dictionary<int, string>
            {
                [0] = "мертв",
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

                    fight.Add(String.Format("Вы: {0}ы, {1}: {2}", healthLine[hero.Health], enemy.Name, healthLine[enemy.Health]));

                    int protagonistRollFirst = Game.Dice.Roll();
                    int protagonistRollSecond = 0;
                    string secondRollLine = String.Empty;
                    bool autoFail = false;

                    int useGlory = UseGloryInFight(enemy, ref fight);
                    string useGloryLine = (useGlory > 0 ? String.Format(" + {0} Славы", useGlory) : String.Empty);

                    if ((hero.Health > 1) || NoMoreEnemies(FightEnemies, noHealthy: true))
                    {
                        protagonistRollSecond = Game.Dice.Roll();
                        secondRollLine = String.Format(" + {0}", Game.Dice.Symbol(protagonistRollSecond));
                        autoFail = (protagonistRollFirst + protagonistRollSecond) < 4;
                    }
                    else
                        autoFail = (protagonistRollFirst == 1);

                    bool autoHit = (protagonistRollFirst + protagonistRollSecond) > 10;

                    int heroStrength = Character.Protagonist.Strength;
                    int protagonistHitStrength = protagonistRollFirst + protagonistRollSecond + useGlory + heroStrength;

                    fight.Add(String.Format("Мощность вашего удара: {0}{1} + {2}{3} = {4}",
                        Game.Dice.Symbol(protagonistRollFirst), secondRollLine, heroStrength, useGloryLine, protagonistHitStrength
                    ));

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
                    int enemyRollSecond = 0;
                    string ememySecondRollLine = String.Empty;

                    if ((enemy.Health > 1) || NoMoreEnemies(FightEnemies, noHealthy: true))
                    {
                        enemyRollSecond = Game.Dice.Roll();
                        ememySecondRollLine = String.Format(" + {0}", Game.Dice.Symbol(enemyRollSecond));
                        autoFail = (enemyRollFirst + enemyRollSecond) < 4;
                    }
                    else
                        autoFail = (enemyRollFirst == 1);

                    autoHit = (enemyRollFirst + enemyRollSecond) > 10;

                    int enemyHitStrength = enemyRollFirst + enemyRollSecond + enemy.Strength;

                    fight.Add(String.Format("Мощность его удара: {0}{1} + {2} = {3}",
                        Game.Dice.Symbol(enemyRollFirst), ememySecondRollLine, enemy.Strength, enemyHitStrength
                    ));

                    bool enemyWin = false;

                    if ((autoHit || (enemyHitStrength > hero.Defence)) && !autoFail)
                    {
                        fight.Add(String.Format("BAD|{0} ранил вас", enemy.Name));

                        hero.Health -= 1;

                        if (hero.Health <= 0)
                            enemyWin = true;
                    }
                    else
                        fight.Add(String.Format("BOLD|Противник не смог ранить вас", enemy.Name));

                    if (enemyWin || (useGlory < 0))
                    {
                        fight.Add(String.Empty);
                        fight.Add(String.Format("BIG|BAD|Вы ПРОИГРАЛИ :("));
                        return fight;
                    }

                    fight.Add(String.Empty);
                }

                round += 1;
            }
        }
    }
}
