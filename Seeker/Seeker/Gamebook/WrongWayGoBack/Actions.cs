using Seeker.Game;
using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.WrongWayGoBack
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public Character Enemy { get; set; }

        public override List<string> Status()
        {
            if (Character.Protagonist.Time <= 0)
            {
                return new List<string> { "Отпущенное время истекло..." };
            }
            else
            {
                TimeSpan time = TimeSpan.FromSeconds(Character.Protagonist.Time);

                string hoursEnd = Game.Services.CoinsNoun(time.Hours, String.Empty, "а", "ов");
                string hours = time.Hours > 0 ? $"{time.Hours} час{hoursEnd} " : String.Empty;

                string minutesEnd = Game.Services.CoinsNoun(time.Minutes, "а", "ы", String.Empty);
                string minutes = time.Minutes > 0 ? $"{time.Minutes} минут{minutesEnd} " : String.Empty;

                string secondsEnd = Game.Services.CoinsNoun(time.Seconds, "а", "ы", String.Empty);
                string seconds = $"{time.Seconds} секунд{secondsEnd}";

                return new List<string> { $"Оставшееся время: {hours}{minutes}{seconds}" };
            }
        }

        public override List<string> AdditionalStatus() => new List<string>
        {
            $"Мастерство: {Character.Protagonist.Skill}",
            $"Выносливость: {Character.Protagonist.Hitpoints}",
            $"Удача: {Character.Protagonist.Luck}",
        };

        public override bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 33;
            toEndText = String.Empty;

            if (Game.Data.CurrentParagraphID == 33)
            {
                return false;
            }
            else if (Character.Protagonist.Time <= 0)
            {
                toEndText = "Время истекло...";
                return true;
            }
            else if (Character.Protagonist.Hitpoints <= 0)
            {
                toEndText = "Начать сначала";
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool Availability(string option) =>
            AvailabilityTrigger(option);

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            if (Type == "FirstAidKit")
            {
                return Game.Option.IsTriggered("Аптечка");
            }
            else
            {
                return true;
            }
        }

        public List<string> FirstAidKit()
        {
            Character.Protagonist.Hitpoints += 4;
            Character.Protagonist.Time -= 60;

            Data.Triggers.Remove("Аптечка");

            return new List<string> { "RELOAD" };
        }

        public List<string> Skill()
        {
            List<string> lines = new List<string>();

            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            int result = firstDice + secondDice;

            lines.Add($"Кубики: {Game.Dice.Symbol(firstDice)} + " +
                $"{Game.Dice.Symbol(secondDice)}");

            if (result <= Character.Protagonist.Skill)
            {
                lines.Add($"Выпавшее значение {result} не превышает уровень Мастерства!");
                lines.Add($"BIG|GOOD|BOLD|Проверка успешно пройдена :)");
               
                Game.Buttons.Disable("Промахнулся");
            }
            else
            {
                lines.Add($"Выпавшее значение {result} больше уровня Мастерства!");
                lines.Add($"BIG|BAD|BOLD|Проверка провалена :(");

                Game.Buttons.Disable("Ты попал");
            }

            return lines;
        }

        public List<string> Luck()
        {
            List<string> lines = new List<string>();

            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            int result = firstDice + secondDice;

            lines.Add($"Кубики: {Game.Dice.Symbol(firstDice)} + " +
                $"{Game.Dice.Symbol(secondDice)}");

            if (result <= Character.Protagonist.Luck)
            {
                lines.Add($"Выпавшее значение {result} не превышает уровень Удачи!");
                lines.Add($"BIG|GOOD|BOLD|Удача на вашей стороне :)");

                Game.Buttons.Disable("Она подвела тебя");
            }
            else
            {
                lines.Add($"Выпавшее значение {result} больше уровня Удачи!");
                lines.Add($"BIG|BAD|BOLD|Удача отвернулась от вас :(");

                Game.Buttons.Disable("Она на твоей стороне");
            }

            if (Character.Protagonist.Luck > 2)
            {
                Character.Protagonist.Luck -= 1;
                lines.Add("Уровень Удачи снижен на единицу");
            }

            return lines;
        }

        public override List<string> Representer()
        {
            List<string> enemy = new List<string>();

            if (Enemy == null)
                return enemy;

            enemy.Add($"{Enemy.Name}\nмастерство {Enemy.Skill}  выносливость {Enemy.Hitpoints}");

            return enemy;
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();
            Character enemy = Enemy.Clone();

            int round = 1;

            while (true)
            {
                fight.Add($"HEAD|BOLD|Раунд: {round}");
                fight.Add($"{enemy.Name} (выносливость {enemy.Hitpoints})");

                Game.Dice.DoubleRoll(out int heroRollFirst, out int heroRollSecond);
                int heroHitStrength = heroRollFirst + heroRollSecond + Character.Protagonist.Skill;

                fight.Add($"Сила твоей атаки: " +
                    $"{Game.Dice.Symbol(heroRollFirst)} + " +
                    $"{Game.Dice.Symbol(heroRollSecond)} + " +
                    $"{Character.Protagonist.Skill} = {heroHitStrength}");

                if (heroRollFirst == heroRollSecond)
                {
                    fight.Add($"GOOD|Ты выкинул дубль!!");
                    fight.Add(String.Empty);
                    fight.Add("BIG|GOOD|Ты ПОБЕДИЛ :)");
                    return fight;
                }

                Game.Dice.DoubleRoll(out int enemyRollFirst, out int enemyRollSecond);
                int enemyHitStrength = enemyRollFirst + enemyRollSecond + enemy.Skill;

                fight.Add($"Сила его атаки: " +
                    $"{Game.Dice.Symbol(enemyRollFirst)} + " +
                    $"{Game.Dice.Symbol(enemyRollSecond)} + " +
                    $"{enemy.Skill} = {enemyHitStrength}");

                if (enemyRollFirst == enemyRollSecond)
                {
                    fight.Add($"BAD|Он выкинул дубль!!");
                    fight.Add(String.Empty);
                    fight.Add("BIG|BAD|Ты ПРОИГРАЛ :(");
                    return fight;
                }

                if (heroHitStrength > enemyHitStrength)
                {
                    fight.Add($"GOOD|{enemy.Name} ранен");

                    if (Game.Option.IsTriggered("Сабля"))
                    {
                        fight.Add("От удара парадной офицерской сабли он теряет 4 очка Выносливости");
                        enemy.Hitpoints -= 4;
                    }
                    else if (Game.Option.IsTriggered("Кирка"))
                    {
                        fight.Add("От удара кирки он теряет 3 очка Выносливости");
                        enemy.Hitpoints -= 3;
                    }
                    else
                    {
                        fight.Add("Он теряет 2 очка Выносливости");
                        enemy.Hitpoints -= 2;
                    }

                    if (enemy.Hitpoints <= 2)
                    {
                        fight.Add(String.Empty);
                        fight.Add("BIG|GOOD|Ты ПОБЕДИЛ :)");
                        return fight;
                    }
                }
                else if (heroHitStrength < enemyHitStrength)
                {
                    fight.Add($"BAD|{enemy.Name} ранил тебя");
                    fight.Add("Ты теряешь 2 очка Выносливости");

                    Character.Protagonist.Hitpoints -= 2;

                    if (Character.Protagonist.Hitpoints <= 2)
                    {
                        fight.Add(String.Empty);
                        fight.Add("BIG|BAD|Ты ПРОИГРАЛ :(");
                        return fight;
                    }
                }
                else
                {
                    fight.Add("BOLD|Ничья в раунде");
                }

                fight.Add("GRAY|+10 секунд времени прошло");
                fight.Add(String.Empty);

                Character.Protagonist.Time -= 10;
                round += 1;
            }
        }
    }
}
