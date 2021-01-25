using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.CreatureOfHavoc
{
    class Actions : Abstract.IActions
    {
        public string ActionName { get; set; }
        public string ButtonName { get; set; }
        public string Aftertext { get; set; }
        public string Trigger { get; set; }

        public List<Character> Enemies { get; set; }
        public Modification Benefit { get; set; }

        public int WoundsToWin { get; set; }
        public int RoundsToWin { get; set; }
        public int RoundsToFight { get; set; }

        public bool Ophidiotaur { get; set; }
        public bool ManicBeast { get; set; }
        public bool GiantHornet { get; set; }

        public List<string> Do(out bool reload, string action = "", bool trigger = false)
        {
            if (trigger)
                Game.Option.Trigger(Trigger);

            string actionName = (String.IsNullOrEmpty(action) ? ActionName : action);
            List<string> actionResult = typeof(Actions).GetMethod(actionName).Invoke(this, new object[] { }) as List<string>;

            reload = (actionResult.Count >= 1) && (actionResult[0] == "RELOAD");

            return actionResult;
        }

        public List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
                enemies.Add(String.Format("{0}\nмастерство {1}  выносливость {2}", enemy.Name, enemy.Mastery, enemy.Endurance));

            return enemies;
        }

        public List<string> Status()
        {
            List<string> statusLines = new List<string>
            {
                String.Format("Мастерство: {0}", Character.Protagonist.Mastery),
                String.Format("Выносливость: {0}", Character.Protagonist.Endurance),
                String.Format("Удачливость: {0}", Character.Protagonist.Luck),
            };

            return statusLines;
        }

        public List<string> AdditionalStatus() => null;

        public List<string> StaticButtons() => new List<string> { };

        public bool StaticAction(string action) => false;

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = "Начать сначала";

            return Character.Protagonist.Endurance <= 0;
        }

        public bool IsButtonEnabled() => true;

        public static bool CheckOnlyIf(string option)
        {
            if (option.Contains("!"))
            {
                if (Game.Data.Triggers.Contains(option.Replace("!", String.Empty).Trim()))
                    return false;
            }
            else if (!Game.Data.Triggers.Contains(option.Trim()))
                return false;

            return true;
        }

        public List<string> Luck() => GoodLuck(out bool _, notInline: true);

        public List<string> GoodLuck(out bool goodLuck, bool notInline = false)
        {
            int fisrtDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();

            goodLuck = (fisrtDice + secondDice) < Character.Protagonist.Luck;

            List<string> luckCheck = new List<string> { String.Format(
                    "Проверка удачи: {0} + {1} {2} {3}",
                    Game.Dice.Symbol(fisrtDice), Game.Dice.Symbol(secondDice), (goodLuck ? "<=" : ">"), Character.Protagonist.Luck
            ) };

            luckCheck.Add((notInline ? String.Empty : "BIG|") + (goodLuck ? "GOOD|УСПЕХ :)" : "BAD|НЕУДАЧА :("));

            if (Character.Protagonist.Luck > 2)
            {
                Character.Protagonist.Luck -= 1;
                luckCheck.Add("Уровень удачи снижен на единицу");
            }

            return luckCheck;
        }

        public List<string> Rocks()
        {
            List<string> rocks = new List<string>();

            for (int i = 0; i < 6; i++)
            {
                int rock = Game.Dice.Roll();

                string inTarget, bold = String.Empty;

                if (rock == 6)
                {
                    Character.Protagonist.Endurance -= 1;
                    inTarget = " - ПОПАЛИ!";
                    bold = "BOLD|";
                }
                else
                    inTarget = "- не попали.";

                rocks.Add(String.Format("{0}Бросок камня: {1}{2}", bold, Game.Dice.Symbol(rock), inTarget));
            }

            Character.Protagonist.Endurance += 3;
            rocks.Add("+3 выносливости за еду");

            return rocks;
        }
        
        public List<string> Hunt()
        {
            List<string> hunt = new List<string>();

            hunt.Add("Пробуете поймать кого-нибудь...");

            hunt.AddRange(GoodLuck(out bool goodLuck));

            string[] huntPray = "птичку, кролика, зайчика, кабанчика, ящерку, мышку, фазанчика".Split(',');

            if (goodLuck)
            {
                Character.Protagonist.Endurance += 2;
                hunt.Add(String.Format("GOOD|Вы поймали {0} и получаете 2 выносливости", huntPray[Game.Dice.Roll() - 1].Trim()));
            }
            else
                hunt.Add("BAD|Вам неудалось никого поймать");

            return hunt;
        }

        public List<string> PoisonOrFood()
        {
            List<string> food = new List<string>();

            food.Add("Пробуете всё на вкус...");

            food.AddRange(GoodLuck(out bool goodLuck));

            if (goodLuck)
            {
                food.Add("GOOD|Вы плотно и без помех поели и получаете 2 выносливости");
                Character.Protagonist.Endurance += 4;
            }
            else
            {
                food.Add("BAD|Вы по незнанию съели горсть ядовитых трав");
                Character.Protagonist.Endurance -= 3;
            }

            return food;
        }

        public List<string> Mastery()
        {
            int firstDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();

            bool goodSkill = (firstDice + secondDice) <= Character.Protagonist.Mastery;

            List<string> skillCheck = new List<string> { String.Format(
                "Проверка мастерства: {0} + {1} {2} {3} мастерство",
                Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), (goodSkill ? "<=" : ">"), Character.Protagonist.Mastery
            ) };

            skillCheck.Add(goodSkill ? "BIG|GOOD|МАСТЕРСТВА ХВАТИЛО :)" : "BIG|BAD|МАСТЕРСТВА НЕ ХВАТИЛО :(");

            return skillCheck;
        }

        private bool WoundAndDeath(ref List<string> fight, ref Character hero, string enemy, int wounds = 2)
        {
            if (wounds == 2)
                fight.Add(String.Format("BAD|{0} ранил вас", enemy));

            hero.Endurance -= wounds;

            if (hero.Endurance <= 0)
            {
                fight.Add(String.Empty);
                fight.Add(String.Format("BIG|BAD|Вы ПРОИГРАЛИ :("));
                return true;
            }
            else
                return false;
        }

        private bool NoMoreEnemies(List<Character> enemies)
        {
            foreach (Character enemy in enemies)
                if (enemy.Endurance > 0)
                    return false;

            return true;
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            int round = 1;
            int enemyWounds = 0;
            bool previousRoundWound = false;

            Character hero = Character.Protagonist;

            while (true)
            {
                fight.Add(String.Format("HEAD|Раунд: {0}", round));

                bool attackAlready = false;
                int protagonistHitStrength = 0;

                foreach (Character enemy in FightEnemies)
                {
                    if (enemy.Endurance <= 0)
                        continue;

                    bool doubleDice = false, doubleSixes = false, doubleDiceEnemy = false;

                    Character enemyInFight = enemy;
                    fight.Add(String.Format("{0} (выносливость {1})", enemy.Name, enemy.Endurance));

                    if (!attackAlready)
                    {
                        int protagonistRollFirst = Game.Dice.Roll();
                        int protagonistRollSecond = Game.Dice.Roll();
                        protagonistHitStrength = protagonistRollFirst + protagonistRollSecond + hero.Mastery;

                        fight.Add(String.Format("Мощность вашего удара: {0} + {1} + {2} = {3}",
                            Game.Dice.Symbol(protagonistRollFirst), Game.Dice.Symbol(protagonistRollSecond), hero.Mastery, protagonistHitStrength
                        ));

                        doubleDice = (protagonistRollFirst == protagonistRollSecond);
                        doubleSixes = doubleDice && (protagonistRollFirst == 6);
                    }

                    int enemyRollFirst = Game.Dice.Roll();
                    int enemyRollSecond = Game.Dice.Roll();
                    int enemyHitStrength = enemyRollFirst + enemyRollSecond + enemy.Mastery;

                    doubleDiceEnemy = (enemyRollFirst == enemyRollSecond);

                    fight.Add(String.Format("Мощность его удара: {0} + {1} + {2} = {3}",
                        Game.Dice.Symbol(enemyRollFirst), Game.Dice.Symbol(enemyRollSecond), enemy.Mastery, enemyHitStrength
                    ));

                    if (ManicBeast && previousRoundWound)
                    {
                        enemyHitStrength += 2;

                        fight.Add(String.Format("+2 бонус к его удару за ярость, итого {0}", enemyHitStrength));

                        previousRoundWound = false;
                    }

                    if (Ophidiotaur && doubleDiceEnemy)
                    {
                        fight.Add("Офидиотавр наносит удар ядовитым жалом");

                        fight.AddRange(GoodLuck(out bool goodLuck));

                        if (goodLuck)
                            fight.Add(String.Format("BOLD|{0} не смог вас ранить", enemy.Name));
                        else if (WoundAndDeath(ref fight, ref hero, enemy.Name))
                            return fight;
                    }
                    else if (GiantHornet && doubleDiceEnemy)
                    {
                        fight.Add("Гигантский наносит удар ядовитым жалом");

                        if (doubleDice)
                        {
                            fight.Add("GOOD|Вы наносите шершню удар Мгновенной Смерти");
                            fight.Add("BAD|Но вы теряете 6 пунктов выносливости");

                            if (WoundAndDeath(ref fight, ref hero, enemy.Name, wounds: 6))
                                return fight;
                            else
                            {
                                fight.Add(String.Empty);
                                fight.Add(String.Format("BIG|GOOD|Вы ПОБЕДИЛИ :)"));
                                return fight;
                            }
                        }
                        else
                        {
                            hero.Endurance = 0;

                            fight.Add(String.Format("BAD|Вы смертельно ранены шершнем"));
                            fight.Add(String.Empty);
                            fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");
                            return fight;
                        }
                    } 
                    else if ((doubleSixes || (protagonistHitStrength > enemyHitStrength)) && !attackAlready)
                    {
                        if (doubleSixes)
                        {
                            fight.Add(String.Format("GOOD|{0} убит наповал", enemy.Name));

                            enemy.Endurance = 0;
                        }
                        else
                        {
                            fight.Add(String.Format("GOOD|{0} ранен", enemy.Name));

                            enemy.Endurance -= 2;
                        }

                        enemyWounds += 1;

                        previousRoundWound = true;

                        bool enemyLost = NoMoreEnemies(FightEnemies);

                        if (enemyLost || ((WoundsToWin > 0) && (WoundsToWin <= enemyWounds)))
                        {
                            fight.Add(String.Empty);
                            fight.Add(String.Format("BIG|GOOD|Вы ПОБЕДИЛИ :)"));
                            return fight;
                        }
                    }
                    else if (protagonistHitStrength > enemyHitStrength)
                    {
                        fight.Add(String.Format("BOLD|{0} не смог вас ранить", enemy.Name));
                    }
                    else if (protagonistHitStrength < enemyHitStrength)
                    {
                        if (WoundAndDeath(ref fight, ref hero, enemy.Name))
                            return fight;
                    }
                    else
                        fight.Add(String.Format("BOLD|Ничья в раунде"));

                    attackAlready = true;

                    if ((RoundsToWin > 0) && (RoundsToWin <= round))
                    {
                        fight.Add(String.Empty);
                        fight.Add("BAD|Отведённые на победу раунды истекли.");
                        fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");
                        return fight;
                    }

                    if ((RoundsToFight > 0) && (RoundsToFight <= round))
                    {
                        fight.Add(String.Empty);
                        fight.Add("BOLD|Отведённые на бой раунды истекли.");
                        return fight;
                    }

                    fight.Add(String.Empty);
                }

                round += 1;
            }
        }
    }
}
