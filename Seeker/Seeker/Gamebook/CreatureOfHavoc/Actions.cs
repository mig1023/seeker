using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Seeker.Gamebook.CreatureOfHavoc
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public List<Character> Enemies { get; set; }

        public int WoundsToWin { get; set; }
        public int RoundsToWin { get; set; }
        public int RoundsToFight { get; set; }

        public bool Ophidiotaur { get; set; }
        public bool ManicBeast { get; set; }
        public bool GiantHornet { get; set; }

        public string SomethingStrange { get; set; }

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
                enemies.Add($"{enemy.Name}\nмастерство {enemy.Mastery}  выносливость {enemy.Endurance}");

            return enemies;
        }

        public override List<string> Status() => new List<string>
        {
            $"Мастерство: {Character.Protagonist.Mastery}",
            $"Выносливость: {Character.Protagonist.Endurance}/{Character.Protagonist.MaxEndurance}",
            $"Удачливость: {Character.Protagonist.Luck}",
        };

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(Character.Protagonist.Endurance, out toEndParagraph, out toEndText);

        public override bool Availability(string option) =>
            AvailabilityTrigger(option);

        public List<string> Luck() =>
            GoodLuck(out bool _, notInline: true);

        public List<string> GoodLuck(out bool goodLuck, bool notInline = false)
        {
            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            goodLuck = (firstDice + secondDice) < Character.Protagonist.Luck;

            string luckLine = goodLuck ? "<=" : ">";
            List<string> luckCheck = new List<string> {
                $"Проверка удачи: {Game.Dice.Symbol(firstDice)} + " +
                $"{Game.Dice.Symbol(secondDice)} {luckLine} {Character.Protagonist.Luck}" };

            luckCheck.Add(
                (notInline ? String.Empty : "BIG|") + 
                (goodLuck ? "GOOD|УСПЕХ :)" : "BAD|НЕУДАЧА :("));

            Game.Buttons.Disable(goodLuck, "Повезло", "Нет");

            if (Character.Protagonist.Luck > 2)
            {
                Character.Protagonist.Luck -= 1;
                luckCheck.Add("Уровень удачи снижен на единицу");
            }

            return luckCheck;
        }

        public override bool IsButtonEnabled(bool secondButton = false) =>
            (Type == "Translation") ? Game.Option.IsTriggered("Translate") : true;

        public List<string> Rocks()
        {
            List<string> rocks = new List<string>();

            for (int i = 0; i < 6; i++)
            {
                int rock = Game.Dice.Roll();

                string inTarget, bold = String.Empty;

                if (rock == 6)
                {
                    Character.Protagonist.Endurance -= 1;
                    inTarget = " - ПОПАЛИ!";
                    bold = "BOLD|";
                }
                else
                {
                    inTarget = "- не попали.";
                }

                rocks.Add($"{bold}Бросок камня: {Game.Dice.Symbol(rock)}{inTarget}");
            }

            Character.Protagonist.Endurance += 3;
            rocks.Add("+3 выносливости за еду");

            return rocks;
        }

        public List<string> Stoning()
        {
            List<string> stoning = new List<string> { "Селяне бросают камни..." };

            stoning.AddRange(GoodLuck(out bool goodLuck));

            if (!goodLuck)
            {
                Character.Protagonist.Endurance -= 2;

                stoning.Add("Несколько камней попали по вам...");
                stoning.Add("BAD|и вы потеряли ещё 2 выносливости!");
            }
            else
            {
                stoning.Add("GOOD|Селяне, к счастью, оказались косоглазые и " +
                    "вы ушли без дополнительного ущерба!");
            }

            return stoning;
        }

        public List<string> Fruit()
        {
            List<string> fruit = new List<string> { "Пробуете трясти дерево..." };

            fruit.AddRange(GoodLuck(out bool goodLuck));

            if (goodLuck)
            {
                Character.Protagonist.Endurance += 4;

                fruit.Add("Волшебный плод падает к вашим ногам!");
                fruit.Add("GOOD|Вы восстанавливаете 4 выносливости!");
            }
            else
            {
                fruit.Add("BAD|Вам не удалось добыть плодов...");
            }

            return fruit;
        }

        public List<string> Hunt()
        {
            List<string> hunt = new List<string> { "Пробуете поймать кого-нибудь..." };

            hunt.AddRange(GoodLuck(out bool goodLuck));

            string[] huntPray = "птичку, кролика, зайчика, кабанчика, ящерку, мышку, фазанчика".Split(',');
            string hunted = huntPray[Game.Dice.Roll() - 1].Trim();

            if (goodLuck)
            {
                Character.Protagonist.Endurance += 2;
                hunt.Add($"GOOD|Вы поймали {hunted} и получаете 2 выносливости");
            }
            else
            {
                hunt.Add("BAD|Вам не удалось никого поймать");
            }

            return hunt;
        }

        public List<string> PoisonOrFood()
        {
            List<string> food = new List<string> { "Пробуете всё на вкус..." };

            food.AddRange(GoodLuck(out bool goodLuck));

            if (goodLuck)
            {
                food.Add("GOOD|Вы плотно и без помех поели и получаете 2 выносливости");
                Character.Protagonist.Endurance += 4;
            }
            else
            {
                food.Add("BAD|Вы по незнанию съели горсть ядовитых трав");
                Character.Protagonist.Endurance -= 3;
            }

            return food;
        }

        public List<string> Mastery()
        {
            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            bool goodSkill = (firstDice + secondDice) <= Character.Protagonist.Mastery;
            string skillLine = goodSkill ? "<=" : ">";

            List<string> skillCheck = new List<string> {
                $"Проверка мастерства: {Game.Dice.Symbol(firstDice)} + " +
                $"{Game.Dice.Symbol(secondDice)} {skillLine} {Character.Protagonist.Mastery} мастерство" };

            skillCheck.Add(Result(goodSkill, "МАСТЕРСТВА ХВАТИЛО", "МАСТЕРСТВА НЕ ХВАТИЛО"));

            return skillCheck;
        }

        public List<string> Translation()
        {
            Regex regex = new Regex(@"([^\.])\s");
            string decode = regex.Replace(SomethingStrange, "$1");

            foreach (char replace in Constants.TranslateReplaces.Values)
                decode = decode.Replace(replace, ' ');

            foreach (KeyValuePair<char, char> replace in Constants.TranslateReplaces)
                decode = decode.Replace(replace.Key, replace.Value);

            return new List<string> { decode };
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            int round = 1, enemyWounds = 0;
            bool previousRoundWound = false;
            Character protagonist = Character.Protagonist;

            while (true)
            {
                fight.Add($"HEAD|BOLD|Раунд: {round}");

                bool attackAlready = false;
                int protagonistHitStrength = 0;

                foreach (Character enemy in FightEnemies)
                {
                    if (enemy.Endurance <= 0)
                        continue;

                    bool doubleDice = false, doubleSixes = false, doubleDiceEnemy = false;

                    Character enemyInFight = enemy;
                    fight.Add($"{enemy.Name} (выносливость {enemy.Endurance})");

                    if (!attackAlready)
                    {
                        Game.Dice.DoubleRoll(out int protagonistRollFirst, out int protagonistRollSecond);
                        protagonistHitStrength = protagonistRollFirst + protagonistRollSecond + Character.Protagonist.Mastery;

                        fight.Add($"Мощность вашего удара: " +
                            $"{Game.Dice.Symbol(protagonistRollFirst)} + " +
                            $"{Game.Dice.Symbol(protagonistRollSecond)} + " +
                            $"{Character.Protagonist.Mastery} = {protagonistHitStrength}");

                        if (Game.Option.IsTriggered("Chestplate"))
                        {
                            protagonistHitStrength += 2;
                            fight.Add($"+2 к мощности удара за нагрудную пластину, итого {protagonistHitStrength}");
                        }

                        doubleDice = (protagonistRollFirst == protagonistRollSecond);
                        doubleSixes = doubleDice && (protagonistRollFirst == 6);
                    }

                    Game.Dice.DoubleRoll(out int enemyRollFirst, out int enemyRollSecond);
                    int enemyHitStrength = enemyRollFirst + enemyRollSecond + enemy.Mastery;
                    doubleDiceEnemy = (enemyRollFirst == enemyRollSecond);

                    fight.Add($"Мощность его удара: " +
                        $"{Game.Dice.Symbol(enemyRollFirst)} + " +
                        $"{Game.Dice.Symbol(enemyRollSecond)} + " +
                        $"{enemy.Mastery} = {enemyHitStrength}");

                    if (ManicBeast && previousRoundWound)
                    {
                        enemyHitStrength += 2;
                        previousRoundWound = false;
                        fight.Add($"+2 бонус к его удару за ярость, итого {enemyHitStrength}");
                    }

                    if (Ophidiotaur && doubleDiceEnemy)
                    {
                        fight.Add("Офидиотавр наносит удар ядовитым жалом");
                        fight.AddRange(GoodLuck(out bool goodLuck));

                        if (goodLuck)
                        {
                            fight.Add($"BOLD|{enemy.Name} не смог вас ранить");
                        }
                        else if (Enemy.WoundAndDeath(ref fight, ref protagonist, enemy.Name))
                        {
                            return fight;
                        }
                    }
                    else if (GiantHornet && doubleDiceEnemy)
                    {
                        fight.Add("Гигантский наносит удар ядовитым жалом");

                        if (doubleDice)
                        {
                            fight.Add("GOOD|Вы наносите шершню удар Мгновенной Смерти");
                            fight.Add("BAD|Но вы теряете 6 пунктов выносливости");

                            if (Enemy.WoundAndDeath(ref fight, ref protagonist, enemy.Name, wounds: 6))
                            {
                                return fight;
                            }
                            else
                            {
                                fight.Add(string.Empty);
                                fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");

                                return fight;
                            }
                        }
                        else
                        {
                            Character.Protagonist.Endurance = 0;

                            fight.Add("BAD|Вы смертельно ранены шершнем");
                            fight.Add(String.Empty);
                            fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");

                            return fight;
                        }
                    } 
                    else if ((doubleSixes || (protagonistHitStrength > enemyHitStrength)) && !attackAlready)
                    {
                        if (doubleSixes)
                        {
                            fight.Add($"GOOD|{enemy.Name} убит наповал");
                            enemy.Endurance = 0;
                        }
                        else
                        {
                            fight.Add($"GOOD|{enemy.Name} ранен");
                            enemy.Endurance -= 2;
                        }

                        enemyWounds += 1;
                        previousRoundWound = true;

                        bool enemyLost = Enemy.NoMore(FightEnemies);

                        if (enemyLost || ((WoundsToWin > 0) && (WoundsToWin <= enemyWounds)))
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
                        if (Enemy.WoundAndDeath(ref fight, ref protagonist, enemy.Name))
                            return fight;
                    }
                    else
                    {
                        fight.Add("BOLD|Ничья в раунде");
                    }

                    attackAlready = true;

                    if ((RoundsToWin > 0) && (RoundsToWin <= round))
                    {
                        fight.Add(String.Empty);
                        fight.Add("BAD|Отведённые на победу раунды истекли.");
                        fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");

                        return fight;
                    }

                    if ((RoundsToFight > 0) && (RoundsToFight <= round))
                    {
                        fight.Add(String.Empty);
                        fight.Add("BOLD|Отведённые на бой раунды истекли.");

                        return fight;
                    }

                    fight.Add(String.Empty);
                }

                round += 1;
            }
        }

        public override bool IsHealingEnabled() =>
            Character.Protagonist.Endurance < Character.Protagonist.MaxEndurance;

        public override void UseHealing(int healingLevel) =>
            Character.Protagonist.Endurance += healingLevel;
    }
}
