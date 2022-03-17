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
            String.Format("Ноги: {0}/4", protagonist.Legs),
            String.Format("Руки: {0}/4", protagonist.Hands),
            String.Format("Корпус: {0}/4", protagonist.Body),
            String.Format("Плечи: {0}/4", protagonist.ShoulderGirdle),
            String.Format("Голова: {0}/3", protagonist.Head),
        };

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Enemies == null)
            {
                return enemies;
            }

            foreach (Character enemy in Enemies)
            {
                string accuracy = (Dogfight ? String.Empty : String.Format("меткость {0}  ", enemy.Accuracy));
                string first = (enemy.First ? "  атакует первым" : String.Empty);

                enemies.Add(String.Format("{0}\n{1}здоровье {2}{3}", enemy.Name, accuracy, enemy.Hitpoints, first));
            }

            return enemies;
        }

        public override string ButtonText() => (Name == "Fight" ? "Перестрелка" : Button);

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(protagonist.Dead, out toEndParagraph, out toEndText);

        private bool SpecialCheck() =>
            (Game.Option.IsTriggered("Mt") || Game.Option.IsTriggered("P")) &&
            !Game.Option.IsTriggered("B") && protagonist.Suspicions <= 3;

        private bool SpecialFCheck() => ((protagonist.Suspicions >= 4) && !Game.Option.IsTriggered("F"));

        public override bool CheckOnlyIf(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option == "specialCheck")
            {
                return (option.Contains("!") ? !SpecialCheck() : SpecialCheck());
            }
            else if (option == "specialFCheck")
            {
                return (option.Contains("!") ? !SpecialFCheck() : SpecialFCheck());
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

        private bool NoMoreEnemies(List<Character> enemies) => enemies.Where(x => x.Hitpoints > 0).Count() == 0;

        private bool NoMoreCartridges(List<Character> enemies) => enemies.Where(x => x.Cartridges > 0).Count() == 0;

        private void ProtagonistWound(Character protagonist, ref List<string> fight, int target, int wound)
        {
            switch (target)
            {
                case 1:
                    fight.Add("BAD|Ранение пришлось в ногу!");

                    if (protagonist.Legs > 0)
                    {
                        protagonist.Legs -= wound;
                        fight.Add(String.Format("BAD|Вы потеряли {0} ед. здоровья ног, теперь оно равно {1} из 4.", wound, protagonist.Legs));

                        if (protagonist.Legs <= 0)
                            fight.Add("BOLD|Вы больше не сможете спасаться бегством или прыгать с любой высоты!");
                    }

                    break;

                case 2:
                    fight.Add("BAD|Ранение пришлось в руку!");

                    if (protagonist.Hands > 0)
                    {
                        protagonist.Hands -= wound;
                        fight.Add(String.Format("BAD|Вы потеряли {0} ед. здоровья рук, теперь оно равно {1} из 4.", wound, protagonist.Hands));
                    }

                    if (protagonist.Accuracy > 0)
                    {
                        protagonist.Accuracy -= 1;
                        fight.Add(String.Format("BAD|Вы также теряете 1 ед. меткости, теперь она равно {0}.", protagonist.Accuracy));
                    }

                    break;

                case 3:
                    fight.Add("BAD|Ранение пришлось в корпус!");

                    protagonist.Body -= wound;
                    fight.Add(String.Format("BAD|Вы потеряли {0} ед. здоровья тела, теперь оно равно {1} из 4.", wound, protagonist.Body));

                    if (protagonist.Body <= 0)
                        protagonist.Dead = true;

                    break;

                case 4:
                    fight.Add("BAD|Ранение пришлось в плечо!");

                    if (protagonist.ShoulderGirdle > 0)
                    {
                        protagonist.ShoulderGirdle -= wound;
                        fight.Add(String.Format("Вы потеряли {0} ед. здоровья плеча, теперь оно равно {1} из 4.", wound, protagonist.ShoulderGirdle));

                        if (protagonist.ShoulderGirdle <= 0)
                            fight.Add("BOLD|Вы больше не можете наносить ударов и обречены в рукопашной!");
                    }

                    break;

                case 5:
                    fight.Add("BAD|Ранение пришлось в голову!");

                    protagonist.Head -= wound;
                    fight.Add(String.Format("Вы потеряли {0} ед. здоровья головы, теперь оно равно {1} из 3.", wound, protagonist.Head));

                    if (protagonist.Head <= 0)
                        protagonist.Dead = true;

                    else if (protagonist.Suspicions < 5)
                    {
                        protagonist.Suspicions += 1;
                        fight.Add("BOLD|Вы также получаете 1 ед. подозрений, так как скрыть рану не удастся.");
                    }

                    break;
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
                ProtagonistWound(protagonist, ref damage, target, Value);

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

            foreach (string parts in Constants.healingParts.Keys.ToList())
                HealingAction(ref healing, parts, ref healingPoints, Constants.healingParts[parts]);

            return healing;
        }

        private bool EnemyAttack(Character protagonist, Character enemy, ref List<string> fight)
        {
            int wound = 0;
            int target = Game.Dice.Roll();
            bool dogfight = Dogfight || (enemy.Cartridges <= 0);

            if (target == 6)
            {
                if (!dogfight)
                    enemy.Cartridges -= 1;

                fight.Add(String.Format("{0} {1}!", enemy.Name, (dogfight ? "промедлил" : "промахнулся")));
                return false;
            }

            if (!dogfight)
            {
                enemy.Cartridges -= 1;
                wound = enemy.Accuracy;
                fight.Add(String.Format("{0} стреляет в вас.", enemy.Name));
            }
            else
            {
                wound = (enemy.Animal ? 2 : 1);
                fight.Add(String.Format("{0} {1} вас.", enemy.Name, (enemy.Animal ? "кусает" : "бьёт")));
            }

            ProtagonistWound(protagonist, ref fight, target, wound);

            if (protagonist.Dead)
            {
                fight.Add(String.Empty);
                fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");

                return true;
            }
            else
                return false;
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());
            
            bool dogFightprotagonist, dogfight = Dogfight || (NoMoreCartridges(FightEnemies) && protagonist.Cartridges <= 0);
            int damagedWeapon = 2;

            while (true)
            {
                foreach (Character enemy in FightEnemies)
                {
                    if (enemy.Hitpoints <= 0)
                        continue;

                    string cartridgesLine = (dogfight || enemy.Animal ? String.Empty : String.Format(", патронов {0}", enemy.Cartridges));
                    fight.Add(String.Format("BOLD|{0}, здоровье {1}{2}", enemy.Name, enemy.Hitpoints, cartridgesLine));

                    if (enemy.First && EnemyAttack(protagonist, enemy, ref fight))
                        return fight;

                    if (DamagedWeapon)
                        dogFightprotagonist = (damagedWeapon <= 0);
                    else
                        dogFightprotagonist = Dogfight || (protagonist.Cartridges <= 0) || (protagonist.Accuracy <= 0);

                    if (!dogFightprotagonist)
                    {
                        fight.Add(String.Format("Вы стреляете{0}.", DamagedWeapon ? " трижды" : String.Empty));

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

                    else if (!enemy.First && EnemyAttack(protagonist, enemy, ref fight))
                        return fight;

                    if (NoMoreEnemies(FightEnemies))
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
