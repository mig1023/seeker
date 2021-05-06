using System;
using System.Collections.Generic;
using System.Text;


namespace Seeker.Gamebook.StringOfWorlds
{
    class Actions : Abstract.IActions
    {
        public string ActionName { get; set; }
        public string ButtonName { get; set; }
        public string Aftertext { get; set; }
        public string Trigger { get; set; }
        public int RoundsToWin { get; set; }
        public bool HeroWoundsLimit { get; set; }
        public bool EnemyWoundsLimit { get; set; }
        public bool DevastatingAttack { get; set; }
        public bool DarknessPenalty { get; set; }
        public string Equipment { get; set; }

        public List<Character> Enemies { get; set; }

        public string Text { get; set; }
        public Abstract.IModification Benefit { get; set; }
 

        public List<string> Do(out bool reload, string action = "", bool trigger = false)
        {
            if (trigger)
                Game.Option.Trigger(Trigger);

            string actionName = (String.IsNullOrEmpty(action) ? ActionName : action);
            List<string> actionResult = typeof(Actions).GetMethod(actionName).Invoke(this, new object[] { }) as List<string>;

            reload = (actionResult.Count >= 1) && (actionResult[0] == "RELOAD");

            return actionResult;
        }

        public List<string> Status()
        {
            List<string> statusLines = new List<string>
            {
                String.Format("Ловкость: {0}", Character.Protagonist.Skill),
                String.Format("Сила: {0}", Character.Protagonist.Strength),
                String.Format("Обаяние: {0}", Character.Protagonist.Charm),
            };

            return statusLines;
        }

        public List<string> AdditionalStatus() => null;

        public List<string> StaticButtons()
        {
            List<string> staticButtons = new List<string> { };

            if (Constants.GetParagraphsWithoutStaticsButtons().Contains(Game.Data.CurrentParagraphID))
                return staticButtons;

            if ((Character.Protagonist.Equipment == "Тюбик") && (Character.Protagonist.Strength < Character.Protagonist.MaxStrength))
                staticButtons.Add("СЪЕСТЬ ПАСТУ");

            return staticButtons;
        }

        public bool StaticAction(string action)
        {
            if (action == "СЪЕСТЬ ПАСТУ")
            {
                Character.Protagonist.Equipment = String.Empty;
                Character.Protagonist.Strength = Character.Protagonist.MaxStrength;
                return true;
            }

            return false;
        }

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = "Начать сначала";

            return Character.Protagonist.Strength <= 0;
        }

        public bool IsButtonEnabled() => !(!String.IsNullOrEmpty(Equipment) && !String.IsNullOrEmpty(Character.Protagonist.Equipment));

        public static bool CheckOnlyIf(string option)
        {
            string[] values = option.Split(new string[] { ">=", "<", " " }, StringSplitOptions.RemoveEmptyEntries);
            int level = (values.Length > 1 ? int.Parse(values[1]) : 0);

            if (option.Contains("БЛАСТЕР >="))
                return level <= Character.Protagonist.Blaster;

            else if (option.Contains("БЛАСТЕР <"))
                return level > Character.Protagonist.Blaster;

            else if (option.Contains("ОЧКИ"))
                return Character.Protagonist.Equipment == "Очки";

            else if (option.Contains("ЗАЖИГАЛКА"))
                return Character.Protagonist.Equipment == "Зажигалка";

            else if (option.Contains("!"))
                return !Game.Data.Triggers.Contains(option.Replace("!", String.Empty).Trim());

            else
                return Game.Data.Triggers.Contains(option);
        }

        public List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (!String.IsNullOrEmpty(Text))
                return new List<string> { Text };

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
                enemies.Add(String.Format("{0}\nловкость {1}  сила {2}", enemy.Name, enemy.Skill, enemy.Strength));

