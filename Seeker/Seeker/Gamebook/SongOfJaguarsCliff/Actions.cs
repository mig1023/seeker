﻿using Seeker.Gamebook.CreatureOfHavoc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.SongOfJaguarsCliff
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public List<Character> Enemies { get; set; }

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
            {
                string weapons = String.Join(", ", enemy.Weapons.Select(x => x.Name));

                string name = Constants.PriorityNames[enemy.Priority];
                string priority = String.IsNullOrEmpty(name) ? name : $"\n{name}";

                enemies.Add($"{enemy.Name}{priority}\n" +
                    $"дистанция {enemy.Distance}  здоровье {enemy.Hitpoints}\n{weapons}");
            }

            return enemies;
        }

        private Character ChooseEnemy(Character fighter, List<Character> fighters)
        {
            if (fighter.IsProtagonist)
            {
                Character enemy = fighters
                    .Where(x => !x.IsProtagonist)
                    .Where(x => x.Wounds < x.Hitpoints)
                    .FirstOrDefault();

                return enemy;
            }
            else
            {
                return Character.Protagonist;
            }
        }

        private bool NoMoreEnemy(List<Character> fighters) =>
            fighters.Where(x => !x.IsProtagonist).Where(x => x.Wounds < x.Hitpoints).Count() <= 0;

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            fight.Add("ОЧЕРЁДНОСТЬ БОЯ:");

            List<Character> fighters = Enemies;
            fighters.Add(Character.Protagonist);
            fighters = fighters.OrderBy(x => x.Priority).ToList();

            foreach (Character fighter in fighters)
            {
                string info = fighter.Priority == 3 ? " - пропускает первый ход" : String.Empty;
                fight.Add($"{fighter.Name}{info}");
            }

            int round = 0;

            while (true)
            {
                round += 1;
                fight.Add($"HEAD|BOLD|Раунд {round}");

                foreach (Character fighter in fighters)
                {
                    if (fighter.Wounds >= fighter.Hitpoints)
                    {
                        continue;
                    }

                    fight.Add(String.Empty);

                    if ((round == 1) && (fighter.Priority == 3))
                    {
                        fight.Add($"{fighter.Name} пропускает ход");
                        continue;
                    }

                    Weapon.NextAction action = Weapon.ChooseWeapon(fighter);
                    Character enemy = ChooseEnemy(fighter, fighters);

                    if ((action == Weapon.NextAction.Continue) || (action == Weapon.NextAction.Change))
                    {
                        if (action == Weapon.NextAction.Change)
                            fight.Add($"{fighter.Name} хватается за {fighter.CurrentWeapon.Name}");

                        fight.Add($"{fighter.Name} стреляет в {enemy.Name} из {fighter.CurrentWeapon.Name}");
                        fight.Add($"{enemy.Name} получает {fighter.CurrentWeapon.Damage} ед. урона!!");

                        enemy.Wounds += fighter.CurrentWeapon.Damage;

                        if (enemy.Wounds >= enemy.Hitpoints)
                        {
                            fight.Add($"{enemy.Name} ПОГИБАЕТ!");

                            if (NoMoreEnemy(fighters))
                            {
                                fight.Add($"ВЫ ПОБЕДИЛИ!!");
                                return fight;
                            }
                            else if (Character.Protagonist.Wounds >= Character.Protagonist.Hitpoints)
                            {
                                fight.Add($"ВЫ ПРОИГРАЛИ...");
                                return fight;
                            }
                        }
                    }
                    else if (action == Weapon.NextAction.Recharge)
                    {
                        fight.Add($"{fighter.Name} перезаряжает свой {fighter.CurrentWeapon.Name}");
                    }
                    else if ((action == Weapon.NextAction.GetCloser) || (action == Weapon.NextAction.MoveAway))
                    {
                        int changeDistace = 0;

                        if (action == Weapon.NextAction.GetCloser)
                        {
                            changeDistace = -50;
                            fight.Add($"{fighter.Name} приближается!");
                        }
                        else
                        {
                            changeDistace = 50;
                            fight.Add($"{fighter.Name} отбегает!");
                        } 

                        if (fighter.IsProtagonist)
                        {
                            foreach (Character eachEnemy in fighters)
                                eachEnemy.Distance += changeDistace;
                        }
                        else
                        {
                            fighter.Distance += changeDistace;
                        }
                    }
                }
            }
        }
    }
}
