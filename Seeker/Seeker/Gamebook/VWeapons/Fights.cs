﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.VWeapons
{
    class Fights
    {
        public static bool NoMoreEnemies(List<Character> enemies) =>
            enemies.Where(x => x.Hitpoints > 0).Count() == 0;

        public static bool NoMoreCartridges(List<Character> enemies) =>
            enemies.Where(x => x.Cartridges > 0).Count() == 0;

        public static void ProtagonistWound(Character protagonist,
            ref List<string> fight, int target, int wound)
        {
            switch (target)
            {
                case 1:
                    fight.Add("BAD|Ранение пришлось в ногу!");

                    if (protagonist.Legs > 0)
                    {
                        protagonist.Legs -= wound;

                        fight.Add($"BAD|Вы потеряли {wound} ед. здоровья ног, " +
                            $"теперь оно равно {protagonist.Legs} из 4.");

                        if (protagonist.Legs <= 0)
                        {
                            fight.Add("BOLD|Вы больше не сможете спасаться " +
                                "бегством или прыгать с любой высоты!");
                        }
                    }

                    break;

                case 2:
                    fight.Add("BAD|Ранение пришлось в руку!");

                    if (protagonist.Hands > 0)
                    {
                        protagonist.Hands -= wound;

                        fight.Add($"BAD|Вы потеряли {wound} ед. здоровья рук, " +
                            $"теперь оно равно {protagonist.Hands} из 4.");
                    }

                    if (protagonist.Accuracy > 0)
                    {
                        protagonist.Accuracy -= 1;

                        fight.Add($"BAD|Вы также теряете 1 ед. меткости, " +
                            $"теперь она равно {protagonist.Accuracy}.");

                        if (protagonist.Accuracy <= 0)
                        {
                            fight.Add("BOLD|Вы больше не можете стрелять, " +
                                "придётся идти в рукопашную!");
                        }
                    }

                    break;

                case 3:
                    fight.Add("BAD|Ранение пришлось в корпус!");

                    protagonist.Body -= wound;

                    fight.Add($"BAD|Вы потеряли {wound} ед. здоровья тела, " +
                        $"теперь оно равно {protagonist.Body} из 4.");

                    if (protagonist.Body <= 0)
                        protagonist.Dead = true;

                    break;

                case 4:
                    fight.Add("BAD|Ранение пришлось в плечо!");

                    if (protagonist.ShoulderGirdle > 0)
                    {
                        protagonist.ShoulderGirdle -= wound;

                        fight.Add($"Вы потеряли {wound} ед. здоровья плеча, " +
                            $"теперь оно равно {protagonist.ShoulderGirdle} из 4.");

                        if (protagonist.ShoulderGirdle <= 0)
                        {
                            fight.Add("BOLD|Вы больше не можете наносить ударов " +
                                "и обречены в рукопашной!");
                        }
                    }

                    break;

                case 5:
                    fight.Add("BAD|Ранение пришлось в голову!");

                    protagonist.Head -= wound;

                    fight.Add($"Вы потеряли {wound} ед. здоровья головы," +
                        $" теперь оно равно {protagonist.Head} из 3.");

                    if (protagonist.Head <= 0)
                    {
                        protagonist.Dead = true;
                    }
                    else if (protagonist.Suspicions < 5)
                    {
                        protagonist.Suspicions += 1;
                        fight.Add("BOLD|Вы также получаете 1 ед. подозрений, " +
                            "так как скрыть рану не удастся.");
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

                string fail = dogfight ? "промедлил" : "промахнулся";
                fight.Add($"{enemy.Name} {fail}!");
                return false;
            }

            if (!dogfight)
            {
                enemy.Cartridges -= 1;
                wound = enemy.Accuracy;
                fight.Add($"{enemy.Name} стреляет в вас.");
            }
            else
            {
                wound = (enemy.Animal ? 2 : 1);
                string action = enemy.Animal ? "кусает" : "бьёт";
                fight.Add($"{enemy.Name} {action} вас.");
            }

            ProtagonistWound(protagonist, ref fight, target, wound);

            if (protagonist.Dead)
            {
                fight.Add(String.Empty);
                fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
