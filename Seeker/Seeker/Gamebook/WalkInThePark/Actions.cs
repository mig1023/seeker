using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.WalkInThePark
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public List<Character> Enemies { get; set; }

        private static Random rand = new Random();

        private static bool IfThisIsFirstPart() =>
            Game.Data.CurrentParagraphID <= Constants.FirstPartSize;

        public override List<string> Status()
        {
            if (IfThisIsFirstPart())
            {
                return new List<string>
                {
                    $"Сила: {Character.Protagonist.Strength}",
                    $"Выносливость: {(double)Character.Protagonist.Endurance / 10}",
                    $"Удача: {Character.Protagonist.Luck}",
                };
            }
            else
            {
                return new List<string>
                {
                    $"Самочувствие: {Character.Protagonist.Health} / {Character.Protagonist.MaxHealth}",
                    $"Мочесть: {(double)Character.Protagonist.Strength}",
                    $"Тихопопость: {Character.Protagonist.Stealth}",
                    $"Фавор: {Character.Protagonist.Fortune}",
                };
            }   
        }

        public override List<string> AdditionalStatus()
        {
            if (IfThisIsFirstPart())
            {
                return new List<string>
                {
                    $"Оружие: {Character.Protagonist.Weapon} (урон {(double)Character.Protagonist.Damage / 10})",
                    $"Деньги: {Character.Protagonist.Money} руб",
                };
            }
            else
            {
                return new List<string>
                {
                    $"Оружие: {Character.Protagonist.Weapon} (урон {Character.Protagonist.Damage})",
                    $"Деньги: {Character.Protagonist.Money} руб",
                    $"Рейтинг: {Character.Protagonist.Rating}",
                };
            }
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText)
        {
            if (IfThisIsFirstPart())
            {
                return GameOverBy(Character.Protagonist.Endurance, out toEndParagraph, out toEndText);
            }
            else if (Game.Data.CurrentParagraphID == 200)
            {
                toEndParagraph = 0;
                toEndText = Output.Constants.GAMEOVER_TEXT;
                return true;
            }
            else
            {
                toEndParagraph = 200;
                toEndText = "Финита ля комедия";
                return GameOverBy(Character.Protagonist.Health, out _, out _);
            }
        }
            

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Price > 0)
            {
                string price = Game.Services.CoinsNoun(Price, "рубль", "рубля", "рублей");
                return new List<string> { $"{Head}, {Price} {price}" };
            }

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
            {
                if (IfThisIsFirstPart())
                {
                    enemies.Add($"{enemy.Name}\nсила {enemy.Strength}  " +
                        $"выносливость {enemy.Endurance}  урон {(double)enemy.Damage / 10}");
                }
                else
                {
                    enemies.Add($"{enemy.Name}\nсамочувствие {enemy.Health}  " +
                        $"мочность {enemy.Strength}  урон {(double)enemy.Damage}");
                }
            }

            return enemies;
        }

        public List<string> Luck()
        {
            List<string> luck = Test(Character.Protagonist.Luck,
                "Проверка фарта", "ФАРТАНУЛО", "НЕ ФАРТАНУЛО", "Повезло", "Не повезло", out _);

            if (Character.Protagonist.Luck > 0)
            {
                Character.Protagonist.Luck -= 1;
                luck.Add("Уровень удачи снижен на единицу");
            }

            return luck;
        }

        public List<string> Fortune()
        {
            List<string> luck = Test(Character.Protagonist.Fortune,
                "Проверка фавора", "ВЫ В ФАВОРЕ", "ВЫ НЕ В ФАВОРЕ",
                "В фаворе, Под пытками он во всем сознался", "Не в фаворе, Он ничего не рассказал", out _);

            if (Character.Protagonist.Fortune > 0)
            {
                Character.Protagonist.Fortune -= 1;
                luck.Add("Уровень фавора снижен на единицу");
            }

            return luck;
        }

        public List<string> Stealth()
        {
            List<string> luck = Test(Character.Protagonist.Fortune,
                "Проверка Тихопопости", "ПОЛНЫЙ УСПЕХ в ТИХОПОПОСТИ", "ПОЛНЫЙ ПРОВАЛ в ТИХОПОПОСТИ",
                "Проканало, Выгорело, Она вынесла все твои пытки и козни",
                "Не проканало, Не выгорело, Откинулась в мучениях", out bool success);

            if (success)
            {
                Character.Protagonist.Stealth += 1;
                luck.Add("Уровень Тихопопости вырос на единицу");
            }
            else if (Character.Protagonist.Stealth > 1)
            {
                Character.Protagonist.Stealth -= 1;
                luck.Add("Уровень Тихопопости снижен на единицу");
            }

            return luck;
        }

        public List<string> Test(int param, string test, string testOk, string testFail,
            string buttonOk, string buttonFail, out bool testPassed)
        {
            int dice = Game.Dice.Roll();

            testPassed = dice <= param;
            string testLine = testPassed ? "<=" : ">";

            List<string> testResult = new List<string> {
                $"{test}: {Game.Dice.Symbol(dice)} " +
                $"{testLine} {param}" };

            testResult.Add(testPassed ? $"BIG|GOOD|{testOk} :)" : $"BIG|BAD|{testFail} :(");

            Game.Buttons.Disable(testPassed, buttonOk, buttonFail);

            return testResult;
        }

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            bool disabledGetOptions = (Price > 0) && Used;
            bool disabledByPrice = (Price > 0) && (Character.Protagonist.Money < Price);

            return !(disabledGetOptions || disabledByPrice);
        }

        public List<string> Get()
        {
            if ((Price > 0) && (Character.Protagonist.Money >= Price))
            {
                Character.Protagonist.Money -= Price;

                if (!Multiple)
                    Used = true;

                if (Benefit != null)
                {
                    Benefit.Do();
                }
                else if (BenefitList != null)
                {
                    foreach (Modification modification in BenefitList)
                        modification.Do();
                }
            }

            return new List<string> { "RELOAD" };
        }

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else
            {
                return AvailabilityTrigger(option);
            }
        }

        private static bool NoMoreEnemies(List<Character> enemies)
        {
            bool part1 = IfThisIsFirstPart();

            int enemiesCount = enemies
                .Where(x => x.GetHealth(part1) > 0)
                .Count();

            return enemiesCount == 0;
        }

        private static string Hit() =>
            $"{Constants.What[rand.Next(Constants.What.Count)]} {Constants.Where[rand.Next(Constants.Where.Count)]}";

        public List<string> Fight()
        {
            bool part1 = IfThisIsFirstPart();

            List<string> fight = new List<string>();
            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            int round = 1;

            while (true)
            {
                fight.Add($"HEAD|BOLD|Раунд: {round}");

                foreach (Character enemy in FightEnemies)
                {
                    if (enemy.GetHealth(part1) <= 0)
                        continue;

                    if (part1)
                        fight.Add($"{enemy.Name} (выносливость {(double)enemy.Endurance / 10})");
                    else
                        fight.Add($"{enemy.Name} (самочувствие {enemy.Health})");

                    int protagonistRoll = Game.Dice.Roll();
                    int protagonistHitStrength = protagonistRoll + Character.Protagonist.Strength;
                    string strength = part1 ? "Сила" : "Охрененность";

                    fight.Add($"{strength} вашего удара: {Game.Dice.Symbol(protagonistRoll)} + " +
                        $"{Character.Protagonist.Strength} = {protagonistHitStrength}");

                    int enemyRoll = Game.Dice.Roll();
                    int enemyHitStrength = enemyRoll + enemy.Strength;

                    fight.Add($"{strength} удара противника: " +
                        $"{Game.Dice.Symbol(enemyRoll)} + {enemy.Strength} = {enemyHitStrength}");

                    if (protagonistHitStrength > enemyHitStrength)
                    {
                        fight.Add($"GOOD|BOLD|{enemy.Name} {Hit()}");

                        if (part1)
                            fight.Add($"Противник теряет {(double)Character.Protagonist.Damage / 10} ед. Выносливости");
                        else
                            fight.Add($"Противник теряет {Character.Protagonist.Damage} ед. Самочувствия");

                        enemy.SetHealth(part1, enemy.GetHealth(part1) - Character.Protagonist.Damage);

                        if (NoMoreEnemies(FightEnemies))
                        {
                            if (Benefit != null)
                                Benefit.Do();

                            fight.Add(String.Empty);
                            fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");
                            return fight;
                        }
                    }
                    else if (protagonistHitStrength < enemyHitStrength)
                    {
                        fight.Add($"BAD|BOLD|{enemy.Name} заехал вам по морде");

                        if (part1)
                            fight.Add($"Вы теряете {(double)enemy.Damage / 10} ед. Выносливости");
                        else
                            fight.Add($"Вы теряете {enemy.Damage} ед. Самочувствия");

                        Character.Protagonist.SetHealth(part1,
                            Character.Protagonist.GetHealth(part1) - enemy.Damage);

                        if (Character.Protagonist.GetHealth(part1) <= 0)
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

        public override bool IsHealingEnabled() =>
            true;

        public override void UseHealing(int healingLevel) =>
            Character.Protagonist.Endurance += 1;
    }
}
