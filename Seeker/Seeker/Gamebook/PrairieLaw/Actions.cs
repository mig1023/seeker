using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.PrairieLaw
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public List<Character> Enemies { get; set; }
        public bool Firefight { get; set; }
        public bool HeroWoundsLimit { get; set; }
        public bool EnemyWoundsLimit { get; set; }

        public string Text { get; set; }
        public int Dices { get; set; }
        
        public Abstract.IModification Benefit { get; set; }

        public override List<string> Status() => new List<string>
        {
            String.Format("Ловкость: {0}", Character.Protagonist.Skill),
            String.Format("Сила: {0}", Character.Protagonist.Strength),
            String.Format("Обаяние: {0}", Character.Protagonist.Charm),
        };


        public override List<string> AdditionalStatus() => new List<string>
        {
            String.Format("Патронов: {0}", Character.Protagonist.Cartridges),
            String.Format("Долларов: {0}", Character.Protagonist.Dollars),
        };

        public override bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = "Начать сначала";

            return Character.Protagonist.Strength <= 0;
        }

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (!String.IsNullOrEmpty(Text))
                return new List<string> { Text };

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
            {
                string line = String.Format("{0}\nловкость {1}  сила {2}", enemy.Name, enemy.Skill, enemy.Strength);

                if (Firefight)
                    line += String.Format("  патроны {0}", enemy.Cartridges);

                enemies.Add(line);
            }

            return enemies;
        }

        public static bool CheckOnlyIf(string option)
        {
            if (option.Contains("ДОЛЛАРЫ >="))
                return int.Parse(option.Split('=')[1]) <= Character.Protagonist.Dollars;

            else
                return Game.Data.Triggers.Contains(option);
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

            int dicesCount = (Dices == 0 ? 1 : Dices);
            int dices = 0;

            for (int i = 1; i <= dicesCount; i++)
            {
                int dice = Game.Dice.Roll();
                dices += dice;
                diceCheck.Add(String.Format("На {0} выпало: {1}", i, Game.Dice.Symbol(dice)));
            }

            Character.Protagonist.Strength -= dices;

            diceCheck.Add(String.Format("BIG|BAD|Вы потеряли жизней: {0}", dices));

            return diceCheck;
        }

        private bool NoMoreEnemies(List<Character> enemies)
        {
            foreach (Character enemy in enemies)
                if (enemy.Strength > (EnemyWoundsLimit ? 2 : 0))
                    return false;

            return true;
        }

        private bool FirefightContinue(List<Character> enemies, ref List<string> fight, bool firefight)
        {
            if (Character.Protagonist.Cartridges > 0)
                return true;

            foreach (Character enemy in enemies)
                if (enemy.Cartridges > 0)
                    return true;

            if (firefight)
            {
                fight.Add("BOLD|У всех закончились патроны, дальше бой продолжится на кулаках");
                fight.Add(String.Empty);
            }

            return false;
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            int round = 1;
            bool firefight = Firefight;

            Character hero = Character.Protagonist;

            while (true)
            {
                firefight = FirefightContinue(FightEnemies, ref fight, firefight);

                fight.Add(String.Format("HEAD|Раунд: {0}", round));

                bool attackAlready = false;
                int heroHitStrength = 0, enemyHitStrength = 0;

                foreach (Character enemy in FightEnemies)
                {
                    if (enemy.Strength <= 0)
                        continue;

                    string cartridgesLine = (enemy.Cartridges > 0 ? String.Format(", патронов {0}", enemy.Cartridges) : String.Empty);
                    fight.Add(String.Format("{0} (сила {1}{2})", enemy.Name, enemy.Strength, cartridgesLine));

                    bool noCartridges = Character.Protagonist.Cartridges <= 0;

                    if (!attackAlready && (!firefight || !noCartridges))
                    {
                        int heroRollFirst = Game.Dice.Roll();
                        int heroRollSecond = Game.Dice.Roll();
                        heroHitStrength = heroRollFirst + heroRollSecond + hero.Skill;

                        string heroHitLine = (firefight ? "Ваш выстрел" : "Мощность вашего удара");

                        fight.Add(String.Format("{0}: {1} + {2} + {3} = {4}",
                            heroHitLine, Game.Dice.Symbol(heroRollFirst), Game.Dice.Symbol(heroRollSecond), hero.Skill, heroHitStrength));

                        if (firefight)
                            Character.Protagonist.Cartridges -= 1;
                    }

                    if (!firefight || (enemy.Cartridges > 0))
                    {
                        int enemyRollFirst = Game.Dice.Roll();
                        int enemyRollSecond = Game.Dice.Roll();
                        enemyHitStrength = enemyRollFirst + enemyRollSecond + enemy.Skill;

                        string enemyHitLine = (firefight ? "Его выстрел" : "Мощность его удара");

                        fight.Add(String.Format("{0}: {1} + {2} + {3} = {4}",
                            enemyHitLine, Game.Dice.Symbol(enemyRollFirst), Game.Dice.Symbol(enemyRollSecond), enemy.Skill, enemyHitStrength));

                        if (firefight)
                            enemy.Cartridges -= 1;
                    }
                    else
                        enemyHitStrength = 0;

                    if ((heroHitStrength == 0) && (enemyHitStrength == 0))
                    { 
                        // nothing to do here
                    }
                    else if ((heroHitStrength > enemyHitStrength) && !attackAlready)
                    {
                        fight.Add(String.Format("GOOD|{0} ранен", enemy.Name));

                        enemy.Strength -= (firefight ? 3 : 2);

                        bool enemyLost = NoMoreEnemies(FightEnemies);

                        if (enemyLost)
                        {
                            fight.Add(String.Empty);
                            fight.Add(String.Format("BIG|GOOD|Вы ПОБЕДИЛИ :)"));
                            return fight;
                        }
                    }
                    else if (heroHitStrength > enemyHitStrength)
                    {
                        fight.Add(String.Format("BOLD|{0} не смог вас ранить", enemy.Name));
                    }
                    else if (heroHitStrength < enemyHitStrength)
                    {
                        fight.Add(String.Format("BAD|{0} ранил вас", enemy.Name));

                        hero.Strength -= (firefight ? 3 : 2);

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

                    fight.Add(String.Empty);
                }

                round += 1;
            }
        }

        public override bool IsHealingEnabled() => Character.Protagonist.Strength < Character.Protagonist.MaxStrength;

        public override void UseHealing(int healingLevel)
        {
            if (healingLevel == -1)
                Character.Protagonist.Strength = Character.Protagonist.MaxStrength;
            else
                Character.Protagonist.Strength += healingLevel;
        }
    }
}
