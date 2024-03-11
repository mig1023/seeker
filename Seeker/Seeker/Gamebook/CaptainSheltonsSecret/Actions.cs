using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.CaptainSheltonsSecret
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public List<Character> Allies { get; set; }
        public List<Character> Enemies { get; set; }
        public int RoundsToWin { get; set; }
        public int WoundsToWin { get; set; }
        public int DamageToWin { get; set; }
        public int MasteryPenalty { get; set; }
        public bool GroupFight { get; set; }

        public override List<string> Status() => new List<string>
        {
            $"Мастерство: {Character.Protagonist.Mastery}",
            $"Выносливость: {Character.Protagonist.Endurance}/{Character.Protagonist.MaxEndurance}",
            $"Золото: {Character.Protagonist.Gold}",
        };

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(Character.Protagonist.Endurance, out toEndParagraph, out toEndText);

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains(","))
            {
                int count = option
                    .Split(',')
                    .Where(x => !Game.Option.IsTriggered(x.Trim()))
                    .Count();

                return count == 0;
            }
            else if (option.Contains("ЗОЛОТО >="))
            {
                return int.Parse(option.Split('=')[1]) <= Character.Protagonist.Gold;
            }
            else
            {
                return AvailabilityTrigger(option);
            }
        }

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Type == "Get")
                return new List<string> { Head };

            if (Enemies == null)
                return enemies;

            if ((Allies != null) && GroupFight)
            {
                foreach (Character ally in Allies)
                    enemies.Add($"{ally.Name}\nмастерство {ally.Mastery}  выносливость {ally.GetEndurance()}");

                enemies.Add("SPLITTER|против");
            }

            foreach (Character enemy in Enemies)
                enemies.Add($"{enemy.Name}\nмастерство {enemy.Mastery}  выносливость {enemy.GetEndurance()}");

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

            while (!succesBreaked && (Character.Protagonist.Endurance > 0))
            {
                Game.Dice.DoubleRoll(out int firstDice, out int secondDice);

                if (((firstDice == 1) || (firstDice == 6)) && (firstDice == secondDice))
                {
                    succesBreaked = true;
                }
                else
                {
                    Character.Protagonist.Endurance -= 1;
                }

                string result = (succesBreaked ? "удачный, дверь поддалась!" : "неудачный, -1 сила" );

                breakingDoor.Add($"Удар: {Game.Dice.Symbol(firstDice)} + " +
                    $"{Game.Dice.Symbol(secondDice)} = {result}");
            }

            breakingDoor.Add(Result(succesBreaked, "ДВЕРЬ ВЗЛОМАНА", "ВЫ УБИЛИСЬ ОБ ДВЕРЬ"));

            return breakingDoor;
        }

        public List<string> Luck()
        {
            List<string> luckCheck = new List<string>
            {
                "Квадраты удачи:",
                "BIG|" + CaptainSheltonsSecret.Luck.Numbers()
            };

            int goodLuck = Game.Dice.Roll();

            if (Game.Option.IsTriggered("EvenLuck"))
            {
                bool even = goodLuck % 2 == 0;
                string no = even ? String.Empty : "не";

                luckCheck.Add($"Проверка чётности: {Game.Dice.Symbol(goodLuck)} - {no}чётное");

                if (even)
                {
                    luckCheck.Add("BIG|GOOD|УСПЕХ :)");
                    luckCheck.Add("Благодаря Заклюятью нет необходимости зачёркивать квадрат удачи!");
                    return luckCheck;
                }
                else
                {
                    luckCheck.Add("Увы, Заклятье Удачи тут не поможет...");
                }
            }

            bool isLuck = Character.Protagonist.Luck[goodLuck];
            string luckLine = isLuck ? "не " : String.Empty;
            luckCheck.Add($"Проверка удачи: {Game.Dice.Symbol(goodLuck)} - {luckLine}зачёркунтый");

            luckCheck.Add(Result(isLuck, "УСПЕХ", "НЕУДАЧА"));
            
            Character.Protagonist.Luck[goodLuck] = !Character.Protagonist.Luck[goodLuck];

            Game.Buttons.Disable(isLuck, "Повезло", "Не повезло");

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
                diceCheck.Add($"На {i} выпало: {Game.Dice.Symbol(dice)}");
            }

            Character.Protagonist.Endurance -= dices;

            diceCheck.Add($"BIG|BAD|Вы потеряли выносливости: {dices}");

            return diceCheck;
        }

        public List<string> LuckRecovery()
        {
            List<string> luckRecovery = new List<string> { "Восстановление удачи:" };

            bool success = CaptainSheltonsSecret.Luck.Recovery(luckRecovery);

            if (!success)
                luckRecovery.Add("BAD|Все цифры и так счастливые!");

            luckRecovery.Add("Цифры удачи теперь:");
            luckRecovery.Add("BIG|" + CaptainSheltonsSecret.Luck.Numbers());

            return luckRecovery;
        }

        public List<string> LuckLose()
        {
            List<string> luckLose = new List<string> { "Потеря удачи:" };

            bool success = CaptainSheltonsSecret.Luck.Lose(luckLose);

            if (success)
                luckLose.Add("GOOD|Все цифры и так несчастливые!\nВам повезло хоть в чём-то!");

            luckLose.Add("Цифры удачи теперь:");
            luckLose.Add("BIG|" + CaptainSheltonsSecret.Luck.Numbers());

            return luckLose;
        }

        public List<string> RollDice() =>
            new List<string> { $"BIG|Бросок: {Game.Dice.Symbol(Game.Dice.Roll())}" };

        public List<string> RollDoubleDices()
        {
            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);

            return new List<string> { $"BIG|Бросок: {Game.Dice.Symbol(firstDice)} + " +
                $"{Game.Dice.Symbol(secondDice)} = {firstDice + secondDice}" };
        }

        public List<string> Mastery()
        {
            int firstDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();
            bool goodMastery = (firstDice + secondDice) <= Character.Protagonist.Mastery;
            string lineMastery = goodMastery ? "<=" : ">";

            List<string> masteryCheck = new List<string> { $"Проверка мастерства: " +
                $"{Game.Dice.Symbol(firstDice)} + {Game.Dice.Symbol(secondDice)} " +
                $"{lineMastery} {Character.Protagonist.Mastery} мастерство" };

            masteryCheck.Add(Result(goodMastery, "МАСТЕРСТВА ХВАТИЛО", "МАСТЕРСТВА НЕ ХВАТИЛО"));

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

        private static bool IsProtagonist(string name) =>
            name == Character.Protagonist.Name;

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
            {
                FightAllies.Add(Character.Protagonist);
            }
            else
            {
                foreach (Character ally in Allies)
                {
                    if (ally == Character.Protagonist)
                        FightAllies.Add(ally);
                    else
                        FightAllies.Add(ally.Clone().SetEndurance());
                }
            }

            while (true)
            {
                fight.Add($"HEAD|BOLD|Раунд: {round}");

                foreach (Character ally in FightAllies)
                {
                    if (ally.Endurance <= 0)
                        continue;

                    if (GroupFight)
                    {
                        string person = (IsProtagonist(ally.Name) ? "Вы" : ally.Name);
                        fight.Add($"{person} (сила {ally.Endurance})");
                    }

                    bool attackAlready = false;
                    int allyHitStrength = 0;
                    int firstAllyRoll = 0;
                    int secondAllyRoll = 0;

                    foreach (Character enemy in FightEnemies)
                    {
                        if (enemy.Endurance <= 0)
                            continue;

                        fight.Add($"{enemy.Name} (сила {enemy.Endurance})");

                        if (!attackAlready)
                        {
                            Game.Dice.DoubleRoll(out firstAllyRoll, out secondAllyRoll);
                            allyHitStrength = firstAllyRoll + secondAllyRoll + (ally.Mastery - MasteryPenalty);
                            string who = IsProtagonist(ally.Name) ? "Ваша" : $"{ally.Name} -";

                            fight.Add($"{who} мощность удара: " +
                                $"{Game.Dice.Symbol(firstAllyRoll)} + {Game.Dice.Symbol(secondAllyRoll)} + " +
                                $"{ally.Mastery} = {allyHitStrength}");
                        }

                        Game.Dice.DoubleRoll(out int firstEnemyRoll, out int secondEnemyRoll);
                        int enemyHitStrength = firstEnemyRoll + secondEnemyRoll + enemy.Mastery;
                        string enemyLine = GroupFight ? $"{enemy.Name} -" : "Его";

                        fight.Add($"{enemyLine} мощность удара: {Game.Dice.Symbol(firstEnemyRoll)} + " +
                            $"{Game.Dice.Symbol(secondEnemyRoll)} + {enemy.Mastery} = {enemyHitStrength}");

                        if ((allyHitStrength > enemyHitStrength) && !attackAlready)
                        {
                            if (enemy.SeaArmour && (firstAllyRoll == secondAllyRoll))
                            {
                                fight.Add("BOLD|Чешуя отразила ваш удар");
                            }
                            else
                            {
                                string group = GroupFight ? enemy.Name : "Он";
                                fight.Add($"GOOD|{group} ранен");
                                enemy.Endurance -= 2 + ally.ExtendedDamage;
                                enemy.Mastery -= ally.MasteryDamage;

                                enemyWounds += 1;

                                bool enemyLost = FightEnemies
                                    .Where(x => ((x.Endurance > 0) && (x.Endurance > DamageToWin)))
                                    .Count() == 0;

                                if (enemyLost || ((WoundsToWin > 0) && (WoundsToWin <= enemyWounds)))
                                {
                                    fight.Add(String.Empty);

                                    bool heroOrAlly = GroupFight && !IsProtagonist(ally.Name);
                                    string who = heroOrAlly ? $"{ally.Name} ПОБЕДИЛ" : "ВЫ ПОБЕДИЛИ";

                                    fight.Add($"BIG|GOOD|{who} :)");

                                    return fight;
                                }
                            }
                        }
                        else if (allyHitStrength > enemyHitStrength)
                        {
                            fight.Add($"BOLD|{enemy.Name} не смог ранить");
                        }
                        else if (allyHitStrength < enemyHitStrength)
                        {
                            bool isEnemy = GroupFight && !IsProtagonist(ally.Name);
                            fight.Add(isEnemy ? $"BAD|{ally.Name} ранен" : "BAD|Вы ранены");
                            ally.Endurance -= 2 + enemy.ExtendedDamage;
                            ally.Mastery -= enemy.MasteryDamage;

                            bool allyLost = FightAllies.Where(x => x.Endurance > 0).Count() == 0;

                            if (allyLost)
                            {
                                fight.Add(String.Empty);

                                bool heroOrAlly = IsProtagonist(ally.Name);
                                string who = heroOrAlly ? "ВЫ ПРОИГРАЛИ" : $"{ally.Name} ПРОИГРАЛ";

                                fight.Add($"BIG|BAD|{who} :(");

                                return fight;
                            }
                        }
                        else
                        {
                            fight.Add("BOLD|Ничья в раунде");
                        }

                        attackAlready = true;

                        if ((RoundsToWin > 0) && (RoundsToWin <= round))
                        {
                            bool isHero = IsProtagonist(ally.Name);
                            string result = isHero ? "ВЫ ПРОИГРАЛИ" : $"{ally.Name} ПРОИГРАЛ";

                            fight.Add(String.Empty);
                            fight.Add("BAD|Отведённые на победу раунды истекли.");
                            fight.Add($"BIG|BAD|{result} :(");
                            return fight;
                        }

                        fight.Add(String.Empty);
                    }
                }

                round += 1;
            }
        }

        public override bool IsHealingEnabled() =>
            Character.Protagonist.Endurance < Character.Protagonist.MaxEndurance;

        public override void UseHealing(int healingLevel) =>
            Character.Protagonist.Endurance += healingLevel;
    }
}
