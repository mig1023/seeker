using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.FaithfulSwordOfTheKing
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
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
            $"Ловкость: {Character.Protagonist.Skill}",
            $"Сила: {Character.Protagonist.Strength}/{Character.Protagonist.MaxStrength}",
            $"Честь: {Character.Protagonist.Honor}",
        };

        private static string ToEcu(int ecu) =>
            String.Format("{0:f2}", (double)ecu / 100).TrimEnd('0').TrimEnd(',').Replace(',', '.');

        public override List<string> AdditionalStatus()
        {
            List<string> statusLines = new List<string>();

            statusLines.Add($"День: {Character.Protagonist.Day}");
            statusLines.Add($"Экю: {ToEcu(Character.Protagonist.Ecu)}");

            if (Character.Protagonist.BulletsAndGubpowder > 0)
                statusLines.Add($"Выстрелов: {Character.Protagonist.BulletsAndGubpowder}");

            statusLines.Add($"Выбранное искусство: {Constants.MeritalArtsNames[Character.Protagonist.MeritalArt]}");

            return statusLines;
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = String.Empty;

            if (Character.Protagonist.Strength <= 0)
            {
                toEndText = Output.Constants.GAMEOVER_TEXT;
            }
            else if (Character.Protagonist.Honor <= 0)
            {
                toEndParagraph = 150;
                toEndText = "Задуматься о чести";

                Character.Protagonist.Strength = 0;
            }
            else
            {
                return false;
            }

            return true;
        }
        public override bool IsButtonEnabled(bool secondButton = false)
        {
            bool disabledMeritalArtButton =
                (MeritalArt != Character.MeritalArts.Nope) && (Character.Protagonist.MeritalArt != Character.MeritalArts.Nope);

            bool disabledGetOptions = (Price > 0) && Used;
            bool disabledByPrice = (Price > 0) && (Character.Protagonist.Ecu < Price);

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

                if (option.Contains("ДЕНЬ >=") && (value > Character.Protagonist.Day))
                    return false;

                else if (option.Contains("ДЕНЬ =") && (value != Character.Protagonist.Day))
                    return false;

                else if (option.Contains("ДЕНЬ <=") && (value < Character.Protagonist.Day))
                    return false;

                else if (option.Contains("ЭКЮ >=") && (value > Character.Protagonist.Ecu))
                    return false;

                return true;
            }
            else if (Enum.TryParse(option, out Character.MeritalArts value))
            {
                return Character.Protagonist.MeritalArt == value;
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
                return new List<string> { $"{Head}, {ToEcu(Price)} экю" };

            if (Type == "Get")
                return new List<string> { Head };

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
                enemies.Add($"{enemy.Name}\nловкость {enemy.Skill}  сила {enemy.Strength}");

            return enemies;
        }

        public List<string> Luck()
        {
            int luckDice = Game.Dice.Roll();

            bool goodLuck = luckDice % 2 == 0;
            string odd = goodLuck ? "чётное" : "нечётное";

            List<string> luckCheck = new List<string> {
                $"Проверка удачи: {Game.Dice.Symbol(luckDice)} - {odd}" };

            luckCheck.Add(Result(goodLuck, "УСПЕХ|НЕУДАЧА"));

            Game.Buttons.Disable(goodLuck, "Повезло", "Не повезло");

            return luckCheck;
        }

        public List<string> Skill()
        {
            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            bool goodSkill = (firstDice + secondDice) <= Character.Protagonist.Skill;
            string compare = goodSkill ? "<=" : ">";

            List<string> skillCheck = new List<string> { $"Проверка ловкости: " +
                $"{Game.Dice.Symbol(firstDice)} + " +
                $"{Game.Dice.Symbol(secondDice)} {compare} " +
                $"{Character.Protagonist.Skill} ловкость" };

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
                string failLine = fail ? String.Empty : "НЕ ";

                doubleCheck.Add($"Бросок: " +
                    $"{Game.Dice.Symbol(firstDice)} и " +
                    $"{Game.Dice.Symbol(secondDice)} - {failLine}дубль");

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

            diceWound.Add($"Бросок: {Game.Dice.Symbol(wounds)}");

            if (wounds < 6)
            {
                Character.Protagonist.Strength -= wounds;
                diceWound.Add($"BIG|BAD|Вы потеряли сил: {wounds}");
            }
            else
            {
                diceWound.Add("BIG|BAD|Выпала шестёрка :(");
            }
 
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
                diceCheck.Add($"На {i} выпало: {Game.Dice.Symbol(dice)}");
            }

            Character.Protagonist.Strength -= dices;

            diceCheck.Add($"BIG|BAD|Вы потеряли жизней: {dices}");

            return diceCheck;
        }

        public List<string> FortunetellersAmbush()
        {
            List<string> ambush = new List<string>();

            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            int result = firstDice + secondDice;
            bool evenNumber = result % 2 == 0;

            ambush.Add($"Кубики: " +
                $"{Game.Dice.Symbol(firstDice)} + " +
                $"{Game.Dice.Symbol(secondDice)} = {result}");

            if (firstDice == secondDice)
            {
                ambush.Add("BIG|BAD|Выпали одинаковые числа :(");
            }
            else if (evenNumber)
            {
                ambush.Add("BIG|GOOD|Получилось четное число :)");
            }
            else
            {
                ambush.Add("BIG|BAD|Получилось нечетное число :(");
            }

            return ambush;
        }

        public List<string> Pursuit()
        {
            List<string> pursuit = new List<string>();

            int threeTimesInRow = 0, theyWin = 0;
            
            for (int i = 1; i < 12; i++)
            {
                Game.Dice.DoubleRoll(out int protagonistSpeed, out int enemiesSpeed);

                pursuit.Add($"Ваша скорость: " +
                    $"{Game.Dice.Symbol(protagonistSpeed)}  <-->  " +
                    $"Их скорость: {Game.Dice.Symbol(enemiesSpeed)}");

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
            if ((MeritalArt != Character.MeritalArts.Nope) && (Character.Protagonist.MeritalArt == Character.MeritalArts.Nope))
            {
                Character.Protagonist.MeritalArt = MeritalArt ?? Character.MeritalArts.Nope;
            }
            else if ((Price > 0) && (Character.Protagonist.Ecu >= Price))
            {
                Character.Protagonist.Ecu -= Price;

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
            bool multiplePistols = (Character.Protagonist.Pistols > 1) && (Character.Protagonist.BulletsAndGubpowder > 1);

            if (WithoutShooting)
            {
                return 0;
            }
            else if ((Character.Protagonist.MeritalArt == Character.MeritalArts.TwoPistols) && multiplePistols)
            {
                return 2;
            }
            else if ((Character.Protagonist.Pistols > 0) && (Character.Protagonist.BulletsAndGubpowder > 0))
            {
                return 1;
            }
            else
            {
                return 0;
            }
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

                if (Fencing.NoMoreEnemies(FightEnemies, EnemyWoundsLimit))
                    continue;

                bool hit = Fencing.LuckyHit(out int shootDice);

                Character.Protagonist.BulletsAndGubpowder -= 1;

                string pistolLine = shots > 1 ? $"{pistol} " : String.Empty;
                string odd = hit ? "чёт" : "нечет";

                fight.Add($"Выстрел из {pistolLine}пистолета: " +
                    $"{Game.Dice.Symbol(shootDice)} - {odd}");

                if (!hit)
                {
                    fight.Add("BOLD|Вы промахнулись...");
                    continue;
                }

                foreach (Character enemy in FightEnemies.Where(x => x.Strength > 0))
                {
                    fight.Add($"GOOD|{enemy.Name} убит");
                    enemy.Strength = 0;
                    break;
                }
            }

            if (Fencing.NoMoreEnemies(FightEnemies, EnemyWoundsLimit))
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
                fight.Add($"HEAD|BOLD|Раунд: {round}");

                if ((Character.Protagonist.MeritalArt == Character.MeritalArts.SecretBlow) && (round == 1))
                {
                    Character enemy = FightEnemies.Where(x => x.Strength > 0).FirstOrDefault();

                    enemy.Strength -= 4;
                    fight.Add($"Тайный удар шпагой: {enemy.Name} теряет 4 силы, " +
                        $"у него осталось {enemy.Strength}");

                    fight.Add(String.Empty);
                }

                bool attackAlready = false;
                int protagonistHitStrength = 0, protagonistRoll = 0;

                foreach (Character enemy in FightEnemies)
                {
                    if (enemy.Strength <= 0)
                        continue;

                    Character enemyInFight = enemy;
                    fight.Add($"{enemy.Name} (сила {enemy.Strength})");

                    if (!attackAlready)
                    {
                        protagonistRoll = Game.Dice.Roll();

                        int protagonistSkill = Character.Protagonist.Skill - SkillPenalty -
                            (Character.Protagonist.MeritalArt == Character.MeritalArts.LefthandFencing ? 0 : enemy.LeftHandPenalty);

                        protagonistHitStrength = (protagonistRoll * 2) + protagonistSkill;

                        fight.Add($"Мощность вашего удара:" +
                            $"{Game.Dice.Symbol(protagonistRoll)} x 2 + " +
                            $"{protagonistSkill} = {protagonistHitStrength}");
                    }

                    int enemyRoll = Game.Dice.Roll();
                    int enemyHitStrength = (enemyRoll * 2) + enemy.Skill;

                    fight.Add($"Мощность его удара: " +
                        $"{Game.Dice.Symbol(enemyRoll)} x 2 + " +
                        $"{enemy.Skill} = {enemyHitStrength}");

                    if ((protagonistHitStrength > enemyHitStrength) && !attackAlready)
                    {
                        if ((enemy.Chainmail > 0) && (protagonistRoll == 3))
                        {
                            fight.Add("BOLD|Кольчуга отразила удар!");
                        }
                        else
                        {
                            fight.Add($"GOOD|{enemy.Name} ранен");

                            bool enemyWound = Fencing.EnemyWound(Character.Protagonist, ref enemyInFight, FightEnemies,
                                protagonistRoll, WoundsToWin, ref enemyWounds, ref fight, EnemyWoundsLimit);

                            if (enemyWound)
                                return fight;
                        }
                    }
                    else if (protagonistHitStrength > enemyHitStrength)
                    {
                        fight.Add($"BOLD|{enemy.Name} не смог вас ранить");
                    }
                    else if (protagonistHitStrength < enemyHitStrength)
                    {
                        if ((Character.Protagonist.Chainmail > 0) && (enemyRoll == 6))
                        {
                            fight.Add("BOLD|Кольчуга отразила удар!");
                        }
                        else
                        {
                            fight.Add($"BAD|{enemy.Name} ранил вас");
                            Character.Protagonist.Strength -= 2;

                            if ((Character.Protagonist.Strength <= 0) || (HeroWoundsLimit && (Character.Protagonist.Strength <= 2)))
                            {
                                fight.Add(String.Empty);
                                fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");
                                return fight;
                            }

                            bool swordAndDagger = (Character.Protagonist.MeritalArt == Character.MeritalArts.SwordAndDagger);

                            if (swordAndDagger && Fencing.LuckyHit(out _, protagonistRoll))
                            {
                                fight.Add($"GOOD|{enemy.Name} ранен вашим кинжалом");

                                bool wound = Fencing.EnemyWound(Character.Protagonist, ref enemyInFight, FightEnemies,
                                    protagonistRoll, WoundsToWin, ref enemyWounds, ref fight, EnemyWoundsLimit, dagger: true);

                                if (wound)
                                    return fight;
                            }
                        }
                    }
                    else
                    {
                        fight.Add("BOLD|Ничья в раунде");
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
            Character.Protagonist.Strength < Character.Protagonist.MaxStrength;

        public override void UseHealing(int healingLevel)
        {
            Character.Protagonist.Strength += healingLevel;
            Character.Protagonist.HadFoodToday += 1;
        }
    }
}
