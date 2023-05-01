using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.VWeapons
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public List<Character> Enemies { get; set; }
        public bool Dogfight { get; set; }
        public int Value { get; set; }
        public int Time { get; set; }
        public bool DamagedWeapon { get; set; }

        public override List<string> Status() => new List<string>
        {
            String.Format("Подозрение: {0}/5", protagonist.Suspicions),
            String.Format("Время: {0}/12", protagonist.Time),
            String.Format("Меткость: {0}/5", protagonist.Accuracy),
            String.Format("Патроны: {0}/8", protagonist.Cartridges),
        };

        public override List<string> AdditionalStatus() => new List<string>
        {
            String.Format("Голова: {0}/3", protagonist.Head),
            String.Format("Плечи: {0}/4", protagonist.ShoulderGirdle),
            String.Format("Корпус: {0}/4", protagonist.Body),
            String.Format("Руки: {0}/4", protagonist.Hands),
            String.Format("Ноги: {0}/4", protagonist.Legs),
        };

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
            {
                string accuracy = (Dogfight ? String.Empty : String.Format("меткость {0}  ", enemy.Accuracy));
                string first = (enemy.First ? "  атакует первым" : String.Empty);

                enemies.Add(String.Format("{0}\n{1}здоровье {2}{3}", enemy.Name, accuracy, enemy.Hitpoints, first));
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
                return (option.Contains("!") ? !Services.SpecialCheck() : Services.SpecialCheck());
            }
            else if (option == "specialFCheck")
            {
                return (option.Contains("!") ? !Services.SpecialFCheck() : Services.SpecialFCheck());
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
                        return false;
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
                damage.Add("BIG|GOOD|Вам повезло и вы отделались лёгким испугом! :)");
            else
                Services.ProtagonistWound(protagonist, ref damage, target, Value);

            return damage;
        }

        public List<string> Cartridges()
        {
            List<string> damage = new List<string>();

            if (Time != 0)
                protagonist.Time += Time;

            protagonist.Cartridges += Value;

            damage.Add(String.Format("BIG|GOOD|+{0} патронов, их у вас теперь {1}.", Value, protagonist.Cartridges));

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

            int diff = ((maxHealing - currentValue) > healingPoints ? healingPoints : (maxHealing - currentValue));

            healingPoints -= diff;
            currentValue += diff;

            SetProperty(protagonist, part, currentValue);

            healing.Add(String.Format("Вы восстановили {0} ед. здоровья {1}, теперь оно равно {2} из {3}.",
                diff, partName, currentValue, maxHealing));
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
            
            bool dogFightprotagonist, dogfight = Dogfight || (Services.NoMoreCartridges(FightEnemies) && protagonist.Cartridges <= 0);
            int damagedWeapon = 2;

            while (true)
            {
                foreach (Character enemy in FightEnemies)
                {
                    if (enemy.Hitpoints <= 0)
                        continue;

                    string cartridgesLine = (dogfight || enemy.Animal ? String.Empty : String.Format(", патронов {0}", enemy.Cartridges));
                    fight.Add(String.Format("BOLD|{0}, здоровье {1}{2}", enemy.Name, enemy.Hitpoints, cartridgesLine));

                    if (enemy.First && Services.EnemyAttack(protagonist, enemy, ref fight, Dogfight))
                        return fight;

                    if (DamagedWeapon)
                        dogFightprotagonist = (damagedWeapon <= 0);
                    else
                        dogFightprotagonist = Dogfight || (protagonist.Cartridges <= 0) || (protagonist.Accuracy <= 0);

                    if (!dogFightprotagonist)
                    {
                        fight.Add(String.Format("Вы стреляете{0}…", DamagedWeapon ? " трижды" : String.Empty));

                        if (DamagedWeapon)
                            damagedWeapon -= 1;
                        else
                            protagonist.Cartridges -= 1;

                        enemy.Hitpoints -= protagonist.Accuracy;
                        fight.Add(String.Format("GOOD|Ваш выстрел отнимает у него {0} ед. здоворья.", protagonist.Accuracy));
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
                        fight.Add("Вы бьёте.");
                        enemy.Hitpoints -= 2;
                        fight.Add("GOOD|Ваш удар отнимаете у него 2 ед. здоворья.");
                    }
                    
                    if (enemy.Hitpoints <= 0)
                        fight.Add(String.Format("GOOD|{0} убит!", enemy.Name));

                    else if (!enemy.First && Services.EnemyAttack(protagonist, enemy, ref fight, Dogfight))
                        return fight;

                    if (Services.NoMoreEnemies(FightEnemies))
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
