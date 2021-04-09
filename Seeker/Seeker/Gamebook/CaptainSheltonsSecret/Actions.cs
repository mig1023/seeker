using System;
using System.Collections.Generic;
using System.Text;


namespace Seeker.Gamebook.CaptainSheltonsSecret
{
    class Actions : Abstract.IActions
    {
        public string ActionName { get; set; }
        public string ButtonName { get; set; }
        public string Aftertext { get; set; }
        public string Trigger { get; set; }

        public List<Character> Allies { get; set; }
        public List<Character> Enemies { get; set; }
        public int RoundsToWin { get; set; }
        public int WoundsToWin { get; set; }
        public int DamageToWin { get; set; }
        public int MasteryPenalty { get; set; }
        public bool GroupFight { get; set; }

        public string Text { get; set; }
        public int Price { get; set; }
        public bool Used { get; set; }
        public bool Multiple { get; set; }

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
                String.Format("Мастерство: {0}", Character.Protagonist.Mastery),
                String.Format("Выносливость: {0}", Character.Protagonist.Endurance),
                String.Format("Золото: {0}", Character.Protagonist.Gold)
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

        public bool IsButtonEnabled() => true;

        public static bool CheckOnlyIf(string option)
        {
            if (option.Contains(","))
            {
                foreach (string oneOption in option.Split(','))
                    if (!Game.Data.Triggers.Contains(oneOption.Trim()))
                        return false;

                return true;
            }
            else if (option.Contains("ЗОЛОТО >="))
                return int.Parse(option.Split('=')[1]) <= Character.Protagonist.Gold;
            else
                return Game.Data.Triggers.Contains(option);
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
                    enemies.Add(String.Format("{0}\nмастерство {1}  выносливость {2}", ally.Name, ally.Mastery, ally.GetEndurance()));

                enemies.Add("SPLITTER|против");
            }

            foreach (Character enemy in Enemies)
                enemies.Add(String.Format("{0}\nмастерство {1}  выносливость {2}", enemy.Name, enemy.Mastery, enemy.GetEndurance()));

            return enemies;
        }

        public List<string> Break()
        {
            List<string> breakingDoor = new List<string> { "Ломаете дверь:" };

            bool succesBreaked = false;

            while (!succesBreaked && (Character.Protagonist.Endurance > 0))
            {
                int firstDice = Game.Dice.Roll();
                int secondDice = Game.Dice.Roll();

                if (((firstDice == 1) || (firstDice == 6)) && (firstDice == secondDice))
                    succesBreaked = true;
                else
                    Character.Protagonist.Endurance -= 1;

                string result = (succesBreaked ? "удачный, дверь поддалась!" : "неудачный, -1 сила" );
                breakingDoor.Add(String.Format(
                    "Удар: {0} + {1} = {2}",
                    Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), result
                ));
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

            luckCheck.Add(String.Format(
                "Проверка удачи: {0} - {1}зачёркунтый",
                Game.Dice.Symbol(goodLuck), (Character.Protagonist.Luck[goodLuck] ? "не " : String.Empty)
            ));

            luckCheck.Add(Character.Protagonist.Luck[goodLuck] ? "BIG|GOOD|УСПЕХ :)" : "BIG|BAD|НЕУДАЧА :(");
            
            Character.Protagonist.Luck[goodLuck] = !Character.Protagonist.Luck[goodLuck];

            return luckCheck;
        }
        
        public List<string> RollDice() => new List<string> { String.Format("BIG|Бросок: {0}", Game.Dice.Symbol(Game.Dice.Roll())) };

