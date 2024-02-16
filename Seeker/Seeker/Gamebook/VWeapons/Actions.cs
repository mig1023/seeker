using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.VWeapons
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public new static Actions StaticInstance = new Actions();
        public new static Actions GetInstance() => StaticInstance;
        private static Character protagonist = Character.Protagonist;

        public List<Character> Enemies { get; set; }
        public bool Dogfight { get; set; }
        public int Value { get; set; }
        public int Time { get; set; }
        public bool DamagedWeapon { get; set; }

        public override List<string> Status() => new List<string>
        {
            $"Подозрение: {protagonist.Suspicions}/5",
            $"Время: {protagonist.Time}/12",
            $"Меткость: {protagonist.Accuracy}/5",
            $"Патроны: {protagonist.Cartridges}/8",
        };

        public override List<string> AdditionalStatus() => new List<string>
        {
            $"Голова: {protagonist.Head}/3",
            $"Плечи: {protagonist.ShoulderGirdle}/4",
            $"Корпус: {protagonist.Body}/4",
            $"Руки: {protagonist.Hands}/4",
            $"Ноги: {protagonist.Legs}/4",
        };

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
            {
                string accuracy = Dogfight ? String.Empty : $"меткость {enemy.Accuracy}  ";
                string first = enemy.First ? "  атакует первым" : String.Empty;

                enemies.Add($"{enemy.Name}\n{accuracy}здоровье {enemy.Hitpoints}{first}");
            }

            return enemies;
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(protagonist.Dead, out toEndParagraph, out toEndText);

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option == "specialCheck")
            {
                return (option.Contains("!") ? !Checks.Special() : Checks.Special());
            }
            else if (option == "specialFCheck")
            {
                return (option.Contains("!") ? !Checks.SpecialF() : Checks.SpecialF());
            }
            else if (option.Contains("|"))
            {
                return option.Split('|').Where(x => Game.Option.IsTriggered(x.Trim())).Count() > 0;
            }
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains(">") || oneOption.Contains("<"))
                    {
                        int level = Game.Services.LevelParse(oneOption);

                        if (oneOption.Contains("ПАТРОНЫ >=") && (level > protagonist.Cartridges))
                            return false;

                        if (oneOption.Contains("ПОДОЗРЕНИЕ >=") && (level > protagonist.Suspicions))
                            return false;

                        if (oneOption.Contains("ПОДОЗРЕНИЕ <") && (level <= protagonist.Suspicions))
                            return false;

                        if (oneOption.Contains("ВРЕМЯ >=") && (level > protagonist.Time))
                            return false;

                        if (oneOption.Contains("ВРЕМЯ <") && (level <= protagonist.Time))
                            return false;
                    }
                    else if (oneOption.Contains("!"))
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
        
        public List<string> Damage()
        {
            List<string> damage = new List<string>();

            if (Time != 0)
                protagonist.Time += Time;

            int target = Game.Dice.Roll();

            if (target == 6)
            {
                damage.Add("BIG|GOOD|Вам повезло и вы отделались лёгким испугом! :)");
            }
            else
            {
                Fights.ProtagonistWound(protagonist, ref damage, target, Value);
            }

            return damage;
        }

        public List<string> Cartridges()
        {
            List<string> damage = new List<string>();

            if (Time != 0)
                protagonist.Time += Time;

            protagonist.Cartridges += Value;

            damage.Add($"BIG|GOOD|+{Value} патронов, " +
                $"их у вас теперь {protagonist.Cartridges}.");

            return damage;
        }

        public List<string> Bomb()
        {
            if (Time != 0)
                protagonist.Time += Time;

            Game.Option.Trigger("B");

            return new List<string> { "BIG|GOOD|Бомба теперь у вас." }; ;
        }

        private void HealingAction(ref List<string> healing, string partName, ref int healingPoints, string part)
        {
            if (healingPoints <= 0)
                return;

            int currentValue = GetProperty(protagonist, part);

            int maxHealing = (partName == "головы" ? 3 : 4);

            if (currentValue == maxHealing)
                return;

            int diff = (maxHealing - currentValue) > healingPoints ? 
                healingPoints : (maxHealing - currentValue);

            healingPoints -= diff;
            currentValue += diff;

            SetProperty(protagonist, part, currentValue);

            healing.Add($"Вы восстановили {diff} ед. здоровья " +
                $"{partName}, теперь оно равно {currentValue} " +
                $"из {maxHealing}.");
        }

        public List<string> Healing()
        {
            List<string> healing = new List<string>();

            int healingPoints = Value;

            if (Time != 0)
                protagonist.Time += Time;

            foreach (string parts in Constants.HealingParts.Keys.ToList())
                HealingAction(ref healing, parts, ref healingPoints, Constants.HealingParts[parts]);

            return healing;
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            bool noCartridges = Fights.NoMoreCartridges(FightEnemies) && protagonist.Cartridges <= 0;
            bool dogFightprotagonist, dogfight = Dogfight || noCartridges;
            int damagedWeapon = 2;

            while (true)
            {
                foreach (Character enemy in FightEnemies)
                {
                    if (enemy.Hitpoints <= 0)
                        continue;

                    string cartridgesLine = dogfight || enemy.Animal ?
                        String.Empty : $", патронов {enemy.Cartridges}";

                    fight.Add($"BOLD|{enemy.Name}, здоровье {enemy.Hitpoints}{cartridgesLine}");

                    if (enemy.First && Fights.EnemyAttack(protagonist, enemy, ref fight, Dogfight))
                        return fight;

                    if (DamagedWeapon)
                    {
                        dogFightprotagonist = (damagedWeapon <= 0);
                    }
                    else
                    {
                        dogFightprotagonist = Dogfight || (protagonist.Cartridges <= 0) || (protagonist.Accuracy <= 0);
                    }

                    if (!dogFightprotagonist)
                    {
                        string shots = DamagedWeapon ? " трижды" : String.Empty;
                        fight.Add($"Вы стреляете{shots}:");

                        if (DamagedWeapon)
                        {
                            damagedWeapon -= 1;
                        }
                        else
                        {
                            protagonist.Cartridges -= 1;
                        }

                        enemy.Hitpoints -= protagonist.Accuracy;
                        fight.Add($"GOOD|Ваш выстрел отнимает у него {protagonist.Accuracy} ед. здоворья.");
                    }
                    else if (protagonist.ShoulderGirdle <= 0)
                    {
                        fight.Add("Ваши ранения слишком страшны, вы не способны противостоять противнику в этом бою...");
                        protagonist.Dead = true;

                        fight.Add(String.Empty);
                        fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");

                        return fight;
                    } 
                    else
                    {
                        fight.Add("Вы бьёте:");
                        enemy.Hitpoints -= 2;
                        fight.Add("GOOD|Ваш удар отнимаете у него 2 ед. здоворья.");
                    }
                    
                    if (enemy.Hitpoints <= 0)
                    {
                        fight.Add($"GOOD|{enemy.Name} убит!");
                    }
                    else if (!enemy.First && Fights.EnemyAttack(protagonist, enemy, ref fight, Dogfight))
                    {
                        return fight;
                    }

                    if (Fights.NoMoreEnemies(FightEnemies))
                    {
                        fight.Add(String.Empty);
                        fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");
                        return fight;
                    }

                    fight.Add(String.Empty);
                }
            }
        }
    }
}
