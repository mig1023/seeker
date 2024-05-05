using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.WalkInThePark
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public List<Character> Enemies { get; set; }

        private static Random rand = new Random();


        private bool IfThisIsFirstPart() =>
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
                    $"Самочувствие: {Character.Protagonist.Health}",
                    $"Мочесть: {(double)Character.Protagonist.Power}",
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
                enemies.Add($"{enemy.Name}\nсила {enemy.Strength}  " +
                    $"выносливость {enemy.Endurance}  урон {(double)enemy.Damage / 10}");
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

        public static bool NoMoreEnemies(List<Character> enemies) =>
            enemies.Where(x => x.Endurance > 0).Count() == 0;

        public static string Hit() =>
            $"{Constants.What[rand.Next(Constants.What.Count)]} {Constants.Where[rand.Next(Constants.Where.Count)]}";

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

                foreach (Character enemy in FightEnemies)
                {
                    if (enemy.Endurance <= 0)
                        continue;

                    fight.Add($"{enemy.Name} (выносливость {(double)enemy.Endurance / 10})");

                    int protagonistRoll = Game.Dice.Roll();
                    int protagonistHitStrength = protagonistRoll + Character.Protagonist.Strength;

                    fight.Add($"Сила вашего удара: {Game.Dice.Symbol(protagonistRoll)} + " +
                        $"{Character.Protagonist.Strength} = {protagonistHitStrength}");

                    int enemyRoll = Game.Dice.Roll();
                    int enemyHitStrength = enemyRoll + enemy.Strength;

                    fight.Add($"Сила удара противника: " +
                        $"{Game.Dice.Symbol(enemyRoll)} + {enemy.Strength} = {enemyHitStrength}");

                    if (protagonistHitStrength > enemyHitStrength)
                    {
                        fight.Add($"GOOD|BOLD|{enemy.Name} {Hit()}");
                        fight.Add($"Противник теряет {(double)Character.Protagonist.Damage / 10} ед. Выносливости");

                        enemy.Endurance -= Character.Protagonist.Damage;

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
                        fight.Add($"Вы теряете {(double)enemy.Damage / 10} ед. Выносливости");

                        Character.Protagonist.Endurance -= enemy.Damage;

                        if (Character.Protagonist.Endurance <= 0)
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
