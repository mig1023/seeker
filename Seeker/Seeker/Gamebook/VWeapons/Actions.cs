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

        public override List<string> Status() => new List<string>
        {
            String.Format("Подозрение: {0}/5", Character.Protagonist.Suspicions),
            String.Format("Время: {0}/12", Character.Protagonist.Time),
            String.Format("Меткость: {0}/5", Character.Protagonist.Accuracy),
            String.Format("Патроны: {0}", Character.Protagonist.Cartridges),
        };

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
                enemies.Add(String.Format("{0}\nметкость {1}  здоровье {2}", enemy.Name, enemy.Accuracy, enemy.Hitpoints));

            return enemies;
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy((Character.Protagonist.Dead ? 0 : 1), out toEndParagraph, out toEndText);

        private bool NoMoreEnemies(List<Character> enemies) => enemies.Where(x => x.Hitpoints > 0).Count() == 0;

        private bool NoMoreCartridges(List<Character> enemies) => enemies.Where(x => x.Cartridges > 0).Count() == 0;

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
                wound = 1;
                fight.Add(String.Format("{0} бьёт вас.", enemy.Name));
            }

            switch (target)
            {
                case 1:
                    fight.Add("BAD|Ранение пришлось в ногу!");

                    if (hero.Legs > 0)
                    {
                        hero.Legs -= wound;
                        fight.Add(String.Format("BAD|Вы потеряли {0} ед. здоровья ног, теперь оно равно {1}.", wound, hero.Legs));

                        if (hero.Legs <= 0)
                            fight.Add("BOLD|Вы больше не сможете спасаться бегством или прыгать с любой высоты!");
                    }

                    break;

                case 2:
                    fight.Add("BAD|Ранение пришлось в руку!");
                    
                    if (hero.Hands > 0)
                    {
                        hero.Hands -= wound;
                        fight.Add(String.Format("BAD|Вы потеряли {0} ед. здоровья рук, теперь оно равно {1}.", wound, hero.Hands));
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
                    fight.Add(String.Format("BAD|Вы потеряли {0} ед. здоровья тела, теперь оно равно {1}.", wound, hero.Body));

                    if (hero.Body <= 0)
                        hero.Dead = true;

                    break;

                case 4:
                    fight.Add("BAD|Ранение пришлось в плечо!");

                    if (hero.ShoulderGirdle > 0)
                    {
                        hero.ShoulderGirdle -= wound;
                        fight.Add(String.Format("Вы потеряли {0} ед. здоровья плеча, теперь оно равно {1}.", wound, hero.ShoulderGirdle));

                        if (hero.ShoulderGirdle <= 0)
                            fight.Add("BOLD|Вы больше не можете наносить ударов и обречены в рукопашной!");
                    }

                    break;

                case 5:
                    fight.Add("BAD|Ранение пришлось в голову!");

                    hero.Head -= wound;
                    fight.Add(String.Format("Вы потеряли {0} ед. здоровья головы, теперь оно равно {1}.", wound, hero.Head));

                    if (hero.Head <= 0)
                        hero.Dead = true;

                    else if (hero.Suspicions < 5)
                    {
                        hero.Suspicions += 1;
                        fight.Add("BOLD|Вы также получаете 1 ед. подозрений, так как скрыть рану не удастся.");
                    }

                    break;
            }

            if (hero.Dead)
            {
                fight.Add(String.Empty);
                fight.Add(String.Format("BIG|BAD|Вы ПРОИГРАЛИ :("));

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

            bool dogFightHero = Dogfight || (hero.Cartridges <= 0) || (hero.Accuracy <= 0);
            bool dogfight = Dogfight || (NoMoreCartridges(FightEnemies) && hero.Cartridges <= 0);

            while (true)
            {
                foreach (Character enemy in FightEnemies)
                {
                    if (enemy.Hitpoints <= 0)
                        continue;

                    string cartridgesLine = (dogfight ? String.Empty : String.Format(", патронов {0}", enemy.Cartridges));
                    fight.Add(String.Format("BOLD|{0}, здоровье {1}{2}", enemy.Name, enemy.Hitpoints, cartridgesLine));

                    if (enemy.First && EnemyAttack(hero, enemy, ref fight))
                        return fight;

                    if (!dogFightHero)
                    {
                        fight.Add("Вы стреляете.");
                        hero.Cartridges -= 1;
                        enemy.Hitpoints -= hero.Accuracy;
                        fight.Add(String.Format("GOOD|Ваш выстрел отнимает у него {0} ед. здоворья.", hero.Accuracy));
                    }
                    else if (hero.ShoulderGirdle <= 0)
                    {
                        fight.Add("Ваши ранения слишком страшны, вы не способны противостоять противнику в этом бою...");
                        hero.Dead = true;

                        fight.Add(String.Empty);
                        fight.Add(String.Format("BIG|BAD|Вы ПРОИГРАЛИ :("));

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
                        fight.Add(String.Format("BIG|GOOD|Вы ПОБЕДИЛИ :)"));
                        return fight;
                    }

                    fight.Add(String.Empty);
                }
            }
        }
    }
}
