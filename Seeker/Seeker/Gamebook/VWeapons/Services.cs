﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.VWeapons
{
    class Services
    {
        public static bool SpecialCheck() =>
            (Game.Option.IsTriggered("Mt") || Game.Option.IsTriggered("P"))
            &&
            !Game.Option.IsTriggered("B") && Character.Protagonist.Suspicions <= 3;

        public static bool SpecialFCheck() =>
            ((Character.Protagonist.Suspicions >= 4) && !Game.Option.IsTriggered("F"));

        public static bool NoMoreEnemies(List<Character> enemies) =>
            enemies.Where(x => x.Hitpoints > 0).Count() == 0;

        public static bool NoMoreCartridges(List<Character> enemies) =>
            enemies.Where(x => x.Cartridges > 0).Count() == 0;

        public static void ProtagonistWound(Character protagonist, ref List<string> fight, int target, int wound)
        {
            switch (target)
            {
                case 1:
                    fight.Add("BAD|Ранение пришлось в ногу!");

                    if (protagonist.Legs > 0)
                    {
                        protagonist.Legs -= wound;

                        fight.Add(String.Format(
                            "BAD|Вы потеряли {0} ед. здоровья ног, теперь оно равно {1} из 4.",
                            wound, protagonist.Legs));

                        if (protagonist.Legs <= 0)
                            fight.Add("BOLD|Вы больше не сможете спасаться бегством или прыгать с любой высоты!");
                    }

                    break;

                case 2:
                    fight.Add("BAD|Ранение пришлось в руку!");

                    if (protagonist.Hands > 0)
                    {
                        protagonist.Hands -= wound;

                        fight.Add(String.Format(
                            "BAD|Вы потеряли {0} ед. здоровья рук, теперь оно равно {1} из 4.",
                            wound, protagonist.Hands));
                    }

                    if (protagonist.Accuracy > 0)
                    {
                        protagonist.Accuracy -= 1;

                        fight.Add(String.Format(
                            "BAD|Вы также теряете 1 ед. меткости, теперь она равно {0}.",
                            protagonist.Accuracy));
                    }

                    break;

                case 3:
                    fight.Add("BAD|Ранение пришлось в корпус!");

                    protagonist.Body -= wound;

                    fight.Add(String.Format(
                        "BAD|Вы потеряли {0} ед. здоровья тела, теперь оно равно {1} из 4.",
                        wound, protagonist.Body));

                    if (protagonist.Body <= 0)
                        protagonist.Dead = true;

                    break;

                case 4:
                    fight.Add("BAD|Ранение пришлось в плечо!");

                    if (protagonist.ShoulderGirdle > 0)
                    {
                        protagonist.ShoulderGirdle -= wound;
                        fight.Add(String.Format(
                            "Вы потеряли {0} ед. здоровья плеча, теперь оно равно {1} из 4.",
                            wound, protagonist.ShoulderGirdle));

                        if (protagonist.ShoulderGirdle <= 0)
                            fight.Add("BOLD|Вы больше не можете наносить ударов и обречены в рукопашной!");
                    }

                    break;

                case 5:
                    fight.Add("BAD|Ранение пришлось в голову!");

                    protagonist.Head -= wound;

                    fight.Add(String.Format(
                        "Вы потеряли {0} ед. здоровья головы, теперь оно равно {1} из 3.",
                        wound, protagonist.Head));

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

        public static bool EnemyAttack(Character protagonist, Character enemy, ref List<string> fight, bool Dogfight)
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
    }
}