        public List<string> RollDoubleDices()
        {
            int firstDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();

            return new List<string> { String.Format(
                "BIG|Бросок: {0} + {1} = {2}",
                Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), (firstDice + secondDice)
            ) };
        }

        public List<string> Mastery()
        {
            int firstDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();

            bool goodMastery = (firstDice + secondDice) <= Character.Protagonist.Mastery;

            List<string> masteryCheck = new List<string> { String.Format(
                "Проверка мастерства: {0} + {1} {2} {3} мастерство",
                Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), (goodMastery ? "<=" : ">"), Character.Protagonist.Mastery
            ) };

            masteryCheck.Add(goodMastery ? "BIG|GOOD|МАСТЕРСТВА ХВАТИЛО :)" : "BIG|BAD|МАСТЕРСТВА НЕ ХВАТИЛО :(");

            return masteryCheck;
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

        private bool IsHero(string name) => name == Character.Protagonist.Name;

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            int round = 1;
            int enemyWounds = 0;

            List<Character> FightAllies = new List<Character>();
            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone().SetEndurance());             

            if (Allies == null)
                FightAllies.Add(Character.Protagonist);
            else
                foreach (Character ally in Allies)
                    if (ally == Character.Protagonist)
                        FightAllies.Add(ally);
                    else
                        FightAllies.Add(ally.Clone().SetEndurance());                   

            while (true)
            {
                fight.Add(String.Format("HEAD|Раунд: {0}", round));

                foreach (Character ally in FightAllies)
                {
                    if (ally.Endurance <= 0)
                        continue;

                    if (GroupFight)
                        fight.Add(String.Format("{0} (сила {1})", (IsHero(ally.Name) ? "Вы" : ally.Name), ally.Endurance));

                    bool attackAlready = false;
                    int allyHitStrength = 0;
                    int firstAllyRoll = 0;
                    int secondAllyRoll = 0;

                    foreach (Character enemy in FightEnemies)
                    {
                        if (enemy.Endurance <= 0)
                            continue;

                        fight.Add(String.Format("{0} (сила {1})", enemy.Name, enemy.Endurance));

                        if (!attackAlready)
                        {
                            firstAllyRoll = Game.Dice.Roll();
                            secondAllyRoll = Game.Dice.Roll();
                            allyHitStrength = firstAllyRoll + secondAllyRoll + (ally.Mastery - MasteryPenalty);

                            fight.Add(
                                String.Format(
                                    "{0} мощность удара: {1} + {2} + {3} = {4}",
                                    (IsHero(ally.Name) ? "Ваша" : String.Format("{0} -", ally.Name)),
                                    Game.Dice.Symbol(firstAllyRoll), Game.Dice.Symbol(secondAllyRoll), ally.Mastery, allyHitStrength
                                )
                            );
                        }
                        

                        int firstEnemyRoll = Game.Dice.Roll();
                        int secondEnemyRoll = Game.Dice.Roll();
                        int enemyHitStrength = firstEnemyRoll + secondEnemyRoll + enemy.Mastery;

                        fight.Add(
                            String.Format(
                                "{0} мощность удара: {1} + {2} + {3} = {4}",
                                (GroupFight ? String.Format("{0} -", enemy.Name) : "Его"),
                                Game.Dice.Symbol(firstEnemyRoll), Game.Dice.Symbol(secondEnemyRoll), enemy.Mastery, enemyHitStrength
                            )
                        );

                        if ((allyHitStrength > enemyHitStrength) && !attackAlready)
                        {
                            if (enemy.SeaArmour && (firstAllyRoll == secondAllyRoll))
                                fight.Add(String.Format("BOLD|Чешуя отразила ваш удар"));
                            else
                            {
                                fight.Add(String.Format("GOOD|{0} ранен", (GroupFight ? enemy.Name : "Он")));
                                enemy.Endurance -= 2 + ally.ExtendedDamage;
                                enemy.Mastery -= ally.MasteryDamage;

                                if (enemy.Endurance <= 0)
                                    enemy.Endurance = 0;

                                enemy.SaveEndurance();

                                enemyWounds += 1;

                                bool enemyLost = true;

                                foreach (Character e in FightEnemies)
                                    if ((e.Endurance > 0) && (e.Endurance > DamageToWin))
                                        enemyLost = false;

                                if (enemyLost || ((WoundsToWin > 0) && (WoundsToWin <= enemyWounds)))
                                {
                                    fight.Add(String.Empty);
                                    fight.Add(String.Format("BIG|GOOD|{0} :)", (GroupFight && !IsHero(ally.Name) ? ally.Name + " ПОБЕДИЛ" : "ВЫ ПОБЕДИЛИ")));
                                    return fight;
                                }
                            }
                        }
                        else if (allyHitStrength > enemyHitStrength)
                        {
                            fight.Add(String.Format("BOLD|{0} не смог ранить", enemy.Name));
                        }
                        else if (allyHitStrength < enemyHitStrength)
                        {
                            fight.Add(GroupFight && !IsHero(ally.Name) ? String.Format("BAD|{0} ранен",  ally.Name) : "BAD|Вы ранены");
                            ally.Endurance -= 2 + enemy.ExtendedDamage;
                            ally.Mastery -= enemy.MasteryDamage;

                            if (ally.Endurance < 0)
                                ally.Endurance = 0;

                            ally.SaveEndurance();

                            bool allyLost = true;

                            foreach (Character a in FightAllies)
                                if (a.Endurance > 0)
                                    allyLost = false;

                            if (allyLost)
                            {
                                fight.Add(String.Empty);
                                fight.Add(String.Format("BIG|BAD|{0} :(", (IsHero(ally.Name) ? "ВЫ ПРОИГРАЛИ" : String.Format("{0} ПРОИГРАЛ", ally.Name))));
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
                            fight.Add(String.Format("BIG|BAD|{0} :(", (IsHero(ally.Name) ? "ВЫ ПРОИГРАЛИ" : String.Format("{0} ПРОИГРАЛ", ally.Name))));
                            return fight;
                        }

                        fight.Add(String.Empty);
                    }
                }

                round += 1;
            }
        }

        public bool IsHealingEnabled() => false;

        public void UseHealing(int healingLevel) => Game.Other.DoNothing();

        public string TextByOptions(string option) => String.Empty;
    }
}
