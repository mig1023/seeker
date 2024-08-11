using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.ColdHeartOfDalrok
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public int RoundsToWin { get; set; }
        public bool HeroWoundsLimit { get; set; }

        public List<Character> Enemies { get; set; }

        public override List<string> Status() => new List<string>
        {
            $"Ловкость: {Character.Protagonist.Skill}",
            $"Сила: {Character.Protagonist.Strength}/{Character.Protagonist.MaxStrength}",
            $"Обаяние: {Character.Protagonist.Charm}",
        };

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(Character.Protagonist.Strength, out toEndParagraph, out toEndText);

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            if (Type == "Get")
            {
                if (Character.Protagonist.BonusesAvailability <= 0)
                {
                    return false;
                }
                else if (Game.Option.IsTriggered(Head))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option == "Стрельба из лука")
            {
                bool skill = Game.Option.IsTriggered(option);
                bool arrows = Character.Protagonist.Arrows > 0;

                return skill && arrows;
            }
            else
            {
                return AvailabilityTrigger(option);
            }
        }

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (!String.IsNullOrEmpty(Head))
                return new List<string> { Head };

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
            {
                string loyalty = enemy.Loyalty > 0 ? $"  верность {enemy.Loyalty}" : String.Empty;
                enemies.Add($"{enemy.Name}\nловкость {enemy.Skill}  сила {enemy.Strength}{loyalty}");
            }

            return enemies;
        }

        public List<string> Luck()
        {
            List<string> luckCheck = new List<string>
            {
                "Цифры удачи:",
                "BIG|" + Luckiness.Numbers()
            };

            int goodLuck = Game.Dice.Roll();
            bool isLuck = Character.Protagonist.Luck[goodLuck];
            string not = isLuck ? "не " : String.Empty;

            luckCheck.Add($"Проверка удачи: {Game.Dice.Symbol(goodLuck)} - {not}зачёркунтый");

            luckCheck.Add(Result(isLuck, "УСПЕХ", "НЕУДАЧА"));

            string luck = "Боги не отвернулись от вас, Повезло, Удачливы, Фортуна за вас";
            string fail = "Вы неудачливы, Нет, Она хмурится и отворачивается, Нет - вы промахнулись";

            Game.Buttons.Disable(isLuck, luck, fail);

            Character.Protagonist.Luck[goodLuck] = !Character.Protagonist.Luck[goodLuck];

            return luckCheck;
        }

        public List<string> LuckRecovery()
        {
            List<string> luckRecovery = new List<string> { "Восстановление удачи:" };

            bool success = false;

            for (int i = 1; i < 7; i++)
            {
                if (!Character.Protagonist.Luck[i])
                {
                    luckRecovery.Add($"GOOD|Цифра {i} восстановлена!");
                    Character.Protagonist.Luck[i] = true;
                    success = true;

                    break;
                }
            }

            if (!success)
                luckRecovery.Add("BAD|Все цифры и так счастливые!");

            luckRecovery.Add("Цифры удачи теперь:");
            luckRecovery.Add("BIG|" + Luckiness.Numbers());

            return luckRecovery;
        }

        public List<string> Charm()
        {
            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            bool goodCharm = (firstDice + secondDice) <= Character.Protagonist.Charm;
            string charmLine = goodCharm ? "<=" : ">";

            List<string> luckCheck = new List<string> {
                $"Проверка обаяния: " +
                $"{Game.Dice.Symbol(firstDice)} + " +
                $"{Game.Dice.Symbol(secondDice)} " +
                $"{charmLine} {Character.Protagonist.Charm}" };

            if (goodCharm)
            {
                luckCheck.Add("BIG|GOOD|УСПЕХ :)");

                if (Character.Protagonist.Charm < 12)
                {
                    luckCheck.Add("Вы увеличили своё обаяние на единицу");
                    Character.Protagonist.Charm += 1;
                }

                Game.Buttons.Disable("Нет");
            }
            else
            {
                luckCheck.Add("BIG|BAD|НЕУДАЧА :(");

                if (Character.Protagonist.Charm > 2)
                {
                    luckCheck.Add("Вы уменьшили своё обаяние на единицу");
                    Character.Protagonist.Charm -= 1;
                }

                Game.Buttons.Disable("Все в порядке");
            }

            return luckCheck;
        }

        public List<string> Break()
        {
            List<string> breakingDoor = new List<string> { "Ломаете дверь:" };

            bool succesBreaked = false;

            while (!succesBreaked && (Character.Protagonist.Strength > 0))
            {
                Game.Dice.DoubleRoll(out int firstDice, out int secondDice);

                if (firstDice == secondDice)
                {
                    succesBreaked = true;
                }
                else
                {
                    Character.Protagonist.Strength -= 1;
                }

                string result = succesBreaked ?
                    "удачный, дверь поддалась!" : "неудачный, -1 сила";

                breakingDoor.Add($"Удар: " +
                    $"{Game.Dice.Symbol(firstDice)} + " +
                    $"{Game.Dice.Symbol(secondDice)} = {result}");
            }

            breakingDoor.Add(Result(succesBreaked, "ДВЕРЬ ВЗЛОМАНА", "ВЫ УБИЛИСЬ ОБ ДВЕРЬ"));

            return breakingDoor;
        }

        public List<string> Get()
        {
            Game.Option.Trigger(Head);
            Character.Protagonist.BonusesAvailability -= 1;

            if (Head == "Стрельба из лука")
            {
                Character.Protagonist.Arrows = 5;
            }

            return new List<string> { "RELOAD" };
        }

        private static bool IsAlive(Character enemy)
        {
            if (enemy.Strength > 0)
            {
                if (enemy.Loyalty == null)
                {
                    return true;
                }
                else if (enemy.Loyalty > 3)
                {
                    return true;
                }
            }
            
            return false;
        }

        private static bool NoMoreEnemies(List<Character> enemies) =>
            enemies.Where(x => IsAlive(x)).Count() == 0;

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
                    if (!IsAlive(enemy))
                        continue;

                    fight.Add($"{enemy.Name} (сила {enemy.Strength})");
                    enemy.RoundWithoutSuccess += 1;

                    if (!attackAlready)
                    {
                        int dice = Game.Dice.Roll();
                        protagonistHitStrength = Character.Protagonist.Skill + (dice * 2);

                        fight.Add($"Мощность вашего удара: " +
                            $"{Game.Dice.Symbol(dice)} x 2 + " +
                            $"{Character.Protagonist.Skill} = {protagonistHitStrength}");
                    }

                    int enemyDice = Game.Dice.Roll();
                    int enemyHitStrength = enemy.Skill + (enemyDice * 2);

                    fight.Add($"Мощность его удара: " +
                        $"{Game.Dice.Symbol(enemyDice)} x 2 + " +
                        $"{enemy.Skill} = {enemyHitStrength}");

                    if ((protagonistHitStrength > enemyHitStrength) && !attackAlready)
                    {
                        fight.Add($"GOOD|{enemy.Name} ранен");

                        enemy.Strength -= 2;

                        if ((enemy.Loyalty != null) && IsAlive(enemy))
                        {
                            enemy.Loyalty -= 2;
                            fight.Add($"GRAY|Его верность снизилась на 2 единицы и теперь равна {enemy.Loyalty}");

                            if (enemy.Loyalty <= 3)
                                fight.Add($"GOOD|{enemy.Name} обращается в позорное бегство :)");
                        }

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

                        bool loyaltyBonus = enemy.Loyalty >= 18;

                        if (loyaltyBonus)
                        {
                            Character.Protagonist.Strength -= 3;
                            fight.Add($"BAD|Его верность так высока, что ранение обошлось вам в 3 силы!");
                        }
                        else
                        {
                            Character.Protagonist.Strength -= 2;
                        }
                        
                        bool limit = HeroWoundsLimit && (Character.Protagonist.Strength <= 2);

                        if ((Character.Protagonist.Strength <= 0) || limit)
                        {
                            fight.Add(String.Empty);
                            fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");
                            return fight;
                        }

                        if (enemy.Loyalty != null)
                        {
                            enemy.RoundWithoutSuccess = 0;
                            enemy.Loyalty += 1;

                            fight.Add($"GRAY|Он нанёс вам ранение и взбодрился - " +
                                $"его верность повысилась на 1 единицу и теперь равна {enemy.Loyalty}");
                        }
                    }
                    else
                    {
                        fight.Add("BOLD|Ничья в раунде");
                    }

                    attackAlready = true;

                    if ((enemy.Loyalty != null) && IsAlive(enemy) && (enemy.RoundWithoutSuccess > 1))
                    {
                        enemy.Loyalty -= 1;
                        enemy.RoundWithoutSuccess = 0;

                        fight.Add($"GRAY|Он не смог вас ранить уже два раунда - " +
                            $"его верность снизилась на 1 единицу и теперь равна {enemy.Loyalty}");

                        if (enemy.Loyalty <= 3)
                            fight.Add($"GOOD|{enemy.Name} обращается в позорное бегство :)");

                        if (NoMoreEnemies(FightEnemies))
                        {
                            fight.Add(String.Empty);
                            fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");
                            return fight;
                        }
                    }

                    if ((RoundsToWin > 0) && (RoundsToWin <= round))
                    {
                        fight.Add(String.Empty);
                        fight.Add("BAD|Отведённые на победу раунды истекли.");
                        fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");
                        return fight;
                    }

                    fight.Add(String.Empty);
                }

                round += 1;
            }
        }

        public override bool IsHealingEnabled() =>
            Character.Protagonist.Strength < Character.Protagonist.MaxStrength;

        public override void UseHealing(int healingLevel) =>
            Character.Protagonist.Strength += healingLevel;
    }
}