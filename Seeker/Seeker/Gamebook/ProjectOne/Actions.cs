﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.ProjectOne
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public List<Character> Allies { get; set; }
        public List<Character> Enemies { get; set; }

        public override List<string> Status() => new List<string>
        {
            $"Ловкость: {protagonist.Skill}/{protagonist.MaxSkill}",
            $"Сила: {protagonist.Endurance}/{protagonist.MaxEndurance}",
            $"Удача: {protagonist.Luck}/{protagonist.MaxLuck}",
        };

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(protagonist.Endurance, out toEndParagraph, out toEndText);

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Enemies == null)
                return enemies;

            if (Allies != null)
            {
                enemies.Add($"Вы\nловкость {protagonist.Skill}  сила {protagonist.Endurance}");

                foreach (Character ally in Allies)
                    enemies.Add($"{ally.Name}\nловкость {ally.Skill}  сила {ally.Endurance}");

                enemies.Add("SPLITTER|против");
            }

            foreach (Character enemy in Enemies)
                enemies.Add($"{enemy.Name}\nловкость {enemy.Skill}  сила {enemy.Endurance}");

            return enemies;
        }

        private static bool NoMoreEnemies(List<Character> enemies) =>
           enemies.Where(x => x.Endurance > 0).Count() == 0;

        private static bool IsProtagonist(string name) =>
            name == Character.Protagonist.Name;

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            List<Character> FightAllies = new List<Character>();
            List<Character> FightEnemies = new List<Character>();

            bool groupFight = false;

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            if (Allies == null)
            {
                FightAllies.Add(protagonist);
            }
            else
            {
                groupFight = true;
                FightAllies.Add(protagonist);

                foreach (Character ally in Allies)
                    FightAllies.Add(ally.Clone());
            }

            int round = 1;

            while (true)
            {
                fight.Add($"HEAD|BOLD|Раунд: {round}");

                List<string> alreadyAttack = new List<string>();

                foreach (Character ally in FightAllies)
                {
                    if (ally.Endurance <= 0)
                        continue;

                    if (groupFight)
                    {
                        string person = (IsProtagonist(ally.Name) ? "Вы" : ally.Name);
                        fight.Add($"{person} (сила {ally.Endurance})");
                    }

                    int allyHitStrength = 0;
                    int enemyHitStrength = 0;

                    foreach (Character enemy in FightEnemies)
                    {
                        if (enemy.Endurance <= 0)
                            continue;

                        fight.Add($"{enemy.Name} (сила {enemy.Endurance})");

                        if (!alreadyAttack.Contains(ally.Name))
                        {
                            Game.Dice.DoubleRoll(out int allyRollFirst, out int allyRollSecond);
                            allyHitStrength = allyRollFirst + allyRollSecond + ally.Skill;
                            string who = IsProtagonist(ally.Name) ? "Ваша" : $"{ally.Name} -";

                            fight.Add($"{who} можность атаки: " +
                                $"{Game.Dice.Symbol(allyRollFirst)} + " +
                                $"{Game.Dice.Symbol(allyRollSecond)} + " +
                                $"{ally.Skill} ловкость = {allyHitStrength}");
                        }

                        if (!alreadyAttack.Contains(enemy.Name))
                        {
                            Game.Dice.DoubleRoll(out int enemyRollFirst, out int enemyRollSecond);
                            enemyHitStrength = enemyRollFirst + enemyRollSecond + enemy.Skill;
                            string enemyLine = groupFight ? $"{enemy.Name} -" : "Его";

                            fight.Add($"{enemyLine} мощность атаки: " +
                                $"{Game.Dice.Symbol(enemyRollFirst)} + " +
                                $"{Game.Dice.Symbol(enemyRollSecond)} + " +
                                $"{enemy.Skill} ловкость = {enemyHitStrength}");
                        }

                        if ((allyHitStrength > enemyHitStrength) && !alreadyAttack.Contains(ally.Name))
                        {
                            fight.Add($"GOOD|{enemy.Name} ранен");

                            enemy.Endurance -= 2;

                            if (NoMoreEnemies(FightEnemies))
                            {
                                fight.Add(String.Empty);
                                fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");
                                return fight;
                            }
                        }
                        else if (allyHitStrength > enemyHitStrength)
                        {
                            fight.Add($"BOLD|{enemy.Name} не смог нанести удар");
                        }
                        else if ((allyHitStrength < enemyHitStrength) && !alreadyAttack.Contains(enemy.Name))
                        {
                            bool isEnemy = groupFight && !IsProtagonist(ally.Name);
                            fight.Add(isEnemy ? $"BAD|{ally.Name} ранен" : "BAD|Вы ранены");

                            ally.Endurance -= 2;

                            bool allyLost = FightAllies.Where(x => x.Endurance > 0).Count() == 0;

                            if (allyLost)
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

                        alreadyAttack.Add(ally.Name);
                        alreadyAttack.Add(enemy.Name);

                        fight.Add(String.Empty);
                    }
                }

                round += 1;
            }
        }
    }
}
