﻿using Seeker.Gamebook.CreatureOfHavoc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.SeasOfBlood
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public List<Character> Enemies { get; set; }

        public override List<string> Status() => new List<string>
        {
            $"Сила команды: {Character.Protagonist.TeamStrength}",
            $"Численность: {Character.Protagonist.TeamSize}/{Character.Protagonist.MaxTeamSize}",
            $"Судовой журнал: {Character.Protagonist.Logbook}/50",
        };

        public override List<string> AdditionalStatus() => new List<string>
        {
            $"Мастерство: {Character.Protagonist.Mastery}",
            $"Выносливость: {Character.Protagonist.Endurance}/{Character.Protagonist.MaxEndurance}",
            $"Удачливость: {Character.Protagonist.Luck}/{Character.Protagonist.MaxLuck}",
            $"Золото: {Character.Protagonist.Coins}",
            $"Добыча: {Character.Protagonist.Spoils}",
        };

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Enemies == null)
                return enemies;

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

                        fight.Add($"Сила вашего удара: " +
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

                        Character.Protagonist.Endurance -= 2;

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

                fight.Add($"Сила удара вашей команды: " +
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
                    fight.Add("Численность вашей команды уменьшилась на 2");

                    Character.Protagonist.TeamSize -= 2;

                    if (Character.Protagonist.TeamSize <= 0)
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

                fight.Add(String.Empty);

                round += 1;
            }
        }
    }
}