            return enemies;
        }

        private string LuckNumbers()
        {
            string luckListShow = String.Empty;

            for (int i = 1; i < 7; i++)
                luckListShow += String.Format("{0} ", Character.Protagonist.Luck[i] ? Constants.LuckList()[i] : Constants.LuckList()[i + 10]);

            return luckListShow;
        }

        public List<string> Luck()
        {
            List<string> luckCheck = new List<string>
            {
                "Цифры удачи:",
                "BIG|" + LuckNumbers()
            };

            int goodLuck = Game.Dice.Roll();

            luckCheck.Add(String.Format("Проверка удачи: {0} - {1}зачёркунтый",
                Game.Dice.Symbol(goodLuck), (Character.Protagonist.Luck[goodLuck] ? "не " : String.Empty)));

            luckCheck.Add(Character.Protagonist.Luck[goodLuck] ? "BIG|GOOD|УСПЕХ :)" : "BIG|BAD|НЕУДАЧА :(");

            Character.Protagonist.Luck[goodLuck] = !Character.Protagonist.Luck[goodLuck];

            return luckCheck;
        }

        public List<string> LuckRecovery()
        {
            List<string> luckRecovery = new List<string> { "Восстановление удачи:" };

            bool success = false;

            for (int i = 1; i < 7; i++)
                if (!Character.Protagonist.Luck[i])
                {
                    luckRecovery.Add(String.Format("GOOD|Цифра {0} восстановлена!", i));
                    Character.Protagonist.Luck[i] = true;
                    success = true;

                    break;
                }

            if (!success)
                luckRecovery.Add("BAD|Все цифры и так счастливые!");

            luckRecovery.Add("Цифры удачи теперь:");
            luckRecovery.Add("BIG|" + LuckNumbers());

            return luckRecovery;
        }

        public List<string> Charm()
        {
            int fisrtDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();

            bool goodCharm = (fisrtDice + secondDice) <= Character.Protagonist.Charm;

            List<string> luckCheck = new List<string> { String.Format(
                "Проверка обаяния: {0} + {1} {2} {3}",
                Game.Dice.Symbol(fisrtDice), Game.Dice.Symbol(secondDice), (goodCharm ? "<=" : ">"), Character.Protagonist.Charm) };

            if (goodCharm)
            {
                luckCheck.Add("BIG|GOOD|УСПЕХ :)");
                luckCheck.Add("Вы увеличили своё обаяние на единицу");

                Character.Protagonist.Charm += 1;
            }
            else
            {
                luckCheck.Add("BIG|BAD|НЕУДАЧА :(");

                if (Character.Protagonist.Charm > 2)
                {
                    luckCheck.Add("Вы уменьшили своё обаяние на единицу");
                    Character.Protagonist.Charm -= 1;
                }
            }

            return luckCheck;
        }

        public List<string> Skill()
        {
            int fisrtDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();

            bool goodSkill = (fisrtDice + secondDice) <= Character.Protagonist.Skill;

            List<string> luckCheck = new List<string> { String.Format(
                "Проверка ловкости: {0} + {1} {2} {3}",
                Game.Dice.Symbol(fisrtDice), Game.Dice.Symbol(secondDice), (goodSkill ? "<=" : ">"), Character.Protagonist.Skill) };

            luckCheck.Add(goodSkill ? "BIG|GOOD|УСПЕХ :)" : "BIG|BAD|НЕУДАЧА :(");

            return luckCheck;
        }

        public List<string> DiceWounds()
        {
            List<string> diceCheck = new List<string> { };

            int dices = 0;

            for (int i = 1; i <= 2; i++)
            {
                int dice = Game.Dice.Roll();
                dices += dice;
                diceCheck.Add(String.Format("На {0} выпало: {1}", i, Game.Dice.Symbol(dice)));
            }

            Character.Protagonist.Strength -= dices;

            diceCheck.Add(String.Format("BIG|BAD|Вы потеряли жизней: {0}", dices));

            return diceCheck;
        }
        
        public List<string> GameOfDice()
        {
            List<string> diceGame = new List<string> { };

            int myResult, enemyResult;

            do
            {
                int firstDice = Game.Dice.Roll();
                int secondDice = Game.Dice.Roll();
                myResult = firstDice + secondDice;

                diceGame.Add(String.Format(
                    "Вы бросили: {0} + {1} = {2}",
                    Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), myResult
                ));

                int hisFirstDice = Game.Dice.Roll();
                int hisSecondDice = Game.Dice.Roll();
                enemyResult = hisFirstDice + hisSecondDice;

                diceGame.Add(String.Format(
                    "Он бросил: {0} + {1} = {2}",
                    Game.Dice.Symbol(hisFirstDice), Game.Dice.Symbol(hisSecondDice), enemyResult
                ));

                diceGame.Add(String.Empty);
            }
            while (myResult == enemyResult);

            diceGame.Add(myResult > enemyResult ? "BIG|GOOD|ВЫИГРАЛИ :)" : "BIG|BAD|ПРОИГРАЛИ :(");

            return diceGame;
        }

        public List<string> Break()
        {
            List<string> breakingDoor = new List<string> { "Ломаете дверь:" };

            bool succesBreaked = false;

            while (!succesBreaked && (Character.Protagonist.Strength > 0))
            {
                int firstDice = Game.Dice.Roll();
                int secondDice = Game.Dice.Roll();

                if (firstDice == secondDice)
                    succesBreaked = true;
                else
                    Character.Protagonist.Strength -= 1;

                string result = (succesBreaked ? "удачный, дверь поддалась!" : "неудачный, -1 сила");

                breakingDoor.Add(String.Format(
                    "Удар: {0} + {1} = {2}",
                    Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), result));
            }

            breakingDoor.Add(succesBreaked ? "BIG|GOOD|ДВЕРЬ ВЗЛОМАНА :)" : "BIG|BAD|ВЫ УБИЛИСЬ ОБ ДВЕРЬ :(");

            return breakingDoor;
        }

        public List<string> Get()
        {
            if (!String.IsNullOrEmpty(Equipment))
                Character.Protagonist.Equipment = Equipment;

            return new List<string> { "RELOAD" };
        }

        private bool NoMoreEnemies(List<Character> enemies)
        {
            foreach (Character enemy in enemies)
                if (enemy.Strength > (EnemyWoundsLimit ? 2 : 0))
                    return false;

            return true;
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            int round = 1, skillPenalty = 0;

            if (DarknessPenalty && (Character.Protagonist.Equipment != "Очки"))
                skillPenalty += 1;

            Character hero = Character.Protagonist;

            while (true)
            {
                fight.Add(String.Format("HEAD|Раунд: {0}", round));

                bool attackAlready = false;
                int protagonistHitStrength = 0;

                foreach (Character enemy in FightEnemies)
                {
                    if (enemy.Strength <= 0)
                        continue;

                    fight.Add(String.Format("{0} (сила {1})", enemy.Name, enemy.Strength));

                    if (!attackAlready)
                    {
                        int protagonistRollFirst = Game.Dice.Roll();
                        int protagonistRollSecond = Game.Dice.Roll();
                        int heroSkill = (hero.Skill - skillPenalty);
                        protagonistHitStrength = protagonistRollFirst + protagonistRollSecond + heroSkill;

                        fight.Add(String.Format("Мощность вашего удара: {0} + {1} + {2} = {3}",
                            Game.Dice.Symbol(protagonistRollFirst), Game.Dice.Symbol(protagonistRollSecond),
                            heroSkill, protagonistHitStrength));
                    }

                    int enemyRollFirst = Game.Dice.Roll();
                    int enemyRollSecond = Game.Dice.Roll();
                    int enemyHitStrength = enemyRollFirst + enemyRollSecond + enemy.Skill;

                    fight.Add(String.Format("Мощность его удара: {0} + {1} + {2} = {3}",
                        Game.Dice.Symbol(enemyRollFirst), Game.Dice.Symbol(enemyRollSecond), enemy.Skill, enemyHitStrength));

                    if ((protagonistHitStrength > enemyHitStrength) && !attackAlready)
                    {
                        fight.Add(String.Format("GOOD|{0} ранен", enemy.Name));

                        enemy.Strength -= 2;

                        bool enemyLost = NoMoreEnemies(FightEnemies);

                        if (enemyLost)
                        {
                            fight.Add(String.Empty);
                            fight.Add(String.Format("BIG|GOOD|Вы ПОБЕДИЛИ :)"));
                            return fight;
                        }
                    }
                    else if (protagonistHitStrength > enemyHitStrength)
                    {
                        fight.Add(String.Format("BOLD|{0} не смог вас ранить", enemy.Name));
                    }
                    else if (protagonistHitStrength < enemyHitStrength)
                    {
                        fight.Add(String.Format("BAD|{0} ранил вас", enemy.Name));

                        hero.Strength -= (DevastatingAttack ? 3 : 2);

                        if ((hero.Strength <= 0) || (HeroWoundsLimit && (hero.Strength <= 2)))
                        {
                            fight.Add(String.Empty);
                            fight.Add(String.Format("BIG|BAD|Вы ПРОИГРАЛИ :("));
                            return fight;
                        }
                    }
                    else
                        fight.Add(String.Format("BOLD|Ничья в раунде"));

                    attackAlready = true;

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

        public bool IsHealingEnabled() => false;

        public void UseHealing(int healingLevel) => Game.Other.DoNothing();

        public string TextByOptions(string option) => String.Empty;
    }
}
