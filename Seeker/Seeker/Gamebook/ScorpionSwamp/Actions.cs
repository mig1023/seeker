﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.ScorpionSwamp
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public List<Character> Enemies { get; set; }

        public override List<string> Status() => new List<string>
        {
            $"Мастерство: {protagonist.Mastery}",
            $"Выносливость: {protagonist.Endurance}/{protagonist.MaxEndurance}",
            $"Удача: {protagonist.Luck}",
        };

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(protagonist.Endurance, out toEndParagraph, out toEndText);

        public List<string> Luck()
        {
            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);

            bool goodLuck = (firstDice + secondDice) <= protagonist.Luck;
            string luckLine = goodLuck ? "<=" : ">";

            List<string> luckCheck = new List<string> {
                $"Проверка удачи: {Game.Dice.Symbol(firstDice)} + " +
                $"{Game.Dice.Symbol(secondDice)} {luckLine} {protagonist.Luck}" };

            luckCheck.Add(goodLuck ? "BIG|GOOD|УСПЕХ :)" : "BIG|BAD|НЕУДАЧА :(");

            if (protagonist.Luck > 2)
            {
                protagonist.Luck -= 1;
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
                return AvailabilityTrigger(option);
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
                        protagonistHitStrength = protagonistRollFirst + protagonistRollSecond + protagonist.Mastery;

                        fight.Add($"Сила вашей атаки: " +
                            $"{Game.Dice.Symbol(protagonistRollFirst)} + " +
                            $"{Game.Dice.Symbol(protagonistRollSecond)} + " +
                            $"{protagonist.Mastery} = {protagonistHitStrength}");
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
                        fight.Add($"Он теряет 2 очка Выносливости");

                        enemy.Endurance -= 2;

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
                        fight.Add($"Вы теряете 2 очка Выносливости");

                        protagonist.Endurance -= 2;

                        if (protagonist.Endurance <= 0)
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
    }
}
