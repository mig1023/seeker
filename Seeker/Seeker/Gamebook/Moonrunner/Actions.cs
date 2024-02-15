using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.Moonrunner
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        public static Actions GetInstance() => StaticInstance;
        private static Character protagonist = Character.Protagonist;

        public List<Character> Enemies { get; set; }

        public int DevastatingAttack { get; set; }
        public int RoundsToFight { get; set; }
        public int WoundsLimit { get; set; }
        public bool ThisIsSkill { get; set; }
        public bool BitesEveryRound { get; set; }
        public bool Invulnerable { get; set; }
        public bool EnemyMasteryInc { get; set; }
        public bool DoubleFail { get; set; }
        public bool ThreeDiceAttack { get; set; }
        public string Stat { get; set; }

        public override List<string> Status() => new List<string>
        {
            $"Мастерство: {protagonist.Mastery}",
            $"Выносливость: {protagonist.Endurance}/{protagonist.MaxEndurance}",
            $"Удача: {protagonist.Luck}",
            $"Золото: {protagonist.Gold}",
        };

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains("|"))
            {
                return option.Split('|').Where(x => Game.Option.IsTriggered(x.Trim())).Count() > 0;
            }
            else if (option.Contains(";"))
            {
                string[] options = option.Split(';');

                int optionMustBe = int.Parse(options[0]);
                string direction = options[1];
                int optionCount = options.Where(x => Game.Option.IsTriggered(x.Trim())).Count();

                switch (direction)
                {
                    case "less":
                        return optionCount < optionMustBe;

                    case "more":
                        return optionCount > optionMustBe;

                    case "exactly":
                    default:
                        return optionCount == optionMustBe;
                }
            }
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains(">") || oneOption.Contains("<"))
                    {
                        int level = Game.Services.LevelParse(oneOption);

                        if (oneOption.Contains("ВЫНОСЛИВОСТЬ <") && (level <= protagonist.Endurance))
                            return false;

                        if (oneOption.Contains("ЗОЛОТО >=") && (level > protagonist.Gold))
                            return false;

                        if (oneOption.Contains("ПРЕДЛОЖЕНИЕ >=") && (level > protagonist.Offer))
                            return false;
                    }
                    else if (oneOption.Contains("!"))
                    {
                        if (Game.Option.IsTriggered(oneOption.Replace("!", String.Empty).Trim()))
                            return false;
                    }
                    else if (!Game.Option.IsTriggered(oneOption.Trim()))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(protagonist.Endurance, out toEndParagraph, out toEndText);

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            if (!String.IsNullOrEmpty(Stat))
            {
                int stat = GetProperty(protagonist, Stat);

                if (secondButton)
                {
                    return stat > 0;
                }
                else
                {
                    return stat < protagonist.Gold;
                }
            }

            bool disabledSkillSlots = ThisIsSkill && (protagonist.SkillSlots < 1);
            bool disabledSkillAlready = ThisIsSkill && Game.Option.IsTriggered(Head);

            return !(disabledSkillSlots || disabledSkillAlready || Used);
        }

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Price > 0)
            {
                string gold = Game.Services.CoinsNoun(Price, "золотой", "золотых", "золотых");
                return new List<string> { $"{Head}, {Price} {gold}" };
            }
            else if (!String.IsNullOrEmpty(Stat))
            {
                int currentStat = GetProperty(protagonist, Stat);
                string diffLine = String.Empty;

                if (currentStat > 0)
                    diffLine = $" (текущее: {currentStat})";

                return new List<string> { $"{Head}{diffLine}" };
            }
            else if (ThisIsSkill)
            {
                return new List<string> { Head };
            }

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
            {
                if (enemy.Endurance > 0)
                {
                    enemies.Add($"{enemy.Name}\nмастерство {enemy.Mastery}  " +
                        $"выносливость {enemy.Endurance}");
                }
                else
                {
                    enemies.Add($"{enemy.Name}\nмастерство {enemy.Mastery} ");
                }
                    
            }

            return enemies;
        }

        public List<string> Get()
        {
            if (!String.IsNullOrEmpty(Stat))
            {
                SetProperty(protagonist, Stat, GetProperty(protagonist, Stat) + 1);
            }
            else if (ThisIsSkill && (protagonist.SkillSlots >= 1))
            {
                Game.Option.Trigger(Head);
                protagonist.SkillSlots -= 1;
            }
            else if ((Price > 0) && (protagonist.Gold >= Price))
            {
                protagonist.Gold -= Price;

                Used = true;

                if (Benefit != null)
                    Benefit.Do();
            }

            return new List<string> { "RELOAD" };
        }

        public List<string> Decrease() => 
            ChangeProtagonistParam(Stat, protagonist, String.Empty, decrease: true);

        public List<string> ThreeDice()
        {
            List<string> dices = new List<string> { };

            int dicesResult = 0;

            for (int i = 1; i <= 3; i++)
            {
                int dice = Game.Dice.Roll();

                dicesResult += dice;

                dices.Add($"На {i} кубикe выпало: {Game.Dice.Symbol(dice)}");
            }

            dices.Add($"BOLD|Итого выпало: {dicesResult}");

            dices.Add(dicesResult > protagonist.Endurance ?
                "BIG|BAD|Больше, чем выносливость :(" : "BIG|GOOD|Меньше, чем выносливость :)");

            return dices;
        }

        public List<string> DiceWounds()
        {
            List<string> wounds = new List<string> { };

            int dice = Game.Dice.Roll();

            wounds.Add($"На кубике выпало: {Game.Dice.Symbol(dice)}");
           
            protagonist.Endurance -= dice * 2;

            wounds.Add($"BIG|BAD|Вы потеряли жизней: {dice * 2}");

            return wounds;
        }

        public List<string> SpellsDice()
        {
            List<string> spell = new List<string> { };

            if (protagonist.EnemySpells <= 0)
            {
                spell.Add("BIG|GOOD|Вы смогли выдержать все его заклятья :)");
                return spell;
            }

            int dice = 0;

            while (true)
            {
                dice = Game.Dice.Roll();

                spell.Add($"На кубике выпало: {Game.Dice.Symbol(dice)}");

                if (Game.Option.IsTriggered(Constants.SpellsList[dice]))
                {
                    spell.Add("Уже было, кидаем ещё раз.");
                }
                else
                {
                    break;
                }
            }

            protagonist.EnemySpells -= dice;

            spell.Add($"Сила Натуры Грула снижается на {dice} и теперь равен {protagonist.EnemySpells}");
            spell.Add($"BIG|BAD|Вам нужно выдержать заклятье: {Constants.SpellsList[dice]}");

            if (Game.Option.IsTriggered("ecproc"))
                spell.Add("Выдержав это заклятье, посмотрите пункт про слово “ecproc”");

            return spell;
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            int round = 1;

            while (true)
            {
                fight.Add($"HEAD|BOLD|Раунд: {round}");

                bool attackAlready = false;
                int protagonistHitStrength = 0;

                if (BitesEveryRound && (protagonist.Endurance > 1))
                {
                    fight.Add("BAD|Из-за укусов вы теряете одну Выносливость!");
                    protagonist.Endurance -= 1;
                }

                foreach (Character enemy in FightEnemies)
                {
                    if ((enemy.Endurance <= 0) && !Invulnerable)
                        continue;

                    if (Invulnerable)
                    {
                        fight.Add(enemy.Name);
                    }
                    else
                    {
                        fight.Add($"{enemy.Name} (выносливость {enemy.Endurance})");
                    }

                    if (!attackAlready)
                    {
                        Game.Dice.DoubleRoll(out int protagonistRollFirst, out int protagonistRollSecond);
                        protagonistHitStrength = protagonistRollFirst + protagonistRollSecond + protagonist.Mastery;

                        fight.Add($"Сила вашего удара: " +
                            $"{Game.Dice.Symbol(protagonistRollFirst)} + " +
                            $"{Game.Dice.Symbol(protagonistRollSecond)} + " +
                            $"{protagonist.Mastery} = {protagonistHitStrength}");
                    }

                    int enemyHitStrength = 0;

                    if (ThreeDiceAttack && !Game.Option.IsTriggered("Акробатика"))
                    {
                        List<int> dices = Fights.TripleDiceRoll(out int failIndex);

                        enemyHitStrength += dices.Sum() - dices[failIndex] + enemy.Mastery;

                        fight.Add($"Сила его удара: " +
                            $"{Game.Dice.Symbol(dices[0])} + " +
                            $"{Game.Dice.Symbol(dices[1])} + " +
                            $"{Game.Dice.Symbol(dices[2])} " +
                            $"(отбрасываем наименьшее значение: {dices[failIndex]}) + " +
                            $"{enemy.Mastery} = {enemyHitStrength}");
                    }
                    else
                    {
                        Game.Dice.DoubleRoll(out int enemyRollFirst, out int enemyRollSecond);
                        enemyHitStrength = enemyRollFirst + enemyRollSecond + enemy.Mastery;

                        fight.Add($"Сила его удара: " +
                            $"{Game.Dice.Symbol(enemyRollFirst)} + " +
                            $"{Game.Dice.Symbol(enemyRollSecond)} + " +
                            $"{enemy.Mastery} = {enemyHitStrength}");

                        if (DoubleFail && (enemyRollFirst == enemyRollSecond))
                        {
                            fight.Add(String.Empty);
                            fight.Add("BOLD|У противника выпал дубль!");
                            return fight;
                        }
                    }

                    if ((protagonistHitStrength > enemyHitStrength) && (!attackAlready || Game.Option.IsTriggered("Сражение")))
                    {
                        if (Invulnerable)
                        {
                            fight.AddRange(Luck.Check(out bool goodLuck));

                            if (goodLuck)
                                return fight;
                        }
                        else
                        {
                            fight.Add($"GOOD|{enemy.Name} ранен");

                            enemy.Endurance -= 2;

                            bool enemyLost = Fights.NoMoreEnemies(FightEnemies, WoundsLimit);

                            if (enemyLost)
                            {
                                fight.Add(String.Empty);
                                fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");
                                return fight;
                            }
                        }
                    }
                    else if (protagonistHitStrength > enemyHitStrength)
                    {
                        fight.Add($"BOLD|{enemy.Name} не смог вас ранить");
                    }
                    else if (protagonistHitStrength < enemyHitStrength)
                    {
                        fight.Add($"BAD|{enemy.Name} ранил вас");

                        protagonist.Endurance -= (DevastatingAttack > 0 ? DevastatingAttack : 2);

                        if (protagonist.Endurance <= 0)
                        {
                            fight.Add(String.Empty);
                            fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");
                            return fight;
                        }

                        if (EnemyMasteryInc)
                        {
                            fight.Add("BOLD|Мастерство противника увеличилось на единицу");
                            enemy.MaxMastery += 1;
                            enemy.Mastery += 1;
                        }
                    }
                    else
                    {
                        fight.Add("BOLD|Ничья в раунде");
                    }

                    attackAlready = true;

                    if ((RoundsToFight > 0) && (RoundsToFight <= round))
                    {
                        fight.Add(String.Empty);
                        fight.Add("BOLD|Отведённые на победу раунды истекли.");
                        return fight;
                    }

                    fight.Add(String.Empty);
                }

                round += 1;
            }
        }

        public override bool IsHealingEnabled() =>
            protagonist.Endurance < protagonist.MaxEndurance;

        public override void UseHealing(int healingLevel) =>
            protagonist.Endurance += healingLevel;
    }
}
