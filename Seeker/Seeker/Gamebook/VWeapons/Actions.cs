using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.VWeapons
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();

        public List<Character> Enemies { get; set; }
        public bool Dogfight { get; set; }
        public int Value { get; set; }
        public int Time { get; set; }
        public bool DamagedWeapon { get; set; }

        public override List<string> Status() => new List<string>
        {
            String.Format("Подозрение: {0}/5", Character.Protagonist.Suspicions),
            String.Format("Время: {0}/12", Character.Protagonist.Time),
            String.Format("Меткость: {0}/5", Character.Protagonist.Accuracy),
            String.Format("Патроны: {0}", Character.Protagonist.Cartridges),
        };

        public override List<string> AdditionalStatus() => new List<string>
        {
            String.Format("Ноги: {0}/4", Character.Protagonist.Legs),
            String.Format("Руки: {0}/4", Character.Protagonist.Hands),
            String.Format("Корпус: {0}/4", Character.Protagonist.Body),
            String.Format("Плечи: {0}/4", Character.Protagonist.ShoulderGirdle),
            String.Format("Голова: {0}/3", Character.Protagonist.Head),
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
            GameOverBy((Character.Protagonist.Dead ? 0 : 1), out toEndParagraph, out toEndText);

        private bool SpecialCheck() =>
            (Game.Data.Triggers.Contains("Mt") || Game.Data.Triggers.Contains("P")) &&
            !Game.Data.Triggers.Contains("B") && Character.Protagonist.Suspicions <= 3;

        private bool SpecialFCheck() => ((Character.Protagonist.Suspicions >= 4) && !Game.Data.Triggers.Contains("F"));

        public override bool CheckOnlyIf(string option)
        {
            if (option == "specialCheck")
                return (option.Contains("!") ? !SpecialCheck() : SpecialCheck());
            
            else if (option == "specialFCheck")
                return (option.Contains("!") ? !SpecialFCheck() : SpecialFCheck());

            else if (option.Contains("|"))
                return option.Split('|').Where(x => Game.Data.Triggers.Contains(x.Trim())).Count() > 0;

            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains(">") || oneOption.Contains("<"))
                    {
                        int level = Game.Other.LevelParse(oneOption);

                        if (oneOption.Contains("ПАТРОНЫ >=") && (level > Character.Protagonist.Cartridges))
                            return false;

                        if (oneOption.Contains("ПОДОЗРЕНИЕ >=") && (level > Character.Protagonist.Suspicions))
                            return false;

                        if (oneOption.Contains("ПОДОЗРЕНИЕ <") && (level <= Character.Protagonist.Suspicions))
                            return false;

                        if (oneOption.Contains("ВРЕМЯ >=") && (level > Character.Protagonist.Time))
                            return false;

                        if (oneOption.Contains("ВРЕМЯ <") && (level <= Character.Protagonist.Time))
                            return false;
                    }
                    else if (oneOption.Contains("!"))
                    {
                        if (Game.Data.Triggers.Contains(oneOption.Replace("!", String.Empty).Trim()))
                            return false;
                    }
                    else if (!Game.Data.Triggers.Contains(oneOption.Trim()))
                        return false;
                }

                return true;
            }
        }

        private bool NoMoreEnemies(List<Character> enemies) => enemies.Where(x => x.Hitpoints > 0).Count() == 0;

        private bool NoMoreCartridges(List<Character> enemies) => enemies.Where(x => x.Cartridges > 0).Count() == 0;

        private void HeroWound(Character hero, ref List<string> fight, int target, int wound)
        {
            switch (target)
            {
                case 1:
                    fight.Add("BAD|Ранение пришлось в ногу!");

                    if (hero.Legs > 0)
                    {
                        hero.Legs -= wound;
                        fight.Add(String.Format("BAD|Вы потеряли {0} ед. здоровья ног, теперь оно равно {1} из 4.", wound, hero.Legs));

                        if (hero.Legs <= 0)
                            fight.Add("BOLD|Вы больше не сможете спасаться бегством или прыгать с любой высоты!");
                    }

                    break;

                case 2:
                    fight.Add("BAD|Ранение пришлось в руку!");

                    if (hero.Hands > 0)
                    {
                        hero.Hands -= wound;
                        fight.Add(String.Format("BAD|Вы потеряли {0} ед. здоровья рук, теперь оно равно {1} из 4.", wound, hero.Hands));
                    }

                    if (hero.Accuracy > 0)
                    {
                        hero.Accuracy -= 1;
                        fight.Add(String.Format("BAD|Вы также теряете 1 ед. меткости, теперь она равно {0}.", hero.Accuracy));
                    }

                    break;

                case 3:
                    fight.Add("BAD|Ранение пришлось в корпус!");

                    hero.Body -= wound;
                    fight.Add(String.Format("BAD|Вы потеряли {0} ед. здоровья тела, теперь оно равно {1} из 4.", wound, hero.Body));

                    if (hero.Body <= 0)
                        hero.Dead = true;

                    break;

                case 4:
                    fight.Add("BAD|Ранение пришлось в плечо!");

                    if (hero.ShoulderGirdle > 0)
                    {
                        hero.ShoulderGirdle -= wound;
                        fight.Add(String.Format("Вы потеряли {0} ед. здоровья плеча, теперь оно равно {1} из 4.", wound, hero.ShoulderGirdle));

                        if (hero.ShoulderGirdle <= 0)
                            fight.Add("BOLD|Вы больше не можете наносить ударов и обречены в рукопашной!");
                    }

                    break;

                case 5:
                    fight.Add("BAD|Ранение пришлось в голову!");

                    hero.Head -= wound;
                    fight.Add(String.Format("Вы потеряли {0} ед. здоровья головы, теперь оно равно {1} из 3.", wound, hero.Head));

                    if (hero.Head <= 0)
                        hero.Dead = true;

                    else if (hero.Suspicions < 5)
                    {
                        hero.Suspicions += 1;
                        fight.Add("BOLD|Вы также получаете 1 ед. подозрений, так как скрыть рану не удастся.");
                    }

                    break;
            }
        }

        public List<string> Damage()
        {
            List<string> damage = new List<string>();

            if (Time != 0)
                Character.Protagonist.Time += Time;

            int target = Game.Dice.Roll();

            if (target == 6)
                damage.Add("BIG|GOOD|Вам повезло и вы отделались лёгким испугом! :)");
            else
                HeroWound(Character.Protagonist, ref damage, target, Value);

            return damage;
        }

        public List<string> Cartridges()
        {
            List<string> damage = new List<string>();

            if (Time != 0)
                Character.Protagonist.Time += Time;

            Character.Protagonist.Cartridges += Value;

            damage.Add(String.Format("BIG|GOOD|+{0} патронов, их у вас теперь {1}.", Value, Character.Protagonist.Cartridges));

            return damage;
        }

        public List<string> Bomb()
        {
            if (Time != 0)
                Character.Protagonist.Time += Time;

            Game.Option.Trigger("B");

            return new List<string> { "BIG|GOOD|Бомба теперь у вас." }; ;
        }

        private void HealingAction(ref List<string> healing, string partName, ref int healingPoints, string part)
        {
            if (healingPoints <= 0)
                return;

            int currentValue = (int)Character.Protagonist.GetType().GetProperty(part).GetValue(Character.Protagonist, null);

            int maxHealing = (partName == "головы" ? 3 : 4);

            if (currentValue == maxHealing)
                return;

            int diff = ((maxHealing - currentValue) > healingPoints ? healingPoints : (maxHealing - currentValue));

            healingPoints -= diff;
            currentValue += diff;

            Character.Protagonist.GetType().GetProperty(part).SetValue(Character.Protagonist, currentValue);

            healing.Add(String.Format("Вы восстановили 1 ед. здоровья {0}, теперь оно равно {1} из {2}.", partName, currentValue, maxHealing));
        }

        public List<string> Healing()
        {
            List<string> healing = new List<string>();
            Character hero = Character.Protagonist;

            int healingPoints = Value;

            if (Time != 0)
                Character.Protagonist.Time += Time;

            while (healingPoints > 0)
                foreach (string parts in Constants.healingParts.Keys.ToList())
                    HealingAction(ref healing, parts, ref healingPoints, Constants.healingParts[parts]);

            return healing;
        }

        private bool EnemyAttack(Character hero, Character enemy, ref List<string> fight)
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

            HeroWound(hero, ref fight, target, wound);

            if (hero.Dead)
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

            Character hero = Character.Protagonist;
            
            bool dogFightHero, dogfight = Dogfight || (NoMoreCartridges(FightEnemies) && hero.Cartridges <= 0);
            int damagedWeapon = 2;

            while (true)
            {
                foreach (Character enemy in FightEnemies)
                {
                    if (enemy.Hitpoints <= 0)
                        continue;

                    string cartridgesLine = (dogfight || enemy.Animal ? String.Empty : String.Format(", патронов {0}", enemy.Cartridges));
                    fight.Add(String.Format("BOLD|{0}, здоровье {1}{2}", enemy.Name, enemy.Hitpoints, cartridgesLine));

                    if (enemy.First && EnemyAttack(hero, enemy, ref fight))
                        return fight;

                    if (DamagedWeapon)
                        dogFightHero = (damagedWeapon <= 0);
                    else
                        dogFightHero = Dogfight || (hero.Cartridges <= 0) || (hero.Accuracy <= 0);

                    if (!dogFightHero)
                    {
                        fight.Add(String.Format("Вы стреляете{0}.", DamagedWeapon ? " трижды" : String.Empty));

                        if (DamagedWeapon)
                            damagedWeapon -= 1;
                        else
                            hero.Cartridges -= 1;

                        enemy.Hitpoints -= hero.Accuracy;
                        fight.Add(String.Format("GOOD|Ваш выстрел отнимает у него {0} ед. здоворья.", hero.Accuracy));
                    }
                    else if (hero.ShoulderGirdle <= 0)
                    {
                        fight.Add("Ваши ранения слишком страшны, вы не способны противостоять противнику в этом бою...");
                        hero.Dead = true;

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

                    else if (!enemy.First && EnemyAttack(hero, enemy, ref fight))
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
