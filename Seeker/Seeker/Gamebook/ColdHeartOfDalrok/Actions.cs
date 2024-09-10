using Seeker.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Seeker.Gamebook.ColdHeartOfDalrok
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public int RoundsToWin { get; set; }
        public bool HeroWoundsLimit { get; set; }
        public bool LastIsDoomed { get; set; }
        public bool LastRunsAway { get; set; }
        public bool LastIsDejected { get; set; }
        public int HeroDamage { get; set; }
        public int EnemyDamage { get; set; }
        public int Dices { get; set; }
        public string Success { get; set; }
        public int DeathDice { get; set; }
        public int StrengthPenalty { get; set; }
        public int SkillPenalty { get; set; }
        public bool DriadStrength { get; set; }

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
                string strength = DriadStrength ? "???" : enemy.Strength.ToString();
                string loyalty = enemy.Loyalty > 0 ? $"  верность {enemy.Loyalty}" : String.Empty;
                enemies.Add($"{enemy.Name}\nловкость {enemy.Skill}   сила {strength}{loyalty}");
            }

            return enemies;
        }

        public List<string> Luck()
        {
            string luck = "Боги не отвернулись от вас, Повезло, Удачливы, Фортуна за вас";
            string fail = "Вы неудачливы, Нет, Она хмурится и отворачивается, Нет - вы промахнулись";

            if (Game.Option.IsTriggered("Месть Темеса"))
            {
                Game.Buttons.Disable(luck);
                Game.Option.Trigger("Месть Темеса", remove: true);
                
                return new List<string>
                {
                    "Бог войны Темес припомнил вам обиду...",
                    "BIG|BAD|Вас постигла НЕУДАЧА :("
                };
            }

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

            Game.Buttons.Disable(isLuck, luck, fail);

            Character.Protagonist.Luck[goodLuck] = !Character.Protagonist.Luck[goodLuck];

            return luckCheck;
        }

        public List<string> LuckRecovery()
        {
            List<string> luckRecovery = new List<string> { "ВОССТАНОВЛЕНИЕ УДАЧИ:\n" };

            bool success = Luckiness.Recovery(luckRecovery);

            if (!success)
                luckRecovery.Add("BAD|Все цифры и так счастливые!");

            luckRecovery.Add("BIG|Цифры удачи теперь:");
            luckRecovery.Add("BIG|" + Luckiness.Numbers());

            return luckRecovery;
        }

        public List<string> DicesLuckRecovery()
        {
            List<string> luckRecovery = new List<string> { "ВОССТАНОВЛЕНИЕ УДАЧИ:\n" };

            Luckiness.DicesRecovery(luckRecovery);

            luckRecovery.Add(String.Empty);
            luckRecovery.Add("BIG|Цифры удачи теперь:");
            luckRecovery.Add("BIG|" + Luckiness.Numbers());

            return luckRecovery;
        }
        
        public List<string> FullLuckRecovery()
        {
            List<string> luckRecovery = new List<string> { "ВОССТАНОВЛЕНИЕ УДАЧИ:\n" };

            Luckiness.FullDicesRecovery(luckRecovery);

            luckRecovery.Add(String.Empty);
            luckRecovery.Add("BIG|Цифры удачи теперь:");
            luckRecovery.Add("BIG|" + Luckiness.Numbers());

            return luckRecovery;
        }

        public List<string> LuckLose()
        {
            List<string> luckLose = new List<string> { "Потеря удачи:" };

            bool success = Luckiness.Lose(luckLose);

            if (success)
                luckLose.Add("GOOD|Все цифры и так несчастливые!\nВам повезло хоть в чём-то!");

            luckLose.Add("Цифры удачи теперь:");
            luckLose.Add("BIG|" + Luckiness.Numbers());

            return luckLose;
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

        public List<string> DiceWounds()
        {
            List<string> diceCheck = new List<string> { };

            int dices = 0;
            int count = Dices > 0 ? Dices : 1;

            for (int i = 1; i <= dices; i++)
            {
                int dice = Game.Dice.Roll();
                dices += dice;

                string diceLine = count == 1 ? String.Empty : $"на {i} ";
                diceCheck.Add($"На {diceLine}кубике выпало: {Game.Dice.Symbol(dice)}");

                if (dice == DeathDice)
                {
                    diceCheck.Add($"BIG|BAD|Ох, как не повезло!");
                    diceCheck.Add($"BAD|Вы убиты наповал :(");

                    Character.Protagonist.Strength = 0;
                    return diceCheck;
                }
            }

            Character.Protagonist.Strength -= dices;

            string line = Game.Services.CoinsNoun(dices, "силу", "силы", "сил");
            diceCheck.Add($"BIG|BAD|Вы потеряли {dices} {line}");

            return diceCheck;
        }

        public List<string> Break()
        {
            Random rand = new Random();

            List<string> breaking = new List<string> { "BIG|ПЫТАЕМСЯ:", String.Empty };

            List<int> success = Success
                .Split(',')
                .Select(x => int.Parse(x))
                .ToList();

            int count = 0;

            while (true)
            {
                int dice = Game.Dice.Roll();
                count += 1;

                breaking.Add($"BOLD|Попытка {count}: {Game.Dice.Symbol(dice)}");
                breaking.Add("GRAY|За эту попытку вы теряете 1 СИЛУ...");

                Character.Protagonist.Strength -= 1;

                if (Character.Protagonist.Strength <= 0)
                {
                    breaking.Add("BIG|BAD|Вы убились в процессе... :(");
                }
                else if (success.Contains(dice))
                {
                    breaking.Add("BIG|BOLD|GOOD|ПОЛУЧИЛОСЬ!");
                    return breaking;
                }
                else
                {
                    string fail = Constants.Fails[rand.Next(Constants.Fails.Count)];
                    breaking.Add($"{fail}...");
                }
            }
        }

        public List<string> Forest()
        {
            List<string> forest = new List<string> { "BIG|ПУТЬ ЧЕРЕЗ ЛЕС:", String.Empty };

            for (int i = 0; i < 6; i += 1)
            {
                int dice = Game.Dice.Roll();

                forest.Add($"BOLD|Бросок {i + 1}: {Game.Dice.Symbol(dice)}");

                if (dice == 3)
                {
                    Character.Protagonist.Strength = 0;

                    forest.Add($"BIG|BAD|Выпала тройка - вам уже не уйти живым от деревьев-убийц...");

                    return forest;
                }
                else if ((dice == 1) || (dice == 6))
                {
                    forest.Add("Вы успешно уклонились от смертоносных сосулек!");
                }
                else
                {
                    Character.Protagonist.Strength -= dice;

                    string line = Game.Services.CoinsNoun(dice, "СИЛУ", "СИЛЫ", "СИЛ");
                    forest.Add($"BAD|Вы теряете {dice} {line} от попавших сосулек...");
                }
            }

            forest.Add(String.Empty);

            if (Character.Protagonist.Strength > 0)
            {
                forest.Add("BIG|GOOD|Вам успешно удалось пройти через лес :)");
            }
            else
            {
                forest.Add("BIG|BAD|Вам не удалось пройти через лес :(");
            }

            return forest;
        }

        public List<string> SeaBattle()
        {
            List<string> battle = new List<string> { };

            int heroShip = 16;
            int enemyShip = 16;

            for (int i = 1; i <= 5; i++)
            {
                battle.Add($"HEAD|BOLD|Раунд {i}");

                Game.Dice.DoubleRoll(out int heroDice, out int enemyDice);

                battle.Add($"Выстрел вашего корабля: {Game.Dice.Symbol(heroDice)}");
                battle.Add($"Выстрел корабля противника: {Game.Dice.Symbol(enemyDice)}");

                if (heroDice > enemyDice)
                {
                    enemyShip -= 4;

                    battle.Add("GOOD|BOLD|Ваш огненный шар поразил вражеский корабль!");
                    battle.Add($"GRAY|У вражеского корабля осталось {enemyShip} ед. прочности");
                }
                else if (heroDice < enemyDice)
                {
                    heroShip -= 4;

                    battle.Add("BAD|BOLD|Вражеский огненный шар поразил ваш корабль!");
                    battle.Add($"GRAY|У вашего корабля осталось {heroShip} ед. прочности");
                }
                else
                {
                    battle.Add("BOLD|Промах!");
                }

                if (enemyShip <= 0)
                {
                    battle.Add(String.Empty);
                    battle.Add($"GOOD|Вражеский корабль пошёл ко дну!");
                    battle.Add($"BIG|GOOD|Вы ПОБЕДИЛИ :)");
                    return battle;
                }
                else if (heroShip <= 0)
                {
                    battle.Add(String.Empty);
                    battle.Add($"BAD|Ваш корабль пошёл ко дну!");
                    battle.Add($"BIG|BAD|Вы ПРОИГРАЛИ :(");
                    return battle;
                }
            }

            battle.Add(String.Empty);
            battle.Add($"BIG|Ничья! Оба корабля остались на плаву!");
            return battle;
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

        private static bool LastEnemy(List<Character> enemies) =>
            enemies.Where(x => IsAlive(x)).Count() == 1;

        private static bool NoMoreEnemies(List<Character> enemies) =>
            enemies.Where(x => IsAlive(x)).Count() == 0;

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            int round = 1;
            bool alreadyDejected = false;

            if (DriadStrength)
            {
                int driad = Game.Dice.Roll();

                fight.Add($"GRAY|Узнаём силу Дриада. Бросаем кубик: {Game.Dice.Symbol(driad)}");

                if (driad == 4)
                {
                    fight.Add("BOLD|Выпала четвёрка - Дриад убит!");
                    fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");

                    return fight;
                }
                else
                {
                    int strength = 10 - driad;
                    FightEnemies[0].Strength = strength;

                    string line = Game.Services.CoinsNoun(strength, "СИЛА", "СИЛЫ", "СИЛ");
                    fight.Add($"BOLD|Сила Дриада: 10 - {driad} = {strength} {line}");
                    fight.Add(String.Empty);
                }
            }

            while (true)
            {
                fight.Add($"HEAD|BOLD|Раунд: {round}");

                bool attackAlready = false;
                int protagonistHit = 0;

                foreach (Character enemy in FightEnemies)
                {
                    if (!IsAlive(enemy))
                        continue;

                    fight.Add($"{enemy.Name} (сила {enemy.Strength})");
                    enemy.RoundWithoutSuccess += 1;

                    if (LastIsDejected && LastEnemy(FightEnemies) && !alreadyDejected)
                    {
                        enemy.Loyalty -= 4;
                        alreadyDejected = true;

                        fight.Add($"GRAY|Оставшись в одиночестве {enemy.Name} здорово трухнул: " +
                            $"его верность теперь равна {enemy.Loyalty}");

                        if (enemy.Loyalty <= 3)
                        {
                            fight.Add($"GOOD|{enemy.Name} обращается в позорное бегство :)");
                            fight.Add(String.Empty);
                            fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");
                            return fight;
                        }
                    }

                    if (LastRunsAway && LastEnemy(FightEnemies))
                    {
                        fight.Add($"GOOD|{enemy.Name} не будет испытывать свою судьбу: " +
                            $"он обращается в позорное бегство :)");
                        fight.Add(String.Empty);
                        fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");
                        return fight;
                    }

                    if (!attackAlready)
                    {
                        int dice = Game.Dice.Roll();
                        int skill = Character.Protagonist.Skill;

                        if (SkillPenalty > 0)
                        {
                            skill -= SkillPenalty;

                            fight.Add($"GRAY|Ловкость снижена на {SkillPenalty} " +
                               $"и равна {skill}");
                        }

                        protagonistHit = skill + (dice * 2);

                        fight.Add($"Мощность вашего удара: " +
                            $"{Game.Dice.Symbol(dice)} x 2 + {skill} = {protagonistHit}");

                        if (StrengthPenalty > 0)
                        {
                            protagonistHit -= StrengthPenalty;

                            fight.Add($"GRAY|Мощность снижается на {StrengthPenalty} " +
                                $"и равна {protagonistHit}");
                        }
                    }

                    int enemyDice = Game.Dice.Roll();
                    int enemyHit = enemy.Skill + (enemyDice * 2);

                    fight.Add($"Мощность его удара: " +
                        $"{Game.Dice.Symbol(enemyDice)} x 2 + " +
                        $"{enemy.Skill} = {enemyHit}");

                    if ((protagonistHit > enemyHit) && !attackAlready)
                    {
                        fight.Add($"GOOD|{enemy.Name} ранен");

                        if (HeroDamage > 0)
                        {
                            enemy.Strength -= HeroDamage;
                            string line = Game.Services.CoinsNoun(EnemyDamage, "силу", "силы", "сил");
                            fight.Add($"GOOD|Ранение обошлось вашему противнику в {EnemyDamage} {line}!");
                        }
                        else
                        {
                            enemy.Strength -= 2;
                        }

                        bool lastDoomed = LastIsDoomed && IsAlive(enemy) && LastEnemy(FightEnemies);

                        if (!lastDoomed && (enemy.Loyalty != null) && IsAlive(enemy))
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
                    else if (protagonistHit > enemyHit)
                    {
                        fight.Add($"BOLD|{enemy.Name} не смог вас ранить");
                    }
                    else if (protagonistHit < enemyHit)
                    {
                        fight.Add($"BAD|{enemy.Name} ранил вас");

                        bool loyaltyBonus = enemy.Loyalty >= 18;

                        if (loyaltyBonus)
                        {
                            Character.Protagonist.Strength -= 3;
                            fight.Add($"BAD|Его верность так высока, что ранение обошлось вам в 3 силы!");
                        }
                        else if (EnemyDamage > 0)
                        {
                            Character.Protagonist.Strength -= EnemyDamage;
                            string line = Game.Services.CoinsNoun(EnemyDamage, "силу", "силы", "сил");
                            fight.Add($"BAD|Ранение обошлось вам в {EnemyDamage} {line}!");
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

                        bool lastDoomed = LastIsDoomed && LastEnemy(FightEnemies);

                        if (!lastDoomed && (enemy.Loyalty != null))
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
                    bool enemyDoomed = LastIsDoomed && LastEnemy(FightEnemies);
                    bool roundsFails = enemy.RoundWithoutSuccess > 1;

                    if (!enemyDoomed && (enemy.Loyalty != null) && IsAlive(enemy) && roundsFails)
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
