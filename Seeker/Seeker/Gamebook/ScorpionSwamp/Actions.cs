using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.ScorpionSwamp
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public List<Character> Enemies { get; set; }
        public int ExtendedDamage { get; set; }
        public int UnluckDamage { get; set; }
        public int UnluckMasteryDamage { get; set; }
        public bool UntilFirstBlood { get; set; }

        public override List<string> Status() => new List<string>
        {
            $"Мастерство: {Character.Protagonist.Mastery}",
            $"Выносливость: {Character.Protagonist.Endurance}/{Character.Protagonist.MaxEndurance}",
            $"Удача: {Character.Protagonist.Luck}",
        };

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(Character.Protagonist.Endurance, out toEndParagraph, out toEndText);

        public List<string> Luck()
        {
            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);

            bool goodLuck = (firstDice + secondDice) <= Character.Protagonist.Luck;
            string luckLine = goodLuck ? "<=" : ">";

            List<string> luckCheck = new List<string> {
                $"Проверка удачи: {Game.Dice.Symbol(firstDice)} + " +
                $"{Game.Dice.Symbol(secondDice)} {luckLine} {Character.Protagonist.Luck}" };

            luckCheck.Add(goodLuck ? "BIG|GOOD|УСПЕХ :)" : "BIG|BAD|НЕУДАЧА :(");

            Game.Buttons.Disable(goodLuck, "Повезло", "Не повезло");

            if ((UnluckDamage > 0) && !goodLuck)
            {
                Character.Protagonist.Endurance -= UnluckDamage;

                string damageLine = Game.Services.CoinsNoun(Math.Abs(UnluckDamage), "очко", "очка", "очков");
                luckCheck.Add($"BAD|Вы теряете {UnluckDamage} {damageLine} Выносливости");
            }

            if ((UnluckMasteryDamage > 0) && !goodLuck)
            {
                Character.Protagonist.Endurance -= UnluckMasteryDamage;

                string damageLine = Game.Services.CoinsNoun(Math.Abs(UnluckDamage), "очко", "очка", "очков");
                luckCheck.Add($"BAD|Вы теряете {UnluckMasteryDamage} {damageLine} Мастерства");
            }

            if (Character.Protagonist.Luck > 2)
            {
                Character.Protagonist.Luck -= 1;
                luckCheck.Add("Уровень удачи снижен на единицу");
            }

            return luckCheck;
        }

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (!AvailabilityTrigger(oneOption))
                        return false;
                }

                return true;
            }
        }

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
                enemies.Add($"{enemy.Name}\nмастерство {enemy.Mastery}  выносливость {enemy.Endurance}");

            return enemies;
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
                bool firstBlood = false;
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

                        fight.Add($"Сила вашей атаки: " +
                            $"{Game.Dice.Symbol(protagonistRollFirst)} + " +
                            $"{Game.Dice.Symbol(protagonistRollSecond)} + " +
                            $"{Character.Protagonist.Mastery} = {protagonistHitStrength}");
                    }

                    Game.Dice.DoubleRoll(out int enemyRollFirst, out int enemyRollSecond);
                    int enemyHitStrength = enemyRollFirst + enemyRollSecond + enemy.Mastery;

                    fight.Add($"Сила его атаки: " +
                        $"{Game.Dice.Symbol(enemyRollFirst)} + " +
                        $"{Game.Dice.Symbol(enemyRollSecond)} + " +
                        $"{enemy.Mastery} = {enemyHitStrength}");

                    if ((protagonistHitStrength > enemyHitStrength) && !attackAlready)
                    {
                        fight.Add($"GOOD|{enemy.Name} ранен");
                        fight.Add("Он теряет 2 очка Выносливости");

                        enemy.Endurance -= 2;
                        firstBlood = true;

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
                        fight.Add($"BAD|{enemy.Name} ранил вас");
                        fight.Add("Вы теряете 2 очка Выносливости");

                        Character.Protagonist.Endurance -= ExtendedDamage > 0 ? ExtendedDamage : 2;
                        firstBlood = true;

                        if (Character.Protagonist.Endurance <= 0)
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

                    if (UntilFirstBlood && firstBlood)
                    {
                        fight.Add("BIG|BOLD|Поединок окончен с первой кровью!");
                        return fight;
                    }
                }

                round += 1;
            }
        }
    }
}
