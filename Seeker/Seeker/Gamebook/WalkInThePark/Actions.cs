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
                    $"Деньги: {Character.Protagonist.Money}",
                };
            }
            else
            {
                return null;
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
            int dice = Game.Dice.Roll();

            bool goodLuck = dice <= Character.Protagonist.Luck;
            string luckLine = goodLuck ? "<=" : ">";

            List<string> luckCheck = new List<string> {
                $"Проверка фарта: {Game.Dice.Symbol(dice)} " +
                $"{luckLine} {Character.Protagonist.Luck}" };

            luckCheck.Add(goodLuck ? "BIG|GOOD|ФАРТАНУЛО :)" : "BIG|BAD|НЕ ФАРТАНУЛО :(");

            if (Character.Protagonist.Luck > 0)
            {
                Character.Protagonist.Luck -= 1;
                luckCheck.Add("Уровень удачи снижен на единицу");
            }

            Game.Buttons.Disable(goodLuck, "Повезло", $"Не повезло");

            return luckCheck;
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
