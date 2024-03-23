using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.DangerFromBehindTheSnowWall
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public List<Character> Enemies { get; set; }
        public bool StrengthLossByDices { get; set; }
        public bool Difference { get; set; }
        public bool DiceUntil { get; set; }

        public override List<string> Status() => new List<string>
        {
            $"Ловкость: {Character.Protagonist.Skill}",
            $"Сила: {Character.Protagonist.Strength}/{Character.Protagonist.MaxStrength}",
            $"Удар: {Character.Protagonist.Damage}",
        };

        public override List<string> AdditionalStatus() => new List<string>
        {
            $"Наблюдательность: {Character.Protagonist.Skill}",
            $"Деньги: {MoneyFormat(Character.Protagonist.Money)}",
            $"Магия: {Character.Protagonist.Magic}",
        };

        private static string MoneyFormat(int ecu) =>
            String.Format("{0:f1}", (double)ecu / 10).TrimEnd('0').TrimEnd(',').Replace(',', '.');

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
                GameOverBy(Character.Protagonist.Strength, out toEndParagraph, out toEndText);

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
                enemies.Add($"{enemy.Name}\nловкость {enemy.Skill}  сила {enemy.Strength}  удар {enemy.Damage}");

            return enemies;
        }

        public static bool NoMoreEnemies(List<Character> enemies) =>
            enemies.Where(x => x.Strength > 0).Count() == 0;

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
                    if (enemy.Strength <= 0)
                        continue;

                    fight.Add($"{enemy.Name} (сила {enemy.Strength})");

                    if (!attackAlready)
                    {
                        int protagonistRoll = Game.Dice.Roll();
                        protagonistHitStrength = (protagonistRoll * 2) + Character.Protagonist.Skill;

                        fight.Add($"Сила вашей атаки: " +
                            $"{Game.Dice.Symbol(protagonistRoll)} x 2 + " +
                            $"{Character.Protagonist.Skill} = {protagonistHitStrength}");
                    }

                    int enemyRoll = Game.Dice.Roll();
                    int enemyHitStrength = (enemyRoll * 2) + enemy.Skill;

                    fight.Add($"Сила его атаки: " +
                        $"{Game.Dice.Symbol(enemyRoll)} x 2 + " +
                        $"{enemy.Skill} = {enemyHitStrength}");

                    if ((protagonistHitStrength > enemyHitStrength) && !attackAlready)
                    {
                        string points = Game.Services.CoinsNoun(Character.Protagonist.Damage, "очко", "очка", "очков");
                        fight.Add($"GOOD|{enemy.Name} ранен");
                        fight.Add($"Он теряет {Character.Protagonist.Damage} {points} Силы");

                        enemy.Strength -= Character.Protagonist.Damage;

                        if (NoMoreEnemies(FightEnemies))
                        {
                            fight.Add(String.Empty);
                            fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");
                            return fight;
                        }
                    }
                    else if (protagonistHitStrength > enemyHitStrength)
                    {
                        fight.Add($"BOLD|{enemy.Name} не смог вас ранить");
                    }
                    else if (protagonistHitStrength < enemyHitStrength)
                    {
                        string points = Game.Services.CoinsNoun(enemy.Damage, "очко", "очка", "очков");
                        fight.Add($"BAD|{enemy.Name} ранил вас");
                        fight.Add($"Вы теряете {enemy.Damage} {points} Силы");

                        Character.Protagonist.Strength -= enemy.Damage;

                        if (Character.Protagonist.Strength <= 0)
                        {
                            fight.Add(String.Empty);
                            fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");
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

        public List<string> Observation()
        {
            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);

            bool notice = (firstDice + secondDice + Character.Protagonist.Observation) > 10;
            string noticeLine = notice ? ">" : "<=";

            List<string> observationCheck = new List<string> {
                $"Проверка наблюдательности: {Game.Dice.Symbol(firstDice)} + " +
                $"{Game.Dice.Symbol(secondDice)} + {Character.Protagonist.Observation} " +
                $"{noticeLine} 10" };

            observationCheck.Add(notice ? "BIG|GOOD|УСПЕХ :)" : "BIG|BAD|НЕУДАЧА :(");

            Game.Buttons.Disable(notice, "Заметили", "Нет");

            return observationCheck;
        }

        private static string Numbers()
        {
            string luckListShow = String.Empty;

            for (int i = 1; i < 7; i++)
            {
                string luck = Constants.LuckList[Character.Protagonist.Luck[i] ? i : i + 10];
                luckListShow += $"{luck} ";
            }

            return luckListShow;
        }

        public List<string> Luck()
        {
            List<string> luckCheck = new List<string>
            {
                "Числа удачи:",
                "BIG|" + Numbers()
            };

            int goodLuck = Game.Dice.Roll();
            bool okResult = Character.Protagonist.Luck[goodLuck];
            string luckLine = okResult ? "не " : String.Empty;

            luckCheck.Add($"Проверка удачи: {Game.Dice.Symbol(goodLuck)} - {luckLine}является Числом Удачи");
            luckCheck.Add(Result(okResult, "УСПЕХ", "НЕУДАЧА"));

            Game.Buttons.Disable(okResult, "Удачливы, Вы удачливы все три попытки из трех", "Не повезло");

            return luckCheck;
        }

        public List<string> SimpleDoubleDice()
        {
            int diceFirst = Game.Dice.Roll();
            int diceSecond = Game.Dice.Roll();
            int result = diceFirst + diceSecond;

            string line = $"{Game.Dice.Symbol(diceFirst)} + " +
                $"{Game.Dice.Symbol(diceSecond)} = {result}";

            if (StrengthLossByDices)
            {
                Character.Protagonist.Strength -= result;
                string strength = Game.Services.CoinsNoun(result, "СИЛУ", "СИЛЫ", "СИЛ");

                line += $"\nBIG|BAD|BOLD|Вы потеряли {result} {strength}";
            }

            if (Difference)
            {
                if (diceFirst == diceSecond)
                {
                    line += "\nBIG|BOLD|Выпали два одинаковых числа!";
                }
                else if (Math.Abs(diceFirst - diceSecond) == 1)
                {
                    line += "\nBIG|BOLD|Разница между выпавшими числами составляет единицу!";
                }
                else
                {
                    line += "\nBIG|BOLD|Разница между выпавшими числами больше единицы!";
                }
            }

            return new List<string> { $"BIG|Кубики: {line}" };
        }

        public List<string> Break()
        {
            List<string> breakingLock = new List<string> { "Сбиваете замок:" };

            bool succesBreaked = false;

            while (!succesBreaked && (Character.Protagonist.Strength > 0))
            {
                Character.Protagonist.Strength -= 1;

                int dice = Game.Dice.Roll();
                string result = "не получилось...";

                if (dice < 3)
                {
                    result = "удачно!";
                    succesBreaked = true;
                }

                breakingLock.Add($"Бьёте по замку: {Game.Dice.Symbol(dice)}  - {result}");
                breakingLock.Add("BAD|За эту попытку вы теряете 1 СИЛУ...");
            }

            breakingLock.Add(Result(succesBreaked, "ЗАМОК СБИТ", "ВЫ УБИЛИСЬ ОБ ЗАМОК"));

            return breakingLock;
        }
    }
}
