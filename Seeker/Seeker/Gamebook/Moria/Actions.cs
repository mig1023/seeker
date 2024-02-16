using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.Moria
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public new static Actions StaticInstance = new Actions();
        public new static Actions GetInstance() => StaticInstance;
        private static Character protagonist = Character.Protagonist;

        public List<string> Enemies { get; set; }

        public override List<string> Status()
        {
            if (!protagonist.Fellowship.Contains("Гэндальф"))
            {
                return new List<string> { "Гэндальф погиб..." };
            }
            else if (protagonist.MagicPause > 0)
            {
                return new List<string> { $"Гэндальф устал (ещё {protagonist.MagicPause} параграфа)" };
            }
            else
            {
                return new List<string> { "Гэндальф готов применять магию" };
            }
        }

        public override List<string> AdditionalStatus()
        {
            List<string> fellowship = Constants.Fellowship
                .OrderByDescending(x => x.Value)
                .Select(x => x.Key)
                .ToList();

            List<string> actualFellowship = new List<string>();

            foreach (string person in fellowship)
            {
                bool stillAlive = protagonist.Fellowship.Contains(person);
                actualFellowship.Add(stillAlive ? person : $"CROSSEDOUT|{person}");
            }

            return actualFellowship;
        }

        public override List<string> Representer()
        {
            if (Enemies == null)
                return new List<string>();

            string name = Enemies[0];
            int strength = Constants.Enemies[name];
            int count = Enemies.Count;
            string line = Game.Services.CoinsNoun(count, "штук", "штуки", "штук");

            return new List<string> { $"{name}\n{strength} сила каждого, всего {count} {line}" };
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(protagonist.Fellowship.Count, out toEndParagraph, out toEndText);

        private string Declination(string enemy, int count)
        {
            List<string> declin = Constants.Declination[enemy]
                .Split(',')
                .Select(x => x.Trim())
                .ToList();

            string line = Game.Services.CoinsNoun(count, declin[0], declin[1], declin[1]);

            return $"{count} {line}";
        }

        private void DeathInFight(ref List<string> fight, string hero)
        {
            fight.Add($"BAD|BOLD|{hero} погиб в бою!");
            fight.Add(String.Empty);
            protagonist.Fellowship.Remove(hero);
        }

        private void PartOfFight(ref List<string> fight, string hero, int count)
        {
            int frags = 0;
            int lastDice = 0;
            bool secondRound = false;
            string enemy = Enemies[0];

            fight.Add($"BOLD|{hero} сражается против {Declination(enemy, count)}");

            while (frags < count)
            {
                int strength = Constants.Fellowship[hero];
                int dice = Game.Dice.Roll();
                int heroAttack = strength + dice;

                fight.Add($"{hero}: {strength} Сила + {Game.Dice.Symbol(dice)} = {heroAttack}");

                if ((dice < lastDice) && !secondRound)
                {
                    fight.Add($"{hero} выкинул кубик меньше, чем с предудщим врагом - к сожалению, это смертельно...");
                    DeathInFight(ref fight, hero);
                    return;
                }

                lastDice = dice;
                secondRound = false;

                strength = Constants.Enemies[enemy];
                dice = Game.Dice.Roll();
                int enemyAttack = strength + dice;

                fight.Add($"{enemy}: {strength} Сила + {Game.Dice.Symbol(dice)} = {enemyAttack}");

                if (heroAttack > enemyAttack)
                {
                    fight.Add($"GOOD|BOLD|{hero} победил!");
                    Enemies.Remove(enemy);
                    frags += 1;
                }
                else if (heroAttack < enemyAttack)
                {
                    DeathInFight(ref fight, hero);
                    return;
                }
                else
                {
                    fight.Add("BOLD|Ничья! Силы противников равны! Сейчас они сойдутся ещё раз!");
                    secondRound = true;
                }
            }

            fight.Add(String.Empty);
        }

        private List<string> StrongWarriorsInFellowship() =>
            protagonist.Fellowship.Where(x => Constants.Fellowship[x] > 3).ToList();

        private bool IsStillSomeoneToFight() =>
            (Enemies.Count > 0) && (protagonist.Fellowship.Count > 0);

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            while (IsStillSomeoneToFight())
            {
                List<string> strongWarriors = StrongWarriorsInFellowship();

                if (Enemies.Count <= (strongWarriors.Count * 3))
                {
                    fight.Add($"GRAY|Врагов не так уж много, поэтому против них выходят сильные войны!");
                    fight.Add(String.Empty);

                    int countForEach = Enemies.Count / strongWarriors.Count;

                    foreach (string warrior in strongWarriors)
                        PartOfFight(ref fight, warrior, countForEach);
                }
                else
                {
                    fight.Add($"GRAY|Враги бесчисленны, сразиться придётся каждому!!");
                    fight.Add(String.Empty);

                    int countForEach = Enemies.Count / protagonist.Fellowship.Count;
                    List<string> allWarriors = new List<string>(protagonist.Fellowship);

                    foreach (string warrior in allWarriors)
                        PartOfFight(ref fight, warrior, countForEach);
                }

                if (IsStillSomeoneToFight())
                {
                    fight.Add($"GRAY|Осталось ещё {Declination(Enemies[0], Enemies.Count)}!");
                }
            }

            fight.Add(Result(protagonist.Fellowship.Count > 0, "Вы ПОБЕДИЛИ!|Вы ПРОИГРАЛИ..."));

            return fight;
        }

        public List<string> Goodluck()
        {
            List<string> diceCheck = new List<string>();

            int dice = Game.Dice.Roll();
            bool coin = dice % 2 == 0;

            if (coin)
            {
                diceCheck.Add("На кубике выпал: Орел");
                diceCheck.Add("BIG|GOOD|BOLD|Удача на вашей стороне! :)");
            }
            else
            {
                diceCheck.Add("На кубике выпала: Решка");
                diceCheck.Add("BIG|BAD|BOLD|Удача отвернулась от вас! :(");
            }

            return diceCheck;
        }

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains("GandalfMagic"))
            {
                if (!protagonist.Fellowship.Contains("Гэндальф"))
                    return false;
                else
                    return protagonist.MagicPause == 0;
            }
            else
            {
                return AvailabilityTrigger(option);
            }
        }

        public List<string> DeathsByArrows()
        {
            List<string> deaths = new List<string>();

            for (int i = 0; i < 2; i++)
            {
                if (protagonist.Fellowship.Count < 1)
                    continue;

                int dice = Game.Dice.Roll(size: protagonist.Fellowship.Count) - 1;
                string name = protagonist.Fellowship[dice];

                deaths.Add($"BIG|BAD|BOLD|Погиб {name}! :(");

                protagonist.Fellowship.Remove(name);
            }

            return deaths;
        }

        public List<string> Balrog()
        {
            List<string> fight = new List<string>();

            int strength = Constants.Fellowship["Гэндальф"];
            int success = 0;

            fight.Add("BOLD|Гэндальф сражается против Балрога");
            fight.Add(String.Empty);

            for (int i = 1; i <= 4; i++)
            {
                if (success >= 3)
                    continue;

                fight.Add($"Раунд {i}");

                int gendalfDice = Game.Dice.Roll();

                fight.Add($"Бросок Гэндальфа: {Game.Dice.Symbol(gendalfDice)}");

                if (gendalfDice > 4)
                {
                    fight.Add("BOLD|GOOD|Гэндальф успешно нанёс ранение Балрогу!");
                    success += 1;
                }
                else
                {
                    fight.Add("BOLD|BAD|Гэндальф не смог ранить Балрога!");
                }

                if (success >= 3)
                    continue;

                int balrogDice = Game.Dice.Roll();

                fight.Add($"Бросок Балрога: {Game.Dice.Symbol(balrogDice)}");

                if (balrogDice >= strength)
                {
                    fight.Add("BOLD|BAD|Балрог наносит смертельный удар!");
                    fight.Add("BOLD|BIG|BAD|Гэндальф погиб :(");
                    protagonist.Fellowship.Remove("Гэндальф");

                    return fight;
                }
                else
                {
                    fight.Add($"BOLD|GOOD|Балрог не смог ранить Гэндальфа!");
                }

                fight.Add(String.Empty);
            }

            if (success >= 3)
            {
                fight.Add("BIG|GOOD|Гэндальф ПОБЕДИЛ :)");
            }
            else
            {
                fight.Add("BIG|GOOD|Гэндальф не смог победить, Балрога, " +
                    "но, тем не менее, он продержался все 4 раунда! :)");
            }

            return fight;
        }

        public List<string> Cast()
        {
            List<string> fight = new List<string>();

            fight.Add("BOLD|Гэндальф творит свои заклятья");
            fight.Add(String.Empty);

            int prev = 0;
            List<string> rounds = new List<string> { "Первый", "Второй", "Третий" };

            for (int i = 1; i <= 3; i++)
            {
                int dice = Game.Dice.Roll();

                fight.Add($"{rounds[i - 1]} бросок Гэндальфа: {Game.Dice.Symbol(dice)}");

                if (dice < prev)
                {
                    fight.Add("BAD|Это меньше предыдущего броска!");
                    fight.Add("BIG|BAD|Волшебство провалено :(");
                    return fight;
                }
                else if (prev > 0)
                {
                    fight.Add("GOOD|Это больше предыдущего броска!");
                }

                prev = dice;
            }

            fight.Add("BIG|GOOD|Волшебство преуспело :)");
            return fight;
        }

        public List<string> RunningUnderArrows()
        {
            List<string> deaths = new List<string>();
            List<string> fellowship = new List<string>(protagonist.Fellowship);

            foreach (string warrior in fellowship)
            {
                if ((warrior == "Гэндальф") || (warrior == "Фродо"))
                    continue;

                deaths.Add($"BOLD|Свою судьбу испытывает {warrior}");

                int dice = Game.Dice.Roll();
                bool coin = dice % 2 == 0;

                if (coin)
                {
                    deaths.Add("На кубике выпал: Орел");
                    deaths.Add("GOOD|Ему повезло!");
                }
                else
                {
                    deaths.Add("На кубике выпала: Решка");
                    deaths.Add("BAD|Удача отвернулась от него!");
                    deaths.Add("BAD|BOLD|Стрела орка его настигла...");

                    protagonist.Fellowship.Remove(warrior);
                }

                deaths.Add(String.Empty);
            }

            return deaths;
        }
    }
}
