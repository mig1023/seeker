using Seeker.Gamebook.CreatureOfHavoc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.SeasOfBlood
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public int Less { get; set; }
        public int More { get; set; }

        public List<Character> Enemies { get; set; }

        public override List<string> Status() => new List<string>
        {
            $"Сила команды: {Character.Protagonist.TeamStrength}",
            $"Численность: {Character.Protagonist.TeamSize}/{Character.Protagonist.MaxTeamSize}",
            $"День: {Character.Protagonist.Logbook}/50",
        };

        public override List<string> AdditionalStatus() => new List<string>
        {
            $"Мастерство: {Character.Protagonist.Mastery}",
            $"Выносливость: {Character.Protagonist.Endurance}/{Character.Protagonist.MaxEndurance}",
            $"Удачливость: {Character.Protagonist.Luck}/{Character.Protagonist.MaxLuck}",
            $"Золото: {Character.Protagonist.Coins}",
            $"Рабы: {Character.Protagonist.Spoils}",
        };

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Type.StartsWith("HirePirates"))
            {
                string coins = Game.Services.CoinsNoun(Price, "монету", "монеты", "монет");
                string multi = Type.EndsWith("Random") ? "ОВ" : "А";
                return new List<string> { $"НАЙМ ПИРАТ{multi}\nза {Price} {coins}" };
            }

            if (Enemies == null)
            {
                return enemies;
            }

            if (Type == "TeamFight")
            {
                Character enemy = Enemies.First();
                enemies.Add($"{enemy.Name}\nсила {enemy.TeamStrength}  численность {enemy.TeamSize}");
            }
            else
            {
                foreach (Character enemy in Enemies)
                    enemies.Add($"{enemy.Name}\nмастерство {enemy.Mastery}  выносливость {enemy.Endurance}");
            }
            
            return enemies;
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = Output.Constants.GAMEOVER_TEXT;

            bool byEndurance = Character.Protagonist.Endurance <= 0;
            bool byTeamSize = Character.Protagonist.TeamSize <= 0;

            return (byEndurance || byTeamSize);
        }

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.StartsWith("СОКРОВИЩА"))
            {
                int level = Game.Services.LevelParse(option);
                int coins = (int)Math.Round((double)Character.Protagonist.Coins / 100) * 100;

                if (level == 100)
                {
                    return coins <= 100;
                }
                else if (level == 800)
                {
                    return coins >= 800;
                }
                else
                {
                    return coins == level;
                }
            }
            else if (option.StartsWith("ДЕНЬГИ >="))
            {
                int money = Game.Services.LevelParse(option);
                return Character.Protagonist.Coins >= money;
            }
            else if (option.EndsWith("ЧИСЛО ДНЕЙ"))
            {
                bool even = Character.Protagonist.Logbook % 2 == 0;
                return option.StartsWith("ЧЕТНОЕ") ? even : !even;
            }
            else
            {
                return AvailabilityTrigger(option);
            }
        }

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            if (Type == "SellSlaves")
            {
                return Character.Protagonist.Spoils > 0;
            }
            else if (Type.StartsWith("HirePirates"))
            {
                bool coins = Character.Protagonist.Coins >= Price;
                bool team = Character.Protagonist.TeamSize < Character.Protagonist.MaxTeamSize;

                return coins && team;
            }
            else
            {
                return true;
            }
        }

        public static bool NoMoreEnemies(List<Character> enemies) =>
            enemies.Where(x => x.Endurance > 0).Count() == 0;

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            int round = 1;

            while (true)
            {
                fight.Add($"HEAD|BOLD|Раунд: {round}");

                bool attackAlready = false;
                int protagonistHitStrength = 0;

                foreach (Character enemy in FightEnemies)
                {
                    if (enemy.Endurance <= 0)
                        continue;

                    fight.Add($"{enemy.Name} (выносливость {enemy.Endurance})");

                    if (!attackAlready)
                    {
                        Game.Dice.DoubleRoll(out int protagonistRollFirst, out int protagonistRollSecond);
                        protagonistHitStrength = protagonistRollFirst + protagonistRollSecond + Character.Protagonist.Mastery;

                        fight.Add($"Сила твоего удара: " +
                            $"{Game.Dice.Symbol(protagonistRollFirst)} + " +
                            $"{Game.Dice.Symbol(protagonistRollSecond)} + " +
                            $"{Character.Protagonist.Mastery} = {protagonistHitStrength}");
                    }

                    Game.Dice.DoubleRoll(out int enemyRollFirst, out int enemyRollSecond);
                    int enemyHitStrength = enemyRollFirst + enemyRollSecond + enemy.Mastery;

                    fight.Add($"Сила его удара: " +
                        $"{Game.Dice.Symbol(enemyRollFirst)} + " +
                        $"{Game.Dice.Symbol(enemyRollSecond)} + " +
                        $"{enemy.Mastery} = {enemyHitStrength}");

                    if ((protagonistHitStrength > enemyHitStrength) && !attackAlready)
                    {
                        fight.Add($"GOOD|{enemy.Name} ранен");
                        fight.Add("Он теряет 2 очка Выносливости");

                        enemy.Endurance -= 2;

                        if (NoMoreEnemies(FightEnemies))
                        {
                            fight.Add(String.Empty);
                            fight.Add("BIG|GOOD|Ты ПОБЕДИЛ :)");
                            return fight;
                        }
                    }
                    else if (protagonistHitStrength > enemyHitStrength)
                    {
                        fight.Add($"BOLD|{enemy.Name} не смог тебя ранить");
                    }
                    else if (protagonistHitStrength < enemyHitStrength)
                    {
                        fight.Add($"BAD|{enemy.Name} ранил тебя");
                        fight.Add("Ты теряешь 2 очка Выносливости");

                        Character.Protagonist.Endurance -= 2;

                        if (Character.Protagonist.Endurance <= 0)
                        {
                            fight.Add(String.Empty);
                            fight.Add("BIG|BAD|Ты ПРОИГРАЛ :(");
                            return fight;
                        }
                    }
                    else
                    {
                        fight.Add("BOLD|Ничья в раунде");
                    }

                    attackAlready = true;

                    fight.Add(String.Empty);
                }

                round += 1;
            }
        }

        public List<string> TeamFight()
        {
            List<string> fight = new List<string>();

            Character enemyTeam = Enemies.First().Clone();

            int round = 1;

            while (true)
            {
                fight.Add($"HEAD|BOLD|Раунд: {round}");

                fight.Add($"{enemyTeam.Name} (численность {enemyTeam.TeamSize})");

                Game.Dice.DoubleRoll(out int teamRollFirst, out int teamRollSecond);
                int teamStrength = teamRollFirst + teamRollSecond + Character.Protagonist.TeamStrength;

                fight.Add($"Сила удара твоей команды: " +
                    $"{Game.Dice.Symbol(teamRollFirst)} + " +
                    $"{Game.Dice.Symbol(teamRollSecond)} + " +
                    $"{Character.Protagonist.TeamStrength} = {teamStrength}");

                Game.Dice.DoubleRoll(out int enemyRollFirst, out int enemyRollSecond);
                int enemyStrength = enemyRollFirst + enemyRollSecond + enemyTeam.TeamStrength;

                fight.Add($"Сила его удара: " +
                    $"{Game.Dice.Symbol(enemyRollFirst)} + " +
                    $"{Game.Dice.Symbol(enemyRollSecond)} + " +
                    $"{enemyTeam.TeamStrength} = {enemyStrength}");

                if (teamStrength > enemyStrength)
                {
                    fight.Add("GOOD|Противник проиграл раунд");
                    fight.Add("Его численность уменьшилась на 2");

                    enemyTeam.TeamSize -= 2;

                    if (enemyTeam.TeamSize <= 0)
                    {
                        fight.Add(String.Empty);
                        fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");
                        return fight;
                    }
                }
                else if (teamStrength < enemyStrength)
                {
                    fight.Add($"BAD|Противник выиграл раунд");
                    fight.Add("Численность твоей команды уменьшилась на 2");

                    Character.Protagonist.TeamSize -= 2;

                    if (Character.Protagonist.TeamSize <= 0)
                    {
                        fight.Add(String.Empty);
                        fight.Add("BIG|BAD|Ты ПРОИГРАЛИ :(");
                        return fight;
                    }
                }
                else
                {
                    fight.Add("BOLD|Ничья в раунде");
                }

                fight.Add(String.Empty);

                round += 1;
            }
        }

        private int TeamSizeBonus(ref List<string> test, string line, int summ, int bonus)
        {
            int newSumm = summ - bonus;

            if (newSumm <= 0)
                newSumm = 0;

            test.Add($"GOOD|Благодаря {line}, " +
                $"выпавшая сумма уменьшается на {bonus} и теперь равна {newSumm}");

            return newSumm;
        }

        public List<string> TeamSizeTest()
        {
            List<string> test = new List<string>();

            int first = Game.Dice.Roll();
            int second = Game.Dice.Roll();
            int third = Game.Dice.Roll();
            int dicesSumm = first + second + third;

            int size = Character.Protagonist.TeamSize;
            string line = Game.Services.CoinsNoun(size, "пират", "пирата", "пиратов");
            test.Add($"Численность команды: {size} {line}");

            test.Add($"Бросаем кубики: {Game.Dice.Symbol(first)} + " +
                $"{Game.Dice.Symbol(second)} + {Game.Dice.Symbol(third)} = {dicesSumm}");

            if (Game.Option.IsTriggered("Благословление"))
            {
                dicesSumm = TeamSizeBonus(ref test, "благословению призрака", dicesSumm, 2);
            }

            if (Game.Option.IsTriggered("Мешки"))
            {
                dicesSumm = TeamSizeBonus(ref test, "мешкам Короля Четырех Ветров", dicesSumm, 4);
            }

            int days = 0;

            if (dicesSumm < size)
            {
                test.Add("BIG|BOLD|Выпало МЕНЬШЕ численности!");

                if (Less > 0)
                    days = Less;

                Game.Buttons.Disable("Больше или равно ЧИСЛЕННОСТИ твоего экипажа");
            }
            else
            {
                test.Add("BIG|BOLD|Выпало БОЛЬШЕ или равно численности!");

                if (More > 0)
                    days = More;

                Game.Buttons.Disable("Меньше ЧИСЛЕННОСТИ твоего экипажа");
            }

            if (days > 0)
            {
                string count = Game.Services.CoinsNoun(days, "день", "дня", "дней");
                test.Add($"В судовой журнал пишем {days} {count}");
                Character.Protagonist.Logbook += days;

                if (Character.Protagonist.Endurance < Character.Protagonist.MaxEndurance)
                {
                    Character.Protagonist.Endurance += 1;
                    test.Add("GRAY|Ты восстанавливаешь 1 единицу выносливости");
                }
            }

            return test;
        }

        public List<string> Luck()
        {
            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);

            bool goodLuck = (firstDice + secondDice) <= Character.Protagonist.Luck;
            string luckLine = goodLuck ? "<=" : ">";

            List<string> luckCheck = new List<string> {
                $"Проверка удачи: {Game.Dice.Symbol(firstDice)} + " +
                $"{Game.Dice.Symbol(secondDice)} {luckLine} {Character.Protagonist.Luck}" };

            luckCheck.Add(goodLuck ? "BIG|GOOD|УСПЕХ :)" : "BIG|BAD|НЕУДАЧА :(");

            if (Character.Protagonist.Luck > 2)
            {
                Character.Protagonist.Luck -= 1;
                luckCheck.Add("Уровень удачи снижен на единицу");
            }

            Game.Buttons.Disable(goodLuck, "Повезло", $"Не повезло");

            return luckCheck;
        }

        public List<string> SellSlaves()
        {
            List<string> sell = new List<string>();

            int spoils = Character.Protagonist.Spoils;

            sell.Add($"Число невольников: {spoils}");
            sell.Add($"Продажная цена: {Price}");

            int summ = spoils * Price;

            Character.Protagonist.Spoils = 0;
            Character.Protagonist.Coins += summ;

            string coins = Game.Services.CoinsNoun(summ, "монета", "монеты", "монет");
            sell.Add($"BIG|Выручка: {spoils} x {Price} = {summ} {coins}");

            return sell;
        }

        private List<string> Hire(int count)
        {
            Character.Protagonist.Coins -= Price;
            Character.Protagonist.TeamSize += count;

            return new List<string> { "RELOAD" };
        }

        public List<string> HirePirates() =>
            Hire(1);

        public List<string> HirePiratesRandom() =>
            Hire(Game.Dice.Roll());
    }
}
