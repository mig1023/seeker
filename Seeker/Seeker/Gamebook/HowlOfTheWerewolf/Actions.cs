using Seeker.Gamebook.HowlOfTheWerewolf.Personages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.HowlOfTheWerewolf
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        public static Actions GetInstance() => StaticInstance;
        private static Character protagonist = Character.Protagonist;

        public enum Specifics
        {
            Nope, ElectricDamage, WitchFight, Ulrich, BlackWidow, Invulnerable, Bats, NeedForSpeed,
            NeedForSpeedAndDead, ToadVenom, IncompleteCorpse, Dehctaw, Moonstone, IcyTouch,
            GlassKnight, AcidDamage, WaterWitch, SnakeFight, Plague, StoneGriffin
        };

        public int Value { get; set; }

        public List<Character> Enemies { get; set; }
        public int RoundsToWin { get; set; }
        public int RoundsWinToWin { get; set; }
        public int RoundsFailToFail { get; set; }
        public int RoundsToFight { get; set; }
        public int WoundsToWin { get; set; }
        public int WoundsToFail { get; set; }
        public int WoundsForTransformation { get; set; }
        public int WoundsLimit { get; set; }
        public int HitStrengthBonus { get; set; }
        public int ExtendedDamage { get; set; }
        public Specifics Specificity { get; set; }

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Price > 0)
            {
                string gold = Game.Services.CoinsNoun(Price, "золотой", "золотых", "золотых");
                return new List<string> { $"{Head}, {Price} {gold}" };
            }

            if (!String.IsNullOrEmpty(Head) || (Type == "Get"))
                return new List<string> { Head };

            if (Type == "WolfFight")
                return new List<string> { "Битва с волками" };

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
            {
                if (enemy.Endurance > 0)
                    enemies.Add($"{enemy.Name}\nмастерство {enemy.Mastery}  выносливость {enemy.Endurance}");
                else
                    enemies.Add($"{enemy.Name}\nмастерство {enemy.Mastery} ");
            }

            return enemies;
        }

        public override List<string> Status() => new List<string>
        {
            $"Мастерство: {protagonist.Mastery}",
            $"Выносливость: {protagonist.Endurance}/{protagonist.MaxEndurance}",
        };

        public override List<string> AdditionalStatus()
        {
            List<string> statusLines = new List<string>
            {
                $"Изменение: {protagonist.Change}",
                $"Золото: {protagonist.Gold}",
                $"Удача: {protagonist.Luck}/{protagonist.MaxLuck}",
            };

            if (protagonist.Crossbow > 0)
                statusLines.Add($"Арбалет: {protagonist.Crossbow}");

            if (protagonist.Gun > 0)
                statusLines.Add($"Пистолет: {protagonist.Gun}");

            if (protagonist.VanRichten > 0)
                statusLines.Add($"Выносливость Ван Рихтена: {protagonist.VanRichten}");

            return statusLines;
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(protagonist.Endurance, out toEndParagraph, out toEndText);

        public List<string> Luck()
        {
            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            bool goodLuck = (firstDice + secondDice) <= protagonist.Luck;
            string luckLine = goodLuck ? "<=" : ">";

            List<string> luckCheck = new List<string> {
                $"Проверка удачи: {Game.Dice.Symbol(firstDice)} + " +
                $"{Game.Dice.Symbol(secondDice)} {luckLine} {protagonist.Luck}" };

            luckCheck.Add(Result(goodLuck, "УСПЕХ|НЕУДАЧА"));

            if (protagonist.Luck > 2)
            {
                protagonist.Luck -= 1;
                luckCheck.Add("Уровень удачи снижен на единицу");
            }

            return luckCheck;
        }

        public List<string> Mastery()
        {
            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            int mastery = (Value > 0 ? Value : protagonist.Mastery);
            bool masteryOk = (firstDice + secondDice) <= mastery;
            string masteryLine = masteryOk ? "<=" : ">";

            List<string> masteryCheck = new List<string> {
                $"Проверка мастерства: {Game.Dice.Symbol(firstDice)} + " +
                $"{Game.Dice.Symbol(secondDice)} {masteryLine} {mastery}" };

            if (Value > 0)
            {
                masteryCheck.Add(Result(!masteryOk, "Мастерства НЕ хватило|Мастерства ХВАТИЛО"));
            }
            else
            {
                masteryCheck.Add(Result(masteryOk, "Мастерства ХВАТИЛО|Мастерства НЕ хватило"));
            }

            return masteryCheck;
        }

        public List<string> Transformation()
        {
            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            int result = firstDice + secondDice;

            string bonusLine = String.Empty;

            if ((Specificity == Specifics.Dehctaw) && Game.Option.IsTriggered("Dehctaw"))
            {
                result -= 2;
                bonusLine = " - 2 за Dehctaw";
            }
            else if ((Specificity == Specifics.Moonstone) && Game.Option.IsTriggered("Лунный камень"))
            {
                result += 3;
                bonusLine = " + 3 за Лунный камень";
            }

            bool changeOk = result > protagonist.Change;
            string cmpLine = Game.Services.Сomparison(result, protagonist.Change);

            List<string> changeCheck = new List<string> {
                $"Проверка: {Game.Dice.Symbol(firstDice)} + " +
                $"{Game.Dice.Symbol(secondDice)}{bonusLine} {cmpLine} " +
                $"{protagonist.Change} изменение" };

            changeCheck.Add(Result(changeOk, "Победил ЧЕЛОВЕК|Победил ВОЛК"));

            return changeCheck;
        }

        public List<string> Dice() =>
            new List<string> { $"BIG|На кубике выпало: {Game.Dice.Symbol(Game.Dice.Roll())}" };

        public List<string> DicesEndurance()
        {
            List<string> diceCheck = new List<string> { };

            int result = 0;

            for (int i = 1; i <= 3; i++)
            {
                int dice = Game.Dice.Roll();
                result += dice;
                diceCheck.Add($"На {i} выпало: {Game.Dice.Symbol(dice)}");
            }

            diceCheck.Add($"BIG|Сумма на кубиках: {result}");

            diceCheck.Add(Result(result < protagonist.Endurance, "Меньше!|Больше"));

            return diceCheck;
        }

        public List<string> DiceAnxiety()
        {
            List<string> diceCheck = new List<string> { };

            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            int result = firstDice + secondDice;

            diceCheck.Add($"На кубиках выпало: {Game.Dice.Symbol(firstDice)} + " +
                $"{Game.Dice.Symbol(firstDice)} = {result}");

            diceCheck.Add($"Текущий уровень тревоги: {protagonist.Anxiety}");

            diceCheck.Add(Result(result > protagonist.Anxiety, "Больше!|Меньше"));

            return diceCheck;
        }

        public List<string> Competition()
        {
            List<string> competition = new List<string> { };

            int penalty = 0;
            string penaltyLine = String.Empty;
            bool inTarget = true;

            for (int i = 1; i <= 3; i++)
            {
                Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
                int result = firstDice + secondDice + penalty;

                if (penalty > 0)
                    penaltyLine = $" + {penalty} пенальти";

                competition.Add($"{i} выстрел: {Game.Dice.Symbol(firstDice)} + " +
                    $"{Game.Dice.Symbol(secondDice)}{penaltyLine} = {result}");

                if (result > protagonist.Mastery)
                {
                    competition.Add("BAD|Это больше Мастерства: вы промахнулись...");
                    inTarget = false;
                }
                else
                {
                    competition.Add("GOOD|Это не превышает Мастерства: вы попали в цель!");
                }

                competition.Add(String.Empty);

                penalty += 1;
            }

            if (inTarget)
            {
                competition.Add("BIG|GOOD|Вы ВЫИГРАЛИ и получаете выигрышь в 5 золотых! :)");
                protagonist.Gold += 5;
            }
            else
            {
                competition.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");
            }

            return competition;
        }

        public List<string> DicesRestore()
        {
            List<string> diceRestore = new List<string> { };

            int dice = Game.Dice.Roll();

            diceRestore.Add($"На кубике выпало: {Game.Dice.Symbol(dice)}");

            string line = "BIG|GOOD|Восстановлен";

            if (dice < 3)
            {
                protagonist.Mastery = protagonist.MaxMastery;
                line += "о Мастерство";
            }
            else if (dice > 4)
            {
                protagonist.Luck = protagonist.MaxLuck;
                line += "а Удача";
            }
            else
            {
                protagonist.Endurance = protagonist.MaxEndurance;
                line += "а Выносливость";
            }

            diceRestore.Add(line);

            return diceRestore;
        }

        public List<string> DiceWounds()
        {
            List<string> diceCheck = new List<string> { };

            int dice = Game.Dice.Roll();

            string bonus = (Value > 0 ? $" + ещё {Value}" : String.Empty);

            diceCheck.Add($"На кубике выпало: {Game.Dice.Symbol(dice)}{bonus}");

            protagonist.Endurance -= dice + Value;

            diceCheck.Add($"BIG|BAD|Вы потеряли жизней: {dice + Value}");

            return diceCheck;
        }

        public List<string> DiceGold()
        {
            List<string> diceCheck = new List<string> { };

            int dice = Game.Dice.Roll();

            diceCheck.Add($"На кубике выпало: {Game.Dice.Symbol(dice)} + ещё {Value}");

            dice += Value;

            protagonist.Gold -= dice;

            diceCheck.Add($"BIG|GOOD|Вы нашли золотых: {dice}");

            return diceCheck;
        }

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            bool disabledByUsed = (Price > 0) && Used;
            bool disabledByPrice = (Price > 0) && (protagonist.Gold < Price);

            return !(disabledByUsed || disabledByPrice);
        }

        public List<string> Get()
        {
            if ((Price > 0) && (protagonist.Gold >= Price))
            {
                protagonist.Gold -= Price;

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
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains(">") || oneOption.Contains("<"))
                    {
                        int level = Game.Services.LevelParse(oneOption);

                        if (option.Contains("ЗОЛОТО >=") && (level > protagonist.Gold))
                            return false;

                        if (option.Contains("КИНЖАЛЫ >=") && (level > protagonist.SilverDaggers))
                            return false;

                        if (option.Contains("КИНЖАЛЫ <") && (level <= protagonist.SilverDaggers))
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

        public List<string> WolfFight()
        {
            List<string> fight = new List<string>();
            Character enemy = Enemies[0];
            Actions action = this;

            Fights.PassageDice(out int dice, out int protagonistPassage);

            fight.Add($"Вы обороняете:" +
                $"{Game.Dice.Symbol(dice)} / 2 = {protagonistPassage}," +
                $"это {Constants.GetPassageName[protagonistPassage]}");

            fight.Add(String.Empty);

            int woulfCount = 0;

            for (int wolf = 1; wolf <= 8; wolf++)
            {
                Fights.PassageDice(out int wolfDice, out int wolfPassage);

                fight.Add($"{wolf} волк: " +
                    $"{Game.Dice.Symbol(wolfDice)} / 2 = {wolfPassage}, " +
                    $"ломится через {Constants.GetPassageName[wolfPassage]}");

                if (protagonistPassage == wolfPassage)
                    woulfCount += 1;
            }

            fight.Add(String.Empty);

            if (woulfCount <= 0)
            {
                fight.Add("GOOD|BIG|Вам повезло: всю работу за вас сделали товарищи :)");
                return fight;
            }
            else
            {
                fight.Add($"BOLD|Вам предстоит сразиться с волками в количестве: {woulfCount}");
            }

            fight.Add(String.Empty);

            Enemies.Clear();

            Paragraphs.EnemyMultiplier(woulfCount, ref action, enemy);

            fight.AddRange(Fight());

            return fight;
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            int round = 1, protagonistWounds = 0, enemyWounds = 0, roundWins = 0, roundFails = 0;
            
            int blackWidowLastAttack = 0;

            bool invulnerable = (Specificity == Specifics.Invulnerable);
            bool speed = ((Specificity == Specifics.NeedForSpeed) || (Specificity == Specifics.NeedForSpeedAndDead));

            if (Specificity == Specifics.Bats)
                RoundsToFight = Fights.MasteryRoundsToFight(ref fight);

            if (Specificity == Specifics.WaterWitch)
                RoundsToWin = Fights.MasteryRoundToWin(ref fight);

            if (Specificity == Specifics.IncompleteCorpse)
                IncompleteCorpse.Specificity(ref FightEnemies, ref fight);

            if ((protagonist.Crossbow > 0) && !invulnerable)
                Fights.CrossbowShot(ref FightEnemies, ref fight, ref enemyWounds);

            bool gunShot = Fights.GunShot(ref FightEnemies, ref fight, ref enemyWounds, WoundsToWin, WoundsLimit);

            if ((protagonist.Gun > 0) && !invulnerable && gunShot)
                return fight;

            while (true)
            {
                fight.Add($"HEAD|BOLD|Раунд: {round}");

                bool attackAlready = false;
                int protagonistHitStrength = 0;

                foreach (Character enemy in FightEnemies)
                {
                    if ((enemy.Endurance <= 0) && !invulnerable)
                        continue;

                    fight.Add($"{enemy.Name} (выносливость {enemy.Endurance})");

                    if (blackWidowLastAttack == 4)
                    {
                        protagonistHitStrength = 0;
                        attackAlready = true;
                    }
                        
                    if (!attackAlready)
                    {
                        Game.Dice.DoubleRoll(out int protagonistRollFirst, out int protagonistRollSecond);
                        protagonistHitStrength = protagonistRollFirst + protagonistRollSecond + protagonist.Mastery + HitStrengthBonus;

                        string bonus = String.Empty;

                        if (HitStrengthBonus > 0)
                        {
                            bonus = $" + {HitStrengthBonus} бонус";
                        }
                        else if (HitStrengthBonus < 0)
                        {
                            bonus = $" - {Math.Abs(HitStrengthBonus)} пенальти";
                        }
                        else if (blackWidowLastAttack == 4)
                        {
                            bonus = " - 1 пенальти за паутину";
                            protagonistHitStrength -= 1;
                        }
                        else if (speed && !Game.Option.IsTriggered("Скорость"))
                        {
                            bonus = " - 1 за остутствие Скорости";
                            protagonistHitStrength -= 1;
                        }

                        fight.Add($"Сила вашего удара: " +
                            $"{Game.Dice.Symbol(protagonistRollFirst)} + " +
                            $"{Game.Dice.Symbol(protagonistRollSecond)} + " +
                            $"{protagonist.Mastery}{bonus} = {protagonistHitStrength}");

                        if ((Specificity == Specifics.SnakeFight) && (round >= 2))
                            HitStrengthBonus = 0;
                    }

                    Game.Dice.DoubleRoll(out int enemyRollFirst, out int enemyRollSecond);
                    int enemyHitStrength = enemyRollFirst + enemyRollSecond + enemy.Mastery;

                    fight.Add($"Сила его удара: " +
                        $"{Game.Dice.Symbol(enemyRollFirst)} + " +
                        $"{Game.Dice.Symbol(enemyRollSecond)} + " +
                        $"{enemy.Mastery} = {enemyHitStrength}");

                    bool webLastAttack = (blackWidowLastAttack == 3);

                    if ((protagonistHitStrength > enemyHitStrength) && !attackAlready && !invulnerable && !webLastAttack)
                    {
                        fight.Add($"GOOD|{enemy.Name} ранен");

                        enemy.Endurance -= (Specificity == Specifics.StoneGriffin ? 1 : 2);

                        roundWins += 1;

                        if ((Specificity == Specifics.GlassKnight) && GlassKnight.Fight(ref fight))
                            enemy.Endurance = 0;

                        if (Fights.EnemyWound(FightEnemies, ref enemyWounds, ref fight, WoundsToWin, WoundsLimit))
                            return fight;
                    }
                    else if (protagonistHitStrength > enemyHitStrength)
                    {
                        fight.Add($"BOLD|{enemy.Name} не смог вас ранить");

                        roundWins += 1;
                    }
                    else if (protagonistHitStrength < enemyHitStrength)
                    {
                        fight.Add($"BAD|{enemy.Name} ранил вас");

                        bool evenHit = (enemyHitStrength % 2 == 0);

                        if (Specificity == Specifics.WitchFight)
                        {
                            Witch.Fight(ref protagonist, ref fight);
                        }
                        else if (Specificity == Specifics.NeedForSpeedAndDead)
                        {
                            if (Werewolf.DeadFight(ref protagonist, ref fight))
                                return fight;
                        }
                        else if (Specificity == Specifics.BlackWidow)
                        {
                            blackWidowLastAttack = BlackWidow.Fight(ref protagonist, ref fight);

                            if (blackWidowLastAttack == 6)
                            {
                                enemy.Endurance -= 2;

                                if (Fights.EnemyWound(FightEnemies, ref enemyWounds, ref fight, WoundsToWin, WoundsLimit))
                                    return fight;
                            }
                        }
                        else if (Specificity == Specifics.SnakeFight)
                        {
                            HitStrengthBonus -= Snake.Fight(ref protagonist, ref fight, round);
                        }
                        else if (Game.Option.IsTriggered("Кольчуга") && evenHit)
                        {
                            fight.Add("Кольчуга защитила вас: вы теряете лишь 1 Выносливость");
                            protagonist.Endurance -= 1;
                        }
                        else
                        {
                            protagonist.Endurance -= (ExtendedDamage > 0 ? ExtendedDamage : 2);
                        }

                        roundFails += 1;
                        protagonistWounds += 1;

                        HitStrengthBonus -= Fights.CheckAdditionalWounds(ref protagonist, ref fight,
                            protagonistWounds, HitStrengthBonus, Specificity);

                        if (protagonistWounds == WoundsForTransformation)
                        {
                            protagonist.Change += 1;

                            fight.Add(String.Empty);
                            fight.Add("BIG|BAD|Трансформация продолжается!");
                            fight.Add($"BAD|Изменение увеличилось на единицу и достигло {protagonist.Change}");
                        }

                        if ((protagonist.Endurance <= 0) || ((WoundsToWin > 0) && (WoundsToWin <= enemyWounds)))
                        {
                            fight.Add(String.Empty);
                            fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");
                            return fight;
                        }
                    }
                    else
                    {
                        fight.Add("BOLD|Ничья в раунде");
                    }

                    attackAlready = true;

                    if (Specificity == Specifics.Ulrich) 
                        enemy.Endurance -= Ulrich.Fight(enemy.Name, ref fight, enemyHitStrength);
                    
                    if (protagonist.VanRichten > 0) 
                        enemy.Endurance -= VanRichten.Fight(enemy.Name, ref fight, enemyHitStrength);

                    if (Fights.EnemyWound(FightEnemies, ref enemyWounds, ref fight, WoundsToWin, WoundsLimit, onlyCheck: true))
                        return fight;

                    bool enoughRounds = (RoundsToFight > 0) && (RoundsToFight <= round);
                    bool notEnoughRounds = (RoundsToWin > 0) && (RoundsToWin <= round);
                    bool enoughRoundsWin = (RoundsWinToWin > 0) && (RoundsWinToWin <= roundWins);
                    bool enoughRoundsFail = (RoundsFailToFail > 0) && (RoundsFailToFail <= roundFails);

                    if (notEnoughRounds || notEnoughRounds || enoughRoundsWin || enoughRoundsFail)
                    {
                        fight.Add(String.Empty);

                        if (notEnoughRounds)
                        {
                            fight.Add("BIG|BAD|Отведённые на победу раунды истекли :(");
                        }
                        else if (enoughRoundsFail)
                        {
                            fight.Add("BIG|BAD|Вы проиграли слишком много раундов :(");
                        }
                        else if (enoughRoundsWin)
                        {
                            fight.Add("BIG|GOOD|Вы выиграли необходимое количество раундов :)");
                        }
                        else
                        {
                            fight.Add("BIG|GOOD|Вы продержались все отведённые на бой раунды :)");
                        }

                        return fight;
                    }

                    fight.Add(String.Empty);
                }

                round += 1;
            }
        }

        public override bool IsHealingEnabled()
        {
            bool enduranceDamage = protagonist.Endurance < protagonist.MaxEndurance;
            bool masteryDamage = protagonist.Mastery < protagonist.MaxMastery;
            bool luckDamage = protagonist.Luck < protagonist.MaxLuck;

            return (enduranceDamage || masteryDamage || luckDamage);
        }

        public override void UseHealing(int healingLevel)
        {
            if (healingLevel == -1)
            {
                protagonist.Endurance = protagonist.MaxEndurance;
            }
            else if (healingLevel == -2)
            {
                protagonist.Mastery = protagonist.MaxMastery;
            }
            else if (healingLevel == -3)
            {
                protagonist.Luck = protagonist.MaxLuck;
            }
            else
            {
                protagonist.Endurance += healingLevel;
            }
        }
    }
}
