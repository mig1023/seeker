using System;
using System.Collections.Generic;
using System.Text;


namespace Seeker.Gamebook.CaptainSheltonsSecret
{
    class Actions : Interfaces.IActions
    {
        public string ActionName { get; set; }
        public string ButtonName { get; set; }
        public string Aftertext { get; set; }
        public string OpenOption { get; set; }

        // Fight
        public List<Character> Allies { get; set; }
        public List<Character> Enemies { get; set; }
        public int RoundsToWin { get; set; }
        public int WoundsToWin { get; set; }
        public int DamageToWin { get; set; }
        public int SkillPenalty { get; set; }
        public bool GroupFight { get; set; }

        // Get
        public string Text { get; set; }
        public int Price { get; set; }
        public bool Used { get; set; }
        public bool Multiple { get; set; }

        public List<string> Do(out bool reload, string action = "", bool openOption = false)
        {
            if (openOption)
                Game.Option.OpenOption(OpenOption);

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
                String.Format("Обаяние: {0}", Character.Protagonist.Charm),
                String.Format("Золото: {0}", Character.Protagonist.Gold)
            };

            return statusLines;
        }

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = "Начать сначала";

            return (Character.Protagonist.Strength <= 0 ? true : false);
        }

        public bool IsButtonEnabled()
        {
            bool disabledGetOptions = (Price > 0) && Used;

            return !disabledGetOptions;
        }

        public static bool CheckOnlyIf(string option)
        {
            if (option.Contains("ЗОЛОТО >="))
                return int.Parse(option.Split('=')[1]) <= Character.Protagonist.Gold;
            else
                return Game.Data.OpenedOption.Contains(option);
        }

        public List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (ActionName == "Get")
            {
                string countMarker = String.Empty;

                return new List<string> { String.Format("{0}{1}", Text, countMarker) };
            }

            if (Enemies == null)
                return enemies;

            if ((Allies != null) && GroupFight)
            {
                foreach (Character ally in Allies)
                    enemies.Add(String.Format("{0}\nловкость {1}  сила {2}", ally.Name, ally.Skill, ally.GetStrength()));

                enemies.Add("SPLITTER|против");
            }
                

            foreach (Character enemy in Enemies)
                enemies.Add(String.Format("{0}\nловкость {1}  сила {2}", enemy.Name, enemy.Skill, enemy.GetStrength()));

            return enemies;
        }

        public List<string> Break()
        {
            List<string> breakingDoor = new List<string> { "Ломаете дверь:" };

            bool succesBreaked = false;

            while (!succesBreaked && (Character.Protagonist.Strength > 0))
            {
                int firstDice = Game.Dice.Roll();
                int secondDice = Game.Dice.Roll();

                if (((firstDice == 1) || (firstDice == 6)) && (firstDice == secondDice))
                    succesBreaked = true;
                else
                    Character.Protagonist.Strength -= 1;

                string result = (succesBreaked ? "удачный, дверь поддалась!" : "неудачный, -1 сила" );
                breakingDoor.Add(String.Format("Удар: {0} ⚄ + {1} ⚄ - {2}", firstDice, secondDice, result));
            }

            breakingDoor.Add(succesBreaked ? "BIG|GOOD|ДВЕРЬ ВЗЛОМАНА :)" : "BIG|BAD|ВЫ УБИЛИСЬ ОБ ДВЕРЬ :(");

            return breakingDoor;
        }

        public List<string> Luck()
        {
            Dictionary<int, string> luckList = new Dictionary<int, string>
            {
                [1] = "①",
                [2] = "②",
                [3] = "③",
                [4] = "④",
                [5] = "⑤",
                [6] = "⑥",

                [11] = "❶",
                [12] = "❷",
                [13] = "❸",
                [14] = "❹",
                [15] = "❺",
                [16] = "❻",
            };

            List<string> luckCheck = new List<string> { "Квадраты удачи:" };

            string luckListShow = String.Empty;

            for (int i = 1; i < 7; i++)
                luckListShow += (Character.Protagonist.Luck[i] ? luckList[i] : luckList[i + 10]) + " ";

            luckCheck.Add("BIG|" + luckListShow);

            int goodLuck = Game.Dice.Roll();

            luckCheck.Add(String.Format("Проверка удачи: {0} ⚄ - {1}зачёркунтый", goodLuck, (Character.Protagonist.Luck[goodLuck] ? "не " : String.Empty)));

            luckCheck.Add(Character.Protagonist.Luck[goodLuck] ? "BIG|GOOD|УСПЕХ :)" : "BIG|BAD|НЕУДАЧА :(");

            Character.Protagonist.Luck[goodLuck] = !Character.Protagonist.Luck[goodLuck];

            return luckCheck;
        }

        public List<string> Get()
        {
            if ((Price > 0) && (Character.Protagonist.Gold >= Price))
            {
                Character.Protagonist.Gold -= Price;

                if (!Multiple)
                    Used = true;
            }

            return new List<string> { "RELOAD" };
        }

        private bool IsHero(string name)
        {
            return (name == "ГЛАВГЕРОЙ" ? true : false);
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            int round = 1;
            int enemyWounds = 0;

            List<Character> FightAllies = new List<Character>();
            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone().SetStrength());             

            foreach (Character ally in Allies)
                if (ally == Character.Protagonist)
                    FightAllies.Add(ally);
                else
                    FightAllies.Add(ally.Clone().SetStrength());                   

            while (true)
            {
                fight.Add(String.Format("HEAD|Раунд: {0}", round));

                foreach (Character ally in FightAllies)
                {
                    if (ally.Strength <= 0)
                        continue;

                    if (GroupFight)
                        fight.Add(String.Format("{0} (сила {1})", (IsHero(ally.Name) ? "Вы" : ally.Name), ally.Strength));

                    foreach (Character enemy in FightEnemies)
                    {
                        if (enemy.Strength <= 0)
                            continue;

                        fight.Add(String.Format("{0} (сила {1})", enemy.Name, enemy.Strength));

                        int firstHeroRoll = Game.Dice.Roll();
                        int secondHeroRoll = Game.Dice.Roll();
                        int allyHitStrength = firstHeroRoll + secondHeroRoll + (ally.Skill - SkillPenalty);

                        fight.Add(
                            String.Format(
                                "{0} мощность удара : {1} ⚄ + {2} ⚄ + {3} = {4}",
                                (IsHero(ally.Name) ? "Ваша" : String.Format("{0} -", ally.Name)), firstHeroRoll, secondHeroRoll, ally.Skill, allyHitStrength
                            )
                        );

                        int firstEnemyRoll = Game.Dice.Roll();
                        int secondEnemyRoll = Game.Dice.Roll();
                        int enemyHitStrength = firstEnemyRoll + secondEnemyRoll + enemy.Skill;

                        fight.Add(
                            String.Format(
                                "{0} мощность удара: {1} ⚄ + {2} ⚄ + {3} = {4}",
                                (GroupFight ? String.Format("{0} -", enemy.Name) : "Его"), firstEnemyRoll, secondEnemyRoll, enemy.Skill, enemyHitStrength
                            )
                        );

                        if (allyHitStrength > enemyHitStrength)
                        {
                            if (enemy.SeaArmour && (firstHeroRoll == secondHeroRoll))
                                fight.Add(String.Format("BOLD|Чешуя отразила ваш удар"));
                            else
                            {
                                fight.Add(String.Format("GOOD|{0} ранен", (GroupFight ? enemy.Name : "Он")));
                                enemy.Strength -= 2 + ally.ExtendedDamage;
                                enemy.Skill -= ally.SkillDamage;

                                if (enemy.Strength <= 0)
                                    enemy.Strength = 0;

                                enemy.SaveStrength();

                                enemyWounds += 1;

                                bool enemyLost = true;

                                foreach (Character e in FightEnemies)
                                    if ((e.Strength > 0) && (e.Strength > DamageToWin))
                                        enemyLost = false;

                                if (enemyLost || ((WoundsToWin > 0) && (WoundsToWin <= enemyWounds)))
                                {
                                    fight.Add(String.Empty);
                                    fight.Add(String.Format("BIG|GOOD|{0} :)", (GroupFight && !IsHero(ally.Name) ? ally.Name + " ПОБЕДИЛ" : "ВЫ ПОБЕДИЛИ")));
                                    return fight;
                                }
                            }
                        }
                        else if (allyHitStrength < enemyHitStrength)
                        {
                            fight.Add(GroupFight && !IsHero(ally.Name) ? String.Format("BAD|{0} ранен",  ally.Name) : "BAD|Вы ранены");
                            ally.Strength -= 2 + enemy.ExtendedDamage;
                            ally.Skill -= enemy.SkillDamage;

                            if (ally.Strength < 0)
                                ally.Strength = 0;

                            ally.SaveStrength();

                            if ((ally.Strength == 0) || (ally.Strength <= DamageToWin))
                            {
                                fight.Add(String.Empty);
                                fight.Add(String.Format("BIG|BAD|{0} :(", (IsHero(ally.Name) ? "ВЫ ПРОИГРАЛИ" : String.Format("{0} ПРОИГРАЛ", ally.Name))));
                                return fight;
                            }
                        }
                        else
                            fight.Add(String.Format("BOLD|Ничья в раунде"));

                        if ((RoundsToWin > 0) && (RoundsToWin <= round))
                        {
                            fight.Add(String.Empty);
                            fight.Add(String.Format("BAD|Отведённые на победу раунды истекли.", RoundsToWin));
                            fight.Add(String.Format("BIG|BAD|{0} :(", (IsHero(ally.Name) ? "ВЫ ПРОИГРАЛИ" : String.Format("{0} ПРОИГРАЛ", ally.Name))));
                            return fight;
                        }

                        fight.Add(String.Empty);
                    }
                }

                round += 1;
            }
        }
    }
}
