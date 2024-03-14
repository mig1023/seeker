using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.MadameGuillotine
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public string Stat { get; set; }
        public string Penalty { get; set; }
        public int Skill { get; set; }
        public int Wounds { get; set; }
        public int Rounds { get; set; }
        public bool FirstBloodOnly { get; set; }

        public List<Character> Enemies { get; set; }
        public bool FireFight { get; set; }

        public override List<string> Representer()
        {
            if (Type == "Test")
            {
                return new List<string> { $"Проверка {Constants.StatNames[Stat]}" };
            }
            else if (!String.IsNullOrEmpty(Stat))
            {
                int currentStat = GetProperty(Character.Protagonist, Stat);
                string diffLine = String.Empty;

                if (currentStat > 11)
                {
                    diffLine = " (максимум)";
                }
                else if (currentStat > 1)
                {
                    diffLine = $" (значение {currentStat})";
                }

                return new List<string> { $"{Head}{diffLine}" };
            }
            else if (!String.IsNullOrEmpty(Head))
            {
                return new List<string> { Head };
            }
            else if (Enemies != null)
            {
                List<string> enemies = new List<string>();

                foreach (Character enemy in Enemies)
                    enemies.Add($"{enemy.Name}\n{enemy.Weapon} {enemy.Skill}  Ранений {enemy.Hitpoints}");

                return enemies;
            }
            else
            {
                return new List<string> { };
            }
        }

        public override List<string> Status() => new List<string>
        {
            $"Ранений: {Character.Protagonist.Wounds} из {Character.Protagonist.Hitpoints}",
        };

        public override List<string> AdditionalStatus() => new List<string>
        {
            $"Сила: {Character.Protagonist.Strength}",
            $"Ловкость: {Character.Protagonist.Agility}",
            $"Удача: {Character.Protagonist.Luck}",
            $"Красноречие: {Character.Protagonist.Speech}",
            $"Стрельба: {Character.Protagonist.Firearms}",
            $"Фехтование: {Character.Protagonist.Fencing}",
            $"Верховая езда: {Character.Protagonist.HorseRiding}",
        };

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
           GameOverBy((Character.Protagonist.Hitpoints - Character.Protagonist.Wounds), out toEndParagraph, out toEndText);

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            if (Type == "Test")
            {
                return true;
            }
            else if (!String.IsNullOrEmpty(Stat))
            {
                int stat = GetProperty(Character.Protagonist, Stat);

                if (secondButton)
                {
                    return stat > 2;
                }
                else
                {
                    return (Character.Protagonist.StatBonuses > 0) && (stat < 12);
                }
            }
            else
            {
                return true;
            }
        }

        private static bool NoMoreEnemies(List<Character> enemies) =>
            enemies.Where(x => x.Wounds < x.Hitpoints).Count() == 0;

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

                foreach (Character enemy in FightEnemies)
                {
                    if (enemy.Wounds >= enemy.Hitpoints)
                        continue;

                    if (!attackAlready)
                    {
                        fight.Add($"Вы атакуете {enemy.Name} ({enemy.Wounds} ранений из {enemy.Hitpoints})");

                        Game.Dice.DoubleRoll(out int firstRoll, out int secondRoll);
                        
                        fight.Add($"Бросок на попадание: " +
                            $"{Game.Dice.Symbol(firstRoll)} + " +
                            $"{Game.Dice.Symbol(secondRoll)}");

                        bool doubleHit = (firstRoll == secondRoll);

                        if (doubleHit && (firstRoll == 1))
                        {
                            fight.Add($"GOOD|Вы выбросили две единицы! {enemy.Name} убит наповал!");
                            enemy.Wounds = enemy.Hitpoints;
                        }
                        else if (firstRoll + secondRoll <= Character.Protagonist.Fencing)
                        {
                            fight.Add($"{firstRoll} + {secondRoll} = {firstRoll + secondRoll} сумма " +
                                $"<= {Character.Protagonist.Fencing}  (фехтование)");

                            fight.Add($"GOOD|Вы ранили {enemy.Name}!");
                            enemy.Wounds += 1;

                            if (FirstBloodOnly)
                            {
                                fight.Add(String.Empty);
                                fight.Add("BIG|BOLD|Дуэль заканчивается с первой кровью");
                                return fight;
                            }
                        }
                        else
                        {
                            fight.Add($"{firstRoll} + {secondRoll} = {firstRoll + secondRoll} сумма " +
                                $"> {Character.Protagonist.Fencing} (фехтование)");

                            fight.Add($"BAD|Вы не смогли ранить {enemy.Name}...");
                        }

                        if (NoMoreEnemies(FightEnemies))
                        {
                            fight.Add(String.Empty);
                            fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");
                            return fight;
                        }
                    }

                    fight.Add($"{enemy.Name} атакует вас (у вас {Character.Protagonist.Wounds} " +
                        $"ранений из {Character.Protagonist.Hitpoints})");

                    Game.Dice.DoubleRoll(out int enemyFirstRoll, out int enemySecondRoll);

                    fight.Add($"Бросок на попадание: " +
                            $"{Game.Dice.Symbol(enemyFirstRoll)} + " +
                            $"{Game.Dice.Symbol(enemySecondRoll)}");

                    bool enemyDoubleHit = (enemyFirstRoll == enemySecondRoll);

                    if (enemyDoubleHit && (enemyFirstRoll == 1))
                    {
                        fight.Add($"BAD|{enemy.Name} выбросил две единицы! Он убил вас!");
                        Character.Protagonist.Wounds = Character.Protagonist.Hitpoints;
                    }
                    else if (enemyFirstRoll + enemySecondRoll <= enemy.Skill)
                    {
                        fight.Add($"{enemyFirstRoll} + {enemySecondRoll} = " +
                            $"{enemyFirstRoll + enemySecondRoll} сумма " +
                            $"<= {enemy.Skill} (фехтование)");

                        fight.Add($"BAD|{enemy.Name} ранил вас...");
                        Character.Protagonist.Wounds += Wounds > 0 ? Wounds : 1;

                        if (FirstBloodOnly)
                        {
                            fight.Add(String.Empty);
                            fight.Add("BIG|BOLD|Дуэль заканчивается с первой кровью");
                            return fight;
                        }
                    }
                    else
                    {
                        fight.Add($"{enemyFirstRoll} + {enemySecondRoll} = " +
                           $"{enemyFirstRoll + enemySecondRoll} сумма " +
                           $"> {enemy.Skill} (фехтования)");

                        fight.Add($"GOOD|{enemy.Name} не смог ранить вас!");
                    }

                    if (Character.Protagonist.Wounds == Character.Protagonist.Hitpoints)
                    {
                        fight.Add(String.Empty);
                        fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");
                        return fight;
                    }

                    attackAlready = true;

                    fight.Add(String.Empty);
                }

                round += 1;

                if ((Rounds < 0) && (round > Rounds))
                {
                    fight.Add(String.Empty);
                    fight.Add("BIG|BOLD|Отведённые на бой раунды кончились");
                    return fight;
                }
            }
        }

        public List<string> Get()
        {
            if (Character.Protagonist.StatBonuses >= 0)
            {
                SetProperty(Character.Protagonist, Stat, GetProperty(Character.Protagonist, Stat) + 1);
                Character.Protagonist.StatBonuses -= 1;
            }

            return new List<string> { "RELOAD" };
        }

        public List<string> Decrease() =>
            ChangeProtagonistParam(Stat, Character.Protagonist, "StatBonuses", decrease: true);

        public List<string> Test()
        {
            List<string> test = new List<string>();

            int level = GetProperty(Character.Protagonist, Stat);
            string stat = Constants.StatNames[Stat];

            if (Skill > 0)
            {
                level = Skill;
            }

            test.Add($"Текущий уровень {stat}: {level}");

            if (!String.IsNullOrEmpty(Penalty))
            {
                test.Add($"GRAY| Пенальти {Penalty} к уровню навыка");
                level += int.Parse(Penalty);
            }

            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);

            bool result = (firstDice + secondDice) <= level;
            string resultCompare = result ? "<=" : ">";

            test.Add($"Проверка: " +
                $"{Game.Dice.Symbol(firstDice)} + {Game.Dice.Symbol(secondDice)} " +
                $"{resultCompare} {level}");

            test.Add(Result(result, "ПРОВЕРКА ПРОЙДЕНА", "ПРОВЕРКА ПРОВЕЛЕНА"));

            Game.Buttons.Disable(result,
                "Всё получилось, Удача с вами, Попали, Проверка прошла успешно, " +
                "Вам удаётся его убедить, Проверка успешна (подстреленный враг падает с повозки)",
                "Проверка провалилась, Удача отвернулась от вас, " +
                "Не попали, Промахнулись, Де ла Валье промахивается");

            if ((Benefit != null) && result)
                Benefit.Do();

            return test;
        }
    }
}
