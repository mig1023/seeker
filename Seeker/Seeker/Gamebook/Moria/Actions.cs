using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.Moria
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public List<string> Enemies { get; set; }

        public override List<string> Status()
        {
            bool canMakeMagic = protagonist.MagicPause == 0;

            string magic = canMakeMagic ? "доступно" :
                $"устал (ещё {protagonist.MagicPause} параграфа)";

            return new List<string> { $"Волшебство Гэндальфа: {magic}" };
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
            string enemy = Enemies[0];

            fight.Add($"BOLD|{hero} сражается против {Declination(enemy, count)}");

            while (frags < count)
            {
                int strength = Constants.Fellowship[hero];
                int dice = Game.Dice.Roll();
                int heroAttack = strength + dice;

                fight.Add($"{hero}: {strength} Сила + {Game.Dice.Symbol(dice)} = {heroAttack}");

                if (dice < lastDice)
                {
                    fight.Add($"{hero} выкинул кубик меньше, чем с предудщим врагом - к сожалению, это смертельно...");
                    DeathInFight(ref fight, hero);
                    return;
                }

                lastDice = dice;

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
                }
            }

            fight.Add(String.Empty);
        }

        private List<string> StrongWarriorsInFellowship() =>
            protagonist.Fellowship.Where(x => Constants.Fellowship[x] > 3).ToList();

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            while ((Enemies.Count > 0) && (protagonist.Fellowship.Count > 0))
            {
                List<string> strongWarriors = StrongWarriorsInFellowship();

                if (Enemies.Count > (strongWarriors.Count * 4))
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
            }

            fight.Add(Result(protagonist.Fellowship.Count > 0, "Вы ПОБЕДИЛИ!|Вы ПРОИГРАЛИ..."));

            return fight;
        }
    }
}
