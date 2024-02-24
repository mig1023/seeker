using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.OctopusIsland
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
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
            $"Обедов: {Character.Protagonist.Food}",
            $"Животворная мазь: {Character.Protagonist.LifeGivingOintment}",
        };

        public override List<string> AdditionalStatus() => new List<string>
        {
            $"Серж: {Character.Protagonist.SergeSkill}/{Character.Protagonist.SergeHitpoint}",
            $"Ксолотл: {Character.Protagonist.XolotlSkill}/{Character.Protagonist.XolotlHitpoint}",
            $"Тибо: {Character.Protagonist.ThibautSkill}/{Character.Protagonist.ThibautHitpoint}",
            $"Суи: {Character.Protagonist.SouhiSkill}/{Character.Protagonist.SouhiHitpoint}",
        };

        public override List<string> StaticButtons()
        {
            List<string> staticButtons = new List<string> { };

            if (Character.Protagonist.LifeGivingOintment <= 0)
                return staticButtons;

            if (Character.Protagonist.SergeHitpoint < 20)
                staticButtons.Add("ВЫЛЕЧИТЬ СЕРЖА");

            if (Character.Protagonist.XolotlHitpoint < 20)
                staticButtons.Add("ВЫЛЕЧИТЬ КСОЛОТЛА");

            if (Character.Protagonist.ThibautHitpoint < 20)
                staticButtons.Add("ВЫЛЕЧИТЬ ТИБО");

            if (Character.Protagonist.SouhiHitpoint < 20)
                staticButtons.Add("ВЫЛЕЧИТЬ СУИ");

            return staticButtons;
        }

        public override bool StaticAction(string action)
        {
            if (action.Contains("СЕРЖА"))
            {
                Character.Protagonist.SergeHitpoint = Ointment.Cure(Character.Protagonist.SergeHitpoint);
            }
            else if (action.Contains("КСОЛОТЛА"))
            {
                Character.Protagonist.XolotlHitpoint = Ointment.Cure(Character.Protagonist.XolotlHitpoint);
            }
            else if (action.Contains("ТИБО"))
            {

                Character.Protagonist.ThibautHitpoint = Ointment.Cure(Character.Protagonist.ThibautHitpoint);
            }
            else if (action.Contains("СУИ"))
            {
                Character.Protagonist.SouhiHitpoint = Ointment.Cure(Character.Protagonist.SouhiHitpoint);
            }
            else
            {
                return false;
            }

            return true;
        }

        public override bool IsButtonEnabled(bool secondButton = false) =>
            !((DinnerHitpointsBonus > 0) && ((Character.Protagonist.Food <= 0) || DinnerAlready));

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
                    int protagonistHitStrength = protagonistRollFirst + protagonistRollSecond + Character.Protagonist.Skill;

                    fight.Add($"{Character.Protagonist.Name}: мощность удара: " +
                        $"{Game.Dice.Symbol(protagonistRollFirst)} + " +
                        $"{Game.Dice.Symbol(protagonistRollSecond)} + " +
                        $"{Character.Protagonist.Skill} = {protagonistHitStrength}");

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
                                Character.Protagonist.StolenStuffs = 0;
                            }

                            Fights.SaveCurrentWarriorHitPoints();

                            return fight;
                        }
                    }
                    else if (protagonistHitStrength < enemyHitStrength)
                    {
                        fight.Add($"BAD|{enemy.Name} ранил {Character.Protagonist.Name}");

                        Character.Protagonist.Hitpoint -= 2;

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
            Character.Protagonist.Food -= 1;

            Character.Protagonist.SouhiHitpoint += DinnerHitpointsBonus;
            Character.Protagonist.SergeHitpoint += DinnerHitpointsBonus;
            Character.Protagonist.ThibautHitpoint += DinnerHitpointsBonus;
            Character.Protagonist.XolotlHitpoint += DinnerHitpointsBonus;

            DinnerAlready = true;

            return new List<string> { "RELOAD" };
        }
    }
}
