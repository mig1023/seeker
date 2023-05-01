using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.FaithfulSwordOfTheKing
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public List<Character> Enemies { get; set; }
        public int RoundsToWin { get; set; }
        public int WoundsToWin { get; set; }
        public int SkillPenalty { get; set; }
        public bool WithoutShooting { get; set; }
        public bool HeroWoundsLimit { get; set; }
        public bool EnemyWoundsLimit { get; set; }

        public Character.MeritalArts? MeritalArt { get; set; }

        public override List<string> Status() => new List<string>
        {
            String.Format("Ловкость: {0}", protagonist.Skill),
            String.Format("Сила: {0}/{1}", protagonist.Strength, protagonist.MaxStrength),
            String.Format("Честь: {0}", protagonist.Honor),

        };

        public override List<string> AdditionalStatus()
        {
            List<string> statusLines = new List<string>();

            statusLines.Add(String.Format("    День: {0}", protagonist.Day));
            statusLines.Add(String.Format("Экю: {0}", Services.ToEcu(protagonist.Ecu)));

            if (protagonist.BulletsAndGubpowder > 0)
                statusLines.Add(String.Format("Выстрелов: {0}", protagonist.BulletsAndGubpowder));

            statusLines.Add(String.Format("Выбранное искусство: {0}", Constants.MeritalArtsNames[protagonist.MeritalArt]));

            return statusLines;
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = String.Empty;

            if (protagonist.Strength <= 0)
                toEndText = "Начать сначала";

            else if (protagonist.Honor <= 0)
            {
                toEndParagraph = 150;
                toEndText = "Задуматься о чести";

                protagonist.Strength = 0;
            }
            else
                return false;

            return true;
        }
        public override bool IsButtonEnabled(bool secondButton = false)
        {
            bool disabledMeritalArtButton =
                (MeritalArt != Character.MeritalArts.Nope) && (protagonist.MeritalArt != Character.MeritalArts.Nope);

            bool disabledGetOptions = (Price > 0) && Used;
            bool disabledByPrice = (Price > 0) && (protagonist.Ecu < Price);

            return !(disabledMeritalArtButton || disabledGetOptions || disabledByPrice);
        }

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains("="))
            {
                int value = int.Parse(option.Split('=')[1]);

                if (option.Contains("ДЕНЬ >=") && (value > protagonist.Day))
                    return false;

                else if (option.Contains("ДЕНЬ =") && (value != protagonist.Day))
                    return false;

                else if (option.Contains("ДЕНЬ <=") && (value < protagonist.Day))
                    return false;

                else if (option.Contains("ЭКЮ >=") && (value > protagonist.Ecu))
                    return false;

                return true;
            }
            else if (Enum.TryParse(option, out Character.MeritalArts value))
            {
                return protagonist.MeritalArt == value;
            }
            else
            {
                return AvailabilityTrigger(option);
            }
        }

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Price > 0)
                return new List<string> { String.Format("{0}, {1} экю", Head, Services.ToEcu(Price)) };

            if (Type == "Get")
                return new List<string> { Head };

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
                enemies.Add(String.Format("{0}\nловкость {1}  сила {2}", enemy.Name, enemy.Skill, enemy.Strength));

            return enemies;
        }

        public List<string> Luck()
        {
            int luckDice = Game.Dice.Roll();

            bool goodLuck = luckDice % 2 == 0;

            List<string> luckCheck = new List<string> { String.Format("Проверка удачи: {0} - {1}",
                Game.Dice.Symbol(luckDice), (goodLuck ? "чётное" : "нечётное")) };

            luckCheck.Add(Result(goodLuck, "УСПЕХ|НЕУДАЧА"));

            return luckCheck;
        }

        public List<string> Skill()
        {
            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            bool goodSkill = (firstDice + secondDice) <= protagonist.Skill;

            List<string> skillCheck = new List<string> { String.Format("Проверка ловкости: {0} + {1} {2} {3} ловкость",
                Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), (goodSkill ? "<=" : ">"), protagonist.Skill) };

            skillCheck.Add(Result(goodSkill, "ЛОВКОСТИ ХВАТИЛО|ЛОВКОСТИ НЕ ХВАТИЛО"));

            return skillCheck;
        }

        public List<string> DicesDoubles()
        {
            List<string> doubleCheck = new List<string>();

            bool doubleFail = false;

            for (int i = 0; i < 2; i++)
            {
                Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
                bool fail = firstDice == secondDice;

                doubleCheck.Add(String.Format("Бросок: {0} и {1} - {2}дубль",
                    Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), (fail ? String.Empty : "НЕ ")));

                if (fail)
                    doubleFail = true;
            }

            doubleCheck.Add(Result(doubleFail, "ВЫПАЛИ|НЕ ВЫПАЛИ"));

            return doubleCheck;
        }

        public List<string> DiceWound()
        {
            List<string> diceWound = new List<string>();

            int wounds = Game.Dice.Roll();

            diceWound.Add(String.Format("Бросок: {0}", Game.Dice.Symbol(wounds)));

            if (wounds < 6)
            {
                protagonist.Strength -= wounds;
                diceWound.Add(String.Format("BIG|BAD|Вы потеряли сил: {0}", wounds));
            }
            else
                diceWound.Add("BIG|BAD|Выпала шестёрка :(");
 
            return diceWound;
        }

        public List<string> DiceDoubleWound()
        {
            List<string> diceCheck = new List<string> { };

            int dices = 0;

            for (int i = 1; i <= 2; i++)
            {
                int dice = Game.Dice.Roll();
                dices += dice;
                diceCheck.Add(String.Format("На {0} выпало: {1}", i, Game.Dice.Symbol(dice)));
            }

            protagonist.Strength -= dices;

            diceCheck.Add(String.Format("BIG|BAD|Вы потеряли жизней: {0}", dices));

            return diceCheck;
        }

        public List<string> FortunetellersAmbush()
        {
            List<string> ambush = new List<string>();

            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            int result = firstDice + secondDice;
            bool evenNumber = result % 2 == 0;

            ambush.Add(String.Format("Кубики: {0} + {1} = {2}", Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), result));

            if (firstDice == secondDice)
                ambush.Add("BIG|BAD|Выпали одинаковые числа :(");
            
            else if (evenNumber)
                ambush.Add("BIG|GOOD|Получилось четное число :)");

            else
                ambush.Add("BIG|BAD|Получилось нечетное число :(");

            return ambush;
        }

        public List<string> Pursuit()
        {
            List<string> pursuit = new List<string>();

            int threeTimesInRow = 0, theyWin = 0;
            
            for (int i = 1; i < 12; i++)
            {
                Game.Dice.DoubleRoll(out int protagonistSpeed, out int enemiesSpeed);

                pursuit.Add(String.Format("Ваша скорость: {0}  <-->  Их скорость: {1}",
                    Game.Dice.Symbol(protagonistSpeed), Game.Dice.Symbol(enemiesSpeed)));

                if (protagonistSpeed > enemiesSpeed)
                {
                    pursuit.Add("GOOD|Вы быстрее");
                    threeTimesInRow = 0;
                }
                else
                {
                    pursuit.Add("BAD|Они быстрее");
                    threeTimesInRow += 1;
                    theyWin += 1;

                    if (threeTimesInRow >= 3)
                    {
                        pursuit.Add(String.Empty);
                        pursuit.Add("BIG|BAD|Они без труда догнали вас :(");

                        return pursuit;
                    }
                }
            }

            pursuit.Add(String.Empty);
            pursuit.Add(Result(theyWin <= 6, "Они не догнали вас|Они догнали вас"));

            return pursuit;
        }

        public List<string> Get()
        {
            if ((MeritalArt != Character.MeritalArts.Nope) && (protagonist.MeritalArt == Character.MeritalArts.Nope))
                protagonist.MeritalArt = MeritalArt ?? Character.MeritalArts.Nope;

            else if ((Price > 0) && (protagonist.Ecu >= Price))
            {
                protagonist.Ecu -= Price;

                if (!Multiple)
                    Used = true;

                if (BenefitList != null)
                {
                    foreach (Modification modification in BenefitList)
                        modification.Do();
                }
            }

            return new List<string> { "RELOAD" };
        }

        private int CountShots()
        {
            bool multiplePistols = (protagonist.Pistols > 1) && (protagonist.BulletsAndGubpowder > 1);

            if (WithoutShooting)
                return 0;
            else if ((protagonist.MeritalArt == Character.MeritalArts.TwoPistols) && multiplePistols)
                return 2;
            else if ((protagonist.Pistols > 0) && (protagonist.BulletsAndGubpowder > 0))
                return 1;
            else
                return 0;
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            int round = 1, enemyWounds = 0;
            int shots = CountShots();

            for (int pistol = 1; pistol <= shots; pistol++)
            {
                if (pistol == 1)
                    fight.Add("Прежде всего пытаемся выстрелить из пистолета.");

                if (Services.NoMoreEnemies(FightEnemies, EnemyWoundsLimit))
                    continue;

                bool hit = Services.LuckyHit(out int shootDice);

                protagonist.BulletsAndGubpowder -= 1;

                string pistolLine = (shots > 1 ? String.Format("{0} ", pistol) : String.Empty);

                fight.Add(String.Format("Выстрел из {0}пистолета: {1} - {2}",
                    pistolLine, Game.Dice.Symbol(shootDice), (hit ? "чёт" : "нечет")));

                if (!hit)
                {
                    fight.Add("BOLD|Вы промахнулись...");
                    continue;
                }

                foreach (Character enemy in FightEnemies.Where(x => x.Strength > 0))
                {
                    fight.Add(String.Format("GOOD|{0} убит", enemy.Name));
                    enemy.Strength = 0;
                    break;
                }
            }

            if (Services.NoMoreEnemies(FightEnemies, EnemyWoundsLimit))
            {
                return fight;
            }
            else if (shots > 0)
            {
                fight.Add(String.Empty);
                fight.Add("LINE|");
            }

            while (true)
            {
                fight.Add(String.Format("HEAD|BOLD|Раунд: {0}", round));

                if ((protagonist.MeritalArt == Character.MeritalArts.SecretBlow) && (round == 1))
                {
                    Character enemy = FightEnemies.Where(x => x.Strength > 0).FirstOrDefault();

                    enemy.Strength -= 4;
                    fight.Add(String.Format("Тайный удар шпагой: {0} теряет 4 силы, у него осталось {1}",
                        enemy.Name, enemy.Strength));

                    fight.Add(String.Empty);
                }

                bool attackAlready = false;
                int protagonistHitStrength = 0, protagonistRoll = 0;

                foreach (Character enemy in FightEnemies)
                {
                    if (enemy.Strength <= 0)
                        continue;

                    Character enemyInFight = enemy;
                    fight.Add(String.Format("{0} (сила {1})", enemy.Name, enemy.Strength));

                    if (!attackAlready)
                    {
                        protagonistRoll = Game.Dice.Roll();

                        int protagonistSkill = protagonist.Skill - SkillPenalty -
                            (protagonist.MeritalArt == Character.MeritalArts.LefthandFencing ? 0 : enemy.LeftHandPenalty);

                        protagonistHitStrength = (protagonistRoll * 2) + protagonistSkill;

                        fight.Add(String.Format("Мощность вашего удара: {0} x 2 + {1} = {2}",
                            Game.Dice.Symbol(protagonistRoll), protagonistSkill, protagonistHitStrength));
                    }

                    int enemyRoll = Game.Dice.Roll();
                    int enemyHitStrength = (enemyRoll * 2) + enemy.Skill;

                    fight.Add(String.Format("Мощность его удара: {0} x 2 + {1} = {2}",
                        Game.Dice.Symbol(enemyRoll), enemy.Skill, enemyHitStrength));

                    if ((protagonistHitStrength > enemyHitStrength) && !attackAlready)
                    {
                        if ((enemy.Chainmail > 0) && (protagonistRoll == 3))
                        {
                            fight.Add(String.Format("BOLD|Кольчуга отразила удар!"));
                        }
                        else
                        {
                            fight.Add(String.Format("GOOD|{0} ранен", enemy.Name));

                            bool enemyWound = Services.EnemyWound(protagonist, ref enemyInFight, FightEnemies,
                                protagonistRoll, WoundsToWin, ref enemyWounds, ref fight, EnemyWoundsLimit);

                            if (enemyWound)
                                return fight;
                        }
                    }
                    else if (protagonistHitStrength > enemyHitStrength)
                    {
                        fight.Add(String.Format("BOLD|{0} не смог вас ранить", enemy.Name));
                    }
                    else if (protagonistHitStrength < enemyHitStrength)
                    {
                        if ((protagonist.Chainmail > 0) && (enemyRoll == 6))
                        {
                            fight.Add(String.Format("BOLD|Кольчуга отразила удар!"));
                        }
                        else
                        {
                            fight.Add(String.Format("BAD|{0} ранил вас", enemy.Name));
                            protagonist.Strength -= 2;

                            if ((protagonist.Strength <= 0) || (HeroWoundsLimit && (protagonist.Strength <= 2)))
                            {
                                fight.Add(String.Empty);
                                fight.Add(String.Format("BIG|BAD|Вы ПРОИГРАЛИ :("));
                                return fight;
                            }

                            bool swordAndDagger = (protagonist.MeritalArt == Character.MeritalArts.SwordAndDagger);

                            if (swordAndDagger && Services.LuckyHit(out _, protagonistRoll))
                            {
                                fight.Add(String.Format("GOOD|{0} ранен вашим кинжалом", enemy.Name));

                                bool wound = Services.EnemyWound(protagonist, ref enemyInFight, FightEnemies,
                                    protagonistRoll, WoundsToWin, ref enemyWounds, ref fight, EnemyWoundsLimit, dagger: true);

                                if (wound)
                                    return fight;
                            }
                        }
                    }
                    else
                    {
                        fight.Add(String.Format("BOLD|Ничья в раунде"));
                    }

                    attackAlready = true;

                    if ((RoundsToWin > 0) && (RoundsToWin <= round))
                    {
                        fight.Add(String.Empty);
                        fight.Add("BAD|Отведённые на победу раунды истекли.");
                        fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");
                        return fight;
                    }

                    fight.Add(String.Empty);
                }

                round += 1;
            }
        }

        public override bool IsHealingEnabled() =>
            protagonist.Strength < protagonist.MaxStrength;

        public override void UseHealing(int healingLevel)
        {
            protagonist.Strength += healingLevel;
            protagonist.HadFoodToday += 1;
        }
    }
}
