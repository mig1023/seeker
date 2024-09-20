using Seeker.Gamebook.CreatureOfHavoc;
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

            if (Price > 0)
            {
                string dollars = Game.Services.CoinsNoun(Price, "доллар", "доллара", "долларов");
                return new List<string> { $"{Head}\n{Price} {dollars}" };
            }

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
            {
                string weapons = String.Join(", ", enemy.Weapons.Select(x => x.Name));

                string priority = String.Empty;
                string name = Constants.PriorityNames[enemy.Priority];

                if (!String.IsNullOrEmpty(name))
                    priority = $"\n{name}";

                if (!String.IsNullOrEmpty(enemy.PriorityComment))
                    priority += $" ({enemy.PriorityComment})";

                enemies.Add($"{enemy.Name}{priority}\n" +
                    $"дистанция {enemy.Distance}  здоровье {enemy.Hitpoints}\n{weapons}");
            }

            return enemies;
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(Character.Protagonist.Wounds >= Character.Protagonist.Hitpoints,
                out toEndParagraph, out toEndText);

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else
            {
                int level = Game.Services.LevelParse(option);

                if (option.Contains("ДОЛЛАРЫ >="))
                {
                    return Character.Protagonist.Dollars >= level;
                }
                else if (option.Contains("АВТОРИТЕТ ="))
                {
                    return Character.Protagonist.Authority == level;
                }
                else if (option.Contains("АВТОРИТЕТ !="))
                {
                    return Character.Protagonist.Authority != level;
                }
                else if (option.Contains("АВТОРИТЕТ <="))
                {
                    return Character.Protagonist.Authority <= level;
                }
                else if (option.Contains("АВТОРИТЕТ >="))
                {
                    return Character.Protagonist.Authority >= level;
                }
                else
                {
                    return AvailabilityTrigger(option);
                }
            }
        }

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            if (Price > 0)
            {
                return !Used && (Price <= Character.Protagonist.Dollars);
            }
            else
            {
                return true;
            }
        }

        public List<string> Get()
        {
            if (Character.Protagonist.Dollars >= Price)
            {
                Character.Protagonist.Dollars -= Price;

                if (!Multiple)
                    Used = true;

                if (Benefit != null)
                    Benefit.Do();
            }

            return new List<string> { "RELOAD" };
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
                fight.Add($"HEAD|BOLD|\n*  *  *  РАУНД {round}  *  *  *");

                foreach (Character fighter in fighters)
                {
                    if (fighter.Wounds >= fighter.Hitpoints)
                    {
                        continue;
                    }

                    if ((round == 1) && (fighter.Priority == 3))
                    {
                        fight.Add($"GRAY|{fighter.Name} пропускает ход");
                        continue;
                    }

                    fight.Add(String.Empty);

                    Character enemy = ChooseEnemy(fighter, fighters);
                    Weapon.NextAction action = Weapon.ChooseWeapon(fighter, enemy);

                    if ((action == Weapon.NextAction.Continue) || (action == Weapon.NextAction.Change))
                    {
                        if (action == Weapon.NextAction.Change)
                            fight.Add($"{fighter.Name} использует {fighter.CurrentWeapon.Name}");

                        string act = fighter.CurrentWeapon.ColdWeapon ? "бьёт" : "стреляет в";
                        fight.Add($"BOLD|{fighter.Name} {act} {enemy.Name}");

                        int damage = fighter.CurrentWeapon.Damage;

                        enemy.Wounds += damage;

                        string marker = enemy.IsProtagonist ? "BAD" : "GOOD";
                        string count = Game.Services.CoinsNoun(damage, "единицу", "единицы", "единицы");
                        fight.Add($"{marker}|BOLD|{enemy.Name} получает {damage} {count} урона!");
                        fight.Add($"Теперь у {enemy.Name} ранений: {enemy.Wounds} из {enemy.Hitpoints} возможных");

                        if (enemy.Wounds >= enemy.Hitpoints)
                        {
                            fight.Add($"{marker}|BOLD|{enemy.Name} ПОГИБ!");

                            if (NoMoreEnemy(fighters))
                            {
                                fight.Add($"BIG|BOLD|\nВЫ ПОБЕДИЛИ! :)");
                                return fight;
                            }
                            else if (Character.Protagonist.Wounds >= Character.Protagonist.Hitpoints)
                            {
                                fight.Add($"BIG|BOLD|\nВЫ ПРОИГРАЛИ :(");
                                return fight;
                            }
                        }

                        if (!fighter.CurrentWeapon.ColdWeapon)
                        {
                            fighter.CurrentWeapon.Cartridges -= 1;

                            int cartridges = fighter.CurrentWeapon.Cartridges;
                            string line = Game.Services.CoinsNoun(cartridges, "пуля", "пули", "пуль");

                            fight.Add($"GRAY|В {fighter.CurrentWeapon.Name} у " +
                                $"{fighter.Name} осталась {cartridges} {line}");
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
                            fight.Add($"BOLD|{fighter.Name} приближается!");
                        }
                        else
                        {
                            changeDistace = 50;
                            fight.Add($"BOLD|{fighter.Name} отбегает!");
                        }

                        if (fighter.IsProtagonist)
                        {
                            foreach (Character eachEnemy in fighters)
                                eachEnemy.Distance += changeDistace;

                            string change = changeDistace > 0 ? "увеличилась" : "сократилась";
                            fight.Add($"Теперь дистанция со всеми противниками {change} на 50 ярдов");
                        }
                        else
                        {
                            fighter.Distance += changeDistace;
                            fight.Add($"Теперь дистанция между противниками равна {fighter.Distance} ярдов");
                        }
                    }
                }
            }
        }

        public override bool IsHealingEnabled() =>
            Character.Protagonist.Wounds > 0;

        public override void UseHealing(int healingLevel) =>
            Character.Protagonist.Wounds -= healingLevel;
    }
}
