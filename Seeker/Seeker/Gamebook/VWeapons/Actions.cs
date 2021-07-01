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
        public bool Firefight { get; set; }

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

        private bool EnemyAttack(Character hero, Character enemy, ref List<string> fight)
        {
            int wound = 0;
            int target = Game.Dice.Roll();

            if (target == 6)
            {
                fight.Add(String.Format("{0} {1}!", enemy.Name, (enemy.Cartridges > 0 ? "промахнулся" : "промедлил")));
                return false;
            }

            if (enemy.Cartridges > 0)
            {
                wound = enemy.Accuracy;
                fight.Add(String.Format("{0} попал в вас!", enemy.Name));
            }
            else
            {
                wound = 1;
                fight.Add(String.Format("{0} бьёт вас!", enemy.Name));
            }

            switch (target)
            {
                case 1:
                    hero.Legs -= wound;
                    fight.Add(String.Format("Ранение пришлось в ногу! Вы потеряли {0} единиц здоровья ног, теперь оно равно {1}", wound, hero.Legs));

                    if (hero.Legs <= 0)
                        fight.Add("Вы больше не сможете спасаться бегством или прыгать с любой высоты!");

                    break;

                case 2:
                    hero.Hands -= wound;
                    fight.Add(String.Format("Ранение пришлось в руку! Вы потеряли {0} единиц здоровья рук, теперь оно равно {1}", wound, hero.Hands));

                    hero.Accuracy -= 1;
                    fight.Add(String.Format("Вы также теряете единицу меткости, теперь она равно {0}", hero.Accuracy));

                    break;

                case 3:
                    hero.Body -= wound;
                    fight.Add(String.Format("Ранение пришлось в корпус! Вы потеряли {0} единиц здоровья тела, теперь оно равно {1}", wound, hero.Body));

                    if (hero.Body <= 0)
                        hero.Dead = true;

                    break;

                case 4:
                    hero.ShoulderGirdle -= wound;
                    fight.Add(String.Format("Ранение пришлось в плечо! Вы потеряли {0} единиц здоровья плеча, теперь оно равно {1}", wound, hero.ShoulderGirdle));

                    if (hero.ShoulderGirdle <= 0)
                        fight.Add("Вы больше не можете наносить ударов и обречены в рукопашной!");

                    break;

                case 5:
                    hero.Head -= wound;
                    fight.Add(String.Format("Ранение пришлось в голову! Вы потеряли {0} единиц здоровья головы, теперь оно равно {1}", wound, hero.Head));

                    hero.Suspicions += 1;
                    fight.Add("Вы также получаете единицу подозрений, так как скрыть рану не удастся");

                    if (hero.Head <= 0)
                        hero.Dead = true;

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

            while (true)
            {
                foreach (Character enemy in FightEnemies)
                {
                    if (enemy.Hitpoints <= 0)
                        continue;

                    string cartridgesLine = (enemy.Cartridges > 0 ? String.Format(", патронов {0}", enemy.Cartridges) : String.Empty);
                    fight.Add(String.Format("{0}{1}", enemy.Name, cartridgesLine));

                    if (enemy.ShootFirst && EnemyAttack(hero, enemy, ref fight))
                        return fight;

                    if (hero.Cartridges > 0)
                    {
                        enemy.Hitpoints -= hero.Accuracy;
                        fight.Add(String.Format("Вы стреляете в {0} и отнимаете у него {1} единиц здоворья", enemy.Name, hero.Accuracy));
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
                        enemy.Hitpoints -= 2;
                        fight.Add(String.Format("Вы бьёте {0} и отнимаете у него 2 единицы здоворья", enemy.Name));
                    }
                    
                    if (!enemy.ShootFirst && (enemy.Hitpoints > 0) && EnemyAttack(hero, enemy, ref fight))
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
