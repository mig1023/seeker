using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.AlamutFortress
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public List<Character> Enemies { get; set; }

        public override List<string> Status() => new List<string>
        {
            $"Сила: {protagonist.Strength}",
            $"Здоровье: {protagonist.Hitpoints}/{protagonist.MaxHitpoints}",
            $"Золото: {protagonist.Gold}"
        };

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
                enemies.Add($"{enemy.Name}\nсила {enemy.Strength}  здоровье {enemy.Hitpoints}");

            return enemies;
        }

        public static bool NoMoreEnemies(List<Character> enemies) =>
            enemies.Where(x => x.Hitpoints > 0).Count() == 0;

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

                    fight.Add($"{enemy.Name} (здоровье {enemy.Hitpoints})");

                    if (!attackAlready)
                    {
                        Game.Dice.DoubleRoll(out int protagonistRollFirst, out int protagonistRollSecond);
                        protagonistHitStrength = protagonistRollFirst + protagonistRollSecond + protagonist.Strength;

                        fight.Add($"Сила вашей атаки: " +
                            $"{Game.Dice.Symbol(protagonistRollFirst)} + " +
                            $"{Game.Dice.Symbol(protagonistRollSecond)} + " +
                            $"{protagonist.Strength} = {protagonistHitStrength}");
                    }

                    Game.Dice.DoubleRoll(out int enemyRollFirst, out int enemyRollSecond);
                    int enemyHitStrength = enemyRollFirst + enemyRollSecond + enemy.Strength;

                    fight.Add($"Сила его атаки: " +
                        $"{Game.Dice.Symbol(enemyRollFirst)} + " +
                        $"{Game.Dice.Symbol(enemyRollSecond)} + " +
                        $"{enemy.Strength} = {enemyHitStrength}");

                    int hitPointsLoses = protagonistHitStrength - enemyHitStrength;
                    string losesLine = Game.Services.CoinsNoun(Math.Abs(hitPointsLoses), "очко", "очка", "очков");

                    if ((hitPointsLoses > 0) && !attackAlready)
                    {
                        fight.Add($"GOOD|{enemy.Name} ранен");
                        fight.Add($"Он теряет {hitPointsLoses} {losesLine} Здоровья");

                        enemy.Hitpoints -= hitPointsLoses;

                        bool enemyLost = NoMoreEnemies(FightEnemies);

                        if (enemyLost)
                        {
                            fight.Add(String.Empty);
                            fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");
                            return fight;
                        }
                    }
                    else if (hitPointsLoses > 0)
                    {
                        fight.Add($"BOLD|{enemy.Name} не смог вас ранить");
                    }
                    else if (hitPointsLoses < 0)
                    {
                        fight.Add($"BAD|{enemy.Name} ранил вас");
                        fight.Add($"Он теряет {Math.Abs(hitPointsLoses)} {losesLine} Здоровья");

                        protagonist.Hitpoints += hitPointsLoses;

                        if (protagonist.Strength <= 0)
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
