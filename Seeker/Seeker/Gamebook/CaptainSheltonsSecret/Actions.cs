using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.CaptainSheltonsSecret
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public List<Character> Allies { get; set; }
        public List<Character> Enemies { get; set; }
        public int RoundsToWin { get; set; }
        public int WoundsToWin { get; set; }
        public int DamageToWin { get; set; }
        public int MasteryPenalty { get; set; }
        public bool GroupFight { get; set; }

        public override List<string> Status() => new List<string>
        {
            String.Format("Мастерство: {0}", protagonist.Mastery),
            String.Format("Выносливость: {0}/{1}", protagonist.Endurance, protagonist.MaxEndurance),
            String.Format("Золото: {0}", protagonist.Gold)
        };

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(protagonist.Endurance, out toEndParagraph, out toEndText);

        public override bool CheckOnlyIf(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains(","))
            {
                return !(option.Split(',').Where(x => !Game.Option.IsTriggered(x.Trim())).Count() > 0);
            }
            else if (option.Contains("ЗОЛОТО >="))
            {
                return int.Parse(option.Split('=')[1]) <= protagonist.Gold;
            }
            else
            {
                return CheckOnlyIfTrigger(option);
            }
        }

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Type == "Get")
                return new List<string> { Text };

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

        public override string ButtonText()
        {
            switch (Type)
            {
                case "Fight":
                    return (GroupFight ? "Пусть сражаются" : "Сражаться");

                case "Luck":
                    return "Проверить удачу";

                case "LuckRecovery":
                    return "Восстановить 1 цифру удачи";

                case "LuckLose":
                    return "Потеряйте 1 цифру удачи";

                default:
                    return Button;
            }
        }

        public List<string> Break()
        {
            List<string> breakingDoor = new List<string> { "Ломаете дверь:" };

            bool succesBreaked = false;

            while (!succesBreaked && (protagonist.Endurance > 0))
            {
                Game.Dice.DoubleRoll(out int firstDice, out int secondDice);

                if (((firstDice == 1) || (firstDice == 6)) && (firstDice == secondDice))
                    succesBreaked = true;
                else
                    protagonist.Endurance -= 1;

                string result = (succesBreaked ? "удачный, дверь поддалась!" : "неудачный, -1 сила" );

                breakingDoor.Add(String.Format(
                    "Удар: {0} + {1} = {2}",
                    Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), result));
            }

            breakingDoor.Add(Result(succesBreaked, "ДВЕРЬ ВЗЛОМАНА|ВЫ УБИЛИСЬ ОБ ДВЕРЬ"));

            return breakingDoor;
        }

        private string LuckNumbers()
        {
            string luckListShow = String.Empty;

            for (int i = 1; i < 7; i++)
                luckListShow += String.Format("{0} ", protagonist.Luck[i] ? Constants.LuckList()[i] : Constants.LuckList()[i + 10]);

            return luckListShow;
        }

        public List<string> Luck()
        {
            List<string> luckCheck = new List<string>
            {
                "Квадраты удачи:",
                "BIG|" + LuckNumbers()
            };

            int goodLuck = Game.Dice.Roll();

            luckCheck.Add(String.Format("Проверка удачи: {0} - {1}зачёркунтый",
                Game.Dice.Symbol(goodLuck), (protagonist.Luck[goodLuck] ? "не " : String.Empty)));

            luckCheck.Add(Result(protagonist.Luck[goodLuck], "УСПЕХ|НЕУДАЧА"));
            
            protagonist.Luck[goodLuck] = !protagonist.Luck[goodLuck];

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

            protagonist.Endurance -= dices;

            diceCheck.Add(String.Format("BIG|BAD|Вы потеряли выносливости: {0}", dices));

            return diceCheck;
        }

        public List<string> LuckRecovery()
        {
            List<string> luckRecovery = new List<string> { "Восстановление удачи:" };

            bool success = false;

            for (int i = 1; i < 7; i++)
            {
                if (!protagonist.Luck[i])
                {
                    luckRecovery.Add(String.Format("GOOD|Цифра {0} восстановлена!", i));
                    protagonist.Luck[i] = true;
                    success = true;

                    break;
                }
            }                

            if (!success)
                luckRecovery.Add("BAD|Все цифры и так счастливые!");

            luckRecovery.Add("Цифры удачи теперь:");
            luckRecovery.Add("BIG|" + LuckNumbers());

            return luckRecovery;
        }

        public List<string> LuckLose()
        {
            List<string> luckLose = new List<string> { "Потеря удачи:" };

            bool success = true;

            for (int i = 1; i < 7; i++)
            {
                if (protagonist.Luck[i])
                {
                    luckLose.Add(String.Format("BAD|Цифра {0} стала несчастливой...", i));
                    protagonist.Luck[i] = false;
                    success = false;

                    break;
                }
            }

            if (success)
                luckLose.Add("GOOD|Все цифры и так несчастливые!\nВам повезло хоть в чём-то!");

            luckLose.Add("Цифры удачи теперь:");
            luckLose.Add("BIG|" + LuckNumbers());

            return luckLose;
        }

        public List<string> RollDice() => new List<string> { String.Format("BIG|Бросок: {0}", Game.Dice.Symbol(Game.Dice.Roll())) };

        public List<string> RollDoubleDices()
        {
            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);

            return new List<string> { String.Format(
                "BIG|Бросок: {0} + {1} = {2}",
                Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), (firstDice + secondDice)) };
        }

        public List<string> Mastery()
        {
            int firstDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();
            bool goodMastery = (firstDice + secondDice) <= protagonist.Mastery;

            List<string> masteryCheck = new List<string> { String.Format(
                "Проверка мастерства: {0} + {1} {2} {3} мастерство",
                Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), (goodMastery ? "<=" : ">"), protagonist.Mastery) };

            masteryCheck.Add(Result(goodMastery, "МАСТЕРСТВА ХВАТИЛО|МАСТЕРСТВА НЕ ХВАТИЛО"));

            return masteryCheck;
        }

        public List<string> Get()
        {
            if ((Price > 0) && (protagonist.Gold >= Price))
            {
                protagonist.Gold -= Price;

                if (!Multiple)
                    Used = true;
            }

            return new List<string> { "RELOAD" };
        }

        private bool IsProtagonist(string name) => name == protagonist.Name;

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
                FightAllies.Add(protagonist);
            else
                foreach (Character ally in Allies)
                    if (ally == protagonist)
                        FightAllies.Add(ally);
                    else
                        FightAllies.Add(ally.Clone().SetEndurance());                   

            while (true)
            {
                fight.Add(String.Format("HEAD|BOLD|Раунд: {0}", round));

                foreach (Character ally in FightAllies)
                {
                    if (ally.Endurance <= 0)
                        continue;

                    if (GroupFight)
                        fight.Add(String.Format("{0} (сила {1})", (IsProtagonist(ally.Name) ? "Вы" : ally.Name), ally.Endurance));

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
                            Game.Dice.DoubleRoll(out firstAllyRoll, out secondAllyRoll);
                            allyHitStrength = firstAllyRoll + secondAllyRoll + (ally.Mastery - MasteryPenalty);

                            fight.Add(String.Format(
                                "{0} мощность удара: {1} + {2} + {3} = {4}",
                                (IsProtagonist(ally.Name) ? "Ваша" : String.Format("{0} -", ally.Name)),
                                Game.Dice.Symbol(firstAllyRoll), Game.Dice.Symbol(secondAllyRoll), ally.Mastery, allyHitStrength));
                        }

                        Game.Dice.DoubleRoll(out int firstEnemyRoll, out int secondEnemyRoll);
                        int enemyHitStrength = firstEnemyRoll + secondEnemyRoll + enemy.Mastery;

                        fight.Add(String.Format(
                            "{0} мощность удара: {1} + {2} + {3} = {4}",
                            (GroupFight ? String.Format("{0} -", enemy.Name) : "Его"),
                            Game.Dice.Symbol(firstEnemyRoll), Game.Dice.Symbol(secondEnemyRoll), enemy.Mastery, enemyHitStrength));

                        if ((allyHitStrength > enemyHitStrength) && !attackAlready)
                        {
                            if (enemy.SeaArmour && (firstAllyRoll == secondAllyRoll))
                                fight.Add(String.Format("BOLD|Чешуя отразила ваш удар"));
                            else
                            {
                                fight.Add(String.Format("GOOD|{0} ранен", (GroupFight ? enemy.Name : "Он")));
                                enemy.Endurance -= 2 + ally.ExtendedDamage;
                                enemy.Mastery -= ally.MasteryDamage;

                                enemyWounds += 1;

                                bool enemyLost = FightEnemies.Where(x => ((x.Endurance > 0) && (x.Endurance > DamageToWin))).Count() == 0;

                                if (enemyLost || ((WoundsToWin > 0) && (WoundsToWin <= enemyWounds)))
                                {
                                    fight.Add(String.Empty);

                                    fight.Add(String.Format("BIG|GOOD|{0} :)",
                                        (GroupFight && !IsProtagonist(ally.Name) ? ally.Name + " ПОБЕДИЛ" : "ВЫ ПОБЕДИЛИ")));

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
                            fight.Add(GroupFight && !IsProtagonist(ally.Name) ? String.Format("BAD|{0} ранен",  ally.Name) : "BAD|Вы ранены");
                            ally.Endurance -= 2 + enemy.ExtendedDamage;
                            ally.Mastery -= enemy.MasteryDamage;

                            bool allyLost = FightAllies.Where(x => x.Endurance > 0).Count() == 0;

                            if (allyLost)
                            {
                                fight.Add(String.Empty);

                                fight.Add(String.Format("BIG|BAD|{0} :(",
                                    (IsProtagonist(ally.Name) ? "ВЫ ПРОИГРАЛИ" : String.Format("{0} ПРОИГРАЛ", ally.Name))));

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
                            fight.Add(String.Format("BIG|BAD|{0} :(", (IsProtagonist(ally.Name) ? "ВЫ ПРОИГРАЛИ" : String.Format("{0} ПРОИГРАЛ", ally.Name))));
                            return fight;
                        }

                        fight.Add(String.Empty);
                    }
                }

                round += 1;
            }
        }

        public override bool IsHealingEnabled() => protagonist.Endurance < protagonist.MaxEndurance;

        public override void UseHealing(int healingLevel) => protagonist.Endurance += healingLevel;
    }
}
