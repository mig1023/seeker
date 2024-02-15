using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.OctopusIsland
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        public static Actions GetInstance() => StaticInstance;
        private static Character protagonist = Character.Protagonist;

        public List<Character> Enemies { get; set; }
        public int WoundsToWin { get; set; }
        public int DinnerHitpointsBonus { get; set; }
        public bool DinnerAlready { get; set; }
        public bool ReturnedStuffs { get; set; }

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
                enemies.Add($"{enemy.Name}\nловкость {enemy.Skill}  жизни {enemy.Hitpoint}");

            return enemies;
        }

        public override List<string> Status() => new List<string>
        {
            $"Обедов: {protagonist.Food}",
            $"Животворная мазь: {protagonist.LifeGivingOintment}",
        };

        public override List<string> AdditionalStatus() => new List<string>
        {
            $"Серж: {protagonist.SergeSkill}/{protagonist.SergeHitpoint}",
            $"Ксолотл: {protagonist.XolotlSkill}/{protagonist.XolotlHitpoint}",
            $"Тибо: {protagonist.ThibautSkill}/{protagonist.ThibautHitpoint}",
            $"Суи: {protagonist.SouhiSkill}/{protagonist.SouhiHitpoint}",
        };

        public override List<string> StaticButtons()
        {
            List<string> staticButtons = new List<string> { };

            if (protagonist.LifeGivingOintment <= 0)
                return staticButtons;

            if (protagonist.SergeHitpoint < 20)
                staticButtons.Add("ВЫЛЕЧИТЬ СЕРЖА");

            if (protagonist.XolotlHitpoint < 20)
                staticButtons.Add("ВЫЛЕЧИТЬ КСОЛОТЛА");

            if (protagonist.ThibautHitpoint < 20)
                staticButtons.Add("ВЫЛЕЧИТЬ ТИБО");

            if (protagonist.SouhiHitpoint < 20)
                staticButtons.Add("ВЫЛЕЧИТЬ СУИ");

            return staticButtons;
        }

        public override bool StaticAction(string action)
        {
            if (action.Contains("СЕРЖА"))
            {
                protagonist.SergeHitpoint = Ointment.Cure(protagonist.SergeHitpoint);
            }
            else if (action.Contains("КСОЛОТЛА"))
            {
                protagonist.XolotlHitpoint = Ointment.Cure(protagonist.XolotlHitpoint);
            }
            else if (action.Contains("ТИБО"))
            {

                protagonist.ThibautHitpoint = Ointment.Cure(protagonist.ThibautHitpoint);
            }
            else if (action.Contains("СУИ"))
            {
                protagonist.SouhiHitpoint = Ointment.Cure(protagonist.SouhiHitpoint);
            }
            else
            {
                return false;
            }

            return true;
        }

        public override bool IsButtonEnabled(bool secondButton = false) =>
            !((DinnerHitpointsBonus > 0) && ((protagonist.Food <= 0) || DinnerAlready));

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains("|"))
            {
                return option.Split('|').Where(x => Game.Option.IsTriggered(x.Trim())).Count() > 0;
            }
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains("!"))
                    {
                        if (Game.Option.IsTriggered(oneOption.Replace("!", String.Empty).Trim()))
                            return false;
                    }
                    else if (!Game.Option.IsTriggered(oneOption.Trim()))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            int round = 1, enemyWounds = 0;

            Fights.SetCurrentWarrior(ref fight, start: true);

            while (true)
            {
                fight.Add("HEAD|Раунд: {round}");

                foreach (Character enemy in FightEnemies)
                {
                    if (enemy.Hitpoint <= 0)
                        continue;

                    Character enemyInFight = enemy;
                    fight.Add($"{enemy.Name} (жизнь {enemy.Hitpoint})");

                    Game.Dice.DoubleRoll(out int protagonistRollFirst, out int protagonistRollSecond);
                    int protagonistHitStrength = protagonistRollFirst + protagonistRollSecond + protagonist.Skill;

                    fight.Add($"{protagonist.Name}: мощность удара: " +
                        $"{Game.Dice.Symbol(protagonistRollFirst)} + " +
                        $"{Game.Dice.Symbol(protagonistRollSecond)} + " +
                        $"{protagonist.Skill} = {protagonistHitStrength}");

                    Game.Dice.DoubleRoll(out int enemyRollFirst, out int enemyRollSecond);
                    int enemyHitStrength = enemyRollFirst + enemyRollSecond + enemy.Skill;

                    fight.Add($"{enemy.Name}: мощность удара: " +
                        $"{Game.Dice.Symbol(enemyRollFirst)} + " +
                        $"{Game.Dice.Symbol(enemyRollSecond)} + " +
                        $"{enemy.Skill} = {enemyHitStrength}");

                    if (protagonistHitStrength > enemyHitStrength)
                    {
                        fight.Add($"GOOD|{enemy.Name} ранен");

                        enemy.Hitpoint -= 2;
                        enemyWounds += 1;

                        bool enemyLost = Fights.NoMoreEnemies(FightEnemies);

                        if (enemyLost || ((WoundsToWin > 0) && (WoundsToWin <= enemyWounds)))
                        {
                            fight.Add(String.Empty);
                            fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");

                            if (ReturnedStuffs)
                            {
                                fight.Add("GOOD|Вы вернули украденные у вас рюкзаки!");
                                protagonist.StolenStuffs = 0;
                            }

                            Fights.SaveCurrentWarriorHitPoints();

                            return fight;
                        }
                    }
                    else if (protagonistHitStrength < enemyHitStrength)
                    {
                        fight.Add($"BAD|{enemy.Name} ранил {protagonist.Name}");

                        protagonist.Hitpoint -= 2;

                        if (!Fights.SetCurrentWarrior(ref fight))
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
                }

                round += 1;
            }
        }

        public List<string> Dinner()
        {
            protagonist.Food -= 1;

            protagonist.SouhiHitpoint += DinnerHitpointsBonus;
            protagonist.SergeHitpoint += DinnerHitpointsBonus;
            protagonist.ThibautHitpoint += DinnerHitpointsBonus;
            protagonist.XolotlHitpoint += DinnerHitpointsBonus;

            DinnerAlready = true;

            return new List<string> { "RELOAD" };
        }
    }
}
