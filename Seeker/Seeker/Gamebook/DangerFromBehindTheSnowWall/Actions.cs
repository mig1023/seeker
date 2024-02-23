using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.DangerFromBehindTheSnowWall
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public new static Actions StaticInstance = new Actions();
        public new static Actions GetInstance() => StaticInstance;
        private static Character protagonist = Character.Protagonist;

        public List<Character> Enemies { get; set; }

        public override List<string> Status() => new List<string>
        {
            $"Ловкость: {protagonist.Skill}",
            $"Сила: {protagonist.Strength}/{protagonist.MaxStrength}",
            $"Удар: {protagonist.Damage}",
        };

        public override List<string> AdditionalStatus() => new List<string>
        {
            $"Наблюдательность: {protagonist.Skill}",
            $"Деньги: {MoneyFormat(protagonist.Money)}",
            $"Магия: {protagonist.Magic}",
        };

        private static string MoneyFormat(int ecu) =>
            String.Format("{0:f1}", (double)ecu / 10).TrimEnd('0').TrimEnd(',').Replace(',', '.');

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
                GameOverBy(protagonist.Strength, out toEndParagraph, out toEndText);

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
                        protagonistHitStrength = (protagonistRoll * 2) + protagonist.Skill;

                        fight.Add($"Сила вашей атаки: " +
                            $"{Game.Dice.Symbol(protagonistRoll)} x 2 + " +
                            $"{protagonist.Skill} = {protagonistHitStrength}");
                    }

                    int enemyRoll = Game.Dice.Roll();
                    int enemyHitStrength = (enemyRoll * 2) + enemy.Skill;

                    fight.Add($"Сила его атаки: " +
                        $"{Game.Dice.Symbol(enemyRoll)} x 2 + " +
                        $"{enemy.Skill} = {enemyHitStrength}");

                    if ((protagonistHitStrength > enemyHitStrength) && !attackAlready)
                    {
                        string points = Game.Services.CoinsNoun(protagonist.Damage, "очко", "очка", "очков");
                        fight.Add($"GOOD|{enemy.Name} ранен");
                        fight.Add($"Он теряет {protagonist.Damage} {points} Силы");

                        enemy.Strength -= protagonist.Damage;

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

                        protagonist.Strength -= enemy.Damage;

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

        public List<string> Observation()
        {
            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);

            bool goodLuck = (firstDice + secondDice + protagonist.Observation) > 10;
            string luckLine = goodLuck ? ">" : "<=";

            List<string> observationCheck = new List<string> {
                $"Проверка наблюдательности: {Game.Dice.Symbol(firstDice)} + " +
                $"{Game.Dice.Symbol(secondDice)} + {protagonist.Observation} " +
                $"{luckLine} 10" };

            observationCheck.Add(goodLuck ? "BIG|GOOD|УСПЕХ :)" : "BIG|BAD|НЕУДАЧА :(");

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

            string luckLine = protagonist.Luck[goodLuck] ? "не " : String.Empty;
            luckCheck.Add($"Проверка удачи: {Game.Dice.Symbol(goodLuck)} - {luckLine}является Числом Удачи");

            luckCheck.Add(Result(protagonist.Luck[goodLuck], "УСПЕХ|НЕУДАЧА"));

            return luckCheck;
        }
    }
}
